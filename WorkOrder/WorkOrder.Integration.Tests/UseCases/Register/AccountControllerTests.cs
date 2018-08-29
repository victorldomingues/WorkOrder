using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorkOrder.Integration.Tests.Configuration;

namespace WorkOrder.Integration.Tests.UseCases.Register
{
    [TestClass]
    public class AccountControllerTests : BaseTest
    {


        //[TestMethod]
        //public async Task Deve_Registrar_Um_Novo_Usuario()
        //{
        //    var guid  = Guid.NewGuid().ToString().Substring(0, 6);
        //    var json = JsonConvert.SerializeObject(new
        //    {
        //        FirstName = $"Victor",
        //        LastName = $"Domingues  - {guid}",
        //        Email = $"victor-{guid}@atrace.com.br",
        //        CompanyName = $"TreezeBit-{guid}",
        //        Hostname = "localhost=44325-{guid}",
        //        Password = "victor123",
        //        ConfirmPassword = "victor123"
        //    });
        //    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    var respose = await Context.HttpClient.PostAsync("v1/account/signup", stringContent);

        //    var teste = respose;
        //}
    }
}
