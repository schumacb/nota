<xsl:stylesheet
                         xmlns:nota="http://nota.org/schema/nota"
        xmlns:geschöpf="http://nota.org/schema/geschöpf"
        xmlns:kultur="http://nota.org/schema/kultur"
        xmlns:profession="http://nota.org/schema/profession"
        xmlns:talent="http://nota.org/schema/talent"
        xmlns:fertigkeit="http://nota.org/schema/fertigkeit"
        xmlns:besonderheit="http://nota.org/schema/besonderheit"
        xmlns:aktionen="http://nota.org/schema/kampf/aktionen"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
 xsi:schemaLocation="http://nota.org/schema/nota ..\..\..\..\..\static\schema\nota.xsd
                http://nota.org/schema/geschöpf ..\..\..\..\..\static\schema\geschöpf.xsd
                http://nota.org/schema/kultur ..\..\..\..\..\static\schema\kultur.xsd
                http://nota.org/schema/profession ..\..\..\..\..\static\schema\profession.xsd
                http://nota.org/schema/talent ..\..\..\..\..\static\schema\talent.xsd
                http://nota.org/schema/fertigkeit ..\..\..\..\..\static\schema\fertigkeit.xsd 
                http://nota.org/schema/besonderheit ..\..\..\..\..\static\schema\besonderheit.xsd 
                http://nota.org/schema/kampf/aktionen ..\..\..\..\..\static\schema\kampf\aktionen.xsd"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html" indent="yes" />
  <xsl:template match="nota:Daten">
    <xsl:apply-templates select="aktionen:Aktionen"/>
  </xsl:template>

  <xsl:template match="aktionen:Aktionen">
    <p>
      Eine übersciht der verschiedenen Aktionen
    </p>
    <xsl:apply-templates select="aktionen:Aktion"/>
  </xsl:template>

  <xsl:template match="aktionen:Aktion">
    <div class="panel panel-default">
      <div class="panel-body">

        <h3>
          <xsl:value-of select="@Name"/>
        </h3>
        <h4>
          <span class="small"> Ausdauerkosten: </span>
          <xsl:value-of select="@Kosten"/>
        </h4>
        <h4>
          <span class="small"> Typ: </span>
<xsl:choose>
  <xsl:when test="./@Typ='Offensiv'">Offensiv</xsl:when>
  <xsl:when test="@Typ='Defensiv'">Defensiv</xsl:when>
  <xsl:when test="@Typ='Ausgeglieschen'">Ausgeglieschen</xsl:when>
  <xsl:when test="@Typ='Keine'">Kein</xsl:when>
  <xsl:otherwise>[Ungültiger wert]</xsl:otherwise>
</xsl:choose>

          <xsl:value-of select="@Kosten"/>
        </h4>
        <p>
          <xsl:value-of select="aktionen:Beschreibung"/>
        </p>

        <xsl:if test="./aktionen:InstantEffekt">
          <h4>Instant Effekt</h4>
          <xsl:value-of select="./aktionen:InstantEffekt"/>
        </xsl:if>

        <xsl:if test="./aktionen:GarantierterEffekt">
          <h4>Garantierter Effekt</h4>
          <xsl:value-of select="./aktionen:GarantierterEffekt"/>
        </xsl:if>

        <xsl:if test="./aktionen:ErfolgEffekt">
          <h4>Erfolg Effekt</h4>
          <xsl:value-of select="./aktionen:ErfolgEffekt"/>
        </xsl:if>

        <xsl:if test="./aktionen:MisserfolgEffekt">
          <h4>Misserfolg Effekt</h4>
          <xsl:value-of select="./aktionen:MisserfolgEffekt"/>
        </xsl:if>

      </div>
    </div>

  </xsl:template>


  <xsl:template match="*">
    [FEHLER IM XSLT]
  </xsl:template>


</xsl:stylesheet>