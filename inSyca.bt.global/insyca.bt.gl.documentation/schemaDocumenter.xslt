<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xsd="http://schemas.xsddoc.codeplex.com/schemaDoc/2009/3"
                xmlns:ddue="http://ddue.schemas.microsoft.com/authoring/2003/5"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:b="http://schemas.microsoft.com/BizTalk/2003" >
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="*">
    <xsl:variable name="record" select="xs:annotation/xs:appinfo/b:recordInfo" />
    <xsl:variable name="field" select="xs:annotation/xs:appinfo/b:fieldInfo" />

    <xsd:schemaDoc>
      <xsl:choose>
        <xsl:when test="$record">
          <ddue:summary>
            <ddue:para>
              <xsl:value-of select="$record/@notes"/>
            </ddue:para>
          </ddue:summary>
        </xsl:when>
        <xsl:when test="$field">
          <ddue:summary>
            <ddue:para>
              <xsl:value-of select="$field/@notes"/>
            </ddue:para>
          </ddue:summary>
        </xsl:when>
      </xsl:choose>
    </xsd:schemaDoc>
  </xsl:template>
</xsl:stylesheet>
