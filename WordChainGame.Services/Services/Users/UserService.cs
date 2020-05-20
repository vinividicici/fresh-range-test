
namespace WordChainGame.Services.Services
{
    using WordChainGame.Services.UnitOfWork;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
      

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void DeleteUserEntities(string userId)
        {
            var topicsToDelete = unitOfWork.Topics.Get(x => x.AuthorId == userId);

            foreach (var topic in topicsToDelete)
            {
                unitOfWork.Topics.Delete(topic);
            }

            var wordsToDelete = unitOfWork.Words.Get(x => x.AuthorId == userId);

            foreach (var word in wordsToDelete)
            {
                unitOfWork.Words.Delete(word);
            }

            var inappropriateWordRequestsToDelete = unitOfWork.InappropriateWordRequests.Get(x => x.RequesterId == userId);

            foreach (var request in inappropriateWordRequestsToDelete)
            {
                unitOfWork.InappropriateWordRequests.Delete(request);
            }

            unitOfWork.Commit();
        }
    }
}
