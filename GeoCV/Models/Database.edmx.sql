
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/08/2015 11:16:53
-- Generated from EDMX file: C:\Users\Joar\Source\Repos\GeoCV\GeoCV\Models\Database.edmx
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
IF OBJECT_ID(N'[dbo].[FK_CVVersjonInnstillinger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CVVersjon] DROP CONSTRAINT [FK_CVVersjonInnstillinger];
GO
IF OBJECT_ID(N'[dbo].[FK_CVVersjonKompetanse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CVVersjon] DROP CONSTRAINT [FK_CVVersjonKompetanse];
GO
IF OBJECT_ID(N'[dbo].[FK_CVVersjonPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CVVersjon] DROP CONSTRAINT [FK_CVVersjonPerson];
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
IF OBJECT_ID(N'[dbo].[FK_ProsjektTekniskProfil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TekniskProfil] DROP CONSTRAINT [FK_ProsjektTekniskProfil];
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
IF OBJECT_ID(N'[dbo].[Feedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedback];
GO
IF OBJECT_ID(N'[dbo].[Innstillinger]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Innstillinger];
GO
IF OBJECT_ID(N'[dbo].[Kompetanse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kompetanse];
GO
IF OBJECT_ID(N'[dbo].[Medlem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Medlem];
GO
IF OBJECT_ID(N'[dbo].[Person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Person];
GO
IF OBJECT_ID(N'[dbo].[Prosjekt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prosjekt];
GO
IF OBJECT_ID(N'[dbo].[TekniskProfil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TekniskProfil];
GO
IF OBJECT_ID(N'[dbo].[Utdannelse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utdannelse];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Arbeidserfaring'
CREATE TABLE [dbo].[Arbeidserfaring] (
    [ArbeidserfaringId] int IDENTITY(1,1) NOT NULL,
    [Arbeidsplass] nvarchar(max)  NOT NULL,
    [Stilling] nvarchar(max)  NOT NULL,
    [Beskrivelse] nvarchar(max)  NOT NULL,
    [Fra] smallint  NULL,
    [Til] smallint  NULL,
    [CVVersjonCVVersjonId] int  NOT NULL
);
GO

-- Creating table 'CVVersjon'
CREATE TABLE [dbo].[CVVersjon] (
    [CVVersjonId] int IDENTITY(1,1) NOT NULL,
    [AspNetUserId] nvarchar(max)  NOT NULL,
    [Beskrivelse] nvarchar(max)  NULL,
    [Person_PersonId] int  NOT NULL,
    [Kompetanse_KompetanseId] int  NOT NULL,
    [Innstillinger_InnstillingerId] int  NOT NULL
);
GO

-- Creating table 'Feedback'
CREATE TABLE [dbo].[Feedback] (
    [FeedbackId] int IDENTITY(1,1) NOT NULL,
    [Beskjed] nvarchar(max)  NOT NULL,
    [Person] nvarchar(max)  NOT NULL,
    [Dato] datetime  NOT NULL
);
GO

-- Creating table 'Innstillinger'
CREATE TABLE [dbo].[Innstillinger] (
    [InnstillingerId] int IDENTITY(1,1) NOT NULL,
    [Fornavn] bit  NOT NULL,
    [Mellomnavn] bit  NOT NULL,
    [Etternavn] bit  NOT NULL,
    [Stilling] bit  NOT NULL,
    [ÅrErfaring] bit  NOT NULL,
    [Språk] bit  NOT NULL,
    [Nasjonalitet] bit  NOT NULL,
    [Fødselsår] bit  NOT NULL,
    [Programmeringsspråk] bit  NOT NULL,
    [Rammeverk] bit  NOT NULL,
    [WebTeknologier] bit  NOT NULL,
    [Databasesystemer] bit  NOT NULL,
    [Serverside] bit  NOT NULL,
    [Operativsystemer] bit  NOT NULL,
    [Utdannelse] bit  NOT NULL,
    [Arbeidserfaring] bit  NOT NULL,
    [Prosjekter] bit  NOT NULL
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
    [Operativsystemer] nvarchar(max)  NULL
);
GO

-- Creating table 'Medlem'
CREATE TABLE [dbo].[Medlem] (
    [MedlemId] int IDENTITY(1,1) NOT NULL,
    [Rolle] nvarchar(max)  NOT NULL,
    [Start] smallint  NOT NULL,
    [Slutt] smallint  NOT NULL,
    [ProsjektProsjektId] int  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'Person'
CREATE TABLE [dbo].[Person] (
    [PersonId] int IDENTITY(1,1) NOT NULL,
    [Fornavn] nvarchar(50)  NOT NULL,
    [Mellomnavn] nvarchar(50)  NULL,
    [Etternavn] nvarchar(80)  NOT NULL,
    [Stilling] nvarchar(50)  NULL,
    [ÅrErfaring] smallint  NULL,
    [Språk] nvarchar(max)  NULL,
    [Nasjonalitet] nvarchar(40)  NULL,
    [Fødselsår] smallint  NULL
);
GO

-- Creating table 'Prosjekt'
CREATE TABLE [dbo].[Prosjekt] (
    [ProsjektId] int IDENTITY(1,1) NOT NULL,
    [Navn] nvarchar(max)  NOT NULL,
    [Kunde] nvarchar(max)  NOT NULL,
    [Beskrivelse] nvarchar(max)  NULL,
    [Fra] smallint  NULL,
    [Til] smallint  NULL
);
GO

-- Creating table 'TekniskProfil'
CREATE TABLE [dbo].[TekniskProfil] (
    [TekniskProfilId] int IDENTITY(1,1) NOT NULL,
    [Navn] nvarchar(50)  NOT NULL,
    [Elementer] nvarchar(max)  NOT NULL,
    [ProsjektProsjektId] int  NOT NULL
);
GO

-- Creating table 'Utdannelse'
CREATE TABLE [dbo].[Utdannelse] (
    [UtdannelseId] int IDENTITY(1,1) NOT NULL,
    [Studiested] nvarchar(max)  NOT NULL,
    [Beskrivelse] nvarchar(max)  NOT NULL,
    [Fra] smallint  NULL,
    [Til] smallint  NULL,
    [CVVersjonCVVersjonId] int  NOT NULL
);
GO

-- Creating table 'ListeKatalog'
CREATE TABLE [dbo].[ListeKatalog] (
    [ListeKatalogId] int IDENTITY(1,1) NOT NULL,
    [Element] nvarchar(max)  NOT NULL,
    [Katalog] nvarchar(max)  NOT NULL
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

-- Creating primary key on [FeedbackId] in table 'Feedback'
ALTER TABLE [dbo].[Feedback]
ADD CONSTRAINT [PK_Feedback]
    PRIMARY KEY CLUSTERED ([FeedbackId] ASC);
GO

-- Creating primary key on [InnstillingerId] in table 'Innstillinger'
ALTER TABLE [dbo].[Innstillinger]
ADD CONSTRAINT [PK_Innstillinger]
    PRIMARY KEY CLUSTERED ([InnstillingerId] ASC);
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

-- Creating primary key on [PersonId] in table 'Person'
ALTER TABLE [dbo].[Person]
ADD CONSTRAINT [PK_Person]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [ProsjektId] in table 'Prosjekt'
ALTER TABLE [dbo].[Prosjekt]
ADD CONSTRAINT [PK_Prosjekt]
    PRIMARY KEY CLUSTERED ([ProsjektId] ASC);
GO

-- Creating primary key on [TekniskProfilId] in table 'TekniskProfil'
ALTER TABLE [dbo].[TekniskProfil]
ADD CONSTRAINT [PK_TekniskProfil]
    PRIMARY KEY CLUSTERED ([TekniskProfilId] ASC);
GO

-- Creating primary key on [UtdannelseId] in table 'Utdannelse'
ALTER TABLE [dbo].[Utdannelse]
ADD CONSTRAINT [PK_Utdannelse]
    PRIMARY KEY CLUSTERED ([UtdannelseId] ASC);
GO

-- Creating primary key on [ListeKatalogId] in table 'ListeKatalog'
ALTER TABLE [dbo].[ListeKatalog]
ADD CONSTRAINT [PK_ListeKatalog]
    PRIMARY KEY CLUSTERED ([ListeKatalogId] ASC);
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

-- Creating foreign key on [Innstillinger_InnstillingerId] in table 'CVVersjon'
ALTER TABLE [dbo].[CVVersjon]
ADD CONSTRAINT [FK_CVVersjonInnstillinger]
    FOREIGN KEY ([Innstillinger_InnstillingerId])
    REFERENCES [dbo].[Innstillinger]
        ([InnstillingerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CVVersjonInnstillinger'
CREATE INDEX [IX_FK_CVVersjonInnstillinger]
ON [dbo].[CVVersjon]
    ([Innstillinger_InnstillingerId]);
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

-- Creating foreign key on [ProsjektProsjektId] in table 'TekniskProfil'
ALTER TABLE [dbo].[TekniskProfil]
ADD CONSTRAINT [FK_ProsjektTekniskProfil]
    FOREIGN KEY ([ProsjektProsjektId])
    REFERENCES [dbo].[Prosjekt]
        ([ProsjektId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProsjektTekniskProfil'
CREATE INDEX [IX_FK_ProsjektTekniskProfil]
ON [dbo].[TekniskProfil]
    ([ProsjektProsjektId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------