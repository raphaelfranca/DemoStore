using DemoStore.Domain.StoreContext.Entities;

namespace DemoStore.Domain.StoreContext.Services
{
    public interface IEmailService
    {
        void Send(string to, string from, string subject, string body);
    }
}