
namespace WordChainGame.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Word
    {
        public Word()
        {
            this.InappropriateWordRequests = new HashSet<InappropriateWordRequest>();
        }
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string WordContent { get; set; }
        public string AuthorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set  ; }
        public Topic Topic { get; set; }
        public User Author { get; set; }
        public virtual ICollection<InappropriateWordRequest> InappropriateWordRequests { get; set; }

    }
}
