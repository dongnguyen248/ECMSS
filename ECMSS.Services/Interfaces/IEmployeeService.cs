using ECMSS.DTO;

namespace ECMSS.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetEmployeeByEpLiteId(string epLiteId);
    }
}