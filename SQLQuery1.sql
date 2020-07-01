IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Task_1')
CREATE DATABASE Task_1
GO

USE Task_1
GO

IF EXISTS (SELECT name FROM sys.sysobjects WHERE name = 'Employees')
drop table Employees
IF EXISTS (SELECT name FROM sys.sysobjects WHERE name = 'Sector')
drop table Sector
IF EXISTS (SELECT name FROM sys.sysobjects WHERE name = 'Location')
drop table Location
IF EXISTS (SELECT name FROM sys.sysobjects WHERE name = 'EmployeeView')
drop view EmployeeView

create table Location
(
--Creating columns in the table. CriminalChargeID is primary key and its value starts from 1 and is incremented each time by 1
LocationID int primary key identity(1,1),
Address nvarchar(50),
)

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
LocationID int foreign key references Location(LocationID) not null,
Manager nvarchar (100),
)

Use Task_1
go

CREATE VIEW EmployeeView
AS SELECT e.EmployeeID,e.FirstName+' '+e.LastName as EmployeeName, e.DateOfBirth, e.IDNumber, e.JMBG, e.Gender, e.PhoneNumber, s.SectorName, l.Address,e.Manager
FROM Employees e, Sector s, Location l
WHERE e.SectorID = s.SectorID
AND e.LocationID = l.LocationID;