﻿using Npgsql;
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
            using var dataTable = Sql.GetAccountOwners.GetDataTable(connection);
            foreach(DataRow row in dataTable.Rows)
            {
                accountOwners.Add((row.Field<string>("first_name"), row.Field<string>("last_name")));
            }
            return accountOwners;
        }

        private static class Sql
        {
            public static string GetAccountOwners = "Select * from account_owner";
            public static string SomeOtherQuery = "Select something from something";
        }

        
    }
}
