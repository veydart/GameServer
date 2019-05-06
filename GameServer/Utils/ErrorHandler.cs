using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameServer.Utils
{
    public class ErrorHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ErrorModel error;
            error = new ErrorModel(context.Exception.Message, context.Exception.ToString());

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(error);
            context.ExceptionHandled = true;
            Console.WriteLine(context.Exception.ToString());
        }
    }
}