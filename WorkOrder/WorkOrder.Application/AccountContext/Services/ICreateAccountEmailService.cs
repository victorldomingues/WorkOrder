namespace WorkOrder.Application.AccountContext.Services
{
    public interface ICreateAccountEmailService
    {
        void Send(string firstName, string lastName, string email, string companyName, string host);
    }
}
