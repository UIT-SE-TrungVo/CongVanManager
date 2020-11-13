
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/13/2020 16:06:21
-- Generated from EDMX file: C:\Users\longt\source\repos\CongVanManager\CongVanManager\CongVanManager\View\QLCV.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CONGVANMANAGER];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__CongVan__MaLoaiC__29221CFB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CongVans] DROP CONSTRAINT [FK__CongVan__MaLoaiC__29221CFB];
GO
IF OBJECT_ID(N'[dbo].[FK__CongVan__NoiGui__2A164134]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CongVans] DROP CONSTRAINT [FK__CongVan__NoiGui__2A164134];
GO
IF OBJECT_ID(N'[dbo].[FK__PhanHoi__MaCongV__2DE6D218]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhanHois] DROP CONSTRAINT [FK__PhanHoi__MaCongV__2DE6D218];
GO
IF OBJECT_ID(N'[dbo].[FK__PhanHoi__TenTaiK__2EDAF651]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhanHois] DROP CONSTRAINT [FK__PhanHoi__TenTaiK__2EDAF651];
GO
IF OBJECT_ID(N'[dbo].[FK_DanhSachNoiNhan_CongVan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DanhSachNoiNhan] DROP CONSTRAINT [FK_DanhSachNoiNhan_CongVan];
GO
IF OBJECT_ID(N'[dbo].[FK_DanhSachNoiNhan_LienHe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DanhSachNoiNhan] DROP CONSTRAINT [FK_DanhSachNoiNhan_LienHe];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CongVans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CongVans];
GO
IF OBJECT_ID(N'[dbo].[LienHes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LienHes];
GO
IF OBJECT_ID(N'[dbo].[LoaiCongVans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoaiCongVans];
GO
IF OBJECT_ID(N'[dbo].[NguoiDungs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NguoiDungs];
GO
IF OBJECT_ID(N'[dbo].[PhanHois]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhanHois];
GO
IF OBJECT_ID(N'[dbo].[QuyDinhs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuyDinhs];
GO
IF OBJECT_ID(N'[dbo].[database_firewall_rules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[database_firewall_rules];
GO
IF OBJECT_ID(N'[dbo].[DanhSachNoiNhan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DanhSachNoiNhan];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CongVans'
CREATE TABLE [dbo].[CongVans] (
    [MaCongVan] bigint  NOT NULL,
    [MaLoaiCongVan] nvarchar(20)  NOT NULL,
    [NoiGui] varchar(50)  NOT NULL,
    [SoKyHieu] varchar(20)  NULL,
    [SoCongVan] int  NULL,
    [NgayCongVan] datetime  NULL,
    [TrichYeu] nvarchar(50)  NULL,
    [TinhTrang] int  NULL,
    [GhiChu] nvarchar(50)  NULL,
    [PDFScan] varchar(20)  NULL,
    [NgayXuLi] datetime  NOT NULL
);
GO

-- Creating table 'LienHes'
CREATE TABLE [dbo].[LienHes] (
    [Email] varchar(50)  NOT NULL,
    [TenLienHe] nvarchar(50)  NULL
);
GO

-- Creating table 'LoaiCongVans'
CREATE TABLE [dbo].[LoaiCongVans] (
    [MaLoaiCongVan] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'NguoiDungs'
CREATE TABLE [dbo].[NguoiDungs] (
    [TenTaiKhoan] varchar(20)  NOT NULL,
    [LoaiNguoiDung] nvarchar(50)  NOT NULL,
    [MatKhau] varchar(50)  NOT NULL
);
GO

-- Creating table 'PhanHois'
CREATE TABLE [dbo].[PhanHois] (
    [MaPhanHoi] varchar(20)  NOT NULL,
    [MaCongVan] bigint  NOT NULL,
    [TenTaiKhoan] varchar(20)  NOT NULL,
    [BinhLuan] nvarchar(100)  NULL
);
GO

-- Creating table 'QuyDinhs'
CREATE TABLE [dbo].[QuyDinhs] (
    [MaQuyDinh] varchar(20)  NOT NULL,
    [GiaTri] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'database_firewall_rules'
CREATE TABLE [dbo].[database_firewall_rules] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(128)  NOT NULL,
    [start_ip_address] varchar(45)  NOT NULL,
    [end_ip_address] varchar(45)  NOT NULL,
    [create_date] datetime  NOT NULL,
    [modify_date] datetime  NOT NULL
);
GO

-- Creating table 'DanhSachNoiNhan'
CREATE TABLE [dbo].[DanhSachNoiNhan] (
    [CongVans1_MaCongVan] bigint  NOT NULL,
    [LienHes_Email] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MaCongVan] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [PK_CongVans]
    PRIMARY KEY CLUSTERED ([MaCongVan] ASC);
GO

-- Creating primary key on [Email] in table 'LienHes'
ALTER TABLE [dbo].[LienHes]
ADD CONSTRAINT [PK_LienHes]
    PRIMARY KEY CLUSTERED ([Email] ASC);
GO

-- Creating primary key on [MaLoaiCongVan] in table 'LoaiCongVans'
ALTER TABLE [dbo].[LoaiCongVans]
ADD CONSTRAINT [PK_LoaiCongVans]
    PRIMARY KEY CLUSTERED ([MaLoaiCongVan] ASC);
GO

-- Creating primary key on [TenTaiKhoan] in table 'NguoiDungs'
ALTER TABLE [dbo].[NguoiDungs]
ADD CONSTRAINT [PK_NguoiDungs]
    PRIMARY KEY CLUSTERED ([TenTaiKhoan] ASC);
GO

-- Creating primary key on [MaPhanHoi] in table 'PhanHois'
ALTER TABLE [dbo].[PhanHois]
ADD CONSTRAINT [PK_PhanHois]
    PRIMARY KEY CLUSTERED ([MaPhanHoi] ASC);
GO

-- Creating primary key on [MaQuyDinh] in table 'QuyDinhs'
ALTER TABLE [dbo].[QuyDinhs]
ADD CONSTRAINT [PK_QuyDinhs]
    PRIMARY KEY CLUSTERED ([MaQuyDinh] ASC);
GO

-- Creating primary key on [id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] in table 'database_firewall_rules'
ALTER TABLE [dbo].[database_firewall_rules]
ADD CONSTRAINT [PK_database_firewall_rules]
    PRIMARY KEY CLUSTERED ([id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] ASC);
GO

-- Creating primary key on [CongVans1_MaCongVan], [LienHes_Email] in table 'DanhSachNoiNhan'
ALTER TABLE [dbo].[DanhSachNoiNhan]
ADD CONSTRAINT [PK_DanhSachNoiNhan]
    PRIMARY KEY CLUSTERED ([CongVans1_MaCongVan], [LienHes_Email] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MaLoaiCongVan] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [FK__CongVan__MaLoaiC__29221CFB]
    FOREIGN KEY ([MaLoaiCongVan])
    REFERENCES [dbo].[LoaiCongVans]
        ([MaLoaiCongVan])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CongVan__MaLoaiC__29221CFB'
CREATE INDEX [IX_FK__CongVan__MaLoaiC__29221CFB]
ON [dbo].[CongVans]
    ([MaLoaiCongVan]);
GO

-- Creating foreign key on [NoiGui] in table 'CongVans'
ALTER TABLE [dbo].[CongVans]
ADD CONSTRAINT [FK__CongVan__NoiGui__2A164134]
    FOREIGN KEY ([NoiGui])
    REFERENCES [dbo].[LienHes]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CongVan__NoiGui__2A164134'
CREATE INDEX [IX_FK__CongVan__NoiGui__2A164134]
ON [dbo].[CongVans]
    ([NoiGui]);
GO

-- Creating foreign key on [MaCongVan] in table 'PhanHois'
ALTER TABLE [dbo].[PhanHois]
ADD CONSTRAINT [FK__PhanHoi__MaCongV__2DE6D218]
    FOREIGN KEY ([MaCongVan])
    REFERENCES [dbo].[CongVans]
        ([MaCongVan])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__PhanHoi__MaCongV__2DE6D218'
CREATE INDEX [IX_FK__PhanHoi__MaCongV__2DE6D218]
ON [dbo].[PhanHois]
    ([MaCongVan]);
GO

-- Creating foreign key on [TenTaiKhoan] in table 'PhanHois'
ALTER TABLE [dbo].[PhanHois]
ADD CONSTRAINT [FK__PhanHoi__TenTaiK__2EDAF651]
    FOREIGN KEY ([TenTaiKhoan])
    REFERENCES [dbo].[NguoiDungs]
        ([TenTaiKhoan])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__PhanHoi__TenTaiK__2EDAF651'
CREATE INDEX [IX_FK__PhanHoi__TenTaiK__2EDAF651]
ON [dbo].[PhanHois]
    ([TenTaiKhoan]);
GO

-- Creating foreign key on [CongVans1_MaCongVan] in table 'DanhSachNoiNhan'
ALTER TABLE [dbo].[DanhSachNoiNhan]
ADD CONSTRAINT [FK_DanhSachNoiNhan_CongVan]
    FOREIGN KEY ([CongVans1_MaCongVan])
    REFERENCES [dbo].[CongVans]
        ([MaCongVan])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LienHes_Email] in table 'DanhSachNoiNhan'
ALTER TABLE [dbo].[DanhSachNoiNhan]
ADD CONSTRAINT [FK_DanhSachNoiNhan_LienHe]
    FOREIGN KEY ([LienHes_Email])
    REFERENCES [dbo].[LienHes]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DanhSachNoiNhan_LienHe'
CREATE INDEX [IX_FK_DanhSachNoiNhan_LienHe]
ON [dbo].[DanhSachNoiNhan]
    ([LienHes_Email]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------