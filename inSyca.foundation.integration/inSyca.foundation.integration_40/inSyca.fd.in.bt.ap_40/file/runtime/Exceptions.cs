//---------------------------------------------------------------------
// File: DotNetFileExceptions.cs
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
using System.Runtime.Serialization;
using inSyca.foundation.integration.biztalk.adapter.common;

namespace inSyca.foundation.integration.biztalk.adapter.file.runtime
{
	internal class Exception : ApplicationException
	{
		public static string UnhandledTransmit_Error = "The .Net File Adapter encounted an error transmitting a batch of messages.";

        public Exception () { }

		public Exception (string msg) : base(msg) { }

		public Exception (Exception inner) : base(String.Empty, inner) { }

		public Exception (string msg, Exception e) : base(msg, e) { }

		protected Exception (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}

