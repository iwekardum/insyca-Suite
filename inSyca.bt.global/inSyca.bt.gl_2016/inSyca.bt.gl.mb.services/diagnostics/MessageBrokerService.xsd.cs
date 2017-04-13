namespace inSyca.bt.gl.mb.services.diagnostics {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"",@"service_response")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"service_response"})]
    public sealed class MessageBrokerService : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" elementFormDefault=""qualified"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""service_response"" nillable=""true"">
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
</xs:schema>";
        
        public MessageBrokerService() {
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
