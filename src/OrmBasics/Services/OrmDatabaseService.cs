using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBasics.Services
{
    public class OrmDatabaseService
    {
        public Npgsql.NpgsqlConnection GetDatabaseConnection()
        {
            var connectionString = Environment.GetEnvironmentVariable("orm_connection", EnvironmentVariableTarget.User);
            return new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
