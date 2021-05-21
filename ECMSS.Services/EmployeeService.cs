using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;
using ECMSS.Repositories.Interfaces;
using ECMSS.Services.Interfaces;
using ECMSS.Utilities;
using System;
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

        public EmployeeDTO GetEmployeeById(int empId)
        {
            return _mapper.Map<EmployeeDTO>(_employeeRepository.GetSingle(x => x.Id == empId, x => x.Department));
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByDeptId(int deptId)
        {
            return _mapper.Map<IEnumerable<EmployeeDTO>>(_employeeRepository.GetMany(e => e.DepartmentId == deptId));
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByName(string empName)
        {
            empName = StringHelper.StringNormalization(empName);
            return _mapper.Map<IEnumerable<EmployeeDTO>>(_employeeRepository.Find(delegate (Employee e)
            {
                if (StringHelper.StringNormalization(e.LastName + " " + e.FirstName).IndexOf(empName, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, x => x.Department));
        }
    }
}