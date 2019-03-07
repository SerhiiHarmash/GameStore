using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GameStore.Models.Entities
{
    public class Publisher : BaseEntity
    {
        [Index(IsUnique = true)]
        [Column(TypeName = "nvarchar")]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [NotMapped]
        public int SupplierId { get; set; }

        [Column(TypeName = "ntext")]       
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }

        public bool IsDeleted { get; set; }

        //[JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
