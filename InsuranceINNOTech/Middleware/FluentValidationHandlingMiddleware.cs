//namespace InsuranceINNOTech;

//    public class FluentValidationHandlingMiddleware<T> : IMiddleware where T : class
//    {
//        private readonly IValidator<T> _validator;
//        private readonly ILogger<FluentValidationHandlingMiddleware<T>> _logger;
//        public FluentValidationHandlingMiddleware(IValidator<T> validator, ILogger<FluentValidationHandlingMiddleware<T>> Logger)
//        {
//            _validator = validator;
//            _logger = Logger;   
//        }


//        public  async Task InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            var model = await context.Request.ReadFromJsonAsync<T>();
//            try
//            {
//                await _validator.ValidateAsync(model);
//                await next(context);
//            }
//            catch (ValidationException ex)
//            {
//                _logger.LogError(ex.Message);
//                context.Response.StatusCode = StatusCodes.Status400BadRequest;
//                throw;
//            }
//        }
//    }
