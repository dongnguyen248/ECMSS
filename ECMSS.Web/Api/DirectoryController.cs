using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class DirectoryController : ApiController
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        [HttpGet]
        public IEnumerable<DirectoryDTO> GetTreeDirectory()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return _directoryService.GetTreeDirectories(empId);
        }

        [HttpGet]
        public DirectoryDTO GetDirectoryFromPath(string path)
        {
            return _directoryService.GetDirFromPath(path);
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
                var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
                _directoryService.DeleteDirectory(empId, path);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public string GetPathFromFileId(int fileId)
        {
            var directory = _directoryService.GetDirFromFileId(fileId);
            return directory.Name;
        }
    }
}