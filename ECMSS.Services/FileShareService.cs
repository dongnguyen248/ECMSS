using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ECMSS.Services
{
    public class FileShareService : IFileShareService
    {
        private readonly IGenericRepository<FileShare> _fileShareRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileShareService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileShareRepository = _unitOfWork.FileShareRepository;
            _mapper = mapper;
        }

        public void AddFileShares(IEnumerable<FileShareDTO> fileShares)
        {
            try
            {
                _fileShareRepository.AddRange(_mapper.Map<IEnumerable<FileShare>>(fileShares));
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}