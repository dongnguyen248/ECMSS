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
        private readonly IMapper _mapper;

        public FileShareService(IGenericRepository<FileShare> fileShareRepository, IMapper mapper)
        {
            _fileShareRepository = fileShareRepository;
            _mapper = mapper;
        }

        public IEnumerable<FileShareDTO> GetFileShares(Guid fileId)
        {
            return _mapper.Map<IEnumerable<FileShareDTO>>(_fileShareRepository.GetMany(f => f.FileId == fileId, f => f.Employee));
        }
    }
}