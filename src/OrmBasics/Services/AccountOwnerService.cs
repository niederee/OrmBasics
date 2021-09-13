using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBasics.Services
{
    public class AccountOwnerService
    {
        private readonly OrmDatabaseService ormDatabaseService;
        private readonly NpgsqlConnection connection;

        public AccountOwnerService(OrmDatabaseService ormDatabaseService)
        {
            this.ormDatabaseService = ormDatabaseService;
            this.connection = ormDatabaseService.GetDatabaseConnection();
        }

        public List<(string firstName, string lastName)> GetAccountOwners()
        {
            List<(string firstName, string lastName)> accountOwners = new();
            connection.OpenConnection();
            using var command = new NpgsqlCommand(Sql.GetAccountOwners, connection);
            var reader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            foreach(DataRow row in dataTable.Rows)
            {
                accountOwners.Add((row.Field<string>("first_name"), row.Field<string>("last_name")));
            }
            return accountOwners;
        }

        private static class Sql
        {
            public static string GetAccountOwners = "Select * from account_owner";
        }

        
    }
}
