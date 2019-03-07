using GameStore.Models.Filter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace GameStore.WEB.Models.ViewModel.Game
{
    public class GamesShowcaseViewModel
    {
        public ICollection<GameCardViewModel> Games { get; set; }

        public GameFilterCriteria GameFilterCriteria { get; set; }

        public int GameCount { get; set; }

        public SelectList ItemsPerPageVariants { get; set; }

        [DisplayName("Platform types")]
        public ICollection<string> PlatformTypes { get; set; }

        [DisplayName("Genres")]
        public ICollection<string> Genres { get; set; }

        [DisplayName("Publishers")]
        public ICollection<string> Publishers { get; set; }
    }
}