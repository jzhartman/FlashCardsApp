namespace FlashCards.Core.Validation;

public static class Errors
{
    public static readonly Error None = Error.None;

    public static readonly Error StackNameRequired = new("StackNameRequired", "Stack name cannot be blank.");

    public static readonly Error StackNameExists = new("StackNameExists", "A stack with that name already exists.");

    public static readonly Error CardFrontTextExists = new("CardFrontTextExists", "A card with that front text already exists.");

    public static readonly Error CardBackTextExists = new("CardBackTextExists", "A card with that back text already exists.");

    public static readonly Error CardFrontTextRequired = new("CardFrontRequired", "Card front text cannot be blank.");

    public static readonly Error CardBackTextRequired = new("CardBackRequired", "Card back text cannot be blank.");



}
