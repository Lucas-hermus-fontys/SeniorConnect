namespace Domain.Exception;

public class InvalidDiscussionFormException : System.Exception
{
    public InvalidDiscussionFormException() : base("Invalid Discussion Form")
    {
        
    }
    
    public InvalidDiscussionFormException(string message) : base("Invalid Discussion Form: " + message)
    {
        
    }
}