using Domain.Interface;
using Domain.Model;

namespace Domain.Validation;

public class TopicValidator : IValidator<Topic>, IBatchValidator<Topic>
{
    public void Validate(List<Topic> topics)
    {
        
    }
    
    public void Validate(Topic topics)
    {
        
    }
}