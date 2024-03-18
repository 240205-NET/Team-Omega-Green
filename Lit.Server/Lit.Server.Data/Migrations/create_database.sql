IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);
GO

CREATE TABLE [Histories] (
    [HistoryId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Histories] PRIMARY KEY ([HistoryId])
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Location] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [Cart] (
    [CartId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY ([CartId]),
    CONSTRAINT [FK_Cart_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Books] (
    [Isbn] nvarchar(450) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [CategoryId] int NOT NULL,
    [CartId] int NULL,
    [HistoryId] int NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Isbn]),
    CONSTRAINT [FK_Books_Cart_CartId] FOREIGN KEY ([CartId]) REFERENCES [Cart] ([CartId]),
    CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Books_Histories_HistoryId] FOREIGN KEY ([HistoryId]) REFERENCES [Histories] ([HistoryId])
);
GO

CREATE TABLE [Reviews] (
    [ReviewId] int NOT NULL IDENTITY,
    [Tating] int NOT NULL,
    [Body] nvarchar(max) NOT NULL,
    [UserId] int NOT NULL,
    [BookId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([ReviewId]),
    CONSTRAINT [FK_Reviews_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Isbn]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Books_CartId] ON [Books] ([CartId]);
GO

CREATE INDEX [IX_Books_CategoryId] ON [Books] ([CategoryId]);
GO

CREATE INDEX [IX_Books_HistoryId] ON [Books] ([HistoryId]);
GO

CREATE UNIQUE INDEX [IX_Cart_UserId] ON [Cart] ([UserId]);
GO

CREATE INDEX [IX_Reviews_BookId] ON [Reviews] ([BookId]);
GO

CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240313144756_CreateDatabase', N'7.0.16');
GO

COMMIT;
GO