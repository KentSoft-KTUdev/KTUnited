
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/07/2018 17:16:17
-- Generated from EDMX file: C:\Users\edvala\source\repos\WebAPI\WebAPI\DataModels\MainDbModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_RoomResident]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidentSet] DROP CONSTRAINT [FK_RoomResident];
GO
IF OBJECT_ID(N'[dbo].[FK_DormitoryResident]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidentSet] DROP CONSTRAINT [FK_DormitoryResident];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitGuest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GuestSet] DROP CONSTRAINT [FK_VisitGuest];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidentVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_ResidentVisit];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ResidentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResidentSet];
GO
IF OBJECT_ID(N'[dbo].[RoomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomSet];
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

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ResidentSet'
CREATE TABLE [dbo].[ResidentSet] (
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PersonalCode] bigint  NOT NULL,
    [Room_Number] int  NOT NULL,
    [Dormitory_ID] int  NOT NULL
);
GO

-- Creating table 'RoomSet'
CREATE TABLE [dbo].[RoomSet] (
    [Number] int  NOT NULL
);
GO

-- Creating table 'GuestSet'
CREATE TABLE [dbo].[GuestSet] (
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PersonalCode] bigint  NOT NULL,
    [VisitID] int  NOT NULL
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
    [ResidentPersonalCode] bigint  NOT NULL
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

-- Creating primary key on [Number] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [PK_RoomSet]
    PRIMARY KEY CLUSTERED ([Number] ASC);
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Room_Number] in table 'ResidentSet'
ALTER TABLE [dbo].[ResidentSet]
ADD CONSTRAINT [FK_RoomResident]
    FOREIGN KEY ([Room_Number])
    REFERENCES [dbo].[RoomSet]
        ([Number])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomResident'
CREATE INDEX [IX_FK_RoomResident]
ON [dbo].[ResidentSet]
    ([Room_Number]);
GO

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

-- Creating foreign key on [VisitID] in table 'GuestSet'
ALTER TABLE [dbo].[GuestSet]
ADD CONSTRAINT [FK_VisitGuest]
    FOREIGN KEY ([VisitID])
    REFERENCES [dbo].[VisitSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitGuest'
CREATE INDEX [IX_FK_VisitGuest]
ON [dbo].[GuestSet]
    ([VisitID]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------