create table Deduction (
	ID varchar(8) primary key not null,
	Supervisior float not null,
	Accident float not null,
	SuperComment nvarchar(30) not null,
	AccidentComment nvarchar(30) not null
)
go