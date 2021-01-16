using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [RoutePrefix("Api/FileInfo")]
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
            var fileInfos = _fileInfoService.GetFileInfos();
            var fileHistory = fileInfos.Select(x => x.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault()).FirstOrDefault();
            var result = fileInfos.Select(x => new
            {
                x.Id,
                x.Name,
                Owner = x.Employee.EpLiteId,
                Modifier = fileHistory.Employee.EpLiteId,
                fileHistory.Size,
                SecurityLevel = "",
                fileHistory.Version,
                fileHistory.ModifiedDate
            });
            return Ok(new { fileInfos = result });
        }

        [Route("GetFileInfosByDirId/{dirId}")]
        [HttpGet]
        public IHttpActionResult GetFileInfosByDirId(int dirId)
        {
            var fileInfos = _fileInfoService.GetFileInfosByDirId(dirId);
            var fileHistory = fileInfos.Select(x => x.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault()).FirstOrDefault();
            var result = fileInfos.Select(x => new
            {
                x.Id,
                x.Name,
                Owner = x.Employee.EpLiteId,
                Modifier = fileHistory.Employee.EpLiteId,
                fileHistory.Size,
                SecurityLevel = "",
                fileHistory.Version,
                fileHistory.ModifiedDate
            });
            return Ok(new { fileInfos = result });
        }

        [Route("GetFileUrl/{id}")]
        [HttpGet]
        public string[] GetFileUrl(int id)
        {
            return _fileInfoService.GetFileUrl(id);
        }

        [HttpPost]
        public HttpResponseMessage UploadNewFile(FileInfoDTO fileInfo)
        {
            try
            {
                _fileInfoService.UploadNewFile(fileInfo);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}