namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bans",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        BeginBan = c.DateTime(nullable: false),
                        EndBan = c.DateTime(),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        UserId = c.String(),
                        Body = c.String(),
                        ParentCommentId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        GameId = c.String(maxLength: 128),
                        IsQuote = c.Boolean(nullable: false),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .Index(t => t.ParentCommentId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Key = c.String(maxLength: 100),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitsInStock = c.Short(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Discontinued = c.Boolean(nullable: false),
                        PublisherId = c.String(maxLength: 128),
                        ViewCount = c.Int(nullable: false),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.Key, unique: true)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 100),
                        ParentGenreId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.ParentGenreId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ParentGenreId);
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.String(maxLength: 100),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Type, unique: true);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(maxLength: 40),
                        Description = c.String(storeType: "ntext"),
                        HomePage = c.String(storeType: "ntext"),
                        IsDeleted = c.Boolean(nullable: false),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GameId = c.String(maxLength: 128),
                        OrderId = c.String(maxLength: 128),
                        OrderDetailTotal = c.Decimal(nullable: false, storeType: "money"),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.GameId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        Stage = c.Int(nullable: false),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shippers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(),
                        Phone = c.String(),
                        ValidUntil = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreGames",
                c => new
                    {
                        Genre_Id = c.String(nullable: false, maxLength: 128),
                        Game_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Game_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.PlatformTypeGames",
                c => new
                    {
                        PlatformType_Id = c.String(nullable: false, maxLength: 128),
                        Game_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PlatformType_Id, t.Game_Id })
                .ForeignKey("dbo.PlatformTypes", t => t.PlatformType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PlatformType_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "GameId", "dbo.Games");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Games", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.PlatformTypeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "PlatformType_Id", "dbo.PlatformTypes");
            DropForeignKey("dbo.Genres", "ParentGenreId", "dbo.Genres");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropIndex("dbo.PlatformTypeGames", new[] { "Game_Id" });
            DropIndex("dbo.PlatformTypeGames", new[] { "PlatformType_Id" });
            DropIndex("dbo.GenreGames", new[] { "Game_Id" });
            DropIndex("dbo.GenreGames", new[] { "Genre_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "GameId" });
            DropIndex("dbo.Publishers", new[] { "CompanyName" });
            DropIndex("dbo.PlatformTypes", new[] { "Type" });
            DropIndex("dbo.Genres", new[] { "ParentGenreId" });
            DropIndex("dbo.Genres", new[] { "Name" });
            DropIndex("dbo.Games", new[] { "PublisherId" });
            DropIndex("dbo.Games", new[] { "Key" });
            DropIndex("dbo.Comments", new[] { "GameId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropTable("dbo.PlatformTypeGames");
            DropTable("dbo.GenreGames");
            DropTable("dbo.Shippers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Publishers");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
            DropTable("dbo.Bans");
        }
    }
}
