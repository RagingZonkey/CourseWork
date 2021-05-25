CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Idusers] INT NULL, 
    [Email] NVARCHAR(50) NULL, 
    [Title] NVARCHAR(50) NULL, 
    [Cost] NVARCHAR(20) NULL, 
    [CostEdit] NVARCHAR(20) NULL, 
    [DurationInSeconds] NVARCHAR(20) NULL, 
    [DurationInSecondsEdit] NVARCHAR(20) NULL, 
    [ManufacturerId] NVARCHAR(20) NULL, 
    [Description] NVARCHAR(500) NULL, 
    [DescriptionEdit] NVARCHAR(500) NULL, 
    [Discount] NVARCHAR(20) NULL, 
    [DiscountEdit] NVARCHAR(20) NULL, 
    [MainImagePath] NVARCHAR(100) NULL, 
    [OrderDate] NVARCHAR(20) NULL, 
    [OrderDateEdit] NVARCHAR(20) NULL, 
    [ReservDay] NVARCHAR(20) NULL
)
