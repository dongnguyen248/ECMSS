using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetEmployeeByEpLiteId(string epLiteId);

        IEnumerable<EmployeeDTO> GetEmployeesByName(string empName);
    }
}