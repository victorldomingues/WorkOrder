using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Domain.WorkOrderContext.Enums;
using WorkOrder.Shared.Entities;
using WorkOrder.Shared.Enums;

namespace WorkOrder.Domain.WorkOrderContext.Entities
{
    public class Order : TenantEntity, IValidatable
    {

        protected Order()
        {

        }

        public Order(Guid tenantId, int code, DateTime? pickUpDate, EModality modality, ETypeOfService type, string description, string equipmentObservation, string solution, decimal total, decimal subTotal, decimal discount, decimal discountPercent, bool hasTotals, Guid customerId, Guid? customFormId, Guid createdById, Guid? responsibleId)
        {

            SetTenantId(tenantId);

            Code = code;
            PickUpDate = pickUpDate;
            Modality = modality;
            Type = type;
            Description = description;
            EquipmentObservation = equipmentObservation;
            Solution = solution;
            Total = total;
            SubTotal = subTotal;
            Discount = discount;
            DiscountPercent = discountPercent;
            CustomerId = customerId;
            CustomFormId = customFormId;
            CreatedById = createdById;
            ResponsibleId = responsibleId;
            OrderItems = new List<OrderItem>();
            Status = EOrderStatus.Open;
            Validate();

        }



        public int Code { get; private set; }
        public DateTime? PickUpDate { get; private set; }
        public DateTime? ClosingDate { get; private set; }
        public DateTime? DateOfCancellation { get; private set; }

        public EModality Modality { get; private set; }
        public ETypeOfService Type { get; private set; }
        public EOrderStatus Status { get; private set; }

        public string Description { get; private set; }
        public string EquipmentObservation { get; private set; }
        public string Solution { get; private set; }

        public decimal Total { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Discount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public bool HasTotals { get; private set; }

        public Customer Customer { get; private set; }
        public Guid CustomerId { get; private set; }

        public CustomForm CustomForm { get; private set; }
        public Guid? CustomFormId { get; private set; }

        public User CreatedBy { get; private set; }
        public Guid CreatedById { get; private set; }

        public User Responsible { get; private set; }
        public Guid? ResponsibleId { get; private set; }

        public ICollection<OrderItem> OrderItems { get; private set; }

        public void SetCustomer(Guid customerId)
        {
            this.CustomerId = customerId;
            this.SetUpdatedAt();
        }

        public void SetResponsible(Guid responsibleId)
        {
            this.ResponsibleId = responsibleId;
            this.SetUpdatedAt();
        }

        public void AddOrderItem(OrderItem workOrderItem)
        {

            this.OrderItems.Add(workOrderItem);

            AddNotifications(workOrderItem.Notifications);

        }

        public void RemoveOrderItem(Guid workOrderItemId)
        {

            var item = OrderItems.FirstOrDefault(x => x.Id == workOrderItemId);
            if (item == null)
            {

                AddNotification("WorkOrderItems", $"Erro ao remover o item, o item não existe");
                return;

            }
            this.SetUpdatedAt();
            OrderItems.Remove(item);
        }

        public void CalculateByItems()
        {
            this.Total = OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.Total);
            this.SubTotal = OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.SubTotal);
            this.Discount = OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.Discount);
            this.DiscountPercent = OrderItems.Where(x => x.EntityStatus != EntityStatus.Deleted).Sum(x => x.DiscountPercent);

        }

        public void FinalizeOrder()
        {
            if (this.Status == EOrderStatus.Canceled)
            {
                AddNotification("Status", "A ordem de serviço foi cancelada");
                return;
            }
            this.ClosingDate = DateTime.Now;
            this.Status = EOrderStatus.Finalized;
            this.SetUpdatedAt();
        }

        public void CancelOrder()
        {

            if (this.Status == EOrderStatus.Finalized)
            {
                AddNotification("Status", "A ordem de serviço já foi cancelada");
                return;
            }
            else if (this.Status == EOrderStatus.Canceled)
            {
                return;
            }

            this.DateOfCancellation = DateTime.Now;
            this.Status = EOrderStatus.Canceled;
            this.SetUpdatedAt();
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                    .Requires()
                    .AreNotEquals(Code, 0, "Code", "Código não pode ser igual a 0!")
                    .IsNotNullOrEmpty(Description, "Description", "A descrição não pode ser vazia!")
                    .HasMinLen(Description, 5, "Description", "A descrição deve ter no minimo 5 caracteres!")
                    .HasMaxLen(Description, 255, "Description", "A descrição deve ter no maximo 5 caracteres!")
                    )
                ;
        }
    }
}
