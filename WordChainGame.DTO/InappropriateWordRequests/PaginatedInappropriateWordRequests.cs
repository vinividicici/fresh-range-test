

namespace WordChainGame.DTO.InappropriateWordRequests
{
    using System.Collections.Generic;
    using WordChainGame.DTO.General;
    public class PaginatedInappropriateWordRequests : PaginationResponseModel
    {
        public ICollection<InappropriateWordRequestsResponseModel> InappropriateWordRequests { get; set; }
    }
}
