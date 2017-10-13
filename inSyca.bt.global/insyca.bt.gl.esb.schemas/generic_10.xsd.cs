namespace insyca.bt.gl.esb.schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "recordcount", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='recordcount' and namespace-uri()='']", XsdType = @"double")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "size", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='size' and namespace-uri()='']", XsdType = @"double")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "index", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='index' and namespace-uri()='']", XsdType = @"double")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.senderid), XPath = @"/*[local-name()='processinginfo' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='senderid' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.receiverid), XPath = @"/*[local-name()='processinginfo' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='receiverid' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.process), XPath = @"/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='process' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.type), XPath = @"/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='type' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.action), XPath = @"/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='action' and namespace-uri()='']", XsdType = @"string")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"batch", @"identification", @"processinginfo", @"query_request", @"query_response", @"status", @"supportinfo"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"insyca.bt.gl.esb.schemas.properties", typeof(global::insyca.bt.gl.esb.schemas.properties))]
    public sealed class generic_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.generic_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:ns0=""https://insyca.bt.gl.esb.schemas.generic.properties"" targetNamespace=""http://insyca.bt.gl.esb.schemas.generic_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:schemaInfo schema_name=""Generic Schemas"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
      <b:imports>
        <b:namespace prefix=""ns0"" uri=""https://insyca.bt.gl.esb.schemas.generic.properties"" location=""insyca.bt.gl.esb.schemas.properties"" />
      </b:imports>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""batch"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Informationen für 'debatching' bzw. 'splitten=' von Messages"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='recordcount' and namespace-uri()='']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='size' and namespace-uri()='']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='index' and namespace-uri()='']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""0"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""recordcount"" type=""xs:double"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Anzahl der Records im Schema"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""size"" nillable=""true"" type=""xs:double"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Batch Größe, Anzahl an Elementen pro Iteration"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""index"" nillable=""true"" type=""xs:double"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Laufvariable bei Iteration"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""identification"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Referenz"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""0"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""category"" nillable=""true"">
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""internal"" />
              <xs:enumeration value=""external"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""type"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Typ"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""parent"" />
              <xs:enumeration value=""ref1"" />
              <xs:enumeration value=""ref2"" />
              <xs:enumeration value=""ref3"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""label"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutiger Bezeicher für die Identification"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""number"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutige Nummer für die Identification"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""processinginfo"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Informationen für die Verarbeitung der Message im BizTalk"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
        <b:properties>
          <b:property name=""ns0:senderid"" xpath=""/*[local-name()='processinginfo' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='senderid' and namespace-uri()='']"" />
          <b:property name=""ns0:receiverid"" xpath=""/*[local-name()='processinginfo' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='receiverid' and namespace-uri()='']"" />
        </b:properties>
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""senderid"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Id von wem die Message empfangen wurde"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""receiverid"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Id an wen die Message gesendet werden soll"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""action"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Zusatzinformationen für auszuführende Aktionen"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""timestamp"" nillable=""true"" type=""xs:dateTime"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Datum und Uhrzeit, wann die Message empfangen wurde"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""faulted"" nillable=""true"" type=""xs:boolean"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Wurde eine Fehlerinformation empfangen?"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""faultedmessage"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Fehlermeldung"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""niltag"" nillable=""true"" type=""xs:boolean"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Platzhalter für 'Nil' Functoid wenn per XSLT Nil Werte transformiert werden"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""unbounded"" name=""custom"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Schlüssel-/Wertpaare für Anpassungen"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""0"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""key"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Schlüssel des Elements"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""value"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Wert des Elements"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""query_request"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property name=""ns0:process"" xpath=""/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='process' and namespace-uri()='']"" />
          <b:property name=""ns0:type"" xpath=""/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='type' and namespace-uri()='']"" />
          <b:property name=""ns0:action"" xpath=""/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='action' and namespace-uri()='']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""1"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""instruction"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Zusatzinformationen für den Empfänger der Nachricht"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""process"" nillable=""true"" type=""xs:string"" />
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""type"" nillable=""true"" type=""xs:string"" />
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""action"" nillable=""true"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""sender"" nillable=""true"">
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""receiver"" nillable=""true"">
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""message"" nillable=""true"">
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:any minOccurs=""0"" maxOccurs=""1"" processContents=""skip"" />
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
  <xs:element name=""query_response"" nillable=""true"">
    <xs:complexType>
      <xs:sequence minOccurs=""1"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""instruction"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Zusatzinformationen für den Empfänger der Nachricht"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence>
              <xs:element minOccurs=""0"" name=""process"" nillable=""true"" type=""xs:string"" />
              <xs:element minOccurs=""0"" name=""type"" nillable=""true"" type=""xs:string"" />
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""action"" nillable=""true"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""sender"" nillable=""true"">
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""receiver"" nillable=""true"">
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""message"" nillable=""true"">
          <xs:complexType>
            <xs:sequence>
              <xs:any minOccurs=""0"" processContents=""skip"" />
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
  <xs:element name=""status"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Status"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Beschreibung/Freitext"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""supportinfo"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <recordInfo notes=""Support Informationen"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""1"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Eindeutiger Wert für die Identifikation"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""timestamp"" nillable=""true"" type=""xs:dateTime"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Zeitstempel"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""messages"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <recordInfo notes=""Container für Nachrichten"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""1"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""unbounded"" name=""message"" nillable=""true"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:recordInfo notes=""Nachricht/Message"" />
                  </xs:appinfo>
                </xs:annotation>
                <xs:complexType>
                  <xs:sequence minOccurs=""1"" maxOccurs=""1"">
                    <xs:element minOccurs=""0"" maxOccurs=""1"" name=""info"" nillable=""true"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:recordInfo notes=""Informationen zur Nachricht"" />
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
                    <xs:element minOccurs=""0"" maxOccurs=""1"" name=""content"" nillable=""true"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:recordInfo notes=""Inhalt der Nachricht"" />
                        </xs:appinfo>
                      </xs:annotation>
                      <xs:complexType>
                        <xs:sequence minOccurs=""1"" maxOccurs=""1"">
                          <xs:any minOccurs=""0"" maxOccurs=""1"" processContents=""skip"" />
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""sender"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Informationen über den Sender der Information"" />
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""information"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Informationstexte"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""0"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_0"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_0"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_1"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_1"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_2"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_2"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_3"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_3"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_4"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_4"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_5"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_5"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_6"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_6"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_7"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_7"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_8"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_8"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""text_9"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Informationstext_9"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""unbounded"" name=""custom"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo notes=""Schlüssel-/Wertpaare für Anpassungen"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence minOccurs=""0"" maxOccurs=""1"">
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""key"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Schlüssel des Elements"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element minOccurs=""0"" maxOccurs=""1"" name=""value"" nillable=""true"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo notes=""Wert des Elements"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public generic_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [7];
                _RootElements[0] = "batch";
                _RootElements[1] = "identification";
                _RootElements[2] = "processinginfo";
                _RootElements[3] = "query_request";
                _RootElements[4] = "query_response";
                _RootElements[5] = "status";
                _RootElements[6] = "supportinfo";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"batch")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "recordcount", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='recordcount' and namespace-uri()='']", XsdType = @"double")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "size", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='size' and namespace-uri()='']", XsdType = @"double")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "index", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='index' and namespace-uri()='']", XsdType = @"double")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"batch"})]
        public sealed class batch : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public batch() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "batch";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"identification")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"identification"})]
        public sealed class identification : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public identification() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "identification";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"processinginfo")]
        [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.senderid), XPath = @"/*[local-name()='processinginfo' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='senderid' and namespace-uri()='']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.receiverid), XPath = @"/*[local-name()='processinginfo' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='receiverid' and namespace-uri()='']", XsdType = @"string")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"processinginfo"})]
        public sealed class processinginfo : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public processinginfo() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "processinginfo";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"query_request")]
        [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.process), XPath = @"/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='process' and namespace-uri()='']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.type), XPath = @"/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='type' and namespace-uri()='']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::insyca.bt.gl.esb.schemas.action), XPath = @"/*[local-name()='query_request' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic_10']/*[local-name()='instruction' and namespace-uri()='']/*[local-name()='action' and namespace-uri()='']", XsdType = @"string")]
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"query_response")]
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"status")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"status"})]
        public sealed class status : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public status() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "status";
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
        
        [Schema(@"http://insyca.bt.gl.esb.schemas.generic_10",@"supportinfo")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"supportinfo"})]
        public sealed class supportinfo : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public supportinfo() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "supportinfo";
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
