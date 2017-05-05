<%@ Page Title="PoReturn" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="PoReturn.aspx.cs" Inherits="WMS_v1._0.Web.PoReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/rackSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top" style="padding: 10px 0;">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>PO单号</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="id_po_NO" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>料号</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="id_item_Name" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>暂收单号</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="id_receipt_NO" runat="server" />
                        </td>
                        <td class="tdLab">
                            <asp:Button ID="commit_btn" CssClass="selectBtn" runat="server" Text="查询" OnClick="select_click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView
                    ID="GridView1"
                    SelectedIndex="0"
                    runat="server"
                    CssClass="mGrid"
                    PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt"
                    AllowPaging="True"
                    AutoGenerateColumns="False"
                    EmptyDataText="没有任何数据可以显示"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                    <Columns>
                        <asp:BoundField DataField="PO_NO" HeaderText="PO单号" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />
                        <asp:BoundField DataField="RECEIPT_NO" HeaderText="暂收单号" />
                        <asp:BoundField DataField="Line_num" HeaderText="Line Num" />
                        <asp:BoundField DataField="RCV_QTY" HeaderText="可退量" />
                        <asp:BoundField DataField="RETURN_qty" HeaderText="退回量" />
                        <asp:BoundField DataField="VENDOR_CODE" HeaderText="厂商代码" />

                        <asp:CommandField SelectText="选择" ShowSelectButton="True" ButtonType="Button" />
                    </Columns>
                    <SelectedRowStyle BackColor="LightCyan"
                        BorderStyle="Solid"
                        ForeColor="DarkBlue"
                        Font-Bold="true" />
                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
        </div>

        <div class="list_foot">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>暂收单号:</label>
                        </td>
                        <td class="tdInput">
                            <input id="receipt_no_id" class="input_text" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>库别:</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
                        </td>
                        <td class="tdLab">
                            <label>区域:</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList2" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>退回量:</label>
                        </td>
                        <td class="tdInput">
                            <input id="return_num_id" class="input_text" type="text" runat="server" />
                        </td>
                        <td class="tdLab" style="text-align: center;">
                            <asp:Button ID="Button4" CssClass="selectBtn" runat="server" Text="退回" OnClick="return_click" />
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
    <%--<div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader">
                <label>PO退回</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>暂收单号:</label>
                    <input id="receipt_no_id" class="input_text" type="text" readonly="readonly" runat="server" />
                </div>
                <div style="text-align: center">
                    <select id="select1" name="select1" style="margin-left: 15%;">
                        <option value="选择库别">选择库别</option>
                    </select>
                    <select id="select2" name="select2" style="text-align: center">
                        <option value="选择区域">选择区域</option>
                    </select>
                </div>
                <div>
                    <label>出货量:</label>
                    <input id="return_num_id" class="input_text" type="text" runat="server" />
                </div>

            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" Text="确定" OnClick="return_click" />
                <button type="button" id="btn_close">取消</button>
            </div>
        </div>
    </div>--%>
    <%-- <script src="JavaScript/poreturn.js" type="text/javascript"></script>--%>
</asp:Content>
