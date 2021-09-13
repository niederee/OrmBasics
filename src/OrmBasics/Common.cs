using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBasics
{
    public static class Common
    {
        public static void OpenConnection(this DbConnection connection)
        {
            if(connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
