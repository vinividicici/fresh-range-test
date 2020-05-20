namespace WordChainGame.Services.Services.Words
{
    public interface IWordService
    {
        void Delete(int wordId);
        void DeleteInappropriateWordRequestForWord(int wordId);
    }
}
