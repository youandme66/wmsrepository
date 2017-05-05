<%@ Page Title="库存查询" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="InventoryQuery.aspx.cs" Inherits="WMS_v1._0.Web.InventoryQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/DealDetial.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../stylesheets/normalize.css" />
    <link type="text/css" rel="stylesheet" href="../stylesheets/stylesheet.css" />
    <link type="text/css" rel="stylesheet" href="../stylesheets/github-light.css" />
    <link type="text/css" rel="stylesheet" href="../jedate/skin/jedate.css" />
    <script type="text/javascript" src="../jedate/jedate.js"></script>

    <div class="list">
        <div class="list_top">
             <div class="list_top_picture">
                <button class="btn_style" id="Button3" type="button" runat="server" onserverclick="cleanMassage">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
               
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                                          
                        <td>
                            <label>料号</label>
                        </td>
                    
                         <td class="tdInput">
                            <input id="item_name" type="text" runat="server"  />
                        </td>

                         <td>
                            <label>库别</label>
                        </td>
                     
                         <td class="tdInput">
                            <input id="subinventory_name" type="text" runat="server"  />
                        </td>
                    </tr>
                    
                    <tr>                       
                        <td>
                            <label></label>
                        </td>
                        <td>
                            <button id="Button1" type="button" runat="server" onserverclick="Select">查询</button>

                        </td>
                        <td>
                            <label></label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <span>库存总表：</span>
            <asp:GridView
                ID="GridView_header"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                runat="server"
                EmptyDataText="Data Is Empty"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="item_id" HeaderText="料号ID" />
                    <asp:BoundField DataField="item_name" HeaderText="料号" /> 
                    <asp:BoundField DataField="subinventory" HeaderText="库别" />
                    <asp:BoundField DataField="onhand_quantiy" HeaderText="在手总量" />
                    <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                    <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="list_foot">
            <span>库存明细表：</span>
            <asp:GridView
                ID="GridView_line"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                runat="server"
                EmptyDataText="Data Is Empty"
                AutoGenerateColumns="False">
                <Columns>     
                    <asp:BoundField DataField="unique_id" HeaderText="unique_id" />
                    <asp:BoundField DataField="item_name" HeaderText="料号" />
                    <asp:BoundField DataField="frame_name" HeaderText="料架" />
                    <asp:BoundField DataField="datecode" HeaderText="生产周期" />
                    <asp:BoundField DataField="onhand_qty" HeaderText="在手量" />
                    <asp:BoundField DataField="simulated_qty" HeaderText="模拟量" />
                    <asp:BoundField DataField="left_qty" HeaderText="剩余量" />
                    <asp:BoundField DataField="return_flag" HeaderText="退料标志" />
                    <asp:BoundField DataField="last_reinspect_time" HeaderText="最后一次复验时间" />
                    <asp:BoundField DataField="last_reinspect_status" HeaderText="最后一次复验状态" />
                    <asp:BoundField DataField="create_time" HeaderText="创建时间" />               
                    <asp:BoundField DataField="update_time" HeaderText="更新时间" />                 
                </Columns>
            </asp:GridView>
        </div>
    </div>
  
</asp:Content>
