<xsl:stylesheet xmlns:nota="http://nota.org/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
 xsi:schemaLocation="http://nota.org/ ..\render\books\Regelwerke\Grundregeln\talente\talent.xsd" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="nota:Talente">

    <xsl:apply-templates select="nota:Talent"/>
  </xsl:template>
  <xsl:template match="nota:Talent">

    <div class="panel panel-default">
      <div class="panel-body">

        <h3>
          <xsl:value-of select="@Name"/>
          <span class="small">
            (<xsl:apply-templates  select="nota:Probe/*[1]" />,
            <xsl:apply-templates  select="nota:Probe/*[2]" />,
            <xsl:apply-templates  select="nota:Probe/*[3]" />)
          </span>
        </h3>
        <h4>
          <span class="small"> Komplexität: </span>
          <xsl:value-of select="@Komplexität"/>
        </h4>
        <p>
          <xsl:value-of select="nota:Beschreibung"/>
        </p>
      </div>
      <xsl:if test="nota:Ableitungen">
        <div class="panel-footer">
          <div class="compact-footer Ableitungen">

            <div class="small">
              <ul>
                <xsl:apply-templates select="nota:Ableitungen"/>
              </ul>
            </div>
          </div>
        </div>
      </xsl:if>
    </div>

  </xsl:template>

  <xsl:template match="nota:Ableitungen">
    <xsl:apply-templates select="*" />
  </xsl:template>

  <xsl:template match="*">
    [FEHLER IM XSLT]
  </xsl:template>

  <xsl:template match="nota:Max">
    <li>
      Maximum
      <ul>
        <xsl:apply-templates select="*" />
      </ul>
    </li>
  </xsl:template>

  <xsl:template match="nota:Ableitung">
    <li>
      <xsl:value-of select="@Name" />
      (<xsl:value-of select="@Anzahl" />)
    </li>
  </xsl:template>

  <xsl:template match="nota:Mut">MU</xsl:template>
  <xsl:template match="nota:Glück">GL</xsl:template>
  <xsl:template match="nota:Intuition">IN</xsl:template>
  <xsl:template match="nota:Klugheit">KL</xsl:template>

</xsl:stylesheet>