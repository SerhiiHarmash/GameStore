using System.Web.Mvc;

namespace GameStore.WEB.PaymentStrategy
{
    internal class Visa : IPayment
    {
        public ActionResult Pay(string customerId)
        {
            return new ViewResult()
            {
                ViewName = "~/Views/Order/Visa.cshtml",
            };
        }
    }
}