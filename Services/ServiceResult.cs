using App.Services.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        [JsonIgnore] public string?  UrlAsCreated { get; set; }


        //static factory methods    
        public static ServiceResult<T> Success(T data,HttpStatusCode status=HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                StatusCode = status
            };
            
        }

        public static ServiceResult<T> SuccessAsCreated(T data,string UrlAsCreated )
        {
            return new ServiceResult<T>()
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                UrlAsCreated=UrlAsCreated
            };

        }
        public static ServiceResult<T> Success (T data)
        {
            return new ServiceResult<T>()
            {
                Data = data
            };
        }

        public static ServiceResult<T> Fail(List<string> errors, T data, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = errors,
                StatusCode = status
            };
        }
        public static ServiceResult<T> Fail(string error,HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = [error]
            };
        }

        
    }
    public class ServiceResult
    {
       
        public bool IsSucces => ErrorMessage == null || ErrorMessage.Count == 0;

        public List<string>? ErrorMessage { get; set; }
        public bool IsFail => !IsSucces;
        public HttpStatusCode StatusCode { get; set; }
        public object UrlAsCreated { get; set; }

        //static factory methods    
        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
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
                ErrorMessage = [error]
            };
        }

        
    }
}
