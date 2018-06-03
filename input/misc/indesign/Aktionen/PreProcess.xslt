<xsl:stylesheet
        xmlns:nota="http://nota.org/schema/nota"
        xmlns:lebewesen="http://nota.org/schema/lebewesen"
        xmlns:kultur="http://nota.org/schema/kultur"
        xmlns:profession="http://nota.org/schema/profession"
        xmlns:talent="http://nota.org/schema/talent"
        xmlns:aktionen="http://nota.org/schema/kampf/aktionen"
        xmlns:fertigkeit="http://nota.org/schema/fertigkeit"
        xmlns:besonderheit="http://nota.org/schema/besonderheit"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
 xsi:schemaLocation="http://nota.org/schema/nota ..\..\..\..\static\schema\nota.xsd
                http://nota.org/schema/lebewesen ..\..\..\..\static\schema\lebewesen.xsd
                http://nota.org/schema/kampf/aktionen ..\..\..\..\static\schema\kampf\aktionen.xsd
                http://nota.org/schema/kultur ..\..\..\..\static\schema\kultur.xsd
                http://nota.org/schema/profession ..\..\..\..\static\schema\profession.xsd
                http://nota.org/schema/talent ..\..\..\..\static\schema\talent.xsd
                http://nota.org/schema/fertigkeit ..\..\..\..\static\schema\fertigkeit.xsd 
                http://nota.org/schema/besonderheit ..\..\..\..\static\schema\besonderheit.xsd" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="nota:Daten">
<xsl:apply-templates select="aktionen:Aktionen"/>  
</xsl:template>

 <xsl:template match="aktionen:Aktionen">
<Aktionen>
<xsl:apply-templates select="aktionen:Aktion"/>
</Aktionen>
</xsl:template>

<xsl:template match="aktionen:Aktion">
<Aktion>
  <Name>
    <xsl:value-of select="@Name"/>
  </Name>
  <Kosten>
    <xsl:value-of select="@Kosten"/>
  </Kosten>
  <Typ>
    <xsl:value-of select="@Typ"/>
  </Typ>

   <xsl:choose>
         <xsl:when test="./aktionen:Mod">
  <Modifikation><xsl:value-of select="./aktionen:Mod"/></Modifikation>
         </xsl:when>
         <xsl:otherwise>
<Modifikation>-</Modifikation>
         </xsl:otherwise>
       </xsl:choose>

<Beschreibung><xsl:value-of select="aktionen:Beschreibung"/></Beschreibung>
</Aktion>
</xsl:template>


<xsl:template match="aktionen:Mod">ModificaitonSample</xsl:template>

<xsl:template match="*">
<Error>[FEHLER IM XSLT] <xsl:value-of select ="name(.)"/> </Error>
</xsl:template>

</xsl:stylesheet>