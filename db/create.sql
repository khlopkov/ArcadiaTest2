USE arcadia_test;

CREATE TABLE Users (
	Id int PRIMARY KEY IDENTITY,
	Email VARCHAR(30) NOT NULL UNIQUE,
	Hash VARCHAR(64) NOT NULL,
	Name VARCHAR(30) NOT NULL
);

CREATE TABLE TaskStatus (
	Name VARCHAR(10) NOT NULL PRIMARY KEY
);

INSERT INTO TaskStatus (name) VALUES ('Active'), ('Cancelled'), ('Resolved');

CREATE TABLE Tasks (
	Id int PRIMARY KEY IDENTITY,
	Name VARCHAR(50) NOT NULL,
	Description TEXT,
	Type VARCHAR(10),
	DueDate DATE,
	Status VARCHAR(10) REFERENCES TaskStatus(Name)
);

CREATE TABLE TaskChanges (
	Id int PRIMARY KEY IDENTITY,
	TaskId int REFERENCES Tasks(id),
	Operation VARCHAR(15) NOT NULL,
	ChangedAt DATETIME NOT NULL,
	OldValue TEXT,
	NewValue TEXT
);

CREATE TABLE UserTasks (
	UserId INT REFERENCES Users(id),
	TaskId INT REFERENCES Tasks(id),
	PRIMARY KEY(UserId, TaskId)
);