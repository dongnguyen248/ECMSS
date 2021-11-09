using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;

namespace ECMSS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = _unitOfWork.DepartmentRepository;
            _mapper = mapper;
        }

        public IEnumerable<DepartmentDTO> GetDepartments()
        {
            return _mapper.Map<IEnumerable<DepartmentDTO>>(_departmentRepository.GetAll(dept => dept.Employees));
        }
    }
}