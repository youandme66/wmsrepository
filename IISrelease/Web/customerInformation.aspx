<%@ Page Title="customerInformation" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="customerInformation.aspx.cs" Inherits="customerInformation.customerInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/Po_PoLine.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link href="CSS/customerInformation.css" rel="stylesheet"/>--%>
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" id="btn_clear" runat="server" onserverclick="clear">
                    <%--<img src="icon/clear.png" /><br />--%>
                   清除
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                   <%-- <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>客户名称</label>

                        </td>
                        <td class="tdInput">
                            <input id="user_name1" type="text" runat="server" />

                        </td>
                        <td class="tdLab">
                            <label>客户代码</label>

                        </td>
                        <td class="tdInput">
                            <input id="code1" type="text" runat="server" />

                        </td>
                        <td class="tdLab">
                            <asp:Button ID="Button8" CssClass="selectBtn" runat="server"  Text="查询" OnClick="selectSomeBySome"/>
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
                            <th style="display:none">key值</th>
                            <th>客户代码</th>
                            <th>客户名称</th>
                            <th>创建时间</th>
                            <th>更改时间</th>
                            <th>创建者</th>
                            <th>更改者</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="display:none"><%#Eval("customer_key") %></td>
                        <td><%#Eval("Customer_code") %></td>
                        <td><%#Eval("customer_NAME") %></td>
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
            <div class="toggerBody">
                <div>
                    <label>客户代码:</label>
                    <input id="customer_code1" class="input_text" type="text" runat="server" />
                </div>
                <div>
                    <label>客户名称:</label>
                    <input id="user_name" class="input_text" type="text" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" Text="确定" OnClick="notarizeInsert"/>
                <asp:Button ID="Button7" runat="server" Text="取消" OnClick="cancelInsert"/>
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div class="update_lab">
                    <input id="key1" type="hidden" runat="server"/>
                    <label>客户代码:</label>
                    <input id="Label2" class="input_text" runat="server" type="text" /><br />
                    <label>客户名称:</label>
                    <input id="user_name2" class="input_text" runat="server" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button5" runat="server" Text="确定" OnClick="notarizeUpdate"/>
                <button class="btn_close" type="button" runat="server">取消</button>
            </div>
        </div>

        <div id="div_togger3">
            <div class="toggerHeader">
                <label>删除</label>
            </div>
            <div class="toggerBody">
                <div class="toggerBody_lab">
                    <label>客户名称:</label>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <input type="text" id="lab" hidden="hidden" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" Text="确定" OnClick="notarizeDelete"/>
                <button class="btn_close" type="button" runat="server">取消</button>
            </div>
        </div>
    </div>
    <script src="JavaScript/customerInformation.js"></script>
</asp:Content>
