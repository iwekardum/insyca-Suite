<!-- XSLT for empty node (if parent exists)-->
<!-- param1: source element-->
<!-- param2: name of destination element-->
<xsl:template name="nilEmptyValue">
  <xsl:param name="param1" />
  <xsl:param name="param2" />
  <xsl:element name="{$param2}">
    <xsl:choose>
      <xsl:when test="$param1 != ''">
        <xsl:value-of select="$param1" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="xsi:nil">true</xsl:attribute>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:element>
</xsl:template>

<!-- XSLT for empty node (if parent exists)-->
<!-- param1: source element-->
<!-- param2: name of destination element-->
<xsl:template name="EmptyValue">
  <xsl:param name="param1" />
  <xsl:param name="param2" />
  <xsl:choose>
    <xsl:when test="$param1 != ''">
      <xsl:element name="{$param2}">
        <xsl:value-of select="$param1" />
      </xsl:element>
    </xsl:when>
    <xsl:otherwise>
      <xsl:element name="{$param2}"/>
    </xsl:otherwise>
  </xsl:choose>
</xsl:template>

<!-- XSLT for empty node (conditional mapping)-->
<!-- param1: source element-->
<!-- param2: name of destination element-->
<!-- param3: condition that evaluates true-->
<xsl:template name="nilEmptyValueConditional">
  <xsl:param name="param1" />
  <xsl:param name="param2" />
  <xsl:param name="param3" />
  <xsl:if test="$param3">
    <xsl:element name="{$param2}">
      <xsl:choose>
        <xsl:when test="$param1 != ''">
          <xsl:value-of select="$param1" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:attribute name="xsi:nil">true</xsl:attribute>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:element>
  </xsl:if>
</xsl:template>

<!-- XSLT for empty node (conditional mapping)-->
<!-- param1: element to proof for boolean value-->
<!-- param2: source element-->
<!-- param3: name of destination element-->
<xsl:template name="nilEmptyValueConditionalVar1">
  <xsl:param name="param1" />
  <xsl:param name="param2" />
  <xsl:param name="param3" />
  <xsl:element name="{$param3}">
    <xsl:choose>
      <xsl:when test="$param1 = 'true' and $param2 != ''">
        <xsl:value-of select="$param2" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="xsi:nil">true</xsl:attribute>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:element>
</xsl:template>

<!-- XSLT for empty node (conditional mapping)-->
<!-- param1: element to proof for boolean value-->
<!-- param2: source element-->
<!-- param3: name of destination element-->
<xsl:template name="nilEmptyDateValue">
  <xsl:param name="param1" />
  <xsl:param name="param2" />
  <xsl:element name="{$param2}">
    <xsl:choose>
      <xsl:when test="$param1 != '1900-01-01' and $param1 != ''">
        <xsl:value-of select="$param1" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="xsi:nil">true</xsl:attribute>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:element>
</xsl:template>

<!-- XSLT for empty node (depends on parent node)-->
<!-- param1: link to parent node-->
<!-- param2: name of destination element-->
<!-- param3: source element-->
<xsl:template name="nilEmptyValue_001">
  <xsl:param name="parentNode" />
  <xsl:param name="elementName" />
  <xsl:param name="elementValue" />
  <xsl:element name="{$elementName}">
    <xsl:choose>
      <xsl:when test="$elementValue != ''">
        <xsl:value-of select="$elementValue" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="xsi:nil">true</xsl:attribute>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:element>
</xsl:template>