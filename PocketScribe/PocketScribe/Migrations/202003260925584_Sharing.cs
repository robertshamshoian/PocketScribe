namespace PocketScribe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sharing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TranscriptID = c.Int(nullable: false),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transcripts", t => t.TranscriptID, cascadeDelete: true)
                .Index(t => t.TranscriptID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shares", "TranscriptID", "dbo.Transcripts");
            DropIndex("dbo.Shares", new[] { "TranscriptID" });
            DropTable("dbo.Shares");
        }
    }
}
