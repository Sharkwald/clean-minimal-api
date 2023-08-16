namespace Customers.Api.Services;

public class Result<TValue, TError>
{
    private readonly TValue _value;
    private readonly TError _error;

    public bool IsSuccess { get; }

    public Result(TValue value)
    {
        _value = value;
        IsSuccess = true;
    }

    public Result(TError error)
    {
        _error = error;
        IsSuccess = false;
    }

    public TReturn Match<TReturn>(Func<TValue, TReturn> success, Func<TError, TReturn> failure)
    {
        return IsSuccess ? success(_value!) : failure(_error!);
    }

    public void Match(Action<TValue> success, Action<TError> failure)
    {
        if (IsSuccess)
            success(_value!);
        else
            failure(_error!);
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);
}

public enum ErrorResult
{
    NotFound = 1,
    Unauthorized = 2,
    UnexpectedError = 3
}

public enum SuccessResult
{
    Success = 1
}