Create table EmployeeData
(ID int primary key identity,
FirstName nvarchar(max),
LastName nvarchar(max),
Gender nvarchar(50),
Salary int)

insert into EmployeeData values('Mark1', 'Wang1', 'Male', 1234)
Go
insert into EmployeeData values('Mark2', 'Wang2', 'Male', 2345)
Go
insert into EmployeeData values('Mark3', 'Wang3', 'Male', 6666)
Go
insert into EmployeeData values('Mark4', 'Wang4', 'Male', 10000)
Go