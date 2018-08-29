using WorkOrder.Shared.Entities;

namespace WorkOrder.Application.SharedContext.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Save(T entity);
        void Update(T entity);
    }
}
