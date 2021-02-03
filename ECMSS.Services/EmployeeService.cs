using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;

namespace ECMSS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = _unitOfWork.EmployeeRepository;
            _mapper = mapper;
        }

        public EmployeeDTO GetEmployeeByEpLiteId(string epLiteId)
        {
            return _mapper.Map<EmployeeDTO>(_employeeRepository.GetSingle(e => e.EpLiteId == epLiteId));
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByName(string empName)
        {
            return _mapper.Map<IEnumerable<EmployeeDTO>>(_employeeRepository.GetMany(e => (e.FirstName + " " + e.LastName).Contains(empName), x => x.Department));
        }
    }
}