using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;

namespace ECMSS.Services
{
    public class FileInfoService : IFileInfoService
    {
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileInfoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
            _mapper = mapper;
        }

        public IEnumerable<FileInfoDTO> GetFileInfos()
        {
            var result = _fileInfoRepository.GetAll(x => x.Employee, x => x.FileHistories);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }
    }
}