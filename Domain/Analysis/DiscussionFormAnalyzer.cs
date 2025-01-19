using System.Formats.Asn1;
using System.Text.RegularExpressions;
using Domain.Enum;
using Domain.Exception;
using Domain.Interface;
using Domain.Model;
using Domain.Util;

namespace Domain.Analysis;

public class DiscussionFormAnalyzer
{
    private readonly CollaborativeSpace _discussionForm;
    private readonly List<Topic> _topics;
    private readonly Double _threshold;
    private readonly IDiscussionFormValidator _validator;
    private readonly ITopicAnalysisStrategy _analysisStrategy;

    public DiscussionFormAnalyzer(
        CollaborativeSpace collaborativeSpace,
        List<Topic> topics,
        IDiscussionFormValidator discussionFormValidator,
        ITopicAnalysisStrategy analysisStrategy,
        Double threshold = 1.0 
    )
    {
        _discussionForm = collaborativeSpace;
        _topics = topics;
        _threshold = threshold;
        _validator = discussionFormValidator;
        _analysisStrategy = analysisStrategy;

        Validate();
    }

    public List<Topic> GetTopicsFromContext()
    {
        var scores = _analysisStrategy.Analyze(_discussionForm, _topics);

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

    private void Validate()
    {
        _validator.Validate(_topics, _discussionForm);
    }
}