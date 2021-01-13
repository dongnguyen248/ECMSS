using ECMSS.Services.Interfaces;
using System.Linq;
using System.Web.Http;

namespace ECMSS.Web.Api
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
        [HttpGet]
        public IHttpActionResult GetFileInfos()
        {
            var fileInfos = _fileInfoService.GetFileInfos().Select(x => new
            {
                x.Id,
                x.Name,
                SecurityLevel = "",
                x.Employee,
                FileHistory = x.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault()
            });
            return Ok(new { fileInfos });
        }

        [Route("getfileurl/{id}")]
        [HttpGet]
        public string[] GetFileUrl(int id)
        {
            return _fileInfoService.GetFileUrl(id);
        }
    }
}