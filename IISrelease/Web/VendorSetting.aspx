<%@ Page Title="VendorSetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="VendorSetting.aspx.cs" Inherits="WMS_v1._0.Web.VendorSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/Po_PoLine.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" id="btn_clear" runat="server" onserverclick="clear">
                    <%--   <img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                    <%--<img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>供应商名称</label>
                        </td>
                        <td class="tdInput">
                            <input id="vendor_name_id" type="text" runat="server" />

                        </td>
                        <td class="tdLab">
                            <label>供应商代码</label>

                        </td>
                        <td class="tdInput">
                            <input id="vendor_key_id" type="text" runat="server" />

                        </td>
                        <td class="tdLab">
                            <asp:Button ID="Button1" CssClass="selectBtn" runat="server" Text="查询" OnClick="selectSome" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="Line_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th style="display: none">供应商ID</th>
                            <th>供应商名称</th>
                            <th>供应商代码</th>
                            <th>创建时间</th>
                            <th>更改时间</th>
                            <th>创建者</th>
                            <th>更改者</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="display: none"><%#Eval("vendor_ID ") %></td>
                        <td><%#Eval("vendor_NAME ") %></td>
                        <td><%#Eval("vendor_key") %></td>
                        <td><%#Eval("CREATE_TIME  ") %></td>
                        <td><%#Eval("UPDATE_TIME ") %></td>
                        <td><%#Eval("CREATE_BY") %></td>
                        <td><%#Eval("UPDATE_BY") %></td>

                        <td>
                            <button id="Button2" class="btn_update" runat="server" type="button">更新</button></td>
                        <td>
                            <button id="Button3" class="btn_delete" runat="server" type="button">删除</button></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader">
                <label>添加</label>
            </div>
            <asp:Panel ID="Panel2" runat="server" DefaultButton="Button4">
                <div class="toggerBody">
                    <div>
                        <label>供应商名称:</label>
                        <input id="vendor_name_insert" class="input_text" type="text" runat="server" />
                        <label>供应商代码:</label>
                        <input id="vendor_key_insert" class="input_text" type="text" runat="server" />
                    </div>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button4" runat="server" Text="确定" OnClick="notarizeInsert" />
                    <asp:Button ID="Button7" runat="server" Text="取消" OnClick="cancelInsert" />
                </div>
            </asp:Panel>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <asp:Panel runat="server" DefaultButton="Button5">
                <div class="toggerBody">
                    <div class="update_lab">
                        <label style="display: none">ID:</label>
                        <input style="display: none" id="vendor_id_update" readonly="readonly" runat="server" class="input_text" type="text" /><br />
                        <label>供应商名称:</label>
                        <input id="vendor_name_update" class="input_text" runat="server" type="text" /><br />
                        <label>供应商代码:</label>
                        <input id="vendor_key_update" class="input_text" runat="server" type="text" />
                    </div>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button5" runat="server" Text="确定" OnClick="notarizeUpdate" />
                    <button id="Button6" class="btn_close" type="button" runat="server">取消</button>
                </div>
            </asp:Panel>
        </div>

        <div id="div_togger3">
            <div class="toggerHeader">
                <label>删除</label>
            </div>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button8">
                <div class="toggerBody">
                    <div class="toggerBody_lab">
                        <label style="display: none">ID:</label>
                        <input style="display: none" id="vendor_id_delete" readonly="readonly" runat="server" class="input_text" type="text" /><br />
                        <label>供应商名称:</label>
                        <asp:Label ID="vendor_name_delete" runat="server" Text="Label" name="vendor_name_delete" ></asp:Label>
                       <%-- <input type="text" id="vendor_name_delete" readonly="true" runat="server" />--%>
                        <br />
                        <label>供应商代码:</label>
                        <asp:Label ID="vendor_key_delete" runat="server" Text="Label" name="vendor_key_delete" ></asp:Label>
                        <%--<input type="text" id="vendor_key_delete" readonly="true" runat="server" />--%>
                    </div>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button8" runat="server" Text="确定" OnClick="notarizeDelete" />
                    <button id="Button9" class="btn_close" type="button" runat="server">取消</button>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script src="JavaScript/vendorsetting.js"></script>
</asp:Content>
