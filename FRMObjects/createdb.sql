CREATE TABLE [dbo].[HubPage] (
    [Id]       BIGINT         IDENTITY (1000, 1) NOT NULL,
    [GUID]     CHAR (36)      DEFAULT (newid()) NOT NULL,
    [Title]    NVARCHAR (255) NOT NULL,
    [Headline] NVARCHAR (255) NOT NULL,
    [Summary]  NVARCHAR (MAX) NOT NULL,
    [Image]    NVARCHAR (255) NOT NULL,
    [Body]     NVARCHAR (MAX) NOT NULL,
    [URL]      NVARCHAR (255) NOT NULL,
    [Owner]    NVARCHAR (255) NOT NULL,
    [Status]   CHAR (1)       NOT NULL,
    CONSTRAINT [PK_HubPage] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_1]
    ON [dbo].[HubPage]([GUID] ASC);
GO
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
    CONSTRAINT [FK_HubPageSection_HubPage] FOREIGN KEY ([HubPageId]) REFERENCES [dbo].[HubPage] ([Id])
);
GO
CREATE TABLE [dbo].[SearchItem] (
    [Id]         BIGINT         IDENTITY (1000, 1) NOT NULL,
    [Domain]     NVARCHAR (10)  NOT NULL,
    [Title]      NVARCHAR (50)  NOT NULL,
    [Keywords]   NVARCHAR (MAX) NOT NULL,
    [Summary]    NVARCHAR (90)  NOT NULL,
    [TargetType] NVARCHAR (12)  NOT NULL,
    [Target]     NVARCHAR (255) NOT NULL,
    [Image]      NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_SearchItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE NONCLUSTERED INDEX [Index_1]
    ON [dbo].[SearchItem]([Domain] ASC);
GO
CREATE TABLE [dbo].[ToolDefinition] (
    [Id]          BIGINT         IDENTITY (1000, 1) NOT NULL,
    [ToolName]    NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (255) NOT NULL,
    [URL]         NVARCHAR (255) NULL,
    [Version]     TINYINT        NOT NULL,
    [Status]      CHAR (1)       DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ToolDefinition] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[ToolStep] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (255) NOT NULL,
    [StepType]    CHAR (3)       NOT NULL,
    [Definition]  NVARCHAR (MAX) NOT NULL,
    [TestObject]  NVARCHAR (MAX) NULL,
    [InMemory]    BIT            NOT NULL,
    [HasInput]    BIT            NOT NULL,
    [HasOutput]   BIT            NOT NULL,
    [DatasetName] NVARCHAR (50)  NOT NULL,
    [Message]     NVARCHAR (255) NOT NULL,
    [Format]      CHAR (10)      NOT NULL,
    [Active]      BIT            NOT NULL,
    CONSTRAINT [PK_ToolStep] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[ToolExecutionLog] (
    [Id]                 BIGINT         IDENTITY (1000, 1) NOT NULL,
    [GUID]               CHAR (36)      DEFAULT (newid()) NOT NULL,
    [ToolDefinitionId]   BIGINT         NOT NULL,
    [Requestor]          NVARCHAR (255) DEFAULT (original_login()) NOT NULL,
    [RunConfiguration]   NVARCHAR (MAX) NOT NULL,
    [Persist]            BIT            NOT NULL,
    [ExitOnFail]         BIT            NOT NULL,
    [Status]             CHAR (1)       NOT NULL,
    [StepNumber]         TINYINT        NOT NULL,
    [LastDataUrl]        NVARCHAR (MAX) NOT NULL,
    [RequestedTimestamp] DATETIME       NOT NULL,
    [RunStartTimestamp]  DATETIME       NULL,
    [RunEndTimestamp]    DATETIME       NULL,
    CONSTRAINT [PK_ToolExecutionLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ToolExecutionLog_ToolDefinition] FOREIGN KEY ([ToolDefinitionId]) REFERENCES [dbo].[ToolDefinition] ([Id])
);
GO
CREATE NONCLUSTERED INDEX [Index_1]
    ON [dbo].[ToolExecutionLog]([GUID] ASC);
GO
CREATE TABLE [dbo].[ToolStepConfig] (
    [ToolDefinitionId] BIGINT  NOT NULL,
    [ToolStepId]       BIGINT  NOT NULL,
    [StepOrder]        TINYINT NOT NULL,
    [Active]           BIT     NOT NULL,
    CONSTRAINT [PK_ToolStepConfig] PRIMARY KEY CLUSTERED ([ToolDefinitionId] ASC, [ToolStepId] ASC),
    CONSTRAINT [FK_ToolStepConfig_ToolDefinition] FOREIGN KEY ([ToolDefinitionId]) REFERENCES [dbo].[ToolDefinition] ([Id]),
    CONSTRAINT [FK_ToolStepConfig_ToolStep] FOREIGN KEY ([ToolStepId]) REFERENCES [dbo].[ToolStep] ([Id])
);
GO
CREATE TABLE [dbo].[Notifications] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Type]        NVARCHAR (50)  NOT NULL,
    [Icon]        NVARCHAR (50)  NOT NULL,
    [Message]     NVARCHAR (255) NOT NULL,
    [URL]         NVARCHAR (255) NOT NULL,
    [Owner]       NVARCHAR (255) NOT NULL,
    [Sender]      NVARCHAR (255) NOT NULL,
    [Processed]   BIT            NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    [ProcessedOn] DATETIME       NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO