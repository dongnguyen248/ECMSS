using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http.Controllers;

namespace ECMSS.Web.Extensions.Auth
{
    public static class JwtManager
    {
        private const string SECRET_KEY = "M0JvSkB+TEQ8IWV0V300bFcyUVVuSHZrVmgnajVnWGknUz4hJEc7NkpXUjpVUlJMWHU9U31KYyk2eG9wTkxk";
        private const int EXPIRE_MINUTES = 60 * 24;

        public static string GenerateToken(string epLiteId, int empId, int expireMinutes = EXPIRE_MINUTES)
        {
            byte[] symmetricKey = Convert.FromBase64String(SECRET_KEY);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, epLiteId), new Claim(ClaimTypes.NameIdentifier, empId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                {
                    return null;
                }
                byte[] symmetricKey = Convert.FromBase64String(SECRET_KEY);
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> ExtractFromHeader(HttpActionContext actionContext)
        {
            AuthenticationHeaderValue authRequest = actionContext.Request.Headers.Authorization;
            Dictionary<string, string> empInfo = new Dictionary<string, string>();
            if (authRequest != null)
            {
                string requestToken = authRequest.Parameter;
                empInfo.Add("EpLiteId", actionContext.Request.Headers.Authorization.Parameter);
                empInfo.Add("Id", GetPrincipal(requestToken).Claims.FirstOrDefault(m => m.Type.Contains("nameidentifier")).Value);
            }
            return empInfo;
        }
    }
}