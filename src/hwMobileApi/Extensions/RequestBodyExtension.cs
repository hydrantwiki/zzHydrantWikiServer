using System.IO;
using Nancy.IO;

namespace HydrantWiki.Mobile.Api.Extensions
{
    public static class RequestBodyExtensions
    {
        public static string ReadAsString(this RequestStream _requestStream)
        {
            using (var reader = new StreamReader(_requestStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}