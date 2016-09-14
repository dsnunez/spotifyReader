namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTimestampColumArtistAlbumTrack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "LastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Artists", "LastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tracks", "LastUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "LastUpdated");
            DropColumn("dbo.Artists", "LastUpdated");
            DropColumn("dbo.Albums", "LastUpdated");
        }
    }
}
