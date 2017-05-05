<%@ Page Title="" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="ReinspectionOperation.aspx.cs" Inherits="WMS_v1._0.Web.ReinspectionOperation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/PoStorage.css" rel="stylesheet" />
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button3" class="btn_style" type="button" runat="server" onserverclick="CleanAllMeassage_Click">
                    清除
                </button>
            </div>
            <div class="cb">
                <div class="list_item">
                    <table class="tab">
                        <tr>
                            <td class="tdLab">
                                <label>料号</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="select_name_reinspect" placeholder="请输入字符" />
                                <%--  <select id="select_name_reinspect" name="select_name_reinspect"  runat="server">                  
                            </select>--%>
                            </td>
                            <td class="tdLab">
                                <label>DateCode</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="DateCode_reinspect" placeholder="请输入字符" />
                            </td>
                            <td class="tdLab">
                                <label>料架</label>
                            </td>
                            <td class="tdInput">
                                <select id="frame_name_reinspect" name="frame_name_reinspect">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLab">
                                <label>复验结果</label>
                            </td>
                            <td class="tdInput">
                                <select id="reinspect_select" name="reinspect_select" runat="server">
                                    <option value="PASS">PASS</option>
                                    <option value="PENG">PENDING</option>
                                </select>
                            </td>
                            <td class="tdLab">
                                <label>备注</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="remark_reinspect" placeholder="请输入字符" />
                            </td>
                            <td class="tdLab">
                                <input id="Button2" class="selectBtn" type="button" value="提交" runat="server" onserverclick="Commite_reinspect" />
                            </td>
                            <td class="tdLab">
                                <input id="Button1" class="selectBtn" type="button" value="已复验" runat="server" onserverclick="pass" />
                            </td>
                            <td class="tdLab">
                                <input id="Button5" class="selectBtn" type="button" value="待复验" runat="server" onserverclick="pending" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="list_foot">
            <label runat="server" id="table_type"></label>
            <asp:Repeater ID="Reinspect_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>

                            <th hidden="hidden">ID</th>
                            <th hidden="hidden">料号ID</th>
                            <th>料号</th>
                            <th>DateCode</th>
                            <th hidden="hidden">料架ID</th>
                            <th>料架</th>
                            <th>复验状态</th>
                            <th>备注</th>
                            <th>检验人员</th>
                            <th>检验时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("UNIQUE_ID") %></td>
                        <td hidden="hidden"><%#Eval("ITEM_ID") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("DATECODE") %></td>
                        <td hidden="hidden"><%#Eval("FRAME_KEY") %></td>
                        <td><%#Eval("FRAME_NAME") %></td>
                        <td><%#Eval("STATUS") %></td>
                        <td><%#Eval("REMARK") %></td>
                        <td><%#Eval("CHECK_USER")%></td>
                        <td><%#Eval("CHECK_TIME")%></td>
                        <%--   <td><button id="btn_choose" class="btn_choose" runat="server" type="button"  >选择</button></td>--%>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        <%--</div>

        <div class="list_foot">--%>
            <label runat="server" id="Label1"></label>
            <asp:Repeater ID="Reinspect_Repeater2" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table2" class="mGrid" border="1">
                        <tr>

                            <th hidden="hidden">ID</th>
                            <th hidden="hidden">料号ID</th>
                            <th>料号</th>
                            <th>DATECODE</th>
                            <th>最后一次复验时间</th>
                            <th hidden="hidden">料架ID</th>
                            <th>料架</th>
                            <th>最后一次复验状态</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("UNIQUE_ID") %></td>
                        <td hidden="hidden"><%#Eval("ITEM_ID") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("DATECODE") %></td>
                        <td><%#Eval("last_reinspect_time") %></td>
                        <td hidden="hidden"><%#Eval("FRAME_KEY") %></td>
                        <td><%#Eval("FRAME_NAME") %></td>
                        <td><%#Eval("last_reinspect_status") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
            </br>
        </div>
        <script src="JavaScript/ReinspectionOperation.js"></script>
    </div>

</asp:Content>
