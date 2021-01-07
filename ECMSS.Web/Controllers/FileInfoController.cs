using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace ECMSS.Web.Controllers
{
    public class FileInfoController : ApiController
    {
        private readonly IFileInfoService _fileInfoService;

        public FileInfoController(IFileInfoService fileInfoService)
        {
            _fileInfoService = fileInfoService;
        }

        public IEnumerable<FileInfoDTO> GetFileInfos()
        {
            return _fileInfoService.GetFileInfos();
        }
    }
}