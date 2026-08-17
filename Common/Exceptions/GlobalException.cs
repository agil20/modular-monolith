using Common.Models;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace Common.Exceptions;

public class GlobalException : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        int statuscode = exception switch
        {
            NotFoundException =>(int) HttpStatusCode.NotFound,
            DublicatedDataException =>(int) HttpStatusCode.BadRequest,
            ConfilictException =>(int) HttpStatusCode.BadRequest,
            _=>(int)HttpStatusCode.InternalServerError

        };

        var response = new ApiResponseModel
            (
            false,statuscode,exception.Message
            );

        httpContext.Response.StatusCode=statuscode;
        await httpContext.Response.WriteAsJsonAsync(response,cancellationToken);
        return true;
    }
}
