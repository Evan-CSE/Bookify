using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Abstractions.Data
{
    internal interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
