using FlashCards.Application.Enums;

namespace FlashCards.Application.Cards.EditTextBySide;

public record EditCardTextBySideCommand(string Text, CardSide Side);