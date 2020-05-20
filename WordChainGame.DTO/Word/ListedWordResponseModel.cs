
namespace WordChainGame.DTO.Word
{
    using WordChainGame.DTO.Topic;

    public class ListedWordResponseModel
    {

        public int Id { get; set; }
        public string WordContent { get; set; }
        public ListedTopicResponseModel Topic { get; set; }
    }
}
