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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708131145_Initial')
BEGIN
    CREATE TABLE [Employee] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Mobile] int NOT NULL,
        CONSTRAINT [PK_Employee] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708131145_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220708131145_Initial', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220708142023_IdentityDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220708142023_IdentityDB', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220709021726_CustomIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220709021726_CustomIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220709021726_CustomIdentity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220709021726_CustomIdentity', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Address] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [City] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Latitude] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Longitude] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [MiddleName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Mobile] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [OTP] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [State] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Street] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [UserType] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Zip_Pincode] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220714073614_AddCustomColumnIdentity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220714073614_AddCustomColumnIdentity', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719061904_FieldChangeApplicationUser')
BEGIN
    EXEC sp_rename N'[AspNetUsers].[Mobile]', N'UserEmail', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719061904_FieldChangeApplicationUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220719061904_FieldChangeApplicationUser', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Zip_Pincode');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Zip_Pincode] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserType');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserType] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserEmail');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserEmail] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Street');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Street] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'State');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [State] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'MiddleName');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [MiddleName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Longitude');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Longitude] decimal(18,2) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Latitude');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Latitude] decimal(18,2) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LastName');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [LastName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [FirstName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'City');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [City] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Address');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Address] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064458_AddColumnNullable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220719064458_AddColumnNullable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064921_RemoveUserEmail')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserEmail');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [UserEmail];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220719064921_RemoveUserEmail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220719064921_RemoveUserEmail', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720132840_Initial Azure')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220720132840_Initial Azure', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220727114020_removeOTPField')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'OTP');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [OTP];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220727114020_removeOTPField')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220727114020_removeOTPField', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220727123436_InitialAzureDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220727123436_InitialAzureDB', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220727124347_InitialAzureDB1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220727124347_InitialAzureDB1', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804085907_editusertable')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [FirstName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804085907_editusertable')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LastName');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [LastName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804085907_editusertable')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'MiddleName');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [MiddleName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804085907_editusertable')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserType');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserType] int NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT 0 FOR [UserType];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804085907_editusertable')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804085907_editusertable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220804085907_editusertable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804091350_usertypefieldchange')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserType');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserType] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804091350_usertypefieldchange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220804091350_usertypefieldchange', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804095541_AddCountryField')
BEGIN
    EXEC sp_rename N'[AspNetUsers].[Zip_Pincode]', N'Zipcode', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804095541_AddCountryField')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Country] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804095541_AddCountryField')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Landmark] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804095541_AddCountryField')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [StoreName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804095541_AddCountryField')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220804095541_AddCountryField', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804100026_RemoveStreet')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Street');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Street];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220804100026_RemoveStreet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220804100026_RemoveStreet', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220808093429_AddVendorAccountRegistration')
BEGIN
    CREATE TABLE [VendorAccountRegistration] (
        [Id] uniqueidentifier NOT NULL,
        [VendorId] uniqueidentifier NOT NULL,
        [AccountNumber] nvarchar(max) NOT NULL,
        [BankName] nvarchar(max) NOT NULL,
        [IFSCCode] nvarchar(max) NOT NULL,
        [AccountHolderName] nvarchar(max) NOT NULL,
        [BranchName] nvarchar(max) NOT NULL,
        [AccountType] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedBy] uniqueidentifier NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_VendorAccountRegistration] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220808093429_AddVendorAccountRegistration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220808093429_AddVendorAccountRegistration', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220809082819_AddWallet')
BEGIN
    CREATE TABLE [PassbookSummary] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [TxnId] bigint NOT NULL,
        [TxnDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Amount] float NOT NULL,
        [Mode] nvarchar(1) NOT NULL,
        CONSTRAINT [PK_PassbookSummary] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220809082819_AddWallet')
BEGIN
    CREATE TABLE [PersonWalletRegistration] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [PanNumber] nvarchar(max) NOT NULL,
        [IsWalletActivate] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedBy] uniqueidentifier NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_PersonWalletRegistration] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220809082819_AddWallet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220809082819_AddWallet', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822121509_AddAmountWallet')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PassbookSummary]') AND [c].[name] = N'Amount');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [PassbookSummary] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [PassbookSummary] ALTER COLUMN [Amount] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822121509_AddAmountWallet')
BEGIN
    ALTER TABLE [PassbookSummary] ADD [Comment] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822121509_AddAmountWallet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220822121509_AddAmountWallet', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Address');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Address];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'City');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [City];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Country');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Country];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Landmark');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Landmark];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Latitude');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Latitude];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Longitude');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Longitude];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'State');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [State];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'StoreName');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [StoreName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Zipcode');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Zipcode];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    CREATE TABLE [VendorStore] (
        [Id] uniqueidentifier NOT NULL,
        [VendorId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Landmark] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [District] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [Zipcode] nvarchar(max) NULL,
        [Latitude] nvarchar(max) NULL,
        [Longitude] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedBy] uniqueidentifier NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_VendorStore] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220824090322_addVendorStore')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220824090322_addVendorStore', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220907082241_AddRemainingBalcolumn')
BEGIN
    ALTER TABLE [PassbookSummary] ADD [RemainingBal] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220907082241_AddRemainingBalcolumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220907082241_AddRemainingBalcolumn', N'6.0.6');
END;
GO

COMMIT;
GO

