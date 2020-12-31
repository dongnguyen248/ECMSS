using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string EpLiteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }

        public virtual DepartmentDTO Department { get; set; }
        public virtual RoleDTO Role { get; set; }
        public virtual ICollection<FileFavoriteDTO> FileFavorites { get; set; }
        public virtual ICollection<FileInfoDTO> FileInfoes { get; set; }
        public virtual ICollection<FileShareDTO> FileShares { get; set; }
    }
}