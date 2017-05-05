<%@ Page Title="rackSettingPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="rackSettingPDA.aspx.cs" Inherits="WMS_v1._0.PDA.rackSettingPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="CSS/rackSettingPDA.css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" id="btn_clear" runat="server" onserverclick="Clear">
                    <%--  <img src="icon/clear.png" /><br />--%>
                    清空
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                    <%--  <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>料架名</label>
                        </td>
                        <td class="tdInput">
                            <input id="Frame_name1" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td class="tdLab">
                            <label>区域</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList2" runat="server"></asp:DropDownList>
                        </td>
                        <td class="tdLab">
                            <label>是否可用</label>
                        </td>
                        <td class="tdInput">
                            <select class="selectInput" id="Enabled1" runat="server">
                                <option>ALL</option>
                                <option>Y</option>
                                <option>N</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>创建者</label>
                        </td>
                        <td class="tdInput">
                            <input id="Create_by1" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>更改者</label>
                        </td>
                        <td class="tdInput">
                            <input id="Update_by1" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <asp:Button ID="Button1" CssClass="selectBtn" runat="server" OnClick="Select" Text="查询" />
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
                            <th>料架名</th>
                            <th>是否可用</th>
                            <th>区域名</th>
                            <th>创建者</th>
                            <th>创建时间</th>
                            <th>更改者</th>
                            <th>更新时间</th>
                            <th>描述</th>
                            <th>更新</th>
                            <th>删除</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("FRAME_KEY") %></td>
                        <td><%#Eval("FRAME_NAME") %></td>
                        <td><%#Eval("ENABLED") %></td>
                        <td><%#Eval("region_name") %></td>
                        <td><%#Eval("create_by") %></td>
                        <td><%#Eval("create_time") %></td>
                        <td><%#Eval("update_by") %></td>
                        <td><%#Eval("update_time") %></td>
                        <td><%#Eval("DESCRIPTION") %></td>
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
                    <label>料架名:</label>
                    <input id="Frame_name" class="input_text" type="text" runat="server" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select id="Enabled" class="select_text" runat="server">
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <%--<div>
                    <label>库别:</label>
                    <select id="Subinventory" class="select_text" name="Subinventory">
                        <option>--------select--------</option>
                    </select>
                </div>--%>
                <div>
                    <label>区域:</label>
                    <%--<asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>--%>
                    <asp:DropDownList class="select_text" ID="Region_key" runat="server"></asp:DropDownList>
                    <%--<select id="Region_key" class="select_text" name="Region_key">
                        <option>--------select--------</option>
                    </select>--%>
                </div>
                <%--<div>
                    <label>创建者:</label>
                    <input id="Create_by" readonly="true" class="input_text" type="text" runat="server" />
                </div>--%>
                <div>
                    <label>描述:</label>
                    <textarea id="Description" class="input_text" runat="server"></textarea>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" OnClick="Insert" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div class="update_lab">
                    <label hidden="hidden">料架号:</label>
                    <asp:Label ID="Label2" CssClass="label2_hidden" runat="server" Text="Label"></asp:Label>
                    <input id="Frame_key2" hidden="hidden" class="input_text" runat="server" type="text" />
                </div>
                <%--<div>
                    <label>创建者:</label>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    <input id="Create_by2" hidden="hidden" runat="server" class="input_text" type="text" />
                </div>--%>
                <div>
                    <label>料架名:</label>
                    <input id="Frame_name2" class="input_text" runat="server" type="text" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select id="Enabled2" class="select_text" runat="server">
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <%--<div>
                    <label>库别:</label>
                    <select id="Subinventory2" class="select_text" name="Subinventory2">
                    </select>
                </div>--%>
                <div>
                    <label>区域:</label>
                    <asp:DropDownList ID="Region_key2" class="select_text" runat="server"></asp:DropDownList>
                </div>
                <div>
                    <label>描述:</label>
                    <textarea id="Description2" class="input_text" runat="server"></textarea>
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
                    <label>料架名:</label>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <input type="text" id="lab" hidden="hidden" runat="server" />
                    <input type="text" id="enable" hidden="hidden" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" OnClick="Delete" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
    </div>
    <script src="JavaScript/rackSetting.js"></script>
</asp:Content>