namespace WordChainGame.DTO.General
{
    public class PaginationResponseModel
    {
        public int Count { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
    }
}
