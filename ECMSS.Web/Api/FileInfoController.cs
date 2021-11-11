﻿using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using ECMSS.Web.Extensions.Core;
using ECMSS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public IHttpActionResult GetFileInfos(int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetFileInfosByUserId(empId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public IHttpActionResult GetFileInfosByDirId(int dirId, int page, int pageSize)
        {
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetFileInfosByDirId(dirId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public string[] GetFileUrl(Guid id, bool isShareUrl = false)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return _fileInfoService.GetFileUrl(id, empId, isShareUrl);
        }

        [HttpGet]
        public string GetFileShareUrl(Guid id)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
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
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetFavoriteFiles(int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetFavoriteFiles(empId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public IHttpActionResult GetImportantFiles(int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetImportantFiles(empId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public IHttpActionResult Search(string searchContent, int page, int pageSize)
        {
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.Search(searchContent, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public IHttpActionResult GetDepartmentFiles(int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetDepartmentFiles(empId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public IHttpActionResult GetSharedFiles(int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetSharedFiles(empId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public IHttpActionResult GetTrashContents(int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetTrashContents(empId, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos)
            };
            return Ok(new { pagedSet });
        }

        [HttpGet]
        public FileInfoViewModel GetFileInfo(Guid id)
        {
            FileInfoDTO fileInfo = _fileInfoService.GetFileInfo(id);
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            FileInfoViewModel fileInfoVM = new FileInfoViewModel()
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
                IsFavorite = fileInfo.FileFavorites.Where(f => f.EmployeeId == empId).Any(),
                IsImportant = fileInfo.FileImportants.Where(i => i.EmployeeId == empId).Any()
            };
            return fileInfoVM;
        }

        private IEnumerable<FileInfoViewModel> ConvertToModels(IEnumerable<FileInfoDTO> fileInfos)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
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
                IsFavorite = x.FileFavorites.Where(f => f.EmployeeId == empId).Any(),
                IsImportant = x.FileImportants.Where(i => i.EmployeeId == empId).Any()
            });
        }

        private FileHistoryDTO GetFileHistory(FileInfoDTO fileInfo)
        {
            return fileInfo.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault();
        }

        [HttpPost]
        public HttpResponseMessage AddFiles(FileUploadViewModel fileUpload)
        {
            try
            {
                IEnumerable<FileInfoDTO> fileInfos = fileUpload.ConvertToFileInfos(HttpContext.Current.Request.Files);
                List<FileInfoDTO> result = _fileInfoService.AddFiles(fileInfos);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage EditFileInfo(FileInfoDTO fileInfo)
        {
            try
            {
                FileInfoDTO result = _fileInfoService.EditFileInfo(fileInfo);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}