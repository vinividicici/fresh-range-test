namespace WordChainGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsDeletedInWords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Words", "IsDeleted", c => c.Boolean(nullable: false));
            Sql("INSERT INTO dbo.AspNetRoles (Id, Name) VALUES (0, 'Admin')");
            Sql("INSERT INTO dbo.AspNetRoles(Id, Name) VALUES(1, 'User')");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Words", "IsDeleted");
        }
    }
}
