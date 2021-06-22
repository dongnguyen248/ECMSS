using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ECMSS.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IGenericRepository<Directory> _directoryRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _directoryRepository = _unitOfWork.DirectoryRepository;
            _employeeRepository = _unitOfWork.EmployeeRepository;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
            _mapper = mapper;
        }

        public IEnumerable<DirectoryDTO> GetTreeDirectories(int empId)
        {
            if (empId == CommonConstants.CEO_USER_ID)
            {
                return _mapper.Map<IEnumerable<DirectoryDTO>>(_directoryRepository.GetAll());
            }
            int deptId = _employeeRepository.GetSingleById(empId).DepartmentId;
            var args = new SqlParameter { ParameterName = "DeptId", SqlDbType = SqlDbType.Int, Value = deptId };
            var directories = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromDeptId @DeptId", args);
            return _mapper.Map<IEnumerable<DirectoryDTO>>(directories);
        }

        public DirectoryDTO GetDirFromFileId(Guid fileId)
        {
            var args = new SqlParameter { ParameterName = "FileId", SqlDbType = SqlDbType.UniqueIdentifier, Value = fileId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromFileId @FileId", args).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }

        public DirectoryDTO GetDirFromId(int dirId)
        {
            var args = new SqlParameter { ParameterName = "DirId", SqlDbType = SqlDbType.Int, Value = dirId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromId @DirId", args).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }

        public DirectoryDTO CreateDirectory(DirectoryDTO directory)
        {
            try
            {
                string fullPath = $@"{CommonConstants.FILE_UPLOAD_PATH}{GetDirFromId((int)directory.ParentId).Name}/";
                var dir = _directoryRepository.Add(new Directory { Name = directory.Name, ParentId = directory.ParentId });
                directory.Name = directory.Name.Trim();
                FileHelper.CreatePath(fullPath, directory.Name);
                _unitOfWork.Commit();
                return _mapper.Map<DirectoryDTO>(dir);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDirectory(int empId, int id)
        {
            try
            {
                var dir = GetDirFromId(id);
                var role = _employeeRepository.GetSingle(x => x.Id == empId).RoleId;
                if (dir is null || role != CommonConstants.MANAGER_ROLE)
                {
                    throw new Exception("Invalid path or does not have permission to delete this directory");
                }
                string fullPath = $@"{CommonConstants.FILE_UPLOAD_PATH}{dir.Name}/";

                _fileInfoRepository.RemoveMulti(x => x.DirectoryId == dir.Id);

                _directoryRepository.RemoveMulti(d => d.ParentId == dir.Id);
                _directoryRepository.Remove(dir.Id);

                FileHelper.Empty(fullPath, true);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}