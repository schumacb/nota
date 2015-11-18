<xsl:stylesheet xmlns:nota="http://nota.org/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
 xsi:schemaLocation="http://nota.org/ ..\render\books\Regelwerke\Grundregeln\talente\talent.xsd" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="nota:Talente">
    <TABLE BORDER="2">
      <TR>
        <TD>ISBN</TD>
        <TD>Title</TD>
        <TD>Price</TD>
      </TR>
      <xsl:apply-templates select="nota:Talent"/>
    </TABLE>
  </xsl:template>
  <xsl:template match="nota:Talent">
    <TR>
      <TD>
        <xsl:value-of select="@Name"/>
      </TD>
      <TD>
        <xsl:value-of select="nota:Beschreibung"/>
      </TD>
      <TD>
        <p>bla</p>
      </TD>
    </TR>
  </xsl:template>
</xsl:stylesheet>