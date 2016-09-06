namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistAddColumnImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "ImageUrl");
        }
    }
}
