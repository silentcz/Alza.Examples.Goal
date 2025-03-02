-- This script checks if the database exists and creates it if it does not exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GoalDatabase')
BEGIN
    CREATE DATABASE GoalDatabase;
    PRINT 'Database GoalDatabase has been created.';
END
ELSE
BEGIN
    PRINT 'Database GoalDatabase already exists.';
END
GO