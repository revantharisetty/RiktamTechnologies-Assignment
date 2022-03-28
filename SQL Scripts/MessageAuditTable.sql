CREATE TABLE MessageAudit (
    AuditId int identity(1,1) PRIMARY KEY,
    UserId int,
	GroupId int,
	MessageText varchar(1024),
	AuditDate DateTime  ,
	LikeCount int,
	DisLikeCount int,
);
