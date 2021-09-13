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

    }
}
