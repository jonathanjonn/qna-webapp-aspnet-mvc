CREATE TABLE [dbo].[tes] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [question] NVARCHAR (MAX) NULL,
    [answer]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_tes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

