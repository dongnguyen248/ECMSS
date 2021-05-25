using System;

namespace ECMSS.DTO
{
    public class FileImportantDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int EmployeeId { get; set; }
        public Guid FileId { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual FileInfoDTO FileInfo { get; set; }
    }
}