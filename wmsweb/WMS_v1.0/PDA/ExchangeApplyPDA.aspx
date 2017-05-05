<%@ Page Title="ExchangeApplyPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="ExchangeApplyPDA.aspx.cs" Inherits="WMS_v1._0.PDA.ExchangeApplyPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <label>调拨单号</label>
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
                            <label>调拨部门</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_description" runat="server" />
                        </td>
                        <td class="tdLab">
                            <input id="Button_Exchange_Header_Commit" type="button" class="input_btn" value="提交单头" runat="server" onserverclick="Exchange_Header_Commit" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                    <%--    <td class="tdLab">
                            <label>调出料架</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_out_frame_key" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_out_frame_key_SelectedIndexChanged" />
                        </td>--%>
                        <td class="tdLab">
                            <label>调出库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_out_subinventory" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList_out_subinventory_SelectedIndexChanged"/>
                        </td>
                        <td class="tdLab">
                            <label>料号</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_item_name" runat="server" />
                        </td>
                       <%-- <td class="tdLab">
                            <label>调入料架</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList_in_frame_key" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_in_frame_key_SelectedIndexChanged" />
                        </td>  --%>
                         <td class="tdLab">
                            <label>调入库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_in_subinventory" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>制程</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList class="selectInput" ID="DropDownList_operation_seq_num" runat="server" />
                        </td>
                      <%--  <td class="tdLab">
                            <label>调拨上限</label></td>
                        <td class="tdInput">
                            <input id="exchange_limit" type="text" runat="server" />
                        </td>--%>
                        <td class="tdLab">
                            <label>需求量</label></td>
                        <td class="tdInput">
                            <input id="required_qty" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>调拨数量</label></td>
                        <td class="tdInput">
                            <input id="exchanged_qty" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <input id="Botton_Exchange_Line_Commit" type="button" class="input_btn" value="添加单身" runat="server" onserverclick="Exchange_Line_Commit" />
                        </td>

                    </tr>
                </table>
            </div>
        </div>
        <div class="list_foot">
            <div class="foot_zb">
                <div class="zb_left">
                    <%--<input id="Button3" type="button" class="input_btn" value="Clear" runat="server" />--%>
                    <input id="Button4" type="button" style=" margin-right: 20px;" class="input_btn" value="提交所有单身" runat="server" onserverclick="Commit_All_Exchange_Line" />

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
                               
                                <asp:BoundField DataField="item_name" HeaderText="料号" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="operation_seq_num" HeaderText="制程ID" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="out_subinventory" HeaderText="调出库别K值" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <%--  <asp:BoundField DataField="out_frame_key" HeaderText="调出料架" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="in_subinventory" HeaderText="调入库别K值" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="in_frame_key" HeaderText="调入料架" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="required_qty" HeaderText="需求量" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="exchanged_qty" HeaderText="调拨数量" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="create_man" HeaderText="创建人" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="create_time" HeaderText="创建时间" ItemStyle-Width="250px">
                                    <ItemStyle Width="250px" />
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
