
namespace WordChainGame.DTO.Word
{
    using System;
    using WordChainGame.DTO.Topic;
    using WordChainGame.DTO.User;

    public class DetailsWordResponseModel
    {
        public int Id { get; set; }
        public string WordContent { get; set; }
        public DateTime DateCreated { get; set; }
        public ListedTopicResponseModel Topic { get; set; }
        public UserResponseModel Author { get; set; }
    }
}
