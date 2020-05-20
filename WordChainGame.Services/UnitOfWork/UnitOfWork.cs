
namespace WordChainGame.Services.UnitOfWork
{
    using WordChainGame.Data.Entities;
    using WordChainGame.Data.Model;
    using WordChainGame.Services.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        WordChainGameContext dbContext;
        public IGenericRepository<InappropriateWordRequest> inappropriateWordRequests;
        public IGenericRepository<Topic> topics;
        public IGenericRepository<Word> words;
        public IGenericRepository<User> users;
        private bool isDisposed;


        public UnitOfWork(WordChainGameContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IGenericRepository<Topic> Topics
        {
            get
            {
                if (topics == null)
                {
                    topics =  new GenericRepository<Topic>(dbContext);
                }
                return topics;
            }
        }
        

        public IGenericRepository<InappropriateWordRequest> InappropriateWordRequests
        {
            get
            {
                if (inappropriateWordRequests == null)
                {
                    inappropriateWordRequests = new GenericRepository<InappropriateWordRequest>(dbContext);
                }
                return inappropriateWordRequests;
            }
        }

        public IGenericRepository<Word> Words
        {
            get
            {
                if (words == null)
                {
                    words = new GenericRepository<Word>(dbContext);
                }
                return words;
            }
        }

        public IGenericRepository<User> Users
        {
            get
            {
                if (users == null)
                {
                    users = new GenericRepository<User>(dbContext);
                }
                return users;
            }
        }

      

        public void Commit()
        {
            dbContext.SaveChanges();
        }


        public void Dispose()
        {
            if (isDisposed)
                return;

            isDisposed = true;
            dbContext.Dispose();
        }
    }
}
