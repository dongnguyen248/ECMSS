using System;

namespace ECMSS.DTO
{
    public class TrashDTO
    {
        public Guid Id { get; set; }
        public int FileId { get; set; }
        public DateTime DeletedDate { get; set; }

        public virtual FileInfoDTO FileInfo { get; set; }
    }
}