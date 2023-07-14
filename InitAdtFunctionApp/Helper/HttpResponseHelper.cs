using CobotADTInitializeFunctionApp.Model;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CobotADTInitializeFunctionApp.Helper
{
    internal class HttpResponseHelper
    {
        private Stopwatch _stopwatch;
        public HttpResponseHelper()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        private static HttpResponseMessage Create(HttpResponseModel httpResponseModel)
        {
            string httpResponseModelJson = JsonConvert.SerializeObject(httpResponseModel);
            return new HttpResponseMessage(httpResponseModel.HttpStatusCode)
            {
                Content = new StringContent(httpResponseModelJson, Encoding.UTF8, "application/json")
            };
        }
        public HttpResponseMessage CreateOkRequest(string message)
        {
            _stopwatch.Stop();
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            httpResponseModel.HttpStatusCode = HttpStatusCode.OK;
            httpResponseModel.Message = message;
            httpResponseModel.Duration = _stopwatch.Elapsed.TotalMilliseconds;
            return Create(httpResponseModel);
        }
        public HttpResponseMessage CreateBadRequest(string message)
        {
            _stopwatch.Stop();
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            httpResponseModel.HttpStatusCode = HttpStatusCode.BadRequest;
            httpResponseModel.Message = message;
            httpResponseModel.Duration = _stopwatch.Elapsed.TotalMilliseconds;
            return Create(httpResponseModel);
        }
        public HttpResponseMessage CreateBadRequest(string message, object exception)
        {
            _stopwatch.Stop();
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            httpResponseModel.HttpStatusCode = HttpStatusCode.BadRequest;
            httpResponseModel.Message = message;
            httpResponseModel.Exception = exception;
            httpResponseModel.Duration = _stopwatch.Elapsed.TotalMilliseconds;
            return Create(httpResponseModel);
        }
    }
}