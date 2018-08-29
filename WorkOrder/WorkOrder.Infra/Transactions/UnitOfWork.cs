using WorkOrder.Infra.Contexts;

namespace WorkOrder.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SaasContext _context;

        public UnitOfWork(SaasContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
