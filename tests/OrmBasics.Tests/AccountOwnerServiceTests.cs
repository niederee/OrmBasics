using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrmBasics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBasics.Tests
{
    [TestClass]
    public class AccountOwnerServiceTests
    {
        [TestMethod]
        public void AccountOwnerService_GetAccountOwners_Works()
        {
            var databaseService = new OrmDatabaseService();
            var accountOwnerService = new AccountOwnerService(databaseService);
            var items = accountOwnerService.GetAccountOwners();
            Assert.IsTrue(items.Count() > 0);
        }

        [TestMethod]
        public void AccountOwnerService_GetAccountOwnersOrm_Works()
        {
            var databaseService = new OrmDatabaseService();
            var accountOwnerService = new AccountOwnerService(databaseService);
            var items = accountOwnerService.GetAccountOwnersOrm();
            Assert.IsTrue(items.Count() > 0);
        }
        [TestMethod]
        public void AccountOwnerService_FindUsersWhoseNameMatchesAPattern_Works()
        {
            var databaseService = new OrmDatabaseService();
            var accountOwnerService = new AccountOwnerService(databaseService);
            var items = accountOwnerService.FindUsersWhoseNameMatchesAPattern("smith");
            Assert.IsTrue(items.Count() > 0);
        }
        [TestMethod]
        public void AccountOwnerService_FindUsersWhoseNameMatchesAPatternlinq_Works()
        {
            var databaseService = new OrmDatabaseService();
            var accountOwnerService = new AccountOwnerService(databaseService);
            var items = accountOwnerService.FindUsersWhoesNameMatchesLinq("smith");
            Assert.IsTrue(items.Count() > 0);
        }
    }
}
