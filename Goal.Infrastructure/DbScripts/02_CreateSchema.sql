Use GoalDatabase;
IF NOT EXISTS (SELECT schema_name
               FROM information_schema.schemata
               WHERE schema_name = 'GoalSchema')
    BEGIN
        CREATE SCHEMA GoalDatabase.GoalSchema;
        PRINT 'Schema "GoalSchema" bylo úspěšně vytvořeno.';
    END
ELSE
    BEGIN
        PRINT 'Schema "GoalSchema" již existuje.';
    END
