using AutoMapper;
using System;
using System.Collections.Generic;

namespace ECMSS.DTO
{
    public class FileInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Tag { get; set; }
        public int DirectoryId { get; set; }

        public virtual DirectoryDTO Directory { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual ICollection<FileFavoriteDTO> FileFavorites { get; set; }
        public virtual ICollection<FileHistoryDTO> FileHistories { get; set; }
        public virtual ICollection<FileShareDTO> FileShares { get; set; }
        public virtual ICollection<FileImportantDTO> FileImportants { get; set; }

        #region IgnoreMap

        [IgnoreMap]
        public string OwnerUser { get; set; }

        [IgnoreMap]
        public byte[] FileData { get; set; }

        #endregion IgnoreMap
    }
}