namespace FlashCards.Core.Validation;

public class ValidationResult<T>
{
    public bool IsValid { get; }
    public T? Value { get; }
    public List<string> Errors { get; }

    private ValidationResult(bool isValid, T? value, List<string> errors)
    {
        IsValid = isValid;
        Value = value;
        Errors = errors;
    }

    public static ValidationResult<T> Success(T value) => new ValidationResult<T>(true, value, new List<string>());

    public static ValidationResult<T> Failure(params string[] errors) => new ValidationResult<T>(false, default, errors.ToList());

    public static ValidationResult<T> Failure(IEnumerable<string> errors) => new ValidationResult<T>(false, default, errors.ToList());
}
