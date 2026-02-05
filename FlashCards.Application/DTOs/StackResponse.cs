using FlashCards.Application.Cards;

namespace FlashCards.Application.DTOs;

public record StackResponse(string Name, List<CardResponse> Cards);
