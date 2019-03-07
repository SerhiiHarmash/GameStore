using System;
using GameStore.Models.Enums;

namespace GameStore.WEB.Models.ViewModel.Order
{
    public class ShortOrderDetailsViewModel
    {
        public string Id { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStage Stage { get; set; }

        public decimal TotalPrice { get; set; }
    }
}