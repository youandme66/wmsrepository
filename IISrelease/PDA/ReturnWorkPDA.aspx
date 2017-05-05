<%@ Page Title="ReturnWorkPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="ReturnWorkPDA.aspx.cs" Inherits="WMS_v1._0.PDA.ReturnWorkPDA" %>

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
                            <label>退料单号</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_invoice_no" runat="server" AutoPostBack="true" />
                        </td>
                        <td class="tdLab">
                            <asp:Button class="selectBtu" ID="Button8" runat="server" OnClick="search" Text="查询" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="ReturnHeaderReater" runat="server">
                <HeaderTemplate>
                    <table id="ReturnHeader_Table" class="mGrid" border="1">
                        <tr>
                            <th>退料单号</th>
                            <th>退料类型</th>
                            <th>部门编号</th>
                            <th>部门名称</th>
                            <th>退料是否完成</th>
                            <th>备注</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("invoice_no") %></td>
                        <td><%#Eval("return_type") %></td>
                        <td><%#Eval("flex_value") %></td>
                        <td><%#Eval("description") %></td>
                        <td><%#Eval("status") %></td>
                        <td><%#Eval("remark") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>

                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="ReturnLineReater" runat="server">
                <HeaderTemplate>
                    <table id="ReturnLine_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">退料单身ID</th>
                            <th>Line_Num</th>
                            <th>退料工单号</th>
                            <th>制程</th>
                            <th>料号</th>
                            <th>库别</th>
                            <th>料架</th>
                            <th>退料数量</th>
                            <th>扣账状态</th>
                            <th>创建时间</th>
                            <th>创建人</th>
                            <th>更新时间</th>
                            <th>更新人</th>
                            <th>扣账</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("return_line_id") %></td>
                        <td><%#Eval("line_num") %></td>
                        <td><%#Eval("return_wo_no") %></td>
                        <td><%#Eval("operation_seq_num_name") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("return_sub_name") %></td>
                        <td><%#Eval("frame_key") %></td>
                        <td><%#Eval("return_qty") %></td>
                        <td><%#Eval("flag") %></td>
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
                    <label>退料工单号</label>
                    <input class="input_text" type="text" id="return_wo_no" readonly="readonly" runat="server" />
                </div>
                <div hidden="hidden">
                    <label>退料库别</label>
                    <input class="input_text" type="text" id="return_sub_name" readonly="readonly" runat="server" />
                </div>
                <%-- <div hidden="hidden">
                    <label>退料料架</label>
                    <input class="input_text" type="text" id="frame_key" readonly="readonly" runat="server" />
                </div>--%>
                <div hidden="hidden">
                    <label>退料量</label>
                    <input class="input_text" type="text" id="return_qty" readonly="readonly" runat="server" />
                </div>
                <div hidden="hidden">
                    <label>退料状态</label>
                    <input class="input_text" type="text" id="flag_debit" readonly="readonly" runat="server" />
                </div>
                <div hidden="hidden">
                    <label>退料单身ID:</label>
                    <input class="input_text" type="text" id="return_line_id_debit" readonly="readonly" runat="server" />
                </div>
                <%-- 这上面的都是要隐藏的属性--%>
                <div>
                    <label>退料料架</label>
                    <input class="input_text" type="text" id="frame_key" runat="server" />
                </div>
                <div>
                    <label>datecode:</label>
                    <input class="input_text" type="text" id="datecode" runat="server" value="" />
                    <%-- <asp:DropDownList ID="DropDownList_datecode" runat="server" />--%>
                </div>
                <div>
                    <label>退回量:</label>
                    <input class="input_text" type="text" id="return_qty_debit" runat="server" />
                </div>
                <%--  <div>
                    <label>料架:</label>
                    <asp:DropDownList ID="DropDownList_frame" runat="server" />
                </div>--%>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button2" runat="server" OnClick="Debit_action" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/ReturnWorkJS.js"></script>
</asp:Content>
