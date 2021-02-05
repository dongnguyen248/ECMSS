using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetEmployeeByEpLiteId(string epLiteId);

        EmployeeDTO GetEmployeeById(int empId);

        IEnumerable<EmployeeDTO> GetEmployeesByName(string empName);
    }
}