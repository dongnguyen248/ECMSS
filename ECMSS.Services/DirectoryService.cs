using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using ECMSS.Utilities.Constants;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectoryService(IGenericRepository<Directory> directoryRepository, IGenericRepository<Employee> employeeRepository,
                                IUnitOfWork unitOfWork, IMapper mapper)
        {
            _directoryRepository = directoryRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<DirectoryDTO> GetTreeDirectories(int empId)
        {
            int deptId = _employeeRepository.GetSingleById(empId).DepartmentId;
            SqlParameter arg = new SqlParameter { ParameterName = "DeptId", SqlDbType = SqlDbType.Int, Value = deptId };
            IEnumerable<Directory> directories = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromDeptId @DeptId", arg);
            return _mapper.Map<IEnumerable<DirectoryDTO>>(directories);
        }

        public DirectoryDTO GetDirFromFileId(Guid fileId)
        {
            SqlParameter arg = new SqlParameter { ParameterName = "FileId", SqlDbType = SqlDbType.UniqueIdentifier, Value = fileId };
            Directory directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromFileId @FileId", arg).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }

        public DirectoryDTO CreateDirectory(DirectoryDTO directory)
        {
            try
            {
                bool isValidFileName = StringHelper.CheckContainSpecialCharacters(directory.Name);
                if (!isValidFileName)
                {
                    throw new Exception("Special character should not be entered");
                }
                string fullPath = $@"{CommonConstants.FILE_UPLOAD_PATH}{GetDirFromId((int)directory.ParentId).Name}/";
                string directoryName = FileHelper.CreateFolder(fullPath, directory.Name);
                Directory dir = _directoryRepository.Add(new Directory { Name = directoryName, ParentId = directory.ParentId });
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
                DirectoryDTO dir = GetDirFromId(id);
                int role = _employeeRepository.GetSingle(x => x.Id == empId).RoleId;
                if (dir is null || role != CommonConstants.MANAGER_ROLE)
                {
                    throw new Exception("Invalid path or does not have permission to delete this directory");
                }
                if (_directoryRepository.CheckContains(x => x.ParentId == id))
                {
                    throw new Exception("There are subdirectories inside the current directory");
                }
                string fullPath = $@"{CommonConstants.FILE_UPLOAD_PATH}{dir.Name}/";
                _directoryRepository.Remove(dir.Id);
                FileHelper.Empty(fullPath, true);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DirectoryDTO GetDirFromId(int dirId)
        {
            SqlParameter arg = new SqlParameter { ParameterName = "DirId", SqlDbType = SqlDbType.Int, Value = dirId };
            Directory directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromId @DirId", arg).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }
    }
}