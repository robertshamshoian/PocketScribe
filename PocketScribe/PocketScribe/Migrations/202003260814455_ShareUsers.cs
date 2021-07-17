namespace PocketScribe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShareUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transcripts", "SharedUsers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transcripts", "SharedUsers");
        }
    }
}
