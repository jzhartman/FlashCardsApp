using Dapper;
using FlashCards.Core.Entities;
using FlashCards.Infrastructure.Dapper;
using FlashCards.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;

namespace FlashCards.IntegrationTests.InfrastructureTests;

public class CardRepositoryIntegrationTests
{
    private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=devFlashCardsAppDb;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    [Fact]
    [Trait("Category", "Integration")]
    public void Add_And_GetById_ShouldPersistCard()
    {
        // Arrange
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        //var insertStackSql = @"insert into Stack (Name) Values (@Name); Select cast(Scope_Identity() as int);";
        //var stackId = connection.QuerySingle<int>(insertStackSql, new { Name = "Design Patterns" });
        var stackId = 1;

        var dapper = new DapperWrapper();
        var repo = new CardRepository(connection, dapper);
        var card = new Card(stackId, "S in Solid", "Single Responsibility Protocol");

        // Act
        var id = repo.Add(card);
        var retrieved = repo.GetById(id);

        // Assert
        Assert.Equal(stackId, retrieved.StackId);
        Assert.Equal("S in Solid", retrieved.FrontText);
        Assert.Equal("Single Responsibility Protocol", retrieved.BackText);

    }

    [Fact]
    [Trait("Category", "Integration")]
    public void GetCardCountByStackName_ShouldReturnCount()
    {
        // Arrange
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var deleteDuplicateSql = @" DELETE FROM Card
                                    WHERE Id NOT IN (
                                    SELECT Min(Id)
                                    FROM Card
                                    GROUP BY FrontText, BackText);";

        connection.Execute(deleteDuplicateSql);

        var stackId = 1;

        var dapper = new DapperWrapper();
        var repo = new CardRepository(connection, dapper);

        // Act
        var count = repo.GetCardCountByStackName("Design Patterns");

        // Assert
        Assert.Equal(count, 1);
    }
}
