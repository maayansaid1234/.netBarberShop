CREATE TABLE Appointments (Id int identity(1,1) primary key,
SchedulingDate dateTime default SYSDATETIME() ,
AppointmentTime dateTime UNIQUE not null,UserId int foreign key
references Users(Id),HaircutId int  foreign key
references Haircuts(Id) ) 
