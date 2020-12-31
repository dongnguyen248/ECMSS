using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}