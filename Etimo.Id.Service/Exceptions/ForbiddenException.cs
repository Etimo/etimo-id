namespace Etimo.Id.Service.Exceptions
{
    public class ForbiddenException : ErrorCodeException
    {
        /// <summary>
        /// Used when an authenticated user is trying to access protected data that it does not have access to.
        /// </summary>
        public ForbiddenException(string message = null) : base(message, "forbidden")
        {
        }
    }
}
