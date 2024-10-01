namespace Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message = "Requested resource is not found")
            : base(message) { }
    }
}
