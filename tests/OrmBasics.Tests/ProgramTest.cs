using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBasics.Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void Program_Main_Should_WriteHelloWorld()
        {
          
            Assert.IsTrue(true);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var args = Enumerable.Empty<string>().ToArray();
            Program.Main(args);
            var consoles = stringWriter.ToString().Split(Environment.NewLine);
            Assert.IsTrue(consoles.Count(a=> a.Equals("Hello World!", StringComparison.Ordinal)) == 1);
        }
    }
}
