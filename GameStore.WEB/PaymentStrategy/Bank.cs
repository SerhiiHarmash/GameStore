using GameStore.Contracts.Interfaces.Services;
using GameStore.Services.PDFDocuments;
using System;
using System.Web.Mvc;

namespace GameStore.WEB.PaymentStrategy
{
    internal class Bank : IPayment
    {
        private readonly IOrderService _orderService;

        public Bank()
        {
            _orderService = DependencyResolver.Current.GetService<IOrderService>();
        }      

        public ActionResult Pay(string customerId)
        {
            var order = _orderService.GetOrderByCustomerId(customerId);
            if (order == null)
            {
                throw new Exception("Order is null");
            }
            
            var paymentReceipt = new PaymentReceipt();
            var byteInfo = paymentReceipt.GetPaymentReceipt(customerId);
            var fileType = ".pdf";
            var fileName = "file.pdf";

            return new FileContentResult(
                byteInfo, fileType)
            {
                FileDownloadName = fileName,
            };
        }
    }
}