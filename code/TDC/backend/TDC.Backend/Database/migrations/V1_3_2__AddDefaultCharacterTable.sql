CREATE TABLE[dbo].[DefaultCharacter] (
    [Id] [nvarchar](100) NOT NULL,
    [Image] [nvarchar](MAX) NOT NULL,
    CONSTRAINT[PK_DefaultCharacter] PRIMARY KEY CLUSTERED([Id] ASC)
    );
GO

INSERT INTO [dbo].[DefaultCharacter] VALUES
    (N'default', N'https://raw.githubusercontent.com/Ninetilt/Project-TDC/refs/heads/dev/docs/resources/to_dewey.svg');
GO