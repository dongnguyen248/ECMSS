using ECMSS.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    public class FileImportantController : ApiController
    {
        private readonly IFileImportantService _fileImportantService;

        public FileImportantController(IFileImportantService fileImportantService)
        {
            _fileImportantService = fileImportantService;
        }

        [HttpPost]
        public HttpResponseMessage AddOrRemoveImportantFile(int fileId, int employeeId)
        {
            try
            {
                _fileImportantService.AddOrRemoveImportantFile(fileId, employeeId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}