namespace WorkOrder.Infra.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
