using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class FilePermissionDTO
    {
        public int Id { get; set; }
        public string Permission { get; set; }
        public virtual ICollection<FileShareDTO> FileShares { get; set; }
    }
}