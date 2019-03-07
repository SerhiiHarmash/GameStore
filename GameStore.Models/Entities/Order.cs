using GameStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GameStore.Models.Entities
{
    public class Order : BaseEntity
    {
        [NotMapped] 
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStage Stage { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public decimal TotalPrice => OrderDetails?.Sum(x => x.OrderDetailTotal) ?? 0;
    }
}
