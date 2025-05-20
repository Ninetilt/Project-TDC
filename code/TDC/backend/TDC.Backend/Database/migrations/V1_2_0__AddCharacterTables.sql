CREATE TABLE[dbo].[Face] (
    [Id] [nvarchar](100) NOT NULL,
    [Image] [nvarchar](MAX) NOT NULL,
    CONSTRAINT[PK_Face] PRIMARY KEY CLUSTERED([Id] ASC)
    );
GO

CREATE TABLE[dbo].[CharacterBody] (
    [Color] [nvarchar](100) NOT NULL,
    [Image] [nvarchar](MAX) NOT NULL,
    CONSTRAINT[PK_CharacterBody] PRIMARY KEY CLUSTERED([Color] ASC)
    );
GO

CREATE TABLE[dbo].[Character] (
    [Username] [nvarchar](16) NOT NULL,
    [FaceId] [nvarchar](100) NULL,
    [Color] [nvarchar](100) NULL,
    [XP] [bigint] NOT NULL DEFAULT(0),
    CONSTRAINT[PK_Character] PRIMARY KEY CLUSTERED([Username] ASC),
    CONSTRAINT[FK_Username_Character] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username]) ON DELETE CASCADE,
    CONSTRAINT[FK_FaceId_Character] FOREIGN KEY([FaceId]) REFERENCES[dbo].[Face]([Id]) ON DELETE SET NULL,
    CONSTRAINT[FK_Color_Character] FOREIGN KEY([Color]) REFERENCES[dbo].[CharacterBody]([Color]) ON DELETE SET NULL
    );
GO

CREATE TABLE[dbo].[Stats] (
    [Username] [nvarchar](16) NOT NULL,
    [Attack] [int] NOT NULL DEFAULT(10),
    [Defense] [int] NOT NULL DEFAULT(10),
    [Speed] [int] NOT NULL DEFAULT(10),
    CONSTRAINT[PK_Stats] PRIMARY KEY CLUSTERED([Username] ASC),
    CONSTRAINT[FK_Username_Stats] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username]) ON DELETE CASCADE
    );
GO