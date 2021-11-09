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
        private readonly IMapper _mapper;

        public DepartmentService(IGenericRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IEnumerable<DepartmentDTO> GetDepartments()
        {
            return _mapper.Map<IEnumerable<DepartmentDTO>>(_departmentRepository.GetAll(dept => dept.Employees));
        }
    }
}