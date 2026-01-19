namespace FlashCards.Core.Validation;

public record Result<T>
{
    public bool IsValid { get; }
    public T? Value { get; }
    public List<string> Errors { get; }

    private Result(bool isValid, T? value, List<string> errors)
    {
        IsValid = isValid;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, new List<string>());

    public static Result<T> Failure(params string[] errors) => new Result<T>(false, default, errors.ToList());

    public static Result<T> Failure(IEnumerable<string> errors) => new Result<T>(false, default, errors.ToList());
}
