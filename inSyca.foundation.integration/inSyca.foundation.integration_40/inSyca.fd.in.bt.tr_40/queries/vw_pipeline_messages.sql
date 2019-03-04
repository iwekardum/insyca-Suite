SELECT        TOP (100) PERCENT BizTalkDTADb.dbo.dtav_FindMessageFacts.[MessageInstance/InstanceID] AS messageinstanceid, 
                         BizTalkDTADb.dbo.dtav_FindMessageFacts.[ServiceInstance/InstanceID] AS serviceinstanceid, BizTalkDTADb.dbo.dtav_FindMessageFacts.[ServiceInstance/ActivityID] AS activityid, 
                         BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/Timestamp] AS timestamp, BizTalkDTADb.dbo.dtav_ServiceFacts.[Service/Type] AS servicetype, 
                         BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/Direction] AS direction, BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/Adapter] AS adapter, BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/Port] AS port, 
                         BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/URL] AS url, BizTalkDTADb.dbo.dtav_ServiceFacts.[Service/Name] AS servicename, BizTalkDTADb.dbo.dtav_ServiceFacts.[ServiceInstance/Host] AS hostname, 
                         BizTalkDTADb.dbo.dtav_ServiceFacts.[ServiceInstance/StartTime] AS starttime, BizTalkDTADb.dbo.dtav_ServiceFacts.[ServiceInstance/EndTime] AS endtime, 
                         BizTalkDTADb.dbo.dtav_ServiceFacts.[ServiceInstance/State] AS state, BizTalkDTADb.dbo.btsv_Tracking_Spool.imgContext, BizTalkDTADb.dbo.btsv_Tracking_Parts.imgPropBag, 
                         BizTalkDTADb.dbo.btsv_Tracking_Parts.imgPart, BizTalkDTADb.dbo.btsv_Tracking_Parts.nNumFragments, BizTalkDTADb.dbo.btsv_Tracking_Parts.uidPartID
FROM            BizTalkDTADb.dbo.dtav_FindMessageFacts LEFT OUTER JOIN
                         BizTalkDTADb.dbo.btsv_Tracking_Parts ON BizTalkDTADb.dbo.dtav_FindMessageFacts.[MessageInstance/InstanceID] = BizTalkDTADb.dbo.btsv_Tracking_Parts.uidMessageID LEFT OUTER JOIN
                         BizTalkDTADb.dbo.btsv_Tracking_Spool ON BizTalkDTADb.dbo.dtav_FindMessageFacts.[MessageInstance/InstanceID] = BizTalkDTADb.dbo.btsv_Tracking_Spool.uidMsgID LEFT OUTER JOIN
                         BizTalkDTADb.dbo.dtav_ServiceFacts ON BizTalkDTADb.dbo.dtav_ServiceFacts.[ServiceInstance/InstanceID] = BizTalkDTADb.dbo.dtav_FindMessageFacts.[ServiceInstance/InstanceID] AND 
                         BizTalkDTADb.dbo.dtav_ServiceFacts.[ServiceInstance/ActivityID] = BizTalkDTADb.dbo.dtav_FindMessageFacts.[ServiceInstance/ActivityID]
WHERE        (BizTalkDTADb.dbo.dtav_ServiceFacts.[Service/Type] = N'Pipeline') AND (BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/Timestamp] > '{0}' AND BizTalkDTADb.dbo.dtav_FindMessageFacts.[Event/Timestamp] <= '{1}')
