using Domain.Analysis;
using Domain.Enum;
using Domain.Exception;
using Domain.Model;
using FluentAssertions;
using Test.Util;

namespace Test.Analysis;

public class DiscussionFormAnalyzerTest
{
    [Fact]
    public void AnalyzerWithValidInputShouldReturnCorrectResult()
    {
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), MockMaker.GetTopics());
        Topic expectedResult = MockMaker.GetTopics()[2];

        List<Topic> result = analyzer.GetTopicsFromContext();

        result.First().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void InvalidDiscussionFormShouldThrow()
    {
        Action act = () => new DiscussionFormAnalyzer(null, MockMaker.GetTopics());

        act.Should().Throw<InvalidDiscussionFormException>();
    }

    [Fact]
    public void InvalidCollaborativeSpaceTypeShouldThrow()
    {
        CollaborativeSpace invalidSpace = MockMaker.GetBaseDiscussionForm();
        invalidSpace.Type = CollaborativeSpaceType.CHAT; 
        Action act = () => new DiscussionFormAnalyzer(invalidSpace, MockMaker.GetTopics());

        act.Should().Throw<InvalidDiscussionFormException>();
    }

    [Fact]
    public void EmptyTopicListShouldThrow()
    {
        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), new List<Topic>());

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void TopicWithoutKeywordsShouldThrow()
    {
        List<Topic> topicsWithoutKeywords = new List<Topic> { new Topic { Id = 1, Keywords = new List<TopicKeyword>() } };
        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithoutKeywords);

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

        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithInvalidKeyword);

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

        Action act = () => new DiscussionFormAnalyzer(MockMaker.GetBaseDiscussionForm(), topicsWithInvalidKeywordWeight);

        act.Should().Throw<InvalidTopicException>();
    }

    [Fact]
    public void GetTopicsFromContextShouldReturnEmptyWhenNoRelevantTopics()
    {
        List<Topic> topics = MockMaker.GetTopics();
        CollaborativeSpace form = MockMaker.GetBaseDiscussionForm();
        form.Description = "No relevant keywords here.";
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(form, topics);

        List<Topic> result = analyzer.GetTopicsFromContext();

        result.Should().BeEmpty();
    }
}
