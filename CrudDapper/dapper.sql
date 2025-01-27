use dbEmployees
go

create table Employee(
	IdEmployee int primary key identity(1,1),
	DocumentNumber varchar(10),
	FullName varchar(50),
	Salary int
)

select * from Employee
go
----------------------------------------------------------------------------
create procedure sp_getEmployees
as
begin
	select
	IdEmployee,
	DocumentNumber,
	FullName,
	Salary
	from Employee
end
go
----------------------------------------------------------------------------
create procedure sp_getEmployee(
@idEmployee int
)
as
begin
	select
	IdEmployee,
	DocumentNumber,
	FullName,
	Salary
	from Employee
	where IdEmployee = @idEmployee
end
go
----------------------------------------------------------------------------
create procedure sp_createEmployee(
@documentNumber varchar(10),
@fullName varchar(50),
@salary int,
@msgError varchar(100) output
)
as
begin
	if(exists(select IdEmployee from Employee where DocumentNumber = @documentNumber))
	begin
		set @msgError = 'El numero de documento ya esta registrado'
		return
	end

	insert into Employee(DocumentNumber, FullName, Salary)
	values (@documentNumber, @fullName, @salary)
	set @msgError = ''

end
go
----------------------------------------------------------------------------
create procedure sp_updateEmployee(
@idEmployee int,
@documentNumber varchar(10),
@fullName varchar(50),
@salary int,
@msgError varchar(100) output
)
as 
begin

	if(exists(select * from Employee
		where DocumentNumber = @documentNumber and IdEmployee != @idEmployee
	))
	begin 
		set @msgError = 'El numero de documento ya esta registrado'
		return
	end

	update Employee set
	DocumentNumber = @documentNumber,
	FullName = @fullName,
	Salary = @salary
	where IdEmployee = IdEmployee

	set @msgError = ''
end
go
----------------------------------------------------------------------------
create procedure sp_deleteEmployee(
@idEmployee int
)
as
begin
	delete from Employee where IdEmployee = @idEmployee
end
go

select * from Employee



