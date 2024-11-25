namespace Infrastructure.Exception
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException() : base("User not found.")
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}