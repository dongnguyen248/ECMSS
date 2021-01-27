using ECMSS.Services.Interfaces;
using ECMSS.Web.Api.Core;
using ECMSS.Web.Extensions.Auth;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileImportantController : ApiControllerCore
    {
        private readonly IFileImportantService _fileImportantService;

        public FileImportantController(IFileImportantService fileImportantService)
        {
            _fileImportantService = fileImportantService;
        }

        [HttpPost]
        public HttpResponseMessage AddOrRemoveImportantFile(int fileId)
        {
            try
            {
                _fileImportantService.AddOrRemoveImportantFile(fileId, _emp.Id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}