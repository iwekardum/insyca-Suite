namespace insyca.bt.gl.esb.schemas.entities.business.warehouse {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://insyca.bt.gl.esb.schemas.entities.business.warehouse.rfid_10.xsd",@"rfid")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"rfid"})]
    public sealed class rfid_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.entities.business.warehouse.rfid_10.xsd"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" elementFormDefault=""qualified"" targetNamespace=""http://insyca.bt.gl.esb.schemas.entities.business.warehouse.rfid_10.xsd"" id=""rfid_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""rfid"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Wareneingang"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""0"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""timestamp"" nillable=""true"" type=""xs:dateTime"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Zeitstempel"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""gate"" nillable=""true"" type=""xs:integer"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutige Nummer für die Identifikation des Ortes an dem die RFID empfangen wurde"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""direction"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Richtung in der sich die RFID bewegt"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""in"" />
              <xs:enumeration value=""out"" />
              <xs:enumeration value=""N"" />
              <xs:enumeration value=""S"" />
              <xs:enumeration value=""W"" />
              <xs:enumeration value=""O"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""action"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Aktion/Meldung"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""tagid"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""UID des RFID Tags"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public rfid_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "rfid";
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
