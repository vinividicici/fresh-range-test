
namespace WordChainGame.DTO.Word
{
    using System.ComponentModel.DataAnnotations;

    public class WordRequestModel
    {
        [Required]
        public string Word { get; set; }
    }
}
