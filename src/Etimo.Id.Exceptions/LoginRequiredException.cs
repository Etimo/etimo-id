namespace Etimo.Id.Exceptions
{
    public class LoginRequiredException : BadRequestException
    {
        /// <summary>
        ///     The resource owner or authorization server denied the request.
        ///     Read more: https://tools.ietf.org/html/rfc6749#section-4.1.2.1
        /// </summary>
        public LoginRequiredException(string message = "End-user authentication required.", string state = null)
            : base(message, "login_required")
        {
            State = state;
        }
    }
}
