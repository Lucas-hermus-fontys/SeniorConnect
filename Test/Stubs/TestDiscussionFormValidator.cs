using Domain.Enum;
using Domain.Exception;
using Domain.Interface;
using Domain.Model;

namespace Test.Stubs;

public class TestDiscussionFormValidator : IDiscussionFormValidator
{
    public void Validate(List<Topic> topics, CollaborativeSpace discussionForm)
    {
        ValidateTopics(topics);
        ValidateDiscussionForm(discussionForm);
    }
    
    public void ValidateDiscussionForm(CollaborativeSpace discussionForm)
    {
        if (discussionForm is null) 
        { 
            throw new InvalidDiscussionFormException("DiscussionForm cannot be null"); 
        }

        if (discussionForm.Type != CollaborativeSpaceType.FORM) 
        { 
            throw new InvalidDiscussionFormException("The provided CollaborativeSpace was not a form"); 
        }
    }

    public void ValidateTopics(List<Topic> topics)
    {
        if (topics is null || !topics.Any()) 
        { 
            throw new InvalidTopicException("Topics cannot be null or empty"); 
        }

        foreach (var topic in topics)
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