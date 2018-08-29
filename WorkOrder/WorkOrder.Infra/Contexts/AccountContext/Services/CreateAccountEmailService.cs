using WorkOrder.Application.AccountContext.Services;

namespace WorkOrder.Infra.Contexts.AccountContext.Services
{
    public class CreateAccountEmailService : ICreateAccountEmailService
    {
        public void Send(string firstName, string lastName, string email, string companyName, string host)
        {
        }
    }
}
