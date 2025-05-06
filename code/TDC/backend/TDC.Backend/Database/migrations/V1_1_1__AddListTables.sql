CREATE TABLE[dbo].[ToDoList] (
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [IsCollaborative] [bit] NOT NULL,
    [IsFinished] [bit] NOT NULL,
    CONSTRAINT[PK_ToDoList] PRIMARY KEY CLUSTERED([Id] ASC)
    );
GO
    
CREATE TABLE[dbo].[ListMembers] (
    [Username] [nvarchar](16) NOT NULL,
    [ListId] [bigint] NOT NULL,
    [IsCreator] [bit] NOT NULL,
    CONSTRAINT[PK_ListMembers_ListMember] PRIMARY KEY CLUSTERED([Username] ASC, [ListId]),
    CONSTRAINT[FK_Username_ListMember] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username]),
    CONSTRAINT[FK_ListId_ListMembers] FOREIGN KEY([ListId]) REFERENCES[dbo].[ToDoList]([Id]) ON DELETE CASCADE
    );
GO

CREATE TABLE[dbo].[ListInvitations] (
    [ForUser] [nvarchar](16) NOT NULL,
    [FromUser] [nvarchar](16) NOT NULL,
    [ListId] [bigint] NOT NULL,
    CONSTRAINT[PK_ListInvitations] PRIMARY KEY CLUSTERED([ForUser] ASC, [FromUser], [ListId]),
    CONSTRAINT[FK_ForUser_ListInvitations] FOREIGN KEY([ForUser]) REFERENCES[dbo].[Account]([Username]),
    CONSTRAINT[FK_FromUser_ListInvitations] FOREIGN KEY([FromUser]) REFERENCES[dbo].[Account]([Username]),
    CONSTRAINT[FK_ListId_ListInvitations] FOREIGN KEY([ListId]) REFERENCES[dbo].[ToDoList]([Id]) ON DELETE CASCADE
    );
GO