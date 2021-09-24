<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<body>
				<table width="400px" border="1">
					<xsl:for-each select="adImpressions/adRotator">
						<tr valign="top">
							<td class="tdInput" colspan="2">
								AdRotator<xsl:value-of select="id"/>
							</td>
						</tr>
						<xsl:for-each select="ad">
							<tr valign="top">
								<td class="tdInput">
									<xsl:value-of select="adname"/>
								</td>
								<td class="tdInput">
									<xsl:value-of select="hitCount"/>
								</td>
							</tr>
						</xsl:for-each>
						<tr valign="top">
							<td class="tdInput" colspan="2">
								
							</td>
						</tr>
					</xsl:for-each>
					<xsl:for-each select="adResponses/adRotator">
						<tr valign="top">
							<td class="tdInput" colspan="2">
								AdRotator<xsl:value-of select="id"/>
							</td>
						</tr>
						<xsl:for-each select="ad">
							<tr valign="top">
								<td class="tdInput">
									<xsl:value-of select="adname"/>
								</td>
								<td class="tdInput">
									<xsl:value-of select="hitCount"/>
								</td>
							</tr>
						</xsl:for-each>
						<tr valign="top">
							<td class="tdInput" colspan="2">
								
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
