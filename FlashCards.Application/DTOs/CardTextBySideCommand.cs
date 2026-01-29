using FlashCards.Application.Enums;

namespace FlashCards.Application.DTOs;

public record CardTextBySideCommand(string StackName, string Text, CardSide Side);
