<%@ Page Title="" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="SingleQuery.aspx.cs" Inherits="WMS_v1._0.Web.SingleQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="CSS/returnOrder.css" />
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>退料工单号</label>
                        </td>
                        <td class="tdInput">
                            <input id="singlequery_return_wo_id" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>单位</label>
                        </td>
                        <td class="tdInput">
                            <input id="singlequery_uom_id" type="text" runat="server" />
                            <%--  <textarea id="singlequery_uom_id" runat="server"  style="resize:none;"></textarea>--%>
                        </td>
                        <td class="tdLab">
                            <label>制程:</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="seq_operation_num1" runat="server" Height="22px" Width="100%" AutoPostBack="false"></asp:DropDownList>
                        </td>
                        <td class="tdInput">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="Subinventory1" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="Subinventory1_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td class="tdLab">
                            <label>区域</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList2" runat="server" Height="25px" AutoPostBack="false"></asp:DropDownList>
                        </td>
                        <td class="tdInput">
                            <asp:Button  style="margin-left:86px" ID="singlequery_id" runat="server" Text="查询单身" OnClick="select_singlequery" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table2" class="mGrid" border="1">
                            <tr>
                                <th>退料单据号</th>
                                <th>Line_Num</th>
                                <th>退料工单号</th>
                                <th>制程</th>
                                <th>料号</th>
                                <th>退料数量</th>
                                <th>单位</th>
                                <th>库别</th>
                                <th>区域</th>
                                <th>退料时间</th>
                                <th>修改时间</th>
                                <th hidden="hidden">退料单身</th>
                                <th>更新</th>
                                <th>删除</th>
                                <th>退料</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("invoice_no") %></td>
                            <td><%#Eval("LINE_NUM") %></td>
                            <td><%#Eval("RETURN_WO_NO") %></td>
                            <td><%#Eval("OPERATION_SEQ_NUM_name") %></td>
                            <td><%#Eval("ITEM_NAME") %></td>
                            <td><%#Eval("RETURN_QTY") %></td>
                            <td><%#Eval("UOM") %></td>
                            <td><%#Eval("RETURN_SUB_name") %></td>
                            <td><%#Eval("LOCATOR") %></td>
                            <td><%#Eval("RETURN_TIME") %></td>
                            <td><%#Eval("UPDATE_TIME") %></td>
                            <td hidden="hidden"><%#Eval("return_line_id") %></td>
                            <td>
                                <button id="update2" class="btn_update2" type="button">更新</button></td>

                            <td>
                                <button id="delete2" class="btn_delete2" type="button">删除</button></td>
                            <td>
                                <button id="operation2" class="btn_operation2" type="button">退料</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </div>

        </div>
    </div>


    <div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div hidden="hidden">
                    <label>return_line_id</label>
                    <input id="return_line_id2" readonly="readonly" class="input_text" runat="server" type="text" />
                </div>
                <div>
                    <label>退料工单号:</label>
                    <input id="return_wo_no2" readonly="readonly" class="input_text" runat="server" type="text" />
                </div>
                <div>
                    <label>制程:</label>
                    <asp:DropDownList ID="seq_operation_num2" runat="server" Height="22px" Width="136px" AutoPostBack="false"></asp:DropDownList>
                </div>
                <div>
                    <label>料号:</label>
                    <input id="item_name2" class="input_text" runat="server" type="text" />
                </div>
                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <label>库别:</label>
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div>
                            <label>区域:</label>
                            <asp:DropDownList ID="DropDownList4" runat="server" Width="50%">
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <label>退料数量:</label>
                    <input id="quit_num2" class="input_text" runat="server" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button8" runat="server" Text="确定" OnClick="update2" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>


        <div id="div_togger2">
            <div class="toggerHeader">
                <label>删除</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>退料单身:</label>
                    <input type="text" id="lab3" readonly="readonly" runat="server" />
                </div>
                <div>
                    <label>退料工单号:</label>
                    <input type="text" id="lab2" readonly="readonly" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button7" runat="server" Text="确定" OnClick="delete_single_query" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger3">
            <div class="toggerHeader">
                <label>退料操作</label>
            </div>
            <div class="toggerBody">
                 <div>
                    <label>退料单身:</label> 
                   <input id="return_line_id" class="input_text"  readonly="readonly" runat="server" type="text" />                 
                  </div>
                  <div>
                    <label>库别:</label> 
                   <input id="singlequery_subinventory" class="input_text"  readonly="readonly" runat="server" type="text" />                 
                  </div>
                  <div>
                   <label>区域:</label>
                  <input id="singlequery_region" class="input_text"  readonly="readonly" runat="server" type="text" /> 
                 </div>
                 <div>
                    <label>料号:</label>
                    <input id="singlequery_item_name" class="input_text" readonly="readonly" runat="server" type="text" />
                </div>
                 <div>
                     <label>退料数量:</label>
                    <input id="singlequery_return_qty" class="input_text"  runat="server" type="text" />
                 </div>          
            </div>
          
            <div class="toggerFooter">
                <asp:Button ID="Button2" runat="server" Text="确定" OnClick="Opration_SingelQuery" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>
    </div>
    <script src="JavaScript/SingleQuery.js"></script>
</asp:Content>


