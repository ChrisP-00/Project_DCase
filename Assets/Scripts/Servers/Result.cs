public class Result
{
    public ResultCodes ResultCodes { get; set; }
    public string ResultMessage { get; set; }
}

public class Result<T>
{
    public ResultCodes ResultCodes { get; set; }
    public string ResultMessage { get; set; }
    public T? Data { get; set; }
}