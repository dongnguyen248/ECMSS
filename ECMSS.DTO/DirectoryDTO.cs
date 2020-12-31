using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class DirectoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<DirectoryDTO> Childrens { get; set; }
        public virtual DirectoryDTO Parents { get; set; }
        public virtual ICollection<FileInfoDTO> FileInfoes { get; set; }
    }
}