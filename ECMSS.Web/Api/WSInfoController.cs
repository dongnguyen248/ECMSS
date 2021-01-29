using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [AllowAnonymous]
    public class WSInfoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage IsOnline()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}