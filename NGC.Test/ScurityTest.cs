using Microsoft.VisualStudio.TestTools.UnitTesting;
using NGC.Model;
using NGC.Common.Helpers;

namespace NGC.Test
{
    [TestClass]
    public class ScurityTest
    {
        [TestMethod]
        public void PasswordVerify()
        {
            User user = new User();
            user.UsetRawPassword("5Mofuyu0");
            Assert.IsTrue(user.PasswordVerify("5Mofuyu0"));

        }
    }
}
