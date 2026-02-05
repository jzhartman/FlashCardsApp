using Dapper;
using Microsoft.Data.SqlClient;

namespace FlashCards.Infrastructure.Initialization;

public class DbInitializer
{
    private readonly string _connectionString;

    public DbInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Initialize()
    {
        CreateDatabaseIfNotExists();
        CreateStackTableIfNotExists();
        CreateCardTableIfNotExists();
        CreateStudySessionTableIfNotExists();
        SeedTablesIfStacksNotExist();
    }

    private void CreateDatabaseIfNotExists()
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);    // Parses connection string into useful properties
        var databaseName = builder.InitialCatalog;      // Sets databaseName to the InitialCatalog property in the connection string

        builder.InitialCatalog = "master";              // Resets to master because it can't create a database from within the database to create

        using var connection = new SqlConnection(builder.ConnectionString);
        connection.Open();

        var sql = $@"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}')
                BEGIN
                    CREATE DATABASE [{databaseName}];
                END";

        connection.Execute(sql);
    }

    private void CreateStackTableIfNotExists()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Stack' AND xType='U')
            CREATE TABLE Stack (
                Id INT NOT NULL PRIMARY KEY IDENTITY,
	            Name NVARCHAR(32) NOT NULL
            );";

        connection.Execute(sql);
    }

    private void CreateCardTableIfNotExists()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Card')
            CREATE TABLE Card (
	            Id INT NOT NULL PRIMARY KEY IDENTITY,
	            StackId INT NOT NULL,
	            FrontText NVARCHAR(250) NOT NULL,
	            BackText NVARCHAR(250) NOT NULL,
	            TimesStudied INT NOT NULL DEFAULT 0,
	            TimesCorrect INT NOT NULL DEFAULT 0,
	            TimesIncorrect INT NOT NULL DEFAULT 0,
                FOREIGN KEY (StackId) REFERENCES Stack(Id)
            );";

        connection.Execute(sql);
    }

    private void CreateStudySessionTableIfNotExists()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='StudySession')
            CREATE TABLE StudySession (
	            Id INT NOT NULL PRIMARY KEY IDENTITY,
	            Time DATETIME NOT NULL,
	            StackId INT NOT NULL,
	            Score  FLOAT NOT NULL,
	            CountStudied INT NOT NULL,
	            CountCorrect INT NOT NULL,
	            CountIncorrect INT NOT NULL,
	            FOREIGN KEY (StackId) REFERENCES Stack(Id)
            );";

        connection.Execute(sql);
    }

    private void SeedTablesIfStacksNotExist()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var seed = @"
            IF NOT EXISTS (SELECT 1 FROM Stack)
            BEGIN
                INSERT INTO Stack (Name)
                VALUES ('Pasta');
                
                DECLARE @PastaStackId INT = SCOPE_IDENTITY();

                INSERT INTO Stack (Name)
                VALUES ('Famous People');

                DECLARE @PeopleStackId INT = SCOPE_IDENTITY();

                INSERT INTO Card (StackId, FrontText, BackText)
                VALUES  (@PastaStackId, 'Elbows', 'Better than knees'),
                        (@PastaStackId, 'Shells', 'Noodles or snail houses'),
                        (@PastaStackId, 'Lasagna', 'Noodle blanket'),
                        (@PastaStackId, 'Rotini', 'Twirly noodle'),
                        (@PastaStackId, 'Ziti', 'Doubles as a straw'),
                        (@PastaStackId, 'Penne', 'Sharp ziti'),
                        (@PastaStackId, 'Spaghetti', 'Thin and wiry'),
                        (@PastaStackId, 'Angel Hair', 'Spaghetti with anorexia'),
                        (@PastaStackId, 'Linguine', 'Spaghetti that ate too much... spaghetti?'),
                        (@PastaStackId, 'Orzo', 'Admit it, this is just rice, people...'),
                        (@PeopleStackId, 'Gandalf', 'Elderly fellow, big gray beard, pointy hat'),
                        (@PeopleStackId, 'Strider', 'This is no mere ranger. He is Aragorn, son of Arathorn. You owe him your allegience'),
                        (@PeopleStackId, 'Samwise', 'The Brave');

                INSERT INTO StudySession (Time, StackId, Score, CountStudied, CountCorrect, CountIncorrect)
                VALUES  ('2026-01-01 11:00:00', @PastaStackId, 100, 10, 10, 0),
                        ('2026-01-02 15:00:00', @PastaStackId, 70, 10, 7, 3),
                        ('2026-01-03 21:00:00', @PastaStackId, 80, 5, 4, 1),
                        ('2026-01-05 11:00:00', @PeopleStackId, 100, 3, 3, 0);
                END";

        connection.Execute(seed);
    }
}
