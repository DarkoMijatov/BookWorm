create database Library
collate  Serbian_Latin_100_CI_AS
go

use Library
go

create table Books(
	ID int identity(1,1) not null,
	Title nvarchar(60) not null,
	AuthorID int not null,
	PublisherID int not null,
	Year int not null,
	GenreID int not null,
	Language nvarchar(30),
	IsRead int not null, /*0-no, 1-yes*/
	Description nvarchar(300)
)
go

drop table Books

create table Authors(
	ID int identity(1,1) not null,
	Name nvarchar(60) not null,
	Country nvarchar(30)
)
go

create table Publishers(
	ID int identity(1,1) not null,
	Publisher nvarchar(60) not null,
	Country nvarchar(30)
)
go

create table Genres(
	ID int identity(1,1) not null,
	Genre nvarchar(30) not null
)
go

insert into Genres values('Akcioni')
insert into Genres values('Autobiografija')
insert into Genres values('Avanturistički')
insert into Genres values('Biografija')
insert into Genres values('Bojanke')
insert into Genres values('Dečije knjige')
insert into Genres values('Domaći autori')
insert into Genres values('Drama')
insert into Genres values('E-knjige')
insert into Genres values('Edukativni')
insert into Genres values('Enciklopedija')
insert into Genres values('Epska fantastika')
insert into Genres values('Erotski')
insert into Genres values('Fantastika')
insert into Genres values('Filozofija')
insert into Genres values('Horor')
insert into Genres values('Interaktivna knjiga')
insert into Genres values('Internet i računari')
insert into Genres values('Istorija')
insert into Genres values('Istorijski triler')
insert into Genres values('Klasici')
insert into Genres values('Knjige za decu')
insert into Genres values('Komedija')
insert into Genres values('Komična fantastika')
insert into Genres values('Kriminalistički')
insert into Genres values('Kuvari')
insert into Genres values('Ljubavni')
insert into Genres values('Marketing i menadžment')
insert into Genres values('Mitologije')
insert into Genres values('Muzika')
insert into Genres values('Naučna fantastika')
insert into Genres values('Poezija')
insert into Genres values('Popularna nauka')
insert into Genres values('Popularna psihologija')
insert into Genres values('Priče')
insert into Genres values('Psihologija')
insert into Genres values('Publicistika')
insert into Genres values('Putopisi')
insert into Genres values('Slikovnice')
insert into Genres values('Sport')
insert into Genres values('Teorije zavere')
insert into Genres values('Tinejdž')
insert into Genres values('Trileri')
insert into Genres values('Umetnost')