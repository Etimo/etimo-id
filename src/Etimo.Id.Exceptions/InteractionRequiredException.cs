namespace Etimo.Id.Exceptions
{
    public class InteractionRequiredException : BadRequestException
    {
        /// <summary>
        ///     The resource owner or authorization server denied the request.
        ///     Read more: https://tools.ietf.org/html/rfc6749#section-4.1.2.1
        /// </summary>
        public InteractionRequiredException(string message = "End-user interaction is required.", string state = null)
            : base(message, "interaction_required")
        {
            State = state;
        }
    }
}
