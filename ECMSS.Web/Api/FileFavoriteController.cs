using ECMSS.Services.Interfaces;
using ECMSS.Web.Api.Core;
using ECMSS.Web.Extensions.Auth;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileFavoriteController : ApiControllerCore
    {
        private readonly IFileFavoriteService _fileFavoriteService;

        public FileFavoriteController(IFileFavoriteService fileFavoriteService)
        {
            _fileFavoriteService = fileFavoriteService;
        }

        [HttpPost]
        public HttpResponseMessage AddOrRemoveFavoriteFile(int fileId)
        {
            try
            {
                _fileFavoriteService.AddOrRemoveFavoriteFile(fileId, _emp.Id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}