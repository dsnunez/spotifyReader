namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistAddColumnSpotifyId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "SpotifyId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "SpotifyId");
        }
    }
}
