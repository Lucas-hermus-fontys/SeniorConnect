namespace Domain.Exception
{
    public class EmailAlreadyExistsException : System.Exception
    {
        public EmailAlreadyExistsException() : base("Email already exists")
        {
        }

        public EmailAlreadyExistsException(string message) : base(message)
        {
        }

        public EmailAlreadyExistsException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}