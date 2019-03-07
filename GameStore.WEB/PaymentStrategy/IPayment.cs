using System.Web.Mvc;

namespace GameStore.WEB.PaymentStrategy
{
    interface IPayment
    {
        ActionResult Pay(string customerId);
    }
}