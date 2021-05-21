using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using FileInfo = ECMSS.Data.FileInfo;

namespace ECMSS.Services
{
    public class FileInfoService : IFileInfoService
    {
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IGenericRepository<FileHistory> _fileHistoryRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDirectoryService _directoryService;
        private readonly IMapper _mapper;
        private readonly Expression<Func<FileInfo, object>>[] _includes;

        public FileInfoService(IUnitOfWork unitOfWork, IMapper mapper, IDirectoryService directoryService)
        {
            _unitOfWork = unitOfWork;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
            _fileHistoryRepository = _unitOfWork.FileHistoryRepository;
            _employeeRepository = _unitOfWork.EmployeeRepository;
            _directoryService = directoryService;
            _mapper = mapper;

            _includes = new Expression<Func<FileInfo, object>>[]
            {
                x => x.Employee,
                x => x.FileHistories,
                x => x.FileHistories.Select(h => h.Employee),
                x => x.FileFavorites,
                x => x.FileImportants
            };
        }

        public IEnumerable<FileInfoDTO> GetFileInfosByUserId(int empId)
        {
            var result = _fileInfoRepository.GetMany(x => x.Employee.Id == empId && x.Trashes.Count == 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId)
        {
            var result = _fileInfoRepository.GetMany(x => x.DirectoryId == dirId && x.Trashes.Count == 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public string[] GetFileUrl(int id, int empId, bool isShareUrl)
        {
            string[] result = new string[3];
            var fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories, x => x.Employee);
            var isOwnerOrShared = _fileInfoRepository.GetSingle(x => x.Id == id && (x.Owner == empId ||
                                                                                    x.FileShares.Count(s => s.EmployeeId == empId &&
                                                                                                       s.Permission == CommonConstants.EDIT_PERMISSION) > 0)) != null;

            string filePath = string.Empty;
            if (Debugger.IsAttached)
            {
                filePath = CommonConstants.FILE_UPLOAD_PATH;
            }
            else
            {
                filePath = "http://172.25.216.127:8081/FileSS/";
            }
            filePath += $"{_directoryService.GetDirFromFileId(id).Name}/{fileInfo.Name}";
            string version = fileInfo.FileHistories.OrderByDescending(x => x.Id).FirstOrDefault().Version;
            result[0] = $"<Download>{Encryptor.Encrypt($"</{fileInfo.Id}/></{filePath}/></{fileInfo.Employee.EpLiteId}/></{version}/></true/>")}";
            result[1] = (isOwnerOrShared || isShareUrl) && IsSupportedFile(fileInfo.Name) ? $"<Download>{Encryptor.Encrypt($"</{fileInfo.Id}/></{filePath}/></{fileInfo.Employee.EpLiteId}/></{version}/></false/>")}" : null;
            result[2] = fileInfo.Name;
            return result;
        }

        public string GetFileShareUrl(int id, int empId)
        {
            var fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories, x => x.Employee);
            var isOwner = _fileInfoRepository.GetSingle(x => x.Id == id && (x.Owner == empId)) != null;
            string filePath = string.Empty;
            if (Debugger.IsAttached)
            {
                filePath = CommonConstants.FILE_UPLOAD_PATH;
            }
            else
            {
                filePath = "http://172.25.216.127:8081/FileSS/";
            }
            filePath += $"{_directoryService.GetDirFromFileId(id).Name}/{fileInfo.Name}";
            string url = isOwner ? $"<FileShareUrl>{Encryptor.Encrypt($"</{fileInfo.Id}/>")}" : null;
            return url;
        }

        public FileInfoDTO GetFileInfo(int id)
        {
            var fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, _includes);
            return _mapper.Map<FileInfoDTO>(fileInfo);
        }

        public void UploadNewFile(FileInfoDTO fileInfo)
        {
            try
            {
                fileInfo.Name = StringHelper.RemoveSharpCharacter(fileInfo.Name);
                fileInfo.Owner = _employeeRepository.GetSingle(x => x.EpLiteId == fileInfo.OwnerUser).Id;
                string filePath = CommonConstants.FILE_UPLOAD_PATH;
                filePath += $"{_directoryService.GetDirFromId(fileInfo.DirectoryId).Name}/{fileInfo.Name}";
                _fileInfoRepository.Add(_mapper.Map<FileInfo>(fileInfo));
                FileHistoryDTO fileHistory = new FileHistoryDTO
                {
                    FileId = fileInfo.Id,
                    Modifier = fileInfo.Owner,
                    Size = fileInfo.FileData.Length / 1024,
                    StatusId = 1,
                    Version = "0.1"
                };
                _fileHistoryRepository.Add(_mapper.Map<FileHistory>(fileHistory));
                FileHelper.SaveFile(filePath, fileInfo.FileData);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<FileInfoDTO> GetFavoriteFiles(int empId)
        {
            var result = _fileInfoRepository.GetMany(x => x.FileFavorites.Count(f => f.EmployeeId == empId) > 0 && x.Trashes.Count == 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetImportantFiles(int empId)
        {
            var result = _fileInfoRepository.GetMany(x => x.FileImportants.Count(i => i.EmployeeId == empId) > 0 && x.Trashes.Count == 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> Search(string searchContent)
        {
            searchContent = StringHelper.StringNormalization(searchContent);
            var result = _fileInfoRepository.Find(delegate (FileInfo f)
            {
                if ((StringHelper.StringNormalization(f.Name).IndexOf(searchContent, StringComparison.CurrentCultureIgnoreCase) >= 0 || f.Employee.EpLiteId.Contains(searchContent))
                && f.Trashes.Count == 0)
                {
                    return true;
                }
                return false;
            }, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetDepartmentFiles(int empId)
        {
            var depId = _employeeRepository.GetSingleById(empId).DepartmentId;
            var result = _fileInfoRepository.GetMany(x => x.Employee.DepartmentId == depId && x.Trashes.Count == 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetSharedFiles(int empId)
        {
            var sharedFiles = _fileInfoRepository.GetMany(x => x.FileShares.Count(s => s.EmployeeId == empId) > 0 && x.Trashes.Count == 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(sharedFiles);
        }

        public IEnumerable<FileInfoDTO> GetTrashContents(int empId)
        {
            var sharedFiles = _fileInfoRepository.GetMany(x => x.Employee.Id == empId && x.Trashes.Count > 0, _includes);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(sharedFiles);
        }

        public FileInfoDTO AddNewFile(FileInfoDTO fileInfo)
        {
            try
            {
                if (_fileInfoRepository.CheckContains(x => x.Name == fileInfo.Name && x.DirectoryId == fileInfo.DirectoryId))
                {
                    throw new Exception();
                }
                fileInfo.Name = StringHelper.RemoveSharpCharacter(fileInfo.Name);
                string filePath = CommonConstants.FILE_UPLOAD_PATH;
                filePath += $"{_directoryService.GetDirFromId(fileInfo.DirectoryId).Name}/{fileInfo.Name}";
                var result = _fileInfoRepository.Add(_mapper.Map<FileInfo>(fileInfo));
                FileHistoryDTO fileHistory = new FileHistoryDTO
                {
                    FileId = result.Id,
                    Modifier = result.Owner,
                    Size = fileInfo.FileData.Length / 1024,
                    StatusId = 1,
                    Version = "0.1"
                };
                _fileHistoryRepository.Add(_mapper.Map<FileHistory>(fileHistory));
                FileHelper.SaveFile(filePath, fileInfo.FileData);
                _unitOfWork.Commit();
                return _mapper.Map<FileInfoDTO>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditFileInfo(FileInfoDTO fileInfo)
        {
            try
            {
                var filesInNewLocation = _fileInfoRepository.GetMany(x => x.DirectoryId == fileInfo.DirectoryId);
                if (filesInNewLocation.Where(x => x.Name == fileInfo.Name).Any())
                {
                    throw new Exception();
                }

                var curFile = _fileInfoRepository.GetSingleById(fileInfo.Id);
                int curDirId = curFile.DirectoryId;

                string rootPath = CommonConstants.FILE_UPLOAD_PATH;
                string srcPath = $"{rootPath}{_directoryService.GetDirFromId(curFile.DirectoryId).Name}/{curFile.Name}";
                string desPath = $"{rootPath}{_directoryService.GetDirFromId(fileInfo.DirectoryId).Name}/{curFile.Name}";

                curFile.DirectoryId = fileInfo.DirectoryId;
                curFile.SecurityLevel = fileInfo.SecurityLevel;
                curFile.Tag = fileInfo.Tag;

                _fileInfoRepository.Update(curFile, f => f.DirectoryId, f => f.SecurityLevel, f => f.Tag);
                if (curDirId != fileInfo.DirectoryId)
                {
                    FileHelper.Move(srcPath, desPath);
                }
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSupportedFile(string fileName)
        {
            string[] fileTrackingExtensions = { ".doc", ".docx", ".xls", ".xlsx", ".xlsm", ".csv", ".ppt", ".pptx", ".pdf" };
            var ext = (Path.GetExtension(fileName) ?? string.Empty).ToLower();
            if (fileTrackingExtensions.Any(ext.Equals))
            {
                return true;
            }
            return false;
        }
    }
}