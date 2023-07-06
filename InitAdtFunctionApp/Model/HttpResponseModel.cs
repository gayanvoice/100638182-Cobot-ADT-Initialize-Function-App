using System.Net;

namespace InitAdtFunctionApp.Model
{
    internal class HttpResponseModel
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public double Duration { get; set; }
        public object Exception { get; set; }
    }
}