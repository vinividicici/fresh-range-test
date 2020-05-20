namespace WordChainGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedGameEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "GameId", "dbo.Games");
            DropIndex("dbo.Topics", new[] { "GameId" });
            DropTable("dbo.Games");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Topics", "GameId");
            AddForeignKey("dbo.Topics", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
    }
}
