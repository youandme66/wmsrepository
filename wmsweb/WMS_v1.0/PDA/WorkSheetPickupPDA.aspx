<%@ Page Title="WorkSheetPickupPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="WorkSheetPickupPDA.aspx.cs" Inherits="WMS_v1._0.PDA.WorkSheetPickupPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- 打印按钮的弹框--%>
    <div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader">
                <label>打印备料单</label>
            </div>
            <div class="toggerBody">
                <div>
                    <input class="input_text" type="text" id="select_text_print" runat="server" placeholder="请输入备料单号" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button3" runat="server" OnClick="transPrint" Text="确定" />
                <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
            </div>
        </div>
    </div>
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture" style="display:none">
                <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>备料单号：</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="simulate_id" runat="server" placeholder="请输入字符" />

                        </td>
                        <td class="tdLab">
                            <button style="width: 70%" id="Button1" type="button" runat="server" onserverclick="Select">查询</button>
                        </td>
                        <td class="tdInput">
                            <asp:Label ID="Label1" runat="server" Text="工单号"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>条码信息：</label></td>
                        <td class="tdInput">
                            <input type="text" id="indc" placeholder="请输入“料号#数量#DateCode”" runat="server" />
                        </td>
                        <td class="tdLab">
                            <button style="width: 70%" id="Button2" type="button" runat="server" onserverclick="Pickup">备料</button>
                        </td>
                    </tr>
                </table>
            </div>
            </div>
        <div class="list_foot">
                <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" EmptyDataText="Data Is Empty" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="SIMULATE_LINE_ID" HeaderText="SIMULATE_LINE_ID" Visible="false" />
                        <asp:BoundField DataField="SIMULATE_ID" HeaderText="备料单号" />
                        <asp:BoundField DataField="WO_NO" HeaderText="工单号" />
                        <asp:BoundField DataField="WO_KEY" HeaderText="工单KEY值" Visible="false" />
                        <asp:BoundField DataField="ITEM_ID" HeaderText="料号ID" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />
                        <asp:BoundField DataField="REQUIREMENT_QTY" HeaderText="需求量" />
                        <asp:BoundField DataField="SIMULATED_QTY" HeaderText="模拟量" />
                        <asp:BoundField DataField="STATUS" HeaderText="状态" />
                        <asp:BoundField DataField="PICKUP_QTY" HeaderText="备料数量" />
                        <asp:BoundField DataField="ISSUED_QTY" HeaderText="发料数量" />
                    </Columns>
                </asp:GridView>
        </div>
    </div>
    
    <script src="JavaScript/PrintJS.js"></script>
</asp:Content>
