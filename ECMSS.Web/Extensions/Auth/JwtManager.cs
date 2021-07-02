using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Http.Controllers;

namespace ECMSS.Web.Extensions.Auth
{
    public static class JwtManager
    {
        private const string SECRET_KEY = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private const int EXPIRE_MINUTES = 60 * 24;

        public static string GenerateToken(string epLiteId, int empId, int expireMinutes = EXPIRE_MINUTES)
        {
            var symmetricKey = Convert.FromBase64String(SECRET_KEY);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, epLiteId), new Claim(ClaimTypes.NameIdentifier, empId.ToString()) }),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                {
                    return null;
                }
                var symmetricKey = Convert.FromBase64String(SECRET_KEY);
                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> ExtractFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;
            var authRequest = actionContext.Request.Headers.Authorization;
            var empInfo = new Dictionary<string, string>();
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
                empInfo.Add("EpLiteId", actionContext.Request.Headers.Authorization.Parameter);
                empInfo.Add("Id", GetPrincipal(requestToken).Claims.FirstOrDefault(m => m.Type.Contains("nameidentifier")).Value);
            }
            return empInfo;
        }
    }
}