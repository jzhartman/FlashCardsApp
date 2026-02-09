namespace FlashCards.Core.Validation;

public static class Errors
{
    public static readonly Error None = Error.None;

    public static readonly Error StackNameRequired = new("StackNameRequired", "Stack name cannot be blank.");

    public static readonly Error StackNameExists = new("StackNameExists", "A stack with that name already exists.");

    public static readonly Error NoStacksExist = new("NoStacksExist", "No stacks exist.");

    public static readonly Error NoCardsExist = new("NoCardsExist", "No cards exist in the current stack.");

    public static readonly Error CardFrontTextExists = new("CardFrontTextExists", "A card with that front text already exists.");

    public static readonly Error CardBackTextExists = new("CardBackTextExists", "A card with that back text already exists.");

    public static readonly Error CardFrontTextRequired = new("CardFrontRequired", "Card front text cannot be blank.");

    public static readonly Error CardBackTextRequired = new("CardBackRequired", "Card back text cannot be blank.");

    public static readonly Error StackEmpty = new("StackEmpty", "The selected stack contains no cards!");

    public static readonly Error InvalidId = new("InvalidId", "Id value is invalid!");

    public static readonly Error StackNameTooLong = new("StackNameTooLong", "The stack name cannot exceed 25 characters!");

    public static readonly Error NoStudySessions = new("NoStudySession", "No study sessions exist for the current selection!");

    public static readonly Error CardTextLengthTooLong = new("CardTextLengthTooLong", "Card text cannot exceed 250 characters!");
}
