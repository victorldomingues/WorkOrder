using Flunt.Notifications;
using Flunt.Validations;

namespace WorkOrder.Domain.WorkOrderContext.ValueObjects
{
    public class OrderItemTotalVo : Notifiable, IValidatable
    {
        protected OrderItemTotalVo()
        {

        }

        public OrderItemTotalVo(decimal unitPrice, int amount)
        {
            UnitPrice = unitPrice;
            Amount = amount;
            Total = 0;
            Subtotal = 0;
            Discount = 0;
            DiscountPercent = 0;
            Calculate();
            Validate();
        }

        public OrderItemTotalVo(decimal total)
        {
            UnitPrice = total;
            Amount = 1;
            Total = total;
            Subtotal = total;
            Discount = 0;
            DiscountPercent = 0;
            Calculate();
            Validate();
        }

        public decimal Total { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Discount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Amount { get; private set; }

        public void SetDiscountPercent(decimal percent)
        {
            this.DiscountPercent = percent;
            this.Discount = (percent * this.Subtotal) / 100;
            Calculate();
        }

        public void SetDiscount(decimal discount)
        {
            this.Discount = discount;
            this.DiscountPercent = (discount * 100) / this.Subtotal;
            Calculate();
        }

        private void Calculate()
        {
            this.Subtotal = UnitPrice * Amount;
            this.Total = this.Subtotal - Discount;
        }

        public void Validate()
        {
            var subtotal = UnitPrice * Amount;
            var total = subtotal - Discount;
            var discount = Subtotal - total;
            var discountPercent = (discount * 100) / this.Subtotal;
            var unitPrice = subtotal / Amount;
            AddNotifications(new Contract()
                    .Requires()
                    .AreNotEquals(Amount, 0, "Amount", "A quantidade não pode ser igual a 0!")
                    .AreEquals(Subtotal, subtotal, "Subtotal", "Subtotal diverge do valor esperado para o cálculo")
                    .AreEquals(Total, total, "Total", "Total diverge do valor esperado para o cálculo")
                    .AreEquals(Discount, discount, "Discount", "Desconto diverge do valor esperado para o cálculo")
                    .AreEquals(DiscountPercent, discountPercent, "Discount", "Prcentagem do desconto diverge do valor esperado para o cálculo")
                    .AreEquals(UnitPrice, unitPrice, "UnitPrice", "Preco unitário diverge do valor esperado para o cálculo")
                );
        }
    }
}
