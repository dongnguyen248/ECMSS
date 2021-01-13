using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace ECMSS.Web.Api
{
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