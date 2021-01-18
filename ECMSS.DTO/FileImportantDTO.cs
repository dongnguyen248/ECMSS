using System;

namespace ECMSS.DTO
{
    public class FileImportantDTO
    {
        public Guid Id { get; set; }
        public int FileId { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual FileInfoDTO FileInfo { get; set; }
    }
}