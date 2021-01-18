using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using System;

namespace ECMSS.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IGenericRepository<Department> DepartmentRepository { get; private set; }
        public IGenericRepository<Directory> DirectoryRepository { get; private set; }
        public IGenericRepository<Employee> EmployeeRepository { get; private set; }
        public IGenericRepository<FileAuthority> FileAuthorityRepository { get; private set; }
        public IGenericRepository<FileFavorite> FileFavoriteRepository { get; private set; }
        public IGenericRepository<FileImportant> FileImportantRepository { get; private set; }
        public IGenericRepository<FileHistory> FileHistoryRepository { get; private set; }
        public IGenericRepository<FileInfo> FileInfoRepository { get; private set; }
        public IGenericRepository<FileShare> FileShareRepository { get; private set; }
        public IGenericRepository<FileStatus> FileStatusRepository { get; private set; }
        public IGenericRepository<Role> RoleRepository { get; private set; }

        private readonly ECMEntities _dbContext;
        private bool _disposed;

        public UnitOfWork(ECMEntities dbContext)
        {
            _dbContext = dbContext;

            DepartmentRepository = new GenericRepository<Department>(_dbContext);
            DirectoryRepository = new GenericRepository<Directory>(_dbContext);
            EmployeeRepository = new GenericRepository<Employee>(_dbContext);
            FileAuthorityRepository = new GenericRepository<FileAuthority>(_dbContext);
            FileFavoriteRepository = new GenericRepository<FileFavorite>(_dbContext);
            FileImportantRepository = new GenericRepository<FileImportant>(_dbContext);
            FileHistoryRepository = new GenericRepository<FileHistory>(_dbContext);
            FileInfoRepository = new GenericRepository<FileInfo>(_dbContext);
            FileShareRepository = new GenericRepository<FileShare>(_dbContext);
            FileStatusRepository = new GenericRepository<FileStatus>(_dbContext);
            RoleRepository = new GenericRepository<Role>(_dbContext);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose();
        }
    }
}