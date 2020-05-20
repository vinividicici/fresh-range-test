namespace WordChainGame.Services.Services
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordChainGame.Common.CustomExceptions;
    using WordChainGame.Data.Entities;
    using WordChainGame.DTO.Topic;
    using WordChainGame.DTO.Word;
    using WordChainGame.Services.UnitOfWork;


    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public PaginatedTopicsResponseModel Get(string orderBy, int top, int skip)
        {
            var topics = new List<Topic>();
            if (orderBy.ToLower() == "name")
            {
                topics = unitOfWork.Topics.Get(orderBy: t => t.OrderBy(s => s.Name), includeProperties: "Author").ToList();
            }
            else if (orderBy.ToLower() == "wordscount")
            {
                topics = unitOfWork.Topics.Get(orderBy: t => t.OrderBy(s => s.WordsCount), includeProperties: "Author").ToList();
            }
            else
            {
                topics = unitOfWork.Topics.Get(includeProperties: "Author").ToList();
            }

            var count = topics.Count;
            var paginatedTopics = topics.Skip(skip).Take(top);
            var response = new PaginatedTopicsResponseModel
            {
                Topics = this.mapper.Map<ICollection<ListedTopicResponseModel>>(paginatedTopics),
                Count = count,
                NextPageUrl = top + skip > count ? null : string.Format("api/topics?orderby={0}&top={1}&skip={2}", orderBy, top + skip, top),
                PreviousPageUrl = skip - top < 0 ? null : string.Format("api/topics?orderby={0}&top={1}&skip={2}", orderBy, skip - top, top),
            };
            return response;
        }

        public PaginatedWordsResponseModel GetWords(int topicId, int top, int skip)
        {
            var words = unitOfWork.Words.Get(x => x.TopicId == topicId, includeProperties: "Author");
            var count = words.Count();
            var paginatedWords = words.Skip(skip).Take(top);
            var response = new PaginatedWordsResponseModel
            {
                Words = mapper.Map<ICollection<ListedWordResponseModel>>(paginatedWords),
                Count = count,
                NextPageUrl = top + skip > count ? null : string.Format("api/topics/{0}/words&top={1}&skip={2}", topicId, top + skip, top),
                PreviousPageUrl = skip - top < 0 ? null : string.Format("api/topics/{0}/words&top={1}&skip={2}", topicId, skip - top, top),
            };
            return response;
        }

        public ListedWordResponseModel AddWord(int topicId, string userId, WordRequestModel model)
        {
            var topic = unitOfWork.Topics
                                  .Get(filter: t => t.Id == topicId,
                                       includeProperties: "Words")
                                  .SingleOrDefault();

            var lastWord = topic.Words.OrderBy(w => w.DateCreated)
                                      .LastOrDefault();

            if(lastWord != null)
            {
                var lastWordLastCharachter = lastWord.WordContent.First().ToString();

                if (topic.Words.Where(w => !w.IsDeleted).Select(w => w.WordContent).Contains(model.Word))
                {
                    throw new InvalidWordException($"The word is already added in this topic.");
                }


                if (topic.Words.Where(w => w.IsDeleted).Select(w => w.WordContent).Contains(model.Word))
                {
                    throw new Exception($"The word is already marked as inappropriate in this topic.");
                }

                if (!model.Word.StartsWith(lastWordLastCharachter))
                {
                    throw new InvalidWordException($"The new word should start with {lastWordLastCharachter}.");
                }
            }
           
            var word = mapper.Map<Word>(model);
            word.AuthorId = userId;
            word.DateCreated = DateTime.Now;
            word.TopicId = topicId;

            unitOfWork.Words.Insert(word);
            topic.WordsCount++;

            unitOfWork.Commit();

            return mapper.Map<ListedWordResponseModel>(word);

        }

        public void RequestWordAsInappropriate(string requesterId, int topicId, int wordId)
        {
            var inappropriateWordRequest = new InappropriateWordRequest
            {
                DateCreated = DateTime.Now,
                InappropriateWordId = wordId,
                RequesterId = requesterId
            };

            unitOfWork.InappropriateWordRequests.Insert(inappropriateWordRequest);
            unitOfWork.Commit();
        }
    }
}
