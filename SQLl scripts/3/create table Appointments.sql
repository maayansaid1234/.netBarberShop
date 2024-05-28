CREATE TABLE Appointments (Id int identity(1,1) primary key,
SchedulingDate dateTime,
AppointmentTime dateTime,UserId int foreign key 
references Users(Id)) 