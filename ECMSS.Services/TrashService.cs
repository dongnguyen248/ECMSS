using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using ECMSS.Utilities.Constants;
using System;
using System.Collections.Generic;

namespace ECMSS.Services
{
    public class TrashService : ITrashService
    {
        private readonly IGenericRepository<Trash> _trashRepository;
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IDirectoryService _directoryService;
        private readonly IUnitOfWork _unitOfWork;

        public TrashService(IGenericRepository<Trash> trashRepository, IGenericRepository<FileInfo> fileInfoRepository,
                            IDirectoryService directoryService, IUnitOfWork unitOfWork)
        {
            _trashRepository = trashRepository;
            _fileInfoRepository = fileInfoRepository;
            _directoryService = directoryService;
            _unitOfWork = unitOfWork;
        }

        public void AddFilesToTrash(Guid[] fileIds, int empId)
        {
            try
            {
                List<Trash> removeFiles = new List<Trash>();
                bool isOwner = false;
                for (int i = 0; i < fileIds.Length; i++)
                {
                    Guid fileId = fileIds[i];
                    isOwner = _fileInfoRepository.GetSingle(x => x.Id == fileId && x.Owner == empId) != null;
                    if (!isOwner)
                    {
                        throw new Exception("Invalid file or does not have permission to delete this file");
                    }
                    removeFiles.Add(new Trash { Id = Guid.NewGuid(), FileId = fileIds[i], DeletedDate = DateTime.Now });
                }
                _trashRepository.AddRange(removeFiles);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CleanTrash(Guid[] fileIds)
        {
            try
            {
                for (int i = 0; i < fileIds.Length; i++)
                {
                    Guid fileId = fileIds[i];
                    DirectoryDTO dir = _directoryService.GetDirFromFileId(fileId);
                    FileInfo fileInfo = _fileInfoRepository.GetSingleById(fileId);
                    _fileInfoRepository.Remove(fileInfo);
                    string fullPath = $@"{CommonConstants.FILE_UPLOAD_PATH}{dir.Name}/{fileInfo.Name}";
                    FileHelper.DeleteFile(fullPath);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RecoverFile(Guid[] fileIds)
        {
            try
            {
                List<Trash> recoverFiles = new List<Trash>();
                Guid fileId = Guid.Empty;
                for (int i = 0; i < fileIds.Length; i++)
                {
                    fileId = fileIds[i];
                    Trash file = _trashRepository.GetSingle(f => f.FileId == fileId);
                    recoverFiles.Add(file);
                }
                _trashRepository.RemoveRange(recoverFiles);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}