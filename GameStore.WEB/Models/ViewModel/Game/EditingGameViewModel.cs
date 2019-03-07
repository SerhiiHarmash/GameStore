using System.Collections.Generic;
using System.ComponentModel;

namespace GameStore.WEB.Models.ViewModel
{
    public class EditingGameViewModel
    {
        public GameViewModel Game { get; set; }

        [DisplayName("Platform type")]
        public ICollection<string> PlatformTypes { get; set; }

        [DisplayName("Genre")]
        public ICollection<string> Genres { get; set; }

        [DisplayName("Publisher")]
        public ICollection<string> Publishers { get; set; }
    }
}