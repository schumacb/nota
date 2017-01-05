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
  <xsl:output method="html" indent="yes" />
  <xsl:template match="nota:Daten">
    <xsl:apply-templates select="aktionen:Aktionen"/>
  </xsl:template>
  <xsl:template match="aktionen:Aktionen">
    <h1>Aktionen</h1>
    <xsl:apply-templates select="aktionen:Aktion"/>
  </xsl:template>
  <xsl:template match="aktionen:Aktion">
    <div class="panel panel-default">
      <div class="panel-heading">
        <h3 class="panel-title">
          <xsl:value-of select="@Name"/>
        </h3>
      </div>
      <div class="panel-body">
        <xsl:value-of select="aktionen:Beschreibung"/>
      </div>
      <ul class="list-group">
        <xsl:if test="./aktionen:Bedingung">
          <li class="list-group-item">
            <h4>
              <small>Bedingung</small>
            </h4>
            <xsl:value-of select="./aktionen:Bedingung"/>
          </li>
        </xsl:if>
        <xsl:if test="./aktionen:InstantEffekt">
          <li class="list-group-item">
            <h4>
              <small>Augenblicklicher Effekt</small>
            </h4>
            <xsl:value-of select="./aktionen:InstantEffekt"/>
          </li>
        </xsl:if>
        <xsl:if test="./aktionen:GarantierterEffekt">
          <li class="list-group-item">
            <h4>
              <small>Garantierter Effekt</small>
            </h4>
            <xsl:value-of select="./aktionen:GarantierterEffekt"/>
          </li>
        </xsl:if>
        <xsl:if test="./aktionen:OffensivErfolg">
          <li class="list-group-item">
            <h4>
              <small>Offensiver Erfolg</small>
            </h4>
            <xsl:value-of select="./aktionen:OffensivErfolg"/>
          </li>
        </xsl:if>
        <xsl:if test="./aktionen:DefensivErfolg">
          <li class="list-group-item">
            <h4>
              <small>Defensiver Erfolg</small>
            </h4>
            <xsl:value-of select="./aktionen:DefensivErfolg"/>
          </li>
        </xsl:if>
             <xsl:if test="./aktionen:OffensivMiserfolg">
          <li class="list-group-item">
            <h4>
              <small>Offensiver Misserfolg</small>
            </h4>
            <xsl:value-of select="./aktionen:OffensivMiserfolg"/>
          </li>
        </xsl:if>
        <xsl:if test="./aktionen:DefensivMiserfolg">
          <li class="list-group-item">
            <h4>
              <small>Defensiver Misserfolg</small>
            </h4>
            <xsl:value-of select="./aktionen:DefensivMiserfolg"/>
          </li>
        </xsl:if>
      </ul>
      <div class="panel-footer">
        <dl class="dl-horizontal" style="margin-bottom:0px;">
          <dt>Ausdauerkosten</dt>
          <dd>
            <xsl:value-of select="@Kosten"/>
          </dd>
          <dt>Typ</dt>
          <dd>
            <xsl:choose>
              <xsl:when test="@Typ='Offensiv'">Offensiv</xsl:when>
              <xsl:when test="@Typ='Defensiv'">Defensiv</xsl:when>
              <xsl:when test="@Typ='Ausgeglieschen'">Offensiv und Passiv</xsl:when>
              <xsl:when test="@Typ='Keine'">Kein</xsl:when>
              <xsl:otherwise>[Ungültiger wert]</xsl:otherwise>
            </xsl:choose>
          </dd>
          <xsl:if test="./aktionen:Mod">
            <dt>Modifikation</dt>
            <dd>
              <xsl:if test="./aktionen:Mod/@ModifierType eq 'Bonus'">
                <span class="label label-success">+
                  <xsl:apply-templates select="./aktionen:Mod/*"/>
                </span>
              </xsl:if>
              <xsl:if test="./aktionen:Mod/@ModifierType eq 'Malus'">
                <span class="label label-danger"><var>-
                  <xsl:apply-templates select="./aktionen:Mod/*"/></var>
                </span>
              </xsl:if>
            </dd>
          </xsl:if>
        </dl>
      </div>
    </div>
  </xsl:template>
  <xsl:template match="aktionen:ConcreteModValueType">
    <xsl:value-of select="@Value"/>
    <xsl:if test="@Type='Percent'">%</xsl:if>
  </xsl:template>
  <xsl:template match="aktionen:VariableModValueType">
    <xsl:value-of select="@Value"/>
  </xsl:template>
  <xsl:template match="aktionen:AddModValueType">
    <xsl:apply-templates select="./*[1]"/>
    <xsl:for-each select="./*[position()>1]">
      + <xsl:apply-templates select="."/>
    </xsl:for-each>
  </xsl:template>
  <xsl:template match="aktionen:MultiplyModValueType">
    <xsl:apply-templates select="./*[1]"/>
    <xsl:for-each select="./*[position()>1]">
      * <xsl:apply-templates select="."/>
    </xsl:for-each>
  </xsl:template>
  <xsl:template match="*">  [FEHLER IM XSLT]            
    <xsl:value-of select="local-name()"/>
  </xsl:template>
</xsl:stylesheet>