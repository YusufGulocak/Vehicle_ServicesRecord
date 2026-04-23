using System.Net;
using System.Text.Json.Serialization;

namespace App.Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; init; }
        [JsonIgnore]
        public bool IsSucces => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsFail => !IsSucces;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore]
        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                StatusCode = status
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated
            };
        }

        public static ServiceResult<T> Fail(List<string> errors, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = errors,
                StatusCode = status
            };
        }

        public static ServiceResult<T> Fail(string error, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = [error],
                StatusCode = status
            };
        }
    }

    public class ServiceResult
    {
        [JsonIgnore]
        public bool IsSucces => ErrorMessage == null || ErrorMessage.Count == 0;
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsFail => !IsSucces;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.NoContent)
        {
            return new ServiceResult()
            {
                StatusCode = status
            };
        }

        public static ServiceResult Fail(List<string> errors, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = errors,
                StatusCode = status
            };
        }

        public static ServiceResult Fail(string error, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = [error],
                StatusCode = status
            };
        }
    }
}
