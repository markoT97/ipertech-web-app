using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IpertechCompany.DbContext
{
    public interface IDbContext
    {
        IDbConnection Connect();
    }
}
