using System;

namespace HardwareStore.Data.Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Created = DateTime.UtcNow;
        }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int Id { get; set; }
    }
}