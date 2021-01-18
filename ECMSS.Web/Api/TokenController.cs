using ECMSS.Web.Extensions.Auth;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [AllowAnonymous]
    public class TokenController : ApiController
    {
        public string Get(string epLiteId)
        {
            return JwtManager.GenerateToken(epLiteId);
        }
    }
}