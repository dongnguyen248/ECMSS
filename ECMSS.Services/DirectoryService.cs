using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;

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

        public IEnumerable<DirectoryDTO> GetDirectories()
        {
            var res = _mapper.Map<IEnumerable<DirectoryDTO>>(_directoryRepository.GetAll(x => x.Childrens, x => x.Parent));
            return res;
        }
    }
}