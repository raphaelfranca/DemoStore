using Demostore.Domain.StoreContext.Entities;
using Demostore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoStore.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenDocumentIsNotValid()
        {
            var document = new Document("12345678910");
            Assert.AreEqual(false, document.Valid);
            Assert.AreEqual(1, document.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenDocumentIsValid()
        {
            var document = new Document("00000000272");
            Assert.AreEqual(true, document.Valid);
            Assert.AreEqual(0, document.Notifications.Count);
        }
    }
}
