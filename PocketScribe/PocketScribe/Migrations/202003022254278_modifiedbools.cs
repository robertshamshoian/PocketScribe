namespace PocketScribe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedbools : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transcripts", "stopped");
            DropColumn("dbo.Transcripts", "paused");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transcripts", "paused", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transcripts", "stopped", c => c.Boolean(nullable: false));
        }
    }
}
