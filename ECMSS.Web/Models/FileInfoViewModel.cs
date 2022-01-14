using ECMSS.DTO;
using System;
using System.Linq;

namespace ECMSS.Web.Models
{
    public class FileInfoViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Modifier { get; set; }
        public int Size { get; set; }
        public int DirectoryId { get; set; }
        public string SecurityLevel { get; set; }
        public string Version { get; set; }
        public string Tag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsImportant { get; set; }

        public FileInfoViewModel()
        {
        }

        public FileInfoViewModel(FileInfoDTO fileInfo, int empId)
        {
            Id = fileInfo.Id;
            Name = fileInfo.Name;
            Owner = fileInfo.Employee.EpLiteId;
            CreatedDate = fileInfo.CreatedDate;
            DirectoryId = fileInfo.DirectoryId;
            Modifier = GetFileHistory(fileInfo).Employee.EpLiteId;
            Size = GetFileHistory(fileInfo).Size;
            SecurityLevel = fileInfo.SecurityLevel;
            Version = GetFileHistory(fileInfo).Version;
            Tag = fileInfo.Tag;
            ModifiedDate = GetFileHistory(fileInfo).ModifiedDate;
            IsFavorite = fileInfo.FileFavorites.Any(f => f.EmployeeId == empId);
            IsImportant = fileInfo.FileImportants.Any(i => i.EmployeeId == empId);
        }

        private FileHistoryDTO GetFileHistory(FileInfoDTO fileInfo)
        {
            return fileInfo.FileHistories.OrderByDescending(u => u.Id).FirstOrDefault();
        }
    }
}