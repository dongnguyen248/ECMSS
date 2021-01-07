using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Controllers
{
    public class FileHistoryController : ApiController
    {
        private readonly IFileHistoryService _fileHistoryService;

        public FileHistoryController(IFileHistoryService fileHistoryService)
        {
            _fileHistoryService = fileHistoryService;
        }

        [HttpPost]
        public HttpResponseMessage UploadFile(FileHistoryDTO fileHistoryDTO)
        {
            try
            {
                _fileHistoryService.UploadFile(fileHistoryDTO);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}