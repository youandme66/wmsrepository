<%@ Page Title="parametersSetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="parametersSetting.aspx.cs" Inherits="WMS_v1._0.Web.parametersSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/parametersSetting.css" rel="stylesheet" />
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" id="Button11" type="button" runat="server" onserverclick="parameters_select_clear">
                   <%-- <img src="icon/clear.png" /><br />--%>
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
                            <label>数据表名</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="Lookup_type" placeholder="请输入数字" />
                        </td>
                        <td class="tdLab">
                            <label>对应栏位</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="Lookup_code" placeholder="请输入字符" />
                        </td>
                        <td class="tdLab">
                            <label>数据字段</label>
                        </td>

                        <td class="tdInput">
                            <input type="text" runat="server" id="Meaning" placeholder="请输入字符" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>数据描述</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="Description" placeholder="请输入字符" />
                        </td>
                        <td class="tdLab">
                            <label>是否可用</label>
                        </td>
                        <td class="tdInput">
                            <select runat="server" id="Enabled">
                                <option value="">--</option>
                                <option value="Y">Y</option>
                                <option value="N">N</option>
                            </select>
                        </td>
                        <td class="tdLab ">
                            <button id="Button12" type="button" class="selectBtn "  runat="server" onserverclick="parameters_Select">查询</button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="parameters_repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th>数据表名</th>
                            <th>对应栏位</th>
                            <th>数据字段</th>
                            <th>数据描述</th>
                            <th>是否可用</th>
                            <th>创建者</th>
                            <th>创建时间</th>
                            <th>更改者</th>
                            <th>更改时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("LOOKUP_TYPE") %></td>
                        <td><%#Eval("LOOKUP_CODE") %></td>
                        <td><%#Eval("MEANING") %></td>
                        <td><%#Eval("DESCRIPTION") %></td>
                        <td><%#Eval("ENABLED") %></td>
                        <td><%#Eval("CREATE_BY") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_BY") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                        <td>
                            <button id="btn_update" class="btn_update" runat="server" type="button">更新</button></td>
                        <td>
                            <button id="btn_delete" class="btn_delete" runat="server" type="button">删除</button></td>
                        <td>
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
                    <label>数据表名:</label>
                    <input class="input_text" type="text" runat="server" placeholder="请输入数字" id="Lookup_type_insert" />
                </div>
                <div>
                    <label>对应栏位:</label>
                    <input class="input_text" type="text" runat="server" placeholder="请输入字符" id="Lookup_code_insert" />
                </div>
                <div>
                    <label>数据字段:</label>
                    <input class="input_text" type="text" runat="server" placeholder="请输入字符" id="Meaning_insert" />
                </div>
                <div>
                    <label>数据描述:</label>
                    <input class="input_text" type="text" runat="server" placeholder="请输入字符" id="Description_insert" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select class="select_text" runat="server" id="enabled_insert">
                        <option value="Y">Y</option>
                        <option value="N">N</option>
                    </select>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button18" runat="server" CssClass="btn_close" Text="确定" OnClick="parameters_Insert" />
                <asp:Button ID="Button15" runat="server" CssClass="btn_close" Text="取消" OnClick="parameters_insert_clear" />
            </div>
        </div>


        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>数据表名:</label>
                    <input class="input_text" id="Lookup_type_update" type="text" readonly="readonly" runat="server" />
                </div>
                <div>
                    <label>对应栏位:</label>
                    <input class="input_text" id="Lookup_code_update" type="text" runat="server" />
                </div>
                <div>
                    <label>数据字段:</label>
                    <input class="input_text" id="Meaning_update" type="text" runat="server" />
                </div>
                <div>
                    <label>数据描述:</label>
                    <input class="input_text" id="Description_update" type="text" runat="server" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select class="select_text" id="enabled_update" runat="server">
                        <option value="Y">Y</option>
                        <option value="N">N</option>
                    </select>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button14" runat="server" OnClick="parameters_Update" Text="确定" />
                <asp:Button ID="Button16" runat="server" OnClick="parameters_update_clear" Text="取消" />
            </div>
        </div>


        <div id="div_togger3">
            <div class="toggerHeader">
                <label>是否确定删除该参数信息？</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>数据表名:</label>
                    <input id="Lookup_type_Delet" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button17" runat="server" OnClick="parameters_Delete" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/parametersSetting.js"></script>
</asp:Content>
