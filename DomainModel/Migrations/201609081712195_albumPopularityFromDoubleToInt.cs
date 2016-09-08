namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class albumPopularityFromDoubleToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "Popularity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "Popularity", c => c.Double(nullable: false));
        }
    }
}
