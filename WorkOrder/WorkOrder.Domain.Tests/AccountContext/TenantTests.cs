using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Shared.Entities;
using WorkOrder.Shared.Enums;

namespace WorkOrder.Domain.Tests.AccountContext
{
    [TestClass]
    public class TenantTests
    {
        [TestMethod]
        public void Deve_Criar_Novo_Tenant()
        {
           
            var tenant = new AppTenant("Treeze", "localhost:43500", null, null);
            Assert.AreEqual(tenant.EntityStatus, EntityStatus.Activated);
            Assert.AreEqual(tenant.Notifications.Count, 0);
            Assert.IsTrue(tenant.Valid);
        }
    }
}
