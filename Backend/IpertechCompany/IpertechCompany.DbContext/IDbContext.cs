using System.Data;

namespace IpertechCompany.DbConnection
{
    public interface IDbContext
    {
        IDbConnection Connect();
    }
}
