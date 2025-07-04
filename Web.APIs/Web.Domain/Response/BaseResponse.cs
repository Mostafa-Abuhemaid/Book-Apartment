using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Application.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? TotalCount { get; set; } // عدد كل العناصر
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? PageNumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? PageSize { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? TotalPage { get; set; }
        public BaseResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
       
        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public BaseResponse(bool success, string message, T data, int totalCount,int pageNumber,int pageSize, int? totalPage)
        {
            Success = success;
            Message = message;
            Data = data;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPage= totalPage;
          
        }
    }
}
