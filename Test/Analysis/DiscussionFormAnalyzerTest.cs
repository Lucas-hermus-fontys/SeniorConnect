using Domain.Analysis;
using Domain.Enum;
using Domain.Exception;
using Domain.Model;
using Domain.Validation;
using FluentAssertions;
using Test.Util;

namespace Test.Analysis;

public class DiscussionFormAnalyzerTest
{
    [Fact]
    public void AnalyzerWithValidInputShouldReturnCorrectResult()
    {
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), MockMaker.GetTopics(), new DiscussionFormValidator(), new KeywordMatchingStrategy());
        Topic expectedResult = MockMaker.GetTopics()[2];

        List<Topic> result = analyzer.GetTopicsFromContext();

        result.First().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void InvalidDiscussionFormShouldThrow()
    {
        Action act = () => new DiscussionFormAnalyzer(null, MockMaker.GetTopics(), new DiscussionFormValidator(), new KeywordMatchingStrategy());

        act.Should().Throw<InvalidDiscussionFormException>();
    }

    [Fact]
    public void InvalidCollaborativeSpaceTypeShouldThrow()
    {
        CollaborativeSpace invalidSpace = MockMaker.GetBaseDiscussionForm();
        invalidSpace.Type = CollaborativeSpaceType.CHAT; 
        Action act = () => new DiscussionFormAnalyzer(invalidSpace, MockMaker.GetTopics(), new DiscussionFormValidator(), new KeywordMatchingStrategy());

        act.Should().Throw<InvalidDiscussionFormException>();
    }

    [Fact]
    public void EmptyTopicListShouldThrow()
    {
        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), new List<Topic>(), new DiscussionFormValidator(), new KeywordMatchingStrategy());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void TopicWithoutKeywordsShouldThrow()
    {
        List<Topic> topicsWithoutKeywords = new List<Topic> { new Topic { Id = 1, Keywords = new List<TopicKeyword>() } };
        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithoutKeywords, new DiscussionFormValidator(), new KeywordMatchingStrategy());

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

        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithInvalidKeyword, new DiscussionFormValidator(), new KeywordMatchingStrategy());

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

        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithInvalidKeywordWeight, new DiscussionFormValidator(), new KeywordMatchingStrategy());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void GetTopicsFromContextShouldReturnEmptyWhenNoRelevantTopics()
    {
        List<Topic> topics = MockMaker.GetTopics();
        CollaborativeSpace form = MockMaker.GetBaseDiscussionForm();
        form.Description = "No relevant keywords here.";
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(form, topics, new DiscussionFormValidator(), new KeywordMatchingStrategy());

        List<Topic> result = analyzer.GetTopicsFromContext();

        result.Should().BeEmpty();
    }
}
