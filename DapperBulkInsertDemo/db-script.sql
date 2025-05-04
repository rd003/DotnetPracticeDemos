USE master
GO

CREATE TABLE Person
(
    PersonId int identity (1,1),
    FirstName NVARCHAR(20),
    LastName NVARCHAR(20)

        constraint PK_Person_Id primary key (PersonId)
)
GO