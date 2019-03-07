using System.ComponentModel;

namespace GameStore.WEB.Models.ViewModel
{
    public class VisaViewModel
    {
        [DisplayName("Card holder’s name")]
        public string CardHolderName { get; set; }

        [DisplayName("Card number")]
        public string CardNumber { get; set; }

        [DisplayName("Date of expiry (month and year)")]
        public string DateOfExpiry { get; set; }

        [DisplayName("CVV2")]
        public string CVV2 { get; set; }
    }
}