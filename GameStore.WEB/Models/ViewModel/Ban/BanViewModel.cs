using GameStore.Models.Enums;
using System.ComponentModel;
using System.Web.Mvc;

namespace GameStore.WEB.Models.ViewModel.Ban
{
    public class BanViewModel
    {
        [DisplayName("Ban duration")]
        public SelectList BanDurations { get; set; }

        public string UserId { get; set; }

        public BanDuration BanDuration { get; set; }
    }
}