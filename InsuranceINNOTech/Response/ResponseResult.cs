namespace InsuranceINNOTech;

public class ResponseResult<TEntity>
{
    public bool Status { get; private set; }
    public int ErrorNumber { get; private set; }
    public TEntity Response { get; set; }

    public ResponseResult(TEntity entity)
    {
        Status = true;
        ErrorNumber = 200;
        Response = entity;
    }
}
public class ResponseResult
{
    public bool Status { get; private set; }
    public int ErrorNumber { get; private set; }
    public string Response { get; private set; }

    public ResponseResult(Exception exception)
    {
        Status = false;
        ErrorNumber = 500;
        Response = exception.Message;
    }
}
