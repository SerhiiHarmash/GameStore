using System;
using System.Collections.Generic;

namespace GameStore.WEB.Models.ViewModel
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<OrderDetailsViewModel> OrdersDetails { get; set; }
    }
}