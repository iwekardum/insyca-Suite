USE [master]
GO

/****** CREATE DATABASE ******/
CREATE DATABASE [isc_tracking]
 CONTAINMENT = NONE
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [isc_tracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

/****** CREATE TABLES ******/
USE [isc_tracking]
GO

CREATE TABLE [dbo].[isc_port_names](
	[name] [nvarchar](256) NOT NULL,
	[friendlyname] [nvarchar](256) NULL,
 CONSTRAINT [PK_isc_port_names] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[isc_pipeline_messages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[messageinstanceid] [uniqueidentifier] NOT NULL,
	[serviceinstanceid] [uniqueidentifier] NOT NULL,
	[activityid] [uniqueidentifier] NOT NULL,
	[interchangeid] [uniqueidentifier] NULL,
	[timestamp] [datetime] NULL,
	[servicetype] [nvarchar](256) NULL,
	[direction] [nvarchar](256) NULL,
	[adapter] [nvarchar](256) NULL,
	[port] [nvarchar](256) NULL,
	[url] [nvarchar](1024) NULL,
	[servicename] [nvarchar](256) NULL,
	[hostname] [nvarchar](256) NULL,
	[starttime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[state] [nvarchar](256) NULL,
	[context] [nvarchar](max) NULL,
	[propbag] [nvarchar](max) NULL,
	[part] [nvarchar](max) NULL,
	[nNumFragments] [int] NULL,
	[uidPartID] [uniqueidentifier] NULL,
 CONSTRAINT [pk_isc_pipeline_messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Index [idx_time_direction]    Script Date: 02.11.2018 18:54:22 ******/
CREATE NONCLUSTERED INDEX [idx_time_direction] ON [dbo].[isc_pipeline_messages]
(
	[id] ASC,
	[timestamp] DESC,
	[direction] ASC
)
INCLUDE ( 	[interchangeid],
	[port],
	[url],
	[hostname],
	[starttime],
	[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE NONCLUSTERED INDEX [idx_port] ON [dbo].[isc_pipeline_messages]
(
	[port] ASC,
	[timestamp] ASC,
	[direction] ASC
)
INCLUDE ( 	[interchangeid],
	[url],
	[hostname],
	[starttime],
	[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


/****** Object:  Index [idx_interchange]    Script Date: 02.11.2018 18:54:55 ******/
CREATE NONCLUSTERED INDEX [idx_interchange] ON [dbo].[isc_pipeline_messages]
(
	[id] ASC,
	[interchangeid] ASC,
	[direction] ASC
)
INCLUDE ( 	[timestamp],
	[port],
	[url],
	[hostname],
	[starttime],
	[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

/****** Object:  Index [idx_id]    Script Date: 02.11.2018 18:55:09 ******/
CREATE NONCLUSTERED INDEX [idx_id] ON [dbo].[isc_pipeline_messages]
(
	[id] ASC
)
INCLUDE ( 	[interchangeid],
	[timestamp],
	[direction],
	[port],
	[url],
	[hostname],
	[starttime],
	[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

/****** CREATE FULLTEXT CATALOG ******/
USE [isc_tracking]
GO

CREATE FULLTEXT CATALOG isc_tracking;  
GO 

CREATE FULLTEXT INDEX ON dbo.isc_pipeline_messages   
 (servicetype, port, url, servicename, hostname, context, propbag, part) KEY INDEX pk_isc_pipeline_messages  
 ON isc_tracking;
GO

ALTER FULLTEXT INDEX ON dbo.isc_pipeline_messages ENABLE; 
GO 
ALTER FULLTEXT INDEX ON dbo.isc_pipeline_messages START FULL POPULATION;
GO

/****** CREATE STORED PROCEDURES ******/
USE [isc_tracking]
GO

CREATE PROCEDURE [dbo].[isc_get_timestamp]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT MAX([timestamp]) 
	FROM [dbo].[isc_pipeline_messages]
END
GO

CREATE PROCEDURE [dbo].[isc_select_detail] 
	-- Add the parameters for the stored procedure here
	@id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT TOP (1) [id]
      ,[interchangeid]
      ,[timestamp]
      ,[direction]
      ,[port]
      ,[url]
      ,[hostname]
      ,[starttime]
      ,[endtime]
      ,[context]
      ,[propbag]
      ,[part]
  FROM [dbo].[isc_pipeline_messages]
  WHERE id = @id
  END
GO

CREATE PROCEDURE [dbo].[isc_select_fieldnames] 
	-- Add the parameters for the stored procedure here
	@date_from datetime = '2000-01-01', 
	@date_to datetime = '2000-01-01',
	@searchvalue nvarchar(20) = '%%',
	@direction nvarchar(10) = '%%',
	@port nvarchar(50) = '%%',
	@resultrows int = 100
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@date_from = '2000-01-01')
			SET @date_from = DATEADD(day, -5, GETDATE())

	IF(@date_to = '2000-01-01')
			SET @date_to = GETDATE()

    -- Insert statements for procedure here
SELECT TOP (@resultrows) [id]
      ,[interchangeid]
--      ,[timestamp]
      ,[direction]
      ,[port]
      ,[url]
      ,[hostname]
      ,[starttime]
      ,[endtime]
--      ,[context]
--      ,[propbag]
--      ,[part]
  FROM [dbo].[isc_pipeline_messages]
  WHERE 
  ([timestamp] BETWEEN @date_from AND @date_to) 
  AND [port] LIKE @port
  AND [direction] LIKE @direction
  ORDER BY timestamp DESC
  END
  GO

  CREATE PROCEDURE [dbo].[isc_select_contains] 
	-- Add the parameters for the stored procedure here
	@date_from datetime = '2000-01-01', 
	@date_to datetime = '2000-01-01',
	@searchvalue nvarchar(20) = '%%',
	@direction nvarchar(10) = '%%',
	@port nvarchar(10) = '%%',
	@resultrows int = 100
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@date_from = '2000-01-01')
			SET @date_from = DATEADD(day, -5, GETDATE())

	IF(@date_to = '2000-01-01')
			SET @date_to = GETDATE()

    -- Insert statements for procedure here
SELECT TOP (@resultrows) [id]
      ,[interchangeid]
--      ,[timestamp]
      ,[direction]
      ,[port]
      ,[url]
      ,[hostname]
      ,[starttime]
      ,[endtime]
--      ,[context]
--      ,[propbag]
--      ,[part]
  FROM [dbo].[isc_pipeline_messages]
  WHERE timestamp BETWEEN @date_from AND @date_to AND 
  CONTAINS((part,propbag,context), @searchvalue) 
  AND ( @direction = '%%' or [direction] = @direction)
  ORDER BY timestamp DESC
  END
  GO

CREATE PROCEDURE [dbo].[isc_select_freetext] 
	-- Add the parameters for the stored procedure here
	@date_from datetime = '2000-01-01', 
	@date_to datetime = '2000-01-01',
	@searchvalue nvarchar(20) = '%%',
	@direction nvarchar(10) = '%%',
	@resultrows int = 100
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@date_from = '2000-01-01')
			SET @date_from = DATEADD(day, -5, GETDATE())

	IF(@date_to = '2000-01-01')
			SET @date_to = GETDATE()

    -- Insert statements for procedure here
SELECT TOP (@resultrows) [id]
      ,[interchangeid]
--      ,[timestamp]
      ,[direction]
      ,[port]
      ,[url]
      ,[hostname]
      ,[starttime]
      ,[endtime]
--      ,[context]
--      ,[propbag]
--      ,[part]
  FROM [dbo].[isc_pipeline_messages]
  WHERE timestamp BETWEEN @date_from AND @date_to AND 
  FREETEXT((part,propbag,context), @searchvalue) 
  AND ( @direction = '%%' or [direction] = @direction)
  ORDER BY timestamp DESC
  END
  GO

  CREATE PROCEDURE [dbo].[isc_select_initial] 
	-- Add the parameters for the stored procedure here
	@date_from datetime = '2000-01-01', 
	@date_to datetime = '2000-01-01',
	@searchvalue nvarchar(20) = '%%',
	@direction nvarchar(10) = '%%',
	@resultrows int = 50
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT TOP (50) [id]
      ,[messageinstanceid]
      ,[serviceinstanceid]
      ,[activityid]
      ,[interchangeid]
      ,[timestamp]
      ,[servicetype]
      ,[direction]
      ,[adapter]
      ,[port]
      ,[url]
      ,[servicename]
      ,[hostname]
      ,[starttime]
      ,[endtime]
      ,[state]
      ,[context]
      ,[propbag]
      ,[part]
      ,[nNumFragments]
      ,[uidPartID]
  FROM [dbo].[isc_pipeline_messages] ORDER BY [timestamp] DESC
  END
  GO

  CREATE PROCEDURE [dbo].[isc_select_port] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM dbo.isc_port_names
	ORDER BY dbo.isc_port_names.friendlyname
END
GO

  CREATE PROCEDURE [dbo].[isc_select_related] 
	-- Add the parameters for the stored procedure here
	@interchangeid uniqueidentifier = '',
	@direction nvarchar(10) = '%%'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT TOP 100 [id]
      ,[interchangeid]
      ,[timestamp]
      ,[direction]
      ,[port]
      ,[url]
      ,[hostname]
      ,[starttime]
      ,[endtime]
  FROM [dbo].[isc_pipeline_messages]
  WHERE interchangeid = @interchangeid 
  AND ( @direction = '%%' or [direction] = @direction)
  ORDER BY [timestamp] DESC
  END
GO

CREATE PROCEDURE [dbo].[isc_del_old_messages] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[isc_pipeline_messages] WHERE [timestamp] < DATEADD(dd, -20, GETDATE())
END
GO

CREATE PROCEDURE [dbo].[isc_get_messagecount] 
	@date_min		datetime,
	@date_max		datetime,
	@item			xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT T.C.value('.', 'NVARCHAR(256)') AS [Name]
    INTO #tblArtefactParams
    FROM @item.nodes('/item/Name') as T(C)

    -- Insert statements for procedure here
SELECT 
      A.port,
	  MIN(A.timestamp) as date_min,
	  MAX(A.timestamp) as date_max,
	  Count(*)as messages
   
  FROM 
  (SELECT port, timestamp FROM [isc_pipeline_messages] WHERE timestamp Between @date_min and @date_max AND port IN (SELECT Name FROM #tblArtefactParams ))as A
  GROUP BY A.port
  END
  GO

CREATE PROCEDURE [dbo].[isc_update_ports] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [isc_port_names] (name)
	SELECT DISTINCT port FROM dbo.isc_pipeline_messages
EXCEPT
	Select name from isc_port_names
END