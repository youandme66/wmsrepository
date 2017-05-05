<%@ Page Title="DCSerach" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="DCSerach.aspx.cs" Inherits="WMS_v1._0.Web.DCSerach" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
          
                <div class="list_item">
                    <table class="tab">
                        <tr>
                            <td>
                                <label>料号</label>
                            </td>
                            <td colspan="2">
                                <input type="text" runat="server" id="item_name" />
                            </td>
                            <td>
                                <label>DateCode</label>
                            </td>
                            <td colspan="2">
                                <input type="text" runat="server" id="datecode" />
                            </td>
                            <td>
                                <label>库别</label>
                            </td>
                            <td colspan="2">
                                <input type="text" runat="server" id="subinventory_name" />
                            </td>
                            <td>
                                <button id="Button1"  style="margin-left: 20px" runat="server" onserverclick="Button1_Click">查询</button>
                            </td>
                            <td>
                                <button id="Button2"  style="margin-left: 20px" runat="server" onserverclick="button2_Click">资料导出</button>
                            </td>
                        </tr>
                    </table>
                </div>
          
            <%--  <div class="row top_padding">
                <label class="col-md-1 text_right">料号</label>
                <input id="item_name" type="text" class="col-md-1" runat="server" />
                <label class="col-md-1 text_right">DateCode</label>
                <input id="datecode" type="text" class="col-md-2" value="%" runat="server" />
            </div>

            <div class="row top_padding">
                <label class="col-md-1 text_right">库别</label>
                <input id="subinventory_name" type="text" class="col-md-1" runat="server" />
                <button id="Button1" class="col-md-1 col-md-offset-1 " runat="server" onserverclick="Button1_Click">查询</button>
                <button id="Button2" class="col-md-1 " style="margin-left: 20px" runat="server" onserverclick="button2_Click">资料导出</button>
            </div>--%>
        </div>
    </div>

    <div class="list_foot">
        <asp:GridView
            ID="GridView1"
            runat="server"
            class="col-md-10"
            AutoGenerateColumns="False"
            EmptyDataText="没有任何数据可以显示">
            <Columns>
                
                <asp:BoundField DataField="item_id" HeaderText="料号" />
                <asp:BoundField DataField="subinventory" HeaderText="库别" />
                <asp:BoundField DataField="frame_key" HeaderText="料架" />
                <asp:BoundField DataField="datecode" HeaderText="DC" />
                <asp:BoundField DataField="onhand_qty" HeaderText="在手量" />
                <asp:BoundField DataField="simulated_qty" HeaderText="模拟量" />
                <asp:BoundField DataField="left_qty" HeaderText="剩余量" />
                <asp:BoundField DataField="return_flag" HeaderText="退料标示" />
                <asp:BoundField DataField="last_reinspect_status" HeaderText="最后复验状态" />
                <asp:BoundField DataField="last_reinspect_time" HeaderText="最后复验时间" />
                <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                <asp:BoundField DataField="update_time" HeaderText="更新时间" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
