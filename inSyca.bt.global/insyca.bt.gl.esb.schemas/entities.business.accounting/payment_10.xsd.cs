namespace insyca.bt.gl.esb.schemas.entities.business.accounting {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://insyca.bt.gl.esb.schemas.entities.business.accounting.payment_10",@"payment")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"payment"})]
    public sealed class payment_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.entities.business.accounting.payment_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://insyca.bt.gl.esb.schemas.entities.business.accounting.payment_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""payment"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Zahlung"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""0"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""label"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutiger Bezeichner für die Identifikation"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""number"" nillable=""true"" type=""xs:integer"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutige Nummer für die Identifikation"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element name=""method"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Zahlungsart"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""cash"" />
              <xs:enumeration value=""cashondelivery"" />
              <xs:enumeration value=""check"" />
              <xs:enumeration value=""creditcard"" />
              <xs:enumeration value=""transfer"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""type"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Zahlungstyp"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""mastercard"" />
              <xs:enumeration value=""visa"" />
              <xs:enumeration value=""amex"" />
              <xs:enumeration value=""paypal"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public payment_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "payment";
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
