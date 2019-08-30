using DemoStore.Domain.StoreContext.Entities;
using DemoStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoStore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var name = new Name("Raphael", "França");
            var document = new Document("0000000272");
            var email = new Email("raphaelfrancabsb@gmail.com");
            var c = new Customer(name, document, email,"8888888888");
            var mouse = new Product("Mouse", "Descrição", "image.png", 59.90M, 10);
            var teclado = new Product("Teclado", "Descrição", "image.png", 159.90M, 10);
            var impressora = new Product("Impressora", "Descrição", "image.png", 359.90M, 10);
            var order = new Order(c);
            order.AddItem(mouse, 5);
            order.AddItem(teclado, 5);
            order.AddItem(impressora, 5);
            
            // simular pedido
            order.Place();

            // simular pagamento
            order.Pay();

            // simular envio
            order.Ship();

            // simular cancelamento
            order.Cancel();



        }
    }
}
