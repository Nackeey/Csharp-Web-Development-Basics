namespace SIS.HTTP.Exceptions
{
    using System;
    using System.Net;

    public class BadRequestException : Exception
    {
        private const string errorMessage = "The Request was malformed of contains unsupported elements.";

        public const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
    }
}
