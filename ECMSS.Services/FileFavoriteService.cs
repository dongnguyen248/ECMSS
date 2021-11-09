using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System;

namespace ECMSS.Services
{
    public class FileFavoriteService : IFileFavoriteService
    {
        private readonly IGenericRepository<FileFavorite> _fileFavoriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FileFavoriteService(IGenericRepository<FileFavorite> fileFavoriteRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _fileFavoriteRepository = fileFavoriteRepository;
        }

        public void AddOrRemoveFavoriteFile(FileFavoriteDTO fileFavoriteDTO)
        {
            FileFavorite fileFavorite = _fileFavoriteRepository.GetSingle(x => x.FileId == fileFavoriteDTO.FileId && x.EmployeeId == fileFavoriteDTO.EmployeeId);
            if (fileFavorite == null)
            {
                fileFavorite = new FileFavorite { Id = Guid.NewGuid(), FileId = fileFavoriteDTO.FileId, EmployeeId = fileFavoriteDTO.EmployeeId };
                _fileFavoriteRepository.Add(fileFavorite);
            }
            else
            {
                _fileFavoriteRepository.Remove(fileFavorite);
            }
            _unitOfWork.Commit();
        }
    }
}