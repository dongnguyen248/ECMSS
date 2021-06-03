using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public IEnumerable<FileShareDTO> GetFileShared(Guid fileId)
        {
            return _fileShareService.GetFileShares(fileId);
        }
    }
}