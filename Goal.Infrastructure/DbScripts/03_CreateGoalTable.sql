-- This script checks if the table exists and creates it if it does not exist
IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_schema = 'GoalSchema' AND table_name = 'Product')
BEGIN
CREATE TABLE GoalSchema.Product (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    Name NVARCHAR(255) NOT NULL,
                                    ImgUri NVARCHAR(2083) NOT NULL,
                                    Price DECIMAL(18,2) NOT NULL,
                                    Description NVARCHAR(MAX) NULL
);
PRINT 'Table Product has been created in schema GoalSchema.';
END
ELSE
BEGIN
    PRINT 'Table Product already exists in schema GoalSchema.';
END
GO
