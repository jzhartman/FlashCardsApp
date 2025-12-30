using Dapper;
using FlashCards.Core.Entities;
using FlashCards.Infrastructure.Repositories;
using Moq;
using System.Data;

namespace FlashCards.UnitTests;

public class CardRepositoryTests
{
    [Fact]
    public void Add_ShouldReturnNewId()
    {
        // ToDo: Kill this test since stupid AI could not help write it correctly...
        // Arrange
        var mockConnection = new Mock<IDbConnection>();
        mockConnection
            .Setup(c => c.QuerySingle<int>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .Returns(42);

        var repo = new CardRepository(mockConnection.Object);
        var card = new Card(1, "S in Solid", "Single Responsibility Protocol");

        // Act
        var id = repo.Add(card);

        //
        Assert.Equal(42, id);
    }
}
