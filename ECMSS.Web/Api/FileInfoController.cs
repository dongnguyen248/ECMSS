using ECMSS.DTO;
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
        public PaginationSet<FileInfoViewModel> GetFileInfos(string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetFileInfosByUserId(empId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public PaginationSet<FileInfoViewModel> GetFileInfosByDirId(int dirId, string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetFileInfosByDirId(dirId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
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
        public PaginationSet<FileInfoViewModel> GetFavoriteFiles(string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetFavoriteFiles(empId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public PaginationSet<FileInfoViewModel> GetImportantFiles(string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetImportantFiles(empId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public PaginationSet<FileInfoViewModel> Search(string searchContent, string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.Search(searchContent, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public PaginationSet<FileInfoViewModel> GetDepartmentFiles(string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetDepartmentFiles(empId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public PaginationSet<FileInfoViewModel> GetSharedFiles(string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetSharedFiles(empId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public PaginationSet<FileInfoViewModel> GetTrashContents(string filterExtension, int page, int pageSize)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            IEnumerable<FileInfoDTO> fileInfos = _fileInfoService.GetTrashContents(empId, filterExtension, page, pageSize, out int totalRow);
            PaginationSet<FileInfoViewModel> pagedSet = new PaginationSet<FileInfoViewModel>()
            {
                PageIndex = page,
                TotalRows = totalRow,
                PageSize = pageSize,
                Items = ConvertToModels(fileInfos, empId)
            };
            return pagedSet;
        }

        [HttpGet]
        public FileInfoViewModel GetFileInfo(Guid id)
        {
            int empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            FileInfoDTO fileInfo = _fileInfoService.GetFileInfo(id);
            FileInfoViewModel fileInfoVM = new FileInfoViewModel(fileInfo, empId);
            return fileInfoVM;
        }

        private IEnumerable<FileInfoViewModel> ConvertToModels(IEnumerable<FileInfoDTO> fileInfos, int empId)
        {
            return fileInfos.Select(x => new FileInfoViewModel(x, empId));
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