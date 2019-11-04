USE [master]
GO
/****** Object:  Database [isc_tracking]    Script Date: 23.10.2019 09:59:46 ******/
CREATE DATABASE [isc_tracking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'isc_tracking', SIZE = 8388608KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1048576KB )
 LOG ON 
( NAME = N'isc_tracking_log', SIZE = 2097152KB , MAXSIZE = 2048GB , FILEGROWTH = 1048576KB )
GO
ALTER DATABASE [isc_tracking] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [isc_tracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [isc_tracking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [isc_tracking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [isc_tracking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [isc_tracking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [isc_tracking] SET ARITHABORT OFF 
GO
ALTER DATABASE [isc_tracking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [isc_tracking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [isc_tracking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [isc_tracking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [isc_tracking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [isc_tracking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [isc_tracking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [isc_tracking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [isc_tracking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [isc_tracking] SET  ENABLE_BROKER 
GO
ALTER DATABASE [isc_tracking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [isc_tracking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [isc_tracking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [isc_tracking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [isc_tracking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [isc_tracking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [isc_tracking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [isc_tracking] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [isc_tracking] SET  MULTI_USER 
GO
ALTER DATABASE [isc_tracking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [isc_tracking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [isc_tracking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [isc_tracking] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [isc_tracking] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [isc_tracking] SET QUERY_STORE = OFF
GO
USE [isc_tracking]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [isc_tracking]
GO
/****** Object:  FullTextCatalog [isc_tracking]    Script Date: 23.10.2019 09:59:46 ******/
CREATE FULLTEXT CATALOG [isc_tracking] WITH ACCENT_SENSITIVITY = ON
AS DEFAULT
GO
/****** Object:  PartitionFunction [ifts_comp_fragment_partition_function_3F1C4B12]    Script Date: 23.10.2019 09:59:46 ******/
CREATE PARTITION FUNCTION [ifts_comp_fragment_partition_function_3F1C4B12](varbinary(128)) AS RANGE LEFT FOR VALUES (0x00390062003600620035003300320033002D0036003600660033002D0034003600370061002D0061006100310039002D003600360030003200360038003300360038003700380030)
GO
/****** Object:  PartitionFunction [ifts_comp_fragment_partition_function_5AEE82B9]    Script Date: 23.10.2019 09:59:47 ******/
CREATE PARTITION FUNCTION [ifts_comp_fragment_partition_function_5AEE82B9](varbinary(128)) AS RANGE LEFT FOR VALUES (0x0039003100350035)
GO
/****** Object:  PartitionFunction [ifts_comp_fragment_partition_function_7740A8A4]    Script Date: 23.10.2019 09:59:47 ******/
CREATE PARTITION FUNCTION [ifts_comp_fragment_partition_function_7740A8A4](varbinary(128)) AS RANGE LEFT FOR VALUES (0x0039006200390032)
GO
/****** Object:  PartitionScheme [ifts_comp_fragment_data_space_3F1C4B12]    Script Date: 23.10.2019 09:59:47 ******/
CREATE PARTITION SCHEME [ifts_comp_fragment_data_space_3F1C4B12] AS PARTITION [ifts_comp_fragment_partition_function_3F1C4B12] TO ([PRIMARY], [PRIMARY])
GO
/****** Object:  PartitionScheme [ifts_comp_fragment_data_space_5AEE82B9]    Script Date: 23.10.2019 09:59:47 ******/
CREATE PARTITION SCHEME [ifts_comp_fragment_data_space_5AEE82B9] AS PARTITION [ifts_comp_fragment_partition_function_5AEE82B9] TO ([PRIMARY], [PRIMARY])
GO
/****** Object:  PartitionScheme [ifts_comp_fragment_data_space_7740A8A4]    Script Date: 23.10.2019 09:59:47 ******/
CREATE PARTITION SCHEME [ifts_comp_fragment_data_space_7740A8A4] AS PARTITION [ifts_comp_fragment_partition_function_7740A8A4] TO ([PRIMARY], [PRIMARY])
GO
/****** Object:  Table [dbo].[isc_pipeline_messages]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[isc_port_names]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Index [idx_id]    Script Date: 23.10.2019 09:59:47 ******/
CREATE NONCLUSTERED INDEX [idx_id] ON [dbo].[isc_pipeline_messages]
(
	[timestamp] DESC,
	[id] DESC
)
INCLUDE([interchangeid],[direction],[port],[url],[hostname],[starttime],[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_interchange]    Script Date: 23.10.2019 09:59:47 ******/
CREATE NONCLUSTERED INDEX [idx_interchange] ON [dbo].[isc_pipeline_messages]
(
	[timestamp] DESC,
	[id] DESC,
	[interchangeid] ASC,
	[direction] ASC
)
INCLUDE([port],[url],[hostname],[starttime],[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_port]    Script Date: 23.10.2019 09:59:47 ******/
CREATE NONCLUSTERED INDEX [idx_port] ON [dbo].[isc_pipeline_messages]
(
	[timestamp] ASC,
	[direction] ASC,
	[port] ASC
)
INCLUDE([interchangeid],[url],[hostname],[starttime],[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_time_direction]    Script Date: 23.10.2019 09:59:47 ******/
CREATE NONCLUSTERED INDEX [idx_time_direction] ON [dbo].[isc_pipeline_messages]
(
	[timestamp] DESC,
	[id] DESC,
	[direction] ASC
)
INCLUDE([interchangeid],[port],[url],[hostname],[starttime],[endtime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_friendlyname_sort]    Script Date: 23.10.2019 09:59:47 ******/
CREATE NONCLUSTERED INDEX [idx_friendlyname_sort] ON [dbo].[isc_port_names]
(
	[friendlyname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[isc_del_old_messages]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_get_messagecount]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_get_timestamp]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_select_contains]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_select_detail]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_select_fieldnames]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_select_freetext]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_select_initial]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[isc_select_initial] 
	-- Add the parameters for the stored procedure here
	@date_from datetime = '2000-01-01', 
	@date_to datetime = '2000-01-01',
	@searchvalue nvarchar(20) = '%%',
	@port nvarchar(20) = '%%',
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
/****** Object:  StoredProcedure [dbo].[isc_select_port]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_select_related]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[isc_update_ports]    Script Date: 23.10.2019 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
USE [master]
GO
ALTER DATABASE [isc_tracking] SET  READ_WRITE 
GO
