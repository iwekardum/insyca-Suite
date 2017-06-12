namespace insyca.bt.gl.esb.schemas.generic {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://insyca.bt.gl.esb.schemas.generic.batch_10",@"batch")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "recordcount", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic.batch_10']/*[local-name()='recordcount' and namespace-uri()='']", XsdType = @"double")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "size", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic.batch_10']/*[local-name()='size' and namespace-uri()='']", XsdType = @"double")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Double), "index", XPath = @"/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic.batch_10']/*[local-name()='index' and namespace-uri()='']", XsdType = @"double")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"batch"})]
    public sealed class batch_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.generic.batch_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://insyca.bt.gl.esb.schemas.generic.batch_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""batch"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Informationen für 'debatching' bzw. 'splitten=' von Messages"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic.batch_10']/*[local-name()='recordcount' and namespace-uri()='']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic.batch_10']/*[local-name()='size' and namespace-uri()='']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='batch' and namespace-uri()='http://insyca.bt.gl.esb.schemas.generic.batch_10']/*[local-name()='index' and namespace-uri()='']"" />
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
</xs:schema>";
        
        public batch_10() {
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
}
