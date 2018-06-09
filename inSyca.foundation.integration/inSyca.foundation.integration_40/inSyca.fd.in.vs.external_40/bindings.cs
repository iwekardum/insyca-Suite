using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace inSyca.foundation.integration.visualstudio.external
{
    public class bindings
    {
        public static void ProcessBinding(FileInfo selectedFile)
        {
            XDocument xBinding = XDocument.Load(selectedFile.FullName);

            Dictionary<string, XElement> list = new Dictionary<string, XElement>();

            ProcessModules(xBinding, list);

            ProcessSendPorts(xBinding, list);

            ProcessReceivePorts(xBinding, list);

            xBinding.Save(selectedFile.FullName);

            File.WriteAllLines(Path.Combine(selectedFile.DirectoryName, "settings.csv"), list.Select(s => string.Format(@"{0};=""{1}""", s.Key, s.Value.Value)));
        }

        private static void ProcessModules(XDocument xBinding, Dictionary<string, XElement> list)
        {
            var schemas = xBinding.Descendants("Schema");

            foreach (var schema in schemas)
            {
                schema.Attribute("AlwaysTrackAllProperties").Value = "${bt_schema_AlwaysTrackAllProperties}";
            }

            var services = xBinding.Descendants("Service");

            foreach (var service in services)
            {
                service.Attribute("State").Value = "${bt_orchestration_State}";
                service.Attribute("TrackingOption").Value = "${bt_orchestration_TrackingOption}";
            }

            var hosts = xBinding.Descendants("Host");

            foreach (var host in hosts)
            {
                XAttribute ntGroupName = host.Attribute("NTGroupName");

                if (ntGroupName != null)
                    ntGroupName.Remove();
            }
        }

        private static void ProcessReceivePorts(XDocument xBinding, Dictionary<string, XElement> list)
        {
            var receivePorts = xBinding.Descendants("ReceivePort").OrderBy(e => e.Descendants("ReceiveLocation").FirstOrDefault().Attribute("Name").Value);

            foreach (var receivePort in receivePorts)
            {
                receivePort.SetElementValue("Tracking", "${bt_rp_Tracking}");

                var receiveLocations = receivePort.Descendants("ReceiveLocation");

                foreach (var receiveLocation in receiveLocations)
                {
                    XElement receiveLocationTransportType = receiveLocation.Descendants("ReceiveLocationTransportType").FirstOrDefault();
                    XElement address = receiveLocation.Element("Address");

                    XElement receiveLocationTransportTypeData = receiveLocation.Descendants("ReceiveLocationTransportTypeData").FirstOrDefault();
                    XElement customProps = XElement.Parse(receiveLocationTransportTypeData.Value);

                    string receiveLocationTransportTypeName = receiveLocationTransportType.Attribute("Name").Value;
                    string receiveLocationName = receiveLocation.Attribute("Name").Value;

                    list.Add(string.Format("--- {0} ---", receiveLocationName.ToUpper()), new XElement("new_sendport"));
                    list.Add(string.Format("{0}_address", receiveLocationName), new XElement(address));

                    switch (receiveLocationTransportTypeName)
                    {
                        case "FILE":
                            {
                                ReceiveLocation_File(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;
                        case "FTP":
                            {
                                ReceiveLocation_FTP(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;

                        case "Windows SharePoint Services":
                            {
                                ReceiveLocation_Sharepoint(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;

                        case "WCF-Custom":
                        case "WCF-CustomIsolated":
                            {
                                ReceiveLocation_WCF_Custom(receiveLocation, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;
                        case "SB-Messaging":
                            {
                                ReceiveLocation_SB(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;
                        default:
                            {
                                address.Value = string.Format("${{{0}_address}}", receiveLocationName);
                            }
                            break;
                    }
                }

            }
        }

        private static void ProcessSendPorts(XDocument xBinding, Dictionary<string, XElement> list)
        {
            var sendPorts = xBinding.Descendants("SendPort").OrderBy(e => e.Attribute("Name").Value); ;

            foreach (var sendPort in sendPorts)
            {
                sendPort.SetElementValue("Tracking", "${bt_sp_Tracking}");

                XElement Filter = sendPort.Descendants("Filter").FirstOrDefault();

                if (Filter != null && !string.IsNullOrEmpty(Filter.Value))
                    Filter.Value = Filter.Value.Replace("\n", string.Empty).Trim();

                XElement primaryTransport = sendPort.Descendants("PrimaryTransport").FirstOrDefault();
                XElement address = primaryTransport.Element("Address");

                XElement transportType = primaryTransport.Descendants("TransportType").FirstOrDefault();
                XElement transportTypeData = primaryTransport.Descendants("TransportTypeData").FirstOrDefault();

                XElement customProps = XElement.Parse(transportTypeData.Value);

                string transportTypeName = transportType.Attribute("Name").Value;
                string sendPortName = sendPort.Attribute("Name").Value;

                list.Add(string.Format("--- {0} ---", sendPortName.ToUpper()), new XElement("new_sendport"));
                list.Add(string.Format("{0}_address", sendPortName), new XElement(address));

                switch (transportTypeName)
                {
                    case "FILE":
                        {
                            SendPort_File(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "FTP":
                        {
                            SendPort_FTP(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "Windows SharePoint Services":
                        {
                            SendPort_Sharepoint(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "WCF-Custom":
                    case "WCF-CustomIsolated":
                        {
                            SendPort_WCF_Custom(address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "SB-Messaging":
                        {
                            SendPort_SB(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    default:
                        address.Value = string.Format("${{{0}_address}}", sendPortName);
                        break;
                }
            }
        }

        private static void SendPort_SB(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_address}}", sendPortName);

            customProps.SetElementValue("SessionIdleTimeout", "${sb_sessionIdleTimeout}");
            customProps.SetElementValue("BatchFlushInterval", "${sb_batchFlushInterval}");
            customProps.SetElementValue("OpenTimeout", "${sb_openTimeout}");
            customProps.SetElementValue("CloseTimeout", "${sb_closeTimeout}");
            customProps.SetElementValue("SendTimeout", "${sb_sendTimeout}");
            customProps.SetElementValue("MaxReceivedMessageSize", "${sb_maxReceivedMessageSize}");

            XElement SharedAccessKeyName = customProps.Element("SharedAccessKeyName");
            // Doesn't work, breaks import of binding. Think it's because of the characters used for SAS
            XElement SharedAccessKey = customProps.Element("SharedAccessKey");

            if (SharedAccessKey != null)
                SharedAccessKey.Remove();
            //    SharedAccessKey = new XElement("SharedAccessKey");

            list.Add(string.Format("{0}_sharedAccessKeyName", sendPortName), new XElement(SharedAccessKeyName));
            //list.Add(string.Format("{0}_sharedAccessKey", sendPortName), new XElement(SharedAccessKey));

            customProps.SetElementValue("SharedAccessKeyName", string.Format("${{{0}_sharedAccessKeyName}}", sendPortName));
            //customProps.SetElementValue("SharedAccessKey", string.Format("${{{0}_sharedAccessKey}}", sendPortName));

            transportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void ReceiveLocation_SB(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("${{{0}_address}}", receiveLocationName);

            customProps.SetElementValue("SessionIdleTimeout", "${sb_sessionIdleTimeout}");
            customProps.SetElementValue("OpenTimeout", "${sb_openTimeout}");
            customProps.SetElementValue("CloseTimeout", "${sb_closeTimeout}");
            customProps.SetElementValue("SendTimeout", "${sb_sendTimeout}");
            customProps.SetElementValue("MaxReceivedMessageSize", "${sb_maxReceivedMessageSize}");

            XElement SharedAccessKeyName = customProps.Element("SharedAccessKeyName");
            XElement SharedAccessKey = customProps.Element("SharedAccessKey");

            if (SharedAccessKey != null)
                SharedAccessKey.Remove();

            list.Add(string.Format("{0}_sharedAccessKeyName", receiveLocationName), new XElement(SharedAccessKeyName));
            //list.Add(string.Format("{0}_sharedAccessKey", receiveLocationName), new XElement(SharedAccessKey));

            customProps.SetElementValue("SharedAccessKeyName", string.Format("${{{0}_sharedAccessKeyName}}", receiveLocationName));
            //customProps.SetElementValue("SharedAccessKey", string.Format("${{{0}_sharedAccessKey}}", receiveLocationName));

            receiveLocationTransportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void ReceiveLocation_WCF_Custom(XElement receiveLocation, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            receiveLocation.SetElementValue("Address", string.Format("${{{0}_address}}", receiveLocationName));

            XElement bindingConfiguration = customProps.Descendants("BindingConfiguration").FirstOrDefault();
            XElement binding = XElement.Parse(bindingConfiguration.Value);

            binding.SetAttributeValue("maxReceivedMessageSize", "${wcf_maxReceivedMessageSize}");
            binding.SetAttributeValue("maxBufferPoolSize", "${wcf_maxBufferPoolSize}");
            binding.SetAttributeValue("maxBufferSize", "${wcf_maxBufferSize}");
            binding.SetAttributeValue("closeTimeout", "${wcf_closeTimeout}");
            binding.SetAttributeValue("openTimeout", "${wcf_openTimeout}");
            binding.SetAttributeValue("receiveTimeout", "${wcf_receiveTimeout}");
            binding.SetAttributeValue("sendTimeout", "${wcf_sendTimeout}");

            bindingConfiguration.Value = binding.ToString();
            receiveLocationTransportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void ReceiveLocation_Sharepoint(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("${{{0}_uri}}", receiveLocationName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement SiteUrl = config.Element("SiteUrl");
            XElement WssLocation = config.Element("WssLocation");
            XElement ViewName = config.Element("ViewName");
            XElement uri = config.Element("uri");

            list.Add(string.Format("{0}_siteurl", receiveLocationName), new XElement(SiteUrl));
            list.Add(string.Format("{0}_wssLocation", receiveLocationName), new XElement(WssLocation));
            list.Add(string.Format("{0}_viewname", receiveLocationName), new XElement(ViewName));
            list.Add(string.Format("{0}_uri", receiveLocationName), new XElement(uri));

            SiteUrl.Value = string.Format("${{{0}_siteurl}}", receiveLocationName);
            WssLocation.Value = string.Format("${{{0}_wssLocation}}", receiveLocationName);
            ViewName.Value = string.Format("${{{0}_viewname}}", receiveLocationName);
            uri.Value = string.Format("${{{0}_uri}}", receiveLocationName);

            adapterConfig.Value = config.ToString();
            receiveLocationTransportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void ReceiveLocation_FTP(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverport}}/${{{0}_targetfolder}}/${{{0}_filemask}}", receiveLocationName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement serverAddress = config.Element("serverAddress");
            XElement serverPort = config.Element("serverPort");
            XElement fileMask = config.Element("fileMask");
            XElement targetFolder = config.Element("targetFolder");
            XElement ssoAffiliateApplication = config.Element("ssoAffiliateApplication");

            list[string.Format("{0}_address", receiveLocationName)].Value = list[string.Format("{0}_address", receiveLocationName)].Value.Replace(fileMask.Value, "");

            list.Add(string.Format("{0}_serveraddress", receiveLocationName), new XElement(serverAddress));
            list.Add(string.Format("{0}_serverport", receiveLocationName), new XElement(serverPort));
            list.Add(string.Format("{0}_filemask", receiveLocationName), new XElement(fileMask));
            list.Add(string.Format("{0}_targetfolder", receiveLocationName), new XElement(targetFolder));
            list.Add(string.Format("{0}_ssoaffiliateapplication", receiveLocationName), new XElement(ssoAffiliateApplication));

            config.SetElementValue("uri", string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverPort}}/${{{0}_targetFolder}}/${{{0}_filemask}}", receiveLocationName));
            config.SetElementValue("serverAddress", string.Format("${{{0}_serveraddress}}", receiveLocationName));
            config.SetElementValue("serverPort", string.Format("${{{0}_serverport}}", receiveLocationName));
            config.SetElementValue("fileMask", string.Format("${{{0}_filemask}}", receiveLocationName));
            config.SetElementValue("targetFolder", string.Format("${{{0}_targetfolder}}", receiveLocationName));
            config.SetElementValue("ssoAffiliateApplication", string.Format("${{{0}_ssoaffiliateapplication}}", receiveLocationName));

            adapterConfig.Value = config.ToString();
            receiveLocationTransportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void SendPort_WCF_Custom(XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_address}}", sendPortName);

            XElement bindingConfiguration = customProps.Descendants("BindingConfiguration").FirstOrDefault();
            XElement binding = XElement.Parse(bindingConfiguration.Value);

            binding.SetAttributeValue("maxReceivedMessageSize", "${wcf_maxReceivedMessageSize}");
            binding.SetAttributeValue("maxBufferPoolSize", "${wcf_maxBufferPoolSize}");
            binding.SetAttributeValue("maxBufferSize", "${wcf_maxBufferSize}");
            binding.SetAttributeValue("closeTimeout", "${wcf_closeTimeout}");
            binding.SetAttributeValue("openTimeout", "${wcf_openTimeout}");
            binding.SetAttributeValue("receiveTimeout", "${wcf_receiveTimeout}");
            binding.SetAttributeValue("sendTimeout", "${wcf_sendTimeout}");

            bindingConfiguration.Value = binding.ToString();
            transportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void SendPort_Sharepoint(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_uri}}", sendPortName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement SiteUrl = config.Element("SiteUrl");
            XElement WssLocation = config.Element("WssLocation");
            XElement FileName = config.Element("FileName");
            XElement uri = config.Element("uri");

            list.Add(string.Format("{0}_siteurl", sendPortName), new XElement(SiteUrl));
            list.Add(string.Format("{0}_wssLocation", sendPortName), new XElement(WssLocation));
            list.Add(string.Format("{0}_filename", sendPortName), new XElement(FileName));
            list.Add(string.Format("{0}_uri", sendPortName), new XElement(uri));

            SiteUrl.Value = string.Format("${{{0}_siteurl}}", sendPortName);
            WssLocation.Value = string.Format("${{{0}_wssLocation}}", sendPortName);
            FileName.Value = string.Format("${{{0}_filename}}", sendPortName);
            uri.Value = string.Format("${{{0}_uri}}", sendPortName);

            adapterConfig.Value = config.ToString();
            transportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void SendPort_FTP(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverport}}/${{{0}_targetfolder}}/${{{0}_targetFileName}}", sendPortName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement uri = config.Element("uri");
            XElement serverAddress = config.Element("serverAddress");
            XElement serverPort = config.Element("serverPort");
            XElement targetFileName = config.Element("targetFileName");
            XElement targetFolder = config.Element("targetFolder");
            XElement ssoAffiliateApplication = config.Element("ssoAffiliateApplication");

            list.Add(string.Format("{0}_serveraddress", sendPortName), new XElement(serverAddress));
            list.Add(string.Format("{0}_serverport", sendPortName), new XElement(serverPort));
            list.Add(string.Format("{0}_targetfilename", sendPortName), new XElement(targetFileName));
            if(targetFolder != null)
                list.Add(string.Format("{0}_targetfolder", sendPortName), new XElement(targetFolder));
            if (ssoAffiliateApplication != null)
                list.Add(string.Format("{0}_ssoaffiliateapplication", sendPortName), new XElement(ssoAffiliateApplication));

            list[string.Format("{0}_address", sendPortName)].Value = list[string.Format("{0}_address", sendPortName)].Value.Replace(targetFileName.Value, "");

            uri.Value = string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverPort}}/${{{0}_targetFolder}}/${{{0}_targetfilename}}", sendPortName);
            serverAddress.Value = string.Format("${{{0}_serveraddress}}", sendPortName);
            serverPort.Value = string.Format("${{{0}_serverport}}", sendPortName);
            targetFileName.Value = string.Format("${{{0}_targetfilename}}", sendPortName);
            if (targetFolder != null)
                targetFolder.Value = string.Format("${{{0}_targetfolder}}", sendPortName);
            if (ssoAffiliateApplication != null)
                ssoAffiliateApplication.Value = string.Format("${{{0}_ssoaffiliateapplication}}", sendPortName);

            adapterConfig.Value = config.ToString();
            transportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void ReceiveLocation_File(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("${{{0}_address}}${{{0}_filemask}}", receiveLocationName);

            XElement FileMask = customProps.Element("FileMask");

            list[string.Format("{0}_address", receiveLocationName)].Value = list[string.Format("{0}_address", receiveLocationName)].Value.Replace(FileMask.Value, "");

            list.Add(string.Format("{0}_filemask", receiveLocationName), new XElement(FileMask));

            FileMask.Value = string.Format("${{{0}_filemask}}", receiveLocationName);

            receiveLocationTransportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

        private static void SendPort_File(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_address}}${{{0}_fileName}}", sendPortName);

            XElement fileName = customProps.Element("FileName");

            list[string.Format("{0}_address", sendPortName)].Value = list[string.Format("{0}_address", sendPortName)].Value.Replace(fileName.Value, "");

            list.Add(string.Format("{0}_fileName", sendPortName), new XElement(fileName));

            fileName.Value = string.Format("${{{0}_fileName}}", sendPortName);

            transportTypeData.Value = customProps.ToString().Replace(System.Environment.NewLine, string.Empty).Replace("  ", string.Empty);
        }

    }
}
