using DemoStore.Domain.StoreContext.Services;

namespace DemoStore.Infra.StoreContext.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            // TODO: Implementar
        }
    }
}