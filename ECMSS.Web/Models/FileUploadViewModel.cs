using ECMSS.DTO;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ECMSS.Web.Models
{
    public class FileUploadViewModel
    {
        public int OwnerId { get; set; }
        public string Tag { get; set; }
        public int DirectoryId { get; set; }
        public string SecurityLevel { get; set; }
        public string[] FileNames { get; set; }
        public FileShareDTO[] FileShares { get; set; }

        public List<FileInfoDTO> ConvertToFileInfos(HttpFileCollection hfc)
        {
            List<FileInfoDTO> fileInfos = new List<FileInfoDTO>();
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                HttpPostedFile hpf = hfc[iCnt];
                FileInfoDTO fileInfo = new FileInfoDTO
                {
                    Name = FileNames[iCnt],
                    Owner = OwnerId,
                    Tag = Tag,
                    DirectoryId = DirectoryId,
                    SecurityLevel = SecurityLevel,
                    FileShares = FileShares,
                    FileData = ConvertStreamToBytes(hpf.InputStream)
                };
                fileInfos.Add(fileInfo);
            }
            return fileInfos;
        }

        private byte[] ConvertStreamToBytes(Stream fs)
        {
            BinaryReader br = new BinaryReader(fs);
            return br.ReadBytes((int)fs.Length);
        }
    }
}