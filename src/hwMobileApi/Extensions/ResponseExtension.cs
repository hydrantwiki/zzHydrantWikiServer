using System.IO;
using HydrantWiki.Mobile.Api.ResponseObjects;
using Nancy;
using Newtonsoft.Json;

namespace HydrantWiki.Mobile.Api.Extensions
{
    public static class ResponseExtension
    {

        public static Response AsError(this IResponseFormatter _formatter, 
            HttpStatusCode _statusCode, BaseResponse _response)
        {
            string responseString = JsonConvert.SerializeObject(_response);

            return new Response
            {
                StatusCode = _statusCode,
                ContentType = "application/json",
                Contents = _stream => (new StreamWriter(_stream) { AutoFlush = true }).Write(responseString)
            };
        }

        public static Response AsSuccess(this IResponseFormatter _formatter, BaseResponse _response)
        {
            return AsSuccess(_formatter, HttpStatusCode.OK, _response);
        }

        public static Response AsSuccess(this IResponseFormatter _formatter, 
            HttpStatusCode _statusCode, BaseResponse _response)
        {
            string responseString = JsonConvert.SerializeObject(_response);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = "application/json",
                Contents = _stream => (new StreamWriter(_stream) {AutoFlush = true}).Write(responseString)
            };
        }
    }
}