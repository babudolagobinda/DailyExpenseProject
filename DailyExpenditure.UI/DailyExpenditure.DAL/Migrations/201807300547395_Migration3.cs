namespace DailyExpenditure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseTypeMaster", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.PaymentTypeMaster", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExpenseTypeMaster", "UserId");
            CreateIndex("dbo.PaymentTypeMaster", "UserId");
            AddForeignKey("dbo.PaymentTypeMaster", "UserId", "dbo.UserDetail", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.ExpenseTypeMaster", "UserId", "dbo.UserDetail", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseTypeMaster", "UserId", "dbo.UserDetail");
            DropForeignKey("dbo.PaymentTypeMaster", "UserId", "dbo.UserDetail");
            DropIndex("dbo.PaymentTypeMaster", new[] { "UserId" });
            DropIndex("dbo.ExpenseTypeMaster", new[] { "UserId" });
            DropColumn("dbo.PaymentTypeMaster", "UserId");
            DropColumn("dbo.ExpenseTypeMaster", "UserId");
        }
    }
}
