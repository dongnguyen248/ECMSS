using ECMSS.Data;
using ECMSS.Repositories.Interfaces;

namespace ECMSS.Repositories
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ECMEntities _dbContext;

        public ECMEntities Init() => _dbContext ?? (_dbContext = new ECMEntities());

        protected override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}