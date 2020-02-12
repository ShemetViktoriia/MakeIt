IF NOT EXISTS(SELECT [Id] FROM [dbo].[Project])
BEGIN
	INSERT INTO [dbo].[Project] 
	(
		[Name],
		[Description],
		[CreatedDate],
		[CreatedBy],
		[UpdatedDate],
		[UpdatedBy],
		[OwnerId],
		[IsClosed]
	)
	VALUES
	  ('Project_1', 'Description_1', '2020-01-01', NULL, '2020-01-01', NULL, 2, 1	),
      ('Project_2', 'Description_2', '2020-01-02', NULL, '2020-01-02', NULL, 2, 0	),
      ('Project_3', 'Description_3', '2020-01-03', NULL, '2020-01-03', NULL, 2, 1	),
      ('Project_4', 'Description_4', '2020-01-04', NULL, '2020-01-04', NULL, 2, 0	),
      ('Project_5', 'Description_5', '2020-01-05', NULL, '2020-01-05', NULL, 2, 1	),
      ('Project_6', 'Description_6', '2020-01-06', NULL, '2020-01-06', NULL, 2, 0	),
      ('Project_7', 'Description_7', '2020-01-07', NULL, '2020-01-07', NULL, 2, 1	),
      ('Project_8', 'Description_8', '2020-01-08', NULL, '2020-01-08', NULL, 2, 0	),
      ('Project_9', 'Description_9', '2020-01-09', NULL, '2020-01-09', NULL, 2, 1	),
      ('Project_10', 'Description_10', '2020-01-10', NULL, '2020-01-10', NULL, 2, 0	),
      ('Project_11', 'Description_11', '2020-01-11', NULL, '2020-01-11', NULL, 2, 1	),
      ('Project_12', 'Description_12', '2020-01-12', NULL, '2020-01-12', NULL, 2, 0	),
      ('Project_13', 'Description_13', '2020-01-13', NULL, '2020-01-13', NULL, 2, 1	),
      ('Project_14', 'Description_14', '2020-01-14', NULL, '2020-01-14', NULL, 2, 0	),
      ('Project_15', 'Description_15', '2020-01-15', NULL, '2020-01-15', NULL, 2, 1	),
      ('Project_16', 'Description_16', '2020-01-16', NULL, '2020-01-16', NULL, 2, 0	),
      ('Project_17', 'Description_17', '2020-01-17', NULL, '2020-01-17', NULL, 2, 1	),
      ('Project_18', 'Description_18', '2020-01-18', NULL, '2020-01-18', NULL, 2, 0	),
      ('Project_19', 'Description_19', '2020-01-19', NULL, '2020-01-19', NULL, 2, 1	),
      ('Project_20', 'Description_20', '2020-01-20', NULL, '2020-01-20', NULL, 2, 0	),
      ('Project_21', 'Description_21', '2020-01-21', NULL, '2020-01-21', NULL, 2, 1	),
      ('Project_22', 'Description_22', '2020-01-22', NULL, '2020-01-22', NULL, 2, 0	),
      ('Project_23', 'Description_23', '2020-01-23', NULL, '2020-01-23', NULL, 2, 1	),
      ('Project_24', 'Description_24', '2020-01-24', NULL, '2020-01-24', NULL, 2, 0	),
      ('Project_25', 'Description_25', '2020-01-25', NULL, '2020-01-25', NULL, 2, 1	),
      ('Project_26', 'Description_26', '2020-01-26', NULL, '2020-01-26', NULL, 2, 0	),
      ('Project_27', 'Description_27', '2020-01-27', NULL, '2020-01-27', NULL, 2, 1	),
      ('Project_28', 'Description_28', '2020-01-28', NULL, '2020-01-28', NULL, 2, 0	),
      ('Project_29', 'Description_29', '2020-01-29', NULL, '2020-01-29', NULL, 2, 1	),
      ('Project_30', 'Description_30', '2020-01-30', NULL, '2020-01-30', NULL, 2, 0	)
END;