<%@ Page Title="DCSerachPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="DCSerachPDA.aspx.cs" Inherits="WMS_v1._0.PDA1.DCSerachPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>料号名</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="Item" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="tdLab">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="Subinventory" runat="server">
                            </asp:DropDownList>
                        </td>
                        <%--  <td>
                            <label>DateCode</label>
                        </td>
                        <td colspan="2">
                            <input type="text" runat="server" id="Datecode" />
                        </td>
                        <td>
                            <label>料架名</label>
                        </td>
                        <td colspan="2">
                             <asp:DropDownList ID="Frame_name" runat="server">
                            </asp:DropDownList>
                        </td>--%>
                        <td class="tdLab">
                            <button id="Button1" style="margin-left: 20px" runat="server" onserverclick="QueryStorage_Click">查询</button>
                        </td>
                        <td>
                            <%--                            <button id="Button2" style="margin-left: 20px" runat="server">资料导出</button>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <label>库存总表:</label>
            <asp:Repeater ID="Storage" runat="server">
                <HeaderTemplate>
                    <table id="Top_Table" class="mGrid" border="1">
                        <tr>
                            <th>料名</th>
                            <th>库别名</th>
                            <th>在手总量</th>
                            <th>创建时间</th>
                            <th>更改时间</th>
                            <th>查询库存明细</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("SUBINVENTORY") %></td>
                        <td><%#Eval("ONHAND_QUANTIY") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                        <td onclick="showDetial(this)">
                            <button id="Button11" class="btn_query_poLine" runat="server" type="button">查询库存明细</button>
                        </td>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>


        <div class="list_foot">
            <label>库存明细表:</label>
            <asp:Repeater ID="Storage_Detail" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">库别名</th>
                            <th>明细表ID</th>
                            <th>料名</th>
                            <th>料架名</th>
                            <th>生产周期</th>
                            <th>在手量</th>
                            <th>模拟量</th>
                            <th>剩余量</th>
                            <th>退料标志</th>
                            <th>最后一次复检时间</th>
                            <th>最后一次复检状态</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr_hide">
                        <td hidden="hidden"><%#Eval("SUBINVENTORY_NAME") %></td>
                        <td><%#Eval("UNIQUE_ID") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("FRAME_NAME") %></td>
                        <td><%#Eval("DATECODE") %></td>
                        <td><%#Eval("ONHAND_QTY") %></td>
                        <td><%#Eval("SIMULATED_QTY") %></td>
                        <td><%#Eval("LEFT_QTY") %></td>
                        <td><%#Eval("RETURN_FLAG") %></td>
                        <td><%#Eval("LAST_REINSPECT_TIME") %></td>
                        <td><%#Eval("LAST_REINSPECT_STATUS") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>


    </div>
    <script src="JavaScript/DCserach.js"></script>
</asp:Content>
