using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GameStore.Models.Enums;

namespace GameStore.Models.Filter
{
    public class GameFilterCriteria
    {
        [DisplayName("Name")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "the name should be min 3")]
        public string GameName { get; set; }

        public ICollection<string> Genres { get; set; }

        [DisplayName("Platforms")]
        public ICollection<string> PlatformTypes { get; set; }

        public ICollection<string> Publishers { get; set; }

        [DisplayName("Items per page")]
        public ItemsPerPage? ItemsPerPage { get; set; }

        public int? Skip { get; set; }

        [DisplayName("Game published")]
        public GamePublished? GamePublished { get; set; }

        public SortFilter? SortCriterion { get; set; }

        public Decimal? MinPrice { get; set; }

        public Decimal? MaxPrice { get; set; }
    }
}
