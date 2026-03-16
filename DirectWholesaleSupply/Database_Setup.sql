-- ============================================================
-- Direct Wholesale Supply – Full SQL Server Setup Script
-- Run this in SQL Server Management Studio (SSMS) or
-- Azure Data Studio as an ALTERNATIVE to EF Core migrations.
-- ============================================================

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DirectWholesaleSupplyDB')
BEGIN
    CREATE DATABASE DirectWholesaleSupplyDB;
    PRINT 'Database created.';
END
GO

USE DirectWholesaleSupplyDB;
GO

-- ────────────────────────────────────────────────────────────
-- TABLES
-- ────────────────────────────────────────────────────────────

IF OBJECT_ID('PriceHistories','U') IS NOT NULL DROP TABLE PriceHistories;
IF OBJECT_ID('AuditLogs','U')      IS NOT NULL DROP TABLE AuditLogs;
IF OBJECT_ID('Products','U')       IS NOT NULL DROP TABLE Products;
IF OBJECT_ID('Users','U')          IS NOT NULL DROP TABLE Users;
IF OBJECT_ID('Categories','U')     IS NOT NULL DROP TABLE Categories;
IF OBJECT_ID('Roles','U')          IS NOT NULL DROP TABLE Roles;
GO

CREATE TABLE Roles (
    RoleID   INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE Categories (
    CategoryID   INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    SortOrder    INT NOT NULL DEFAULT 0
);

CREATE TABLE Users (
    UserID    INT IDENTITY(1,1) PRIMARY KEY,
    RoleID    INT NOT NULL REFERENCES Roles(RoleID) ON DELETE CASCADE,
    FullName  NVARCHAR(100) NOT NULL,
    Username  NVARCHAR(50)  NOT NULL,
    Password  NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2     NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE Products (
    ProductID      INT IDENTITY(1,1) PRIMARY KEY,
    CategoryID     INT             NOT NULL REFERENCES Categories(CategoryID) ON DELETE CASCADE,
    ProductName    NVARCHAR(150)   NOT NULL,
    Unit           NVARCHAR(20)    NOT NULL DEFAULT 'kg',
    WholesalePrice DECIMAL(10,2)   NOT NULL DEFAULT 0,
    RetailPrice    DECIMAL(10,2)   NOT NULL DEFAULT 0,
    Notes          NVARCHAR(255)   NULL,
    IsActive       BIT             NOT NULL DEFAULT 1,
    SortOrder      INT             NOT NULL DEFAULT 0
);

CREATE TABLE PriceHistories (
    HistoryID         INT IDENTITY(1,1) PRIMARY KEY,
    ProductID         INT           NOT NULL REFERENCES Products(ProductID) ON DELETE CASCADE,
    UserID            INT           NOT NULL REFERENCES Users(UserID),
    OldWholesalePrice DECIMAL(10,2) NOT NULL DEFAULT 0,
    NewWholesalePrice DECIMAL(10,2) NOT NULL DEFAULT 0,
    OldRetailPrice    DECIMAL(10,2) NOT NULL DEFAULT 0,
    NewRetailPrice    DECIMAL(10,2) NOT NULL DEFAULT 0,
    ChangedAt         DATETIME2     NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE AuditLogs (
    LogID   INT IDENTITY(1,1) PRIMARY KEY,
    UserID  INT           NOT NULL REFERENCES Users(UserID),
    Action  NVARCHAR(100) NOT NULL,
    Details NVARCHAR(500) NULL,
    LogDate DATETIME2     NOT NULL DEFAULT GETUTCDATE()
);

-- EF Core migrations history table
IF OBJECT_ID('__EFMigrationsHistory','U') IS NULL
BEGIN
    CREATE TABLE __EFMigrationsHistory (
        MigrationId    NVARCHAR(150) NOT NULL PRIMARY KEY,
        ProductVersion NVARCHAR(32)  NOT NULL
    );
END
GO

-- ────────────────────────────────────────────────────────────
-- SEED DATA
-- ────────────────────────────────────────────────────────────

SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (RoleID, RoleName) VALUES (1,'Admin'),(2,'Staff');
SET IDENTITY_INSERT Roles OFF;

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (UserID,RoleID,FullName,Username,Password,CreatedAt) VALUES
    (1,1,'Administrator','admin','admin123','2025-01-01'),
    (2,2,'Staff User','staff','staff123','2025-01-01');
SET IDENTITY_INSERT Users OFF;

SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (CategoryID,CategoryName,SortOrder) VALUES
    (1,'Fresh Vegetables',1),
    (2,'Premium Salad Greens',2),
    (3,'Seafood',3),
    (4,'Fresh Fruits',4),
    (5,'Pork – Fattener',5),
    (6,'Pork – Kitsahan / Bagkun',6);
SET IDENTITY_INSERT Categories OFF;

SET IDENTITY_INSERT Products ON;
INSERT INTO Products (ProductID,CategoryID,ProductName,Unit,WholesalePrice,RetailPrice,Notes,IsActive,SortOrder) VALUES
-- ── Fresh Vegetables ──────────────────────────────────────
( 1,1,'Broccoli','kg',60,70,NULL,1,1),
( 2,1,'Cauliflower','kg',55,65,NULL,1,2),
( 3,1,'Repolyo (Cabbage)','kg',35,45,NULL,1,3),
( 4,1,'Baguio Beans','kg',70,80,NULL,1,4),
( 5,1,'Sitaw (String Beans)','kg',50,60,NULL,1,5),
( 6,1,'Ampalaya (Bitter Gourd)','kg',45,55,NULL,1,6),
( 7,1,'Talong (Eggplant)','kg',40,50,NULL,1,7),
( 8,1,'Kamatis (Tomato)','kg',40,50,NULL,1,8),
( 9,1,'Sibuyas (Onion)','kg',60,75,NULL,1,9),
(10,1,'Bawang (Garlic)','kg',90,110,NULL,1,10),
(11,1,'Luya (Ginger)','kg',80,100,NULL,1,11),
(12,1,'Carrots','kg',50,60,NULL,1,12),
(13,1,'Patatas (Potato)','kg',55,65,NULL,1,13),
(14,1,'Sayote (Chayote)','kg',30,40,NULL,1,14),
(15,1,'Pechay (Bok Choy)','kg',35,45,NULL,1,15),
(16,1,'Kangkong (Water Spinach)','kg',25,35,NULL,1,16),
(17,1,'Kulitis (Amaranth Leaves)','kg',25,35,NULL,1,17),
(18,1,'Okra','kg',40,50,NULL,1,18),
(19,1,'Squash (Kalabasa)','kg',30,40,NULL,1,19),
(20,1,'Sweet Pepper (Bell Pepper)','kg',80,95,NULL,1,20),
(21,1,'Siling Haba (Long Green Chili)','kg',60,75,NULL,1,21),
(22,1,'Siling Labuyo (Bird''s Eye Chili)','kg',100,120,NULL,1,22),
(23,1,'Upo (Bottle Gourd)','pc',25,35,NULL,1,23),
(24,1,'Patola (Sponge Gourd)','pc',20,30,NULL,1,24),
(25,1,'Mais (Corn)','pc',15,20,NULL,1,25),
-- ── Premium Salad Greens ───────────────────────────────────
(26,2,'Romaine Lettuce','kg',120,150,NULL,1,1),
(27,2,'Iceberg Lettuce','kg',100,130,NULL,1,2),
(28,2,'Butter Lettuce','kg',130,160,NULL,1,3),
(29,2,'Arugula','kg',200,250,NULL,1,4),
(30,2,'Baby Spinach','kg',180,220,NULL,1,5),
(31,2,'Kale','kg',160,200,NULL,1,6),
(32,2,'Mixed Salad Greens','kg',150,185,NULL,1,7),
(33,2,'Watercress','kg',140,175,NULL,1,8),
(34,2,'Celery','kg',130,160,NULL,1,9),
(35,2,'Radicchio','kg',180,220,NULL,1,10),
-- ── Seafood ───────────────────────────────────────────────
(36,3,'Bangus (Milkfish)','kg',160,185,NULL,1,1),
(37,3,'Tilapia','kg',120,145,NULL,1,2),
(38,3,'Galunggong (Blue Mackerel Scad)','kg',140,165,NULL,1,3),
(39,3,'Alumahan (Indian Mackerel)','kg',150,175,NULL,1,4),
(40,3,'Tanigue (Spanish Mackerel)','kg',280,320,NULL,1,5),
(41,3,'Maya-Maya (Red Snapper)','kg',300,350,NULL,1,6),
(42,3,'Lapu-Lapu (Grouper)','kg',350,400,NULL,1,7),
(43,3,'Hipon (Shrimp)','kg',280,320,NULL,1,8),
(44,3,'Pusit (Squid)','kg',200,240,NULL,1,9),
(45,3,'Talaba (Oyster)','kg',120,150,NULL,1,10),
(46,3,'Alimango (Mud Crab)','kg',450,520,NULL,1,11),
(47,3,'Alimasag (Blue Swimming Crab)','kg',350,400,NULL,1,12),
(48,3,'Dilis (Anchovies)','kg',100,130,NULL,1,13),
(49,3,'Bisugo (Threadfin Bream)','kg',160,190,NULL,1,14),
(50,3,'Tuna (Yellowfin)','kg',250,290,NULL,1,15),
-- ── Fresh Fruits ──────────────────────────────────────────
(51,4,'Saging (Banana) – Lakatan','kg',50,65,NULL,1,1),
(52,4,'Saging (Banana) – Latundan','kg',40,55,NULL,1,2),
(53,4,'Mangga (Mango) – Carabao','kg',80,100,NULL,1,3),
(54,4,'Mangga (Mango) – Green','kg',60,75,NULL,1,4),
(55,4,'Pinya (Pineapple)','pc',45,60,NULL,1,5),
(56,4,'Watermelon','kg',25,35,NULL,1,6),
(57,4,'Papaya (Ripe)','kg',30,40,NULL,1,7),
(58,4,'Papaya (Green / Unripe)','kg',25,35,NULL,1,8),
(59,4,'Kalamansi (Calamondin)','kg',60,80,NULL,1,9),
(60,4,'Dalandan (Orange)','kg',70,90,NULL,1,10),
(61,4,'Avocado','kg',100,130,NULL,1,11),
(62,4,'Santol','kg',50,65,NULL,1,12),
(63,4,'Lanzones','kg',80,100,NULL,1,13),
(64,4,'Rambutan','kg',60,80,NULL,1,14),
(65,4,'Durian','kg',100,130,NULL,1,15),
-- ── Pork – Fattener ───────────────────────────────────────
(66,5,'Pork – Liempo (Belly)','kg',280,320,NULL,1,1),
(67,5,'Pork – Kasim (Shoulder)','kg',250,290,NULL,1,2),
(68,5,'Pork – Pigue (Ham / Leg)','kg',260,300,NULL,1,3),
(69,5,'Pork – Buto (Bones)','kg',120,150,NULL,1,4),
(70,5,'Pork – Laman (Lean Meat)','kg',270,310,NULL,1,5),
(71,5,'Pork – Ribs (Spareribs)','kg',260,300,NULL,1,6),
(72,5,'Pork – Ulo (Head)','kg',180,220,NULL,1,7),
(73,5,'Pork – Paa (Feet / Pata)','kg',200,240,NULL,1,8),
(74,5,'Pork – Atay (Liver)','kg',160,200,NULL,1,9),
(75,5,'Pork – Batok (Nape)','kg',250,290,NULL,1,10),
-- ── Pork – Kitsahan / Bagkun ──────────────────────────────
(76,6,'Kitsahan – Regular','kg',160,200,NULL,1,1),
(77,6,'Kitsahan – Premium','kg',190,230,NULL,1,2),
(78,6,'Bagkun (Pork Rind / Chicharon Cut)','kg',150,190,NULL,1,3),
(79,6,'Taba (Fatback)','kg',80,110,NULL,1,4),
(80,6,'Balunbalunan (Pork Intestines)','kg',120,160,NULL,1,5);
SET IDENTITY_INSERT Products OFF;

-- Mark migration as applied so EF won't try to re-run it
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion)
VALUES ('20250101000000_InitialCreate', '8.0.0');

PRINT 'Setup complete. 80 products seeded across 6 categories.';
GO
