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
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
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
                return StringHelper.StringNormalization(e.LastName + " " + e.FirstName).IndexOf(empName, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }, x => x.Department));
        }
    }
}