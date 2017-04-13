namespace insyca.bt.gl.esb.schemas.entities.contact {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://insyca.bt.gl.esb.schemas.entities.contact.language_10",@"language")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"language"})]
    public sealed class language_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.entities.contact.language_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://insyca.bt.gl.esb.schemas.entities.contact.language_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""language"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Sprache"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence minOccurs=""0"" maxOccurs=""1"">
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""id"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Nummer"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""name"" nillable=""true"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""Name"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""iso639-1"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""ISO Code 639-1"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""aa"" />
              <xs:enumeration value=""ab"" />
              <xs:enumeration value=""af"" />
              <xs:enumeration value=""ak"" />
              <xs:enumeration value=""am"" />
              <xs:enumeration value=""ar"" />
              <xs:enumeration value=""an"" />
              <xs:enumeration value=""as"" />
              <xs:enumeration value=""av"" />
              <xs:enumeration value=""ae"" />
              <xs:enumeration value=""ay"" />
              <xs:enumeration value=""az"" />
              <xs:enumeration value=""ba"" />
              <xs:enumeration value=""ba"" />
              <xs:enumeration value=""bm"" />
              <xs:enumeration value=""be"" />
              <xs:enumeration value=""bn"" />
              <xs:enumeration value=""bh"" />
              <xs:enumeration value=""bi"" />
              <xs:enumeration value=""bo"" />
              <xs:enumeration value=""bs"" />
              <xs:enumeration value=""br"" />
              <xs:enumeration value=""bg"" />
              <xs:enumeration value=""ca"" />
              <xs:enumeration value=""cs"" />
              <xs:enumeration value=""ch"" />
              <xs:enumeration value=""ce"" />
              <xs:enumeration value=""cu"" />
              <xs:enumeration value=""cv"" />
              <xs:enumeration value=""kw"" />
              <xs:enumeration value=""co"" />
              <xs:enumeration value=""cr"" />
              <xs:enumeration value=""cy"" />
              <xs:enumeration value=""da"" />
              <xs:enumeration value=""de"" />
              <xs:enumeration value=""dv"" />
              <xs:enumeration value=""dz"" />
              <xs:enumeration value=""el"" />
              <xs:enumeration value=""en"" />
              <xs:enumeration value=""eo"" />
              <xs:enumeration value=""et"" />
              <xs:enumeration value=""eu"" />
              <xs:enumeration value=""ee"" />
              <xs:enumeration value=""fo"" />
              <xs:enumeration value=""fa"" />
              <xs:enumeration value=""fj"" />
              <xs:enumeration value=""fi"" />
              <xs:enumeration value=""fr"" />
              <xs:enumeration value=""fy"" />
              <xs:enumeration value=""ff"" />
              <xs:enumeration value=""gd"" />
              <xs:enumeration value=""ga"" />
              <xs:enumeration value=""gl"" />
              <xs:enumeration value=""gv"" />
              <xs:enumeration value=""gn"" />
              <xs:enumeration value=""gu"" />
              <xs:enumeration value=""ht"" />
              <xs:enumeration value=""ha"" />
              <xs:enumeration value=""he"" />
              <xs:enumeration value=""hz"" />
              <xs:enumeration value=""hi"" />
              <xs:enumeration value=""ho"" />
              <xs:enumeration value=""hr"" />
              <xs:enumeration value=""hu"" />
              <xs:enumeration value=""hy"" />
              <xs:enumeration value=""ig"" />
              <xs:enumeration value=""io"" />
              <xs:enumeration value=""ii"" />
              <xs:enumeration value=""iu"" />
              <xs:enumeration value=""ie"" />
              <xs:enumeration value=""ia"" />
              <xs:enumeration value=""id"" />
              <xs:enumeration value=""ik"" />
              <xs:enumeration value=""is"" />
              <xs:enumeration value=""it"" />
              <xs:enumeration value=""jv"" />
              <xs:enumeration value=""ja"" />
              <xs:enumeration value=""kl"" />
              <xs:enumeration value=""kn"" />
              <xs:enumeration value=""ks"" />
              <xs:enumeration value=""ka"" />
              <xs:enumeration value=""kr"" />
              <xs:enumeration value=""kk"" />
              <xs:enumeration value=""km"" />
              <xs:enumeration value=""ki"" />
              <xs:enumeration value=""rw"" />
              <xs:enumeration value=""ky"" />
              <xs:enumeration value=""kv"" />
              <xs:enumeration value=""kg"" />
              <xs:enumeration value=""ko"" />
              <xs:enumeration value=""kj"" />
              <xs:enumeration value=""ku"" />
              <xs:enumeration value=""lo"" />
              <xs:enumeration value=""la"" />
              <xs:enumeration value=""lv"" />
              <xs:enumeration value=""li"" />
              <xs:enumeration value=""ln"" />
              <xs:enumeration value=""lt"" />
              <xs:enumeration value=""lb"" />
              <xs:enumeration value=""lu"" />
              <xs:enumeration value=""lg"" />
              <xs:enumeration value=""mh"" />
              <xs:enumeration value=""ml"" />
              <xs:enumeration value=""mr"" />
              <xs:enumeration value=""mk"" />
              <xs:enumeration value=""mg"" />
              <xs:enumeration value=""mt"" />
              <xs:enumeration value=""mn"" />
              <xs:enumeration value=""mi"" />
              <xs:enumeration value=""ms"" />
              <xs:enumeration value=""my"" />
              <xs:enumeration value=""na"" />
              <xs:enumeration value=""nv"" />
              <xs:enumeration value=""nr"" />
              <xs:enumeration value=""nd"" />
              <xs:enumeration value=""ng"" />
              <xs:enumeration value=""ne"" />
              <xs:enumeration value=""nl"" />
              <xs:enumeration value=""nn"" />
              <xs:enumeration value=""nb"" />
              <xs:enumeration value=""no"" />
              <xs:enumeration value=""ny"" />
              <xs:enumeration value=""oc"" />
              <xs:enumeration value=""oj"" />
              <xs:enumeration value=""or"" />
              <xs:enumeration value=""om"" />
              <xs:enumeration value=""os"" />
              <xs:enumeration value=""pa"" />
              <xs:enumeration value=""pi"" />
              <xs:enumeration value=""pl"" />
              <xs:enumeration value=""pt"" />
              <xs:enumeration value=""ps"" />
              <xs:enumeration value=""qu"" />
              <xs:enumeration value=""rm"" />
              <xs:enumeration value=""ro"" />
              <xs:enumeration value=""rn"" />
              <xs:enumeration value=""ru"" />
              <xs:enumeration value=""sg"" />
              <xs:enumeration value=""sa"" />
              <xs:enumeration value=""si"" />
              <xs:enumeration value=""sk"" />
              <xs:enumeration value=""sl"" />
              <xs:enumeration value=""se"" />
              <xs:enumeration value=""sm"" />
              <xs:enumeration value=""sn"" />
              <xs:enumeration value=""sd"" />
              <xs:enumeration value=""so"" />
              <xs:enumeration value=""st"" />
              <xs:enumeration value=""es"" />
              <xs:enumeration value=""sq"" />
              <xs:enumeration value=""sc"" />
              <xs:enumeration value=""sr"" />
              <xs:enumeration value=""ss"" />
              <xs:enumeration value=""su"" />
              <xs:enumeration value=""sw"" />
              <xs:enumeration value=""sv"" />
              <xs:enumeration value=""ty"" />
              <xs:enumeration value=""ta"" />
              <xs:enumeration value=""tt"" />
              <xs:enumeration value=""te"" />
              <xs:enumeration value=""tg"" />
              <xs:enumeration value=""tl"" />
              <xs:enumeration value=""th"" />
              <xs:enumeration value=""ti"" />
              <xs:enumeration value=""to"" />
              <xs:enumeration value=""tn"" />
              <xs:enumeration value=""ts"" />
              <xs:enumeration value=""tk"" />
              <xs:enumeration value=""tr"" />
              <xs:enumeration value=""tw"" />
              <xs:enumeration value=""ug"" />
              <xs:enumeration value=""uk"" />
              <xs:enumeration value=""ur"" />
              <xs:enumeration value=""uz"" />
              <xs:enumeration value=""ve"" />
              <xs:enumeration value=""vi"" />
              <xs:enumeration value=""vo"" />
              <xs:enumeration value=""wa"" />
              <xs:enumeration value=""wo"" />
              <xs:enumeration value=""xh"" />
              <xs:enumeration value=""yi"" />
              <xs:enumeration value=""yo"" />
              <xs:enumeration value=""za"" />
              <xs:enumeration value=""zh"" />
              <xs:enumeration value=""zu"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""iso639-2"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""ISO Code 639-2"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""aar"" />
              <xs:enumeration value=""abk"" />
              <xs:enumeration value=""afr"" />
              <xs:enumeration value=""aka"" />
              <xs:enumeration value=""amh"" />
              <xs:enumeration value=""ara"" />
              <xs:enumeration value=""arg"" />
              <xs:enumeration value=""asm"" />
              <xs:enumeration value=""ava"" />
              <xs:enumeration value=""ave"" />
              <xs:enumeration value=""aym"" />
              <xs:enumeration value=""aze"" />
              <xs:enumeration value=""bak"" />
              <xs:enumeration value=""bal"" />
              <xs:enumeration value=""bam"" />
              <xs:enumeration value=""bel"" />
              <xs:enumeration value=""ben"" />
              <xs:enumeration value=""bih"" />
              <xs:enumeration value=""bis"" />
              <xs:enumeration value=""bod"" />
              <xs:enumeration value=""bos"" />
              <xs:enumeration value=""bre"" />
              <xs:enumeration value=""bul"" />
              <xs:enumeration value=""cat"" />
              <xs:enumeration value=""ces"" />
              <xs:enumeration value=""cha"" />
              <xs:enumeration value=""che"" />
              <xs:enumeration value=""chu"" />
              <xs:enumeration value=""chv"" />
              <xs:enumeration value=""cor"" />
              <xs:enumeration value=""cos"" />
              <xs:enumeration value=""cre"" />
              <xs:enumeration value=""cym"" />
              <xs:enumeration value=""dan"" />
              <xs:enumeration value=""deu"" />
              <xs:enumeration value=""div"" />
              <xs:enumeration value=""dzo"" />
              <xs:enumeration value=""ell"" />
              <xs:enumeration value=""eng"" />
              <xs:enumeration value=""epo"" />
              <xs:enumeration value=""est"" />
              <xs:enumeration value=""eus"" />
              <xs:enumeration value=""ewe"" />
              <xs:enumeration value=""fao"" />
              <xs:enumeration value=""fas"" />
              <xs:enumeration value=""fij"" />
              <xs:enumeration value=""fin"" />
              <xs:enumeration value=""fra"" />
              <xs:enumeration value=""fry"" />
              <xs:enumeration value=""ful"" />
              <xs:enumeration value=""gla"" />
              <xs:enumeration value=""gle"" />
              <xs:enumeration value=""glg"" />
              <xs:enumeration value=""glv"" />
              <xs:enumeration value=""grn"" />
              <xs:enumeration value=""guj"" />
              <xs:enumeration value=""hat"" />
              <xs:enumeration value=""hau"" />
              <xs:enumeration value=""heb"" />
              <xs:enumeration value=""her"" />
              <xs:enumeration value=""hin"" />
              <xs:enumeration value=""hmo"" />
              <xs:enumeration value=""hrv"" />
              <xs:enumeration value=""hun"" />
              <xs:enumeration value=""hye"" />
              <xs:enumeration value=""ibo"" />
              <xs:enumeration value=""ido"" />
              <xs:enumeration value=""iii"" />
              <xs:enumeration value=""iku"" />
              <xs:enumeration value=""ile"" />
              <xs:enumeration value=""ina"" />
              <xs:enumeration value=""ind"" />
              <xs:enumeration value=""ipk"" />
              <xs:enumeration value=""isl"" />
              <xs:enumeration value=""ita"" />
              <xs:enumeration value=""jav"" />
              <xs:enumeration value=""jpn"" />
              <xs:enumeration value=""kal"" />
              <xs:enumeration value=""kan"" />
              <xs:enumeration value=""kas"" />
              <xs:enumeration value=""kat"" />
              <xs:enumeration value=""kau"" />
              <xs:enumeration value=""kaz"" />
              <xs:enumeration value=""khm"" />
              <xs:enumeration value=""kik"" />
              <xs:enumeration value=""kin"" />
              <xs:enumeration value=""kir"" />
              <xs:enumeration value=""kom"" />
              <xs:enumeration value=""kon"" />
              <xs:enumeration value=""kor"" />
              <xs:enumeration value=""kua"" />
              <xs:enumeration value=""kur"" />
              <xs:enumeration value=""lao"" />
              <xs:enumeration value=""lat"" />
              <xs:enumeration value=""lav"" />
              <xs:enumeration value=""lij"" />
              <xs:enumeration value=""lin"" />
              <xs:enumeration value=""lit"" />
              <xs:enumeration value=""ltz"" />
              <xs:enumeration value=""lub"" />
              <xs:enumeration value=""lug"" />
              <xs:enumeration value=""mah"" />
              <xs:enumeration value=""mal"" />
              <xs:enumeration value=""mar"" />
              <xs:enumeration value=""mkd"" />
              <xs:enumeration value=""mlg"" />
              <xs:enumeration value=""mlt"" />
              <xs:enumeration value=""mon"" />
              <xs:enumeration value=""mri"" />
              <xs:enumeration value=""msa"" />
              <xs:enumeration value=""mya"" />
              <xs:enumeration value=""nau"" />
              <xs:enumeration value=""nav"" />
              <xs:enumeration value=""nbl"" />
              <xs:enumeration value=""nde"" />
              <xs:enumeration value=""ndo"" />
              <xs:enumeration value=""nep"" />
              <xs:enumeration value=""nld"" />
              <xs:enumeration value=""nno"" />
              <xs:enumeration value=""nob"" />
              <xs:enumeration value=""nor"" />
              <xs:enumeration value=""nya"" />
              <xs:enumeration value=""oci"" />
              <xs:enumeration value=""oji"" />
              <xs:enumeration value=""ori"" />
              <xs:enumeration value=""orm"" />
              <xs:enumeration value=""oss"" />
              <xs:enumeration value=""pan"" />
              <xs:enumeration value=""pli"" />
              <xs:enumeration value=""pol"" />
              <xs:enumeration value=""por"" />
              <xs:enumeration value=""pus"" />
              <xs:enumeration value=""que"" />
              <xs:enumeration value=""roh"" />
              <xs:enumeration value=""ron"" />
              <xs:enumeration value=""run"" />
              <xs:enumeration value=""rus"" />
              <xs:enumeration value=""sag"" />
              <xs:enumeration value=""san"" />
              <xs:enumeration value=""sin"" />
              <xs:enumeration value=""slk"" />
              <xs:enumeration value=""slv"" />
              <xs:enumeration value=""sme"" />
              <xs:enumeration value=""smo"" />
              <xs:enumeration value=""sna"" />
              <xs:enumeration value=""snd"" />
              <xs:enumeration value=""som"" />
              <xs:enumeration value=""sot"" />
              <xs:enumeration value=""spa"" />
              <xs:enumeration value=""sqi"" />
              <xs:enumeration value=""srd"" />
              <xs:enumeration value=""srp"" />
              <xs:enumeration value=""ssw"" />
              <xs:enumeration value=""sun"" />
              <xs:enumeration value=""swa"" />
              <xs:enumeration value=""swe"" />
              <xs:enumeration value=""tah"" />
              <xs:enumeration value=""tam"" />
              <xs:enumeration value=""tat"" />
              <xs:enumeration value=""tel"" />
              <xs:enumeration value=""tgk"" />
              <xs:enumeration value=""tgl"" />
              <xs:enumeration value=""tha"" />
              <xs:enumeration value=""tir"" />
              <xs:enumeration value=""ton"" />
              <xs:enumeration value=""tsn"" />
              <xs:enumeration value=""tso"" />
              <xs:enumeration value=""tuk"" />
              <xs:enumeration value=""tur"" />
              <xs:enumeration value=""twi"" />
              <xs:enumeration value=""uig"" />
              <xs:enumeration value=""ukr"" />
              <xs:enumeration value=""urd"" />
              <xs:enumeration value=""uzb"" />
              <xs:enumeration value=""ven"" />
              <xs:enumeration value=""vie"" />
              <xs:enumeration value=""vol"" />
              <xs:enumeration value=""wln"" />
              <xs:enumeration value=""wol"" />
              <xs:enumeration value=""xho"" />
              <xs:enumeration value=""yid"" />
              <xs:enumeration value=""yor"" />
              <xs:enumeration value=""zha"" />
              <xs:enumeration value=""zho"" />
              <xs:enumeration value=""zul"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public language_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "language";
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
