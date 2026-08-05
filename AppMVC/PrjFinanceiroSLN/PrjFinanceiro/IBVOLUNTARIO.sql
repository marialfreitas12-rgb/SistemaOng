 Create database dbBooking
 go
 Create table voluntario(
 Idvoluntario int identity(1,1) primary key,
 Nome varchar(100) not null ,
 telefone varchar (20),
 email varchar (100),
 disponibilidade varchar (200)
 
 );
