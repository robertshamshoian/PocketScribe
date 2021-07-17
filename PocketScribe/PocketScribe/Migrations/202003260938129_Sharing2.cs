namespace PocketScribe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sharing2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transcripts", "SharedUsers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transcripts", "SharedUsers", c => c.String());
        }
    }
}
