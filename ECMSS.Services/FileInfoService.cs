using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ECMSS.Services
{
    public class FileInfoService : IFileInfoService
    {
        private readonly IGenericRepository<FileInfo> _fileInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDirectoryService _directoryService;
        private readonly IMapper _mapper;

        public FileInfoService(IUnitOfWork unitOfWork, IMapper mapper, IDirectoryService directoryService)
        {
            _unitOfWork = unitOfWork;
            _fileInfoRepository = _unitOfWork.FileInfoRepository;
            _directoryService = directoryService;
            _mapper = mapper;
        }

        public IEnumerable<FileInfoDTO> GetFileInfos()
        {
            var result = _fileInfoRepository.GetAll(x => x.Employee, x => x.FileHistories);
            return _mapper.Map<IEnumerable<FileInfoDTO>>(result);
        }

        public string GetFileUrl(int id)
        {
            var fileInfo = _fileInfoRepository.GetSingle(x => x.Id == id, x => x.FileHistories);
            string filePath = ConfigHelper.Read("FileUploadPath");
            filePath += $"{_directoryService.GetPathFromFileId(id)}/{fileInfo.Name}";
            string version = fileInfo.FileHistories.OrderByDescending(x => x.Id).FirstOrDefault().Version;
            string pattern = $"ECMProtocol: <Download>[{fileInfo.Id}][{filePath}][{fileInfo.Owner}][{version}]";
            return pattern;
        }
    }
}