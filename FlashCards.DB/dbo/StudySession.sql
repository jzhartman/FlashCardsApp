CREATE TABLE [dbo].[StudySession]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Time] DATETIME NOT NULL,
	[StackId] INT NOT NULL,
	[Score]  FLOAT NOT NULL,
	[CountStudied] INT NOT NULL,
	[CountCorrect] INT NOT NULL,
	[CountIncorrect] INT NOT NULL,
	CONSTRAINT [FK_StudySession_ToTStack] FOREIGN KEY ([StackId]) REFERENCES [Stack]([Id])
)
