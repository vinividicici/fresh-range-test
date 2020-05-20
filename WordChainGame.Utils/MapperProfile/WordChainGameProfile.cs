
namespace WordChainGame.Utils.MapperProfile
{
    using AutoMapper;
    using WordChainGame.Data.Entities;
    using WordChainGame.DTO.InappropriateWordRequests;
    using WordChainGame.DTO.Topic;
    using WordChainGame.DTO.User;
    using WordChainGame.DTO.Word;

    public class WordChainGameProfile : Profile
    {
        public WordChainGameProfile()
        {
            //Topics
            CreateMap<TopicRequestModel, Topic>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.AuthorId, opts => opts.Ignore())
                .ForMember(dest => dest.WordsCount, opts => opts.Ignore())
                .ForMember(dest => dest.Author, opts => opts.Ignore())
                .ForMember(dest => dest.Words, opts => opts.Ignore());

            CreateMap<Topic, DetailsTopicResponseModel>();
            CreateMap<Topic, ListedTopicResponseModel>();
            
            //Users
            CreateMap<User, UserResponseModel>();

            //Words
            CreateMap<Word, DetailsWordResponseModel>();
            CreateMap<Word, ListedWordResponseModel>();
            CreateMap<WordRequestModel, Word>()
                .ForMember(dest => dest.WordContent, opts => opts.MapFrom(w => w.Word))
                .ForAllOtherMembers(opts => opts.Ignore());

        }
    }
}
