using System;
using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Domain.WorkOrderContext.Enums;

namespace WorkOrder.Application.WorkOrderContext.Requests
{
    public class CreateOrderRequest : Notifiable, IValidatable
    {
        protected CreateOrderRequest()
        {
            Validate();
        }


        public DateTime? PickUpDate { get; set; }
        public EModality Modality { get;  set; }
        public ETypeOfService Type { get; private set; }
        public decimal Total { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Discount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public bool HasTotals { get; private set; }
        public Guid? CustomerId { get; private set; }

        public void Validate()
        {

        }


    }
}
