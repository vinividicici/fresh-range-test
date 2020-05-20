
namespace WordChainGame.DTO.Topic
{
    using System.Collections.Generic;
    using WordChainGame.DTO.General;

    public class PaginatedTopicsResponseModel : PaginationResponseModel
    {
        public ICollection<ListedTopicResponseModel> Topics { get; set; }
    }
}
