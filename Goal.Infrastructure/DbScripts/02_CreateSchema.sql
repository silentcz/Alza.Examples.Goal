-- This script checks if the schema exists and creates it if it does not exist
IF NOT EXISTS (SELECT schema_name FROM information_schema.schemata WHERE schema_name = 'GoalSchema')
BEGIN
    CREATE SCHEMA GoalSchema AUTHORIZATION dbo;
    PRINT 'Schema GoalSchema has been created.';
END
ELSE
BEGIN
    PRINT 'Schema GoalSchema already exists.';
END
GO