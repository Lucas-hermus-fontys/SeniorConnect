using Domain.Model;

namespace Domain.Interface;

public interface IDiscussionFormValidator
{
    public void Validate(List<Topic> topics, CollaborativeSpace discussionForm);
    public void ValidateDiscussionForm(CollaborativeSpace discussionForm);
    public void ValidateTopics(List<Topic> topics);
}