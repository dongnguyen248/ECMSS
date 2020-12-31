using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class FileAuthorityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FileShareDTO> FileShares { get; set; }
    }
}