namespace SuiviPortefeuilleRBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        Sens = c.Int(nullable: false),
                        NumberOfShares = c.Int(nullable: false),
                        Fees = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(),
                        Stock_StockDescriptionId = c.Int(nullable: false),
                        Portfolio_PortfolioId = c.Int(),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("dbo.StockDescriptions", t => t.Stock_StockDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_PortfolioId)
                .Index(t => t.Stock_StockDescriptionId)
                .Index(t => t.Portfolio_PortfolioId);
            
            CreateTable(
                "dbo.StockDescriptions",
                c => new
                    {
                        StockDescriptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        LastPrice = c.Double(nullable: false),
                        ChangePercent = c.Double(nullable: false),
                        SpreadLowTargetPercent = c.Double(nullable: false),
                        SpreadHighTargetPercent = c.Double(nullable: false),
                        PriceTargetMin = c.Double(nullable: false),
                        PriceTargetMax = c.Double(nullable: false),
                        DividendYield = c.Double(nullable: false),
                        Payout = c.Double(nullable: false),
                        PriceEarningRatio = c.Double(nullable: false),
                        DividendPerShare = c.Double(nullable: false),
                        EarningsPerShare = c.Double(nullable: false),
                        EpsEstimateCurrentYear = c.Double(nullable: false),
                        PriceToBook = c.Double(nullable: false),
                        ExDividendDate = c.DateTime(),
                        BookToValuePerShare = c.Double(nullable: false),
                        GrahamPrice = c.Double(nullable: false),
                        GrahamSpread = c.Double(nullable: false),
                        PtfPercent = c.Double(nullable: false),
                        Amount = c.Double(nullable: false),
                        NumberOfSharesTarget = c.Int(nullable: false),
                        AmountTarget = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StockDescriptionId);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        PortfolioId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TargetValue = c.Double(nullable: false),
                        Value = c.Double(nullable: false),
                        Liquidity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        StockDescriptionId = c.Int(nullable: false),
                        NumberOfShares = c.Int(nullable: false),
                        InvestedValue = c.Double(nullable: false),
                        UnitaryPrice = c.Double(nullable: false),
                        DividendYield = c.Double(nullable: false),
                        Return = c.Double(nullable: false),
                        Portfolio_PortfolioId = c.Int(),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.StockDescriptions", t => t.StockDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_PortfolioId)
                .Index(t => t.StockDescriptionId)
                .Index(t => t.Portfolio_PortfolioId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Stocks", "Portfolio_PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Stocks", "StockDescriptionId", "dbo.StockDescriptions");
            DropForeignKey("dbo.Operations", "Portfolio_PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Operations", "Stock_StockDescriptionId", "dbo.StockDescriptions");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Stocks", new[] { "Portfolio_PortfolioId" });
            DropIndex("dbo.Stocks", new[] { "StockDescriptionId" });
            DropIndex("dbo.Operations", new[] { "Portfolio_PortfolioId" });
            DropIndex("dbo.Operations", new[] { "Stock_StockDescriptionId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Stocks");
            DropTable("dbo.Portfolios");
            DropTable("dbo.StockDescriptions");
            DropTable("dbo.Operations");
        }
    }
}