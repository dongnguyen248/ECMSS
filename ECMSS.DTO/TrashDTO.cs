using System;

namespace ECMSS.DTO
{
    public class TrashDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? DeletedDate { get; set; } = DateTime.Now;
        public Guid? FileId { get; set; }

        public virtual FileInfoDTO FileInfo { get; set; }
    }
}