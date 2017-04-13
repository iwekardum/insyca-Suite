<!-- XSLT for mass copy without namespace-->
<!-- param1: source element name-->
<xsl:template name = "massCopyWithoutNamespace">
  <xsl:param name="param1" />
  <xsl:apply-templates mode="copy-no-ns" select="//*[local-name()=$param1]"/>
</xsl:template>

<xsl:template mode="copy-no-ns" match="*">
  <xsl:element name="{name(.)}">
    <xsl:copy-of select="@*"/>
    <xsl:apply-templates mode="copy-no-ns" select="node()"/>
  </xsl:element>
</xsl:template>

<!-- XSLT for mass copy with specific namespace-->
<!-- param1: source element name-->
<!-- param2: namespace-->
<xsl:template name = "massCopyWithNamespace">
  <xsl:param name="param1" />
  <xsl:param name="param2" />
  <xsl:apply-templates mode="copy-no-ns" select="//*[local-name()=$param1]">
    <xsl:with-param name="param1" select="$param2"/>
  </xsl:apply-templates>
</xsl:template>

<xsl:template mode="copy-no-ns" match="*">
  <xsl:param name="param1" />
  <xsl:element name="{name(.)}" namespace="{$param1}">
    <xsl:copy-of select="@*"/>
    <xsl:apply-templates mode="copy-no-ns" select="node()"/>
  </xsl:element>
</xsl:template>
