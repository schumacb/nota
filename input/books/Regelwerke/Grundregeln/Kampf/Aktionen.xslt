<xsl:stylesheet 
  xmlns:nota="http://nota.org/schema/nota" 
  xmlns:geschöpf="http://nota.org/schema/geschöpf" 
  xmlns:kultur="http://nota.org/schema/kultur" 
  xmlns:profession="http://nota.org/schema/profession" 
  xmlns:talent="http://nota.org/schema/talent" 
  xmlns:fertigkeit="http://nota.org/schema/fertigkeit" 
  xmlns:besonderheit="http://nota.org/schema/besonderheit" 
  xmlns:aktionen="http://nota.org/schema/kampf/aktionen" 
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://nota.org/schema/nota ..\..\..\..\..\static\schema\nota.xsd                http://nota.org/schema/geschöpf ..\..\..\..\..\static\schema\geschöpf.xsd                http://nota.org/schema/kultur ..\..\..\..\..\static\schema\kultur.xsd                http://nota.org/schema/profession ..\..\..\..\..\static\schema\profession.xsd                http://nota.org/schema/talent ..\..\..\..\..\static\schema\talent.xsd                http://nota.org/schema/fertigkeit ..\..\..\..\..\static\schema\fertigkeit.xsd                 http://nota.org/schema/besonderheit ..\..\..\..\..\static\schema\besonderheit.xsd                 http://nota.org/schema/kampf/aktionen ..\..\..\..\..\static\schema\kampf\aktionen.xsd" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html" indent="no" />
  <xsl:template match="nota:Daten">
    <xsl:apply-templates select="aktionen:Aktionen"/>
  </xsl:template>
  <xsl:template match="aktionen:Aktionen">

<h1>Aktionen</h1>
    <xsl:apply-templates select="aktionen:Aktion"/>
  </xsl:template>

  <xsl:template match="aktionen:Aktion" xml:space="preserve">

:::Action

# <xsl:value-of select="@Name"/>  

<xsl:value-of select="aktionen:Beschreibung"/>

        <xsl:if test="./aktionen:Bedingung">
## Bedingung
<xsl:value-of select="./aktionen:Bedingung"/>
        </xsl:if>
        <xsl:if test="./aktionen:InstantEffekt">
## Augenblicklicher Effekt
<xsl:value-of select="./aktionen:InstantEffekt"/>
        </xsl:if>
        <xsl:if test="./aktionen:GarantierterEffekt">
## Garantierter Effekt
<xsl:value-of select="./aktionen:GarantierterEffekt"/>
        </xsl:if>
        <xsl:if test="./aktionen:OffensivErfolg">
## Offensiver Erfolg
<xsl:value-of select="./aktionen:OffensivErfolg"/>
        </xsl:if>
        <xsl:if test="./aktionen:DefensivErfolg">
## Defensiver Erfolg
<xsl:value-of select="./aktionen:DefensivErfolg"/>
        </xsl:if>
        <xsl:if test="./aktionen:OffensivMiserfolg">
## Offensiver Misserfolg
<xsl:value-of select="./aktionen:OffensivMiserfolg"/>
        </xsl:if>
        <xsl:if test="./aktionen:DefensivMiserfolg">
## Defensiver Misserfolg
<xsl:value-of select="./aktionen:DefensivMiserfolg"/>
        </xsl:if>
**Ausdauerkosten** _<xsl:value-of select="@Kosten"/>_

**Typ** _<xsl:choose>
              <xsl:when test="@Typ='Offensiv'">Offensiv</xsl:when>
              <xsl:when test="@Typ='Defensiv'">Defensiv</xsl:when>
              <xsl:when test="@Typ='Ausgeglieschen'">Offensiv und Passiv</xsl:when>
              <xsl:when test="@Typ='Frei'">Frei</xsl:when>
              <xsl:when test="@Typ='Sekundär'">Sekundär</xsl:when>
              <xsl:otherwise>[Ungültiger wert]</xsl:otherwise>
            </xsl:choose>_
          <xsl:if test="./aktionen:Mod">
**Modifikation**

_<xsl:if test="./aktionen:Mod/@ModifierType eq 'Bonus'">
                ::{.label .label-success} &#65291;
                  <xsl:apply-templates select="./aktionen:Mod/*"/>
                ::
              </xsl:if>
              <xsl:if test="./aktionen:Mod/@ModifierType eq 'Malus'">
                ::{.label .label-danger} &#x2212;
                  <xsl:apply-templates select="./aktionen:Mod/*"/>
                ::
              </xsl:if>
          </xsl:if>_
:::
  </xsl:template>

  <xsl:template match="aktionen:ConcreteModValueType">
    <var><xsl:value-of select="@Value"/>
    <xsl:if test="@Type='Percent'">%</xsl:if></var>
  </xsl:template>

  <xsl:template match="aktionen:VariableModValueType">
    <var><xsl:value-of select="@Value"/></var>
  </xsl:template>

  <xsl:template match="aktionen:AddModValueType">
    <xsl:apply-templates select="./*[1]"/>
    <xsl:for-each select="./*[position()>1]">
      &#65291; <xsl:apply-templates select="."/>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="aktionen:MultiplyModValueType">
    <xsl:apply-templates select="./*[1]"/>
    <xsl:for-each select="./*[position()>1]">
      &#8226; <xsl:apply-templates select="."/>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="*">  [FEHLER IM XSLT]            
    <xsl:value-of select="local-name()"/>
  </xsl:template>
</xsl:stylesheet>