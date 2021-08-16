using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrmBasics.Services;

namespace OrmBasics.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OrmBasics_Service_HelloWorldService_GetHelloWorld_Works()
        {
            #region Arrange
            var service = new HelloWorldService();
            #endregion

            #region Act
            var result = service.GetHelloWorld();
            #endregion

            #region Assert
            Assert.IsTrue("Hello".Equals(result, System.StringComparison.Ordinal));
            #endregion
        }
    }
}
