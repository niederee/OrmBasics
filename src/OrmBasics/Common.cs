using System;
using System.Collections.Generic;
using System.Data;
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

        public static DataTable GetDataTable(this string sql, DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            connection.OpenConnection();
            using var reader = command.ExecuteReader();
            using var dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }
    }
}
