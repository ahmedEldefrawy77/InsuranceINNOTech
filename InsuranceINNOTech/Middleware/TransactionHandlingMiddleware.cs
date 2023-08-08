using System.Data;

namespace InsuranceINNOTech;

    public class TransactionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<TransactionHandlingMiddleware> _logger;
        private readonly ApplicationDbContext _context;
       
        
        public TransactionHandlingMiddleware(ILogger<TransactionHandlingMiddleware> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            IDbContextTransaction? Transaction = null;
            try
            {
               Transaction = _context.Database.BeginTransaction();
                await next(context);
               Transaction.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
               Transaction.Rollback();
                throw;
            }
            
        }
    }

