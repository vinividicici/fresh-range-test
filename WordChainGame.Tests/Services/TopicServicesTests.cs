

namespace WordChainGame.Tests.Services
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using WordChainGame.Common.CustomExceptions;
    using WordChainGame.Data.Entities;
    using WordChainGame.DTO.Word;
    using WordChainGame.Services.Services;
    using WordChainGame.Services.UnitOfWork;
    using WordChainGame.Utils.MapperProfile;

    [TestFixture]
    public class TopicServicesTests
    {
        TopicService service;
        Mock<IUnitOfWork> mockedUnitOfWork;

        [SetUp]
        public void Setup()
        {
            mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WordChainGameProfile());
            });
            var mapper = new Mapper(mapperConfig);

            service = new TopicService(mockedUnitOfWork.Object, mapper);
        }

        [Test]
        public void AddWord_WhenTheWordsCountInTopicIsZero_ShouldInsertTheWord()
        {
            var topic = new Topic
            {
                Id = 1,
                WordsCount = 0,
            };
            var requestModel = new WordRequestModel { Word = "dog" };

            this.mockedUnitOfWork.Setup(x => x.Topics.Get(It.IsAny<Expression<Func<Topic, bool>>>(), null, It.IsAny<string>()))
                                 .Returns(new List<Topic> { topic });
            this.mockedUnitOfWork.Setup(x => x.Words.Insert(It.IsAny<Word>()));
            this.mockedUnitOfWork.Setup(x => x.Commit());

            this.service.AddWord(1, "userId", requestModel);


            this.mockedUnitOfWork.Verify(x => x.Words.Insert(It.IsAny<Word>()), Times.Once);
            this.mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);

        }

        [Test]
        public void AddWord_WhenTheWordIsAlreadyAddedToTheTopic_ShouldThrowExceptionWithCorrectMessage()
        {
            var wordsInTopic = new List<Word>
            {
                new Word { WordContent = "dog", TopicId = 1 }
            };
            var topic = new Topic
            {
                Id = 1,
                WordsCount = 0,
                Words = wordsInTopic
            };
            var requestModel = new WordRequestModel { Word = "dog" };
      
            this.mockedUnitOfWork.Setup(x => x.Topics.Get(It.IsAny<Expression<Func<Topic, bool>>>(), null, It.IsAny<string>()))
                                 .Returns(new List<Topic> { topic });
            this.mockedUnitOfWork.Setup(x => x.Words.Insert(It.IsAny<Word>()));
            this.mockedUnitOfWork.Setup(x => x.Commit());

            Assert.Throws<InvalidWordException>(() => this.service.AddWord(1, "userId", requestModel), message: "The word is already added in this topic.");
            this.mockedUnitOfWork.Verify(x => x.Words.Insert(It.IsAny<Word>()), Times.Never);
            this.mockedUnitOfWork.Verify(x => x.Commit(), Times.Never);

        }


        [Test]
        public void AddWord_WhenTheWordIsMarkedAsInappropriate_ShouldThrowExceptionWithCorrectMessage()
        {
            var wordsInTopic = new List<Word>
            {
                new Word { WordContent = "dog", TopicId = 1, IsDeleted = true }
            };
            var topic = new Topic
            {
                Id = 1,
                WordsCount = 0,
                Words = wordsInTopic
            };
            var requestModel = new WordRequestModel { Word = "dog" };

            this.mockedUnitOfWork.Setup(x => x.Topics.Get(It.IsAny<Expression<Func<Topic, bool>>>(), null, It.IsAny<string>()))
                                 .Returns(new List<Topic> { topic });
            this.mockedUnitOfWork.Setup(x => x.Words.Insert(It.IsAny<Word>()));
            this.mockedUnitOfWork.Setup(x => x.Commit());

            Assert.Throws<InvalidWordException>(() => this.service.AddWord(1, "userId", requestModel), message: "The word is already marked as inappropriate in this topic.");
            this.mockedUnitOfWork.Verify(x => x.Words.Insert(It.IsAny<Word>()), Times.Never);
            this.mockedUnitOfWork.Verify(x => x.Commit(), Times.Never);

        }

        [Test]
        public void AddWord_WhenTheWordDoesntStartWithCorrectCharachter_ShouldThrowExceptionWithCorrectMessage()
        {
            var wordsInTopic = new List<Word>
            {
                new Word { WordContent = "dog", TopicId = 1, IsDeleted = false}
            };
            var topic = new Topic
            {
                Id = 1,
                WordsCount = 0,
                Words = wordsInTopic
            };
            var requestModel = new WordRequestModel { Word = "cat" };

            this.mockedUnitOfWork.Setup(x => x.Topics.Get(It.IsAny<Expression<Func<Topic, bool>>>(), null, It.IsAny<string>()))
                                 .Returns(new List<Topic> { topic });
            this.mockedUnitOfWork.Setup(x => x.Words.Insert(It.IsAny<Word>()));
            this.mockedUnitOfWork.Setup(x => x.Commit());

            Assert.Throws<InvalidWordException>(() => this.service.AddWord(1, "userId", requestModel), message: "The new word should start with t.");
            this.mockedUnitOfWork.Verify(x => x.Words.Insert(It.IsAny<Word>()), Times.Never);
            this.mockedUnitOfWork.Verify(x => x.Commit(), Times.Never);

        }

        [Test]
        public void AddWord_WhenTheWordStartsWithCorrectCharachterAndIsUnique_ShouldInsertTheWord()
        {
            var wordsInTopic = new List<Word>
            {
                new Word { WordContent = "cat", TopicId = 1, IsDeleted = false}
            };
            var topic = new Topic
            {
                Id = 1,
                WordsCount = 1,
                Words = wordsInTopic
            };
            var requestModel = new WordRequestModel { Word = "turtle" };

            this.mockedUnitOfWork.Setup(x => x.Topics.Get(It.IsAny<Expression<Func<Topic, bool>>>(), null, It.IsAny<string>()))
                                 .Returns(new List<Topic> { topic });
            this.mockedUnitOfWork.Setup(x => x.Words.Insert(It.IsAny<Word>()));
            this.mockedUnitOfWork.Setup(x => x.Commit());

            var result = this.service.AddWord(1, "userId", requestModel);

            this.mockedUnitOfWork.Verify(x => x.Words.Insert(It.IsAny<Word>()), Times.Once);
            this.mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
            Assert.AreEqual(topic.WordsCount, 2);
        }
    }
}
