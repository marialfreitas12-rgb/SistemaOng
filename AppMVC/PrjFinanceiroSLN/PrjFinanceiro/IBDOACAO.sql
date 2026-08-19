create database dBooking
go
Create table Doacao (
IdDoacao int identity(1,1) primary key,
DataDoacao varchar(20) not null,
Status varchar(50),
IdAnimal int,
IdPessoa int
)


