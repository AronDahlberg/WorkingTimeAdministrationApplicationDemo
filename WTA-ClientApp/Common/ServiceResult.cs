using System.Net;

namespace WTA_ClientApp.Common
{
    public class ServiceResult<T>
    {
        /// <summary> True if the operation succeeded (HTTP 2xx); false otherwise. </summary>
        public bool IsSuccess { get; set; }

        /// <summary> The returned data, if any (null on failure). </summary>
        public T? Data { get; set; }

        /// <summary> A diagnostic error message. </summary>
        public string? ErrorMessage { get; set; }

        /// <summary> The HTTP status code returned from the API. </summary>
        public HttpStatusCode? StatusCode { get; set; }
    }
}
