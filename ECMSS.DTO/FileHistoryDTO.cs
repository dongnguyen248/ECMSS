using AutoMapper;
using System;

namespace ECMSS.DTO
{
    public class FileHistoryDTO
    {
        private const int FILE_UPLOAD_STATUS = 2;
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Version { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public int StatusId { get; set; } = FILE_UPLOAD_STATUS;
        public int Size { get; set; }
        public int Modifier { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual FileInfoDTO FileInfo { get; set; }
        public virtual FileStatusDTO FileStatus { get; set; }

        #region IgnoreMap
        [IgnoreMap]
        public string ModifierUser { get; set; }

        [IgnoreMap]
        public string FileName { get; set; }

        [IgnoreMap]
        public byte[] FileData { get; set; }

        #endregion IgnoreMap
    }
}