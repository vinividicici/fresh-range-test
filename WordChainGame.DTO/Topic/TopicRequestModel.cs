
namespace WordChainGame.DTO.Topic
{
    using System.ComponentModel.DataAnnotations;

    public class TopicRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}
