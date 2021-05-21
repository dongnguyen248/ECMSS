using AutoMapper;
using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
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
        private readonly IMapper _mapper;

        public TrashService(IUnitOfWork unitOfWork, IMapper mapper, IDirectoryService directoryService)
        {
            _unitOfWork = unitOfWork;
            _trashRepository = _unitOfWork.TrashRepository;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
            _directoryService = directoryService;
            _mapper = mapper;
        }

        public void AddFilesToTrash(int[] fileIds, int empId)
        {
            try
            {
                List<Trash> trashes = new List<Trash>();
                var isOwner = false;
                for (int i = 0; i < fileIds.Length; i++)
                {
                    var fileId = fileIds[i];
                    isOwner = _fileInfoRepository.GetSingle(x => x.Id == fileId && x.Owner == empId) != null;
                    if (!isOwner)
                    {
                        throw new Exception("Invalid file or does not have permission to delete this file");
                    }
                    trashes.Add(new Trash { Id = Guid.NewGuid(), FileId = fileIds[i], DeletedDate = DateTime.Now });
                }
                _trashRepository.AddRange(trashes);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CleanTrash(int[] fileIds)
        {
            try
            {
                for (int i = 0; i < fileIds.Length; i++)
                {
                    int fileId = fileIds[i];
                    var dir = _directoryService.GetDirFromFileId(fileId);
                    var fileInfo = _fileInfoRepository.GetSingleById(fileId);
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

        public void RecoverFile(int[] fileIds)
        {
            try
            {
                List<Trash> trashes = new List<Trash>();
                for (int i = 0; i < fileIds.Length; i++)
                {
                    int fileId = fileIds[i];
                    var file = _trashRepository.GetSingle(f => f.FileId == fileId);
                    trashes.Add(file);
                }
                _trashRepository.RemoveRange(trashes);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}