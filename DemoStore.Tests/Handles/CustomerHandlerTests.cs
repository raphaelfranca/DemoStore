using DemoStore.Domain.StoreContext.CustomerCommands.Inputs;
using DemoStore.Domain.StoreContext.Entities;
using DemoStore.Domain.StoreContext.Handlers;
using DemoStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoStore.Tests
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Raphael";
            command.LastName = "França";
            command.Document = "28659170377";
            command.Email = "raphaelfrancabsb@gmail.com";
            command.Phone = "11999999997";

            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.IsValid);
        }
    }
}