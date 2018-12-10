use TeacherReward
go

create table TeacherCrosswiseTaskInfo (
	ID varchar(8) primary key not null,
	CTaskName nvarchar(25) not null,
	CTaskLevel nvarchar(20) not null,
	CTaskStartTime nvarchar(20) not null,
	CTaskEndTime nvarchar(20) not null,
	CTaskComment nvarchar(30) not null
)
go

create table TeacherPrizeInfo (
	ID varchar(8) primary key not null,
	Prize1Name nvarchar(20) not null,
	Prize1Level nvarchar(5) not null,
	Prize1Rank nvarchar(5) not null,
	Prize1Comment nvarchar(30) not null,
	Prize2Name nvarchar(20) not null,
	Prize2Level nvarchar(5) not null,
	Prize2Comment nvarchar(30) not null,
	Prize2Rank nvarchar(5) not null,
	Prize3Name nvarchar(20) not null,
	Prize3Level nvarchar(5) not null,
	Prize3Comment nvarchar(30) not null,
	Prize3Rank nvarchar(5) not null,
	Prize4Name nvarchar(20) not null,
	Prize4Level nvarchar(5) not null,
	Prize4Rank nvarchar(5) not null,
	Prize4Comment nvarchar(30) not null
)
go

create table TeacherProjectInfo (
	ID varchar(8) primary key not null,
	Project1Name nvarchar(25) not null,
	Project1Level nvarchar(5) not null,
	Project1StartTime nvarchar(20) not null,
	Project1EndTime nvarchar(20) not null,
	Project1Role nvarchar(10) not null,
	Project1Comment nvarchar(30) not null,
	Project2Name nvarchar(25) not null,
	Project2Level nvarchar(5) not null,
	Project2StartTime nvarchar(20) not null,
	Project2EndTime nvarchar(20) not null,
	Project2Role nvarchar(10) not null,
	Project2Comment nvarchar(30) not null,
	Project3Name nvarchar(25) not null,
	Project3Level nvarchar(5) not null,
	Project3StartTime nvarchar(20) not null,
	Project3EndTime nvarchar(20) not null,
	Project3Role nvarchar(10) not null,
	Project3Comment nvarchar(30) not null,
	Project4Name nvarchar(25) not null,
	Project4Level nvarchar(5) not null,
	Project4StartTime nvarchar(20) not null,
	Project4EndTime nvarchar(20) not null,
	Project4Role nvarchar(10) not null,
	Project4Comment nvarchar(30) not null
)
go

create table TeacherPublishThesisInfo (
	ID varchar(8) primary key not null,
	PublishThesis1Name nvarchar(25) not null,
	PublishThesis1Time nvarchar(20) not null,
	PublishThesis1Type nvarchar(20) not null,
	PublishThesis1Author nvarchar(10) not null,
	PublishThesis1Comment nvarchar(30) not null,
	PublishThesis2Name nvarchar(25) not null,
	PublishThesis2Time nvarchar(20) not null,
	PublishThesis2Type nvarchar(20) not null,
	PublishThesis2Author nvarchar(10) not null,
	PublishThesis2Comment nvarchar(30) not null,
	PublishThesis3Name nvarchar(25) not null,
	PublishThesis3Time nvarchar(20) not null,
	PublishThesis3Type nvarchar(20) not null,
	PublishThesis3Author nvarchar(10) not null,
	PublishThesis3Comment nvarchar(30) not null,
	PublishThesis4Name nvarchar(25) not null,
	PublishThesis4Time nvarchar(20) not null,
	PublishThesis4Type nvarchar(20) not null,
	PublishThesis4Author nvarchar(10) not null,
	PublishThesis4Comment nvarchar(30) not null
)
go

create table TeacherTaskInfo (
	ID varchar(8) primary key not null,
	Task1Name nvarchar(20) not null,
	Task1StartTime nvarchar(20) not null,
	Task1EndTime nvarchar(20) not null,
	Task1Level nvarchar(5) not null,
	Task1Role nvarchar(10) not null,
	Task1Comment nvarchar(30) not null,
	Task2Name nvarchar(20) not null,
	Task2StartTime nvarchar(20) not null,
	Task2EndTime nvarchar(20) not null,
	Task2Level nvarchar(5) not null,
	Task2Role nvarchar(10) not null,
	Task2Comment nvarchar(30) not null,
	Task3Name nvarchar(20) not null,
	Task3StartTime nvarchar(20) not null,
	Task3EndTime nvarchar(20) not null,
	Task3Level nvarchar(5) not null,
	Task3Role nvarchar(10) not null,
	Task3Comment nvarchar(30) not null,
	Task4Name nvarchar(20) not null,
	Task4StartTime nvarchar(20) not null,
	Task4EndTime nvarchar(20) not null,
	Task4Level nvarchar(5) not null,
	Task4Role nvarchar(10) not null,
	Task4Comment nvarchar(30) not null
)
go

create table TeacherTeachThesisInfo (
	ID varchar(8) primary key not null,
	TeachThesis1Type nvarchar(5) not null,
	TeachThesis1Author nvarchar(10) not null,
	TeachThesis1Name nvarchar(25) not null,
	TeachThesis1Publisher nvarchar(25) not null,
	TeachThesis1Comment nvarchar(30) not null,
	TeachThesis2Type nvarchar(5) not null,
	TeachThesis2Author nvarchar(10) not null,
	TeachThesis2Name nvarchar(25) not null,
	TeachThesis2Publisher nvarchar(25) not null,
	TeachThesis2Comment nvarchar(30) not null,
	TeachThesis3Type nvarchar(5) not null,
	TeachThesis3Author nvarchar(10) not null,
	TeachThesis3Name nvarchar(25) not null,
	TeachThesis3Publisher nvarchar(25) not null,
	TeachThesis3Comment nvarchar(30) not null,
	TeachThesis4Type nvarchar(5) not null,
	TeachThesis4Author nvarchar(10) not null,
	TeachThesis4Name nvarchar(25) not null,
	TeachThesis4Publisher nvarchar(25) not null,
	TeachThesis4Comment nvarchar(30) not null
)
go

create table TeachingAwardInfo (
	ID varchar(8) primary key not null,
	Comment nvarchar(30) not null
)
go
