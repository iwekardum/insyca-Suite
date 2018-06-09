namespace inSyca.bt.gl.mb.services.diagnostics {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"getVersion", @"getVersionResponse", @"logMessage", @"logMessageResponse"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"inSyca.bt.gl.mb.services.diagnostics.MessageBrokerService_www_insyca_com_messagebroker", typeof(global::inSyca.bt.gl.mb.services.diagnostics.MessageBrokerService_www_insyca_com_messagebroker))]
    public sealed class MessageBrokerService_www_insyca_com_IMessageBroker : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:tns=""http://www.inSyca.com/IMessageBroker"" elementFormDefault=""qualified"" targetNamespace=""http://www.inSyca.com/IMessageBroker"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""inSyca.bt.gl.mb.services.diagnostics.MessageBrokerService_www_insyca_com_messagebroker"" namespace=""http://www.inSyca.com/messagebroker"" />
  <xs:annotation>
    <xs:appinfo>
      <references xmlns=""http://schemas.microsoft.com/BizTalk/2003"">
        <reference targetNamespace=""http://www.inSyca.com/messagebroker"" />
      </references>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""getVersion"">
    <xs:complexType>
      <xs:sequence />
    </xs:complexType>
  </xs:element>
  <xs:element name=""getVersionResponse"">
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" name=""getVersionResult"" nillable=""true"">
          <xs:complexType>
            <xs:annotation>
              <xs:appinfo>
                <ActualType Name=""service_response"" Namespace=""http://schemas.datacontract.org/2004/07/inSyca.foundation.communication.itf"" xmlns=""http://schemas.microsoft.com/2003/10/Serialization/"">
                </ActualType>
              </xs:appinfo>
            </xs:annotation>
            <xs:sequence>
              <xs:any namespace=""http://www.inSyca.com/messagebroker/service_response_10"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""logMessage"">
    <xs:complexType>
      <xs:sequence>
        <xs:element xmlns:q1=""http://www.inSyca.com/messagebroker"" minOccurs=""0"" name=""inDocument"" nillable=""true"" type=""q1:BizTalkMessageWrapper"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""logMessageResponse"">
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" name=""logMessageResult"" nillable=""true"">
          <xs:complexType>
            <xs:annotation>
              <xs:appinfo>
                <ActualType Name=""service_response"" Namespace=""http://schemas.datacontract.org/2004/07/inSyca.foundation.communication.itf"" xmlns=""http://schemas.microsoft.com/2003/10/Serialization/"">
                </ActualType>
              </xs:appinfo>
            </xs:annotation>
            <xs:sequence>
              <xs:any namespace=""http://www.inSyca.com/messagebroker/service_response_10"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public MessageBrokerService_www_insyca_com_IMessageBroker() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [4];
                _RootElements[0] = "getVersion";
                _RootElements[1] = "getVersionResponse";
                _RootElements[2] = "logMessage";
                _RootElements[3] = "logMessageResponse";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
        
        [Schema(@"http://www.inSyca.com/IMessageBroker",@"getVersion")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"getVersion"})]
        public sealed class getVersion : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public getVersion() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "getVersion";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://www.inSyca.com/IMessageBroker",@"getVersionResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"getVersionResponse"})]
        public sealed class getVersionResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public getVersionResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "getVersionResponse";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://www.inSyca.com/IMessageBroker",@"logMessage")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"logMessage"})]
        public sealed class logMessage : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public logMessage() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "logMessage";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://www.inSyca.com/IMessageBroker",@"logMessageResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"logMessageResponse"})]
        public sealed class logMessageResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public logMessageResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "logMessageResponse";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
    }
}
