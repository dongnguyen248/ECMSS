using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileImportantController : ApiController
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
                var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
                _fileImportantService.AddOrRemoveImportantFile(fileId, empId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}