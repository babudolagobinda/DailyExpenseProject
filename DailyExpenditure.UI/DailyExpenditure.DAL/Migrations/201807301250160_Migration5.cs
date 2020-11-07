namespace DailyExpenditure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalaryMaster", "Month", c => c.String());
            AddColumn("dbo.SalaryMaster", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalaryMaster", "Year");
            DropColumn("dbo.SalaryMaster", "Month");
        }
    }
}
