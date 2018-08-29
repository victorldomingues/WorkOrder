using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.AccountContext.Enums;
using WorkOrder.Domain.SharedContext.Entities;
using WorkOrder.Domain.SharedContext.Enums;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Domain.WorkOrderContext.Entities;
using WorkOrder.Shared;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.Tests
{
    public abstract class DomainBaseUnitTest
    {
        protected AppTenant Tenant;
        protected User User;
        protected Customer Customer;

        protected DomainBaseUnitTest()
        {

            Settings.SecurityKey = "cbffeba849124af8b7b89675c223fd3d";
            Tenant = new AppTenant("Treeze", "localhost:43500", null, null);
            User = new User(new NameVo("Victor", "Luiz"), new EmailVo("victor.luiz@treezebit.com"), UserRole.User, new PasswordVo("12345678", "12345678"), null, Tenant.Id);

            var code = 1;
            var firstName = "Victor";
            var lastName = "Domingues";
            var name = new NameVo(firstName, lastName);
            var number = "11992535010";
            var phone = new PhoneVo(number);
            var phone2 = new PhoneVo(number);
            var cpf = "3333333333";
            var document = new DocumentVo(cpf, EDocumentType.Cpf);
            var emailAddress = "victor@atrace.com.br";
            var email = new EmailVo(emailAddress);

            var address = new Address(Tenant.Id, "", "", "", "", "", "", "", "");

            Customer = new Customer(Tenant.Id, code, name, phone, phone2, email, document, address.Id, null);
        }
    }
}
