use GoalDatabase;
-- Index to product name
CREATE NONCLUSTERED INDEX IX_Product_Name ON GoalSchema.Product (Name);

-- Index to product price
CREATE NONCLUSTERED INDEX IX_Product_Price ON GoalSchema.Product (Price);

-- Multi-column index on product name and price
CREATE NONCLUSTERED INDEX IX_Product_Name_Price ON GoalSchema.Product (Name, Price);