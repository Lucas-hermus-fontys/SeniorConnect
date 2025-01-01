using System.Formats.Asn1;
using System.Text.RegularExpressions;
using Domain.Enum;
using Domain.Exception;
using Domain.Model;
using Domain.Util;

namespace Domain.Analysis;

public class DiscussionFormAnalyzer
{
    private readonly CollaborativeSpace _discussionForm;
    private readonly List<Topic> _topics;
    private readonly Double _threshold;

    public DiscussionFormAnalyzer(CollaborativeSpace collaborativeSpace, List<Topic> topics, Double threshold = 1.0)
    {
        _discussionForm = collaborativeSpace;
        _topics = topics;
        Validate();
        _threshold = threshold;
    }

    public List<Topic> GetTopicsFromContext()
    {
        var scores = new Dictionary<int, double>();
        
        foreach (var topic in _topics)
        {
            double score = 0;

            foreach (TopicKeyword keyword in topic.Keywords)
            {
                int occurrences = (_discussionForm.Description + _discussionForm.Name).CountSubstringOccurrences(keyword.Name);
                score += occurrences * keyword.Weight;
            }

            scores[topic.Id] = score;
        }

        var relevantTopics = scores.Where(s => s.Value >= _threshold)
            .OrderByDescending(s => s.Value)
            .Select(s => s.Key)
            .ToList();

        if (relevantTopics.Count == 0)
        {
            return new List<Topic>();
        }
        return _topics.Where(t => relevantTopics.Contains(t.Id)).ToList();
    }
    
    public void Validate()
    {
        if (_discussionForm is null) 
        { 
            throw new InvalidDiscussionFormException("DiscussionForm cannot be null"); 
        }

        if (_discussionForm.Type != CollaborativeSpaceType.FORM) 
        { 
            throw new InvalidDiscussionFormException("The provided CollaborativeSpace was not a form"); 
        }

        if (_topics is null || !_topics.Any()) 
        { 
            throw new InvalidTopicException("Topics cannot be null or empty"); 
        }

        foreach (var topic in _topics)
        {
            if (topic.Keywords == null || !topic.Keywords.Any())
            {
                throw new InvalidTopicException($"Topic {topic.Id} does not have any keywords.");
            }

            foreach (var keyword in topic.Keywords)
            {
                if (string.IsNullOrWhiteSpace(keyword.Name))
                {
                    throw new InvalidTopicException($"Keyword in Topic {topic.Id} has no name.");
                }

                if (keyword.Weight <= 0)
                {
                    throw new InvalidTopicException($"Keyword {keyword.Name} in Topic {topic.Id} has a non-positive weight.");
                }
            }
        }
    }
}