namespace FlashCards.Application.Stacks.GetAll;

public record StackResponseWithCounts(int Id, string Name, int CardCount, int SessionCount);