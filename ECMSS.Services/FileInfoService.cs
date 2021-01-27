using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public string[] GetFileUrl(int id, int empId)
        {
            string[] result = new string[3];
            var fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories, x => x.Employee);
            var isOwnerOrShared = _fileInfoRepository.GetSingle(x => x.Id == id && (x.Owner == empId || x.FileShares.Count(s => s.EmployeeId == empId) > 0)) != null;

            string filePath = ConfigHelper.Read("FileUploadPath");
            filePath += $"{_directoryService.GetDirFromFileId(id).Name}/{fileInfo.Name}";
            string version = fileInfo.FileHistories.OrderByDescending(x => x.Id).FirstOrDefault().Version;
            result[0] = $"<Download>[{fileInfo.Id}][{filePath}][{fileInfo.Employee.EpLiteId}][{version}][true]";
            result[1] = isOwnerOrShared ? $"<Download>[{fileInfo.Id}][{filePath}][{fileInfo.Employee.EpLiteId}][{version}][false]" : null;
            result[2] = fileInfo.Name;

            result[0] = Encryptor.Encrypt(result[0]);
            result[1] = result[1] != null ? Encryptor.Encrypt(result[1]) : null;

            return result;
        }

        public void UploadNewFile(FileInfoDTO fileInfo)
        {
            try
            {
                fileInfo.Owner = _employeeRepository.GetSingle(x => x.EpLiteId == fileInfo.OwnerUser).Id;
                string filePath = ConfigHelper.Read("FileUploadPath");
                filePath += $"{_directoryService.GetDirFromId(fileInfo.DirectoryId).Name}/{fileInfo.Name}";
                FileHelper.SaveFile(filePath, fileInfo.FileData);
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
            var result = _fileInfoRepository.GetMany(x => (x.Name.Contains(searchContent) || x.Employee.EpLiteId == searchContent) && x.Trashes.Count == 0, _includes);
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
    }
}