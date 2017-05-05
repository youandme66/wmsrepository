<%@ Page Title="AllocationPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="AllocationPDA.aspx.cs" Inherits="WMS_v1._0.PDA.AllocationPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <%--<a href="#" class="picture_width" style="outline: 0;">
                    <img src="icon/clear.png" /><br />
                    <label>Clear</label>
                </a>--%>
                <button class="btn_style" type="button">
                   <%-- <img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <%--<a href="#" class="picture_width" style="outline: 0;">
                    <img src="icon/insert.png" /><br />
                    <label>Insert</label>
                </a>--%>
                <button class="btn_style"  type="button">
                    <%--<img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>调拨单号</label>

                        </td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" id="invoice_no" runat="server" />

                        </td>
                        <td class="tdLab">
                            <label>调拨料号</label>

                        </td>
                        <td class="tdInput">
                            <select class="InputStyle" id="item_name" runat="server">
                                <option>aaaaaaaaaa</option>
                            </select>

                        </td>
                        <td class="tdLab">
                            <label>品名</label>

                        </td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>剩余量</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>调出ORG</label></td>
                        <td class="tdInput">
                            <input class="InputStyle"  type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>调入ORG</label></td>
                        <td class="tdInput">
                            <input  class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>类别</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>数量</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" id="required_qty" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>调出库别</label></td>
                        <td class="tdInput">

                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>调入库别</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>备注</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        
                        <td class="tdLab">
                            <button id="Button1" style="width:80%;" type="button"  runat="server" onserverclick="Insert">提交</button>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>调出储位</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>调入储位</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>申请量</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" type="text" disabled="disabled" />
                        </td>

                    </tr>

                </table>
            </div>
        </div>

        <div class="list_foot">
            <div class="cb_top">
                <label>调拨单号</label>
                <input  type="text" id="invoice_no1" runat="server" />
                <button id="Button2" style="width:50px" type="button" runat="server" onserverclick="Select">?</button>
            </div>
            <asp:GridView ID="GridView1"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                AllowPaging="true"
                PageSize="6"
                runat="server"
                EmptyDataText="Data Is Empty"
                AutoGenerateColumns="False"
                OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="invoice_no" HeaderText="调拨单号" />
                    <asp:BoundField DataField="out_subinventory_key" HeaderText="调出库别" />
                    <asp:BoundField DataField="in_subinventory_key" HeaderText="调入库别" />
                    <asp:BoundField DataField="status" HeaderText="状态" />
                    <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                    <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                    <%--<asp:BoundField DataField="EXCHANGED_WO_NO" HeaderText="调拨工单号" />--%>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView2" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="6" runat="server" EmptyDataText="Data Is Empty" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="item_id" HeaderText="料号ID" />
                    <asp:BoundField DataField="item_name" HeaderText="调拨料号" />
                    <asp:BoundField DataField="required_qty" HeaderText="需求量" />
                    <asp:BoundField DataField="exchanged_qty" HeaderText="调拨数量" />
                    <asp:BoundField DataField="exchanged_time" HeaderText="调拨时间" />
                    <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                    <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                    <asp:BoundField DataField="exchange_wo_no" HeaderText="调拨工单号" />
                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>