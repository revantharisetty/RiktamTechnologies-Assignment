CREATE TABLE Users (
    UserId int identity(1,1) PRIMARY KEY,
    UserName varchar(255) NOT NULL,
    FirstName varchar(255),
	LastName varchar(255),
    MobileNumber int,
	UserPassword VarChar(255)
);
