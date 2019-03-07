using System;

namespace GameStore.Models.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
