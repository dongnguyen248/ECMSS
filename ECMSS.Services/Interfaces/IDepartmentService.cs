using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDTO> GetDepartments();
    }
}