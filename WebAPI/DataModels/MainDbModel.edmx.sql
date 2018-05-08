
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/07/2018 00:36:25
-- Generated from EDMX file: C:\Users\edvala\OneDrive\KTUnited\WebAPI\DataModels\MainDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WebAPI-KentSoft_db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DormitoryResident]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidentSet] DROP CONSTRAINT [FK_DormitoryResident];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidentVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_ResidentVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_GuardDormitory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GuardSet] DROP CONSTRAINT [FK_GuardDormitory];
GO
IF OBJECT_ID(N'[dbo].[FK_GuardVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_GuardVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_AdministratorDormitory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AdministratorSet] DROP CONSTRAINT [FK_AdministratorDormitory];
GO
IF OBJECT_ID(N'[dbo].[FK_DormitoryVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_DormitoryVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomDormitory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomSet] DROP CONSTRAINT [FK_RoomDormitory];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomResident]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidentSet] DROP CONSTRAINT [FK_RoomResident];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitGuest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_VisitGuest];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ResidentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResidentSet];
GO
IF OBJECT_ID(N'[dbo].[GuestSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GuestSet];
GO
IF OBJECT_ID(N'[dbo].[DormitorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DormitorySet];
GO
IF OBJECT_ID(N'[dbo].[VisitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VisitSet];
GO
IF OBJECT_ID(N'[dbo].[GuardSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GuardSet];
GO
IF OBJECT_ID(N'[dbo].[AdministratorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdministratorSet];
GO
IF OBJECT_ID(N'[dbo].[RoomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ResidentSet'
CREATE TABLE [dbo].[ResidentSet] (
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PersonalCode] bigint  NOT NULL,
    [RoomID] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Dormitory_ID] int  NOT NULL
);
GO

-- Creating table 'GuestSet'
CREATE TABLE [dbo].[GuestSet] (
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PersonalCode] bigint  NOT NULL
);
GO

-- Creating table 'DormitorySet'
CREATE TABLE [dbo].[DormitorySet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VisitSet'
CREATE TABLE [dbo].[VisitSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [VisitRegDateTime] datetime  NOT NULL,
    [IsOver] bit  NOT NULL,
    [VisitEndDateTime] datetime  NOT NULL,
    [ResidentPersonalCode] bigint  NOT NULL,
    [GuardPersonalCode] bigint  NOT NULL,
    [DormitoryID] int  NOT NULL,
    [Guest_PersonalCode] bigint  NULL
);
GO

-- Creating table 'GuardSet'
CREATE TABLE [dbo].[GuardSet] (
    [PersonalCode] bigint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Dormitory_ID] int  NOT NULL
);
GO

-- Creating table 'AdministratorSet'
CREATE TABLE [dbo].[AdministratorSet] (
    [PersonalCode] bigint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Dormitory_ID] int  NOT NULL
);
GO

-- Creating table 'RoomSet'
CREATE TABLE [dbo].[RoomSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DormitoryID] int  NOT NULL,
    [Number] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PersonalCode] in table 'ResidentSet'
ALTER TABLE [dbo].[ResidentSet]
ADD CONSTRAINT [PK_ResidentSet]
    PRIMARY KEY CLUSTERED ([PersonalCode] ASC);
GO

-- Creating primary key on [PersonalCode] in table 'GuestSet'
ALTER TABLE [dbo].[GuestSet]
ADD CONSTRAINT [PK_GuestSet]
    PRIMARY KEY CLUSTERED ([PersonalCode] ASC);
GO

-- Creating primary key on [ID] in table 'DormitorySet'
ALTER TABLE [dbo].[DormitorySet]
ADD CONSTRAINT [PK_DormitorySet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [PK_VisitSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [PersonalCode] in table 'GuardSet'
ALTER TABLE [dbo].[GuardSet]
ADD CONSTRAINT [PK_GuardSet]
    PRIMARY KEY CLUSTERED ([PersonalCode] ASC);
GO

-- Creating primary key on [PersonalCode] in table 'AdministratorSet'
ALTER TABLE [dbo].[AdministratorSet]
ADD CONSTRAINT [PK_AdministratorSet]
    PRIMARY KEY CLUSTERED ([PersonalCode] ASC);
GO

-- Creating primary key on [ID] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [PK_RoomSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Dormitory_ID] in table 'ResidentSet'
ALTER TABLE [dbo].[ResidentSet]
ADD CONSTRAINT [FK_DormitoryResident]
    FOREIGN KEY ([Dormitory_ID])
    REFERENCES [dbo].[DormitorySet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DormitoryResident'
CREATE INDEX [IX_FK_DormitoryResident]
ON [dbo].[ResidentSet]
    ([Dormitory_ID]);
GO

-- Creating foreign key on [ResidentPersonalCode] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_ResidentVisit]
    FOREIGN KEY ([ResidentPersonalCode])
    REFERENCES [dbo].[ResidentSet]
        ([PersonalCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidentVisit'
CREATE INDEX [IX_FK_ResidentVisit]
ON [dbo].[VisitSet]
    ([ResidentPersonalCode]);
GO

-- Creating foreign key on [Dormitory_ID] in table 'GuardSet'
ALTER TABLE [dbo].[GuardSet]
ADD CONSTRAINT [FK_GuardDormitory]
    FOREIGN KEY ([Dormitory_ID])
    REFERENCES [dbo].[DormitorySet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GuardDormitory'
CREATE INDEX [IX_FK_GuardDormitory]
ON [dbo].[GuardSet]
    ([Dormitory_ID]);
GO

-- Creating foreign key on [GuardPersonalCode] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_GuardVisit]
    FOREIGN KEY ([GuardPersonalCode])
    REFERENCES [dbo].[GuardSet]
        ([PersonalCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GuardVisit'
CREATE INDEX [IX_FK_GuardVisit]
ON [dbo].[VisitSet]
    ([GuardPersonalCode]);
GO

-- Creating foreign key on [Dormitory_ID] in table 'AdministratorSet'
ALTER TABLE [dbo].[AdministratorSet]
ADD CONSTRAINT [FK_AdministratorDormitory]
    FOREIGN KEY ([Dormitory_ID])
    REFERENCES [dbo].[DormitorySet]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdministratorDormitory'
CREATE INDEX [IX_FK_AdministratorDormitory]
ON [dbo].[AdministratorSet]
    ([Dormitory_ID]);
GO

-- Creating foreign key on [DormitoryID] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_DormitoryVisit]
    FOREIGN KEY ([DormitoryID])
    REFERENCES [dbo].[DormitorySet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DormitoryVisit'
CREATE INDEX [IX_FK_DormitoryVisit]
ON [dbo].[VisitSet]
    ([DormitoryID]);
GO

-- Creating foreign key on [DormitoryID] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [FK_RoomDormitory]
    FOREIGN KEY ([DormitoryID])
    REFERENCES [dbo].[DormitorySet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomDormitory'
CREATE INDEX [IX_FK_RoomDormitory]
ON [dbo].[RoomSet]
    ([DormitoryID]);
GO

-- Creating foreign key on [RoomID] in table 'ResidentSet'
ALTER TABLE [dbo].[ResidentSet]
ADD CONSTRAINT [FK_RoomResident]
    FOREIGN KEY ([RoomID])
    REFERENCES [dbo].[RoomSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomResident'
CREATE INDEX [IX_FK_RoomResident]
ON [dbo].[ResidentSet]
    ([RoomID]);
GO

-- Creating foreign key on [Guest_PersonalCode] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_VisitGuest]
    FOREIGN KEY ([Guest_PersonalCode])
    REFERENCES [dbo].[GuestSet]
        ([PersonalCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitGuest'
CREATE INDEX [IX_FK_VisitGuest]
ON [dbo].[VisitSet]
    ([Guest_PersonalCode]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------