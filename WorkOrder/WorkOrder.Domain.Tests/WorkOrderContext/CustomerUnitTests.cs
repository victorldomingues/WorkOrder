using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.SharedContext.Entities;
using WorkOrder.Domain.SharedContext.Enums;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Domain.WorkOrderContext.Entities;

namespace WorkOrder.Domain.Tests.WorkOrderContext
{
    [TestClass]
    public class CustomerUnitTests : DomainBaseUnitTest
    {
        [TestMethod]
        public void Deve_Criar_Um_Novo_Cliente()
        {
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

            var customer = new Customer(Tenant.Id, code, name, phone, phone2, email, document, address.Id, null);

            Assert.IsTrue(customer.Valid);
            Assert.AreEqual(customer.TenantId, Tenant.Id);
            Assert.AreEqual(customer.Name, name);
            Assert.AreEqual(customer.Phone, phone);
            Assert.AreEqual(customer.Phone2, phone2);
            Assert.AreEqual(customer.Email, email);
            Assert.AreEqual(customer.Document, document);
            Assert.AreEqual(customer.Code, code);

        }
    }
}
