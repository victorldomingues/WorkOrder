using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.WorkOrderContext.Entities;
using WorkOrder.Domain.WorkOrderContext.Enums;
using WorkOrder.Domain.WorkOrderContext.ValueObjects;
using WorkOrder.Shared.Enums;

namespace WorkOrder.Domain.Tests.WorkOrderContext
{
    [TestClass]
    public class OrderUnitTests : DomainBaseUnitTest
    {

        private Order _ordem;

        [TestInitialize]
        public void SetUp()
        {
            _ordem = null;
        }

        [TestMethod]
        public void Deve_Criar_Uma_Nova_Order_De_Servico()
        {
            var code = 1;
            var modality = EModality.Separate;
            var typeOfService = ETypeOfService.FrontDesk;
            var description = "Ordem de serviço teste";
            var observation = "Ordem de serviço observação";
            var solution = "trocar bateria";
            _ordem = new Order(Tenant.Id, code, null, modality, typeOfService, description, observation, solution, 0, 0, 0, 0, false, Customer.Id, null, User.Id, null);

            Assert.IsTrue(_ordem.Valid);

            Assert.AreEqual(_ordem.Code, code);
            Assert.AreEqual(_ordem.Modality, modality);
            Assert.AreEqual(_ordem.Type, typeOfService);
            Assert.AreEqual(_ordem.Description, description);
            Assert.AreEqual(_ordem.EquipmentObservation, observation);
            Assert.AreEqual(_ordem.Solution, solution);
            Assert.AreEqual(_ordem.Total, 0);
            Assert.AreEqual(_ordem.SubTotal, 0);
            Assert.AreEqual(_ordem.Discount, 0);
            Assert.AreEqual(_ordem.DiscountPercent, 0);

        }

        [TestMethod]
        public void Deve_Atribuir_Um_Novo_Cliente()
        {
            Deve_Criar_Uma_Nova_Order_De_Servico();

            Customer.SetId(Guid.NewGuid());

            _ordem.SetCustomer(Customer.Id);

            _ordem.Validate();

            Assert.IsTrue(_ordem.Valid);

            Assert.AreEqual(Customer.Id, _ordem.CustomerId);
        }

        [TestMethod]
        public void Deve_Setar_Um_Responsavel()
        {
            Deve_Criar_Uma_Nova_Order_De_Servico();

            User.SetId(Guid.NewGuid());

            _ordem.SetResponsible(User.Id);

            _ordem.Validate();

            Assert.IsTrue(_ordem.Valid);

            Assert.AreEqual(User.Id, _ordem.ResponsibleId);
        }

        [TestMethod]
        public void Deve_Criar_Um_Item_De_Uma_Ordem()
        {
            Deve_Criar_Uma_Nova_Order_De_Servico();
            var nro = 1;
            var nome = $"Item da ordem {nro} ";
            var serie = Guid.NewGuid().ToString().Substring(0, 6).Replace("-", "");
            var marcaModelo = $"SONY PS{nro}";
            var cor = $"COR X.Y.Z.{nro}";
            var observacao = $"OBS X.Y.Z.{nro}";
            var precoUnit = 3.375M * nro;
            var quantidade = nro;
            var total = new OrderItemTotalVo(precoUnit, quantidade);
            var orderItem = new OrderItem(Tenant.Id, _ordem.Id, nome, serie, marcaModelo, cor, observacao, quantidade, total);
            Assert.IsTrue(orderItem.Valid);
            Assert.AreEqual(orderItem.Name, nome);
            Assert.AreEqual(orderItem.Serie, serie);
            Assert.AreEqual(orderItem.BrandModel, marcaModelo);
            Assert.AreEqual(orderItem.Color, cor);
            Assert.AreEqual(orderItem.Observation, observacao);
            Assert.AreEqual(orderItem.UnitPrice, total.UnitPrice);
            Assert.AreEqual(orderItem.Amount, total.Amount);
            Assert.AreEqual(orderItem.Total, total.Total);
            Assert.AreEqual(orderItem.SubTotal, total.Subtotal);
            Assert.AreEqual(orderItem.Discount, total.Discount);
            Assert.AreEqual(orderItem.DiscountPercent, total.DiscountPercent);
        }

        [TestMethod]
        public void Deve_Adicionar_Itens_Na_Ordem()
        {
            Deve_Criar_Uma_Nova_Order_De_Servico();
            for (int i = 0; i < 3; i++)
            {
                var nro = i + 1;
                var nome = $"Item da ordem {nro} ";
                var serie = Guid.NewGuid().ToString().Substring(0, 6).Replace("-", "");
                var marcaModelo = $"SONY PS{nro}";
                var cor = $"COR X.Y.Z.{nro}";
                var observacao = $"OBS X.Y.Z.{nro}";
                var precoUnit = 3.375M * nro;
                var quantidade = nro;
                var total = new OrderItemTotalVo(precoUnit, quantidade);
                var orderItem = new OrderItem(Tenant.Id, _ordem.Id, nome, serie, marcaModelo, cor, observacao, quantidade, total);
                Assert.IsTrue(orderItem.Valid);
                _ordem.AddOrderItem(orderItem);
            }
            Assert.IsTrue(_ordem.Valid);
            Assert.AreEqual(_ordem.OrderItems.Count, 3);

        }



        [TestMethod]
        public void Deve_Remover_Itens_Da_Ordem()
        {
            Deve_Adicionar_Itens_Na_Ordem();

            var item = _ordem.OrderItems.First();

            _ordem.RemoveOrderItem(item.Id);

            Assert.IsTrue(_ordem.Valid);
            Assert.AreEqual(_ordem.OrderItems.Count, 2);
            var findItem = _ordem.OrderItems.FirstOrDefault(x => x.Id == item.Id);
            Assert.IsNull(findItem);
        }


        [TestMethod]
        public void Deve_Calcular_Por_Items()
        {

            Deve_Adicionar_Itens_Na_Ordem();

            _ordem.CalculateByItems();

            var total = _ordem.OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.Total);
            var subtotal = _ordem.OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.SubTotal);
            var discount = _ordem.OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.Discount);
            var discountPercent = _ordem.OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.DiscountPercent);

            Assert.IsTrue(_ordem.Valid);
            Assert.AreEqual(_ordem.Total, total);
            Assert.AreEqual(_ordem.SubTotal, subtotal);
            Assert.AreEqual(_ordem.Discount, discount);
            Assert.AreEqual(_ordem.DiscountPercent, discountPercent);

        }


        [TestMethod]
        public void Deve_Cancelar_Ordem()
        {

            Deve_Criar_Uma_Nova_Order_De_Servico();

            _ordem.CancelOrder();

            Assert.IsTrue(_ordem.Valid);
            Assert.IsTrue(_ordem.DateOfCancellation.HasValue);
            Assert.AreEqual(_ordem.Status, EOrderStatus.Canceled);

        }

    }
}
