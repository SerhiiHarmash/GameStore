using System;
using GameStore.Models.Enums;

namespace GameStore.Models.Entities
{
    public class Log
    {
        public EntityAction EntityAction { get; set; }

        public Type EntityType { get; set; }

        public DateTime Date { get; set; }

        public string LastVersion { get; set; }

        public string NewVersion { get; set; }
    }
}
