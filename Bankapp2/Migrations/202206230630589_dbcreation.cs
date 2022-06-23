namespace Bankapp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbcreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        AccountTypeName = c.String(),
                        AccountSubType = c.String(),
                        InterestRate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.CustAccounts",
                c => new
                    {
                        CustAccountId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustAccountId)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Email = c.String(),
                        Mobile = c.Long(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MaritalStatus = c.String(),
                        ZipCodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.ZipCodes", t => t.ZipCodeId, cascadeDelete: true)
                .Index(t => t.ZipCodeId);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ZipCodeId = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZipCodeId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.LoanAccounts",
                c => new
                    {
                        LoanAccountId = c.Int(nullable: false, identity: true),
                        CustAccountId = c.Int(nullable: false),
                        BalanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BranchCode = c.String(),
                        RateOfInterest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanDuration = c.Int(nullable: false),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LoanAccountId)
                .ForeignKey("dbo.CustAccounts", t => t.CustAccountId, cascadeDelete: true)
                .Index(t => t.CustAccountId);
            
            CreateTable(
                "dbo.LoanAccTxns",
                c => new
                    {
                        LoanAccTxnId = c.Int(nullable: false, identity: true),
                        LoanAccountId = c.Int(nullable: false),
                        EmiDate = c.DateTime(nullable: false),
                        EmiAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        PendingAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LoanAccTxnId)
                .ForeignKey("dbo.LoanAccounts", t => t.LoanAccountId, cascadeDelete: true)
                .Index(t => t.LoanAccountId);
            
            CreateTable(
                "dbo.LoanDurations",
                c => new
                    {
                        LoanDurationId = c.Int(nullable: false, identity: true),
                        Duration = c.String(),
                    })
                .PrimaryKey(t => t.LoanDurationId);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        MaritalStatusId = c.String(nullable: false, maxLength: 128),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.MaritalStatusId);
            
            CreateTable(
                "dbo.SavingAccounts",
                c => new
                    {
                        SavingAccountId = c.Int(nullable: false, identity: true),
                        CustAccountId = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransferLimit = c.Long(nullable: false),
                        BranchCode = c.String(),
                    })
                .PrimaryKey(t => t.SavingAccountId)
                .ForeignKey("dbo.CustAccounts", t => t.CustAccountId, cascadeDelete: true)
                .Index(t => t.CustAccountId);
            
            CreateTable(
                "dbo.SavingAccTxns",
                c => new
                    {
                        SavingAccTxnId = c.Int(nullable: false, identity: true),
                        SavingAccountId = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TxnDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TxnDetails = c.String(),
                    })
                .PrimaryKey(t => t.SavingAccTxnId)
                .ForeignKey("dbo.SavingAccounts", t => t.SavingAccountId, cascadeDelete: true)
                .Index(t => t.SavingAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingAccTxns", "SavingAccountId", "dbo.SavingAccounts");
            DropForeignKey("dbo.SavingAccounts", "CustAccountId", "dbo.CustAccounts");
            DropForeignKey("dbo.LoanAccTxns", "LoanAccountId", "dbo.LoanAccounts");
            DropForeignKey("dbo.LoanAccounts", "CustAccountId", "dbo.CustAccounts");
            DropForeignKey("dbo.CustAccounts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "ZipCodeId", "dbo.ZipCodes");
            DropForeignKey("dbo.ZipCodes", "CityId", "dbo.Cities");
            DropForeignKey("dbo.CustAccounts", "AccountTypeId", "dbo.AccountTypes");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropIndex("dbo.SavingAccTxns", new[] { "SavingAccountId" });
            DropIndex("dbo.SavingAccounts", new[] { "CustAccountId" });
            DropIndex("dbo.LoanAccTxns", new[] { "LoanAccountId" });
            DropIndex("dbo.LoanAccounts", new[] { "CustAccountId" });
            DropIndex("dbo.ZipCodes", new[] { "CityId" });
            DropIndex("dbo.Customers", new[] { "ZipCodeId" });
            DropIndex("dbo.CustAccounts", new[] { "AccountTypeId" });
            DropIndex("dbo.CustAccounts", new[] { "CustomerId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropTable("dbo.SavingAccTxns");
            DropTable("dbo.SavingAccounts");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.LoanDurations");
            DropTable("dbo.LoanAccTxns");
            DropTable("dbo.LoanAccounts");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.Customers");
            DropTable("dbo.CustAccounts");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.AccountTypes");
        }
    }
}
