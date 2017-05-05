<%@ Page Title="analogAcquisitionPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="analogAcquisitionPDA.aspx.cs" Inherits="WMS_v1._0.PDA.analogAcquisitionPDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/analogAcquisition.css" rel="stylesheet" />
    <div class="list">
        <div class="list_top">
            <asp:Panel ID="Panel1" DefaultButton="add_wo_no" runat="server">
                <div class="section-top">
                    <div class="Label">
                        <label>模拟工单</label>
                    </div>
                    <div class="textInput">
                        <asp:TextBox ID="wo_no_1" runat="server" Text=""></asp:TextBox>
                    </div>
                    <div class="inputBtn">
                        <asp:Button runat="server" ID="add_wo_no" Text="add" Style="display: none" OnClick="add_wo_no_Click" OnClientClick="a()"/>
                        <asp:Button runat="server" ID="latest_demand" Text="最新需求" OnClick="one_latest_demand"  OnClientClick="a()"/>
                    </div>

                    <div class="inputBtn">
                        <asp:Button ID="Button1" style="width: 50px" runat="server" Text="查询" OnClick="select_one_simulate"></asp:Button>
                    </div>
                    <div class="inputBtn">
                         <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />
                    </div>
                </div>

                <div class="section-bottom">
                    <div class="ctrlBtn">
                        <ul>
                            <li>
                               <asp:Button runat="server" ID="start_analog" Text="开始模拟" OnClick="start_analog_Click"/>

                            </li>
                            <li>
                                <asp:Button runat="server" ID="export_excel" Text="导出Excel" OnClick="export_excel_Click"/>
                            </li>
                            <li>
                                <asp:Button ID="clear_button" runat="server"  OnClick="clear_button_Click" Text="清空"/>
                            </li>
                        </ul>
                    </div>
                    <div class="header_Table" style="overflow: auto">
                        <asp:GridView ID="GridView_Add" runat="server" BorderStyle="None" CellPadding="0" GridLines ="None" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="工单模拟号：" datafield="wo_no_ml"/>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="headerWord">
                        <span id="status" runat="server"></span>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="list_foot">
            <div class="list-foot-header" style="padding-bottom: 0px;padding-left:10px;padding-top:10px">
                <button class="list-foot-header-btn" type="button" onclick="Cmd(1)">工单需求</button><button class="list-foot-header-btn" type="button" onclick="Cmd(2)">模拟结果</button><button class="list-foot-header-btn" type="button" onclick="Cmd(3)">缺料明细</button>
            </div>
            <div id="panl1" runat="server">
                 <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="requirement_line_id" HeaderText="需求ID" Visible="false" />
                        <asp:BoundField DataField="wo_no" HeaderText="工单号" />
                        <asp:BoundField DataField="bom_version" HeaderText="BOM版本" />
                        <asp:BoundField DataField="item_name" HeaderText="料号" />
                        <asp:BoundField DataField="operation_seq_num" HeaderText="制程" />
                        <asp:BoundField DataField="required_qty" HeaderText="需求量" />
                        <asp:BoundField DataField="issued_qty" HeaderText="发料量" />
                        <asp:BoundField DataField="issue_invoice_qty" HeaderText="领料量" />
                        <asp:BoundField DataField="return_invoice_qty" HeaderText="退料量" />
                        <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                        <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                    </Columns>
                </asp:GridView>
            </div>
            <div id="panl2" style="display:none" runat="server">
                <asp:GridView ID="GridView2" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="simulate_line_id" HeaderText="模拟id" Visible="false"/>
                        <asp:BoundField DataField="unique_id" HeaderText="唯一ID" Visible="false" />
                        <asp:BoundField DataField="simulate_id" HeaderText="备料序号" />
                        <asp:BoundField DataField="wo_no" HeaderText="工单号" />
                        <asp:BoundField DataField="wo_key" HeaderText="工单key值" Visible="false"/>
                        <asp:BoundField DataField="item_id" HeaderText="料号id" />
                        <asp:BoundField DataField="requirement_qty" HeaderText="需求量" />
                        <asp:BoundField DataField="simulated_qty" HeaderText="模拟量" />
                        <asp:BoundField DataField="status" HeaderText="状态" />
                        <asp:BoundField DataField="pickup_qty" HeaderText="备料量" />
                        <asp:BoundField DataField="issued_qty" HeaderText="发料量" />
                    </Columns>
                </asp:GridView>
            </div>
            <div id="panl3" style="display:none" runat="server">
                <asp:GridView ID="GridView3" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TL_id" HeaderText="仓库" />
                    </Columns>
                </asp:GridView>
            </div>
           </div>
           <%-- 打印按钮的弹框--%>
            <div id="zindex3">
                <div id="div_togger3">
                    <div class="toggerHeader">
                        <label>打印模拟单</label>
                    </div>
                    <div class="toggerBody">
                        <div>
                            <input class="input_text" type="text" id="select_text_print" runat="server" placeholder="请输入备料单号" />
                        </div>
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Button2" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>
    <script src="JavaScript/analogAcquisition.js"></script>
    <script src="JavaScript/print_wo_no.js"></script>
</asp:Content>