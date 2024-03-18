Imports System
Imports System.IO
Imports Microsoft.VisualBasic
Public Class ClsRTFHF
    Public Function rtf_br() As String
        Return "\par"
    End Function
    Public Function rtf_br2() As String
        Return "\par\par"
    End Function
    Public Function rtf_bold(ByVal text As String) As String
        Return "\b " & text & " \b0 "
    End Function
    Public Function rtf_Italic(ByVal text As String) As String
        Return "\i " & text & " \i0"
    End Function
    Public Function rtf_ItalicAndBold(ByVal text As String) As String
        Return "\i " & "\b " & text & " \b0" & " \i0"
    End Function
    Public Function rtf_UnderLine(ByVal text As String) As String
        Return "\ul " & text & " \ul0"
    End Function
    Public Function rtf_BoldAndUnderLine(ByVal text As String) As String
        Return "\b " & "\ul " & text & " \ul0" & " \b0 "
    End Function
    Public Function rtf_LineSpace(ByVal text As String) As String
        Return "\slmult " & text & " \slmult1"
    End Function
    Public Function rtf_LineSpacing(ByVal size As Integer) As String
        Return "\sl" & size
    End Function
    Public Function rtf_JustifyCenter() As String
        Return "\qc "
    End Function
    Public Function rtf_JustifyLeft() As String
        Return "\ql "
    End Function
    Public Function rtf_JustifyRight() As String
        Return "\qr "
    End Function
    Public Function rtf_LeftSingleQuote() As String
        Return "\lquote "
    End Function
    Public Function rtf_rightSingleQuote() As String
        Return "\rquote "
    End Function
    Public Function rtf_LeftDoubleQuotationMark() As String
        Return "\ldblquote "
    End Function
    Public Function rtf_RightDoubleQuotationMark() As String
        Return "\rdblquote "
    End Function
    Public Function rtf_AllCapitals() As String
        Return "\caps "
    End Function
    Public Function rtf_Justified() As String
        Return "\qj "
    End Function
    Public Function rtf_Spacebefore(ByVal size As Integer) As String
        Return "\sb" & size
    End Function
    Public Function rtf_Spaceafter(ByVal size As Integer) As String
        Return "\sa" & size
    End Function
    Public Function rtf_FontSize(ByVal size As Integer) As String
        Return "\fs" & size
    End Function
    Public Function rtf_CurrentPageNumber() As String
        Return "\chpgn "
    End Function
    Public Function rtf_Bullet() As String
        Return "\bullet "
    End Function
    Public Function rtf_Header(ByVal sHeader As String, ByVal sFunctionName As String, ByVal SFYear As String) As String
        Return "{\header\pard\fs20 " & sHeader & "\par\fs20 " & sFunctionName & " Report for the quarter ended " & SFYear & " \par\plain \ql \li0\ri0\nowidctlpar\brdrt\brdrs\brdrw10\brsp100 \faauto\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033\par}{\fonttbl{\f0\fswiss\fcharset0 Book Antiqua;}}"
    End Function
    Public Function rtf_ESHeader(ByVal sHeader As String, ByVal sFunctionName As String, ByVal SFYear As String) As String
        Return "{\header\pard\qr\fs20 " & sHeader & " \par\fs20 Executive Summary \par\fs20 " & sFunctionName & " Q4 " & SFYear & " \par\plain \qr \li0\ri0\nowidctlpar\brdrt\brdrs\brdrw10\brsp100 \faauto\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033\par}{\fonttbl{\f0\fswiss\fcharset0 Book Antiqua;}}"
    End Function
    Public Function rtf_PHeader(ByVal sHeader As String, ByVal sFunctionName As String, ByVal SFYear As String) As String
        Return "{\header\pard\qr\fs26 " & sHeader & " \par\fs26 Proposal for \fs26 " & sFunctionName & " \par\plain \qr \li0\ri0\nowidctlpar\brdrt\brdrs\brdrw10\brsp100 \faauto\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033\par}{\fonttbl{\f0\fswiss\fcharset0 Book Antiqua;}}"
    End Function
    Public Function rtf_MHeader(ByVal sHeader As String, ByVal SFYear As String) As String
        Return "{\header\pard\qr\fs20 " & sHeader & " \fs20 " & SFYear & " \par\plain \qr \li0\ri0\nowidctlpar\brdrt\brdrs\brdrw10\brsp100 \faauto\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033\par}{\fonttbl{\f0\fswiss\fcharset0 Book Antiqua;}}"
    End Function
    Public Function rtf_SmallCapitals() As String
        Return "\scaps "
    End Function
    Public Function rtf_PageBreak() As String
        Return "\page "
    End Function
    Public Function rtf_emdash() As String
        Return "\emdash "
    End Function
    Public Function rtf_Smalls() As String
        Return "\smls "
    End Function
    Public Function rtf_CurrentTime() As String
        Return "\chtime "
    End Function
    Public Function rtf_StartSetting(ByVal sCompanyName As String) As String
        Return "{\rtf1\ansi\ansicpg1252\uc1 \deff0\deflang1033\deflangfe1033{\fonttbl{\f0\froman\fcharset0\fprq2{\*\panose 02020603050405020304}Book Antiqua ;}{\f53\froman\fcharset238\fprq2 Book Antiqua;}{\f54\froman\fcharset204\fprq2 Book Antiqua;}{\f56\froman\fcharset161\fprq2 Book Antiqua;}{\f57\froman\fcharset162\fprq2 Book Antiqua;}{\f58\froman\fcharset177\fprq2 Book Antiqua ;}{\f59\froman\fcharset178\fprq2 Book Antiqua;}{\f60\froman\fcharset186\fprq2 Book Antiqua;}}{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;\red255\green102\blue0;\red51\green153\blue102;\red51\green51\blue153;\red153\green51\blue0;\red204\green153\blue255;\red255\green153\blue0;\red51\green51\blue0;\red0\green51\blue0;\red255\green153\blue204;\red51\green204\blue204;\red153\green204\blue0;\red102\green102\blue153;\red153\green204\blue255;\red255\green204\blue0;\red153\green51\blue102;\red0\green51\blue102;\red0\green204\blue255;}{\stylesheet{\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \snext0 Normal;}{\s1\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel0\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 1;}{\s2\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel1\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 2;}{\s3\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel2\adjustright\rin0\lin0\itap0 \fs40\cf5\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 3;}{\s4\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel3\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 4;}{\s5\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel4\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 5;}{\s6\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel5\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 6;}{\s7\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel6\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 7;}{\s8\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel7\adjustright\rin0\lin0\itap0 \fs40\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 8;}{\s9\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel8\adjustright\rin0\lin0\itap0 \fs40\cf1\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext0 heading 9;}{\*\cs10 \additive Default Paragraph Font;}}{\info{\title LOD DOCUMENT}{\author HISHAM,SOBRI&KADIR}{\operator mms}{\creatim\yr2005\mo5\dy2\hr22\min6}{\revtim\yr2005\mo5\dy6\hr17\min49}{\version12}{\edmins28}{\nofpages2}{\nofwords0}{\nofchars0}{\*\company MMCSPL}{\nofcharsws0}{\vern8247}}\widowctrl\ftnbj\aenddoc\noxlattoyen\expshrtn\noultrlspc\dntblnsbdb\nospaceforul\hyphcaps0\horzdoc\dghspace120\dgvspace120\dghorigin1701\dgvorigin1984\dghshow0\dgvshow3\jcompress\viewkind1\viewscale100\nolnhtadjtbl \fet0\sectd \linex0\sectdefaultcl {\footer\pard\plain\li0\ri0\nowidctlpar\brdrt\brdrs\brdrw10\brsp100 \faauto\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 {\footer\pard\ql\fs20 " & sCompanyName & "}                                                                                            {\footer\pard\ql\fs18 Page }{\field{\*\fldinst {\fs18 PAGE}}{\fldrslt {\fs18\lang1024\langfe1024\noproof 1}}}{\fs18  of }{\field{\*\fldinst {\fs18 NUMPAGES}}{\fldrslt {\fs18\lang1024\langfe1024\noproof 2}}}{\fs18  \par }}{\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang{\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang{\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang{\pntxta .}}{\*\pnseclvl4\pnlcltr\pnstart1\pnindent720\pnhang{\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang{\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang{\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang{\pntxtb (}{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang{\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang{\pntxtb (}{\pntxta )}}\pard\plain \s9\ql \li0\ri0\keepn\widctlpar\aspalpha\aspnum\faauto\outlinelevel8\adjustright\rin0\lin0\itap0 \fs40\cf1\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 "
    End Function
    Public Function FontColor_Blue() As String
        Return "{\cf2"
    End Function
    Public Function FontColor_BrightGreen() As String
        Return "{\cf4"
    End Function
    Public Function FontColor_Pink() As String
        Return "{\cf5"
    End Function
    Public Function FontColor_Yellow() As String
        Return "{\cf7"
    End Function
    Public Function FontColor_White() As String
        Return "{\cf8"
    End Function
    Public Function FontColor_Teal() As String
        Return "{\cf10"
    End Function
    Public Function FontColor_DarkRed() As String
        Return "{\cf13"
    End Function
    Public Function FontColor_DarkYellow() As String
        Return "{\cf14"
    End Function
    Public Function FontColor_Gray50() As String
        Return "{\cf15"
    End Function
    Public Function FontColor_Gray25() As String
        Return "{\cf16"
    End Function
    Public Function FontColor_Red() As String
        Return "{\cf17"
    End Function
    Public Function FontColor_Orange() As String
        Return "{\cf17 "
    End Function
    Public Function Fontcolor_Close() As String
        Return " }"
    End Function
    Public Function FontColor_SeaGreen() As String
        Return "{\cf18"
    End Function
    Public Function FontColor_Indigo() As String
        Return "{\cf19"
    End Function
    Public Function FontColor_Levender() As String
        Return "{\cf21"
    End Function
    Public Function FontColor_Brown() As String
        Return "{\cf20"
    End Function
    Public Function FontColor_LightOrange() As String
        Return "{\cf22"
    End Function
    Public Function FontColor_LightGreen() As String
        Return "{\cf22"
    End Function
    Public Function FontColor_OliveGreen() As String
        Return "{\cf23"
    End Function
    Public Function FontColor_DarkGreen() As String
        Return "{\cf24"
    End Function
    Public Function FontColor_Rose() As String
        Return "{\cf25"
    End Function
    Public Function FontColor_Aqua() As String
        Return "{\cf26"
    End Function
    Public Function FontColor_Lime() As String
        Return "{\cf27"
    End Function
    Public Function FontColor_BlueGray() As String
        Return "{\cf28"
    End Function
    Public Function FontColor_PaleBlue() As String
        Return "{\cf29"
    End Function
    Public Function FontColor_Gold() As String
        Return "{\cf30"
    End Function
    Public Function FontColor_Plum() As String
        Return "{\cf31"
    End Function
    Public Function FontColor_SktBlue() As String
        Return "{\cf33"
    End Function
    Public Function FontColor_DarkTeal() As String
        Return "{\cf32"
    End Function
    Public Function FontColor_Black() As String
        Return "{ "
    End Function
    Public Function FontColor_Nth(ByVal n As Integer, ByVal text As String) As String
        Return "{\cf" & n
    End Function
    Public Function rtf_BackGroundColor() As String
        Return "\cb75 "
    End Function
    Public Function rtf_UnderLineColor(ByVal text As String) As String
        Return "\ulc35 "
    End Function
    Public Function FileExtension(ByVal filename As String)
        Dim sExt As String
        sExt = Left(StrReverse(filename), 3)
        Return StrReverse(sExt)
    End Function
    Public Function table_RowStart(ByVal ColSize As Integer) As String
        Dim rowStrTag1, rowStrTag2, rowStrTag4, endTag As String
        Dim strMidTag As String = ""
        Dim i As Integer
        Dim iCellWidth As Long
        rowStrTag1 = "\trowd \trgaph108\trleft-108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trftsWidth1\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddfr3\clvertalt\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10"
        rowStrTag2 = "\clbrdrr\brdrs\brdrw10\cltxlrtb\clftsWidth3\clwWidth4000\cellx" '1771
        rowStrTag4 = "\clvertalt\clbrdrt\brdrs\brdrw10 \clbrdrl\brdrs\brdrw10 \clbrdrb\brdrs\brdrw10"
        iCellWidth = 1663
        For i = 1 To ColSize - 1
            strMidTag = strMidTag & rowStrTag2 & CType(iCellWidth, String) & rowStrTag4
            iCellWidth = iCellWidth + 1771
        Next
        iCellWidth = iCellWidth + 1
        endTag = "\clbrdrr\brdrs\brdrw10 \cltxlrtb\clftsWidth3\clwWidth5001\cellx" & CType(iCellWidth, String) & " " '1772

        rowStrTag1 = rowStrTag1 & strMidTag & endTag
        Return rowStrTag1
    End Function
    Public Function table_RowStartL(ByVal ColSize As Integer) As String
        Dim rowStrTag1, rowStrTag2, rowStrTag4, endTag As String
        Dim strMidTag As String = ""
        Dim i As Integer
        Dim iCellWidth As Long
        rowStrTag1 = "\trowd \trgaph108\trleft-108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trftsWidth1\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddfr3\clvertalt\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10"
        rowStrTag2 = "\clbrdrr\brdrs\brdrw10\cltxlrtb\clftsWidth3\clwWidth10000\cellx" '1771
        rowStrTag4 = "\clvertalt\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw100"
        iCellWidth = 6000

        If ColSize = 1 Then ' If Col Size is only one.
            strMidTag = strMidTag & rowStrTag2 & CType(iCellWidth, String) & rowStrTag4
        Else
            For i = 1 To ColSize - 1
                strMidTag = strMidTag & rowStrTag2 & CType(iCellWidth, String) & rowStrTag4
                iCellWidth = iCellWidth + 3000
            Next
        End If
        iCellWidth = iCellWidth + 1
        endTag = "\clbrdrr\brdrs\brdrw10\cltxlrtb \clftsWidth3\clwWidth10000\cellx" & CType(iCellWidth, String) & " " '1772

        rowStrTag1 = rowStrTag1 & strMidTag & endTag
        Return rowStrTag1
    End Function
    Public Function table_RowCloseL(ByVal ColSize As Integer) As String
        Dim rowStrTag1, rowStrTag2, rowStrTag4, endTag As String
        Dim strMidTag As String = ""
        Dim i As Integer
        Dim iCellWidth As Long
        rowStrTag1 = "{\trowd \trgaph108\trleft-108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trftsWidth1\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddfr3\clvertalt\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10"
        rowStrTag2 = "\clbrdrr\brdrs\brdrw10\cltxlrtb\clftsWidth3\clwWidth10000\cellx" '1771
        rowStrTag4 = "\clvertalt\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10"
        iCellWidth = 6000

        If ColSize = 1 Then ' If Col Size is only one.
            strMidTag = strMidTag & rowStrTag2 & CType(iCellWidth, String) & rowStrTag4
        Else
            For i = 1 To ColSize - 1
                strMidTag = strMidTag & rowStrTag2 & CType(iCellWidth, String) & rowStrTag4
                iCellWidth = iCellWidth + 3000
            Next
        End If
        iCellWidth = iCellWidth + 1
        endTag = "\clbrdrr\brdrs\brdrw10\cltxlrtb \clftsWidth3\clwWidth10000\cellx" & iCellWidth & "\row}" ''1772

        rowStrTag1 = rowStrTag1 & strMidTag & endTag
        Return rowStrTag1
    End Function
    Public Function table_RowClose(ByVal ColSize As Integer) As String
        Dim rowStrTag1, rowStrTag2, rowStrTag4, endTag As String
        Dim strMidTag As String = ""
        Dim i As Integer
        Dim iCellWidth As Long
        rowStrTag1 = "{\trowd \trgaph108\trleft-108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trftsWidth1\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddfr3\clvertalt\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10"
        rowStrTag2 = "\clbrdrr\brdrs\brdrw10 \cltxlrtb\clftsWidth3\clwWidth4000\cellx" '1771
        rowStrTag4 = "\clvertalt\clbrdrt\brdrs\brdrw10 \clbrdrl\brdrs\brdrw10 \clbrdrb\brdrs\brdrw10"
        iCellWidth = 1663
        For i = 1 To ColSize - 1
            strMidTag = strMidTag & rowStrTag2 & CType(iCellWidth, String) & rowStrTag4
            iCellWidth = iCellWidth + 1771
        Next
        iCellWidth = iCellWidth + 1
        endTag = "\clbrdrr\brdrs\brdrw10 \cltxlrtb\clftsWidth3\clwWidth5000\cellx" & iCellWidth & "\row}" ''1772

        rowStrTag1 = rowStrTag1 & strMidTag & endTag
        Return rowStrTag1
    End Function
    Public Function Table_CellsStart() As String
        Return "\pard\plain \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\faauto\adjustright\rin0\lin0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 {"
    End Function
    Public Function Table_CellsStartL() As String
        Return "\pard\plain \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\faauto\adjustright\rin0\lin0 \fs24\lang3033\langfe3033\cgrid\langnp3033\langfenp3033 {"
    End Function
    Public Function Table_CellsClose() As String
        Return "}\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\faauto\adjustright\rin0\lin0"
    End Function
    Public Function Table_CellsCloseL() As String
        Return "}\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\faauto\adjustright\rin0\lin0"
    End Function
    Public Function Table_CellText(ByVal text As String) As String
        Return text & "\cell"
    End Function
    Public Function rtf_CloseTag() As String
        Return "}"
    End Function
    Public Function table_CloseTag() As String
        Return "\pard \ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 {\par }"
    End Function
End Class
