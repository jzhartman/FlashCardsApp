namespace FlashCards.Core.Validation;

public record Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public List<Error> Errors { get; }

    private Result(bool isSuccess, T? value, List<Error> errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, new List<Error>());
    public static Result<T> Failure(params Error[] errors) => new Result<T>(false, default, errors.ToList());
}
