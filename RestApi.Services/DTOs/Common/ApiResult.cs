using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace RestApi.Core.DTOs.Common
{
    [Serializable]
    public class ApiResult<T> : IApiResult
    {
        public ApiResult()
        {
            this.Success = true;
        }

        public ApiResult(T data) : this()
        {
            this.Data = data;

            if (typeof(T) == typeof(bool))
                this.Success = (bool)Convert.ChangeType(data, typeof(bool));
        }

        public ApiResult(T data, params string[] messages) : this(data)
        {
            this.Messages = messages;
        }

        public ApiResult(T data, bool success) : this(data)
        {
            this.Success = success;
        }

        public ApiResult(bool success, params string[] messages) : this(default(T), messages)
        {
            this.Success = success;
        }

        public bool Success { get; set; }

        public T Data { get; set; }

        public string[] Messages { get; set; }

        object IApiResult.Data => Data;
    }

    [Serializable]
    public class ApiResult<Source, Target> : ApiResult<Target>
    {
        public ApiResult() : base()
        {

        }

        public ApiResult(IMapper mapper, Source data) : this()
        {
            if (data != null)
                this.Data = mapper.Map<Source, Target>(data);
        }
    }

    public interface IApiResult
    {
        bool Success { get; }
        string[] Messages { get; }
        object Data { get; }
    }
}
