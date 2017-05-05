<%@ Page Title="ShipSettingPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="ShipSettingPDA.aspx.cs" Inherits="WMS_v1._0.PDA.ShipSettingPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .hidden {
            display: none;
        }
    </style>
    <div class="list">
        <div class="list_top">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>客户名称</label>
                        </td>
                        <td class="tdInput">
                            <select class="selectInput" id="customer_name_c" name="customer_name_c">
                                <option>ALL</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <button id="Select_Top" class="selectBtn" type="button" runat="server" onserverclick="Top_Select">查找</button>
                        </td>
                        <td class="tdLab">
                            <button type="button" class="selectBtn" id="btn_insert">插入新数据</button>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                    <label>出货单单头:</label>

                    <asp:GridView
                        ID="Top_GridView"
                        SelectedIndex="-1"
                        runat="server"
                        CssClass="mGrid"
                        AllowPaging="True"
                        PageSize="15"
                        PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt"
                        AutoGenerateColumns="False" OnPageIndexChanging="Top_GridView_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="ship_key" HeaderText="ID">
                                <FooterStyle CssClass="hidden" />
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_name" HeaderText="客户名称" />
                            <asp:BoundField DataField="item_name" HeaderText="料号"></asp:BoundField>
                            <asp:BoundField DataField="ship_no" HeaderText="出货单号" />
                            <asp:BoundField DataField="request_qty" HeaderText="需求量" />
                            <asp:BoundField DataField="picked_qty" HeaderText="出货量" />
                            <asp:BoundField DataField="status" HeaderText="状态" />
                            <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                            <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                            <asp:ButtonField Text="编辑" ButtonType="Button" HeaderText="编辑">
                                <ControlStyle CssClass="Top_Edit" />
                            </asp:ButtonField>
                            <asp:ButtonField Text="删除" ButtonType="Button" HeaderText="删除">
                                <ControlStyle CssClass="Top_Delete" />
                            </asp:ButtonField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightCyan"
                            BorderStyle="Solid"
                            ForeColor="DarkBlue"
                            Font-Bold="true" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>


        <div id="zindex">

            <div id="div_togger1">
                <div class="toggerHeader">
                    <label>添加单头数据</label>
                </div>
                <div class="toggerBody">
                    <div>
                        <label>客户名称:</label>
                        <select class="select_text" id="insert_customer_name_c" name="insert_customer_name_c"></select>
                    </div>
                    <div>
                        <label>出货单号:</label>
                        <input class="input_text" type="text" runat="server" id="top_insert_ship_no" />
                        <input class="input_text" type="button" id="get_new_ship_no" onclick="getNew_ship_no()" value="新出货单号" />
                    </div>
                    <div>
                        <label>料号:</label>
                        <select class="select_text" id="insert_item_name_c" name="insert_item_name_c"></select>
                    </div>
                    <div>
                        <label>需求量:</label>
                        <input class="input_text" type="text" runat="server" id="top_insert_request_qty" />
                    </div>
                    <div hidden="hidden">
                        <label>出货量:</label>
                        <input class="input_text" type="text" value="0" runat="server" id="top_insert_picked_qty" />
                    </div>

                    <div class="toggerFooter">
                        <asp:Button ID="Submit_Top_Insert" runat="server" Text="确定" OnClick="Top_Insert" />
                        <button class="btn_close" type="button">取消</button>
                    </div>
                </div>
            </div>


            <div id="div_togger2">
                <div class="toggerHeader">
                    <label>更新单头数据</label>
                </div>
                <div class="toggerBody">
                    <div>
                        <input class="input_text" type="text" hidden="hidden" readonly="readonly" runat="server" id="edit_ship_key" />
                    </div>
                    <div>
                        <input class="input_text" type="text" hidden="hidden" runat="server" id="edit_ship_no" />
                    </div>
                    <div>
                        <label>客户名称:</label>
                        <input class="input_text" type="text" runat="server" id="edit_customer" />
                    </div>
                    <div>
                        <label>料号:</label>
                        <input class="input_text" type="text" runat="server" id="edit_part_no" />
                    </div>
                    <div>
                        <label>需求量:</label>
                        <input class="input_text" type="text" runat="server" id="Top_request_qty" />
                    </div>
                    <div hidden="hidden">
                        <label>出货量:</label>
                        <input class="input_text" type="text" runat="server" id="Top_picked_qty" />
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Submit_Top_Update" runat="server" Text="确定" OnClick="Top_Edit" />
                        <button class="btn_close" type="button">取消</button>
                    </div>
                </div>
            </div>


            <div id="div_togger3">
                <div class="toggerHeader">
                    <label>确定删除该条信息?</label>
                </div>
                <div class="toggerBody">
                    <div>
                        <input class="input_text" type="text" hidden="hidden" readonly="readonly" runat="server" id="delete_id" />
                    </div>
                    <div>
                        <label>客户名称:</label>
                        <input class="input_text" readonly="readonly" type="text" runat="server" id="delete_customer" />
                    </div>
                    <div>
                        <label>料号：</label>
                        <input readonly="readonly" class="input_text" type="text" runat="server" id="delete_item_name1" />
                    </div>
                    <div>
                        <label>需求量:</label>
                        <input readonly="readonly" class="input_text" type="text" runat="server" id="delete_request_qty" />
                    </div>
                    <div>
                        <label>出货量:</label>
                        <input class="input_text" readonly="readonly" type="text" runat="server" id="delete_picked_qty1" />
                    </div>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Submit_Top_Delete" runat="server" Text="确定" OnClick="Top_Delete" />
                    <button class="btn_close" type="button">取消</button>
                </div>
            </div>
        </div>
    </div>
    <script src="JavaScript/Ship_project.js"></script>
</asp:Content>
