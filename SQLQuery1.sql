IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Task_1')
CREATE DATABASE Task_1
GO

USE Task_1
GO

IF EXISTS (SELECT name FROM sys.sysobjects WHERE name = 'Employees')
drop table Employees
IF EXISTS (SELECT name FROM sys.sysobjects WHERE name = 'Sector')
drop table Sector

create table Sector
(
--Creating columns in the table. CriminalChargeID is primary key and its value starts from 1 and is incremented each time by 1
SectorID int primary key identity(1,1),
SectorName nvarchar(50),
)

create table Employees
(
--Creating columns in the table. CriminalChargeID is primary key and its value starts from 1 and is incremented each time by 1
EmployeeID int primary key identity(1,1),
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,
DateOfBirth DateTime Check (year(GetDate()) - year(DateOfBirth) >= 16) not null,
IDNumber nvarchar(9) Check (LEN(IDNumber) = 9 and ISNUMERIC(IDNumber) = 1) not null,
JMBG nvarchar(13) Check (LEN(JMBG) = 13 and ISNUMERIC(JMBG) = 1) not null,
Gender nvarchar (10) Check (UPPER(Gender) = 'MALE' or UPPER(Gender) = 'FEMALE' or UPPER(Gender) = 'OTHER') not null,
PhoneNumber nvarchar (30) not null,
SectorID int foreign key references Sector(SectorID) not null,
Location nvarchar(50) not null,
Manager nvarchar (100),
)