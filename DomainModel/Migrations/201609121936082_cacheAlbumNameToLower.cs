namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cacheAlbumNameToLower : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "NameToLower", c => c.String());
            Sql(@"UPDATE a
                    SET a.NameToLower = LOWER(b.Name)
                    FROM dbo.Artists a
                    INNER JOIN dbo.Artists b
                    on a.Id = b.Id");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "NameToLower");
        }
    }
}
