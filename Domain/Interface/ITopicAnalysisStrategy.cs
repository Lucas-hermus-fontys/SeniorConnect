using Domain.Model;

namespace Domain.Interface;

public interface ITopicAnalysisStrategy
{
    Dictionary<int, double> Analyze(CollaborativeSpace discussionForm, List<Topic> topics);
}