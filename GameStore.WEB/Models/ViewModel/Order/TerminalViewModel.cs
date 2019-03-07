using System;

namespace GameStore.WEB.Models.ViewModel
{
    public class TerminalViewModel
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public Decimal TotalPrice { get; set; }
    }
}