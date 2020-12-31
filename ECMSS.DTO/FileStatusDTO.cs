using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class FileStatusDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<FileHistoryDTO> FileHistories { get; set; }
    }
}