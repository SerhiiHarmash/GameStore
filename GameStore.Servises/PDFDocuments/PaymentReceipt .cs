using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web.Mvc;

namespace GameStore.Services.PDFDocuments
{
    public class PaymentReceipt
    {
        private readonly IOrderService _orderService;

        public PaymentReceipt()
        {
            _orderService = DependencyResolver.Current.GetService<IOrderService>();
        }

        public byte[] GetPaymentReceipt(string customerId)
        {
            var order = _orderService.GetOrderByCustomerId(customerId);

            if (order == null)
            {
                throw new Exception("Order is null");
            }

            var workStream = new MemoryStream();
            var document = new Document(PageSize.A4, 50, 50, 50, 50);

            PdfWriter.GetInstance(document, workStream);
            PdfPTable table = CreateTable(order);

            document.Open();

            AddHeaderForDocument(document);
            document.Add(table);
            AddFooterForDocument(document, order);

            document.Close();

            var fileDocument = workStream.ToArray();
            return fileDocument;
        }

        private void AddHeaderForDocument(Document document)
        {
            var text = "Global Bank";
            var paragraph = new Paragraph();
            paragraph.SpacingBefore = 20;
            paragraph.SpacingAfter = 100;
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Font = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24f, BaseColor.BLACK);
            paragraph.Add(text);
            document.Add(paragraph);

            text = "Receipt of payment";
            paragraph = new Paragraph();
            paragraph.SpacingBefore = 10;
            paragraph.SpacingAfter = 20;
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 24f, BaseColor.BLACK);
            paragraph.Add(text);
            document.Add(paragraph);

            text = "Order details";
            paragraph = new Paragraph();
            paragraph.SpacingBefore = 10;
            paragraph.SpacingAfter = 20;
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18f, BaseColor.BLACK);
            paragraph.Add(text);
            document.Add(paragraph);
        }

        private PdfPTable CreateTable(Order order)
        {
            var table = new PdfPTable(4);

            AddHeaderForTable(table);

            AddProduct(order, table);

            AddTotalPrice(order, table);

            return table;
        }

        private void AddFooterForDocument(Document document, Order order)
        {
            Paragraph paragraph = new Paragraph();

            var text = $"Person Id: {order.CustomerId}";
            paragraph = new Paragraph();
            paragraph.SpacingBefore = 10;
            paragraph.SpacingAfter = 20;
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14f, BaseColor.BLACK);
            paragraph.Add(text);
            document.Add(paragraph);

            text = $"Date of payment: {DateTime.UtcNow.ToLongDateString()}";
            paragraph = new Paragraph();
            paragraph.SpacingBefore = 10;
            paragraph.SpacingAfter = 20;
            paragraph.Alignment = Element.ALIGN_RIGHT;
            paragraph.Font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14f, BaseColor.BLACK);
            paragraph.Add(text);
            document.Add(paragraph);
        }

        private void AddTotalPrice(Order order, PdfPTable table)
        {
            var totalPrice = order.TotalPrice;

            var cell = new PdfPCell(new Phrase("Total price"));
            cell.BorderWidth = 1;
            cell.Colspan = 3;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(totalPrice.ToString()));
            cell.BorderWidth = 1;
            cell.Colspan = 1;
            table.AddCell(cell);
        }

        private void AddProduct(Order order, PdfPTable table)
        {
            var i = 1;
            foreach (var product in order.OrderDetails)
            {
                AddCell(table, i.ToString());
                AddCell(table, product.Game.Name);
                AddCell(table, product.Quantity.ToString());
                AddCell(table, product.OrderDetailTotal.ToString());
                i++;
            }
        }

        private void AddHeaderForTable(PdfPTable table)
        {
            AddCell(table, "#");
            AddCell(table, "Product");
            AddCell(table, "Quantity");
            AddCell(table, "OrderDetailTotal");
        }

        private void AddCell(PdfPTable table, string content)
        {
            var cell = new PdfPCell(new Phrase(content));
            cell.BorderWidth = 1;
            cell.Colspan = 1;
            table.AddCell(cell);
        }
    }
}
