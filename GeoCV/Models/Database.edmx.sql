
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/22/2015 20:47:16
-- Generated from EDMX file: C:\Users\Jago\Documents\Visual Studio 2013\Projects\GeoCV\GeoCV\Models\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [cv];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CVVersjonArbeidserfaring]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Arbeidserfaring] DROP CONSTRAINT [FK_CVVersjonArbeidserfaring];
GO
IF OBJECT_ID(N'[dbo].[FK_CVVersjonKompetanse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CVVersjon] DROP CONSTRAINT [FK_CVVersjonKompetanse];
GO
IF OBJECT_ID(N'[dbo].[FK_CVVersjonPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CVVersjon] DROP CONSTRAINT [FK_CVVersjonPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_CVVersjonProsjekt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prosjekt] DROP CONSTRAINT [FK_CVVersjonProsjekt];
GO
IF OBJECT_ID(N'[dbo].[FK_CVVersjonUtdannelse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utdannelse] DROP CONSTRAINT [FK_CVVersjonUtdannelse];
GO
IF OBJECT_ID(N'[dbo].[FK_MedlemPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Medlem] DROP CONSTRAINT [FK_MedlemPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_ProsjektMedlem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Medlem] DROP CONSTRAINT [FK_ProsjektMedlem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Arbeidserfaring]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Arbeidserfaring];
GO
IF OBJECT_ID(N'[dbo].[CVVersjon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CVVersjon];
GO
IF OBJECT_ID(N'[dbo].[DatabasesystemListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DatabasesystemListe];
GO
IF OBJECT_ID(N'[dbo].[Kompetanse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kompetanse];
GO
IF OBJECT_ID(N'[dbo].[Medlem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Medlem];
GO
IF OBJECT_ID(N'[dbo].[OperativsystemListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OperativsystemListe];
GO
IF OBJECT_ID(N'[dbo].[Person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Person];
GO
IF OBJECT_ID(N'[dbo].[ProgrammeringsspråkListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgrammeringsspråkListe];
GO
IF OBJECT_ID(N'[dbo].[Prosjekt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prosjekt];
GO
IF OBJECT_ID(N'[dbo].[RammeverkListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RammeverkListe];
GO
IF OBJECT_ID(N'[dbo].[ServersideListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServersideListe];
GO
IF OBJECT_ID(N'[dbo].[SpråkListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpråkListe];
GO
IF OBJECT_ID(N'[dbo].[Utdannelse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utdannelse];
GO
IF OBJECT_ID(N'[dbo].[WebTeknologiListe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WebTeknologiListe];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Arbeidserfaring'
CREATE TABLE [dbo].[Arbeidserfaring] (
    [ArbeidserfaringId] int IDENTITY(1,1) NOT NULL,
    [Arbeidsplass] nvarchar(max)  NULL,
    [Rolle] nvarchar(max)  NULL,
    [Beskrivelse] nvarchar(max)  NOT NULL,
    [Fra] smallint  NULL,
    [Til] smallint  NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL,
    [CVVersjonCVVersjonId] int  NOT NULL
);
GO

-- Creating table 'CVVersjon'
CREATE TABLE [dbo].[CVVersjon] (
    [CVVersjonId] int IDENTITY(1,1) NOT NULL,
    [AspNetUserId] nvarchar(max)  NOT NULL,
    [Beskrivelse] nvarchar(max)  NOT NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL,
    [Person_PersonId] int  NOT NULL,
    [Kompetanse_KompetanseId] int  NOT NULL
);
GO

-- Creating table 'DatabasesystemListe'
CREATE TABLE [dbo].[DatabasesystemListe] (
    [DatabasesystemListeId] int IDENTITY(1,1) NOT NULL,
    [Databasesystem] varchar(50)  NOT NULL
);
GO

-- Creating table 'Kompetanse'
CREATE TABLE [dbo].[Kompetanse] (
    [KompetanseId] int IDENTITY(1,1) NOT NULL,
    [Programmeringsspråk] nvarchar(max)  NULL,
    [Rammeverk] nvarchar(max)  NULL,
    [WebTeknologier] nvarchar(max)  NULL,
    [Databasesystemer] nvarchar(max)  NULL,
    [Serverside] nvarchar(max)  NULL,
    [Operativsystemer] nvarchar(max)  NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL
);
GO

-- Creating table 'Medlem'
CREATE TABLE [dbo].[Medlem] (
    [MedlemId] int IDENTITY(1,1) NOT NULL,
    [Rolle] nvarchar(max)  NULL,
    [Start] nvarchar(max)  NULL,
    [Slutt] nvarchar(max)  NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL,
    [ProsjektProsjektId] int  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'OperativsystemListe'
CREATE TABLE [dbo].[OperativsystemListe] (
    [OperativsystemListeId] int IDENTITY(1,1) NOT NULL,
    [Operativsystem] varchar(50)  NOT NULL
);
GO

-- Creating table 'Person'
CREATE TABLE [dbo].[Person] (
    [PersonId] int IDENTITY(1,1) NOT NULL,
    [Fornavn] nvarchar(max)  NOT NULL,
    [Etternavn] nvarchar(max)  NOT NULL,
    [Stilling] nvarchar(max)  NOT NULL,
    [ÅrErfaring] smallint  NOT NULL,
    [Språk] nvarchar(max)  NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL
);
GO

-- Creating table 'ProgrammeringsspråkListe'
CREATE TABLE [dbo].[ProgrammeringsspråkListe] (
    [ProgrammeringsspråkListeId] int IDENTITY(1,1) NOT NULL,
    [Programmeringsspråk] varchar(50)  NOT NULL
);
GO

-- Creating table 'Prosjekt'
CREATE TABLE [dbo].[Prosjekt] (
    [ProsjektId] int IDENTITY(1,1) NOT NULL,
    [Navn] nvarchar(max)  NULL,
    [Bedrift] nvarchar(max)  NULL,
    [Beskrivelse] nvarchar(max)  NOT NULL,
    [Fra] smallint  NULL,
    [Til] smallint  NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL,
    [CVVersjonCVVersjonId] int  NOT NULL
);
GO

-- Creating table 'RammeverkListe'
CREATE TABLE [dbo].[RammeverkListe] (
    [RammeverkId] int IDENTITY(1,1) NOT NULL,
    [Rammeverk] varchar(50)  NOT NULL
);
GO

-- Creating table 'ServersideListe'
CREATE TABLE [dbo].[ServersideListe] (
    [ServersideId] int IDENTITY(1,1) NOT NULL,
    [Serverside] varchar(50)  NOT NULL
);
GO

-- Creating table 'SpråkListe'
CREATE TABLE [dbo].[SpråkListe] (
    [SpråkId] int IDENTITY(1,1) NOT NULL,
    [Språk] varchar(50)  NULL
);
GO

-- Creating table 'Utdannelse'
CREATE TABLE [dbo].[Utdannelse] (
    [UtdannelseId] int IDENTITY(1,1) NOT NULL,
    [Beskrivelse] nvarchar(max)  NOT NULL,
    [Fra] smallint  NULL,
    [Til] smallint  NULL,
    [LagtTil] datetime  NOT NULL,
    [Modifisert] datetime  NOT NULL,
    [CVVersjonCVVersjonId] int  NOT NULL
);
GO

-- Creating table 'WebTeknologiListe'
CREATE TABLE [dbo].[WebTeknologiListe] (
    [WebTeknologiListeId] int IDENTITY(1,1) NOT NULL,
    [WebTeknologi] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ArbeidserfaringId] in table 'Arbeidserfaring'
ALTER TABLE [dbo].[Arbeidserfaring]
ADD CONSTRAINT [PK_Arbeidserfaring]
    PRIMARY KEY CLUSTERED ([ArbeidserfaringId] ASC);
GO

-- Creating primary key on [CVVersjonId] in table 'CVVersjon'
ALTER TABLE [dbo].[CVVersjon]
ADD CONSTRAINT [PK_CVVersjon]
    PRIMARY KEY CLUSTERED ([CVVersjonId] ASC);
GO

-- Creating primary key on [DatabasesystemListeId] in table 'DatabasesystemListe'
ALTER TABLE [dbo].[DatabasesystemListe]
ADD CONSTRAINT [PK_DatabasesystemListe]
    PRIMARY KEY CLUSTERED ([DatabasesystemListeId] ASC);
GO

-- Creating primary key on [KompetanseId] in table 'Kompetanse'
ALTER TABLE [dbo].[Kompetanse]
ADD CONSTRAINT [PK_Kompetanse]
    PRIMARY KEY CLUSTERED ([KompetanseId] ASC);
GO

-- Creating primary key on [MedlemId] in table 'Medlem'
ALTER TABLE [dbo].[Medlem]
ADD CONSTRAINT [PK_Medlem]
    PRIMARY KEY CLUSTERED ([MedlemId] ASC);
GO

-- Creating primary key on [OperativsystemListeId] in table 'OperativsystemListe'
ALTER TABLE [dbo].[OperativsystemListe]
ADD CONSTRAINT [PK_OperativsystemListe]
    PRIMARY KEY CLUSTERED ([OperativsystemListeId] ASC);
GO

-- Creating primary key on [PersonId] in table 'Person'
ALTER TABLE [dbo].[Person]
ADD CONSTRAINT [PK_Person]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [ProgrammeringsspråkListeId] in table 'ProgrammeringsspråkListe'
ALTER TABLE [dbo].[ProgrammeringsspråkListe]
ADD CONSTRAINT [PK_ProgrammeringsspråkListe]
    PRIMARY KEY CLUSTERED ([ProgrammeringsspråkListeId] ASC);
GO

-- Creating primary key on [ProsjektId] in table 'Prosjekt'
ALTER TABLE [dbo].[Prosjekt]
ADD CONSTRAINT [PK_Prosjekt]
    PRIMARY KEY CLUSTERED ([ProsjektId] ASC);
GO

-- Creating primary key on [RammeverkId] in table 'RammeverkListe'
ALTER TABLE [dbo].[RammeverkListe]
ADD CONSTRAINT [PK_RammeverkListe]
    PRIMARY KEY CLUSTERED ([RammeverkId] ASC);
GO

-- Creating primary key on [ServersideId] in table 'ServersideListe'
ALTER TABLE [dbo].[ServersideListe]
ADD CONSTRAINT [PK_ServersideListe]
    PRIMARY KEY CLUSTERED ([ServersideId] ASC);
GO

-- Creating primary key on [SpråkId] in table 'SpråkListe'
ALTER TABLE [dbo].[SpråkListe]
ADD CONSTRAINT [PK_SpråkListe]
    PRIMARY KEY CLUSTERED ([SpråkId] ASC);
GO

-- Creating primary key on [UtdannelseId] in table 'Utdannelse'
ALTER TABLE [dbo].[Utdannelse]
ADD CONSTRAINT [PK_Utdannelse]
    PRIMARY KEY CLUSTERED ([UtdannelseId] ASC);
GO

-- Creating primary key on [WebTeknologiListeId] in table 'WebTeknologiListe'
ALTER TABLE [dbo].[WebTeknologiListe]
ADD CONSTRAINT [PK_WebTeknologiListe]
    PRIMARY KEY CLUSTERED ([WebTeknologiListeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CVVersjonCVVersjonId] in table 'Arbeidserfaring'
ALTER TABLE [dbo].[Arbeidserfaring]
ADD CONSTRAINT [FK_CVVersjonArbeidserfaring]
    FOREIGN KEY ([CVVersjonCVVersjonId])
    REFERENCES [dbo].[CVVersjon]
        ([CVVersjonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CVVersjonArbeidserfaring'
CREATE INDEX [IX_FK_CVVersjonArbeidserfaring]
ON [dbo].[Arbeidserfaring]
    ([CVVersjonCVVersjonId]);
GO

-- Creating foreign key on [Kompetanse_KompetanseId] in table 'CVVersjon'
ALTER TABLE [dbo].[CVVersjon]
ADD CONSTRAINT [FK_CVVersjonKompetanse]
    FOREIGN KEY ([Kompetanse_KompetanseId])
    REFERENCES [dbo].[Kompetanse]
        ([KompetanseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CVVersjonKompetanse'
CREATE INDEX [IX_FK_CVVersjonKompetanse]
ON [dbo].[CVVersjon]
    ([Kompetanse_KompetanseId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'CVVersjon'
ALTER TABLE [dbo].[CVVersjon]
ADD CONSTRAINT [FK_CVVersjonPerson]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Person]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CVVersjonPerson'
CREATE INDEX [IX_FK_CVVersjonPerson]
ON [dbo].[CVVersjon]
    ([Person_PersonId]);
GO

-- Creating foreign key on [CVVersjonCVVersjonId] in table 'Prosjekt'
ALTER TABLE [dbo].[Prosjekt]
ADD CONSTRAINT [FK_CVVersjonProsjekt]
    FOREIGN KEY ([CVVersjonCVVersjonId])
    REFERENCES [dbo].[CVVersjon]
        ([CVVersjonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CVVersjonProsjekt'
CREATE INDEX [IX_FK_CVVersjonProsjekt]
ON [dbo].[Prosjekt]
    ([CVVersjonCVVersjonId]);
GO

-- Creating foreign key on [CVVersjonCVVersjonId] in table 'Utdannelse'
ALTER TABLE [dbo].[Utdannelse]
ADD CONSTRAINT [FK_CVVersjonUtdannelse]
    FOREIGN KEY ([CVVersjonCVVersjonId])
    REFERENCES [dbo].[CVVersjon]
        ([CVVersjonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CVVersjonUtdannelse'
CREATE INDEX [IX_FK_CVVersjonUtdannelse]
ON [dbo].[Utdannelse]
    ([CVVersjonCVVersjonId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'Medlem'
ALTER TABLE [dbo].[Medlem]
ADD CONSTRAINT [FK_MedlemPerson]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Person]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MedlemPerson'
CREATE INDEX [IX_FK_MedlemPerson]
ON [dbo].[Medlem]
    ([Person_PersonId]);
GO

-- Creating foreign key on [ProsjektProsjektId] in table 'Medlem'
ALTER TABLE [dbo].[Medlem]
ADD CONSTRAINT [FK_ProsjektMedlem]
    FOREIGN KEY ([ProsjektProsjektId])
    REFERENCES [dbo].[Prosjekt]
        ([ProsjektId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProsjektMedlem'
CREATE INDEX [IX_FK_ProsjektMedlem]
ON [dbo].[Medlem]
    ([ProsjektProsjektId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------