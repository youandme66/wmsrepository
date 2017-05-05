<%@ Page Title="customerSetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="customerSetting.aspx.cs" Inherits="customerSetting.customerSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
     
    </style>
   <link href="CSS/customerSetting.css" rel="stylesheet" />
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" id="btn_clear" runat="server" onserverclick="clear">
                <%--    <img src="icon/clear.png" /><br />--%>
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
                            <input id="user_name1" type="text" runat="server" />

                        </td>
                        <td class="tdLab">
                            <label>是否可用</label>

                        </td>
                        <td class="tdInput">
                            <select id="Enabled1" runat="server">
                                <option></option>
                                <option>Y</option>
                                <option>N</option>
                            </select>

                        </td>
                        <td class="tdLab">
                            <label>部门</label>
                        </td>
                        <td class="tdInput">
                            <select id="Select1"   name="Select1">
                                 <option></option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>描述</label>
                        </td>
                        <td class="tdInput">
                            <textarea runat="server" id="Textarea1" style="width:100%"></textarea>
                        </td>
                        <td class="tdLab">
                            <asp:Button ID="Button1" class="selectBtn" runat="server" Text="查询" OnClick="selectAll" />
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
                            <th style="display:none">用户ID</th>
                            <th>用户名</th>
                            <th>描述</th>
                            <th>是否可用</th>
                            <th>创建时间</th>
                            <th>更改时间</th>
                            <th>创建者</th>
                            <th>更改者</th>
                            <th>部门</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="display:none"><%#Eval("USER_ID ") %></td>
                        <td><%#Eval("USER_NAME") %></td>
                        <td><%#Eval("DESCRIPTION") %></td>
                        <td><%#Eval("ENABLED") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_TIME ") %></td>
                        <td><%#Eval("CREATE_BY") %></td>
                        <td><%#Eval("UPDATE_BY") %></td>
                        <td><%#Eval("Dept_no ") %></td>

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
                    <label>用户名:</label>
                    <input id="Frame_name" class="input_text" type="text" runat="server" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select id="Enabled" class="select_text" runat="server">
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <div>
                    <label>部门:</label>
                    <select id="Department" class="select_text"  name="Department">
                    </select>
                </div>
                <div>
                    <label>描述:</label>
                    <textarea id="Description" col="150px" class="input_text" runat="server"></textarea>
                </div>

            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" Text="确定" OnClick="notarizeAdd" />
                <asp:Button ID="btn_close" runat="server" Text="取消" />
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div class="update_lab"  style="display:none" >
                    <label>用户ID:</label>
                    <input id="Label2" readonly="readonly" class="input-text" runat="server" type="text" /><br />
                </div>
                <div>
                    <label>用户名:</label>
                    <input id="Frame_name2" class="input_text" runat="server" type="text" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select id="Enabled2" class="select_text" runat="server">
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <div>
                    <label>部门:</label>
                    <select  id="Department2"  class="select_text"  name="Department2">
                    </select>
                </div>
                <div>
                    <label>描述:</label>
                    <textarea id="Description2" class="input_text" runat="server"></textarea>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button5" runat="server" Text="确定" OnClick="notarizeUpdate" />
                <asp:Button ID="btm_close" runat="server" Text="取消" />
            </div>
        </div>

        <div id="div_togger3">
            <div class="toggerHeader">
                <label>删除</label>
            </div>
            <div class="toggerBody">
                <div class="toggerBody_lab">
                    <label>用户名:</label>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <input type="text" id="lab" readonly="readonly" runat="server" style="display:none"/>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" Text="确定" OnClick="notarizeDelete" />
                <asp:Button ID="bt_close" runat="server" Text="取消" />
            </div>
        </div>
    </div>
    <script src="JavaScript/customerSetting.js"></script>
    <%--  <script type="text/javascript">
        function load() {
            alert('正在执行');
            var line_table = document.getElementById('Line_Table');
            var tr1 = line_table.getElementsByTagName('tr');
            var td1;
            var tr2 = line_table.getElementsByTagName('tr');
            var td2;
            for (i = 0; i < btn_update.length; i++) {
               td1=tr1[i].getElementsByTagName('td');
                for (j = 0; j < btn_update.length; j++) {
                   td2 = tr2[j].getElementsByTagName('td');
                    if (td2[0].innerHTML == td1[6].innerHTML) {
                        td1[6].innerHTML = td2[1].innerHTML;
                        continue;
                    }
                }

            }
        }
    </script>--%>
</asp:Content>
