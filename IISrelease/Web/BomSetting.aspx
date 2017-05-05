<%@  Page Title="BomSetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="BomSetting.aspx.cs" Inherits="WMS_v1._0.Web.BomSetting" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/BomSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" runat="server" onserverclick="CleanAllMeassage_Click">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" type="button" runat="server" id="btn_insert">
                   <%-- <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>大料号</label></td>
                        <td class="tdInput">
                          <%--  <input type="text" id="wo_no_Query" runat="server"  placeholder="请输入字符"/>--%>
                            <input class="input_text" type="text" id="item_name1" runat="server" placeholder="请输入料号"/>
                        </td>
                        <td class="tdLab">
                            <label>料号</label></td>
                        <td class="tdInput">
                           <%-- <input type="text" id="item_name_Query" runat="server" placeholder="请输入字符"/>--%>
                            <input class="input_text" type="text" id="small_item_name3" runat="server" placeholder="请输入料号"/>
                        </td>
                         <td class="tdLab">
                            <label>version</label></td>
                        <td class="tdInput">
                          <%--  <input type="text" id="wo_no_Query" runat="server"  placeholder="请输入字符"/>--%>
                            <input class="input_text" type="text" id="version" runat="server" placeholder="请输入版本号"/>
                        </td>
                        <td class="tdLab">
                            <label>制程代号</label>
                        </td>
                        <td class="tdInput">
                          <%--  <input type="text" id="operation_seq_num_Query" runat="server"  placeholder="请输入数字" />--%>
                             <select id="operation_seq_num_Query" name="operation_seq_num_Query">
                               <option value="">ALL</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <asp:Button ID="queryButton" CssClass="selectBtn" runat="server" OnClick="QueryMeassage_Click" Text="查询"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
    <%--        <asp:GridView ID="bomGridView" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" EmptyDataText="Data Is Empty" AutoGenerateColumns="False" OnRowDeleting="DeletMassage_Click">
                <Columns>
                    <asp:BoundField DataField="WO_NO" HeaderText="工单编号" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />                
                    <asp:BoundField DataField="OPERATION_SEQ_NUM" HeaderText="制程" />
                    <asp:BoundField DataField="Required_qty" HeaderText="需要用量" />
                    <asp:BoundField DataField="CREATE_TIME" HeaderText="创建时间" />
                    <asp:BoundField DataField="UPDATE_TIME" HeaderText="更新时间" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <button class="btn_update" type="button">更新</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Delete" runat="server" CommandName="Delete" Text="删除" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
            <asp:Repeater ID="bomRepeater" runat="server" >
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">Bom表ID</th>
                            <th>大料号</th>
                            <th>料号</th>
                            <th>版本号</th>
                            <th>需要用量</th>
                            <th>制程</th>
                            <th>创建者</th>
                            <th>更改者</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("BOM_ID") %></td>
                        <td><%#Eval("BOM_ITEM_NAME") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("BOM_VERSION") %></td>
                        <td><%#Eval("OPERATION_QTY") %></td>
                        <td><%#Eval("OPERATION_SEQ_NUM") %></td>
                        <td><%#Eval("CREATE_MAN") %></td>
                        <td><%#Eval("UPDATE_BY") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
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
                    <label>大料号：</label>
                    <%--<input class="input_text" type="text" id="wo_no_Insert" runat="server" placeholder="请输入字符"/>--%>
                    <input class="input_text" type="text" id="item_name2" runat="server" placeholder="请输入料号"/>
                </div>
                <div>
                    <label>料号:</label>
                   <%-- <input class="input_text" type="text" id="item_name_Insert" runat="server" placeholder="请输入字符"/>--%>
                    <input class="input_text" type="text" id="small_item_name1" runat="server" placeholder="请输入料号"/>
                </div>
                <div>
                    <label>version：</label>
                    <%--<input class="input_text" type="text" id="wo_no_Insert" runat="server" placeholder="请输入字符"/>--%>
                    <input class="input_text" type="text" id="version2" runat="server" placeholder="请输入版本号"/>
                </div>
                <div>
                    <label>制程代号:</label>
                    <%--<input class="input_text" type="text" id="operation_seq_num_Insert" runat="server" placeholder="请输入数字"/>--%>
                      <select id="operation_seq_num_Insert" name="operation_seq_num_Insert">                              
                            </select>
                </div>
                <div>
                    <label>需要用量:</label>
                    <input class="input_text" type="text" id="required_qty_Insert" runat="server" placeholder="请输入数字"/>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button runat="server" OnClick="InsertMeassage_Click" Text="确定"/>
                 <asp:Button ID="Button1" runat="server" OnClick="CleanInsertMessage" Text="取消"/>
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                 <div style="display:none">
                    <label>Bom表ID:</label>
                    <input class="input_text" type="text" id="requirement_line_id_Update"  readonly="readonly" runat="server" placeholder="请输入字符"/>
                </div>
                <div>
                    <label>大料号:</label>
                    <input class="input_text" type="text" id="item_name3" runat="server" readonly="readonly"/>
                </div>
                <div>
                    <label>料号:</label>
                  <%--  <input class="input_text" type="text" id="item_name_Update" runat="server" placeholder="请输入字符"/>--%>
                    <input class="input_text" type="text" id="small_item_name2" runat="server" placeholder="请输入料号"/>
                </div>
                <div>
                    <label>version：</label>
                    <%--<input class="input_text" type="text" id="wo_no_Insert" runat="server" placeholder="请输入字符"/>--%>
                    <input class="input_text" type="text" id="version3" runat="server" placeholder="请输入版本号"/>
                </div>
                <div>
                    <label>制程代号:</label>
             <%--       <input class="input_text" type="text" id="operation_seq_num_Update" runat="server" placeholder="请输入数字"/>--%>
                    <select id="operation_seq_num_Update" name="operation_seq_num_Update">
                                </select>
                </div>
                <div>
                    <label>需要用量:</label>
                    <input class="input_text" type="text" id="required_qty_Update" runat="server" placeholder="请输入数字"/>
                </div>
            </div>
            <div class="toggerFooter"> 
                 <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateMeassage_Click" Text="确定"/>
                 <asp:Button ID="Button2" runat="server" OnClick="CleanUpdateMessage" Text="取消"/>
            </div>
        </div>


           <div id="div_togger3">
            <div class="toggerHeader">
                <label>是否确定删除该条Bom信息？</label>
            </div>
            <div class="toggerBody">
                <div style="display:none">
                    <label>Bom表ID:</label>
                    <input id="requirement_line_Delet" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
                <div>
                    <label>大料号：</label>
                   <%--<input class="input_text" type="text" id="wo_no_Delete" readonly="readonly" runat="server" name="wo_no_Delete" />--%>
                    <asp:Label ID="item_name_Delete1" runat="server" Text="Label" name="wo_no_Delete" ></asp:Label>
                </div>
                <div>
                    <label>料号:</label>
                 <%-- <input class="input_text" type="text" id="item_name_Delete" readonly="readonly" runat="server" name="item_name_Delete" />--%>
                    <asp:Label ID="item_name_Delete2" runat="server" Text="Label" name="item_name_Delete" ></asp:Label>
                </div>
                <div>
                    <label>版本:</label>
                 <%-- <input class="input_text" type="text" id="item_name_Delete" readonly="readonly" runat="server" name="item_name_Delete" />--%>
                    <asp:Label ID="version4" runat="server" Text="Label" name="item_name_Delete" ></asp:Label>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" Text="确定" OnClick="DeletMeassage_Click" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>


    </div>
    <script src="JavaScript/BomSetting.js"></script>
</asp:Content>
