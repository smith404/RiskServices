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
    [Id]          BIGINT         IDENTITY (1000, 1) NOT NULL,
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
CREATE TABLE [dbo].[Profile] (
    [Id]          BIGINT         IDENTITY (1000, 1) NOT NULL,
    [UserName]    NVARCHAR (255) NOT NULL,
    [ProfileName] NVARCHAR (255) NOT NULL,
    [ChannelId]   BIGINT         NOT NULL,
    [Type]        NVARCHAR (50)  NOT NULL,
    [Icon]        NVARCHAR (50)  NOT NULL,
    [Message]     NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_UserName]
    ON [dbo].[Profile]([UserName] ASC);
GO
CREATE TABLE [dbo].[Channel] (
    [Id]          BIGINT         IDENTITY (1000, 1) NOT NULL,
    [ChannelName] NVARCHAR (255) NOT NULL,
    [Type]        NVARCHAR (50)  NOT NULL,
    [Icon]        NVARCHAR (50)  NOT NULL,
    [Message]     NVARCHAR (255) NOT NULL,
    [Private]     BIT            NOT NULL,
    [Owner]       BIGINT         NOT NULL,
    CONSTRAINT [PK_Channel] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_ChannelName]
    ON [dbo].[Channel]([ChannelName] ASC);
GO
CREATE TABLE [dbo].[ProfileChannel] (
    [ProfileId] BIGINT   NOT NULL,
    [ChannelId] BIGINT   NOT NULL,
    [Status]    CHAR (1) NOT NULL,
    CONSTRAINT [PK_ProfileChannel] PRIMARY KEY CLUSTERED ([ProfileId] ASC, [ChannelId] ASC),
);
GO
CREATE TABLE [dbo].[NotificationChannel] (
    [NotificationId] BIGINT   NOT NULL,
    [ChannelId]      BIGINT   NOT NULL,
    [Status]         CHAR (1) NOT NULL,
    CONSTRAINT [PK_NotificationChannel] PRIMARY KEY CLUSTERED ([NotificationId] ASC, [ChannelId] ASC),
);
GO
CREATE VIEW [dbo].[V_ProcessedNotifications]
AS
SELECT n.*, p.UserName, c.ChannelName, s.Status AS ProcessedStatus, s.ProcessedOn
FROM dbo.Notifications n, dbo.Channel c, dbo.Profile p, dbo.ProfileChannel j, NotificationStatus s
WHERE n.ChannelId = c.Id
AND j.ProfileId = p.Id
AND j.ChannelId = c.Id
AND s.NotificationId = n.Id
AND s.ProfileId = p.Id
GO
CREATE VIEW [dbo].[V_UnprocessedNotifications]
AS
SELECT n.*, p.UserName, c.ChannelName
FROM dbo.Notifications n, dbo.Channel c, dbo.Profile p, dbo.ProfileChannel j
WHERE n.ChannelId = c.Id
AND j.ProfileId = p.Id
AND j.ChannelId = c.Id
AND NOT EXISTS (SELECT 1 FROM NotificationStatus s WHERE s.NotificationId = n.Id AND s.ProfileId = p.Id)
GO
CREATE PROCEDURE [dbo].[GetNotifications]	@UserName nvarchar(255)
AS
BEGIN
    SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM dbo.Profile WHERE UserName = @UserName)
	BEGIN
		SELECT Id, Type, Icon, Message, URL, Sender, ChannelId, Status, CreatedOn, UserName, ChannelName
		FROM dbo.V_UnprocessedNotifications
		WHERE UserName = @UserName
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION
		DECLARE @ProfileId BIGINT;
		DECLARE @ChannelId BIGINT;
		INSERT INTO dbo.Profile(UserName,DisplayName,Type,Icon,Message,ChannelId)
		VALUES(@UserName,@UserName,'User','None','Auto Generated Account',0)
		SET @ProfileId = @@IDENTITY
		INSERT INTO dbo.Channel(ChannelName,Type,Icon,Message,Private,Owner)
		VALUES(@UserName,'Profile','None','Auto Generated Channel',1,@ProfileId)
		SET @ChannelId = @@IDENTITY
		UPDATE dbo.Profile SET ChannelId = @ChannelId
		WHERE Id = @ProfileId
		INSERT INTO dbo.ProfileChannel(ProfileId,ChannelId,Status)
		VALUES(@ProfileId,@ChannelId,'A')
		COMMIT TRANSACTION
		SELECT Id, Type, Icon, Message, URL, Sender, ChannelId, Status, CreatedOn, UserName, ChannelName
		FROM dbo.V_UnprocessedNotifications
		WHERE UserName = @UserName
	END
END
GO
CREATE PROCEDURE [dbo].[SendNotification]	@Sender nvarchar(255), @ChannelName nvarchar(255), @Type  nvarchar(50), @Icon nvarchar(50), @Message nvarchar(MAX), @URL nvarchar(MAX)
AS
BEGIN
    SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM dbo.Channel WHERE ChannelName = @ChannelName)
	BEGIN
		DECLARE @ChannelId BIGINT;
		SELECT @ChannelId = Id
		FROM dbo.Channel
		WHERE ChannelName = @ChannelName
		INSERT INTO dbo.Notifications(Type,Icon,Message,URL,Sender,ChannelId,Status,CreatedOn)
		VALUES(@Type,@Icon,@Message,@URL,@Sender,@ChannelId,'N',CURRENT_TIMESTAMP)
	END
END
GO
CREATE TABLE [dbo].[AttributeMetadata] (
    [id]                BIGINT        NOT NULL,
    [id_entity]         BIGINT        NOT NULL,
    [name]              VARCHAR (50)  NOT NULL,
    [description]       VARCHAR (250) NOT NULL,
    [read_only]        BIT           NOT NULL,
    [depricated]        BIT           NOT NULL,
    [display_value]        BIT           NOT NULL,
    [filterable]        BIT           NOT NULL,
    [searchable]        BIT           NOT NULL,
    [required]        BIT           NOT NULL,
    [hiddern]        BIT           NOT NULL,
    [transient]        BIT           NOT NULL,
    [unique]        BIT           NOT NULL,
    [custom_object]        BIT           NOT NULL,
    [custom_object_type]              VARCHAR (50)  NOT NULL,
    [read_permission] VARCHAR (60) NOT NULL,
    [write_permission] VARCHAR (60) NOT NULL,
    [id_attribute_type] BIGINT        NOT NULL,
    CONSTRAINT [PK_AttributeMetadata] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO
CREATE TABLE [dbo].[AttributeType] (
    [id]        BIGINT       NOT NULL,
    [name]      VARCHAR (50) NOT NULL,
    [type_name] VARCHAR (50) NOT NULL,
    [read_permission] VARCHAR (60) NOT NULL,
    [admin_permission] VARCHAR (60) NOT NULL,
    [update_permission] VARCHAR (60) NOT NULL,
    [create_permission] VARCHAR (60) NOT NULL,
    [delete_permission] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_AttributeType] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO
CREATE TABLE [dbo].[EntityMetadata] (
    [id]          BIGINT        NOT NULL,
    [name]        VARCHAR (50)  NOT NULL,
    [description] VARCHAR (250) NOT NULL,
    [depricated]  BIT           NOT NULL,
    [table_name]  VARCHAR (50)  NOT NULL,
    [id_key]      BIGINT        NOT NULL,
    CONSTRAINT [PK_EntityMetadata] PRIMARY KEY CLUSTERED ([id] ASC)
);
