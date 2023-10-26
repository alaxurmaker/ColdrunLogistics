using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ColdrunLogistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private Dictionary<ErrorType, int> statusCodesByErrorType = new Dictionary<ErrorType, int>
        {
            { ErrorType.NotFound, StatusCodes.Status404NotFound },
            { ErrorType.Validation, StatusCodes.Status400BadRequest },
            { ErrorType.Conflict, StatusCodes.Status409Conflict }
        };

        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

                foreach (var error in errors)
                {
                    modelStateDictionary.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem(modelStateDictionary);
            }

            if (errors.Any(e => e.Type == ErrorType.Unexpected))
            {
                return Problem();
            }

            Error firstError = errors[0];
            int statusCode = statusCodesByErrorType.TryGetValue(firstError.Type, out statusCode)
                ? statusCode
                : StatusCodes.Status500InternalServerError;

            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}
