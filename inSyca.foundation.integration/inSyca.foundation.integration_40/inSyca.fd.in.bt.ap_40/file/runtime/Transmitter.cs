//---------------------------------------------------------------------
// File: DotNetFileTransmitter.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework runtime adapter.
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Xml;
using Microsoft.BizTalk.Component.Interop;
using inSyca.foundation.integration.biztalk.adapter.common;
using Microsoft.BizTalk.TransportProxy.Interop;

namespace inSyca.foundation.integration.biztalk.adapter.file.runtime
{
	/// <summary>
	/// This is a singleton class for DotNetFile send adapter. All the messages, going to various
	/// send ports of this adapter type, will go through this class.
	/// </summary>
	public class Transmitter : AsyncTransmitter
	{
		internal static string DOTNET_FILE_NAMESPACE = "http://schemas.microsoft.com/BizTalk/2003/SDK_Samples/Messaging/Transports/dotnetfile-properties";

		public Transmitter() : base(
			".Net FILE Transmit Adapter",
			"1.0",
			"Send files form BizTalk to disk",
			".NetFILE",
            new Guid("17A6257D-9A41-4EA4-8AFD-0ABEB7959976"),
			DOTNET_FILE_NAMESPACE,
			typeof(TransmitterEndpoint),
			TransmitProperties.BatchSize)
		{
		}
	
		protected override void HandlerPropertyBagLoaded ()
		{
			IPropertyBag config = this.HandlerPropertyBag;
			if (null != config)
			{
				XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(config);
				if (null != handlerConfigDom)
				{
					TransmitProperties.ReadTransmitHandlerConfiguration(handlerConfigDom);
				}
			}
		}
	}
}
