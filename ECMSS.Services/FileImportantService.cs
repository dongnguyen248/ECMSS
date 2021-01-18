using AutoMapper;
using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System;

namespace ECMSS.Services
{
    public class FileImportantService : IFileImportantService
    {
        private readonly IGenericRepository<FileImportant> _fileImportantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileImportantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileImportantRepository = _unitOfWork.FileImportantRepository;
            _mapper = mapper;
        }

        public void AddOrRemoveImportantFile(int fileId, int employeeId)
        {
            FileImportant fileImportant = _fileImportantRepository.GetSingle(x => x.FileId == fileId && x.EmployeeId == employeeId);
            if (fileImportant == null)
            {
                fileImportant = new FileImportant { Id = Guid.NewGuid(), FileId = fileId, EmployeeId = employeeId };
                _fileImportantRepository.Add(fileImportant);
            }
            else
            {
                _fileImportantRepository.Delete(fileImportant);
            }
            _unitOfWork.Commit();
        }
    }
}