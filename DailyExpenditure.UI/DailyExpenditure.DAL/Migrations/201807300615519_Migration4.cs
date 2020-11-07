namespace DailyExpenditure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalaryMaster", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.SalaryMaster", "UserId");
            AddForeignKey("dbo.SalaryMaster", "UserId", "dbo.UserDetail", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalaryMaster", "UserId", "dbo.UserDetail");
            DropIndex("dbo.SalaryMaster", new[] { "UserId" });
            DropColumn("dbo.SalaryMaster", "UserId");
        }
    }
}
