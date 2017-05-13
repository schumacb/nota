<xsl:stylesheet xml:space="preserve" 
  xmlns:nota="http://nota.org/schema/nota" 
  xmlns:geschöpf="http://nota.org/schema/geschöpf" 
  xmlns:kultur="http://nota.org/schema/kultur" 
  xmlns:profession="http://nota.org/schema/profession" 
  xmlns:talent="http://nota.org/schema/talent" 
  xmlns:fertigkeit="http://nota.org/schema/fertigkeit" 
  xmlns:besonderheit="http://nota.org/schema/besonderheit" 
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://nota.org/schema/nota ..\..\..\..\..\static\schema\nota.xsd                http://nota.org/schema/geschöpf ..\..\..\..\..\static\schema\geschöpf.xsd                http://nota.org/schema/kultur ..\..\..\..\..\static\schema\kultur.xsd                http://nota.org/schema/profession ..\..\..\..\..\static\schema\profession.xsd                http://nota.org/schema/talent ..\..\..\..\..\static\schema\talent.xsd                http://nota.org/schema/fertigkeit ..\..\..\..\..\static\schema\fertigkeit.xsd                 http://nota.org/schema/besonderheit ..\..\..\..\..\static\schema\besonderheit.xsd" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="text" indent="no" />
  <xsl:template match="nota:Daten">
    <xsl:apply-templates select="besonderheit:Besonderheiten"/>
  </xsl:template>

  <xsl:template match="besonderheit:Besonderheiten">
    <xsl:apply-templates select="besonderheit:Besonderheit[besonderheit:Bedingung/besonderheit:Besonderheit[@Name='Auserwählter']]"/>
  </xsl:template>


  <xsl:template match="besonderheit:Besonderheit">
:::Gabe

::<xsl:value-of select="@Name"/>::{.Titel}

:::Beschreibung

<xsl:value-of select="besonderheit:Beschreibung"/>

::: 

  </xsl:template>
  <xsl:template match="*">[FEHLER IM XSLT]</xsl:template>
</xsl:stylesheet>