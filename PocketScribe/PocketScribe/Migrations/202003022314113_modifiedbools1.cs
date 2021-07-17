namespace PocketScribe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedbools1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transcripts", "stopped", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transcripts", "paused", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transcripts", "paused");
            DropColumn("dbo.Transcripts", "stopped");
        }
    }
}
