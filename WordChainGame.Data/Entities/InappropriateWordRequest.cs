
namespace WordChainGame.Data.Entities
{
    using System;

    public partial class InappropriateWordRequest
    { 
        public int Id { get; set; }
        public int InappropriateWordId { get; set; }
        public bool? IsInappropriate { get; set; }
        public string RequesterId { get; set; }
        public DateTime DateCreated { get; set; }
        public Word InappropriateWord { get; set; }
        public User Requester { get; set; }
    }
}
