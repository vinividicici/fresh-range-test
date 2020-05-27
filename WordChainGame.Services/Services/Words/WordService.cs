

namespace WordChainGame.Services.Services.Words
{
    using System.Linq;
    using WordChainGame.Services.UnitOfWork;
    public class WordService : IWordService
    {
        private readonly IUnitOfWork unitOfWork;

        public WordService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void Delete(int wordId)
        {
            var wordToDelete = unitOfWork.Words.Get(r => r.Id == wordId);

            var topic = unitOfWork.Topics.GetByID(wordToDelete.FirstOrDefault().TopicId);          

            wordToDelete.FirstOrDefault().IsDeleted = true;

            topic.WordsCount--;            

            unitOfWork.Commit();
            
        }

        public void DeleteInappropriateWordRequestForWord(int wordId)
        {
            var inappropriateRequest = unitOfWork.InappropriateWordRequests
                                                 .Get(filter: r => r.InappropriateWordId == wordId,
                                                      includeProperties: "InappropriateWord.Topic")
                                                 .ToList();

            for (int i = 0; i < inappropriateRequest.Count(); i++)
            {
                inappropriateRequest[i].IsInappropriate = false;
            }

            unitOfWork.Commit();
        }
    }
}
