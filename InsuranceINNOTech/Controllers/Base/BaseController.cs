

namespace InsuranceINNOTech;

    public class BaseController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IBaseUnitOfWork<TEntity> _unitOfWork;
        public BaseController(IBaseUnitOfWork<TEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    public virtual async Task<IActionResult> Read(Guid id)
    {
        TEntity entity = await _unitOfWork.Read(id);

        ResponseResult<TEntity> response = new(entity);
       

        

        return Ok(response);
    }

    public virtual async Task<IActionResult> Read()
        {
            IEnumerable<TEntity> entities = await _unitOfWork.ReadAll();

            ResponseResult<IEnumerable<TEntity>> response = new(entities);

            return Ok(response);
        }

        public virtual async Task<IActionResult> Post(TEntity entity)
        {
            await _unitOfWork.Create(entity);

            ResponseResult<string> response = new($"{typeof(TEntity).Name} Created");

            return Ok(response);
        }

        public virtual async Task<IActionResult> Put(TEntity entity)
        {
            await _unitOfWork.UpdateAsync(entity);

            ResponseResult<string> response = new($"{typeof(TEntity).Name} Has been Updated");

            return Ok(response);
        }
   
        protected virtual async Task<IActionResult> Delete(TEntity entity)
        {
        await _unitOfWork.Delete(entity);

        ResponseResult<string> response = new($"{typeof(TEntity).Name} Deleted");

        return Ok(response);
        }
        
        protected void SetCookie(string name, string value, DateTime ExpiresTime) =>
        Response.Cookies.Append(name, value, new CookieOptions()
        {
            Expires = ExpiresTime,
            HttpOnly = true
        });
    protected Guid GetUserId() => new(User.FindFirst("Id")?.Value);
}


