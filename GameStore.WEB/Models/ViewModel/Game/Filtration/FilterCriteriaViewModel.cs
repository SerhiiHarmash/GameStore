using GameStore.Models.Enums;
using System;
using System.Collections.Generic;

namespace GameStore.WEB.Models.ViewModel.Game.Filtration
{
    public class FilterCriteriaViewModel
    {
        public string GameName { get; set; }

        public ICollection<string> Genres { get; set; }

        public ICollection<string> PlatformTypes { get; set; }

        public ICollection<string> Publishers { get; set; }

        public GamePublished? GamePublished { get; set; }

        public Decimal? MinPrice { get; set; }

        public Decimal? MaxPrice { get; set; }
    }
}