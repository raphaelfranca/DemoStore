using DemoStore.Domain.StoreContext.CustomerCommands.Inputs;
using DemoStore.Domain.StoreContext.Entities;
using DemoStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Raphael";
            command.LastName = "França";
            command.Document = "28659170377";
            command.Email = "raphaelfrancabsb@gmail.com";
            command.Phone = "11999999997";

            Assert.AreEqual(true, command.Valid());
        }
    }
}