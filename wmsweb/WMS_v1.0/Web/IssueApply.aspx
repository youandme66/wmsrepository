<%@ Page Title="领料单申请" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="IssueApply.aspx.cs" Inherits="WMS_v1._0.Web.IssueApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/IssueApply.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button2" class=" btn_style picture_width" style="outline: 0;" runat="server" onserverclick="NewInvoice_no">
                    <%-- <img src="icon/insert.png" /><br />--%>
                   新增
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>领料单号</label>
                        </td>
                        <td class="tdInput">
                            <input id="invoice_no" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>部门代码</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_Flax_value" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Flax_value_SelectedIndexChanged" />
                        </td>
                        <td class="tdLab">
                            <label>领料部门</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_description" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>领料类型</label>
                        </td>
                        <td class="tdInput">
                            <select id="issue_type" name="issue_type" class="select_text">
                                <option value="">选择类型</option>
                                <option>工单领料</option>
                                <option>非工单领料</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>领料原因</label>
                        </td>
                        <td class="tdInput">
                            <input id="remark" type="text" runat="server" placeholder="可不填写" />
                        </td>
                        <td class="tdLab"></td>
                        <td class="tdButton">
                            <input id="Button_Issue_Header_Commit" type="button" class="input_btn" value="提交单头" runat="server" onserverclick="Issue_Header_Commit" />
                        </td>
                    </tr>

                    <tr>


                        <td>
                            <br />
                            <%--  <br />--%>
                            <span>工单领料</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>领料工单号（已发料）</label>
                        </td>
                        <%-- <td colspan="2">
                            <input id="return_wo_no" type="text" runat="server" />
                        </td>--%>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_issue_wo_no" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_issue_wo_no_SelectedIndexChanged" />
                        </td>

                        <td class="tdLab"></td>
                        <td class="tdButton">
                            <input id="Button_import_short_detail" type="button" class="input_btn" value="导入缺料明细" runat="server" onserverclick="import_short_detail" />
                        </td>

                        <td class="tdLab">
                            <label>料号</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_item_name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_item_name_SelectedIndexChanged" />
                        </td>
                        <%--  <td class="tdLab">
                            <label>料架</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_frame_key" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_frame_key_SelectedIndexChanged" />
                        </td>--%>
                        <td class="tdLab">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_issue_sub_key" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>制程</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_operation_seq_num" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>需求量</label></td>
                        <td class="tdInput">
                            <input id="required_qty" type="text" runat="server" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>发料量</label></td>
                        <td class="tdInput">
                            <input id="simulated_qty" type="text" runat="server" disabled="disabled" />
                        </td>
                        <td class="tdLab">
                            <label>领料量</label></td>
                        <td class="tdInput">
                            <input id="issued_qty" type="text" runat="server" placeholder="该量不能超过料架的量" />
                        </td>
                        <td class="tdLab"></td>
                        <td class="tdButton">
                            <input id="Botton_Issue_Line_Commit" type="button" class="input_btn" value="添加单身" runat="server" onserverclick="Issue_Line_Commit" />
                        </td>

                    </tr>
                    <%-- 非工单领料部分--%>
                    <tr>
                        <td>
                            <%-- <br />--%>
                            <span>非工单领料</span>
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
                            <label>制程</label></td>
                        <td class="tdInput">
                            <input id="operation_seq_num2" type="text" runat="server" placeholder="请输入制程代号" />
                        </td>
                        <td class="tdLab">
                            <label>领料量</label></td>
                        <td class="tdInput">
                            <input id="issued_qty2" type="text" runat="server" placeholder="不要超过库存量" />
                        </td>
                        <td class="tdLab"></td>
                        <td class="tdButton">
                            <input id="Button1" type="button" class="input_btn" value="添加单身" runat="server" onserverclick="Issue_Line_Commit" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="list_foot">
            <div class="foot_zb">
                <div class="zb_left">
                    <%--<input id="Button3" type="button" class="input_btn" value="Clear" runat="server" />--%>
                    <input id="Button4" type="button" style="width: 200px; margin-right: 20px;" class="input_btn" value="提交所有单身" runat="server" onserverclick="Commit_All_Issue_Line" />

                </div>
                <div class="zb_right" aria-multiline="False">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" BorderWidth="1px">
                        <asp:GridView
                            CssClass="mGrid"
                            ID="GridView1"
                            class="GridView"
                            EmptyDataText="Data Is Empty"
                            runat="server" AutoGenerateColumns="False"  OnRowDeleting="Delete_row"  OnRowUpdating="TaskGridView_RowUpdating"  SelectionMode="FullRowSelect">
                            <Columns>
                                <%-- <asp:BoundField DataField="Id_diy" HeaderText="项次" />--%>
                                
                                <asp:BoundField DataField="line_num" HeaderText="line_Num" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField> 
                                
                                                            
                                <asp:BoundField  DataField="item_name" HeaderText="料号" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />                           
                                </asp:BoundField>
                                      
                               

                        <asp:TemplateField HeaderText="制程ID">
                            <ItemTemplate>
                                <asp:TextBox ID="peration_seq_num" Width="70px" runat="server" Text='<%# Eval("peration_seq_num") %>'></asp:TextBox>
                            </ItemTemplate>                       
                        </asp:TemplateField>

                                <asp:BoundField DataField="wo_no" HeaderText="领料工单" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="required_qty" HeaderText="需求量" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="simulated_qty" HeaderText="发料量" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="issued_qty" HeaderText="领料量" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="领料量">
                            <ItemTemplate>
                                <asp:TextBox ID="issued_qty" Width="70px" runat="server" Text='<%# Eval("issued_qty") %>'></asp:TextBox>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                               <%-- <asp:BoundField DataField="issued_sub" HeaderText="库别" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>--%>

                        <asp:TemplateField HeaderText="库别">
                            <ItemTemplate>
                                <asp:TextBox ID="issued_sub" Width="70px" runat="server" Text='<%# Eval("issued_sub") %>'></asp:TextBox>
                            </ItemTemplate>                        
                        </asp:TemplateField>


                                <%--  <asp:BoundField DataField="frame_key" HeaderText="料架" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>--%>

                                <asp:BoundField DataField="create_man" HeaderText="创建人" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="create_time" HeaderText="创建时间" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>

                                <%--<asp:TemplateField HeaderText="更新">
                                    <ItemTemplate>
                                        <%-- <asp:Button ID="btn_update" class="btn_update" runat="server" Text="更新" />
                                        <button id="btn_update" class="btn_update" runat="server" type="button">更新</button>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                                <asp:TemplateField HeaderText="更新">
                                    <ItemTemplate>
                                        <asp:Button ID="Update" CommandName="Update" runat="server" Text="更新"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
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

       <%-- <div id="zindex">
            <div id="div_togger2">               
                <div class="toggerHeader">
                    <label>更新</label>
                </div>
                <div class="toggerBody">
                    <div>
                        <label>库别</label>
                        <input class="input_text" type="text" id="subinventory_update" runat="server" placeholder="请输入库别" />
                    </div>
                    <div>
                        <label>制程</label>
                        <input class="input_text" type="text" id="route_update" runat="server" placeholder="请输入制程代号" />
                    </div>
                </div>
                <div class="toggerFooter">
                    <asp:Button ID="UpdateButton" runat="server" OnClick="Update_row" Text="确定" />
                    <asp:Button ID="Button5" runat="server" OnClick="CleanUpdateMessage" Text="取消" />
                </div>
            </div>
        </div>--%>

    </div>
    <script src="JavaScript/IssueApply.js"></script>
</asp:Content>
