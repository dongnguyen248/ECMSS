using AutoMapper;
using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ECMSS.Services
{
    public class TrashService : ITrashService
    {
        private IGenericRepository<Trash> _trashRepository;
        private IGenericRepository<FileInfo> _fileInfoRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public TrashService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _trashRepository = _unitOfWork.TrashRepository;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
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
    }
}