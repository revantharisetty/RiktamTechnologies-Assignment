CREATE TABLE Groups (
    GroupId int identity(1,1) PRIMARY KEY,
	GroupName varchar(200),
	CreatedDate datetime,
	CreatedBy int    ,
);

