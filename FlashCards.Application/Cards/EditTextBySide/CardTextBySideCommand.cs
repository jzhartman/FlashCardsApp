using FlashCards.Application.Enums;

namespace FlashCards.Application.Cards.EditTextBySide;

public record CardTextBySideCommand(string StackName, string Text, CardSide Side);
