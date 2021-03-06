﻿<%@ Page Title="warehouseSetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="warehouseSetting.aspx.cs" Inherits="WMS_v1._0.Web.warehouseSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/rackSetting.css" />
    <link rel="stylesheet" href="CSS/warehouseSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" id="Button1" type="button" runat="server" onserverclick="CleanAllMeassage_Click">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" id="btn_insert" type="button">
                    <%--<img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>库别名</label>
                        </td>
                        <td class="tdInput">
                            <select id="subinventory_name_Query" name="subinventory_name_Query">
                                <option value="">ALL</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>是否可用</label>
                        </td>
                        <td class="tdInput">
                            <select id="enabled" name="enabled_Query">
                                <option value="">ALL</option>
                                <option>Y</option>
                                <option>N</option>
                            </select>
                        </td>  
                        <td class="tdLab">
                            <label>描述:</label>
                        </td>
                      
                        <td class="tdInput">
                            
                            <textarea class="input_text" id="description_Query" placeholder="请在60字以内进行描述" maxlength="60" runat="server"></textarea>
                        </td>
                        <td class="tdLab">
                            <button runat="server" class="selectBtn" id="select" value="" onserverclick="QueryMeassage_Click">查询</button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="list_foot" id="list_foot">
            <asp:Repeater ID="subinventoryRepeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th style="display:none">库别Key</th>
                            <th>库别名</th>
                            <th>创建者</th>
                            <th>创建时间</th>
                            <th>更改者</th>
                            <th>更改时间</th>
                            <th>是否可用</th>
                            <th>描述</th>
                            <th>更新</th>
                            <th>删除</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="display:none"><%#Eval("SUBINVENTORY_KEY") %></td>
                        <td><%#Eval("SUBINVENTORY_NAME") %></td>
                        <td><%#Eval("CREATE_name") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_name") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                        <td><%#Eval("ENABLED") %></td>
                        <td><%#Eval("DESCRIPTION") %></td>
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
                <label>库别名:</label>
                 <input class="input_text" type="text" id="subinventory_name_Insert" name="subinventory_name_Insert" runat="server"/>
            </div>
            <div class="toggerBody">
                <label>是否可用:</label>
                <select class="select_text" id="enabled_Insert"  name="enabled_Insert">
                    <option>Y</option>
                    <option>N</option>
                </select>
            </div>
            <div class="toggerBody">
                <label>描述:</label>
                <textarea class="input_text" id="description_Insert" placeholder="请在60字以内进行描述" maxlength="60" runat="server"></textarea>
            </div>

            <div class="toggerFooter">
                <asp:Button CssClass="btn_close" ID="Button4" runat="server" OnClick="InsertMeassage_Click" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div>
                <div class="toggerBody" style="display:none">
                    <label >库别ID:</label>
                    <input class="input_text" type="text" readonly="readonly" id="subinventory_key_Update" name="subinventory_key_Update" runat="server"/>
                </div>
                <div class="toggerBody">
                    <label>库别名:</label>
                     <input class="input_text" type="text" readonly="readonly" id="subinventory_name_Update" name="subinventory_name_Update" runat="server"/>
                  <%--  <select id="subinventory_name_Update" name="subinventory_name_Update">
                    <option value="">ALL</option>
                    </select>--%>
                </div>
                <div class="toggerBody">
                    <label>是否可用:</label>
                    <select class="select_text" id="enabled_Update" name="enabled_Update">
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <div class="toggerBody">
                    <label>描述:</label>
                    <textarea class="input_text" id="description_Update" placeholder="请在60字以内进行描述" maxlength="60" name="description2" runat="server"></textarea>
                </div>
                <div class="toggerFooter">
                    <asp:Button CssClass="btn_close" ID="bnt_up" runat="server" OnClick="UpdateMeassage_Click" Text="确定" />
                    <button class="btn_close" type="button">取消</button>
                </div>
            </div>
        </div>
        <div id="div_togger3">
            <div class="toggerHeader">
                <label>确定删除么</label>
            </div>
            <div style="text-align: center">
                <div class="toggerBody">
                    <label>库别名:</label>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <input type="text" id="subinventory_key_Delete" name="subinventory_key_Delete"  hidden="hidden" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button CssClass="btn_close" ID="Button5" runat="server" OnClick="DeletMeassage_Click" Text="删除" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
    </div>
    <script src="JavaScript/jquery-2.2.4.min.js"></script>
    <script src="JavaScript/warehouseSetting.js"></script>
</asp:Content>
