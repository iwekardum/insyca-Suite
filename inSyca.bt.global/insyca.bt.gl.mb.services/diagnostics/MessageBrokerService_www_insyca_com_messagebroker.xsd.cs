namespace inSyca.bt.gl.mb.services.diagnostics {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://www.inSyca.com/messagebroker",@"BizTalkMessageWrapper")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"BizTalkMessageWrapper"})]
    public sealed class MessageBrokerService_www_insyca_com_messagebroker : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:tns=""http://www.inSyca.com/messagebroker"" elementFormDefault=""qualified"" targetNamespace=""http://www.inSyca.com/messagebroker"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:complexType name=""BizTalkMessageWrapper"">
    <xs:sequence>
      <xs:element minOccurs=""0"" name=""BizTalkMessage"" nillable=""true"">
        <xs:complexType mixed=""true"">
          <xs:sequence>
            <xs:any minOccurs=""0"" maxOccurs=""unbounded"" processContents=""lax"" />
          </xs:sequence>
          <xs:anyAttribute />
        </xs:complexType>
      </xs:element>
    </xs:sequence>
  </xs:complexType>
  <xs:element name=""BizTalkMessageWrapper"" nillable=""true"" type=""tns:BizTalkMessageWrapper"" />
</xs:schema>";
        
        public MessageBrokerService_www_insyca_com_messagebroker() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "BizTalkMessageWrapper";
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
