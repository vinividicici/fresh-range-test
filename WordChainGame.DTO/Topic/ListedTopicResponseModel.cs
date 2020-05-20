namespace WordChainGame.DTO.Topic
{
    using WordChainGame.DTO.User;

    public class ListedTopicResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WordsCount { get; set; }
        public UserResponseModel Author { get; set; }
    }
}
