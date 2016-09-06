namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistAndTrackAddSpotifyIdAndImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "SpotifyId", c => c.String());
            AddColumn("dbo.Albums", "ImageUrl", c => c.String());
            AddColumn("dbo.Tracks", "SpotifyId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "SpotifyId");
            DropColumn("dbo.Albums", "ImageUrl");
            DropColumn("dbo.Albums", "SpotifyId");
        }
    }
}
