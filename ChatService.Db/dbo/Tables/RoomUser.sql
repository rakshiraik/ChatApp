CREATE TABLE [dbo].[RoomUser] (
    [Id]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [RoomId] BIGINT         NOT NULL,
    [UserId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_RoomUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);

