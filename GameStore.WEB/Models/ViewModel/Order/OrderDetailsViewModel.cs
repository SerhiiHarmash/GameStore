namespace GameStore.WEB.Models.ViewModel
{
    public class OrderDetailsViewModel
    {
        public string Id { get; set; }

        public string ProductKey { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal OrderDetailTotal { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}