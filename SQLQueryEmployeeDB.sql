CREATE DATABASE EmployeeDB;
GO

USE EmployeeDB;
GO

CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,           
    FirstName NVARCHAR(50) NOT NULL,                   
    LastName NVARCHAR(50) NOT NULL,                    
    Email NVARCHAR(100) NOT NULL,                      
    Phone NVARCHAR(15),                                
    HireDate DATETIME NOT NULL DEFAULT GETDATE(),      
    CONSTRAINT UQ_Email UNIQUE (Email),                
    CONSTRAINT CHK_HireDate CHECK (HireDate <= GETDATE()),
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);
GO

CREATE INDEX idx_LastName ON Employees (LastName); 
CREATE INDEX idx_HireDate ON Employees (HireDate); 
GO

CREATE TRIGGER trg_UpdateTimestamp
ON Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Employees
    SET UpdatedAt = GETDATE()
    WHERE EmployeeID IN (SELECT DISTINCT EmployeeID FROM Inserted);
END;
GO


INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate)
VALUES ('Alice', 'Smith', 'alice.smith1@example.com', '123-456-7890', '2024-01-01');
GO

UPDATE Employees
SET Phone = '987-654-3210'
WHERE EmployeeID = 1;
GO



SELECT * FROM Employees;
GO

drop table Employees
GO


