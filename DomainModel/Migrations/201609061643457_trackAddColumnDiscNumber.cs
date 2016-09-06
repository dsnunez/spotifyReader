namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trackAddColumnDiscNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "DiscNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "DiscNumber");
        }
    }
}
