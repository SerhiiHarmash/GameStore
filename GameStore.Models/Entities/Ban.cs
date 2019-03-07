using System;

namespace GameStore.Models.Entities
{
    public class Ban : BaseEntity
    {
        public string UserId { get; set; }

        public DateTime BeginBan { get; set; }

        public DateTime? EndBan { get; set; }
    }
}
