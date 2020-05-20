
namespace WordChainGame.DTO.Word
{
    using System.Collections.Generic;
    using WordChainGame.DTO.General;

    public class PaginatedWordsResponseModel : PaginationResponseModel
    {
        public ICollection<ListedWordResponseModel> Words { get; set; }
    }
}
