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

        public FileImportantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _fileImportantRepository = _unitOfWork.FileImportantRepository;
        }

        public void AddOrRemoveImportantFile(Guid fileId, int employeeId)
        {
            FileImportant fileImportant = _fileImportantRepository.GetSingle(x => x.FileId == fileId && x.EmployeeId == employeeId);
            if (fileImportant == null)
            {
                fileImportant = new FileImportant { Id = Guid.NewGuid(), FileId = fileId, EmployeeId = employeeId };
                _fileImportantRepository.Add(fileImportant);
            }
            else
            {
                _fileImportantRepository.Remove(fileImportant);
            }
            _unitOfWork.Commit();
        }
    }
}