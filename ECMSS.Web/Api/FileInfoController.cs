using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using ECMSS.Web.Models;
using System;
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
        public IHttpActionResult GetFileInfos()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
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
        public string[] GetFileUrl(Guid id, bool isShareUrl = false)
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return _fileInfoService.GetFileUrl(id, empId, isShareUrl);
        }

        [HttpGet]
        public string GetFileShareUrl(Guid id)
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return _fileInfoService.GetFileShareUrl(id, empId);
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
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            var fileInfos = _fileInfoService.GetFavoriteFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetImportantFiles()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
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
        public IHttpActionResult GetDepartmentFiles()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            var fileInfos = _fileInfoService.GetDepartmentFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetSharedFiles()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            var fileInfos = _fileInfoService.GetSharedFiles(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public IHttpActionResult GetTrashContents()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            var fileInfos = _fileInfoService.GetTrashContents(empId);
            var result = ConvertToModels(fileInfos);
            return Ok(new { fileInfos = result });
        }

        [HttpGet]
        public FileInfoViewModel GetFileInfo(Guid id)
        {
            var fileInfo = _fileInfoService.GetFileInfo(id);
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            var fileInfoVM = new FileInfoViewModel()
            {
                Id = fileInfo.Id,
                Name = fileInfo.Name,
                Owner = fileInfo.Employee.EpLiteId,
                CreatedDate = fileInfo.CreatedDate,
                Modifier = GetFileHistory(fileInfo).Employee.EpLiteId,
                Size = GetFileHistory(fileInfo).Size,
                SecurityLevel = fileInfo.SecurityLevel,
                Version = GetFileHistory(fileInfo).Version,
                Tag = fileInfo.Tag,
                ModifiedDate = GetFileHistory(fileInfo).ModifiedDate,
                IsFavorite = fileInfo.FileFavorites.Where(f => f.EmployeeId == empId).Count() > 0,
                IsImportant = fileInfo.FileImportants.Where(i => i.EmployeeId == empId).Count() > 0
            };
            return fileInfoVM;
        }

        private IEnumerable<FileInfoViewModel> ConvertToModels(IEnumerable<FileInfoDTO> fileInfos)
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return fileInfos.Select(x => new FileInfoViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Employee.EpLiteId,
                CreatedDate = x.CreatedDate,
                DirectoryId = x.DirectoryId,
                Tag = x.Tag,
                Modifier = GetFileHistory(x).Employee.EpLiteId,
                Size = GetFileHistory(x).Size,
                SecurityLevel = x.SecurityLevel,
                Version = GetFileHistory(x).Version,
                ModifiedDate = GetFileHistory(x).ModifiedDate,
                IsFavorite = x.FileFavorites.Where(f => f.EmployeeId == empId).Count() > 0,
                IsImportant = x.FileImportants.Where(i => i.EmployeeId == empId).Count() > 0
            });
        }

        private FileHistoryDTO GetFileHistory(FileInfoDTO fileInfo)
        {
            return fileInfo.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault();
        }

        [HttpPost]
        public HttpResponseMessage AddFiles(IEnumerable<FileInfoDTO> fileInfos)
        {
            try
            {
                var result = _fileInfoService.AddFiles(fileInfos);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage EditFileInfo(FileInfoDTO fileInfo)
        {
            try
            {
                var result = _fileInfoService.EditFileInfo(fileInfo);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}