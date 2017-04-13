namespace insyca.bt.gl.esb.schemas.entities.contact {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://insyca.bt.gl.esb.schemas.entities.contact.country_10",@"country")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"country"})]
    public sealed class country_10 : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://insyca.bt.gl.esb.schemas.entities.contact.country_10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://insyca.bt.gl.esb.schemas.entities.contact.country_10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""country"" nillable=""true"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo notes=""Land"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
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
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""iso3166-1alpha2"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""ISO Code 3166-1 Alpha-2"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""AF"" />
              <xs:enumeration value=""AX"" />
              <xs:enumeration value=""AL"" />
              <xs:enumeration value=""DZ"" />
              <xs:enumeration value=""AS"" />
              <xs:enumeration value=""AD"" />
              <xs:enumeration value=""AO"" />
              <xs:enumeration value=""AI"" />
              <xs:enumeration value=""AQ"" />
              <xs:enumeration value=""AG"" />
              <xs:enumeration value=""AR"" />
              <xs:enumeration value=""AM"" />
              <xs:enumeration value=""AW"" />
              <xs:enumeration value=""AU"" />
              <xs:enumeration value=""AT"" />
              <xs:enumeration value=""AZ"" />
              <xs:enumeration value=""BS"" />
              <xs:enumeration value=""BH"" />
              <xs:enumeration value=""BD"" />
              <xs:enumeration value=""BB"" />
              <xs:enumeration value=""BY"" />
              <xs:enumeration value=""BE"" />
              <xs:enumeration value=""BZ"" />
              <xs:enumeration value=""BJ"" />
              <xs:enumeration value=""BM"" />
              <xs:enumeration value=""BT"" />
              <xs:enumeration value=""BO"" />
              <xs:enumeration value=""BQ"" />
              <xs:enumeration value=""BA"" />
              <xs:enumeration value=""BW"" />
              <xs:enumeration value=""BV"" />
              <xs:enumeration value=""BR"" />
              <xs:enumeration value=""IO"" />
              <xs:enumeration value=""BN"" />
              <xs:enumeration value=""BG"" />
              <xs:enumeration value=""BF"" />
              <xs:enumeration value=""BI"" />
              <xs:enumeration value=""CV"" />
              <xs:enumeration value=""KH"" />
              <xs:enumeration value=""CM"" />
              <xs:enumeration value=""CA"" />
              <xs:enumeration value=""KY"" />
              <xs:enumeration value=""CF"" />
              <xs:enumeration value=""TD"" />
              <xs:enumeration value=""CL"" />
              <xs:enumeration value=""CN"" />
              <xs:enumeration value=""CX"" />
              <xs:enumeration value=""CC"" />
              <xs:enumeration value=""CO"" />
              <xs:enumeration value=""KM"" />
              <xs:enumeration value=""CG"" />
              <xs:enumeration value=""CD"" />
              <xs:enumeration value=""CK"" />
              <xs:enumeration value=""CR"" />
              <xs:enumeration value=""CI"" />
              <xs:enumeration value=""HR"" />
              <xs:enumeration value=""CU"" />
              <xs:enumeration value=""CW"" />
              <xs:enumeration value=""CY"" />
              <xs:enumeration value=""CZ"" />
              <xs:enumeration value=""DK"" />
              <xs:enumeration value=""DJ"" />
              <xs:enumeration value=""DM"" />
              <xs:enumeration value=""DO"" />
              <xs:enumeration value=""EC"" />
              <xs:enumeration value=""EG"" />
              <xs:enumeration value=""SV"" />
              <xs:enumeration value=""GQ"" />
              <xs:enumeration value=""ER"" />
              <xs:enumeration value=""EE"" />
              <xs:enumeration value=""ET"" />
              <xs:enumeration value=""FK"" />
              <xs:enumeration value=""FO"" />
              <xs:enumeration value=""FJ"" />
              <xs:enumeration value=""FI"" />
              <xs:enumeration value=""FR"" />
              <xs:enumeration value=""GF"" />
              <xs:enumeration value=""PF"" />
              <xs:enumeration value=""TF"" />
              <xs:enumeration value=""GA"" />
              <xs:enumeration value=""GM"" />
              <xs:enumeration value=""GE"" />
              <xs:enumeration value=""DE"" />
              <xs:enumeration value=""GH"" />
              <xs:enumeration value=""GI"" />
              <xs:enumeration value=""GR"" />
              <xs:enumeration value=""GL"" />
              <xs:enumeration value=""GD"" />
              <xs:enumeration value=""GP"" />
              <xs:enumeration value=""GU"" />
              <xs:enumeration value=""GT"" />
              <xs:enumeration value=""GG"" />
              <xs:enumeration value=""GN"" />
              <xs:enumeration value=""GW"" />
              <xs:enumeration value=""GY"" />
              <xs:enumeration value=""HT"" />
              <xs:enumeration value=""HM"" />
              <xs:enumeration value=""VA"" />
              <xs:enumeration value=""HN"" />
              <xs:enumeration value=""HK"" />
              <xs:enumeration value=""HU"" />
              <xs:enumeration value=""IS"" />
              <xs:enumeration value=""IN"" />
              <xs:enumeration value=""ID"" />
              <xs:enumeration value=""IR"" />
              <xs:enumeration value=""IQ"" />
              <xs:enumeration value=""IE"" />
              <xs:enumeration value=""IM"" />
              <xs:enumeration value=""IL"" />
              <xs:enumeration value=""IT"" />
              <xs:enumeration value=""JM"" />
              <xs:enumeration value=""JP"" />
              <xs:enumeration value=""JE"" />
              <xs:enumeration value=""JO"" />
              <xs:enumeration value=""KZ"" />
              <xs:enumeration value=""KE"" />
              <xs:enumeration value=""KI"" />
              <xs:enumeration value=""KP"" />
              <xs:enumeration value=""KR"" />
              <xs:enumeration value=""KW"" />
              <xs:enumeration value=""KG"" />
              <xs:enumeration value=""LA"" />
              <xs:enumeration value=""LV"" />
              <xs:enumeration value=""LB"" />
              <xs:enumeration value=""LS"" />
              <xs:enumeration value=""LR"" />
              <xs:enumeration value=""LY"" />
              <xs:enumeration value=""LI"" />
              <xs:enumeration value=""LT"" />
              <xs:enumeration value=""LU"" />
              <xs:enumeration value=""MO"" />
              <xs:enumeration value=""MK"" />
              <xs:enumeration value=""MG"" />
              <xs:enumeration value=""MW"" />
              <xs:enumeration value=""MY"" />
              <xs:enumeration value=""MV"" />
              <xs:enumeration value=""ML"" />
              <xs:enumeration value=""MT"" />
              <xs:enumeration value=""MH"" />
              <xs:enumeration value=""MQ"" />
              <xs:enumeration value=""MR"" />
              <xs:enumeration value=""MU"" />
              <xs:enumeration value=""YT"" />
              <xs:enumeration value=""MX"" />
              <xs:enumeration value=""FM"" />
              <xs:enumeration value=""MD"" />
              <xs:enumeration value=""MC"" />
              <xs:enumeration value=""MN"" />
              <xs:enumeration value=""ME"" />
              <xs:enumeration value=""MS"" />
              <xs:enumeration value=""MA"" />
              <xs:enumeration value=""MZ"" />
              <xs:enumeration value=""MM"" />
              <xs:enumeration value=""NA"" />
              <xs:enumeration value=""NR"" />
              <xs:enumeration value=""NP"" />
              <xs:enumeration value=""NL"" />
              <xs:enumeration value=""NC"" />
              <xs:enumeration value=""NZ"" />
              <xs:enumeration value=""NI"" />
              <xs:enumeration value=""NE"" />
              <xs:enumeration value=""NG"" />
              <xs:enumeration value=""NU"" />
              <xs:enumeration value=""NF"" />
              <xs:enumeration value=""MP"" />
              <xs:enumeration value=""NO"" />
              <xs:enumeration value=""OM"" />
              <xs:enumeration value=""PK"" />
              <xs:enumeration value=""PW"" />
              <xs:enumeration value=""PS"" />
              <xs:enumeration value=""PA"" />
              <xs:enumeration value=""PG"" />
              <xs:enumeration value=""PY"" />
              <xs:enumeration value=""PE"" />
              <xs:enumeration value=""PH"" />
              <xs:enumeration value=""PN"" />
              <xs:enumeration value=""PL"" />
              <xs:enumeration value=""PT"" />
              <xs:enumeration value=""PR"" />
              <xs:enumeration value=""QA"" />
              <xs:enumeration value=""RE"" />
              <xs:enumeration value=""RO"" />
              <xs:enumeration value=""RU"" />
              <xs:enumeration value=""RW"" />
              <xs:enumeration value=""BL"" />
              <xs:enumeration value=""SH"" />
              <xs:enumeration value=""KN"" />
              <xs:enumeration value=""LC"" />
              <xs:enumeration value=""MF"" />
              <xs:enumeration value=""PM"" />
              <xs:enumeration value=""VC"" />
              <xs:enumeration value=""WS"" />
              <xs:enumeration value=""SM"" />
              <xs:enumeration value=""ST"" />
              <xs:enumeration value=""SA"" />
              <xs:enumeration value=""SN"" />
              <xs:enumeration value=""RS"" />
              <xs:enumeration value=""SC"" />
              <xs:enumeration value=""SL"" />
              <xs:enumeration value=""SG"" />
              <xs:enumeration value=""SX"" />
              <xs:enumeration value=""SK"" />
              <xs:enumeration value=""SI"" />
              <xs:enumeration value=""SB"" />
              <xs:enumeration value=""SO"" />
              <xs:enumeration value=""ZA"" />
              <xs:enumeration value=""GS"" />
              <xs:enumeration value=""SS"" />
              <xs:enumeration value=""ES"" />
              <xs:enumeration value=""LK"" />
              <xs:enumeration value=""SD"" />
              <xs:enumeration value=""SR"" />
              <xs:enumeration value=""SJ"" />
              <xs:enumeration value=""SZ"" />
              <xs:enumeration value=""SE"" />
              <xs:enumeration value=""CH"" />
              <xs:enumeration value=""SY"" />
              <xs:enumeration value=""TW"" />
              <xs:enumeration value=""TJ"" />
              <xs:enumeration value=""TZ"" />
              <xs:enumeration value=""TH"" />
              <xs:enumeration value=""TL"" />
              <xs:enumeration value=""TG"" />
              <xs:enumeration value=""TK"" />
              <xs:enumeration value=""TO"" />
              <xs:enumeration value=""TT"" />
              <xs:enumeration value=""TN"" />
              <xs:enumeration value=""TR"" />
              <xs:enumeration value=""TM"" />
              <xs:enumeration value=""TC"" />
              <xs:enumeration value=""TV"" />
              <xs:enumeration value=""UG"" />
              <xs:enumeration value=""UA"" />
              <xs:enumeration value=""AE"" />
              <xs:enumeration value=""GB"" />
              <xs:enumeration value=""US"" />
              <xs:enumeration value=""UM"" />
              <xs:enumeration value=""UY"" />
              <xs:enumeration value=""UZ"" />
              <xs:enumeration value=""VU"" />
              <xs:enumeration value=""VE"" />
              <xs:enumeration value=""VN"" />
              <xs:enumeration value=""VG"" />
              <xs:enumeration value=""VI"" />
              <xs:enumeration value=""WF"" />
              <xs:enumeration value=""EH"" />
              <xs:enumeration value=""YE"" />
              <xs:enumeration value=""ZM"" />
              <xs:enumeration value=""ZW"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""iso3166-1alpha3"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""ISO Code 3166-1 Alpha-3"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""AFG"" />
              <xs:enumeration value=""ALA"" />
              <xs:enumeration value=""ALB"" />
              <xs:enumeration value=""DZA"" />
              <xs:enumeration value=""ASM"" />
              <xs:enumeration value=""AND"" />
              <xs:enumeration value=""AGO"" />
              <xs:enumeration value=""AIA"" />
              <xs:enumeration value=""ATA"" />
              <xs:enumeration value=""ATG"" />
              <xs:enumeration value=""ARG"" />
              <xs:enumeration value=""ARM"" />
              <xs:enumeration value=""ABW"" />
              <xs:enumeration value=""AUS"" />
              <xs:enumeration value=""AUT"" />
              <xs:enumeration value=""AZE"" />
              <xs:enumeration value=""BHS"" />
              <xs:enumeration value=""BHR"" />
              <xs:enumeration value=""BGD"" />
              <xs:enumeration value=""BRB"" />
              <xs:enumeration value=""BLR"" />
              <xs:enumeration value=""BEL"" />
              <xs:enumeration value=""BLZ"" />
              <xs:enumeration value=""BEN"" />
              <xs:enumeration value=""BMU"" />
              <xs:enumeration value=""BTN"" />
              <xs:enumeration value=""BOL"" />
              <xs:enumeration value=""BES"" />
              <xs:enumeration value=""BIH"" />
              <xs:enumeration value=""BWA"" />
              <xs:enumeration value=""BVT"" />
              <xs:enumeration value=""BRA"" />
              <xs:enumeration value=""IOT"" />
              <xs:enumeration value=""BRN"" />
              <xs:enumeration value=""BGR"" />
              <xs:enumeration value=""BFA"" />
              <xs:enumeration value=""BDI"" />
              <xs:enumeration value=""CPV"" />
              <xs:enumeration value=""KHM"" />
              <xs:enumeration value=""CMR"" />
              <xs:enumeration value=""CAN"" />
              <xs:enumeration value=""CYM"" />
              <xs:enumeration value=""CAF"" />
              <xs:enumeration value=""TCD"" />
              <xs:enumeration value=""CHL"" />
              <xs:enumeration value=""CHN"" />
              <xs:enumeration value=""CXR"" />
              <xs:enumeration value=""CCK"" />
              <xs:enumeration value=""COL"" />
              <xs:enumeration value=""COM"" />
              <xs:enumeration value=""COG"" />
              <xs:enumeration value=""COD"" />
              <xs:enumeration value=""COK"" />
              <xs:enumeration value=""CRI"" />
              <xs:enumeration value=""CIV"" />
              <xs:enumeration value=""HRV"" />
              <xs:enumeration value=""CUB"" />
              <xs:enumeration value=""CUW"" />
              <xs:enumeration value=""CYP"" />
              <xs:enumeration value=""CZE"" />
              <xs:enumeration value=""DNK"" />
              <xs:enumeration value=""DJI"" />
              <xs:enumeration value=""DMA"" />
              <xs:enumeration value=""DOM"" />
              <xs:enumeration value=""ECU"" />
              <xs:enumeration value=""EGY"" />
              <xs:enumeration value=""SLV"" />
              <xs:enumeration value=""GNQ"" />
              <xs:enumeration value=""ERI"" />
              <xs:enumeration value=""EST"" />
              <xs:enumeration value=""ETH"" />
              <xs:enumeration value=""FLK"" />
              <xs:enumeration value=""FRO"" />
              <xs:enumeration value=""FJI"" />
              <xs:enumeration value=""FIN"" />
              <xs:enumeration value=""FRA"" />
              <xs:enumeration value=""GUF"" />
              <xs:enumeration value=""PYF"" />
              <xs:enumeration value=""ATF"" />
              <xs:enumeration value=""GAB"" />
              <xs:enumeration value=""GMB"" />
              <xs:enumeration value=""GEO"" />
              <xs:enumeration value=""DEU"" />
              <xs:enumeration value=""GHA"" />
              <xs:enumeration value=""GIB"" />
              <xs:enumeration value=""GRC"" />
              <xs:enumeration value=""GRL"" />
              <xs:enumeration value=""GRD"" />
              <xs:enumeration value=""GLP"" />
              <xs:enumeration value=""GUM"" />
              <xs:enumeration value=""GTM"" />
              <xs:enumeration value=""GGY"" />
              <xs:enumeration value=""GIN"" />
              <xs:enumeration value=""GNB"" />
              <xs:enumeration value=""GUY"" />
              <xs:enumeration value=""HTI"" />
              <xs:enumeration value=""HMD"" />
              <xs:enumeration value=""VAT"" />
              <xs:enumeration value=""HND"" />
              <xs:enumeration value=""HKG"" />
              <xs:enumeration value=""HUN"" />
              <xs:enumeration value=""ISL"" />
              <xs:enumeration value=""IND"" />
              <xs:enumeration value=""IDN"" />
              <xs:enumeration value=""IRN"" />
              <xs:enumeration value=""IRQ"" />
              <xs:enumeration value=""IRL"" />
              <xs:enumeration value=""IMN"" />
              <xs:enumeration value=""ISR"" />
              <xs:enumeration value=""ITA"" />
              <xs:enumeration value=""JAM"" />
              <xs:enumeration value=""JPN"" />
              <xs:enumeration value=""JEY"" />
              <xs:enumeration value=""JOR"" />
              <xs:enumeration value=""KAZ"" />
              <xs:enumeration value=""KEN"" />
              <xs:enumeration value=""KIR"" />
              <xs:enumeration value=""PRK"" />
              <xs:enumeration value=""KOR"" />
              <xs:enumeration value=""KWT"" />
              <xs:enumeration value=""KGZ"" />
              <xs:enumeration value=""LAO"" />
              <xs:enumeration value=""LVA"" />
              <xs:enumeration value=""LBN"" />
              <xs:enumeration value=""LSO"" />
              <xs:enumeration value=""LBR"" />
              <xs:enumeration value=""LBY"" />
              <xs:enumeration value=""LIE"" />
              <xs:enumeration value=""LTU"" />
              <xs:enumeration value=""LUX"" />
              <xs:enumeration value=""MAC"" />
              <xs:enumeration value=""MKD"" />
              <xs:enumeration value=""MDG"" />
              <xs:enumeration value=""MWI"" />
              <xs:enumeration value=""MYS"" />
              <xs:enumeration value=""MDV"" />
              <xs:enumeration value=""MLI"" />
              <xs:enumeration value=""MLT"" />
              <xs:enumeration value=""MHL"" />
              <xs:enumeration value=""MTQ"" />
              <xs:enumeration value=""MRT"" />
              <xs:enumeration value=""MUS"" />
              <xs:enumeration value=""MYT"" />
              <xs:enumeration value=""MEX"" />
              <xs:enumeration value=""FSM"" />
              <xs:enumeration value=""MDA"" />
              <xs:enumeration value=""MCO"" />
              <xs:enumeration value=""MNG"" />
              <xs:enumeration value=""MNE"" />
              <xs:enumeration value=""MSR"" />
              <xs:enumeration value=""MAR"" />
              <xs:enumeration value=""MOZ"" />
              <xs:enumeration value=""MMR"" />
              <xs:enumeration value=""NAM"" />
              <xs:enumeration value=""NRU"" />
              <xs:enumeration value=""NPL"" />
              <xs:enumeration value=""NLD"" />
              <xs:enumeration value=""NCL"" />
              <xs:enumeration value=""NZL"" />
              <xs:enumeration value=""NIC"" />
              <xs:enumeration value=""NER"" />
              <xs:enumeration value=""NGA"" />
              <xs:enumeration value=""NIU"" />
              <xs:enumeration value=""NFK"" />
              <xs:enumeration value=""MNP"" />
              <xs:enumeration value=""NOR"" />
              <xs:enumeration value=""OMN"" />
              <xs:enumeration value=""PAK"" />
              <xs:enumeration value=""PLW"" />
              <xs:enumeration value=""PSE"" />
              <xs:enumeration value=""PAN"" />
              <xs:enumeration value=""PNG"" />
              <xs:enumeration value=""PRY"" />
              <xs:enumeration value=""PER"" />
              <xs:enumeration value=""PHL"" />
              <xs:enumeration value=""PCN"" />
              <xs:enumeration value=""POL"" />
              <xs:enumeration value=""PRT"" />
              <xs:enumeration value=""PRI"" />
              <xs:enumeration value=""QAT"" />
              <xs:enumeration value=""REU"" />
              <xs:enumeration value=""ROU"" />
              <xs:enumeration value=""RUS"" />
              <xs:enumeration value=""RWA"" />
              <xs:enumeration value=""BLM"" />
              <xs:enumeration value=""SHN"" />
              <xs:enumeration value=""KNA"" />
              <xs:enumeration value=""LCA"" />
              <xs:enumeration value=""MAF"" />
              <xs:enumeration value=""SPM"" />
              <xs:enumeration value=""VCT"" />
              <xs:enumeration value=""WSM"" />
              <xs:enumeration value=""SMR"" />
              <xs:enumeration value=""STP"" />
              <xs:enumeration value=""SAU"" />
              <xs:enumeration value=""SEN"" />
              <xs:enumeration value=""SRB"" />
              <xs:enumeration value=""SYC"" />
              <xs:enumeration value=""SLE"" />
              <xs:enumeration value=""SGP"" />
              <xs:enumeration value=""SXM"" />
              <xs:enumeration value=""SVK"" />
              <xs:enumeration value=""SVN"" />
              <xs:enumeration value=""SLB"" />
              <xs:enumeration value=""SOM"" />
              <xs:enumeration value=""ZAF"" />
              <xs:enumeration value=""SGS"" />
              <xs:enumeration value=""SSD"" />
              <xs:enumeration value=""ESP"" />
              <xs:enumeration value=""LKA"" />
              <xs:enumeration value=""SDN"" />
              <xs:enumeration value=""SUR"" />
              <xs:enumeration value=""SJM"" />
              <xs:enumeration value=""SWZ"" />
              <xs:enumeration value=""SWE"" />
              <xs:enumeration value=""CHE"" />
              <xs:enumeration value=""SYR"" />
              <xs:enumeration value=""TWN"" />
              <xs:enumeration value=""TJK"" />
              <xs:enumeration value=""TZA"" />
              <xs:enumeration value=""THA"" />
              <xs:enumeration value=""TLS"" />
              <xs:enumeration value=""TGO"" />
              <xs:enumeration value=""TKL"" />
              <xs:enumeration value=""TON"" />
              <xs:enumeration value=""TTO"" />
              <xs:enumeration value=""TUN"" />
              <xs:enumeration value=""TUR"" />
              <xs:enumeration value=""TKM"" />
              <xs:enumeration value=""TCA"" />
              <xs:enumeration value=""TUV"" />
              <xs:enumeration value=""UGA"" />
              <xs:enumeration value=""UKR"" />
              <xs:enumeration value=""ARE"" />
              <xs:enumeration value=""GBR"" />
              <xs:enumeration value=""USA"" />
              <xs:enumeration value=""UMI"" />
              <xs:enumeration value=""URY"" />
              <xs:enumeration value=""UZB"" />
              <xs:enumeration value=""VUT"" />
              <xs:enumeration value=""VEN"" />
              <xs:enumeration value=""VNM"" />
              <xs:enumeration value=""VGB"" />
              <xs:enumeration value=""VIR"" />
              <xs:enumeration value=""WLF"" />
              <xs:enumeration value=""ESH"" />
              <xs:enumeration value=""YEM"" />
              <xs:enumeration value=""ZMB"" />
              <xs:enumeration value=""ZWE"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""iso3166-1numeric"" nillable=""true"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo notes=""ISO Code 3166-1 Numerisch"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:simpleType>
            <xs:restriction base=""xs:string"">
              <xs:enumeration value=""004"" />
              <xs:enumeration value=""248"" />
              <xs:enumeration value=""008"" />
              <xs:enumeration value=""012"" />
              <xs:enumeration value=""016"" />
              <xs:enumeration value=""020"" />
              <xs:enumeration value=""024"" />
              <xs:enumeration value=""660"" />
              <xs:enumeration value=""010"" />
              <xs:enumeration value=""028"" />
              <xs:enumeration value=""032"" />
              <xs:enumeration value=""051"" />
              <xs:enumeration value=""533"" />
              <xs:enumeration value=""036"" />
              <xs:enumeration value=""040"" />
              <xs:enumeration value=""031"" />
              <xs:enumeration value=""044"" />
              <xs:enumeration value=""048"" />
              <xs:enumeration value=""050"" />
              <xs:enumeration value=""052"" />
              <xs:enumeration value=""112"" />
              <xs:enumeration value=""056"" />
              <xs:enumeration value=""084"" />
              <xs:enumeration value=""204"" />
              <xs:enumeration value=""060"" />
              <xs:enumeration value=""064"" />
              <xs:enumeration value=""068"" />
              <xs:enumeration value=""535"" />
              <xs:enumeration value=""070"" />
              <xs:enumeration value=""072"" />
              <xs:enumeration value=""074"" />
              <xs:enumeration value=""076"" />
              <xs:enumeration value=""086"" />
              <xs:enumeration value=""096"" />
              <xs:enumeration value=""100"" />
              <xs:enumeration value=""854"" />
              <xs:enumeration value=""108"" />
              <xs:enumeration value=""132"" />
              <xs:enumeration value=""116"" />
              <xs:enumeration value=""120"" />
              <xs:enumeration value=""124"" />
              <xs:enumeration value=""136"" />
              <xs:enumeration value=""140"" />
              <xs:enumeration value=""148"" />
              <xs:enumeration value=""152"" />
              <xs:enumeration value=""156"" />
              <xs:enumeration value=""162"" />
              <xs:enumeration value=""166"" />
              <xs:enumeration value=""170"" />
              <xs:enumeration value=""174"" />
              <xs:enumeration value=""178"" />
              <xs:enumeration value=""180"" />
              <xs:enumeration value=""184"" />
              <xs:enumeration value=""188"" />
              <xs:enumeration value=""384"" />
              <xs:enumeration value=""191"" />
              <xs:enumeration value=""192"" />
              <xs:enumeration value=""531"" />
              <xs:enumeration value=""196"" />
              <xs:enumeration value=""203"" />
              <xs:enumeration value=""208"" />
              <xs:enumeration value=""262"" />
              <xs:enumeration value=""212"" />
              <xs:enumeration value=""214"" />
              <xs:enumeration value=""218"" />
              <xs:enumeration value=""818"" />
              <xs:enumeration value=""222"" />
              <xs:enumeration value=""226"" />
              <xs:enumeration value=""232"" />
              <xs:enumeration value=""233"" />
              <xs:enumeration value=""231"" />
              <xs:enumeration value=""238"" />
              <xs:enumeration value=""234"" />
              <xs:enumeration value=""242"" />
              <xs:enumeration value=""246"" />
              <xs:enumeration value=""250"" />
              <xs:enumeration value=""254"" />
              <xs:enumeration value=""258"" />
              <xs:enumeration value=""260"" />
              <xs:enumeration value=""266"" />
              <xs:enumeration value=""270"" />
              <xs:enumeration value=""268"" />
              <xs:enumeration value=""276"" />
              <xs:enumeration value=""288"" />
              <xs:enumeration value=""292"" />
              <xs:enumeration value=""300"" />
              <xs:enumeration value=""304"" />
              <xs:enumeration value=""308"" />
              <xs:enumeration value=""312"" />
              <xs:enumeration value=""316"" />
              <xs:enumeration value=""320"" />
              <xs:enumeration value=""831"" />
              <xs:enumeration value=""324"" />
              <xs:enumeration value=""624"" />
              <xs:enumeration value=""328"" />
              <xs:enumeration value=""332"" />
              <xs:enumeration value=""334"" />
              <xs:enumeration value=""336"" />
              <xs:enumeration value=""340"" />
              <xs:enumeration value=""344"" />
              <xs:enumeration value=""348"" />
              <xs:enumeration value=""352"" />
              <xs:enumeration value=""356"" />
              <xs:enumeration value=""360"" />
              <xs:enumeration value=""364"" />
              <xs:enumeration value=""368"" />
              <xs:enumeration value=""372"" />
              <xs:enumeration value=""833"" />
              <xs:enumeration value=""376"" />
              <xs:enumeration value=""380"" />
              <xs:enumeration value=""388"" />
              <xs:enumeration value=""392"" />
              <xs:enumeration value=""832"" />
              <xs:enumeration value=""400"" />
              <xs:enumeration value=""398"" />
              <xs:enumeration value=""404"" />
              <xs:enumeration value=""296"" />
              <xs:enumeration value=""408"" />
              <xs:enumeration value=""410"" />
              <xs:enumeration value=""414"" />
              <xs:enumeration value=""417"" />
              <xs:enumeration value=""418"" />
              <xs:enumeration value=""428"" />
              <xs:enumeration value=""422"" />
              <xs:enumeration value=""426"" />
              <xs:enumeration value=""430"" />
              <xs:enumeration value=""434"" />
              <xs:enumeration value=""438"" />
              <xs:enumeration value=""440"" />
              <xs:enumeration value=""442"" />
              <xs:enumeration value=""446"" />
              <xs:enumeration value=""807"" />
              <xs:enumeration value=""450"" />
              <xs:enumeration value=""454"" />
              <xs:enumeration value=""458"" />
              <xs:enumeration value=""462"" />
              <xs:enumeration value=""466"" />
              <xs:enumeration value=""470"" />
              <xs:enumeration value=""584"" />
              <xs:enumeration value=""474"" />
              <xs:enumeration value=""478"" />
              <xs:enumeration value=""480"" />
              <xs:enumeration value=""175"" />
              <xs:enumeration value=""484"" />
              <xs:enumeration value=""583"" />
              <xs:enumeration value=""498"" />
              <xs:enumeration value=""492"" />
              <xs:enumeration value=""496"" />
              <xs:enumeration value=""499"" />
              <xs:enumeration value=""500"" />
              <xs:enumeration value=""504"" />
              <xs:enumeration value=""508"" />
              <xs:enumeration value=""104"" />
              <xs:enumeration value=""516"" />
              <xs:enumeration value=""520"" />
              <xs:enumeration value=""524"" />
              <xs:enumeration value=""528"" />
              <xs:enumeration value=""540"" />
              <xs:enumeration value=""554"" />
              <xs:enumeration value=""558"" />
              <xs:enumeration value=""562"" />
              <xs:enumeration value=""566"" />
              <xs:enumeration value=""570"" />
              <xs:enumeration value=""574"" />
              <xs:enumeration value=""580"" />
              <xs:enumeration value=""578"" />
              <xs:enumeration value=""512"" />
              <xs:enumeration value=""586"" />
              <xs:enumeration value=""585"" />
              <xs:enumeration value=""275"" />
              <xs:enumeration value=""591"" />
              <xs:enumeration value=""598"" />
              <xs:enumeration value=""600"" />
              <xs:enumeration value=""604"" />
              <xs:enumeration value=""608"" />
              <xs:enumeration value=""612"" />
              <xs:enumeration value=""616"" />
              <xs:enumeration value=""620"" />
              <xs:enumeration value=""630"" />
              <xs:enumeration value=""634"" />
              <xs:enumeration value=""638"" />
              <xs:enumeration value=""642"" />
              <xs:enumeration value=""643"" />
              <xs:enumeration value=""646"" />
              <xs:enumeration value=""652"" />
              <xs:enumeration value=""654"" />
              <xs:enumeration value=""659"" />
              <xs:enumeration value=""662"" />
              <xs:enumeration value=""663"" />
              <xs:enumeration value=""666"" />
              <xs:enumeration value=""670"" />
              <xs:enumeration value=""882"" />
              <xs:enumeration value=""674"" />
              <xs:enumeration value=""678"" />
              <xs:enumeration value=""682"" />
              <xs:enumeration value=""686"" />
              <xs:enumeration value=""688"" />
              <xs:enumeration value=""690"" />
              <xs:enumeration value=""694"" />
              <xs:enumeration value=""702"" />
              <xs:enumeration value=""534"" />
              <xs:enumeration value=""703"" />
              <xs:enumeration value=""705"" />
              <xs:enumeration value=""090"" />
              <xs:enumeration value=""706"" />
              <xs:enumeration value=""710"" />
              <xs:enumeration value=""239"" />
              <xs:enumeration value=""728"" />
              <xs:enumeration value=""724"" />
              <xs:enumeration value=""144"" />
              <xs:enumeration value=""729"" />
              <xs:enumeration value=""740"" />
              <xs:enumeration value=""744"" />
              <xs:enumeration value=""748"" />
              <xs:enumeration value=""752"" />
              <xs:enumeration value=""756"" />
              <xs:enumeration value=""760"" />
              <xs:enumeration value=""158"" />
              <xs:enumeration value=""762"" />
              <xs:enumeration value=""834"" />
              <xs:enumeration value=""764"" />
              <xs:enumeration value=""626"" />
              <xs:enumeration value=""768"" />
              <xs:enumeration value=""772"" />
              <xs:enumeration value=""776"" />
              <xs:enumeration value=""780"" />
              <xs:enumeration value=""788"" />
              <xs:enumeration value=""792"" />
              <xs:enumeration value=""795"" />
              <xs:enumeration value=""796"" />
              <xs:enumeration value=""798"" />
              <xs:enumeration value=""800"" />
              <xs:enumeration value=""804"" />
              <xs:enumeration value=""784"" />
              <xs:enumeration value=""826"" />
              <xs:enumeration value=""840"" />
              <xs:enumeration value=""581"" />
              <xs:enumeration value=""858"" />
              <xs:enumeration value=""860"" />
              <xs:enumeration value=""548"" />
              <xs:enumeration value=""862"" />
              <xs:enumeration value=""704"" />
              <xs:enumeration value=""092"" />
              <xs:enumeration value=""850"" />
              <xs:enumeration value=""876"" />
              <xs:enumeration value=""732"" />
              <xs:enumeration value=""887"" />
              <xs:enumeration value=""894"" />
              <xs:enumeration value=""716"" />
            </xs:restriction>
          </xs:simpleType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public country_10() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "country";
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
