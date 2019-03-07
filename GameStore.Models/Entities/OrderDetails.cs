using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models.Entities
{
    public class OrderDetails : BaseEntity
    {
        public Game Game { get; set; }

        public string GameId { get; set; }

        [NotMapped]
        public int ProductId { get; set; }

        public Order Order { get; set; }

        public string OrderId { get; set; }

        [NotMapped]
        public int MongoOrderId { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderDetailTotal { get; set; }

        public short Quantity { get; set; }

        //public bool IsDeleted { get; set; }

        public float Discount { get; set; }      
    }
}
