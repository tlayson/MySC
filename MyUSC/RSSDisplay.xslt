<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xalan="http://xml.apache.org/xalan" xmlns:str="xalan://java.lang.String">
<xsl:output method="html" version="4.0" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/">
		<html>
			<body>
				<table border="0">
					<xsl:for-each select="feed/entry">
						<tr>
							<td>
								<span class="smSiteColorTxt">
									<xsl:value-of select="published"/>
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:element name="a">
									<xsl:attribute name="href">
										<xsl:value-of select="id"/>
									</xsl:attribute>
									<xsl:attribute name="style">color: #000000</xsl:attribute>
									<xsl:attribute name="target">_blank</xsl:attribute>
									<xsl:call-template name="replace-string">
										<xsl:with-param name="text" select="title"/>
										<xsl:with-param name="replace" select="'&amp;apos;'"/>
										<xsl:with-param name="with" select='"&apos;"'/>
									</xsl:call-template>
								</xsl:element>
							</td>
						</tr>
						<tr>
							<td>
								<span class="smCommentTxt">
									<xsl:call-template name="replace-string">
										<xsl:with-param name="text" select="summary"/>
										<xsl:with-param name="replace" select="'&amp;apos;'"/>
										<xsl:with-param name="with" select='"&apos;"'/>
									</xsl:call-template>
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdRSS" >
								<br></br>
							</td>
						</tr>
					</xsl:for-each>
					<xsl:for-each select="rss/channel/item">
						<tr>
							<td>
								<span class="smSiteColorTxt">
									<xsl:value-of select="pubDate"/>
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:element name="a">
									<xsl:attribute name="href">
										<xsl:value-of select="link"/>
									</xsl:attribute>
									<xsl:attribute name="style">color: #000000</xsl:attribute>
									<xsl:attribute name="target">_blank</xsl:attribute>
									<xsl:call-template name="replace-string">
										<xsl:with-param name="text" select="title"/>
										<xsl:with-param name="replace" select="'&amp;apos;'"/>
										<xsl:with-param name="with" select='"&apos;"'/>
									</xsl:call-template>
								</xsl:element>
							</td>
						</tr>
						<tr>
							<td>
								<span class="smCommentTxt">
									<xsl:call-template name="replace-string">
										<xsl:with-param name="text" select="description"/>
										<xsl:with-param name="replace" select="'&amp;apos;'"/>
										<xsl:with-param name="with" select='"&apos;"'/>
									</xsl:call-template>
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdRSS" >
								<br></br>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>

	<xsl:template name="replace-string">
        <xsl:param name="text"/>
        <xsl:param name="replace"/>
        <xsl:param name="with"/>
        <xsl:choose>
            <xsl:when test="contains($text,$replace)">
                <xsl:value-of select="substring-before($text,$replace)"/>
                <xsl:value-of select="$with"/>
                <xsl:call-template name="replace-string">
                    <xsl:with-param name="text"
                        select="substring-after($text,$replace)"/>
                    <xsl:with-param name="replace" select="$replace"/>
                    <xsl:with-param name="with" select="$with"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$text"/>
            </xsl:otherwise>
        </xsl:choose>
	</xsl:template>

</xsl:stylesheet>
