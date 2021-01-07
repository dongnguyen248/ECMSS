using AutoMapper;
using System;

namespace ECMSS.DTO
{
    public class FileHistoryDTO
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Version { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int StatusId { get; set; }
        public int Size { get; set; }

        public virtual FileInfoDTO FileInfo { get; set; }
        public virtual FileStatusDTO FileStatus { get; set; }

        #region IgnoreMap
        [IgnoreMap]
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        #endregion IgnoreMap
    }
}