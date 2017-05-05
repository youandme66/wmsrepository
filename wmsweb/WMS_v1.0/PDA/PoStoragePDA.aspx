<%@ Page Title="PoStoragePDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="PoStoragePDA.aspx.cs" Inherits="WMS_v1._0.PDA.PoStoragePDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="CSS/PoStorage.css" rel="stylesheet" />
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button3" class="btn_style" type="button" runat="server" onserverclick="CleanAllMeassage_Click">
                    <%--   <img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />
            </div>

            <%-- 打印按钮的弹框--%>
            <div id="zindex">
                <div id="div_togger1">
                    <div class="toggerHeader">
                        <label>打印领料单</label>
                    </div>
                    <div class="toggerBody">
                        <div>
                            <label>暂收单号</label>
                            <input class="input_text" type="text" id="receive_no_print" runat="server" placeholder="请输入领料单号" />
                            <label>入库量</label>
                            <input class="input_text" type="text" id="rcv_qty_print" runat="server" placeholder="请输入领料单号" />
                        </div>
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Button4" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>



            <div class="cb">
                <div class="list_item">
                    <table class="tab">
                        <tr>
                            <td class="tdLab">
                                <label>料号</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="ITEM_name2" placeholder="请输入字符" />
                                <%--<select id="ITEM_name2" name="ITEM_name2">
                              <option value="">ALL</option>
                                </select>--%>
                            </td>
                            <td class="tdLab">
                                <label>暂收单号</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="Rec_Num2" placeholder="请输入字符" />
                            </td>
                            <td class="tdLab">
                                <label>PO</label>
                            </td>
                            <td class="tdInput">
                                <input type="text" runat="server" id="PO_num2" placeholder="请输入字符" />
                            </td>
                            <td class="tdLab">
                                <input id="Button2" class="selectBtn" type="button" value="查询" runat="server" onserverclick="search_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--<asp:GridView
                ID="GridView2"
                runat="server"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                AllowPaging="true"
                PageSize="6"
                AutoGenerateColumns="False"
                EmptyDataText="Data Is Empty" OnPageIndexChanging="GridView2_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="po_no" HeaderText="PO单号" />
                    <asp:BoundField DataField="po_line_id" HeaderText="Line Num" />
                    <asp:BoundField DataField="item_name" HeaderText="料号" />
                    <asp:BoundField DataField="receipt_no" HeaderText="暂收单号" />
                    <asp:BoundField DataField="dateCode" HeaderText="DateCode" />
                    <asp:BoundField DataField="rcv_qty" HeaderText="暂收量" />
                    <asp:BoundField DataField="deliver_qty" HeaderText="入库量" />
                </Columns>
            </asp:GridView>--%>
            <asp:Repeater ID="PoStorage_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th>PO单号</th>
                            <th>Line Num</th>
                            <th>料号</th>
                            <th>是否完成入库</th>
                            <th>暂收单号</th>
                            <th>DateCode</th>
                            <th>暂收量</th>
                            <th>允收量</th>
                            <th>退回量</th>
                            <th>已入库量</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("PO_NO") %></td>
                        <td><%#Eval("PO_LINE_ID") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("STATUS") %></td>
                        <td><%#Eval("RECEIPT_NO") %></td>
                        <td><%#Eval("DATECODE")%></td>
                        <td><%#Eval("RCV_QTY")%></td>
                        <td><%#Eval("ACCEPTED_QTY")%></td>
                        <td><%#Eval("RETURN_QTY")%></td>
                        <td><%#Eval("DELIVER_QTY")%></td>
                        <td>
                            <button id="btn_choose" class="btn_choose" runat="server" type="button">选择</button></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>



        <div class="list_foot">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>料号</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="ITEM_name" placeholder="请输入字符" readonly="readonly" />
                        </td>
                        <td class="tdLab">
                            <label>暂收单号</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="Rec_Num" placeholder="请输入字符" readonly="readonly" />
                        </td>
                        <td class="tdLab">
                            <label>入库量</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="Rec_qty" placeholder="请输入数字" />
                        </td>
                        <td class="tdLab" style="display: none">
                            <input type="text" runat="server" id="Accepted_qty" />
                            <input type="text" runat="server" id="Deliver_qty" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>DATECODE</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="datecode" placeholder="请输入字符" readonly="readonly" />
                        </td>

                        <td class="tdLab">
                            <label>料架</label>
                        </td>
                        <td class="tdInput">
                            <%--                             <select id="frame_key" name="frame_key">
                              <option value="">选择料架</option>
                                </select>--%>
                            <asp:DropDownList class="selectInput" ID="frame_name" runat="server" AutoPostBack="True" />
                        </td>

                        <td class="tdLab">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="subinventory_name" readonly="readonly" />
                            <%--                        <select id="issued_sub_key" name="issued_sub_key">
                              <option value="">选择库别</option>
                                </select>--%>
                        </td>

                        <td class="tdLab">
                            <input id="Button1" class="selectBtn" type="button" value="入库" runat="server" onserverclick="PoStorage_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>


        <script src="JavaScript/PoStorage.js"></script>
    </div>
    <script src="JavaScript/PrintJS.js"></script>
</asp:Content>
