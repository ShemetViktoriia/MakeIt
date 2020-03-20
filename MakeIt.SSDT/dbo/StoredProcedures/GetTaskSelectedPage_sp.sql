CREATE PROCEDURE [dbo].[GetTaskSelectedPage_sp]
 (
	@SearchValue NVARCHAR(50) = NULL,
	@PageNo INT = 1,
	@PageSize INT = 10,
	@SortColumn NVARCHAR(20) = 'Title',
	@SortOrder NVARCHAR(20) = 'ASC',
    @AssignedUserId INT = 0
)
 AS BEGIN
 SET NOCOUNT ON;

 SET @SearchValue = LTRIM(RTRIM(@SearchValue));
 
 declare @tablevariable table
(
   -- columns with same types as dynamic query   
   RecordsTotal INT,
   ID INT,
   Title NVARCHAR(100),
   [Description] NVARCHAR(500),
   DueDate DATETIME,
   [Priority] NVARCHAR(100),
   [Status] NVARCHAR(100),
   Project NVARCHAR(100),
   AssignedUser NVARCHAR(100),
   CreatedUser NVARCHAR(100)
);

 WITH CTE_Results AS 
(
    SELECT * from Task 
	WHERE (@SearchValue IS NULL OR Title LIKE '%' + @SearchValue + '%') AND (AssignedUserId = @AssignedUserId)
	 	    ORDER BY
   	 CASE WHEN (@SortColumn = 'Title' AND @SortOrder='ASC')
                    THEN Title
        END ASC,
        CASE WHEN (@SortColumn = 'Title' AND @SortOrder='DESC')
                   THEN Title
		END DESC,
	 CASE WHEN (@SortColumn = 'Description' AND @SortOrder='ASC')
                    THEN [Description]
        END ASC,
        CASE WHEN (@SortColumn = 'Description' AND @SortOrder='DESC')
                   THEN [Description]
		END DESC 
      OFFSET @PageNo ROWS
      FETCH NEXT @PageSize ROWS ONLY
	),
CTE_TotalRows AS 
(
 select count(Id) as RecordsTotal from Task WHERE (@SearchValue IS NULL OR Title LIKE '%' + @SearchValue + '%'  AND (AssignedUserId = @AssignedUserId))
)
   Insert into @tablevariable
   Select RecordsTotal,
          T.Id AS ID,
          T.Title AS Title,
          T.[Description] AS [Description],
          T.DueDate AS DueDate,
          P.[Name] AS [Priority],
          S.[Name] AS [Status],
          Pr.[Name] AS Project,
          UA.UserName AS AssignedUser,
          UC.UserName AS CreatedUser
   FROM CTE_TotalRows, [dbo].[Task] AS T
   JOIN [dbo].[User] AS UA ON UA.Id= T.AssignedUserId
   JOIN [dbo].[User] AS UC ON UC.Id= T.CreatedUserId
   JOIN [dbo].[Status] AS S ON T.StatusId=S.Id
   JOIN [dbo].[Priority] AS P ON T.PriorityId=P.Id
   JOIN [dbo].[Project] AS Pr ON T.ProjectId=Pr.Id
   WHERE EXISTS (SELECT 1 FROM CTE_Results WHERE CTE_Results.Id = T.Id)

   select * from @tablevariable
   RETURN

   END
GO