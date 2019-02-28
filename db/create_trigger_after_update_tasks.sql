CREATE TRIGGER Tasks_AFTER_UDPATE
	ON Tasks AFTER UPDATE
	AS
		DECLARE @newType VARCHAR(10);
		DECLARE @newStatus VARCHAR(10);
		DECLARE @newName VARCHAR(30);
		SET @newStatus = (SELECT Status FROM INSERTED);
		SET @newType = (SELECT Type FROM INSERTED);
		SET @newName = (SELECT Name FROM INSERTED);

		DECLARE @oldStatus VARCHAR(10);
		DECLARE @oldName VARCHAR(30);
		DECLARE @oldType VARCHAR(10);
		SET @oldStatus = (SELECT Status FROM DELETED);
		SET @oldType = (SELECT Type FROM DELETED);
		SET @oldName = (SELECT Name FROM DELETED);

		IF @newStatus != @oldStatus  
			INSERT INTO TaskChanges (TaskId, Operation, ChangedAt, OldValue, NewValue) 
				VALUES((SELECT Id FROM INSERTED), 'Updated status', CURRENT_TIMESTAMP, @oldStatus, @newStatus);

		IF @newType != @oldType
			INSERT INTO TaskChanges (TaskId, Operation, ChangedAt, OldValue, NewValue) 
				VALUES((SELECT Id FROM INSERTED), 'Updated type', CURRENT_TIMESTAMP, @oldType, @newType);

		IF @newName!= @oldName
			INSERT INTO TaskChanges (TaskId, Operation, ChangedAt, OldValue, NewValue) 
				VALUES((SELECT Id FROM INSERTED), 'Updated name', CURRENT_TIMESTAMP, @oldName, @newName);