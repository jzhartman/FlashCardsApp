using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Application.UseCases.Cards;
using FlashCards.Core.Entities;
using Moq;

namespace FlashCards.UnitTests.ApplicationTests;

public class StackUseCaseTests
{
    [Fact]
    public void GetAllStackNamesAndCardCountsHandler_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var cardRepoMock = new Mock<ICardRepository>();
        var stackRepoMock = new Mock<IStackRepository>();

        stackRepoMock.Setup(s => s.GetIdByName("CoolStack")).Returns(1);            // Mocks returning an Id of 1 for a stack name of "CoolStack"

        cardRepoMock.Setup(c => c.Add(It.IsAny<Card>())).Callback<Card>(d => d.Id = 13).Returns(13); // Sets any card to return an Id of 13 (arbitrary number)

        var addCardUseCase = new AddCardHandler(cardRepoMock.Object, stackRepoMock.Object);
        var command = new AddCardCommand("CoolStack", "Gandalf", "Elderly chap, big grey beard, pointy hat");

        // Act
        var result = addCardUseCase.Handle(command);
        // Result<CardResponse>.Success(new(id, cardCommand.FrontText, cardCommand.BackText, 0, 0, 0));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(13, result.Value!.Id);
        Assert.Equal("Gandalf", result.Value!.FrontText);
        Assert.Equal("Elderly chap, big grey beard, pointy hat", result.Value!.BackText);
        Assert.Equal(0, result.Value!.TimesStudied);
        Assert.Equal(0, result.Value!.TimesCorrect);
        Assert.Equal(0, result.Value!.TimesIncorrect);
    }
}
