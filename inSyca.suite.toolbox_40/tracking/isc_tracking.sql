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
