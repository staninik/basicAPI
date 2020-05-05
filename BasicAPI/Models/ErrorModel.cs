using System;

namespace BasicAPI.Models
{
    public class ErrorModel
    {
        public ErrorModel(string message, int httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        public string Message { get; set; }

        public int HttpStatusCode { get; set; }
    }
}