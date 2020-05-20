
namespace WordChainGame.Services.UnitOfWork
{
    using System;
    using WordChainGame.Data.Entities;
    using WordChainGame.Services.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Topic> Topics { get; }
        IGenericRepository<InappropriateWordRequest> InappropriateWordRequests { get; }
        IGenericRepository<Word> Words { get; }
        IGenericRepository<User> Users { get; }
        void Commit();
    }
}
