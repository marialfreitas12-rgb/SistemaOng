create database dbBooking
go
Create table Animal(
IdAnimal int identity(1,1) primary key,
Nome varchar (100) not null,
Sexo varchar (10) not null,
Idade int ,
Dataentrada DATE not null,
Datasaida DATE not null,
Status varchar (100) not null,
Porte varchar (8) ,
Raça varchar (100) not null

);