<%@ Page Title="AllocationList" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="AllocationList.aspx.cs" Inherits="WMS_v1._0.Web.AllocationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <%--<a href="#" class="picture_width" style="outline: 0;">
                    <img src="icon/clear.png" /><br />
                    <label>Clear</label>
                </a>--%>
                <button class="btn_style" type="button" runat="server" onserverclick="CleanAllMessage_Click">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" id="Button2" type="button" runat="server" onserverclick="invoice_no_make_Click">
                    <%--<img src="icon/insert.png" /><br />--%>
                   生成
                </button>
                <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />
                <%--<a href="#" class="picture_width" style="outline: 0;">
                    <img src="icon/insert.png" /><br />
                    <label>Insert</label>
                </a>--%>
                <%-- <button type="button">
                    <img src="icon/insert.png" /><br />
                    Insert
                </button>--%>
            </div>

             <%-- 打印按钮的弹框--%>
            <div id="zindex">
                <div id="div_togger1">
                    <div class="toggerHeader">
                        <label>打印调拨单</label>
                    </div>
                    <div class="toggerBody">
                        <div>
                            <input class="input_text" type="text" id="select_text_print" runat="server" placeholder="请输入调拨单号" />
                        </div>
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Button3" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>


            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td>
                            <label>新调拨单</label>

                        </td>
                        <td>
                            <input type="text" runat="server" id="invoice_no"  readonly="readonly" disabled="disabled"/>
                        </td>

                        <td>
                            <label>调出库别</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="out_subinventory_name" runat="server" Height="25px" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>
                            <label>调入库别</label>

                        </td>
                        <td>
                            <asp:DropDownList ID="in_subinventory_name" runat="server" Height="25px" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                
                        <td>
                            <label>调出Locator</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="out_locator_name" runat="server" Height="25px" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>
                            <label>调入Locator</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="in_locator_name" runat="server" Height="25px" AutoPostBack="true"></asp:DropDownList>
                        </td>

                        <td>
                            <input id="Button1" type="button" value="添加" runat="server" onserverclick="AddMeassage_Clisk1" />
                        </td>
                    </tr>
                    <tr>
<%--                        <td>
                            <label>调拨原因</label>
                        </td>
                        <td>
                            <input type="text" runat="server" id="Text1" placeholder="请输入字符" />
                        </td>
                        <td>
                            <label>部门代码</label>
                        </td>
                        <td>
                            <input type="text" runat="server" id="Text2" placeholder="请输入字符" />
                        </td>--%>

                    </tr>
                </table>
            </div>
        </div>

        <div class="zb_left" id="Div1">
            <asp:GridView ID="exchang_header_gridview"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                AllowPaging="true"
                PageSize="6"
                runat="server"
                EmptyDataText="Data Is Empty"
                AutoGenerateColumns="False" OnPageIndexChanging="exchange_message_gridview_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Invoice_no" HeaderText="调拨单号" />
                    <asp:BoundField DataField="out_subinventory_key" HeaderText="调出库别" />
                    <asp:BoundField DataField="in_subinventory_key" HeaderText="调入库别" />
                    <asp:BoundField DataField="in_locator_id" HeaderText="In Loc" />
                    <asp:BoundField DataField="out_locator_id" HeaderText="Out Loc" />
                    <asp:BoundField DataField="exchange_wo_no" HeaderText="创建者" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="list_foot">
            <div class="foot_zb">
                <div class="list_item">
                    <table class="tab">
                        <tr>
                            <td>
                                <div class="list_item">
                                    <table class="tab">
                                        <tr>
                                            <td>
                                                <label>料号</label>
                                            </td>
                                            <td>
                                                <%--<input type="text" runat="server" id="item_name" placeholder="请输入字符"/>--%>
                                                    <asp:DropDownList ID="item_name_List" runat="server" Height="25px" AutoPostBack="true"></asp:DropDownList>
                                            </td>
                                               <td>
                                                <label>DateCode</label></td>
                                            <td>
                                                <input type="text" runat="server" id="datecode" placeholder="请输入数字" />
                                            </td>
                                            <td>
                                                <label>调拨量</label>
                                            </td>
                                            <td>
                                                <input type="text" runat="server" id="exchange_qty" placeholder="请输入数字" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>制程</label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="operation_seq_num" runat="server" Height="25px" AutoPostBack="true"></asp:DropDownList>
                                            </td>
 <%--                                                                                       <td>
                                                <label>需求量</label>
                                            </td>--%>
                                            <td>
                                                <input type="text" runat="server" id="required_qty" placeholder="请输入数字" hidden="hidden" />
                                            </td>
                                         
                                            <%--   <td>
                                                <input type="text" runat="server" id="operation_seq_num" placeholder="请输入数字" />
                                            </td>--%>
                                            <%--                                            <td>
                                                <label>工单</label></td>
                                            <td>
                                                <input type="text" runat="server" id="exchange_wo_no"/>
                                            </td>--%>
                                            <td>
                                                <label>备注</label>
                                            </td>
                                            <td>
                                                <input type="text" runat="server" id="remark" placeholder="请输入字符" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                                <div class="list_item">
                                    <table class="tab">
                                        <tr>
                                            <td>
                                                <input type="button" value="添加" runat="server" onserverclick="AddMeassage_Clisk" /></td>
                                        </tr>
                                        <%--                                        <tr>
                                            <td>
                                                <input type="button" value="清除" runat="server" onserverclick="CleanAllMessage_Click" /></td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <input type="button" value="提交" runat="server" onserverclick="CommitMessage_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="foot_cb">
                <div class="zb_left" id="zb_left" style="margin-top: 5px;">
                    <div class="list_item">
                        <table class="tab">

                            <%--                            <tr>
                                <td>
                                    <input type="button" value="Commit" runat="server" onserveclick="CleanAllMessage_Click"/></td>
                            </tr>--%>
                        </table>
                    </div>
                </div>
                <div class="zb_left" id="zb_right">
                    <asp:GridView ID="exchange_message_gridview"
                        CssClass="mGrid"
                        PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt"
                        AllowPaging="true"
                        PageSize="6"
                        runat="server"
                        EmptyDataText="Data Is Empty"
                        AutoGenerateColumns="False" OnPageIndexChanging="exchange_message_gridview_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Invoice_no" HeaderText="调拨单号" />
                            <asp:BoundField DataField="out_subinventory_key" HeaderText="调出库别" />
                            <asp:BoundField DataField="in_subinventory_key" HeaderText="调入库别" />
                            <asp:BoundField DataField="in_locator_id" HeaderText="In Loc" />
                            <asp:BoundField DataField="out_locator_id" HeaderText="Out Loc" />
                            <asp:BoundField DataField="item_name" HeaderText="料号" />
                            <asp:BoundField DataField="Operation_seq_num" HeaderText="制程" />
                            <asp:BoundField DataField="required_qty" HeaderText="需求量" />
                            <asp:BoundField DataField="exchange_qty" HeaderText="调拨量" />
                            <asp:BoundField DataField="remark" HeaderText="备注" />
                            <asp:BoundField DataField="exchange_wo_no" HeaderText="创建者" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
        <script src="JavaScript/allocationList.js"></script>
        <script src="JavaScript/PrintJS.js"></script>
    </div>
</asp:Content>
