namespace insyca.bt.gl.esb.schemas.entities.physical {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://insyca.bt.gl.esb.schemas.entities.physical.quantity_10",@"quantity")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"quantity"})]
    public sealed class quantity_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.entities.physical.quantity_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://insyca.bt.gl.esb.schemas.entities.physical.quantity_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""quantity"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Masse"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""0"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""type"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Typ: Soll/Ist etc."" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""actual"" />
              <xs:enumeration value=""target"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""unit"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Einheit; Stück, Pallette, Kolli, etc."" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""value"" nillable=""true"" type=""xs:decimal"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Wert"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public quantity_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "quantity";
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
