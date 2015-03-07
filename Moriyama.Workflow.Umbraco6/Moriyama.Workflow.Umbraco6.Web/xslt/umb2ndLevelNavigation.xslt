<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [ <!ENTITY nbsp "&#x00A0;"> ]>
<xsl:stylesheet 
  version="1.0" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns:msxml="urn:schemas-microsoft-com:xslt" 
  xmlns:umbraco.library="urn:umbraco.library"
  exclude-result-prefixes="msxml umbraco.library">
    
<xsl:output method="xml" omit-xml-declaration="yes"/>

<xsl:param name="currentPage"/>

<xsl:template match="/">

  <xsl:variable name="items" select="$currentPage/ancestor-or-self::* [@isDoc and @level = 2]/* [@isDoc and string(umbracoNaviHide) != '1']"/>
  
<!-- The fun starts here -->
    
<xsl:if test="count($items) &gt; 0">
<ul>
<xsl:for-each select="$items">
  <li>
    <a href="{umbraco.library:NiceUrl(@id)}">
      <xsl:value-of select="@nodeName"/>
    </a>
  </li>
</xsl:for-each>
</ul>
    </xsl:if>

</xsl:template>

</xsl:stylesheet>