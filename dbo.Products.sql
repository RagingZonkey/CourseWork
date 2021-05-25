CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(200) NULL, 
    [Cost] NVARCHAR(20) NULL, 
    [CostEdit] NVARCHAR(20) NULL, 
    [Description] NVARCHAR(500) NULL, 
    [DescriptionEdit] NVARCHAR(500) NULL, 
    [MainImagePath] NVARCHAR(300) NULL, 
    [IsActive] NVARCHAR(20) NULL, 
    [ManufacturerId] NVARCHAR(20) NULL
)
