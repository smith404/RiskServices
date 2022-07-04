
CREATE TABLE [dbo].[HubPage] (
    [Id]       BIGINT         IDENTITY (1000, 1) NOT NULL,
    [Title]    NVARCHAR (255) NOT NULL,
    [Headline] NVARCHAR (255) NOT NULL,
    [Summary]  NVARCHAR (MAX) NOT NULL,
    [Image]    NVARCHAR (255) NOT NULL,
    [Body]     NVARCHAR (MAX) NOT NULL,
    [URL]      NVARCHAR (255) NULL,
    CONSTRAINT [PK_HubPage] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[HubPageSection] (
    [Id]        BIGINT         IDENTITY (1000, 1) NOT NULL,
    [HubPageId] BIGINT         NOT NULL,
    [Title]     NVARCHAR (255) NOT NULL,
    [Headline]  NVARCHAR (255) NOT NULL,
    [Summary]   NVARCHAR (MAX) NOT NULL,
    [Image]     NVARCHAR (255) NOT NULL,
    [Body]      NVARCHAR (MAX) NOT NULL,
    [URL]       NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_HubPageSection] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HubPage] FOREIGN KEY ([HubPageId]) REFERENCES [dbo].[HubPage] ([Id])
);

CREATE TABLE [dbo].[SearchableItems] (
    [Id]         BIGINT         IDENTITY (1000, 1) NOT NULL,
    [Title]      NVARCHAR (50)  NOT NULL,
    [Keywords]   NVARCHAR (MAX) NOT NULL,
    [Summary]    NVARCHAR (90) NOT NULL,
    [TargetType] NVARCHAR (12)  NOT NULL
);

