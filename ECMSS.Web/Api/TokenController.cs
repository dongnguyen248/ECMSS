using ECMSS.Web.Extensions.Auth;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [AllowAnonymous]
    public class TokenController : ApiController
    {
        public string GetToken(string epLiteId)
        {
            string token = JwtManager.GenerateToken(epLiteId);
            var user = JwtManager.GetPrincipal(token);
            return token;
        }
    }
}