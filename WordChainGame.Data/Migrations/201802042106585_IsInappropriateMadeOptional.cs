namespace WordChainGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsInappropriateMadeOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InappropriateWordRequestMappings", "IsInappropriate", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InappropriateWordRequestMappings", "IsInappropriate", c => c.Boolean(nullable: false));
        }
    }
}
