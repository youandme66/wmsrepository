<%@ Page Title="" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="ReturnApplyPDA.aspx.cs" Inherits="WMS_v1._0.PDA.ReturnApplyPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button2" class=" btn_style picture_width" style="outline: 0;" runat="server" onserverclick="NewInvoice_no">
                    新增
                </button>
                <%--  <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />--%>
            </div>

            <%--           <%-- 打印按钮的弹框--%>
            <%--  <div id="zindex">
                <div id="div_togger1">
                    <div class="toggerHeader">
                        <label>打印领料单</label>
                    </div>
                    <div class="toggerBody">
                        <div>
                            <input class="input_text" type="text" id="select_text_print" runat="server" placeholder="请输入领料单号" />
                        </div>
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Button1" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>--%>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>退料单号</label>
                        </td>
                        <td class="tdInput">
                            <input id="invoice_no" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>部门代码</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_Flax_value" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Flax_value_SelectedIndexChanged" />
                        </td>
                        <td class="tdLab">
                            <label>退料部门</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_description" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>退料类别</label>
                        </td>
                        <td class="tdInput">
                            <select class="selectInput" id="return_type" name="return_type" class="select_text">
                                <option value="">选择类型</option>
                                <option>工单退料</option>
                                <option>非工单退料</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>退料原因</label>
                        </td>
                        <td class="tdInput">
                            <input id="remark" type="text" runat="server" />
                        </td>
                        <td class="tdLab"></td>
                        <td class="tdInput">
                            <input id="Button_Return_Header_Commit" type="button" class="input_btn" value="提交单头" runat="server" onserverclick="Return_Header_Commit" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <br />
                            <%--  <br />--%>
                            <span>工单退料</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>退料工单</label>
                        </td>
                        <%-- <td colspan="2">
                            <input id="return_wo_no" type="text" runat="server" />
                        </td>--%>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_return_wo_no" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_return_wo_no_SelectedIndexChanged" />
                        </td>
                        <td class="tdLab">
                            <label>料号</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_item_name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_item_name_SelectedIndexChanged" />
                        </td>
                        <%--<td class="tdLab">
                            <label>料架</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_frame_key" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_frame_key_SelectedIndexChanged" />
                        </td>--%>
                        <td class="tdLab">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_return_sub_key" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>制程</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_operation_seq_num" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>退料上限（发料量+领料量）</label></td>
                        <td class="tdInput">
                            <input id="return_limit" type="text" runat="server" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>数量</label></td>
                        <td class="tdInput">
                            <input id="return_qty" type="text" runat="server" />
                        </td>
                        <td class="tdLab"></td>
                        <td class="tdInput">
                            <input id="Botton_Return_Line_Commit" type="button" class="input_btn" value="添加单身" runat="server" onserverclick="Return_Line_Commit" />
                        </td>

                    </tr>
                    <%-- 非工单退料部分--%>
                    <tr>
                        <td>
                            <%-- <br />--%>
                            <span>非工单退料</span>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td class="tdLab">
                            <label>料号</label></td>
                        <td class="tdInput">
                            <input id="item_name2" type="text" runat="server" placeholder="所有料号" />
                        </td>
                        <td class="tdLab">
                            <label>库别</label></td>
                        <td class="tdInput">
                            <input id="subinventory2" type="text" runat="server" />
                        </td>
                        <%--  <td class="tdLab">
                            <label>料架</label></td>
                        <td class="tdInput">
                            <input id="frame2" type="text" runat="server"  />
                        </td>--%>
                        <td class="tdLab">
                            <label>制程代号</label></td>
                        <td class="tdInput">
                            <input id="operation_seq_num2" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>退料量</label></td>
                        <td class="tdInput">
                            <input id="return_qty2" type="text" runat="server" placeholder="不要超过库存量" />
                        </td>
                        <td class="tdLab"></td>
                        <td class="tdInput">
                            <input id="Button1" type="button" class="input_btn" value="添加单身" runat="server" onserverclick="Return_Line_Commit" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="list_foot">
            <div class="foot_zb">
                <div class="zb_left">
                    <%--<input id="Button3" type="button" class="input_btn" value="Clear" runat="server" />--%>
                    <input id="Botton_Commit_All_Return_Line" type="button" style="margin-right: 20px;" class="input_btn" value="提交所有单身" runat="server" onserverclick="Commit_All_Return_Line" />

                </div>
                <div class="zb_right" aria-multiline="False">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" BorderWidth="1px">
                        <asp:GridView
                            CssClass="mGrid"
                            ID="GridView1"
                            class="GridView"
                            EmptyDataText="Data Is Empty"
                            runat="server" AutoGenerateColumns="False" OnRowDeleting="Delete_row">
                            <Columns>
                                <asp:BoundField DataField="line_num" HeaderText="Line_Num" ItemStyle-Width="50px">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="return_wo_no" HeaderText="退料工单号" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="item_name" HeaderText="料号" ItemStyle-Width="50px">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="operation_seq_num" HeaderText="制程ID" ItemStyle-Width="50px">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="return_qty" HeaderText="退料数量" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="return_sub_key" HeaderText="库别ID" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="frame_key" HeaderText="料架" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="create_man" HeaderText="创建者" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="create_time" HeaderText="创建时间" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="删除">
                                    <ItemTemplate>
                                        <asp:Button ID="Delete" CommandName="Delete" runat="server" Text="删除" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
