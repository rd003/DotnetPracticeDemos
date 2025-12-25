create database PersonDb;

use PersonDb;

create table People
(
   Id int primary key auto_increment,
   FirstName varchar(30) not null, 
   LastName varchar(30) not null
);

insert into People(FirstName, LastName)
values ('John','Doe'), 
('Ravindra','Devrani'), 
('Jane', 'Doe');