<%@ Page Title="PoAcceptancePDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="PoAcceptancePDA.aspx.cs" Inherits="WMS_v1._0.PDA.PoAcceptancePDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- <link href="CSS/PoAcceptancePDA.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="CSS/PoAcceptance.css" rel="stylesheet" />
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button1" class="btn_style" type="button" runat="server" onserverclick="CleanAllMessage_Click">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>暂收单号</label>
                        </td>
                        <td class="tdInput">
                            <select class="user_name1" id="receipt_no" name="receipt_no" >
                                <option>请选择暂收单号</option>
                            </select>
                            <input runat="server" type="hidden" id="hiddent" name="hiddent" value="" />

                        </td>
                        <td class="tdLab">
                            <button id="Button2" class="selectBtn" type="button" runat="server" onserverclick="QueryMessage_Click">确定</button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="list_foot">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>允收量</label>
                        </td>
                        <td class="tdInput">
                            <input class="user_name1" type="text" runat="server" id="accepted_qty" placeholder="请输入数字" /><input class="receive1" type="text" runat="server" id="receive" hidden="hidden" />
                        </td>
                        <td class="tdLab">
                            <label>退回量</label>
                        </td>
                        <td class="tdInput">
                            <input class="user_name1" type="text" runat="server" id="return_qty" placeholder="请输入数字" />
                        </td>
                        <td class="tdLab">
                            <button id="Button5" class="selectBtn" type="button" runat="server" onserverclick="ModifyMessage_Click">修改</button>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="200px">
                <asp:GridView
                    ID="receiveMtl_gridview"
                    AllowPaging="true"
                    PageSize="1"
                    PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt"
                    CssClass="mGrid"
                    runat="server"
                    EmptyDataText="Data Is Empty"
                    AutoGenerateColumns="False" OnPageIndexChanging="receiveMtl_gridview_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Receipt_no" HeaderText="暂收单号" />
                        <asp:BoundField DataField="Item_id" HeaderText="料号ID" />
                        <asp:BoundField DataField="Item_name" HeaderText="料号" />
                        <asp:BoundField DataField="Rcv_qty" HeaderText="暂收量" />
                        <asp:BoundField DataField="Accepted_qty" HeaderText="允收数量" />
                        <asp:BoundField DataField="Return_qty" HeaderText="退回量" />
                        <asp:BoundField DataField="Deliver_qty" HeaderText="入库量" />
                        <asp:BoundField DataField="Po_no" HeaderText="采购单号" />
                        <asp:BoundField DataField="Vendor_code" HeaderText="厂商代码" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
    <script src="JavaScript/PoAcceptance.js"></script>
</asp:Content>
