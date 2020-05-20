
namespace WordChainGame.Services.Services
{
    using WordChainGame.Data.Entities;

    public interface IUserService
    {
        void DeleteUserEntities(string userId);
    }
}
