use master;
go

drop database if exists PersonDb;
go

create database PersonDb;
go

use PersonDb;
go

Create table People
(
  PersonID int identity(1,1),
  PersonName nvarchar(50) not null,
  PersonEmail Nvarchar(50) not null

  Constraint PK_People_PersonId primary key (PersonID)
);
go

Create unique nonclustered index IX_People_Email on People(PersonEmail);
go

insert into People(PersonName,PersonEmail)
values
('Ravindra','ravindra@gmail.com'),
('Ramesh','ramesh@gmail.com'),
('Harish','harish@gmail.com'),
('Saraswati','saraswati@gmail.com');

go