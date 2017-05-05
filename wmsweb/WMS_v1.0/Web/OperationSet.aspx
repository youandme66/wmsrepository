<%@ Page Title="OperationSet" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="OperationSet.aspx.cs" Inherits="WMS_v1._0.Web.OperationSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/BomSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link href="CSS/OperationSet.css" rel="stylesheet" />--%>
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" id="btn_clear" runat="server" onserverclick="Clear">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                    <%--<img src="icon/insert.png" />--%>
                    <%--<br />--%>
                    插入
                </button>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>制程代号</label>
                        </td>
                        <td class="tdInput">
                            <input id="Route_id" type="text" runat="server"  placeholder="请输入制程代号"/>
                        </td>
                        <td class="tdLab" hidden="hidden">
                            <label>创建者</label></td>
                        <td class="tdInput" hidden="hidden">
                            <input id="Create_by_id" type="text" runat="server"  placeholder="请输入字符"/>
                        </td>
                        <td class="tdLab" hidden="hidden">
                            <label>更改者</label></td>
                        <td class="tdInput" hidden="hidden" >
                            <input id="Update_by_id" type="text" runat="server"  placeholder="请输入字符"/>
                        </td>
                        <td class="tdLab" style="text-align: center;">
                            <asp:Button ID="Button1" runat="server" OnClick="Select" Text="查询" />

                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot" id="list_foot">
            <asp:Repeater ID="Line_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">制程id</th>
                            <th>制程代号</th>
                            <th>描述</th>
                            <th>创建者</th>
                            <th>创建时间</th>
                            <th>更改者</th>
                            <th>更改时间</th>
                            <th>更新</th>
                            <th>删除</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("ROUTE_ID")%></td>
                        <td><%#Eval("ROUTE")%></td>
                        <td><%#Eval("DESCRIPTION")%></td>
                        <td><%#Eval("CREATE_BY")%></td>
                        <td><%#Eval("CREATE_TIME")%></td>
                        <td><%#Eval("UPDATE_BY")%></td>
                        <td><%#Eval("UPDATE_TIME")%></td>
                        <td>
                            <button id="Button1" class="btn_update" runat="server" type="button">更新</button></td>
                        <td>
                            <button id="Button2" class="btn_delete" runat="server" type="button">删除</button></td>
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
                    <label>制程代号</label>
                    <input id="Route_id1" class="input_text" type="text" runat="server" />
                </div>
                <div>
                    <label>描述</label>
                    <input id="Description_id1" class="input_text" type="text" runat="server" />
                </div>

            </div>

            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" OnClick="Insert" Text="确定" />
                <asp:Button ID="Button3" runat="server" OnClick="Inclear" Text="取消" />
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div hidden ="hidden">
                    <label>创建者:</label>
                    <input id="Create_by_id2" class="input_text" runat="server" type="text"  disabled="disabled" />
                </div>
                <div class="update_lab" hidden="hidden">
                    <label>制程id:</label>
                    <asp:Label ID="Label2" runat="server" Text="Label" ></asp:Label>
                    <input id="Route2" hidden="hidden" class="input_text" runat="server" type="text" />
                </div>
                
                <div>
                    <label>制程代号:</label>
                    <input id="Route_id2" class="input_text" runat="server" type="text" readonly="readonly"/>
                </div>
                <div>
                    <label>描述:</label>
                    <input id="Description_id2" class="input_text" runat="server" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button5" OnClick="Update" runat="server" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
        <div id="div_togger3">
            <div class="toggerHeader">
                <label>删除</label>
            </div>
            <div class="toggerBody">
                <div class="toggerBody_lab">
                    <label>制程代号:</label>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    <input type="text" id="Route_id3" hidden="hidden" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" OnClick="Delete" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
    </div>
    <script src="JavaScript/OperationSet.js"></script>
</asp:Content>
