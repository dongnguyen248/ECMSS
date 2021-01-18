using AutoMapper;
using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System;

namespace ECMSS.Services
{
    public class FileFavoriteService : IFileFavoriteService
    {
        private readonly IGenericRepository<FileFavorite> _fileFavoriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileFavoriteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileFavoriteRepository = _unitOfWork.FileFavoriteRepository;
            _mapper = mapper;
        }

        public void AddOrRemoveFavoriteFile(int fileId, int employeeId)
        {
            FileFavorite fileFavorite = _fileFavoriteRepository.GetSingle(x => x.FileId == fileId && x.EmployeeId == employeeId);
            if (fileFavorite == null)
            {
                fileFavorite = new FileFavorite { Id = Guid.NewGuid(), FileId = fileId, EmployeeId = employeeId };
                _fileFavoriteRepository.Add(fileFavorite);
            }
            else
            {
                _fileFavoriteRepository.Delete(fileFavorite);
            }
            _unitOfWork.Commit();
        }
    }
}