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
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return _directoryService.GetTreeDirectories(empId);
        }

        [HttpPost]
        public HttpResponseMessage CreateDirectory(DirectoryDTO directory)
        {
            try
            {
                DirectoryDTO dir = _directoryService.CreateDirectory(directory);
                return Request.CreateResponse(HttpStatusCode.OK, dir);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteDirectory(int id)
        {
            try
            {
                int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
                _directoryService.DeleteDirectory(empId, id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}