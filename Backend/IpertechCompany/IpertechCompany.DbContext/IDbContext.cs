using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IpertechCompany.DbConnection
{
    public interface IDbContext
    {
        IDbConnection Connect();
    }
}
