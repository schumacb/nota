<xsl:stylesheet
                         xmlns:nota="http://nota.org/schema/nota"
        xmlns:geschöpf="http://nota.org/schema/geschöpf"
        xmlns:kultur="http://nota.org/schema/kultur"
        xmlns:profession="http://nota.org/schema/profession"
        xmlns:talent="http://nota.org/schema/talent"
        xmlns:fertigkeit="http://nota.org/schema/fertigkeit"
        xmlns:besonderheit="http://nota.org/schema/besonderheit"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
 xsi:schemaLocation="http://nota.org/schema/nota ..\..\..\..\..\static\schema\nota.xsd
                http://nota.org/schema/geschöpf ..\..\..\..\..\static\schema\geschöpf.xsd
                http://nota.org/schema/kultur ..\..\..\..\..\static\schema\kultur.xsd
                http://nota.org/schema/profession ..\..\..\..\..\static\schema\profession.xsd
                http://nota.org/schema/talent ..\..\..\..\..\static\schema\talent.xsd
                http://nota.org/schema/fertigkeit ..\..\..\..\..\static\schema\fertigkeit.xsd 
                http://nota.org/schema/besonderheit ..\..\..\..\..\static\schema\besonderheit.xsd" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="nota:Daten">
    <xsl:apply-templates select="talent:Talente"/>  
  </xsl:template>

  <xsl:template match="talent:Talente">
    <xsl:apply-templates select="talent:Talent"/>
  </xsl:template>
  <xsl:template match="talent:Talent">

    <div class="panel panel-default">
      <div class="panel-body">

        <h3>
          <xsl:value-of select="@Name"/>
          <span class="small">
            (<xsl:apply-templates  select="talent:Probe/*[1]" />,
            <xsl:apply-templates  select="talent:Probe/*[2]" />,
            <xsl:apply-templates  select="talent:Probe/*[3]" />)
          </span>
        </h3>
        <h4>
          <span class="small"> Komplexität: </span>
          <xsl:value-of select="@Komplexität"/>
        </h4>
        <p>
          <xsl:value-of select="talent:Beschreibung"/>
        </p>
      </div>
      <xsl:if test="talent:Ableitungen">
        <div class="panel-footer">
          <div class="compact-footer Ableitungen">

            <div class="small">
              <ul>
                <xsl:apply-templates select="talent:Ableitungen"/>
              </ul>
            </div>
          </div>
        </div>
      </xsl:if>
    </div>

  </xsl:template>

  <xsl:template match="talent:Ableitungen">
    <xsl:apply-templates select="*" />
  </xsl:template>

  <xsl:template match="*">
    [FEHLER IM XSLT]
  </xsl:template>

  <xsl:template match="talent:Max">
    <li>
      Maximum
      <ul>
        <xsl:apply-templates select="*" />
      </ul>
    </li>
  </xsl:template>

  <xsl:template match="talent:Ableitung">
    <li>
      <xsl:value-of select="@Name" />
      (<xsl:value-of select="@Anzahl" />)
    </li>
  </xsl:template>

  <xsl:template match="talent:Mut">MU</xsl:template>
  <xsl:template match="talent:Glück">GL</xsl:template>
  <xsl:template match="talent:Klugheit">KL</xsl:template>
  <xsl:template match="talent:Intuition">IN</xsl:template>
  <xsl:template match="talent:Gewandtheit">GE</xsl:template>
  <xsl:template match="talent:Feinmotorik">IN</xsl:template>
  <xsl:template match="talent:Sympathie">SY</xsl:template>
  <xsl:template match="talent:Einschüchterung">ES</xsl:template>
  <xsl:template match="talent:Stärke">ST</xsl:template>
  <xsl:template match="talent:Konstitution">KO</xsl:template>

</xsl:stylesheet>