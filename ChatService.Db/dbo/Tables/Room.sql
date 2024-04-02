CREATE TABLE [dbo].[Room] (
    [Id]                BIGINT       IDENTITY (1, 1) NOT NULL,
    [RoomName]          VARCHAR (50) NOT NULL,
    [NoOfPeopleAllowed] INT          NOT NULL,
    [IsDeleted]         BIT          NOT NULL,
    [CreatedOn]         DATETIME     NOT NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([Id] ASC)
);

