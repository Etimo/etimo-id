namespace Etimo.Id.Exceptions
{
    public class UnauthorizedClientException : BadRequestException
    {
        /// <summary>
        ///     The authenticated client is not authorized to use this authorization grant type.
        ///     Read more:
        ///     - https://tools.ietf.org/html/rfc6749#section-4.1.2.1
        ///     - https://tools.ietf.org/html/rfc6749#section-5.2
        /// </summary>
        public UnauthorizedClientException(string message, string state = null)
            : base(message, "unauthorized_client")
        {
            State = state;
        }
    }
}
