using ECMSS.Data;
using ECMSS.Repositories.Interfaces;

namespace ECMSS.Repositories
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ECMEntities _dbContext;

        public ECMEntities Init()
        {
            if (_dbContext == null)
            {
                _dbContext = new ECMEntities();
                _dbContext.Configuration.LazyLoadingEnabled = false;
                _dbContext.Configuration.AutoDetectChangesEnabled = false;
            }
            return _dbContext;
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}