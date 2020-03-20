IF NOT EXISTS(SELECT [Id] FROM [dbo].[Task])
BEGIN
	INSERT INTO [dbo].[Task] 
	(
		[Title],
		[Description],
		[DueDate],
        [CreatedDate],
		[UpdatedDate],
		[AssignedUserId],
		[CreatedUserId],
        [PriorityId],
        [ProjectId],
        [StatusId]
	)
	VALUES
      ('Task_1', 'Test desc for task_1', '2020-03-30', '2020-03-19', '2020-01-19', 6, 2, 1, 2, 3),
      ('Task_2', 'Test desc for task_2', '2020-03-30', '2020-03-19', '2020-01-19', 14, 2, 2, 3, 3),
      ('Task_3', 'Test desc for task_3', '2020-03-30', '2020-03-19', '2020-01-19', 17, 2, 4, 4, 3),
      ('Task_4', 'Test desc for task_4', '2020-03-30', '2020-03-19', '2020-01-19', 6, 2, 1, 5, 3),
      ('Task_5', 'Test desc for task_5', '2020-03-30', '2020-03-19', '2020-01-19', 14, 2, 2, 6, 3),
      ('Task_6', 'Test desc for task_6', '2020-03-30', '2020-03-19', '2020-01-19', 17, 2, 4, 7, 3),
      ('Task_7', 'Test desc for task_7', '2020-03-30', '2020-03-19', '2020-01-19', 6, 2, 1, 8, 3),
      ('Task_8', 'Test desc for task_8', '2020-03-30', '2020-03-19', '2020-01-19', 14, 2, 2, 9, 3),
      ('Task_9', 'Test desc for task_9', '2020-03-30', '2020-03-19', '2020-01-19', 17, 2, 4, 10, 3),
      ('Task_10', 'Test desc for task_10', '2020-03-30', '2020-03-19', '2020-01-19', 6, 2, 1, 11, 3),
      ('Task_11', 'Test desc for task_11', '2020-03-30', '2020-03-19', '2020-01-19', 14, 2, 2, 12, 3)
END;