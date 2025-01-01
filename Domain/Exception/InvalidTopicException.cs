namespace Domain.Exception;

public class InvalidTopicException : System.Exception
{
    public InvalidTopicException() : base("Invalid Topic")
    {
        
    }
    
    public InvalidTopicException(string message) : base("Invalid Topic: " + message)
    {
        
    }
}