<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageDir/CARD.Master" Async="true"
    CodeBehind="TPOTXN002.aspx.cs" Inherits="CyberTestWeb.src.TPOTXN002" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phdContent" runat="server">
    <asp:Panel ID="PnlContent" runat="server" Style="width: 956px">
        <table style="width: 956px" class="tablestyle1">
            <tr class="trstyle">
                <td class="tdhstyle">
                    <asp:Literal runat="server" Text="<%$ Resources:S,  POSTING_DTE%>"> </asp:Literal>
                    <asp:Literal runat="server" Text="<%$ Resources:S,  START%>"></asp:Literal>
                </td>
                <td class="tdbstyle">
                    <asp:TextBox ID="txtwherePOSTING_DTE_FR" runat="server" class="textbox_char_100"
                        MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image3" runat="server" ImageUrl="/card/images/calendar.png" />
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtwherePOSTING_DTE_FR"
                        DaysModeTitleFormat="yyyy,MMMM" TodaysDateFormat="yyyy-MM-dd" PopupPosition="TopLeft"
                        Format="yyyy/MM/dd" PopupButtonID="Image3" ClearTime="True">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="meewherePOSTING_DTE_FR" runat="server" MaskType="Date"
                        TargetControlID="txtwherePOSTING_DTE_FR" Mask="9999/99/99" AutoComplete="False">
                    </cc1:MaskedEditExtender>
                    <cc1:MaskedEditValidator ID="mevwherePOSTING_DTE_FR" runat="server" MaskType="Date"
                        TargetControlID="txtwherePOSTING_DTE_FR" Mask="9999/99/99" AutoComplete="False"
                        ControlToValidate="txtwherePOSTING_DTE_FR" ControlExtender="meewherePOSTING_DTE_FR"
                        InvalidValueBlurredMessage="*" Display="Dynamic">
                    </cc1:MaskedEditValidator>
                    <cc1:TextBoxWatermarkExtender ID="tbwwherePOSTING_DTE_FR" runat="server" TargetControlID="txtwherePOSTING_DTE_FR"
                        WatermarkText="yyyy/mm/dd" />
                </td>
                <td class="tdhstyle">
                    <asp:Literal runat="server" Text="<%$ Resources:S,  POSTING_DTE%>"> </asp:Literal>
                    <asp:Literal runat="server" Text="<%$ Resources:S,  END%>"></asp:Literal>
                </td>
                <td class="tdbstyle">
                    <asp:TextBox ID="txtwherePOSTING_DTE_TO" runat="server" class="textbox_char_100"
                        MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image4" runat="server" ImageUrl="/card/images/calendar.png" />
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtwherePOSTING_DTE_TO"
                        DaysModeTitleFormat="yyyy,MMMM" TodaysDateFormat="yyyy-MM-dd" PopupPosition="TopLeft"
                        Format="yyyy/MM/dd" PopupButtonID="Image4" ClearTime="True">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="meewherePOSTING_DTE_TO" runat="server" MaskType="Date"
                        TargetControlID="txtwherePOSTING_DTE_TO" Mask="9999/99/99" AutoComplete="False">
                    </cc1:MaskedEditExtender>
                    <cc1:MaskedEditValidator ID="mevwherePOSTING_DTE_TO" runat="server" MaskType="Date"
                        TargetControlID="txtwherePOSTING_DTE_TO" Mask="9999/99/99" AutoComplete="False"
                        ControlToValidate="txtwherePOSTING_DTE_TO" ControlExtender="meewherePOSTING_DTE_TO"
                        InvalidValueBlurredMessage="*" Display="Dynamic">
                    </cc1:MaskedEditValidator>
                    <cc1:TextBoxWatermarkExtender ID="tbwwherePOSTING_DTE_TO" runat="server" TargetControlID="txtwherePOSTING_DTE_TO"
                        WatermarkText="yyyy/mm/dd" />
                </td>
                <td class="tdhstyle">
                    <asp:Literal runat="server" Text="<%$ Resources:S,  CODE%>"> </asp:Literal>
                </td>
                <td class="tdbstyle" colspan="2">
                    <asp:DropDownList ID="ddlOPTION" runat="server" CssClass="DropDownList_120">
                        <asp:ListItem Text="<%$ Resources:S, USER%>" Value="USER"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:S, PMT%>" Value="PMT"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:S, SYSTEM%>" Value="SYSTEM"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                &nbsp;<asp:ImageButton ID="itnQuery" runat="server" ImageUrl="/card/images/itn/itnquery.jpg"
                    OnClick="itnQuery_Click" />
                    &nbsp;<asp:ImageButton ID="itnPrint" runat="server" ImageUrl="/card/images/itn/itnprint.jpg"
                        OnClick="itnPrint_Click" />
                    &nbsp;<asp:ImageButton ID="itnClear" runat="server" ImageUrl="/card/images/itn/itnclear.jpg"
                        OnClick="itnClear_Click" OnClientClick="return confirm(&quot;確定清除?&quot;);" />

                    <asp:Button ID="itnTest" runat="server" Text="TEST" OnClick="itnTest_Click" />

                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:GridView ID="gdvdata" runat="server" CellPadding="4" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gdvdata_PageIndexChanging" OnRowDataBound="gdvdata_RowDataBound"
                        OnRowCreated="gdvdata_RowCreated" AllowSorting="True" OnSorting="gdvdata_Sorting"
                        CssClass="GridView_all" RowStyle-CssClass="GridView_RowStyle" AlternatingRowStyle-CssClass="GridView_AltRowStyle"
                        HeaderStyle-CssClass="GridView_HeaderStyle" FooterStyle-CssClass="GridView_FooterStyle"
                        ForeColor="White" EmptyDataText="No Data" EmptyDataRowStyle-CssClass="GridView_EmptyDataRowStyle">
                        <PagerStyle HorizontalAlign="Center" CssClass="GridView_PagerStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:S, SELECT%>">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rtnselection" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="<%$ Resources:S, ACCT_NBR%>" DataField="ACCT_NBR">
                                <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, CARD_PRODUCT%>" DataField="CARD_PRODUCT_DESCR">
                                <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, CARD_NBR%>" DataField="CARD_NBR">
                                <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, CARD_FLAG%>" DataField="USER_CHAR_4">
                                <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, SEQ%>" DataField="SEQ">
                                <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, EFF_DTE%>" DataField="EFF_DTE" DataFormatString="<%$ Resources:S, DATE_FORMAT_GV%>">
                                <ItemStyle Width="76px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, POSTING_DTE%>" DataField="POSTING_DTE"
                                DataFormatString="<%$ Resources:S, DATE_FORMAT_GV%>">
                                <ItemStyle Width="76px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, DESCR%>" DataField="DESCR">
                                <ItemStyle Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, AMT%>" DataField="AMT" DataFormatString="<%$ Resources:S, AMT_FORMAT_GV%>">
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, CODE%>" DataField="TYPE_CODE">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, MCC_CODE%>" DataField="MCC_CODE_DESCR">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, CURRENCY%>" DataField="ORIG_CURRENCY">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, ORIG_AMT%>" DataField="ORIG_AMT" DataFormatString="<%$ Resources:S, AMT_FORMAT_GV%>">
                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, COUNTRY_CODE%>" DataField="COUNTRY_CODE">
                                <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, AUTH_CODE%>" DataField="AUTH_CODE">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S,  PMT_AMT%>" DataField="PMT_AMT" DataFormatString="<%$ Resources:S, AMT_FORMAT_GV%>">
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:S, TERM1%>" DataField="STMT_MONTH"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="trstyle">
                <td colspan="7" class="tdtstyle">&nbsp;<asp:ImageButton ID="itnUpdate" runat="server" OnClick="itnUpdate_Click" />
                    &nbsp;<asp:ImageButton ID="itnDetail" runat="server" OnClick="itnDetail_Click" />
                    &nbsp;<asp:ImageButton ID="itnInst" runat="server" OnClick="itnInst_Click" />
                    &nbsp;<asp:ImageButton ID="itnAdjust" runat="server" OnClick="itnAdjust_Click" />
                    &nbsp;<asp:ImageButton ID="itnDownLoad" runat="server" OnClick="itnDOWNLOAD_Click" />
                    &nbsp;<asp:ImageButton ID="itnAnalysis" runat="server" OnClick="itnAnalysis_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlDetail" runat="server" Visible="False" Style="width: 956px">
        <table style="width: 956px" class="tablestyle1">
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" GroupingText="<%$ Resources:S,  MER_INF%>"
                        CssClass="Panel">
                        <table width="100%">
                            <tr class="trstyle">
                                <td class="tdhstyle_200" width="25%">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  SOURCE_CODE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle" width="25%">
                                    <asp:TextBox ID="txtSOURCE_CODE" runat="server" CssClass="textbox_char_200"> </asp:TextBox>
                                    <asp:Label ID="lblFILE_SOURCE" runat="server" CssClass="Label_char" Visible="false" />
                                </td>
                                <td class="tdhstyle_200" width="25%">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  CODE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle" width="25%">
                                    <asp:TextBox ID="txtCODE" runat="server" CssClass="textbox_char_200"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  COUNTRY_AND_CITY%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtCOUNTRY_CODE" runat="server" CssClass="textbox_char_80"> </asp:TextBox>
                                    <asp:TextBox ID="txtCITY_CODE" runat="server" CssClass="textbox_char_80"></asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  MCC_CODE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtMCC_CODE" runat="server" CssClass="textbox_char_200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  ACQ_BANK_NBR%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtACQ_BANK_NBR" runat="server" CssClass="textbox_char_200"></asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  MER_NBR%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtMER_NBR" runat="server" CssClass="textbox_char_200"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  DESCR_TX%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtDESCR" runat="server" CssClass="textbox_char_200"> </asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  POS_NBR%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtPOS_NBR" runat="server" CssClass="textbox_char_200"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  ARN%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtARN" runat="server" CssClass="textbox_char_200"> </asp:TextBox>
                                </td>

                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" GroupingText="<%$ Resources:S, RETAIL_INF%>"
                        CssClass="Panel">
                        <table width="100%">
                            <tr class="trstyle">
                                <td class="tdhstyle_200" width="25%">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  EFF_DTE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle" width="25%">
                                    <asp:TextBox ID="txtEFF_DTE" runat="server" CssClass="textbox_char_100"> </asp:TextBox>
                                </td>
                                <td class="tdhstyle_200" width="25%">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  POSTING_DTE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle" width="25%">
                                    <asp:TextBox ID="txtPOSTING_DTE" runat="server" CssClass="textbox_char_100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  AMT_TX%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtAMT" runat="server" CssClass="textbox_numeric"> </asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  MT_TYPE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtMT_TYPE" runat="server" CssClass="textbox_char_50"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  AUTH_CODE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtAUTH_CODE" runat="server" CssClass="textbox_char_50"> </asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  CARD_PRODUCT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtCARD_PRODUCT_DESCR" runat="server" CssClass="textbox_char_100"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  ORIG_AMT%>"> </asp:Literal>
                                    /
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:S,  CURRENCY%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtORIG_AMT" runat="server" CssClass="textbox_numeric"> </asp:TextBox>
                                    <asp:TextBox ID="txtORIG_CURRENCY" runat="server" CssClass="textbox_char_80"> </asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  CONV_RATE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtCONV" runat="server" CssClass="textbox_char_140"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:S, B2D_EXCHANGE_AMT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtUSER_AMT_4" runat="server" CssClass="textbox_numeric"> </asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, CARD_NBR%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtCARD_NBR" runat="server" CssClass="textbox_char_140"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel3" runat="server" GroupingText="<%$ Resources:S, OTHER%>" CssClass="Panel">
                        <table width="100%">
                            <%--    <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, RETRIEVE_DTE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtRETRIEVE_DTE" runat="server" CssClass="textbox_char_100"></asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, CHARGE_BACK_DTE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtCHARGE_BACK_DTE" runat="server" CssClass="textbox_char_100"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr class="trstyle">
                                <td class="tdhstyle_200" width="25%">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, AUTH_MATCH%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle" width="25%">
                                    <asp:CheckBox ID="cbxAUTH_MATCH" runat="server" />
                                    <asp:Literal runat="server" Text="<%$ Resources:S, SUCCESS%>"> </asp:Literal>
                                </td>
                                <td class="tdhstyle_200" width="25%">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, AUTH_MATCH_AMT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle" width="25%">
                                    <asp:TextBox ID="txtAUTH_MATCH_AMT" runat="server" CssClass="textbox_numeric"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  PMT_AMT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtPMT_AMT" runat="server" CssClass="textbox_numeric"></asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  REVOLVING_AMT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtREVOLVING_AMT" runat="server" CssClass="textbox_numeric"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  INST_TERM%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtINST_TERM" runat="server" CssClass="textbox_numeric"></asp:TextBox>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  INST_TTL_AMT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtINST_TTL_AMT" runat="server" CssClass="textbox_numeric"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S,  TX_RATE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtTX_RATE" runat="server" CssClass="textbox_numeric"> </asp:TextBox>
                                    <cc1:MaskedEditExtender ID="meeTX_RATE" runat="server" MaskType="Number" TargetControlID="txtTX_RATE"
                                        Mask="999.999" InputDirection="RightToLeft">
                                    </cc1:MaskedEditExtender>
                                    %
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, RESERVED_FIELD%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle"></td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, MNT_USER%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:Label ID="lblmnt_user" runat="server" CssClass="Label_char"></asp:Label>
                                </td>
                                <td class="tdhstyle_200">
                                    <asp:Literal runat="server" Text="<%$ Resources:S, MNT_DT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:Label ID="lblmnt_dt" runat="server" CssClass="Label_char"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;<asp:ImageButton ID="itnSave" runat="server" ValidationGroup="chkg1" OnClick="itnSave_Click" />
                    &nbsp;<asp:ImageButton ID="itnCancel" runat="server" OnClick="itnCancel_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phdMessage" runat="server">
</asp:Content>
