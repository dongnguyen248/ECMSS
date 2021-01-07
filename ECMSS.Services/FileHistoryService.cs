using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ECMSS.Services
{
    public class FileHistoryService : IFileHistoryService
    {
        private readonly IGenericRepository<FileHistory> _fileHistoryRepository;
        private readonly IGenericRepository<Directory> _directoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileHistoryRepository = _unitOfWork.FileHistoryRepository;
            _directoryRepository = _unitOfWork.DirectoryRepository;
            _mapper = mapper;
        }

        public void UploadFile(FileHistoryDTO fileHistory)
        {
            try
            {
                string filePath = ConfigHelper.Read("FileUploadPath");
                filePath += $"{GetPathFromFileId(fileHistory.Id)}/{fileHistory.FileName}";
                FileHelper.SaveFile(filePath, fileHistory.FileData);
                _fileHistoryRepository.Update(_mapper.Map<FileHistory>(fileHistory));
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetPathFromFileId(int fileId)
        {
            var idParam = new SqlParameter { ParameterName = "FileId", SqlDbType = SqlDbType.Int, Value = fileId };
            var directory = _directoryRepository.ExecuteQuery("EXEC Proc_GetPathFromFileId @FileId", idParam).FirstOrDefault();
            return directory.Name;
        }
    }
}