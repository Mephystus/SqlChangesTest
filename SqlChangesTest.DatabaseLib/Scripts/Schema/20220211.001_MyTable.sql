
IF EXISTS (SELECT 1 
           FROM sys.tables
           WHERE name = 'MyTable') 
	BEGIN
		PRINT 'TABLE ALREADY CREATED!'
		RETURN
	END

CREATE TABLE MyTable(
	Id int NOT NULL,
	Name nvarchar(50) NOT NULL
) ON [PRIMARY]
