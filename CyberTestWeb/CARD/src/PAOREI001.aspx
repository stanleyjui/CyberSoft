<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDir/CARD.Master" AutoEventWireup="true" Async="true"
    EnableEventValidation="false" CodeBehind="PAOREI001.aspx.cs" Inherits="CyberTestWeb.src.PAOREI001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phdContent" runat="server">
    <asp:Panel ID="PnlContent" runat="server" Style="width: 956px">
        <table style="width: 956px" class="tablestyle">
            <tr class="trstyle">
                <td colspan="4">
                    <asp:GridView ID="gdvdata" runat="server" CellPadding="4" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gdvdata_PageIndexChanging" OnRowDataBound="gdvdata_RowDataBound"
                        OnDataBound="gdvdata_DataBound" AllowSorting="True" OnSorting="gdvdata_Sorting" OnRowCreated="gdvdata_RowCreated"
                        CssClass="GridView_all" RowStyle-CssClass="GridView_RowStyle" AlternatingRowStyle-CssClass="GridView_AltRowStyle"
                        HeaderStyle-CssClass="GridView_HeaderStyle" FooterStyle-CssClass="GridView_FooterStyle"
                        ForeColor="White" EmptyDataText="No Data" EmptyDataRowStyle-CssClass="GridView_EmptyDataRowStyle">
                        <PagerStyle HorizontalAlign="Center" CssClass="GridView_PagerStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:S, SELECT%>">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rtnselection" runat="server" TabIndex="6" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="REVIEW_TYPE">
                                <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REVIEW_TYPE_DESCR" HeaderText="<%$ Resources:S, REVIEW_TYPE%>" SortExpression="REVIEW_TYPE_DESCR">
                                <ItemStyle HorizontalAlign="CENTER" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REVIEW_DAY" HeaderText="<%$ Resources:S, REVIEW_DAY%>" SortExpression="REVIEW_DAY">
                                <ItemStyle HorizontalAlign="CENTER" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCR" HeaderText="<%$ Resources:S, DESCR%>">
                                <ItemStyle HorizontalAlign="CENTER" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VALID_FLAG" HeaderText="<%$ Resources:S, VALID%>" SortExpression="VALID_FLAG">
                                <ItemStyle HorizontalAlign="CENTER" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CARD_REVIEW_TERM" HeaderText="<%$ Resources:S, CARD_REVIEW_TERM%>">
                                <ItemStyle HorizontalAlign="CENTER" />
                            </asp:BoundField>
                            <asp:BoundField DataField="POST_RESULT" HeaderText="<%$ Resources:S, FN_ITEM.11%>"
                                SortExpression="POST_RESULT">
                                <ItemStyle HorizontalAlign="CENTER" Width="30px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="trstyle">
                <td colspan="4" class="tdtstyle">
                    <asp:ImageButton ID="itnInsert" runat="server" ImageUrl="../../images/itn/itninsert.jpg"
                        OnClick="itnInsert_Click" />
                    &nbsp;<asp:ImageButton ID="itnCopy" runat="server" ImageUrl="../../images/itn/itncopy.jpg"
                        OnClick="itnCopy_Click" />
                    &nbsp;<asp:ImageButton ID="itnUpdate" runat="server" ImageUrl="../../images/itn/itnupdate.jpg"
                        OnClick="itnUpdate_Click" />
                    &nbsp;<asp:ImageButton ID="itnDetail" runat="server" ImageUrl="../../images/itn/itndetail.jpg"
                        OnClick="itnDetail_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="PnlContent"
        Enabled="False" Color="#666666" BorderColor="#666666" Radius="6" Corners="Bottom">
    </cc1:RoundedCornersExtender>
    <asp:Panel ID="PnlDetail" runat="server" Visible="False" Style="width: 956px">
        <table style="width: 956px" class="tablestyle">
            <tr>
                <td width="40%">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="<%$ Resources:S, SETUP%>"
                        CssClass="Panel_300">
                        <table width="100%">
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <span style="color: #ff0066">*</span>
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:S, REVIEW_TYPE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlREVIEW_TYPE" runat="server" CssClass="DropDownList">
                                        <asp:ListItem Text="<%$ Resources:S, REVIEW_TYPE.Y%>" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:S, REVIEW_TYPE.D%>" Value="D"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:S, DESCR%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtDESCR" runat="server" CssClass="textbox_char_160" MaxLength="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:S, REVIEW_DAY%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:TextBox ID="txtREVIEW_DAY" runat="server" CssClass="textbox_char_50"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fteREVIEW_DAY" runat="server" FilterType="Numbers" TargetControlID="txtREVIEW_DAY">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>

                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal41" runat="server" Text="<%$ Resources:S, CARD_REVIEW_TERM%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCARD_REVIEW_TERM" runat="server" CssClass="DropDownList_100">
                                        <asp:ListItem Text=" 1Month" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" 2Month" Value="2"></asp:ListItem>
                                        <asp:ListItem Text=" 3Month" Value="3"></asp:ListItem>
                                        <asp:ListItem Text=" 4Month" Value="4"></asp:ListItem>
                                        <asp:ListItem Text=" 5Month" Value="5"></asp:ListItem>
                                        <asp:ListItem Text=" 6Month" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:S, VALID%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlVALID_FLAG" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal46" runat="server" Text="<%$ Resources:S, RESERVED_FIELD%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle"></td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:S, RESERVED_FIELD%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle"></td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal38" runat="server" Text="<%$ Resources:S, MNT_USER%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:Label ID="lblmnt_user" runat="server" CssClass="Label_char"></asp:Label>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal39" runat="server" Text="<%$ Resources:S, MNT_DT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:Label ID="lblmnt_dt" runat="server" CssClass="Label_char"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td width="60%">
                    <asp:Panel ID="Panel2" runat="server" GroupingText="<%$ Resources:S, SETUP%>" CssClass="Panel_300">
                        <table width="100%">
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:S, CHECK_ACCT_CTLCODE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_ACCT_CONTROLCODE" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:S, CHECK_CARD_CTLCODE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_CARD_CONTROLCODE" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:S, CHECK_DELQ%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_DELQ" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:S, CHECK_DELQ_CNT%>"> </asp:Literal>
                                    <asp:TextBox ID="txtCHECK_DELQ_CNT" runat="server" CssClass="textbox_char_30"> </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fteCHECK_DELQ_CNT" runat="server" FilterType="Numbers" TargetControlID="txtCHECK_DELQ_CNT">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:S, CHECK_DELQ_STMT_BAL%>"> </asp:Literal>
                                    <asp:TextBox ID="txtCHECK_DELQ_STMT_BAL" runat="server" CssClass="textbox_char_80"> </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fteCHECK_DELQ_STMT_BAL" runat="server" FilterType="Numbers" TargetControlID="txtCHECK_DELQ_STMT_BAL">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:S, CHECK_LIMIT%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_LIMIT" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:S, CHECK_CREDIT_LIMIT%>"> </asp:Literal>
                                    <asp:TextBox ID="txtCHECK_CREDIT_LIMIT" runat="server" CssClass="textbox_char_100"> </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="dteCHECK_CREDIT_LIMIT" runat="server" FilterType="Numbers" TargetControlID="txtCHECK_CREDIT_LIMIT">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:S, CHECK_CONSUME%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_CONSUME" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:S, CHECK_CONSUME_MONTH%>"> </asp:Literal>
                                    <asp:DropDownList ID="ddlCHECK_CONSUME_MONTH" runat="server" CssClass="DropDownList_100">
                                        <asp:ListItem Text=" 1Month" Value="01"></asp:ListItem>
                                        <asp:ListItem Text=" 2Month" Value="02"></asp:ListItem>
                                        <asp:ListItem Text=" 3Month" Value="03"></asp:ListItem>
                                        <asp:ListItem Text=" 4Month" Value="04"></asp:ListItem>
                                        <asp:ListItem Text=" 5Month" Value="05"></asp:ListItem>
                                        <asp:ListItem Text=" 6Month" Value="06"></asp:ListItem>
                                        <asp:ListItem Text=" 7Month" Value="07"></asp:ListItem>
                                        <asp:ListItem Text=" 8Month" Value="08"></asp:ListItem>
                                        <asp:ListItem Text=" 9Month" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10Month" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11Month" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12Month" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:S, CHECK_FOREIGNER%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_FOREIGNER" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:S, CHECK_FOREIGNER_NATIONALITY%>"> </asp:Literal>
                                    <asp:TextBox ID="txtCHECK_FOREIGNER_NATIONALITY" runat="server" CssClass="textbox_char_50"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:S, CHECK_ARC_EXPIR%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_ARC_EXPIR" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:S, CHECK_ARC_EXPIR_MONTH%>"> </asp:Literal>
                                    <asp:DropDownList ID="ddlCHECK_ARC_EXPIR_MONTH" runat="server" CssClass="DropDownList_100">
                                        <asp:ListItem Text=" 1Month" Value="01"></asp:ListItem>
                                        <asp:ListItem Text=" 2Month" Value="02"></asp:ListItem>
                                        <asp:ListItem Text=" 3Month" Value="03"></asp:ListItem>
                                        <asp:ListItem Text=" 4Month" Value="04"></asp:ListItem>
                                        <asp:ListItem Text=" 5Month" Value="05"></asp:ListItem>
                                        <asp:ListItem Text=" 6Month" Value="06"></asp:ListItem>
                                        <asp:ListItem Text=" 7Month" Value="07"></asp:ListItem>
                                        <asp:ListItem Text=" 8Month" Value="08"></asp:ListItem>
                                        <asp:ListItem Text=" 9Month" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10Month" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11Month" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12Month" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:S, CHECK_OPENED%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_OPENED" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="trstyle">
                                <td class="tdhstyle">
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:S, CHECK_MAGNETIC_STRIPE%>"> </asp:Literal>
                                </td>
                                <td class="tdbstyle">
                                    <asp:DropDownList ID="ddlCHECK_MAGNETIC_STRIPE" runat="server" CssClass="DropDownList_50">
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="trstyle">
                <td colspan="3" class="tdtstyle">
                    <asp:ImageButton ID="itnSave" runat="server" ImageUrl="../images/itn/itnsave.jpg"
                        OnClick="itnSave_Click" OnClientClick="if(Page_ClientValidate()){return confirm(&quot;確定儲存?&quot;);} else {return false;}"
                        ValidationGroup="chkg1" />
                    &nbsp;<asp:ImageButton ID="itnClear" runat="server" ImageUrl="../images/itn/itnclear.jpg"
                        OnClick="itnClear_Click" OnClientClick="return confirm(&quot;確定清除?&quot;);" />
                    &nbsp;<asp:ImageButton ID="itnApprove" runat="server" ImageUrl="../images/itn/itnapprove.jpg"
                        OnClientClick="if(Page_ClientValidate()){return confirm(&quot;確定放行?&quot;);} else {return false;}"
                        OnClick="itnApprove_Click" ValidationGroup="chkg1" />
                    &nbsp;<asp:ImageButton ID="itnCheck" runat="server" ImageUrl="../../images/itn/itncheck.jpg"
                        OnClientClick="if(Page_ClientValidate()){return confirm(&quot;確定審核?&quot;);} else {return false;}"
                        OnClick="itnCheck_Click" ValidationGroup="chkg1" />
                    &nbsp;<asp:ImageButton ID="itnCancel" runat="server" ImageUrl="../images/itn/itncancel.jpg"
                        OnClick="itnCancel_Click" OnClientClick="return confirm(&quot;確定取消?&quot;);" />

                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="PnlDetail"
        Enabled="False" Color="#666666" BorderColor="#666666" Radius="6" Corners="Bottom">
    </cc1:RoundedCornersExtender>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phdMessage" runat="server">
</asp:Content>

