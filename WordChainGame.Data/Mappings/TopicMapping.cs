

namespace WordChainGame.Data.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using WordChainGame.Data.Entities;

    public class TopicMapping : EntityTypeConfiguration<Topic>
    {
        public TopicMapping()
        {
            this.ToTable("Topics");

            this.HasKey(p => p.Id);

            this.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(p => p.WordsCount)
                .IsRequired();
            
            this.HasRequired(p => p.Author)
                .WithMany(p => p.Topics)
                .HasForeignKey(p => p.AuthorId);
        }
    }
}
