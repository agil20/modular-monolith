using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models;

public class ApiResponseModel
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

  
    public ApiResponseModel(bool isSuccess, int statusCode, string message, object data)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public ApiResponseModel(bool isSuccess, int statusCode, string message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
}