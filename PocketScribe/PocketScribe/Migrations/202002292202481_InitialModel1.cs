namespace PocketScribe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transcripts", "UserName", c => c.String());
            DropColumn("dbo.Transcripts", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transcripts", "User", c => c.String());
            DropColumn("dbo.Transcripts", "UserName");
        }
    }
}
