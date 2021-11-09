using ECMSS.Data;
using ECMSS.Repositories.Interfaces;

namespace ECMSS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private ECMEntities _dbContext;
        protected ECMEntities DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}