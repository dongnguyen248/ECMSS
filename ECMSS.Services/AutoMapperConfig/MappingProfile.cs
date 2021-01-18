using AutoMapper;
using ECMSS.Data;
using ECMSS.DTO;

namespace ECMSS.Services.AutoMapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Directory, DirectoryDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<FileAuthority, FileAuthorityDTO>().ReverseMap();
            CreateMap<FileFavorite, FileFavoriteDTO>().ReverseMap();
            CreateMap<FileImportant, FileImportantDTO>().ReverseMap();
            CreateMap<FileHistory, FileHistoryDTO>().ReverseMap();
            CreateMap<FileInfo, FileInfoDTO>().ReverseMap();
            CreateMap<FileShare, FileShareDTO>().ReverseMap();
            CreateMap<FileStatus, FileStatusDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}