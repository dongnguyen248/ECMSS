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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _directoryRepository = _unitOfWork.DirectoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<DirectoryDTO> GetTreeDirectories()
        {
            var res = _mapper.Map<IEnumerable<DirectoryDTO>>(_directoryRepository.GetAll());
            return res;
        }

        public string GetPathFromFileId(int fileId)
        {
            var idParam = new SqlParameter { ParameterName = "FileId", SqlDbType = SqlDbType.Int, Value = fileId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetPathFromFileId @FileId", idParam).FirstOrDefault();
            return directory.Name;
        }

        public string GetPathFromDirId(int dirId)
        {
            var idParam = new SqlParameter { ParameterName = "DirId", SqlDbType = SqlDbType.Int, Value = dirId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetPathFromDirId @DirId", idParam).FirstOrDefault();
            return directory.Name;
        }

        public void CreateDirectory(string dirName, string parentName)
        {
            try
            {
                var dir = _directoryRepository.GetSingle(x => x.Name == parentName);
                string path = $@"{ConfigHelper.Read("FileUploadPath")}{GetPathFromDirId(dir.Id)}\";
                _directoryRepository.Add(new Directory { Name = dirName, ParentId = dir.Id });
                FileHelper.CreatePath(path, dirName);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}