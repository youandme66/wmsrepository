<%@ Page Title="DpartmentSettingPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="DpartmentSettingPDA.aspx.cs" Inherits="WMS_v1._0.PDA.DpartmentSettingPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <label>部门编号</label></td>
                        <td class="tdInput">
                            <input id="flex_value_select" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>部门名称</label></td>
                        <td class="tdInput">
                           <select class="selectInput"  runat="server" id="description_select">
                           <option value=""></option> 
                           </select>
                         <%--   <input type="text" id="department_name_select" runat="server" />--%>
                        </td>
                         <td class="tdLab">
                            <label>是否可用</label>
                        </td>
                        <td class="tdInput">
                            <select class="selectInput"  runat="server" id="enabled_select">  
                                <option value=""></option>                          
                                <option value="Y">Y</option>
                                <option value="N">N</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <asp:Button ID="SelectPn" CssClass="selectBtn" runat="server" Text="查询" OnClick="Select_Click_Department" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>


        <div class="list_foot">
            <asp:Repeater ID="Department_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">部门id</th>
                            <th >部门编号</th>
                            <th>部门名称</th>
                            <th>是否可用</th>                           
                            <th>创建者</th>
                            <th>更改者</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("department_id") %></td>
                        <td><%#Eval("flex_value") %></td>
                        <td><%#Eval("description")%></td>
                        <td><%#Eval("ENABLED") %></td>                        
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
            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button3">
                <div class="toggerBody">   
                    <div>
                        <label>部门编号:</label>
                        <input id="flex_value_insert" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>            
                    <div>
                        <label>部门名称:</label>
                        <input id="description_insert" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>
                    <div>
                        <label>是否可用:</label>
                         <select runat="server" id="enabled_insert">                              
                                <option value="Y">Y</option>
                                <option value="N">N</option>
                            </select>
                       <%-- <input id="enabled_insert_id" class="input_text" type="text" placeholder="请输入字符" runat="server" />--%>
                    </div>                  
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button3" CssClass="btn_close" runat="server" Text="确定" OnClick="Insert_Click_Department" />
                    <asp:Button ID="Button2" CssClass="btn_close" runat="server" Text="取消"/>                 
                </div>
            </asp:Panel>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <asp:Panel ID="Panel2" runat="server" DefaultButton="Button4">
                <div class="toggerBody">
                    <div hidden="hidden">
                        <label >部门ID:</label>
                        <input id="department_id_update" class="input_text" readonly="readonly" placeholder="请输入数字" type="text" runat="server" />
                    </div>
                    <div>
                        <label>部门编号:</label>
                     <%--   <select  id="department_name_update" name="department_name_update" class="input_text">                                     
                        </select>--%>
                        <input id="flex_value_update" class="input_text" type="text" readonly="readonly" placeholder="请输入字符" runat="server" />
                    </div>
                    <div>
                        <label>部门名称:</label>                 
                        <input id="description_update" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>
                     <div>  
                           <label  hidden="hidden" >部门名称:</label>                                     
                        <input id="description_update_old"     hidden="hidden"   readonly="readonly" class="input_text" type="text" placeholder="请输入字符" runat="server" />
                    </div>
                 

                    <div>
                        <label>是否可用:</label>
                         <select runat="server" id="enabled_update_id">               
                                <option value="Y">Y</option>
                                <option value="N">N</option>
                            </select>
                   <%--     <input type="text" id="enabled_update_id" class="input_text" placeholder="请输入字符" runat="server" />--%>
                    </div>                  
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button4" runat="server" CssClass="btn_close" Text="确定" OnClick="Update_Click_Department" />
                    <asp:Button ID="Button5" runat="server" Text="取消" CssClass="btn_close" />    
                </div>
            </asp:Panel>
        </div>

        <div id="div_togger3">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="Button6">
                <div class="toggerHeader">
                    <label>是否确定删除该部门？</label>
                </div>
                <div class="toggerBody">
                    <div>                
                        <input id="department_id_delete"  hidden="hidden" class="input_text" runat="server" readonly="readonly" type="text" />
                    </div>
                    <div>
                         <label>部门名称:</label>
                           <asp:Label ID="department_name_delete" runat="server" Text="Label"></asp:Label>
                      <%--   <input id="department_name_delete" class="input_text" runat="server" readonly="readonly" type="text" />--%>
                    </div>

                </div>
                <div class="toggerFooter">
                    <asp:Button ID="Button6" runat="server" Text="确定" OnClick="Delect_Click_Department" />
                    <button class="btn_close" type="button">取消</button>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script src="JavaScript/DepartmentSetting.js"></script>
</asp:Content>
