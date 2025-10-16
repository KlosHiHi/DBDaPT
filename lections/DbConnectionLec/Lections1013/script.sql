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
CREATE TABLE [Category] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK__Category__19093A0B33DBDD24] PRIMARY KEY ([CategoryId])
);

CREATE TABLE [Game] (
    [GameId] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Price] decimal(16,2) NOT NULL,
    [Description] nvarchar(500) NULL,
    [IsDeleted] bit NOT NULL,
    [KeysAmount] smallint NOT NULL DEFAULT CAST(50 AS smallint),
    CONSTRAINT [PK__Game__2AB897FD9C1E03C4] PRIMARY KEY ([GameId]),
    CONSTRAINT [FK_Game_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId])
);

CREATE INDEX [IX_Game_CategoryId] ON [Game] ([CategoryId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251016082528_InitialCreate1', N'9.0.9');

ALTER TABLE [Category] ADD [Description] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251016082844_AddDescriptionToCategory', N'9.0.9');

COMMIT;
GO

