namespace Infrastructure.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(
            string message = "server is not responding, sorry for the inconvenience"
        )
            : base(message) { }
    }
}
