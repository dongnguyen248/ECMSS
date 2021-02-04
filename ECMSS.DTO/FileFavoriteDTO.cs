using System;

namespace ECMSS.DTO
{
    public class FileFavoriteDTO
    {
        public int FileId { get; set; }
        public int EmployeeId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();

        public virtual EmployeeDTO Employee { get; set; }
        public virtual FileInfoDTO FileInfo { get; set; }
    }
}