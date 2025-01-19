using Domain.Interface;
using Domain.Model;
using Domain.Util;

namespace Test.Stubs;

public class TestKeywordMatchingStrategy : ITopicAnalysisStrategy
{
    public Dictionary<int, double> Analyze(CollaborativeSpace discussionForm, List<Topic> topics)
    {
        var scores = new Dictionary<int, double>();

        foreach (var topic in topics)
        {
            double score = 0;

            foreach (TopicKeyword keyword in topic.Keywords)
            {
                int occurrences = 
                    (discussionForm.Description + discussionForm.Name).CountSubstringOccurrences(keyword.Name);
                score += occurrences * keyword.Weight;
            }

            scores[topic.Id] = score;
        }
        return scores;
    }
}