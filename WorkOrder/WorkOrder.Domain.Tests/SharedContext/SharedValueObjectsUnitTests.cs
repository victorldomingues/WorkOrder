using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Shared;

namespace WorkOrder.Domain.Tests.SharedContext
{
    [TestClass]
    public class SharedValueObjectsUnitTests
    {

        [TestInitialize]
        public void Setup()
        {
            Settings.SecurityKey = "cbffeba849124af8b7b89675c223fd3d";
        }

        [TestMethod]
        public void Deve_Criar_Um_Valor_De_Objeto_Nome()
        {

            var nome = new NameVo("Victor", "Domingues");
            Assert.AreEqual(nome.FirstName, "Victor");
            Assert.AreEqual(nome.LastName, "Domingues");
            Assert.IsTrue(nome.Valid);

        }

        [TestMethod]
        public void Deve_Criar_Um_Valor_De_Objeto_Telefone()
        {

            var telefone = new PhoneVo("11992535010");
            Assert.AreEqual(telefone.Number, "11992535010");
            Assert.IsTrue(telefone.Valid);

        }


        [TestMethod]
        public void Deve_Criar_Um_Valor_De_Objeto_Email()
        {

            var email = new EmailVo("victor@atrace.com.br");
            Assert.AreEqual(email.Address, "victor@atrace.com.br");
            Assert.IsTrue(email.Valid);

        }

        [TestMethod]
        public void Deve_Criar_Um_Valor_De_Objeto_Senha()
        {

            var password = new PasswordVo("12345678", "12345678");
            Assert.IsTrue(password.Valid);

        }
    }
}
