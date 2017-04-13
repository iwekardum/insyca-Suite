<!-- XSLT for generate key value pairs-->
<xsl:template name="keyValue_001">
  <xsl:param name="recordName" />
  <xsl:param name="keyElementName" />
  <xsl:param name="keyElementValue" />
  <xsl:param name="valueElementName" />
  <xsl:param name="valueElementValue" />
  <xsl:element name="{$recordName}">
    <xsl:element name="{$keyElementName}">
      <xsl:value-of select="$keyElementValue" />
    </xsl:element>
    <xsl:element name="{$valueElementName}">
      <xsl:value-of select="$valueElementValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>

<!-- XSLT for generate identification record-->
<xsl:template name="identification_001">
  <xsl:param name="typeValue" />
  <xsl:param name="numberValue" />
  <xsl:param name="labelValue" />
  <xsl:element name="identification">
    <xsl:element name="type">
      <xsl:value-of select="typeValue" />
    </xsl:element>
    <xsl:element name="number">
      <xsl:value-of select="numberValue" />
    </xsl:element>
    <xsl:element name="label">
      <xsl:value-of select="labelValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>

<!-- XSLT for generate identification record with namespace-->
<xsl:template name="identification_002">
  <xsl:param name="namespaceValue" />
  <xsl:param name="typeValue" />
  <xsl:param name="numberValue" />
  <xsl:param name="labelValue" />
  <xsl:element name="identification" namespace="{$namespaceValue}">
    <xsl:element name="type">
      <xsl:value-of select="$typeValue" />
    </xsl:element>
    <xsl:element name="number">
      <xsl:value-of select="$numberValue" />
    </xsl:element>
    <xsl:element name="label">
      <xsl:value-of select="$labelValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>

<!-- XSLT for generate reference record-->
<xsl:template name="reference_001">
  <xsl:param name="typeValue" />
  <xsl:param name="labelValue" />
  <xsl:element name="reference">
    <xsl:element name="type">
      <xsl:value-of select="typeValue" />
    </xsl:element>
    <xsl:element name="label">
      <xsl:value-of select="labelValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>

<!-- XSLT for generate phone record-->
<xsl:template name="phone_001">
  <xsl:param name="typeValue" />
  <xsl:param name="completeValue" />
  <xsl:element name="phone">
    <xsl:element name="type">
      <xsl:value-of select="typeValue" />
    </xsl:element>
    <xsl:element name="country">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="city">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="localnumber">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="extension">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="complete">
      <xsl:value-of select="completeValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>

<!-- XSLT for generate phone record with namespace-->
<xsl:template name="phone_002">
  <xsl:param name="namespaceValue" />
  <xsl:param name="typeValue" />
  <xsl:param name="completeValue" />
  <xsl:element name="phone"  namespace="{$namespaceValue}">
    <xsl:element name="type">
      <xsl:value-of select="$typeValue" />
    </xsl:element>
    <xsl:element name="country">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="city">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="localnumber">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="extension">
      <xsl:attribute name="xsi:nil">true</xsl:attribute>
    </xsl:element>
    <xsl:element name="complete">
      <xsl:value-of select="$completeValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>

<!-- XSLT for generate amount record-->
<xsl:template name="amount_001">
  <xsl:param name="typeValue" />
  <xsl:param name="grossamountValue" />
  <xsl:param name="netamountValue" />
  <xsl:param name="taxamountValue" />
  <xsl:param name="currencyValue" />
  <xsl:param name="exchangerateValue" />
  <xsl:element name="amount">
    <xsl:element name="type">
      <xsl:value-of select="typeValue" />
    </xsl:element>
    <xsl:element name="grossamount">
      <xsl:value-of select="grossamountValue" />
    </xsl:element>
    <xsl:element name="netamount">
      <xsl:value-of select="netamountValue" />
    </xsl:element>
    <xsl:element name="taxamount">
      <xsl:value-of select="taxamountValue" />
    </xsl:element>
    <xsl:element name="currency">
      <xsl:value-of select="currencyValue" />
    </xsl:element>
    <xsl:element name="exchangerate">
      <xsl:value-of select="exchangerateValue" />
    </xsl:element>
  </xsl:element>
</xsl:template>
