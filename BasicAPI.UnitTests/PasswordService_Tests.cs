using BasicAPI.Services;
using NUnit.Framework;

namespace BasicAPI.UnitTests
{
    [TestFixture]
    public class PasswordService_Tests
    {
        private IPasswordService passwordService;

        [SetUp]
        public void SetUp()
        {
            passwordService = new PasswordService();
        }

        [Test]
        public void HashPassword_IsEqual()
        {
            var hash = passwordService.HashPassword("LD4eiGRUsKs2f4L7PwJlFQ==", "Password!");

            Assert.AreEqual("H1ijpjW52arMZhqQuyDManNi/01f62q/NK1DNckH3sE=", hash, "Hashed password should be equal");

        }

        [Test]
        public void ArePasswordsEqual_ReturnsTrue()
        {
            var areEqual = passwordService.ArePasswordsEqual("Password!", "LD4eiGRUsKs2f4L7PwJlFQ==", "H1ijpjW52arMZhqQuyDManNi/01f62q/NK1DNckH3sE=");

            Assert.IsTrue(areEqual, "Passwords should be equal");
        }

        [Test]
        public void HashPassword_IsNotEqual()
        {
            var hash = passwordService.HashPassword("A23xR38yNEvZ9gi8hWAIjw==", "Password!");

            Assert.AreNotEqual("ZuO4HSRBzNf2eMO7GF8a5CMHDJZYANmRu9b6d33ylxE=", hash, "Hashed password should be equal");
        }

        [Test]
        public void ArePasswordsEqual_ReturnsFalse()
        {
            var areEqual = passwordService.ArePasswordsEqual("Password!", "A23xR38yNEvZ9gi8hWAIjw==", "ZuO4HSRBzNf2eMO7GF8a5CMHDJZYANmRu9b6d33ylxE=");

            Assert.IsFalse(areEqual, "Passwords should be equal");
        }
    }
}