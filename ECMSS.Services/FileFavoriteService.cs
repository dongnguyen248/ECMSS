using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System;
using System.Collections.Generic;

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

        public void AddFavoriteFiles(IEnumerable<FileFavoriteDTO> fileFavorites)
        {
            try
            {
                _fileFavoriteRepository.AddRange(_mapper.Map<IEnumerable<FileFavorite>>(fileFavorites));
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                _fileFavoriteRepository.Delete(fileFavorite);
            }
            _unitOfWork.Commit();
        }
    }
}