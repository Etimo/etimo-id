namespace Etimo.Id.Exceptions
{
    public class ConsentRequiredException : BadRequestException
    {
        /// <summary>
        ///     The resource owner or authorization server denied the request.
        ///     Read more: https://tools.ietf.org/html/rfc6749#section-4.1.2.1
        /// </summary>
        public ConsentRequiredException(string message = "End-user consent is required.", string state = null)
            : base(message, "consent_required")
        {
            State = state;
        }
    }
}
