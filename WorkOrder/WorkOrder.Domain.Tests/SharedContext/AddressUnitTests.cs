using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.SharedContext.Entities;

namespace WorkOrder.Domain.Tests.SharedContext
{
    [TestClass]
    public class AddressUnitTests : DomainBaseUnitTest
    {
        [TestMethod]
        public void Deve_Criar_Um_Novo_Endereco()
        {

            var logradouro = "Rua Francisco Salatino";
            var bairro = "Jd Maria Helena";
            var cep = "06787040";
            var numero = "412";
            var complemento = "fim da rua";
            var cidade = "Taboão da serra";
            var estado = "São Paulo";
            var endereco = new Address(Tenant.Id, logradouro, bairro, cidade, estado, numero, cep, complemento);

            Assert.AreEqual(endereco.PublicPlace, logradouro);
            Assert.AreEqual(endereco.Number, numero);
            Assert.AreEqual(endereco.Neighborhood, bairro);
            Assert.AreEqual(endereco.City, cidade);
            Assert.AreEqual(endereco.State, estado);
            Assert.AreEqual(endereco.Complement, complemento);
            Assert.AreEqual(endereco.ZipCode, cep);

            Assert.IsTrue(endereco.Valid);


        }
    }
}
