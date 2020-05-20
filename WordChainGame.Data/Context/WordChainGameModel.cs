
namespace WordChainGame.Data.Model
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using WordChainGame.Data.Entities;
    using WordChainGame.Data.Mappings;

    public partial class WordChainGameContext : IdentityDbContext<User>
    {
        public WordChainGameContext()
            : base("name=WordChainGameConnection")
        {
        }

        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<InappropriateWordRequest> InappropriateWordRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InappropriateWordRequestMapping());
            modelBuilder.Configurations.Add(new TopicMapping());
            modelBuilder.Configurations.Add(new WordMapping());
            base.OnModelCreating(modelBuilder);

        }
    }
}

