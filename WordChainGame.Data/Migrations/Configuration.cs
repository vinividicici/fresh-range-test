namespace WordChainGame.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using WordChainGame.Data.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<WordChainGameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WordChainGameContext context)
        {
           
        }
    }
}
