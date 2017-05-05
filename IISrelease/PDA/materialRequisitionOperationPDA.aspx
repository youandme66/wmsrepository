<%@ Page Title="materialRequisitionOperationPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="materialRequisitionOperationPDA.aspx.cs" Inherits="WMS_v1._0.PDA.materialRequisitionOperationPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/materialRequisitionOperationPDA.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" id="Button1" type="button" runat="server" >
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
                             <select style="width:100%" id="issue_no" name="issue_no">
                              <option value="">选择单号</option>
                                </select>
                              </td>
                        <td class="tdLab">
                            <asp:Button  class="selectBtu"  ID="Button8" runat="server" OnClick="search_issue" Text="确定" />
                        </td>
                          <td class="tdLab">
                            <asp:Button  class="selectBtu"  ID="Button3" runat="server" OnClick="export" Text="导出" />
                        </td>
                        </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="issue" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th>领料ID</th>
                            <th>领料单号</th>
                            <th>料号</th>
                             <th>领料类型</th>
                            <th>是否完成领料</th>
                             <th>模拟量</th>
                             <th>需求量</th>
                             <th>领料量</th>
                            <th>区域</th>
                            <th>料架号</th>
                            <th>库别</th>
                            <th>制程</th>
                            <th>创建时间</th>
                            <th>修改</th>
                            <th>删除</th>
                            <th>扣账</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ISSUE_LINE_ID") %></td>
                        <td><%#Eval("INVOICE_NO") %></td>
                        <td><%#Eval("ITEM_NAME") %></td>
                        <td><%#Eval("ISSUE_TYPE") %></td>
                         <td><%#Eval("STATUS") %></td>
                        <td><%#Eval("SIMULATED_QTY") %></td>
                        <td><%#Eval("REQUIRED_QTY") %></td>
                        <td><%#Eval("ISSUED_QTY") %></td>
                        <td><%#Eval("LOCATOR") %></td>
                        <td><%#Eval("FRAME_KEY") %></td>
                        <td><%#Eval("ISSUED_SUB_KEY") %></td>
                        <td><%#Eval("OPERATION_SEQ_NUM") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td>
                            <button id="btn_update" class="btn_update" runat="server" type="button">修改</button></td>
                        <td>
                            <button id="btn_delete" class="btn_delete" runat="server" type="button">删除</button></td>
                        <td>
                            <button id="btn_debit"  class="btn_debit"  runat="server" type="button">扣账</button></td>
                        <td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    
    <div id="zindex">
        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>领料ID:</label>
                    <input class="input_text" id="issue_line_id_update" type="text" readonly="readonly" runat="server" />
                </div>
                <div>
                    <label>料号:</label>
                    <input class="input_text" type="text" id="item_name_update" runat="server" />
                </div>
                <div>
                    <label>领料量:</label>
                    <input class="input_text" type="text" id="issued_qty_update" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" OnClick="update_issued_qty" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger3">
            <div class="toggerHeader">
                <label>是否确定删除该领料信息？</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>领料ID:</label>
                    <input id="issue_line_id_Delet" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
            </div>

            <div class="toggerFooter">
                <asp:Button ID="Button7" runat="server" OnClick="delete_issued_qty" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger1">
            <div class="toggerHeader">
                <label>是否确定进行扣账？</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>料号:</label>
                    <input class="input_text" type="text" id="item_name_debit" readonly="readonly" runat="server" />
                </div>
                <div>
                    <label>领料量:</label>
                    <input class="input_text" type="text" id="issued_qty_debit" readonly="readonly" runat="server" />
                </div>
                 <div>
                    <label>料架号:</label>
                    <input class="input_text" id="frame_key_debit" type="text" readonly="readonly" runat="server" />
                </div>
                <div>
                    <label>库别:</label>
                    <input class="input_text" id="issued_sub_key_debit" type="text" readonly="readonly" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button2" runat="server" OnClick="Debit_action" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
    </div>

    <script src="JavaScript/materiaRequisitionOperation.js"></script>
</asp:Content>