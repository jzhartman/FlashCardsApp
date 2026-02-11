using FlashCards.Application.Enums;

namespace FlashCards.Application.Cards.ValidateCardTextBySide;

public record ValidateCardTextBySideCommand(int StackId, string Text, CardSide Side);
