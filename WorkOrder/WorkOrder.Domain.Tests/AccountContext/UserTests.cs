using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.AccountContext.Enums;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Shared;
using WorkOrder.Shared.Entities;
using WorkOrder.Shared.Enums;

namespace WorkOrder.Domain.Tests.AccountContext
{
    [TestClass]
    public class UserTests
    {
        [TestInitialize]
        public void Setup()
        {
            Settings.SecurityKey = "cbffeba849124af8b7b89675c223fd3d";
        }

        [TestMethod]
        public void Deve_Criar_Novo_Usuario()
        {
            Settings.SecurityKey = "cbffeba849124af8b7b89675c223fd3d";
            var tenant = new AppTenant("Treeze", "localhost:43500", null, null);
            var user = new User(new NameVo("Victor", "Luiz"), new EmailVo("victor.luiz@treezebit.com"), UserRole.User, new PasswordVo("12345678", "12345678"), null, tenant.Id);
            Assert.AreEqual(user.EntityStatus, EntityStatus.Activated);
            Assert.AreEqual(user.Notifications.Count, 0);
            Assert.IsTrue(user.Valid);
        }
    }
}
