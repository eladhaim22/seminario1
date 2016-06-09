CREATE TABLE [dbo].[Cheque] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (256) NULL,
	[Amount] INT NOT NULL
    CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

