CREATE TABLE [dbo].[Messages] (
    [Id]              BIGINT       NOT NULL,
    [Message]         VARCHAR (50) NOT NULL,
    [RoomUserId]      BIGINT       NOT NULL,
    [ParentMessageId] BIGINT       NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

