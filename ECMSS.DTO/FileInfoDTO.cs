﻿using AutoMapper;
using ECMSS.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECMSS.DTO
{
    public class FileInfoDTO
    {
        [Required(ErrorMessage = "Enter file name")]
        public string Name { get; set; }

        public int Owner { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string Tag { get; set; }
        public int DirectoryId { get; set; }
        public string SecurityLevel { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Extension => FileHelper.GetFileExtension(Name);

        public virtual DirectoryDTO Directory { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual ICollection<FileFavoriteDTO> FileFavorites { get; set; }
        public virtual ICollection<FileHistoryDTO> FileHistories { get; set; }
        public virtual ICollection<FileShareDTO> FileShares { get; set; }
        public virtual ICollection<FileImportantDTO> FileImportants { get; set; }
        public virtual ICollection<TrashDTO> Trashes { get; set; }

        #region IgnoreMap

        [IgnoreMap]
        public string OwnerUser { get; set; }

        [IgnoreMap]
        public byte[] FileData { get; set; }

        #endregion IgnoreMap
    }
}