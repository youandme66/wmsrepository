<%@ Page Title="PnMaterialsIssue" Language="C#" AutoEventWireup="true" MasterPageFile="~/Web/headerFooter.Master" CodeBehind="PnMaterialsIssue.aspx.cs" Inherits="WMS_v1._0.Web.PnMaterialsIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/BomSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" id="Button1" type="button" runat="server" onserverclick="Clean_input_Click">
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
                            <label>料号</label></td>
                        <td class="tdInput">
                            <input id="ItemName_pn" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>料号描述</label></td>
                        <td class="tdInput">
                            <input type="text" id="ItemDesc_pn" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>单位</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="UOM_pn" runat="server" />
                        </td>
                        <td class="tdLab">
                            <asp:Button ID="SelectPn" CssClass="selectBtn" runat="server" Text="查询" OnClick="Select_Click_Pn" />
                            <%--   <button type="button"  onclick="Select_Click_Pn">select</button>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>


        <div class="list_foot">
            <asp:Repeater ID="Pn_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th>item_id</th>
                            <th>料号</th>
                            <th>料号描述</th>
                            <th>单位</th>
                            <th>创建者</th>
                            <th>更改者</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ITEM_ID") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("ITEM_DESC") %></td>
                        <td><%#Eval("UOM") %></td>
                        <td><%#Eval("CREATE_USER")%></td>
                        <td><%#Eval("UPDATE_USER")%></td>
                        <td><%#Eval("CREATE_TIME")%></td>
                        <td><%#Eval("UPDATE_TIME")%></td>
                        <td>
                            <button id="btn_update" class="btn_update" runat="server" type="button">修改</button></td>

                        <td>
                            <button id="btn_delete" class="btn_delete" runat="server" type="button">删除</button></td>
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
            <asp:Panel runat="server" DefaultButton="Button3">
                <div class="toggerBody">
                    <div>
                        <label>料号:</label>
                        <input id="ItemName_insert_pn" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>

                    <div>
                        <label>料号描述:</label>
                        <input id="ItemDesc_insert_pn" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>
                    <div>
                        <label>单位:</label>
                        <input id="Uom_insert_pn" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>
                    <%--   <div>
                    <label>创建者:</label>
                    <input id="Create_user_pn" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                </div>--%>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button3" CssClass="btn_close" runat="server" Text="确定" OnClick="Insert_Click_pn" />
                    <asp:Button ID="Button2" CssClass="btn_close" runat="server" Text="取消" OnClick="Cancle_insert_pn" />
                    <%--  <button class="btn_close" type="button"  runat="server" >确定</button>--%>
                    <%--  <button class="btn_close" type="button">取消</button>--%>
                </div>
            </asp:Panel>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <asp:Panel runat="server" DefaultButton="Button4">
                <div class="toggerBody">
                    <div>
                        <label>料号:</label>
                        <input id="ItemName_update_pn" class="input_text" readonly="readonly" placeholder="请输入字符" type="text" runat="server" />
                    </div>
                    <div>
                        <label>料号描述:</label>
                        <input id="ItemDesc_update_pn" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>
                    <div>
                        <label>单位:</label>
                        <input type="text" id="Uom_update_pn" class="input_text" placeholder="请输入字符" runat="server" />
                    </div>
                    <%--  <div>
                    <label>更改者:</label>
                    <input id="Update_User_pn" class="input_text" runat="server" placeholder="请输入字符" type="text" />
                </div>--%>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button4" runat="server" CssClass="btn_close" Text="确定" OnClick="Update_Click_Pn" />
                    <asp:Button ID="Button5" runat="server" Text="取消" CssClass="btn_close" OnClick="Cancle_update_pn" />
                    <%--      <button id="Button1" class="btn_close" type="button" runat="server" >确定</button>--%>
                    <%--    <button class="btn_close" type="button">取消</button>--%>
                </div>
            </asp:Panel>
        </div>


        <div id="div_togger3">
            <asp:Panel runat="server" DefaultButton="Button6">
                <div class="toggerHeader">
                    <label>是否确定删除料号？</label>
                </div>
                <div class="toggerBody">
                    <div>
                        <label>料号:</label>
                        <input id="ItemName_delete_pn" class="input_text" runat="server" readonly="readonly" type="text" />
                    </div>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button6" runat="server" Text="确定" OnClick="Delect_Click_Pn" />
                    <button class="btn_close" type="button">取消</button>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script src="JavaScript/PnMaterialsIssue.js"></script>
</asp:Content>
