<%@ Page Title="" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="IssueWorkPDA.aspx.cs" Inherits="WMS_v1._0.PDA.IssueWorkPDA" %>

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
                            <label>领料单号</label>
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
            <asp:Repeater ID="IssueHeaderReater" runat="server">
                <HeaderTemplate>
                    <table id="IssueHeader_Table" class="mGrid" border="1">
                        <tr>
                            <th>领料单号</th>
                            <th>领料类型</th>
                            <th>部门编号</th>
                            <th>部门名称</th>
                            <th>领料是否完成</th>
                            <th>备注</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("invoice_no") %></td>
                        <td><%#Eval("issue_type") %></td>
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
            <asp:Repeater ID="IssueLineReater" runat="server">
                <HeaderTemplate>
                    <table id="IssueLine_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">领料单身ID</th>
                            <th>Line_Num</th>
                            <th>领料工单号</th>
                            <th>制程</th>
                            <th>料号</th>
                            <th>库别</th>
                            <th>料架</th>
                            <th>需求量</th>
                            <th>发料量</th>
                            <th>领料量</th>
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
                        <td hidden="hidden"><%#Eval("issue_line_id") %></td>
                        <td><%#Eval("line_num") %></td>
                        <td><%#Eval("wo_no") %></td>
                        <td><%#Eval("operation_seq_num_name") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("issued_sub") %></td>
                        <td><%#Eval("FRAME_KEY") %></td>
                        <td><%#Eval("REQUIRED_QTY") %></td>
                        <td><%#Eval("SIMULATED_QTY") %></td>
                        <td><%#Eval("ISSUED_QTY") %></td>
                        <td><%#Eval("status") %></td>
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
                    <label>领料工单号</label>
                    <input class="input_text" type="text" id="wo_no" readonly="readonly" runat="server" />
                </div>
                <div hidden="hidden">
                    <label>领料库别</label>
                    <input class="input_text" type="text" id="issued_sub" readonly="readonly" runat="server" />
                </div>
                <%--  <div hidden="hidden">
                    <label>领料料架</label>
                    <input class="input_text" type="text" id="frame" readonly="readonly" runat="server" />
                </div>--%>
                <div hidden="hidden">
                    <label>领料量</label>
                    <input class="input_text" type="text" id="issued_qty" readonly="readonly" runat="server" />
                </div>
                <div hidden="hidden">
                    <label>领料状态</label>
                    <input class="input_text" type="text" id="flag_debit" readonly="readonly" runat="server" />
                </div>
                <div hidden="hidden">
                    <label>领料单身ID:</label>
                    <input class="input_text" type="text" id="issue_line_id_debit" readonly="readonly" runat="server" />
                </div>
                <%-- 这上面的都是要隐藏的属性--%>
                <div>
                    <label>料架</label>
                    <input class="input_text" type="text" id="frame" runat="server" />
                </div>
                <div>
                    <label>datecode:</label>
                    <input class="input_text" type="text" id="datecode" runat="server" value="" />
                    <%-- <asp:DropDownList ID="DropDownList_datecode" runat="server" />--%>
                </div>
                <div>
                    <label>领料量:</label>
                    <input class="input_text" type="text" id="issued_qty_debit" runat="server" />
                </div>

            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button2" runat="server" OnClick="Debit_action" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/IssueWork.js"></script>
</asp:Content>
