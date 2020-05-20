
namespace WordChainGame.DTO.Topic
{
    using System.Collections.Generic;
    using WordChainGame.DTO.User;
    using WordChainGame.DTO.Word;

    public class DetailsTopicResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WordsCount { get; set; }
        public UserResponseModel Author { get; set; }
        public ICollection<ListedWordResponseModel> Words { get; set; }
    }
}
