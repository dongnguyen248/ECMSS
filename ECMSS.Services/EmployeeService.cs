using AutoMapper;
using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;

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
    }
}