using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models.Entities
{
    public class Shipper : BaseEntity
    {
        [NotMapped]
        public int ShipperId { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }
    }
}
