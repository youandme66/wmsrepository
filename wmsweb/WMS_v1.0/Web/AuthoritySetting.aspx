<%@ Page Title="AuthoritySetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="AuthoritySetting.aspx.cs" Inherits="WMS_v1._0.Web.AuthoritySetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/BomSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--   <link href="CSS/AuthoritySetting.css" rel="stylesheet" />--%>
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" runat="server" onserverclick="CleanAllMeassage_Click">
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
                            <label>用户名</label>
                        </td>
                        <td class="tdInput">
                            <select id="user_id_Authority" name="user_id_Authority">
                                <option></option>
                            </select>
                        </td>

                        <td class="tdLab">
                            <label>界面名称</label>
                        </td>
                        <td class="tdInput">
                            <select id="program_id_Authority" name="program_id_Authority">
                                <option></option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>是否可用</label>
                        </td>
                        <td class="tdInput">
                            <select runat="server" id="select_id_Authority">
                                <option value=""></option>
                                <option value="Y">Y</option>
                                <option value="N">N</option>
                            </select>
                        </td>

                        <td class="tdLab">
                            <asp:Button ID="SelectPn" CssClass="selectBtn" runat="server" Text="查询" OnClick="Select_Authority" />
                        </td>
                    </tr>
                </table>
            </div>       
        </div>


            <div class="list_foot">
                <asp:Repeater ID="AuthoritySetting_Repeater" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table" class="mGrid" border="1">
                            <tr>
                                <th hidden="hidden">ID</th>
                                <th>用户名</th>
                                <th>界面名称</th>
                                <th>是否可用</th>
                                <th>创建者</th>
                                <th>更改者</th>
                                <th>创建时间</th>
                                <th>更新时间</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td hidden="hidden" ><%#Eval("ID") %></td>
                            <td><%#Eval("USER_NAME") %></td>
                            <td><%#Eval("PROGRAM_NAME") %></td>
                            <td><%#Eval("ENABLED") %></td>
                            <td><%#Eval("CREATE_BY") %></td>
                            <td><%#Eval("UPDATE_BY")%></td>
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
            <div class="toggerBody">
                <div>
                    <label>用户名:</label>
                    <select id="insert_user_id_Authority" class="select_text" name="insert_user_id_Authority">
                    </select>

                    <%-- <input id="insert_user_id_Authority" class="input_text" placeholder="请输入数字" type="text" runat="server" />--%>
                </div>
                <div>
                    <label>界面名称:</label>
                    <select id="insert_program_id_Authority" class="select_text" name="insert_program_id_Authority">
                    </select>

                    <%-- <input id="insert_program_id_Authority" class="input_text" placeholder="请输入数字" type="text" runat="server" />--%>
                </div>
                <div>
                    <label>是否可用:</label>
                    <select class="select_text" runat="server" id="insert_select_id_Authority">
                        <option value="Y">Y</option>
                        <option value="N">N</option>
                    </select>
                </div>



            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button3" CssClass="btn_close" runat="server" Text="确定" OnClick="Insert_Authority" />
                <asp:Button ID="Button2" CssClass="btn_close" runat="server" Text="取消" />
                <%--  <button class="btn_close" type="button"  runat="server" >确定</button>--%>
                <%--  <button class="btn_close" type="button">取消</button>--%>
            </div>
        </div>
    </div>


    <div id="div_togger2">
        <div class="toggerHeader">
            <label>更新</label>
        </div>
        <div class="toggerBody">
            <div>
                 <label>ID:</label>
                <input id="update_program_id" class="select_text" readonly="readonly"  type="text" runat="server" />
            </div>
           <div>
                <label>用户名:</label> 
                    <input id="update_user_id_authority" class="select_text" readonly="readonly"  type="text" runat="server" />        
                <%-- <select id="update_user_id_authority" class="select_text" name="update_user_id_authority">
                </select>--%>
            </div>
         
          <div>
                <label>界面名称:</label>
               <input id="update_program_id_Authority" class="select_text" readonly="readonly"  type="text" runat="server" />        
               <%-- <select id="update_program_id_Authority" class="select_text" name="update_program_id_Authority">
                </select>--%>
            </div>
            <div>
                <label>是否可用:</label>
                <select class="select_text" runat="server" id="update_select_id_Authority">
                    <option value="Y">Y</option>
                    <option value="N">N</option>
                </select>
            </div>
        </div>
        <div class="toggerFooter">
            <asp:Button ID="Button4" runat="server" CssClass="btn_close" Text="确定" OnClick="Update_Authority" />
            <asp:Button ID="Button5" runat="server" Text="取消" CssClass="btn_close" />
            <%--      <button id="Button1" class="btn_close" type="button" runat="server" >确定</button>--%>
            <%--    <button class="btn_close" type="button">取消</button>--%>
        </div>
    </div>



    <div id="div_togger3">
        <div class="toggerHeader">
            <label>是否确定删除此条数据?</label>
        </div>
        <div class="toggerBody">
            <div>               
                <input id="delete_user_id_authority"  hidden="hidden"  class="select_text" readonly="readonly"  type="text" runat="server" />
            </div>
            <div>
                 <label>用户名:</label>
                <input id="delete_user_name_authority" class="select_text" readonly="readonly"  type="text" runat="server" />
            </div>
            <div>
                 <label>界面名称:</label>
                <input id="delete_program_name_authority" class="select_text" readonly="readonly"  type="text" runat="server" />
            </div>
        </div>
        <div class="toggerFooter">
            <asp:Button ID="Button6" runat="server" Text="确定" OnClick="Delete_Authority" />
            <button class="btn_close" type="button">取消</button>
        </div>
    </div>

    <script src="JavaScript/AuthoritySetting.js"></script>
</asp:Content>

