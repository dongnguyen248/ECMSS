using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileShareController : ApiController
    {
        private readonly IFileShareService _fileShareService;

        public FileShareController(IFileShareService fileShareService)
        {
            _fileShareService = fileShareService;
        }

        [HttpPost]
        public HttpResponseMessage AddFileShares(IEnumerable<FileShareDTO> fileShares)
        {
            try
            {
                _fileShareService.AddFileShares(fileShares);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IEnumerable<FileShareDTO> GetFileShared(Guid fileId)
        {
            return _fileShareService.GetFileShares(fileId);
        }
    }
}