<%@ Page Title="" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="workOrderShippingPDA.aspx.cs" Inherits="WMS_v1._0.PDA.workOrderShippingPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button3" class="btn_style" type="button" runat="server" onserverclick="cleanMassage">
                    清除
                </button>
            </div>
            <div class="cb">
                <div class="list_item">
                    <table class="tab">
                        <tr>
                            <td class="tdLab">
                                <label>出货单号</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="ship_no" placeholder="请输入字符" name="ship_no" />
                            </td>
                            <td class="tdLab">
                                <input id="Button1" class="selectBtn" type="button" value="确定" runat="server" onserverclick="searchCustomer" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLab">
                                <label>客户代码</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="customer_id" readonly="readonly" name="customer_id" />
                            </td>
                            <td class="tdLab">
                                <label>客户名称</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="customer_name" readonly="readonly" name="customer_name" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdLab">
                                <label>工单号</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="wo_no" placeholder="请输入字符" />
                            </td>
                            <td class="tdLab">
                                <input id="Button2" class="selectBtn" type="button" value="确定" runat="server" onserverclick="searchPartNo" />
                            </td>
                        </tr>
                        <tr>

                            <td class="tdLab">
                                <label>料号</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="part_no" readonly="readonly" />
                            </td>
                            <td class="tdLab">
                                <label>需求量</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="request_qty" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLab">
                                <label>出货数量</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="picked_qty" placeholder="请输入字符" />
                            </td>
                            <td class="tdLab">
                                <input id="Button4" class="selectBtn" type="button" value="出货" runat="server" onserverclick="workOrderShip" />
                            </td>
                        </tr>
                        <tr>

                            <td class="tdLab">
                                <label>已出货量</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="ship_qty" readonly="readonly" />
                            </td>
                            <td class="tdLab">
                                <label>状态</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="status" readonly="readonly" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="list_top">
            <label id="Label1" runat="server"></label>
            <asp:Repeater ID="workOrderShip_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th>出货单号</th>
                            <th>工单号</th>
                            <th>料号</th>
                            <%--<th>工单开工量</th>--%>
                            <th>工单出货量</th>
                            <th>出货人员</th>
                            <th>更新时间</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("SHIP_NO") %></td>
                        <td><%#Eval("WO_NO") %></td>
                        <td><%#Eval("part_no") %></td>
                        <%--<td><%#Eval("target_qty") %></td>--%>
                        <td><%#Eval("PICKED_QTY") %></td>
                        <td><%#Eval("SHIP_MAN") %></td>
                        <td><%#Eval("UPDATE_TIME")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="list_top">
            <label id="Label2" runat="server"></label>
            <asp:Repeater ID="enable_wo_no" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table2" class="mGrid" border="1">
                        <tr>
                            <th>工单号</th>
                            <th>料号</th>
                            <th>工单开工量</th>
                            <th>工单入库量</th>
                            <th>工单出货量</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("wo_no") %></td>
                        <td><%#Eval("part_no") %></td>
                        <td><%#Eval("target_qty") %></td>
                        <td><%#Eval("turnin_qty") %></td>
                        <td><%#Eval("shipped_qty") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>

        <script src="JavaScript/workOrderShipping.js"></script>
    </div>
</asp:Content>
