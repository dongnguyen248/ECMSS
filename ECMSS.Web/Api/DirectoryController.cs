using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Api.Core;
using ECMSS.Web.Extensions.Auth;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class DirectoryController : ApiControllerCore
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        [HttpGet]
        public IEnumerable<DirectoryDTO> GetTreeDirectory()
        {
            return _directoryService.GetTreeDirectories();
        }

        [HttpPost]
        public HttpResponseMessage CreateDirectory(string dirName, string path)
        {
            try
            {
                _directoryService.CreateDirectory(dirName, path);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteDirectory(string path)
        {
            try
            {
                _directoryService.DeleteDirectory(_emp.Id, path);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}