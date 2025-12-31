using Dapper;
using FlashCards.Core.Entities;
using FlashCards.Infrastructure.Dapper;
using FlashCards.Infrastructure.Repositories;
using Moq;
using System.Data;

namespace FlashCards.UnitTests;

public class CardRepositoryTests
{
    [Fact]
    public void Add_ShouldReturnNewId()
    {
        // Arrange
        var mockConnection = new Mock<IDbConnection>();
        var mockDapper = new Mock<IDapperWrapper>();

        mockDapper
            .Setup(d => d.QuerySingle<int>(
                mockConnection.Object,
                It.IsAny<string>(),
                It.IsAny<object>()))
            .Returns(42);

        var repo = new CardRepository(mockConnection.Object, mockDapper.Object);
        var card = new Card(1, "S in Solid", "Single Responsibility Protocol");

        // Act
        var id = repo.Add(card);

        //
        Assert.Equal(42, id);
    }
}
