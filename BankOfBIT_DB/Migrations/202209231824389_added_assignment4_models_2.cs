namespace BankOfBIT_DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_assignment4_models_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false, identity: true),
                        InstitutionNumber = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionId);
            
            CreateTable(
                "dbo.NextUniqueNumbers",
                c => new
                    {
                        NextUniqueNumberId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.NextUniqueNumberId);
            
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PayeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payees");
            DropTable("dbo.NextUniqueNumbers");
            DropTable("dbo.Institutions");
        }
    }
}
