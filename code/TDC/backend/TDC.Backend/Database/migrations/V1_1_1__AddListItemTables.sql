CREATE TABLE[dbo].[ListItem] (
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [ListId] [bigint] NOT NULL,
    [Description] [nvarchar](MAX) NOT NULL,
    [Effort] [tinyint] NOT NULL DEFAULT(1),
    CONSTRAINT[PK_ListItem] PRIMARY KEY CLUSTERED([Id] ASC),
    CONSTRAINT[FK_ListId_ListItem] FOREIGN KEY([ListId]) REFERENCES[dbo].[ToDoList]([Id]) ON DELETE CASCADE,
    );
GO

CREATE TABLE[dbo].[ItemStatus] (
    [Username] [nvarchar](16) NOT NULL,
    [ItemId] [bigint] NOT NULL,
    [IsDone] [bit] NOT NULL DEFAULT(0),
    CONSTRAINT[PK_ItemStatus] PRIMARY KEY CLUSTERED([ItemId] ASC, [Username]),
    CONSTRAINT[FK_ItemId_ItemStatus] FOREIGN KEY([ItemId]) REFERENCES[dbo].[ListItem]([Id]) ON DELETE CASCADE,
    CONSTRAINT[FK_Username_ItemSTatus] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username])
    );
GO