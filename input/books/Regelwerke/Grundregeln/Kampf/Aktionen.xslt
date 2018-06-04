<xsl:stylesheet 
  xml:space="preserve"
  xmlns:nota="http://nota.org/schema/nota" 
  xmlns:lebewesen="http://nota.org/schema/lebewesen" 
  xmlns:kultur="http://nota.org/schema/kultur" 
  xmlns:profession="http://nota.org/schema/profession" 
  xmlns:talent="http://nota.org/schema/talent" 
  xmlns:fertigkeit="http://nota.org/schema/fertigkeit" 
  xmlns:besonderheit="http://nota.org/schema/besonderheit" 
  xmlns:aktionen="http://nota.org/schema/kampf/aktionen" 
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://nota.org/schema/nota ..\..\..\..\..\static\schema\nota.xsd                http://nota.org/schema/lebewesen ..\..\..\..\..\static\schema\lebewesen.xsd                http://nota.org/schema/kultur ..\..\..\..\..\static\schema\kultur.xsd                http://nota.org/schema/profession ..\..\..\..\..\static\schema\profession.xsd                http://nota.org/schema/talent ..\..\..\..\..\static\schema\talent.xsd                http://nota.org/schema/fertigkeit ..\..\..\..\..\static\schema\fertigkeit.xsd                 http://nota.org/schema/besonderheit ..\..\..\..\..\static\schema\besonderheit.xsd                 http://nota.org/schema/kampf/aktionen ..\..\..\..\..\static\schema\kampf\aktionen.xsd" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html" indent="no" />
  <xsl:template match="nota:Daten">
    <xsl:apply-templates select="aktionen:Aktionen"/>
  </xsl:template>
  <xsl:template match="aktionen:Aktionen">

# Aktionen

<xsl:apply-templates select="aktionen:Aktion"/>
</xsl:template>

  <xsl:template match="aktionen:Aktion" >

:::Action

## <xsl:value-of select="@Name"/>  

<xsl:value-of select="aktionen:Beschreibung"/>

        <xsl:if test="./aktionen:Bedingung">
### Bedingung
<xsl:value-of select="./aktionen:Bedingung"/>
        </xsl:if>
        <xsl:if test="./aktionen:InstantEffekt">
### Augenblicklicher Effekt
<xsl:value-of select="./aktionen:InstantEffekt"/>
        </xsl:if>
        <xsl:if test="./aktionen:GarantierterEffekt">
### Garantierter Effekt
<xsl:value-of select="./aktionen:GarantierterEffekt"/>
        </xsl:if>
        <xsl:if test="./aktionen:OffensivErfolg">
### Offensiver Erfolg
<xsl:value-of select="./aktionen:OffensivErfolg"/>
        </xsl:if>
        <xsl:if test="./aktionen:DefensivErfolg">
### Defensiver Erfolg
<xsl:value-of select="./aktionen:DefensivErfolg"/>
        </xsl:if>
        <xsl:if test="./aktionen:OffensivMiserfolg">
### Offensiver Misserfolg
<xsl:value-of select="./aktionen:OffensivMiserfolg"/>
        </xsl:if>
        <xsl:if test="./aktionen:DefensivMiserfolg">
### Defensiver Misserfolg
<xsl:value-of select="./aktionen:DefensivMiserfolg"/>
        </xsl:if>
**Ausdauerkosten** _<xsl:value-of select="@Kosten"/>_

**Typ** _<xsl:choose>
              <xsl:when test="@Typ='Offensiv'">Offensiv</xsl:when>
              <xsl:when test="@Typ='Defensiv'">Defensiv</xsl:when>
              <xsl:when test="@Typ='Ausgeglieschen'">Offensiv und Passiv</xsl:when>
              <xsl:when test="@Typ='Frei'">Frei</xsl:when>
              <xsl:when test="@Typ='Sekundär'">Sekundär</xsl:when>
              <xsl:when test="@Typ='Unterstützend'">Unterstützend</xsl:when>
              <xsl:otherwise>[Ungültiger wert] (<xsl:value-of select="@Typ"/>)</xsl:otherwise>
            </xsl:choose>_
          <xsl:if test="./aktionen:Mod">
**Modifikation**

<xsl:if test="./aktionen:Mod/@ModifierType eq 'Bonus'">
::&#65291;<xsl:apply-templates select="./aktionen:Mod/*"/>::{.bonus}
</xsl:if>
<xsl:if test="./aktionen:Mod/@ModifierType eq 'Malus'">
::&#x2212;<xsl:apply-templates select="./aktionen:Mod/*"/>::{.malus}
</xsl:if>
</xsl:if>
:::
</xsl:template>

<xsl:template match="aktionen:ConcreteModValueType"><xsl:value-of select="@Value"/><xsl:if test="@Type='Percent'">%</xsl:if></xsl:template>

<xsl:template match="aktionen:VariableModValueType"><xsl:value-of select="@Value"/></xsl:template>

  <xsl:template match="aktionen:AddModValueType"><xsl:apply-templates select="./*[1]"/> <xsl:for-each select="./*[position()>1]"> &#65291; <xsl:apply-templates select="."/></xsl:for-each></xsl:template>

  <xsl:template match="aktionen:SubstractModValueType"><xsl:apply-templates select="./*[1]"/> <xsl:for-each select="./*[position()>1]"> &#x2212; <xsl:apply-templates select="."/></xsl:for-each></xsl:template>

  <xsl:template match="aktionen:MultiplyModValueType"><xsl:apply-templates select="./*[1]"/><xsl:for-each select="./*[position()>1]"> &#8226; <xsl:apply-templates select="."/></xsl:for-each></xsl:template>

  <xsl:template match="*">  [[FEHLER IM XSLT]] (<xsl:value-of select="local-name()"/>)
  </xsl:template>
</xsl:stylesheet>