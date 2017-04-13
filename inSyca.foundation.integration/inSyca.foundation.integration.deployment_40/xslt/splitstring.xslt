<xsl:template name="WritePropertyNodeTemplate">
  <xsl:param name="input" />

  <!-- create property node -->
  <!-- create single instance children nodes -->
  <!-- call splitter template which accepts the "|" separated string -->
  <xsl:choose>
    <xsl:when test="string-length($input) > 0">
      <xsl:call-template name="StringSplit">
        <xsl:with-param name="val" select="$input" />
      </xsl:call-template>
    </xsl:when>
    <xsl:otherwise>
      <xsl:element name="ns4:reference">
        <xsl:element name="referencetype">
          <xsl:attribute name="xsi:nil">true</xsl:attribute>
        </xsl:element>
        <xsl:element name="reference">
          <xsl:attribute name="xsi:nil">true</xsl:attribute>
        </xsl:element>
      </xsl:element>
    </xsl:otherwise>
  </xsl:choose>

</xsl:template>

<!-- This template accepts a string and pulls out the value before the designated delimiter -->
<xsl:template name="StringSplit">
  <xsl:param name="val" />

  <!-- do a check to see if the input string (still) has a "|" in it -->
  <xsl:choose>
    <xsl:when test="contains($val, ',')">
      <!-- pull out the value of the string before the "|" delimiter -->
      <xsl:element name="ns4:reference">
        <referencetype>
          <xsl:value-of select="substring-before(substring-before($val, ','), '=')" />
        </referencetype>
        <reference>
          <xsl:value-of select="substring-after(substring-before($val, ','), '=')" />
        </reference>
      </xsl:element>
      <!-- recursively call this template and pass in value AFTER the "|" delimiter -->
      <xsl:call-template name="StringSplit">
        <xsl:with-param name="val" select="substring-after($val, ',')" />
      </xsl:call-template>
    </xsl:when>
    <xsl:otherwise>
      <!-- if there is no more delimiter values, print out the whole string -->
      <xsl:element name="ns4:reference">
        <referencetype>
          <xsl:value-of select="substring-before($val, '=')" />
        </referencetype>
        <reference>
          <xsl:value-of select="substring-after($val, '=')" />
        </reference>
      </xsl:element>
    </xsl:otherwise>
  </xsl:choose>

</xsl:template>