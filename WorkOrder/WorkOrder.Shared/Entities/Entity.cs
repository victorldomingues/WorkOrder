using System;
using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Shared.Enums;

namespace WorkOrder.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            EntityStatus = EntityStatus.Activated;
            CreatedAt = DateTime.Now;
        }

        [Key]
        public Guid Id { get; protected set; }

        public EntityStatus EntityStatus { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public void SetActivate()
        {
            EntityStatus = EntityStatus.Activated;
        }

        public void SetPending()
        {
            EntityStatus = EntityStatus.Pending;
        }

        public void SetDesabled()
        {
            EntityStatus = EntityStatus.Desabled;
        }

        public void SetDelete()
        {
            EntityStatus = EntityStatus.Deleted;
            DeletedAt = DateTime.Now;
        }


        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
