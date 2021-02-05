using System;

namespace ECMSS.DTO
{
    public class TrashDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int FileId { get; set; }
        public DateTime DeletedDate { get; set; } = DateTime.Now;

        public virtual FileInfoDTO FileInfo { get; set; }
    }
}