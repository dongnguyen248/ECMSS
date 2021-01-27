using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Api.Core;
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
    public class FileInfoController : ApiControllerCore
    {
        private readonly IFileInfoService _fileInfoService;

        public FileInfoController(IFileInfoService fileInfoService)
        {
            _fileInfoService = fileInfoService;
        }

        [HttpGet]
        public IHttpActionResult GetFileInfos()
        {
            var fileInfos = _fileInfoService.GetFileInfosByUserId(_emp.Id);
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
            return _fileInfoService.GetFileUrl(id, _emp.Id);
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
        public IHttpActionResult GetFavoriteFiles()
        {
            var fileInfos = _fileInfoService.GetFavoriteFiles(_emp.Id);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetImportantFiles()
        {
            var fileInfos = _fileInfoService.GetImportantFiles(_emp.Id);
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
        public IHttpActionResult GetDepartmentFiles()
        {
            var fileInfos = _fileInfoService.GetDepartmentFiles(_emp.Id);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetSharedFiles()
        {
            var fileInfos = _fileInfoService.GetSharedFiles(_emp.Id);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetTrashContents()
        {
            var fileInfos = _fileInfoService.GetTrashContents(_emp.Id);
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
                Modifier = GetFileHistory(x).Employee.EpLiteId,
                Size = GetFileHistory(x).Size,
                SecurityLevel = "",
                Version = GetFileHistory(x).Version,
                ModifiedDate = GetFileHistory(x).ModifiedDate,
                IsFavorite = x.FileFavorites.Count > 0,
                IsImportant = x.FileImportants.Count > 0
            });
        }

        private FileHistoryDTO GetFileHistory(FileInfoDTO fileInfo)
        {
            return fileInfo.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault();
        }
    }
}