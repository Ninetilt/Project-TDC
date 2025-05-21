CREATE TABLE[dbo].[Rewarding] (
    [ListId] [bigint] NOT NULL,
    [RewardingMessage] [nvarchar](MAX) NOT NULL,
    CONSTRAINT[PK_Rewarding] PRIMARY KEY CLUSTERED([ListId] ASC),
    CONSTRAINT[FK_ListId_Rewarding] FOREIGN KEY([ListId]) REFERENCES[dbo].[ToDoList]([Id]) ON DELETE CASCADE,
    );
GO

CREATE TABLE[dbo].[OpenRewards] (
    [ListId] [bigint] NOT NULL,
    [Username] [nvarchar](16) NOT NULL,
    CONSTRAINT[PK_OpenRewards] PRIMARY KEY CLUSTERED([ListId] ASC, [Username]),
    CONSTRAINT[FK_ListId_OpenRewards] FOREIGN KEY([ListId]) REFERENCES[dbo].[ToDoList]([Id]) ON DELETE CASCADE,
    CONSTRAINT[FK_Username_OpenRewards] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username]) ON DELETE CASCADE
    );
GO