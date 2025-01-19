using Domain.Analysis;
using Domain.Enum;
using Domain.Exception;
using Domain.Model;
using FluentAssertions;
using Test.Stubs;
using Test.Util;

namespace Test.Analysis;

public class DiscussionFormAnalyzerTest
{
    [Fact]
    public void AnalyzerWithValidInputShouldReturnCorrectResult()
    {
        // Arrange
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(
            MockMaker.GetBaseDiscussionForm(),
            MockMaker.GetTopics(), new TestDiscussionFormValidator(),
            new TestKeywordMatchingStrategy()
        );
        Topic expectedResult = MockMaker.GetTopics()[2];

        // Act
        List<Topic> result = analyzer.GetTopicsFromContext();

        //Assert
        result.First().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void InvalidDiscussionFormShouldThrow()
    {
        // Arrange
        Action act = () => new DiscussionFormAnalyzer(
            null,
            MockMaker.GetTopics(),
            new TestDiscussionFormValidator(),
            new TestKeywordMatchingStrategy()
        );

        //Act and assert
        act.Should().Throw<InvalidDiscussionFormException>();
    }

    [Fact]
    public void InvalidCollaborativeSpaceTypeShouldThrow()
    {
        CollaborativeSpace invalidSpace = MockMaker.GetBaseDiscussionForm();
        invalidSpace.Type = CollaborativeSpaceType.CHAT;
        Action act = () => new DiscussionFormAnalyzer(invalidSpace, MockMaker.GetTopics(),
            new TestDiscussionFormValidator(), new TestKeywordMatchingStrategy());

        act.Should().Throw<InvalidDiscussionFormException>();
    }

    [Fact]
    public void EmptyTopicListShouldThrow()
    {
        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), new List<Topic>(),
            new TestDiscussionFormValidator(), new TestKeywordMatchingStrategy());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void TopicWithoutKeywordsShouldThrow()
    {
        List<Topic> topicsWithoutKeywords = new List<Topic>
            { new Topic { Id = 1, Keywords = new List<TopicKeyword>() } };
        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithoutKeywords,
            new TestDiscussionFormValidator(), new TestKeywordMatchingStrategy());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void TopicWithEmptyKeywordNameShouldThrow()
    {
        List<Topic> topicsWithInvalidKeyword = new List<Topic>
        {
            new Topic
            {
                Id = 1,
                Keywords = new List<TopicKeyword>
                {
                    new TopicKeyword { Name = "", Weight = 1.0 }
                }
            }
        };

        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithInvalidKeyword,
            new TestDiscussionFormValidator(), new TestKeywordMatchingStrategy());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void TopicWithNonPositiveKeywordWeightShouldThrow()
    {
        List<Topic> topicsWithInvalidKeywordWeight = new List<Topic>
        {
            new Topic
            {
                Id = 1,
                Keywords = new List<TopicKeyword>
                {
                    new TopicKeyword { Name = "test", Weight = 0 }
                }
            }
        };

        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithInvalidKeywordWeight,
            new TestDiscussionFormValidator(), new TestKeywordMatchingStrategy());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void GetTopicsFromContextShouldReturnEmptyWhenNoRelevantTopics()
    {
        List<Topic> topics = MockMaker.GetTopics();
        CollaborativeSpace form = MockMaker.GetBaseDiscussionForm();
        form.Description = "No relevant keywords here.";
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(form, topics, new TestDiscussionFormValidator(),
            new TestKeywordMatchingStrategy());

        List<Topic> result = analyzer.GetTopicsFromContext();

        result.Should().BeEmpty();
    }
}