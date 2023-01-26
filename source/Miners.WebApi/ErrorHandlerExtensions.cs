using Microsoft.AspNetCore.Mvc;
using Miners.Domain.Exceptions.Results;
using System.ComponentModel.DataAnnotations;

namespace Miners.WebApi
{
    internal static class ErrorHandlerExtensions
    {
        public static ActionResult HandleError(this Exception exception)
            => exception switch
            {
                UnprocessableEntityException exc => 
                    new UnprocessableEntityObjectResult(new { Error = exc.Error, Details = exc.Details }),
                ValidationException exc => 
                    new BadRequestObjectResult(exc.GetDetails()),
                OperationCanceledException exc => 
                    new BadRequestObjectResult(new { Message = "Operation was canceled!" }),

                _ => throw exception
            };

        static ValidationProblemDetails GetDetails(this ValidationException exp) =>
            new ValidationProblemDetails() { Detail = exp.Message };
    }
}
