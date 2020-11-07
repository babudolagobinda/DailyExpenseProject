namespace DailyExpenditure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalaryMaster",
                c => new
                    {
                        SalaryId = c.Int(nullable: false, identity: true),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProvidentFund = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncomeTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExtraDeduct = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeductWithReturn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryInHand = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalaryMaster");
        }
    }
}
