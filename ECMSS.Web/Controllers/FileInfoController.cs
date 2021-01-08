using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace ECMSS.Web.Controllers
{
    [RoutePrefix("api/fileinfo")]
    public class FileInfoController : ApiController
    {
        private readonly IFileInfoService _fileInfoService;
        
        public FileInfoController(IFileInfoService fileInfoService)
        {
            _fileInfoService = fileInfoService;
        }

        [Route("")]
        public IEnumerable<FileInfoDTO> GetFileInfos()
        {
            return _fileInfoService.GetFileInfos();
        }

        [Route("getfileurl/{id}")]
        public string GetFileUrl(int id)
        {
            return _fileInfoService.GetFileUrl(id);
        }
    }
}