namespace insyca.bt.gl.esb.schemas.generic {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"query_request", @"query_response"})]
    public sealed class query_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.generic.query_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://insyca.bt.gl.esb.schemas.generic.query_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""query_request"">
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""target"" nillable=""true"">
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""process"" nillable=""true"" type=""xs:string"" />
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""type"" nillable=""true"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""sender"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Informationen über den Sender der Information"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" />
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
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""label"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Bezeichner für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""receiver"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Informationen über den Empfänger der Information"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" />
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
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""label"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Bezeichner für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""message"" nillable=""true"">
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:any minOccurs=""0"" maxOccurs=""1"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""unbounded"" name=""parameters"" nillable=""true"">
          <xs:complexType>
            <xs:sequence>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""parameter"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""name"" nillable=""true"" type=""xs:string"" />
                    <xs:element name=""value"" nillable=""true"" type=""xs:string"" />
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element minOccurs=""0"" name=""operator"" nillable=""true"">
                <xs:simpleType>
                  <xs:restriction base=""xs:string"">
                    <xs:enumeration value=""and"" />
                    <xs:enumeration value=""or"" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""query_response"">
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" name=""target"" nillable=""true"">
          <xs:complexType>
            <xs:sequence>
              <xs:element minOccurs=""0"" name=""process"" nillable=""true"" type=""xs:string"" />
              <xs:element minOccurs=""0"" name=""type"" nillable=""true"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name=""sender"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Informationen über den Sender der Information"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence>
              <xs:element minOccurs=""0"" name=""id"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" name=""number"" nillable=""true"" type=""xs:integer"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutige Nummer für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" name=""label"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Bezeichner für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name=""receiver"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Informationen über den Empfänger der Information"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence>
              <xs:element minOccurs=""0"" name=""id"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" name=""number"" nillable=""true"" type=""xs:integer"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutige Nummer für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" name=""label"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Eindeutiger Bezeichner für die Identifikation"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name=""message"" nillable=""true"">
          <xs:complexType>
            <xs:sequence>
              <xs:any minOccurs=""0"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""unbounded"" name=""parameters"" nillable=""true"">
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""parameter"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""name"" nillable=""true"" type=""xs:string"" />
                    <xs:element name=""value"" nillable=""true"" type=""xs:string"" />
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""operator"" nillable=""true"">
                <xs:simpleType>
                  <xs:restriction base=""xs:string"">
                    <xs:enumeration value=""and"" />
                    <xs:enumeration value=""or"" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public query_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [2];
                _RootElements[0] = "query_request";
                _RootElements[1] = "query_response";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic.query_10",@"query_request")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"query_request"})]
        public sealed class query_request : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public query_request() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "query_request";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic.query_10",@"query_response")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"query_response"})]
        public sealed class query_response : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public query_response() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "query_response";
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
