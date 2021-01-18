using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Collections.Generic;
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
            return _directoryService.GetTreeDirectories();
        }
    }
}