CREATE TABLE[dbo].[Account]([Username][nvarchar](16) NOT NULL,
                            [Email][nvarchar](100) NOT NULL UNIQUE,
                            [Password][nvarchar](200) NOT NULL,
                            [Description][nvarchar](MAX) NULL,
                            CONSTRAINT[PK_Account] PRIMARY KEY CLUSTERED([Username] ASC)
);
GO

CREATE TABLE[dbo].[Friends] ([Username][nvarchar](16) NOT NULL,
                            [Friend][nvarchar](16) NOT NULL,
                            CONSTRAINT[PK_Friends] PRIMARY KEY CLUSTERED([Username] ASC, [Friend]),
                            CONSTRAINT[FK_Friends_Username] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username]) ON DELETE CASCADE,
                            CONSTRAINT[FK_Friends_Friend] FOREIGN KEY([Friend]) REFERENCES[dbo].[Account]([Username])
);
GO 
    
CREATE TABLE[dbo].[Requests]([Username][nvarchar](16) NOT NULL,
                             [Request][nvarchar](16) NOT NULL,
                             CONSTRAINT[PK_Requests] PRIMARY KEY CLUSTERED([Username] ASC, [Request]),
                             CONSTRAINT
                                 [FK_Requests_Username] FOREIGN KEY([Username]) REFERENCES[dbo].[Account]([Username]) ON DELETE CASCADE,
                             CONSTRAINT[FK_Requests_Request] FOREIGN KEY([Request]) REFERENCES[dbo]
                                 .[Account]([Username]) 
);
GO
