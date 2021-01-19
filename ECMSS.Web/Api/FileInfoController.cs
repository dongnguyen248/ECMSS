using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using ECMSS.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class FileInfoController : ApiController
    {
        private readonly IFileInfoService _fileInfoService;

        public FileInfoController(IFileInfoService fileInfoService)
        {
            _fileInfoService = fileInfoService;
        }

        [HttpGet]
        public IHttpActionResult GetFileInfosByUserId(int empId)
        {
            var fileInfos = _fileInfoService.GetFileInfosByUserId(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetFileInfosByDirId(int dirId)
        {
            var fileInfos = _fileInfoService.GetFileInfosByDirId(dirId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

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

        [HttpGet]
        public IHttpActionResult GetFavoriteFiles(int empId)
        {
            var fileInfos = _fileInfoService.GetFavoriteFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetImportantFiles(int empId)
        {
            var fileInfos = _fileInfoService.GetImportantFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult Search(string searchContent)
        {
            var fileInfos = _fileInfoService.Search(searchContent);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetDepartmentFiles(int empId)
        {
            var fileInfos = _fileInfoService.GetDepartmentFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetSharedFiles(int empId)
        {
            var fileInfos = _fileInfoService.GetSharedFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }
        
        [HttpGet]
        public IHttpActionResult GetTrashContents(int empId)
        {
            var fileInfos = _fileInfoService.GetTrashContents(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }
        private IEnumerable<FileInfoViewModel> ConvertToModels(IEnumerable<FileInfoDTO> fileInfos)
        {
            var fileHistory = fileInfos.Select(x => x.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault()).FirstOrDefault();
            return fileInfos.Select(x => new FileInfoViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Employee.EpLiteId,
                Modifier = fileHistory.Employee.EpLiteId,
                Size = fileHistory.Size,
                SecurityLevel = "",
                Version = fileHistory.Version,
                ModifiedDate = fileHistory.ModifiedDate,
                IsFavorite = x.FileFavorites.Count > 0,
                IsImportant = x.FileImportants.Count > 0
            });
        }
    }
}