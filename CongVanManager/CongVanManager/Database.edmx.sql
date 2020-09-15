
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/15/2020 14:26:22
-- Generated from EDMX file: C:\Users\longt\source\repos\CongVanManager\CongVanManager\CongVanManager\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LoaiCongVans'
CREATE TABLE [dbo].[LoaiCongVans] (
    [Id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LienHes'
CREATE TABLE [dbo].[LienHes] (
    [Email] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CongVans'
CREATE TABLE [dbo].[CongVans] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SoKyHieu] int  NULL,
    [NgayCongVan] datetime  NULL,
    [TrichYeu] nvarchar(max)  NULL,
    [GhiChu] nvarchar(max)  NULL,
    [PDFScanLocation] nvarchar(max)  NOT NULL,
    [NgayXuLi] datetime  NOT NULL,
    [LoaiCongVan_Id] nvarchar(max)  NOT NULL,
    [LienHe_Email] nvarchar(max)  NOT NULL,
    [HopCongVan_LoaiHop] int  NOT NULL
);
GO

-- Creating table 'NoiNhans'
CREATE TABLE [dbo].[NoiNhans] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CongVan_Id] int  NOT NULL,
    [LienHe_Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HopCongVans'
CREATE TABLE [dbo].[HopCongVans] (
    [LoaiHop] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LoaiCongVans'
ALTER TABLE [dbo].[LoaiCongVans]
ADD CONSTRAINT [PK_LoaiCongVans]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Email] in table 'LienHes'
ALTER TABLE [dbo].[LienHes]
ADD CONSTRAINT [PK_LienHes]
    PRIMARY KEY CLUSTERED ([Email] ASC);
GO

-- Creating primary key on [Id] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [PK_CongVans]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NoiNhans'
ALTER TABLE [dbo].[NoiNhans]
ADD CONSTRAINT [PK_NoiNhans]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoaiHop] in table 'HopCongVans'
ALTER TABLE [dbo].[HopCongVans]
ADD CONSTRAINT [PK_HopCongVans]
    PRIMARY KEY CLUSTERED ([LoaiHop] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LoaiCongVan_Id] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [FK_LoaiCongVanCongVan]
    FOREIGN KEY ([LoaiCongVan_Id])
    REFERENCES [dbo].[LoaiCongVans]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoaiCongVanCongVan'
CREATE INDEX [IX_FK_LoaiCongVanCongVan]
ON [dbo].[CongVans]
    ([LoaiCongVan_Id]);
GO

-- Creating foreign key on [LienHe_Email] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [FK_LienHeCongVan]
    FOREIGN KEY ([LienHe_Email])
    REFERENCES [dbo].[LienHes]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LienHeCongVan'
CREATE INDEX [IX_FK_LienHeCongVan]
ON [dbo].[CongVans]
    ([LienHe_Email]);
GO

-- Creating foreign key on [CongVan_Id] in table 'NoiNhans'
ALTER TABLE [dbo].[NoiNhans]
ADD CONSTRAINT [FK_CongVanDanhSachNoiNhan]
    FOREIGN KEY ([CongVan_Id])
    REFERENCES [dbo].[CongVans]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CongVanDanhSachNoiNhan'
CREATE INDEX [IX_FK_CongVanDanhSachNoiNhan]
ON [dbo].[NoiNhans]
    ([CongVan_Id]);
GO

-- Creating foreign key on [LienHe_Email] in table 'NoiNhans'
ALTER TABLE [dbo].[NoiNhans]
ADD CONSTRAINT [FK_LienHeDanhSachNoiNhan]
    FOREIGN KEY ([LienHe_Email])
    REFERENCES [dbo].[LienHes]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LienHeDanhSachNoiNhan'
CREATE INDEX [IX_FK_LienHeDanhSachNoiNhan]
ON [dbo].[NoiNhans]
    ([LienHe_Email]);
GO

-- Creating foreign key on [HopCongVan_LoaiHop] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [FK_HopCongVanCongVan]
    FOREIGN KEY ([HopCongVan_LoaiHop])
    REFERENCES [dbo].[HopCongVans]
        ([LoaiHop])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HopCongVanCongVan'
CREATE INDEX [IX_FK_HopCongVanCongVan]
ON [dbo].[CongVans]
    ([HopCongVan_LoaiHop]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------