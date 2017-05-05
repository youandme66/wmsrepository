<%@ Page Title="WorkSheetIn" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="WorkSheetIn.aspx.cs" Inherits="WMS_v1._0.Web.WorkSheetIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/worksheetIn.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <%-- 打印按钮的弹框--%>
            <div id="zindex">
                <div id="div_togger1">
                    <div class="toggerHeader">
                        <label>打印入库单</label>
                    </div>
                    <div class="toggerBody">
                        <div>
                            <input class="input_text" type="text" id="select_text_print" runat="server" placeholder="请输工单編号" />
                            <input class="input_text" type="text" id="select_text_print2" runat="server" placeholder="请输入需要入庫的數量" />
                        </div>
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Button2" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>


    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style"  type="reset" runat="server" onserverclick="cleanup">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                 <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                <button class="btn_style"  type="button" runat="server" onserverclick="search">
                   <%-- <img src="icon/搜索.png" /><br />--%>
                    搜索
                </button>
                <input type="button" name="Submit" value="打印" class="btn_style picture_width" style="display:none" id="Print" runat="server"  />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label style="color:red">请先选择库别和料架！</label>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        
                        <td class="tdLab">
                            <label>工单编号</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="wo_no" name="wo_no" oninput="change_gethand();" />

                        </td>
                       
                        <td class="tdLab">
                            <label>数量</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="number" runat="server" />
                        </td>
                         <td class="tdLab">
                            <label>料号</label>
                         </td>
                        <td class="tdInput">
                            <input type="text" readonly="readonly" id="item_name" name="item_name" />
                        </td>
                        <td class="tdLab">
                            <label>工单量</label>
                         </td>
                        <td class="tdInput">
                            <input type="text" readonly="readonly" id="target_qty" name="target_qty" />
                        </td>
                        <td class="tdLab"></td>
                         <td class="tdLab">
                            <label>已存量</label>
                         </td>
                        <td class="tdInput">
                            <input type="text" readonly="readonly" id="onhand" name="onhand" />
                        </td>
                         <asp:UpdatePanel ID="updatepanl12" runat="server">
                            <ContentTemplate>
                            <td class="tdLab">
                            <label>库别</label>
                         </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                                <td class="tdLab">
                            <label>料架</label>
                         </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                        </td>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        

                                          
                        <td class="tdLab" style="text-align: center;">
                            <button id="Button1" class="tdBtu" type="button" runat="server" onserverclick="workSheetIn">入库</button>
                        </td>
                       
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
            <div class="list_foot">
                <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" EmptyDataText="Data Is Empty" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />
                        <asp:BoundField DataField="ITEM_ID" HeaderText="料号ID" />
                        <asp:BoundField DataField="ONHAND_QUANTIY" HeaderText="在手总量" />
                        <asp:BoundField DataField="SUBINVENTORY" HeaderText="库别" />
                        <asp:BoundField DataField="CREATE_TIME" HeaderText="创建时间" />
                        <asp:BoundField DataField="UPDATE_TIME" HeaderText="更新时间" />
                    </Columns>
                </asp:GridView>

                <%--<asp:GridView ID="GridView2" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" EmptyDataText="Data Is Empty" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ITEM_ID" HeaderText="料架ID" />
                        <asp:BoundField DataField="FRAME_KEY" HeaderText="料架" />
                        <asp:BoundField DataField="SUBINVENTORY" HeaderText="库别" />
                        <asp:BoundField DataField="DATECODE" HeaderText="生产周期" />
                        <asp:BoundField DataField="ONHAND_QTY" HeaderText="在手量" />
                        <asp:BoundField DataField="SIMULATED_QTY" HeaderText="模拟量" />
                        <asp:BoundField DataField="LEFT_QTY" HeaderText="剩余量" />
                        <asp:BoundField DataField="RETURN_FLAG" HeaderText="退料标示" />
                        <asp:BoundField DataField="LAST_REINSPECT_TIME" HeaderText="最后一次复验时间" />
                        <asp:BoundField DataField="LAST_REINSPECT_STATUS" HeaderText="最后一次复验状态" />
                        <asp:BoundField DataField="CREATE_TIME" HeaderText="创建时间" />
                        <asp:BoundField DataField="UPDATE_TIME" HeaderText="更新时间" />
                    </Columns>
                </asp:GridView>--%>
            </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
        </div>
    </div>
    <script src="JavaScript/worksheetin.js"></script>
    <script src="JavaScript/PrintJS.js"></script>
</asp:Content>
