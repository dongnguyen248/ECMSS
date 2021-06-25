using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileHistoryController : ApiController
    {
        private readonly IFileHistoryService _fileHistoryService;

        public FileHistoryController(IFileHistoryService fileHistoryService)
        {
            _fileHistoryService = fileHistoryService;
        }

        [HttpPost]
        public HttpResponseMessage UploadFile(FileHistoryDTO fileHistory)
        {
            try
            {
                _fileHistoryService.UploadFile(fileHistory);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}