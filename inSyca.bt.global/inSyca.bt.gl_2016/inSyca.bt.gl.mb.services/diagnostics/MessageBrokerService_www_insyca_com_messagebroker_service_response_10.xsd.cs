namespace inSyca.bt.gl.mb.services.diagnostics {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://www.inSyca.com/messagebroker/service_response_10",@"service_response")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"service_response"})]
    public sealed class MessageBrokerService_www_insyca_com_messagebroker_service_response_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://www.inSyca.com/messagebroker/service_response_10"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" attributeFormDefault=""qualified"" elementFormDefault=""qualified"" targetNamespace=""http://www.inSyca.com/messagebroker/service_response_10"" id=""service_response"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element msdata:IsDataSet=""true"" msdata:UseCurrentLocale=""true"" name=""service_response"">
    <xs:complexType>
      <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
        <xs:element name=""response"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""timestamp_started"" type=""xs:dateTime"" />
              <xs:element name=""timestamp_finished"" type=""xs:dateTime"" />
              <xs:element name=""status"" type=""xs:int"" />
              <xs:element name=""message"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public MessageBrokerService_www_insyca_com_messagebroker_service_response_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "service_response";
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
