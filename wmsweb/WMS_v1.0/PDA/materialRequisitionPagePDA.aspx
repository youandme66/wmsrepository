<%@ Page Title="materialRequisitionPagePDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="materialRequisitionPagePDA.aspx.cs" Inherits="WMS_v1._0.PDA.materialRequisitionPagePDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <%--<button id="Button1" class="picture_width" style="outline: 0;" runat="server">
                    <img src="icon/clear.png" /><br />
                    Clear
                </button>--%>
                <button  id="Button2" class=" btn_style picture_width" style="outline: 0;" runat="server" onserverclick="insert">
                   <%-- <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>领料单号</label>
                        </td>
                        <td class="tdInput">
                            <input class="InputStyle" id="invoice_no" disabled="disabled" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>领料人</label>
                        </td>
                        <td class="tdInput">
                            <input class="InputStyle" id="issue_man" type="text" runat="server" />

                        </td>
                        <td class="tdLab">
                            <label>客户代码</label>
                        </td>
                        <td  class="tdInput">
                            <input class="InputStyle" id="customer_key" type="text" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>制程别</label>

                        </td>
                        <td class="tdInput">
                            <select class="InputStyle" id="operation_seq_num_c" name="operation_seq_num_c">
                            </select>
                        </td>
                        <td hidden="hidden">
                            <select class="InputStyle" id="operation_seq_num" runat="server">
                                <option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                            </select>
                        </td>

                        <td class="tdLab">
                            <label>领料类型</label>

                        </td>
                        <td class="tdInput">
                            <select class="InputStyle" id="issue_type" runat="server">
                                <option>工单领料</option>
                                <option>非工单领料</option>
                            </select>

                        </td>
                        <td class="tdLab">
                            <label>工单号</label>
                        </td>
                        <td class="tdInput">
                            <select class="InputStyle" id="wo_no_c" name="wo_no_c">
                            </select>
                        </td>
                        <td hidden="hidden" >
                            <input class="InputStyle" id="wo_no" type="text" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>库别</label>

                        </td>
                        <td class="tdInput">
                            <select class="InputStyle" id="issued_sub_key_c" name="issued_sub_key_c">
                            </select>

                        </td>
                        <td hidden="hidden">
                            <select class="InputStyle" id="issued_sub_key" name="issued_sub_key_c" runat="server">
                            </select>
                        </td>
                       
                        <td class="tdLab">
                            <input id="Button7" style="width:76%" type="button" class="input_btn" value="提交" runat="server" onserverclick="Issue_Header_Commit" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>料号</label>

                        </td>
                        <td class="tdInput">
                            <input class="InputStyle" id="item_name" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>需求量</label>
                        </td>
                        <td  class="tdInput">
                            <input class="InputStyle" id="required_qty" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>模拟量</label></td>
                        <td class="tdInput">
                            <input class="InputStyle" id="simulated_qty" type="text" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>领料量</label></td>
                        <td  class="tdInput">
                            <input class="InputStyle" id="issued_qty" type="text" runat="server" />
                        </td>
                        
                        <%--<td>
                            <label>领料工单号</label>
                        </td>
                        <td colspan="2">
                            <input id="issue_wo_no" type="text" runat="server" />
                        </td>
                        <td>
                            <label>备料数量</label></td>
                        <td colspan="2">
                            <input id="pickup_qty" type="text" runat="server" /></td>--%>
                        <td class="tdLab">
                            <label>料架号</label>
                        </td>
                        <td class="tdInput">
                            <select class="InputStyle" id="frame_key_c" name="frame_key_c">
                            </select>
                        </td>
                     
                        <td hidden="hidden" >
                            <input class="InputStyle" id="frame_key" type="text" runat="server" />
                        </td>
       
                        <td class="tdLab">
                            <input id="Button6" style="width:70%" type="button" class="input_btn" value="添加" runat="server" onserverclick="Issue_Line_Commit" />
                        </td>
                      
                    </tr>
                </table>
            </div>
        </div>
        <div class="list_foot">
            <div class="foot_zb">
                <div class="zb_left">
                    <%--<input id="Button3" type="button" class="input_btn" value="Clear" runat="server" />--%>
                    <input id="Button4" type="button" style=" margin-right: 20px;width:70% "  class="input_btn" value="提交" runat="server" onserverclick="Commit_All_Issue_Line" />
                    
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
                                <%-- <asp:BoundField DataField="Id_diy" HeaderText="项次" />--%>
                                <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="REQUIRED_QTY" HeaderText="需求量" ItemStyle-Width="50px">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIMULATED_QTY" HeaderText="模拟量" ItemStyle-Width="50px">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ISSUED_QTY" HeaderText="领料量" ItemStyle-Width="50px">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FRAME_KEY" HeaderText="料架号" ItemStyle-Width="150px">
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
            <div class="foot_cb">
                <div class="cb_top">
                    <label>查询领料单号</label>
                    <input type="text" runat="server" id="select_text" value="%" />
                    <button id="Button5" style="display:inline-block;width:10%;" type="button" runat="server" onserverclick="select">查询</button>
                </div>
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="200px" BorderWidth="1px">
                    <asp:GridView
                        ID="GridView2"
                        CssClass="mGrid"
                        class="GridView"
                        runat="server"
                        PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt"
                        AllowPaging="true"
                        PageSize="3"
                        EmptyDataText="Data Is Empty"
                        AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="invoice_no" HeaderText="领料单号" />
                            <asp:BoundField DataField="issue_type" HeaderText="领料类别" />
                            <asp:BoundField DataField="wo_no" HeaderText="工单号" />
                            <asp:BoundField DataField="operation_seq_num" HeaderText="制程号" />
                            <asp:BoundField DataField="issued_sub_key" HeaderText="库别" />
                            <asp:BoundField DataField="issue_man" HeaderText="领料人" />
                            <asp:BoundField DataField="customer_key" HeaderText="客户代码" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script src="JavaScript/materialRequisition.js"></script>
</asp:Content>
