using ECMSS.Data;

namespace ECMSS.Repositories.Interfaces
{
    public interface IDbFactory
    {
        ECMEntities Init();
    }
}