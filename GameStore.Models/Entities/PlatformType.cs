using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GameStore.Models.Entities
{
    public class PlatformType : BaseEntity
    {
        [Index(IsUnique = true)]
        [StringLength(100, MinimumLength = 2)]
        public string Type { get; set; }

        //[JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
