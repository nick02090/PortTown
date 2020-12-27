using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using WebAPI.Controllers;

namespace WebAPITest
{
    [TestClass]
    public class TownTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new TownController();

            var result = controller.Get();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
