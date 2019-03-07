using GameStore.Models.Entities;
using GameStore.Models.Enums;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.Mongo
{
    public class MongoClassesMapperInitializator
    {
        public static void Initialize()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.MapMember(x => x.Id).SetElementName("_id");
                sc.MapMember(x => x.Id).SetSerializer(new EntityIdSerializer());
            });

            BsonClassMap.RegisterClassMap<Order>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.MapMember(m => m.CustomerId).SetElementName("CustomerID");
                sc.MapMember(m => m.OrderDate).SetElementName("OrderDate");
                sc.MapMember(m => m.OrderId).SetElementName("OrderID");
                sc.MapMember(m => m.Stage).SetDefaultValue(OrderStage.Completed);
                sc.MapMember(x => x.OrderDate).SetSerializer(new DateSerializer());
            });

            BsonClassMap.RegisterClassMap<Genre>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.MapMember(m => m.Name).SetElementName("CategoryName");
                sc.MapMember(m => m.CategoryId).SetElementName("CategoryID");
            });

            BsonClassMap.RegisterClassMap<Publisher>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.MapMember(m => m.CompanyName).SetElementName("CompanyName");
                sc.MapMember(m => m.HomePage).SetElementName("HomePage");
                sc.MapMember(m => m.SupplierId).SetElementName("SupplierID");
            });

            BsonClassMap.RegisterClassMap<OrderDetails>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.MapMember(m => m.MongoOrderId).SetElementName("OrderID");          
                sc.MapMember(m => m.ProductId).SetElementName("ProductID");        
                sc.MapMember(m => m.OrderDetailTotal).SetElementName("UnitPrice");
                sc.MapMember(m => m.Quantity).SetElementName("Quantity");
            });

            BsonClassMap.RegisterClassMap<Game>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.MapMember(m => m.Name).SetElementName("ProductName");
                sc.MapMember(m => m.Description).SetElementName("QuantityPerUnit");
                sc.MapMember(m => m.Price).SetElementName("UnitPrice");
                sc.MapMember(m => m.UnitsInStock).SetElementName("UnitsInStock");
                sc.MapMember(m => m.Discontinued).SetElementName("Discontinued");
                sc.MapMember(m => m.ProductId).SetElementName("ProductID");
                sc.MapMember(m => m.SupplierId).SetElementName("SupplierID");
                sc.MapMember(m => m.CategoryId).SetElementName("CategoryID");
                sc.MapMember(m => m.ViewCount).SetElementName("ViewCount");
                sc.MapMember(m => m.ViewCount).SetDefaultValue(0);
            });

            BsonClassMap.RegisterClassMap<Shipper>(sc =>
            {
                sc.SetIgnoreExtraElements(true);
                sc.AutoMap();
                sc.MapMember(x => x.ShipperId).SetElementName("ShipperID");
                sc.MapMember(x => x.Phone).SetElementName("Phone");
            });
        }
    }
}
