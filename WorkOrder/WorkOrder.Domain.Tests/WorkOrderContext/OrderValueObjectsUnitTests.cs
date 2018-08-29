using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.WorkOrderContext.ValueObjects;

namespace WorkOrder.Domain.Tests.WorkOrderContext
{
    [TestClass]
    public class OrderValueObjectsUnitTests
    {
        [TestMethod]
        public void Deve_Criar_Um_Novo_Order_Item_Total()
        {

            var totalVo = new OrderItemTotalVo(10);

            Assert.IsTrue(totalVo.Valid);
            Assert.AreEqual(totalVo.Subtotal, 10);
            Assert.AreEqual(totalVo.Amount, 1);
            Assert.AreEqual(totalVo.Total, 10);
            Assert.AreEqual(totalVo.Discount, 0);
            Assert.AreEqual(totalVo.DiscountPercent, 0);
            Assert.AreEqual(totalVo.UnitPrice, 10);

            totalVo.SetDiscount(5);

            Assert.AreEqual(totalVo.Subtotal, 10);
            Assert.AreEqual(totalVo.Amount, 1);
            Assert.AreEqual(totalVo.Total, 5);
            Assert.AreEqual(totalVo.Discount, 5);
            Assert.AreEqual(totalVo.DiscountPercent, 50);
            Assert.AreEqual(totalVo.UnitPrice, 10);

            totalVo.SetDiscount(0);

            Assert.IsTrue(totalVo.Valid);
            Assert.AreEqual(totalVo.Subtotal, 10);
            Assert.AreEqual(totalVo.Amount, 1);
            Assert.AreEqual(totalVo.Total, 10);
            Assert.AreEqual(totalVo.Discount, 0);
            Assert.AreEqual(totalVo.DiscountPercent, 0);
            Assert.AreEqual(totalVo.UnitPrice, 10);


            totalVo.SetDiscountPercent(50);

            Assert.AreEqual(totalVo.Subtotal, 10);
            Assert.AreEqual(totalVo.Amount, 1);
            Assert.AreEqual(totalVo.Total, 5);
            Assert.AreEqual(totalVo.Discount, 5);
            Assert.AreEqual(totalVo.DiscountPercent, 50);
            Assert.AreEqual(totalVo.UnitPrice, 10);

            var calculo = 13.432M * 3;
            var desconto = 0.432M;

            var totalVo2 = new OrderItemTotalVo(13.432M, 3);

            Assert.IsTrue(totalVo2.Valid);

            Assert.AreEqual(totalVo2.Subtotal, calculo);
            Assert.AreEqual(totalVo2.Amount, 3);
            Assert.AreEqual(totalVo2.Total, calculo);
            Assert.AreEqual(totalVo2.Discount, 0);
            Assert.AreEqual(totalVo2.DiscountPercent, 0);
            Assert.AreEqual(totalVo2.UnitPrice, 13.432M);

            totalVo2.SetDiscount(0.432M);

            Assert.IsTrue(totalVo2.Valid);
            Assert.AreEqual(totalVo2.Total, totalVo2.Total);
            Assert.AreEqual(totalVo2.Subtotal, calculo);
            Assert.AreEqual(totalVo2.Discount, desconto);
            Assert.AreEqual(totalVo2.DiscountPercent, 1.0720667063728409767718880286M);
            Assert.AreEqual(totalVo2.UnitPrice, 13.432M);


        }
    }
}
