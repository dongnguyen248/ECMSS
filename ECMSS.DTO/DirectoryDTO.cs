using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECMSS.DTO
{
    public class DirectoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter directory name")]
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public virtual ICollection<DirectoryDTO> Childrens { get; set; }
        public virtual DirectoryDTO Parent { get; set; }
        public virtual ICollection<FileInfoDTO> FileInfoes { get; set; }
    }
}