using System;

namespace osu_api_wrapper
{
    public class ApiKeyMissingException : Exception
    {
        public ApiKeyMissingException() : base("osu!api Key is missing") { }
    }
}
