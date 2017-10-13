<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xsd="http://schemas.xsddoc.codeplex.com/schemaDoc/2009/3"
                xmlns:ddue="http://ddue.schemas.microsoft.com/authoring/2003/5"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:b="http://schemas.microsoft.com/BizTalk/2003" >
  <xsl:output method="xml" indent="yes"/>


  <!--<xsl:template match="*">

    <xsl:variable name="schemaDoc" select="xs:annotation/xs:appinfo/xsd:schemaDoc" />
    <xsl:variable name="doc" select="xs:annotation/xs:documentation/text()" />
    <xsl:variable name="record" select="xs:appinfo/b:recordInfo" />
    <xsl:variable name="field" select="xs:appinfo/b:fieldInfo" />

    <xsl:choose>
      <xsl:when test="$schemaDoc">
        <xsl:apply-templates select="$schemaDoc" mode="copy"/>
      </xsl:when>
      <xsl:when test="$doc">
        <xsd:schemaDoc>
          <ddue:summary>
            <ddue:para>
              <xsl:apply-templates select="$doc" mode="copy"/>
            </ddue:para>
          </ddue:summary>
        </xsd:schemaDoc>
      </xsl:when>
        <xsl:when test="$record">
          <xsd:schemaDoc>
            <ddue:summary>
            <ddue:para>
              <xsl:value-of select="$record/@notes"/>
            </ddue:para>
          </ddue:summary>
          </xsd:schemaDoc>
        </xsl:when>
        <xsl:when test="$field">
          <xsd:schemaDoc>
            <ddue:summary>
            <ddue:para>
              <xsl:value-of select="$field/@notes"/>
            </ddue:para>
          </ddue:summary>
          </xsd:schemaDoc>
        </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="@*|node()" mode="copy">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" mode="copy"/>
    </xsl:copy>
  </xsl:template>-->

  <xsl:template match="*">
    <xsl:variable name="schema" select="xs:annotation/xs:appinfo/b:schemaInfo" />
    <xsl:variable name="record" select="xs:annotation/xs:appinfo/b:recordInfo" />
    <xsl:variable name="field" select="xs:annotation/xs:appinfo/b:fieldInfo" />

    <xsd:schemaDoc>
      <xsl:choose>
        <xsl:when test="$schema">
          <ddue:summary>
            <ddue:para>
              <xsl:value-of select="$schema/@schema_name"/>
            </ddue:para>
          </ddue:summary>
        </xsl:when>
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


  <!--<xsl:template match="*">
    <xsl:apply-templates select="xs:annotation" />
  </xsl:template>

  <xsl:template match="*">
    <xsl:variable name="record" select="xs:appinfo/b:recordInfo" />
    <xsl:variable name="field" select="xs:appinfo/b:fieldInfo" />

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
  </xsl:template>-->

</xsl:stylesheet>
