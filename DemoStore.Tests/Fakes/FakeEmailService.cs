using DemoStore.Domain.StoreContext.Entities;
using DemoStore.Domain.StoreContext.Services;

namespace DemoStore.Tests
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            
        }
    }
}