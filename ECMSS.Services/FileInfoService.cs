using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using ECMSS.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using FileInfo = ECMSS.Data.FileInfo;
using FileShare = ECMSS.Data.FileShare;

namespace ECMSS.Services
{
    public class FileInfoService : IFileInfoService
    {
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IGenericRepository<FileHistory> _fileHistoryRepository;
        private readonly IGenericRepository<FileShare> _fileShareRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IDirectoryService _directoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Expression<Func<FileInfo, object>>[] _includes;

        private const string BEGIN_VERSION = "0.1";
        private const int CREATE_STATUS = 1;

        public FileInfoService(IGenericRepository<FileInfo> fileInfoRepository, IGenericRepository<FileHistory> fileHistoryRepository,
                               IGenericRepository<FileShare> fileShareRepository, IGenericRepository<Employee> employeeRepository,
                               IDirectoryService directoryService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileHistoryRepository = fileHistoryRepository;
            _fileShareRepository = fileShareRepository;
            _employeeRepository = employeeRepository;
            _directoryService = directoryService;
            _unitOfWork = unitOfWork;
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

        public IEnumerable<FileInfoDTO> GetFileInfosByUserId(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.Employee.Id == empId && !x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.DirectoryId == dirId && !x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetFavoriteFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.FileFavorites.Any(f => f.EmployeeId == empId) && !x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetImportantFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.FileImportants.Any(f => f.EmployeeId == empId) && !x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> Search(string searchContent, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            searchContent = StringHelper.StringNormalization(searchContent);
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.Find(delegate (FileInfo f)
            {
                return (StringHelper.StringNormalization(f.Name).IndexOf(searchContent, StringComparison.CurrentCultureIgnoreCase) >= 0 || f.Employee.EpLiteId.Contains(searchContent))
                && !f.Trashes.Any();
            }, _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetDepartmentFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            int deptId = _employeeRepository.GetSingleById(empId).DepartmentId;
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.Employee.DepartmentId == deptId && !x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetSharedFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.FileShares.Any(s => s.EmployeeId == empId) && !x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public IEnumerable<FileInfoDTO> GetTrashContents(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow)
        {
            IQueryable<FileInfo> fileInfos = _fileInfoRepository.GetMany(x => x.Employee.Id == empId && x.Trashes.Any(), _includes).Where(LinqHelper.GetFromType(filterExtension));
            totalRow = fileInfos.Count();
            IQueryable<FileInfo> result = fileInfos.OrderByDescending(x => x.CreatedDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public string[] GetFileUrl(Guid id, int empId, bool isShareUrl)
        {
            string[] result = new string[3];
            FileInfo fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories, x => x.Employee);
            bool isOwnerOrShared = _fileInfoRepository.GetSingle(x => x.Id == id && (x.Owner == empId ||
                                                                                    x.FileShares.Any(s => s.EmployeeId == empId &&
                                                                                                       s.Permission == CommonConstants.EDIT_PERMISSION))) != null;

            string filePath = Env.IS_DEVELOPMENT ? CommonConstants.FILE_UPLOAD_PATH : "http://172.25.216.127:8082/FileSS/";
            filePath += $"{_directoryService.GetDirFromFileId(id).Name}/{fileInfo.Name}";
            string version = fileInfo.FileHistories.OrderByDescending(x => x.Id).FirstOrDefault().Version;
            result[0] = $"<Download>{Encryptor.Encrypt($"</{fileInfo.Id}/></{filePath}/></{fileInfo.Employee.EpLiteId}/></{version}/></true/>")}";
            result[1] = (isOwnerOrShared || isShareUrl) && CheckSupportedFile(fileInfo.Name) ? $"<Download>{Encryptor.Encrypt($"</{fileInfo.Id}/></{filePath}/></{fileInfo.Employee.EpLiteId}/></{version}/></false/>")}" : null;
            result[2] = fileInfo.Name;
            return result;
        }

        public string GetFileShareUrl(Guid id, int empId)
        {
            FileInfo fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories, x => x.Employee);
            bool isOwner = _fileInfoRepository.GetSingle(x => x.Id == id && (x.Owner == empId)) != null;
            string filePath = Env.IS_DEVELOPMENT ? CommonConstants.FILE_UPLOAD_PATH : "http://172.25.216.127:8082/FileSS/";
            filePath += $"{_directoryService.GetDirFromFileId(id).Name}/{fileInfo.Name}";
            string url = isOwner ? $"<FileShareUrl>{Encryptor.Encrypt($"</{fileInfo.Id}/>")}" : null;
            return url;
        }

        public FileInfoDTO GetFileInfo(Guid id)
        {
            FileInfo fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, _includes);
            return _mapper.Map<FileInfoDTO>(fileInfo);
        }

        public void UploadNewFile(FileInfoDTO fileInfo)
        {
            try
            {
                bool isValidFileName = StringHelper.CheckContainSpecialCharacters(fileInfo.Name);
                if (isValidFileName)
                {
                    throw new Exception("Special character should not be entered");
                }
                fileInfo.Owner = _employeeRepository.GetSingle(x => x.EpLiteId == fileInfo.OwnerUser).Id;
                string filePath = CommonConstants.FILE_UPLOAD_PATH;
                filePath += $"{_directoryService.GetDirFromId(fileInfo.DirectoryId).Name}/{fileInfo.Name}";
                _fileInfoRepository.Add(_mapper.Map<FileInfo>(fileInfo));
                FileHistoryDTO fileHistory = new FileHistoryDTO
                {
                    FileId = fileInfo.Id,
                    Modifier = fileInfo.Owner,
                    Size = fileInfo.FileData.Length / 1024,
                    StatusId = CREATE_STATUS,
                    Version = BEGIN_VERSION
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

        public List<FileInfoDTO> AddFiles(IEnumerable<FileInfoDTO> fileInfos)
        {
            List<string> insertedFiles = new List<string>();
            try
            {
                bool isValidFileName = false;
                foreach (FileInfoDTO item in fileInfos)
                {
                    isValidFileName = StringHelper.CheckContainSpecialCharacters(item.Name);
                }
                if (isValidFileName)
                {
                    throw new Exception("Special character should not be entered");
                }
                if (fileInfos.Any(f => f.DirectoryId == 0))
                {
                    throw new Exception("No folder has been selected yet");
                }

                List<FileInfoDTO> files = new List<FileInfoDTO>();
                foreach (FileInfoDTO fi in fileInfos)
                {
                    if (_fileInfoRepository.CheckContains(x => x.Name == fi.Name && x.DirectoryId == fi.DirectoryId))
                    {
                        throw new Exception("There is a file with the same name in the same directory");
                    }
                    string filePath = CommonConstants.FILE_UPLOAD_PATH;
                    filePath += $"{_directoryService.GetDirFromId(fi.DirectoryId).Name}/{fi.Name}";
                    if (fi.FileShares != null)
                    {
                        fi.FileShares.ToList().ForEach(x => { x.FileId = fi.Id; x.Id = Guid.NewGuid(); });
                    }
                    FileHistoryDTO fileHistory = new FileHistoryDTO
                    {
                        FileId = fi.Id,
                        Modifier = fi.Owner,
                        Size = fi.FileData.Length / 1024,
                        StatusId = CREATE_STATUS,
                        Version = BEGIN_VERSION
                    };
                    fi.FileHistories = new FileHistoryDTO[] { fileHistory };
                    _fileInfoRepository.Add(_mapper.Map<FileInfo>(fi));

                    FileHelper.SaveFile(filePath, fi.FileData);
                    insertedFiles.Add(filePath);
                    files.Add(fi);
                }
                _unitOfWork.Commit();
                return files;
            }
            catch (Exception ex)
            {
                foreach (string item in insertedFiles)
                {
                    FileHelper.DeleteFile(item);
                }
                throw ex;
            }
        }

        public FileInfoDTO EditFileInfo(FileInfoDTO fileInfo)
        {
            try
            {
                bool isValidFileName = StringHelper.CheckContainSpecialCharacters(fileInfo.Name);
                if (isValidFileName)
                {
                    throw new Exception("Special character should not be entered");
                }

                FileInfo prevState = _fileInfoRepository.GetSingleById(fileInfo.Id);
                prevState.Name = fileInfo.Name;
                prevState.DirectoryId = fileInfo.DirectoryId;
                prevState.SecurityLevel = fileInfo.SecurityLevel;
                prevState.Tag = fileInfo.Tag;

                bool isChangedLocation = fileInfo.DirectoryId != prevState.DirectoryId || fileInfo.Name != prevState.Name;
                string srcPath = $"{CommonConstants.FILE_UPLOAD_PATH}{_directoryService.GetDirFromId(prevState.DirectoryId).Name}/{prevState.Name}";
                string desPath = $"{CommonConstants.FILE_UPLOAD_PATH}{_directoryService.GetDirFromId(fileInfo.DirectoryId).Name}/{fileInfo.Name}";

                _fileInfoRepository.Update(prevState);

                _fileShareRepository.RemoveMulti(x => x.FileId == fileInfo.Id);
                _fileShareRepository.AddRange(_mapper.Map<IEnumerable<FileShare>>(fileInfo.FileShares));

                if (isChangedLocation)
                {
                    IEnumerable<FileInfo> filesInDesLocation = _fileInfoRepository.GetMany(x => x.DirectoryId == fileInfo.DirectoryId);
                    if (filesInDesLocation.Where(x => x.Name == fileInfo.Name).Any())
                    {
                        throw new Exception("There is a file with the same name in the same directory");
                    }
                    FileHelper.Move(srcPath, desPath);
                }
                _unitOfWork.Commit();
                return _mapper.Map<FileInfoDTO>(prevState);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckSupportedFile(string fileName)
        {
            string[] fileTrackingExtensions = { ".doc", ".docx", ".xls", ".xlt", ".xlsx", ".xlsm", ".xlsb", ".xltx", ".xltm", ".csv", ".ppt", ".pptx" };
            string ext = (Path.GetExtension(fileName) ?? string.Empty).ToLower();
            return fileTrackingExtensions.Any(ext.Equals);
        }
    }
}