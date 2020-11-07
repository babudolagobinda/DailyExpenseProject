namespace DailyExpenditure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyFirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseMaster",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        NameOfExpense = c.String(),
                        Amount = c.String(),
                        Date = c.DateTime(nullable: false),
                        ExpenseTypeId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseId)
                .ForeignKey("dbo.ExpenseTypeMaster", t => t.ExpenseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTypeMaster", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.UserDetail", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ExpenseTypeId)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.ExpenseTypeMaster",
                c => new
                    {
                        ExpenseTypeId = c.Int(nullable: false, identity: true),
                        ExpenseType = c.String(),
                    })
                .PrimaryKey(t => t.ExpenseTypeId);
            
            CreateTable(
                "dbo.PaymentTypeMaster",
                c => new
                    {
                        PaymentTypeId = c.Int(nullable: false, identity: true),
                        PaymentType = c.String(),
                    })
                .PrimaryKey(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.UserDetail",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailId = c.String(),
                        PhoneNo = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        UserImage = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseMaster", "UserId", "dbo.UserDetail");
            DropForeignKey("dbo.ExpenseMaster", "PaymentTypeId", "dbo.PaymentTypeMaster");
            DropForeignKey("dbo.ExpenseMaster", "ExpenseTypeId", "dbo.ExpenseTypeMaster");
            DropIndex("dbo.ExpenseMaster", new[] { "PaymentTypeId" });
            DropIndex("dbo.ExpenseMaster", new[] { "ExpenseTypeId" });
            DropIndex("dbo.ExpenseMaster", new[] { "UserId" });
            DropTable("dbo.UserDetail");
            DropTable("dbo.PaymentTypeMaster");
            DropTable("dbo.ExpenseTypeMaster");
            DropTable("dbo.ExpenseMaster");
        }
    }
}
