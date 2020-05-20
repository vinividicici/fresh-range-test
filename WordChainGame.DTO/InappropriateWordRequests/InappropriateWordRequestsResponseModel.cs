
namespace WordChainGame.DTO.InappropriateWordRequests
{
    using System;
    using WordChainGame.DTO.User;
    using WordChainGame.DTO.Word;

    public class InappropriateWordRequestsResponseModel
    {
        public int Id { get; set; }
        public int InappropriateWordId { get; set; }
        public bool IsInappropriate { get; set; }
        public string RequesterId { get; set; }
        public DateTime DateCreated { get; set; }
        public DetailsWordResponseModel InappropriateWord { get; set; }
        public UserResponseModel Requester { get; set; }
    }
}
