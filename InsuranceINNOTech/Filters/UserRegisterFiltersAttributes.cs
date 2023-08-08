
namespace InsuranceINNOTech;

    public class UserRegisterFiltersAttributes<User> :Attribute, IAsyncActionFilter
    {
    
        private static IValidator<User> _validator;
        private static ILogger<UserRegisterFiltersAttributes<User>> _logger;

       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var httpContext = context.HttpContext;
           var model = await httpContext.Request.ReadFromJsonAsync<User>();
           try
           {
               await _validator.ValidateAsync(model); 
               await next();
           }
           catch (ValidationException ex)
           {
               _logger.LogError(ex.Message);
               throw;
           }
        }
    }

