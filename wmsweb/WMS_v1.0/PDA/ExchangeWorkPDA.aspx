<%@ Page Title="ExchangeWorkPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="ExchangeWorkPDA.aspx.cs" Inherits="WMS_v1._0.PDA.ExchangeWorkPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" id="Button1" type="button" runat="server" onserverclick="cleanMassage">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>调拨单号</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList Class="selectInput" ID="DropDownList_invoice_no" runat="server" AutoPostBack="true" />
                        </td>
                        <td class="tdLab">
                            <asp:Button class="selectBtu" ID="Button8" runat="server" OnClick="search" Text="查询" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="ExchangeHeaderReater" runat="server">
                <HeaderTemplate>
                    <table id="ExchangeHeader_Table" class="mGrid" border="1">
                        <tr>
                            <th>调拨单号</th>
                            <th>部门编号</th>
                            <th>部门名称</th> 
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("invoice_no") %></td>
                        <td><%#Eval("flex_value") %></td>
                        <td><%#Eval("description") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>

                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="ExchangeLineReater" runat="server">
                <HeaderTemplate>
                    <table id="ExchangeLine_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">调拨单身ID</th>
                            <th>料号</th>
                            <th>制程代号</th>
                            <th>调出库别</th>
                            <th>调出料架</th>
                            <th>调入库别</th>
                            <th>调入料架</th>
                            <th>需求量</th>
                            <th>调拨数量</th>
                            <th>创建时间</th>
                            <th>创建人</th>
                            <th>更新时间</th>
                            <th>更新人</th>
                            <th>扣账</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("exchange_line_id") %></td>
                        <td><%#Eval("ITEM_NAME") %></td> 
                        <td><%#Eval("operation_seq_num_name") %></td>
                        <td><%#Eval("out_subinventory_name") %></td>
                        <td><%#Eval("out_frame_key") %></td>
                        <td><%#Eval("in_subinventory_name") %></td>
                        <td><%#Eval("in_frame_key") %></td>
                        <td><%#Eval("required_qty") %></td>
                        <td><%#Eval("exchanged_qty") %></td>
                        <td><%#Eval("create_time") %></td>
                        <td><%#Eval("create_man") %></td>
                        <td><%#Eval("update_time") %></td>
                        <td><%#Eval("update_man") %></td>
                        <td>
                            <button id="btn_debit" class="btn_debit" runat="server" type="button">扣账</button></td>
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
                <label>是否确定进行扣账？</label>
            </div>
            <div class="toggerBody">
                <div hidden="hidden">
                    <label>料号</label>
                    <input class="input_text" type="text" id="item_name" readonly="readonly" runat="server" />
                </div>
            
                <div hidden="hidden">
                    <label>调入库别</label>
                    <input class="input_text" type="text" id="in_subinventory" readonly="readonly" runat="server" />
                </div>
              <%--   <div hidden="hidden">
                    <label>调入料架</label>
                    <input class="input_text" type="text" id="in_frame_key" readonly="readonly" runat="server" />
                </div>--%>
                <div hidden="hidden">
                    <label>调出库别</label>
                    <input class="input_text" type="text" id="out_subinventory" readonly="readonly" runat="server" />
                </div>
                <%-- <div hidden="hidden">
                    <label>调出料架</label>
                    <input class="input_text" type="text" id="out_frame_key" readonly="readonly" runat="server" />
                </div>--%>
                <div hidden="hidden">
                    <label>需求量</label>
                    <input class="input_text" type="text" id="exchanged_qty" readonly="readonly" runat="server" />
                </div>
              
                <div hidden="hidden">
                    <label>调拨单身ID:</label>
                    <input class="input_text" type="text" id="exchange_line_id_debit" readonly="readonly" runat="server" />
                </div>
                 <div hidden="hidden">
                    <label>更新人:</label>
                    <input class="input_text" type="text" id="update_man" readonly="readonly" runat="server" />
                </div>
               <%-- 这上面的都是要隐藏的属性--%>
                <div >
                    <label>调出料架</label>
                    <input class="input_text" type="text" id="out_frame_key"  runat="server" />
                </div>
                <div >
                    <label>调入料架</label>
                    <input class="input_text" type="text" id="in_frame_key"  runat="server" />
                </div>
                <div>
                    <label>datecode:</label>
                    <input class="input_text" type="text" id="datecode" runat="server" value=""/>
                    <%-- <asp:DropDownList ID="DropDownList_datecode" runat="server" />--%>
                </div>
                <div>
                    <label>调拨量:</label>
                    <input class="input_text" type="text" id="exchanged_qty_debit" runat="server" />
                </div>            
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button2" runat="server" OnClick="Debit_action" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/ExchangeWorkJS.js"></script>
</asp:Content>
