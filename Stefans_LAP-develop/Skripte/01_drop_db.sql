USE master;
GO

IF NOT DB_ID('Innovation4Austria') IS NULL ALTER DATABASE Innovation4austria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
IF NOT DB_ID('Innovation4Austria') IS NULL DROP DATABASE Innovation4austria;
GO