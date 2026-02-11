namespace FlashCards.Application.Stacks.GetAll;

public record StackNamesWithCountsResponse(int Id, string Name, int CardCount, int SessionCount);