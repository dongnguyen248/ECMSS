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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _directoryRepository = _unitOfWork.DirectoryRepository;
            _employeeRepository = _unitOfWork.EmployeeRepository;
            _mapper = mapper;
        }

        public IEnumerable<DirectoryDTO> GetTreeDirectories()
        {
            var res = _mapper.Map<IEnumerable<DirectoryDTO>>(_directoryRepository.GetAll());
            return res;
        }

        public DirectoryDTO GetDirFromFileId(int fileId)
        {
            var args = new SqlParameter { ParameterName = "FileId", SqlDbType = SqlDbType.Int, Value = fileId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromFileId @FileId", args).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }

        public DirectoryDTO GetDirFromId(int dirId)
        {
            var args = new SqlParameter { ParameterName = "DirId", SqlDbType = SqlDbType.Int, Value = dirId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromId @DirId", args).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }

        private DirectoryDTO GetDirFromPath(string path)
        {
            var args = new SqlParameter { ParameterName = "Path", SqlDbType = SqlDbType.NVarChar, Value = path };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetDirFromPath @Path", args).FirstOrDefault();
            return _mapper.Map<DirectoryDTO>(directory);
        }

        public void CreateDirectory(string dirName, string path)
        {
            try
            {
                var dir = GetDirFromPath(path);
                if (dir is null)
                {
                    throw new Exception("Invalid path");
                }
                string fullPath = $@"{ConfigHelper.Read("FileUploadPath")}{path}/";
                _directoryRepository.Add(new Directory { Name = dirName, ParentId = dir.Id });
                FileHelper.CreatePath(fullPath, dirName);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDirectory(int empId, string path)
        {
            try
            {
                var dir = GetDirFromPath(path);
                var role = _employeeRepository.GetSingle(x => x.Id == empId).RoleId;
                if (dir is null || role != CommonConstants.MANAGER_ROLE)
                {
                    throw new Exception("Invalid path or does not have permission to delete this directory");
                }
                string fullPath = $@"{ConfigHelper.Read("FileUploadPath")}{path}/";
                _directoryRepository.Delete(dir.Id);
                FileHelper.DeleteFolder(fullPath);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}