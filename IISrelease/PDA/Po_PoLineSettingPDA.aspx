<%@ Page Title="Po_PoLineSettingPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="Po_PoLineSettingPDA.aspx.cs" Inherits="WMS_v1._0.Web.Po_PoLineSettingPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="CSS/Po_PoLinePDA.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                 <button id="btn_style" class="btn_style" type="button" runat="server" onserverclick="CleanAllMeassage_Click">
                    <%--   <img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                    <%--<img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>PO单号</label>
                        </td>
                        <td class="tdInput">
                            <input id="po_no" type="text" runat="server" placeholder="请输入字符" />

                        </td>

                        <td class="tdLab">
                            <label>厂商代码</label></td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="vendor_keys" runat="server">
                            </asp:DropDownList>
                        </td>

                        <td class="tdLab">
                            <asp:Button ID="bt_QueryPO_Click" CssClass="selectBtn" runat="server" Text="查询" OnClick="QueryPO_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <label>PO单头:</label>
            <asp:Repeater ID="PO_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Top_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">Header id</th>
                            <th>PO单号</th>
                            <th>厂商代码</th>
                            <th>创建者</th>
                            <th>更改者</th>
                            <th>创建时间</th>
                            <th>更改时间</th>
                            <th>修改</th>
                            <th>删除</th>
                            <th>查询PO单身</th>
                            <th>添加PO单身</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("PO_HEADER_ID") %></td>
                        <td><%#Eval("PO_NO") %></td>
                        <td><%#Eval("VENDOR_KEY") %></td>
                        <td><%#Eval("CREATE_BY") %></td>
                        <td><%#Eval("UPDATE_BY") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                        <td>
                            <button id="btn_update" class="btn_update" runat="server" type="button">修改</button></td>
                        <td>
                            <button id="btn_delete" class="btn_delete" runat="server" type="button">删除</button></td>
                        <td onclick="showDetial(this)">
                            <button id="Button11" class="btn_query_poLine" runat="server" type="button">查询PO单身</button>
                        </td>
                        <td>
                            <button id="btn_insert_poLine" class="btn_insert_poLine" runat="server" type="button">添加PO单身</button></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="list_foot">
            <label>PO单身:</label>
            <asp:Repeater ID="po_Line_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">po_header_id</th>
                            <th>PO单号</th>
                            <th>Line序号</th>
                            <th>料号</th>
                            <th>总量</th>
                            <th>是否可用</th>
                            <th>修改</th>
                            <th>删除</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr_hide">
                        <td hidden="hidden"><%#Eval("PO_HEADER_ID") %></td>
                        <td><%#Eval("PO_NO") %></td>
                        <td><%#Eval("LINE_NUM") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("REQUEST_QTY") %></td>
                        <td><%#Eval("CANCEL_FLAG") %></td>
                        <td hidden="hidden"><%#Eval("PO_LINE_ID") %></td>
                        <td>
                            <button id="btn_update_poline" class="btn_update_poline" runat="server" type="button">修改</button></td>
                        <td>
                            <button id="btn_delete__poline" class="btn_delete__poline" runat="server" type="button">删除</button></td>
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
                    <label>PO单号:</label>
                    <input id="po_no_Insert" class="input_text" type="text" runat="server" placeholder="请输入字符" />
                </div>
                <div>
                    <label>厂商代码:</label>
                    <select id="vendor_key_Insert" class="select_text" name="vendor_key_Insert">
                    </select>
                    <%--<asp:dropdownlist id="vendor_key_Insert" runat="server" AutoPostBack="True" />--%>
                </div>

                <%--                <div>
                    <label>创建者:</label>
                    <input id="create_by_Insert" class="input_text" type="text" runat="server" />
                </div>--%>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" Text="确定" OnClick="InsertPO_Click" />
                <asp:Button ID="Button2" runat="server" Text="取消" OnClick="CleanPO_Insert_Click" />
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>PO单号:</label>
                    <input id="po_no_Update" readonly="readonly" class="input_text" type="text" runat="server" placeholder="请输入字符" />
                </div>
                <div>
                    <label>厂商代码:</label>
                    <%--<asp:dropdownlist id="vendor_key_Update" runat="server" AutoPostBack="True" />--%>
                    <select id="vendor_key_Update" class="select_text" name="vendor_key_Update">
                    </select>
                </div>
                <%--                <div>
                    <label>更改者:</label>
                    <input id="Update_by_Update" class="input_text" runat="server" type="text" />
                </div>--%>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button5" runat="server" Text="确定" OnClick="UpdatePO_Click" />
                <asp:Button ID="Button3" runat="server" Text="取消" />
            </div>
        </div>

        <div id="div_togger3">
            <div class="toggerHeader">
                <label>是否确定删除该PO单头信息，单身信息会一并删除？</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>PO单号:</label>
                    <input id="po_no_delet" class="input_text" runat="server" readonly="readonly" type="text" placeholder="请输入字符" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" Text="确定" OnClick="DeletPO_Click" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>


        <div id="div_togger4">
            <div class="toggerHeader">
                <label>添加PO单身</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label hidden="hidden">Header id:</label>
                    <input id="po_header_id_insertPoLine" class="input_text" type="text" runat="server" hidden="hidden" />
                </div>
                <div>
                    <label>PO单号:</label>
                    <input id="PO_NO1" class="input_text" runat="server" type="text" placeholder="请输入字符" readonly="readonly" />
                </div>
                <%--                <div>
                    <label>Line序号:</label>
                    <input id="line_num_insertPoLine" class="input_text" type="text" runat="server" placeholder="请输入数字" />
                </div>--%>
                <div>
                    <label>料号:</label>

                    <select id="Item" class="select_text" name="Item">
                    </select>
                    <%-- <input id="Item" class="input_text" type="text" runat="server"   placeholder="请输入字符串" />--%>
                </div>

                <div>
                    <label>总量:</label>
                    <input id="request_qty_insertPoLine" class="input_text" type="text" runat="server" placeholder="请输入数字" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select id="cancel_flag_insertPoLine" class="select_text" runat="server">
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button7" runat="server" Text="确定" OnClick="InsertPO_Line_Click" />
                <asp:Button ID="Button10" runat="server" Text="取消" OnClick="CleanPO_Line_Insert_Click" />
            </div>
        </div>


        <div id="div_togger5">
            <div class="toggerHeader">
                <label>PO_Line更新</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label hidden="hidden">Header id:</label>
                    <input id="po_header_id_Update_poLine" readonly="readonly" class="input_text" type="text" runat="server" hidden="hidden" />
                </div>
                <div>
                    <label>PO单号:</label>
                    <input id="PO_NO2" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
                <div>
                    <label>Line序号:</label>
                    <input id="line_num_Update_poLine" class="input_text" type="text" runat="server" readonly="readonly" />
                </div>
                <div>
                    <label>料号:</label>
                    <%--<input id="item_id_Update_poLine" class="input_text" runat="server" type="text"  placeholder="请输入数字" />--%>
                    <select id="Item1" class="select_text" name="Item1"></select>
                </div>
                <div>
                    <label>总量:</label>
                    <input id="request_qty_Update_poLine" name="request_qty_Update_poLine" class="input_text" runat="server" type="text" placeholder="请输入数字" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select id="cancel_flag_Update_poLine" class="select_text" runat="server">
                        <option selected="selected">Y</option>
                        <option>N</option>
                    </select>
                </div>
            </div>
            <div class="toggerFooter" onclick="showDetial(this)">
                <asp:Button ID="Button9" runat="server" Text="确定" OnClick="UpdatePO_Line_Click" />
                <asp:Button ID="Button8" runat="server" Text="取消" OnClick="CleanPO_Line_Update_Click" />
            </div>
        </div>


        <div id="div_togger6">
            <div class="toggerHeader">
                <label>是否确定删除该信息？</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>PO单号:</label>
                    <input id="po_no_Delet_poLine" class="input_text" runat="server" readonly="readonly" type="text" />
                    <label>Line序号:</label>
                    <input id="po_line_id_delet" class="input_text" runat="server" readonly="readonly" type="text" hidden="hidden" />
                    <input id="line_num_delet" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button12" runat="server" Text="确定" OnClick="DeletPO_Line_Click" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>




        <%-- <div id="div_togger7" hidden="hidden">
            <div class="toggerHeader">
                <label>查询PO单身信息</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>Header ID:</label>
                    <input id="po_header_id_Query" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                
                <asp:Button ID="Button12" runat="server" Text="确定" OnClick="QueryPO_Line_Click" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>--%>
    </div>
    <script src="JavaScript/Po_PoLine.js"></script>
</asp:Content>



