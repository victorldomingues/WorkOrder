using System;
using Flunt.Validations;
using WorkOrder.Domain.WorkOrderContext.ValueObjects;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.WorkOrderContext.Entities
{
    public class OrderItem : TenantEntity, IValidatable
    {

        protected OrderItem()
        {


        }

        public OrderItem(Guid tenantId, Guid workOrderId, string name, string serie, string brandModel, string color, string observation, int amount, OrderItemTotalVo total)
        {
            SetTenantId(tenantId);
            WorkOrderId = workOrderId;
            Name = name;
            Serie = serie;
            BrandModel = brandModel;
            Color = color;
            Observation = observation;
            Amount = amount;
            UnitPrice = total.UnitPrice;
            Total = total.Total;
            SubTotal = total.Subtotal;
            Discount = total.Discount;
            DiscountPercent = total.DiscountPercent;
        }

        public string Name { get; private set; }
        public string Serie { get; private set; }
        public string BrandModel { get; private set; }
        public string Color { get; private set; }
        public string Observation { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Total { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Discount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public string Image { get; private set; }
        public Order WorkOrder { get; private set; }
        public Guid WorkOrderId { get; private set; }



        public void SetImage(string image)
        {
            this.Image = image;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                    .Requires()
                    .AreNotEquals(Amount, 0, "Amount", "A quantidade não pode ser igual a 0!")
                    .IsNotNullOrEmpty(Name, "Name", "O nome não pode ser vazio!")
                    .HasMinLen(Name, 5, "Name", "O nome deve ter no minimo 5 caracteres!")
                    .HasMaxLen(Name, 255, "Name", "O Nome deve ter no maximo 5 caracteres!")
                )

                ;
        }

    }
}
