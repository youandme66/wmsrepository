<%@ Page Title="PoSuspense" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="PoSuspense.aspx.cs" Inherits="WMS_v1._0.Web.PoSuspense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/PoStorage.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" runat="server" onserverclick="ClearALLMessage_Click">
                  <%--  <img src="icon/delete.png" /><br />--%>
                    清除
                </button>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>PO单号</label>
                        </td>
                        <td class="tdInput">
 <asp:dropdownlist id="po_no" runat="server" AutoPostBack="True" />
                        </td>
                        <td class="tdLab">
                            <label>Line序号</label>
                        </td>
                        <td class="tdInput">
                           <asp:dropdownlist id="line_num" runat="server"/>
                        </td>
                        <td class="tdLab">
                            <asp:Button runat="server" CssClass="selectBtn" ID="btn2" OnClick="ChangeNav_Click" Text="提交" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>料号ID</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="item_id_auto" disabled="disabled" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>料号名称</label></td>
                        <td class="tdInput">
                             <input type="text" id="item_name_auto" disabled="disabled" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>需求量</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="request_qty" disabled="disabled" runat="server" />
                             <input type="text" id="po_header_id"  runat="server" style="display:none"/>
                             <input type="text" id="po_line_id"  runat="server"  style="display:none"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>VendorName</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="vendor_name" runat="server" disabled="disabled"  /> </td>
                        <td class="tdLab">
                            <label>收料量</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="rcv_qty" runat="server" placeholder="请输入数字" />
                        </td>
                        <td class="tdLab">
                            <label>DateCode</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="datecode" runat="server" placeholder="请输入字符" />
                        </td>
                        <td class="tdLab">
                            <asp:Button runat="server" CssClass="selectBtn" ID="Button4" OnClick="PoCommitmessage_Click" Text="保存全部" />
                        </td>
                                                    <td class="tdInput">
                                <input type="text" id="vendor_code" runat="server" disabled="disabled" hidden="hidden"/>
                            </td>
                    </tr>
                </table>
            </div>


            <div id="foot_zbSecond" class="foot_zb1" style="display: none;">
                <asp:GridView ID="serch_POHeader" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="6" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                        <asp:BoundField DataField="vendor_key" HeaderText="vendor_key" />
                        <asp:BoundField DataField="CREATE_TIME" HeaderText="CREATE_TIME" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="serch_POLine" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="6" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="PO_LINE_ID" HeaderText="PO_LINE_ID" />
                        <asp:BoundField DataField="ITEM_ID" HeaderText="ITEM_ID" />
                        <asp:BoundField DataField="LINE_NUM" HeaderText="LINE_NUM" />
                        <asp:BoundField DataField="REQUEST_QTY" HeaderText="REQUEST_QTY" />
                    </Columns>
                </asp:GridView>
            </div>

        </div>

        <div class="list_foot">
            <div class="cb_top">
                <div class="list_item">
                    <table class="tab">
                        <tr>
                            <td class="tdLab">
                                <label>暂收单号</label>

                            </td>
                            <td class="tdInput">
                                <input type="text" value="" runat="server" id="Receipt_no_Query" placeholder="请输入字符" />

                            </td>
                            <td class="tdLab">
                                <label>料号</label>

                            </td>
                            <td class="tdInput">
                                <input type="text" value="" runat="server" id="Item_name_Query" placeholder="请输入字符" />

                            </td>
                            <td class="tdLab">
                                <label>PO</label>

                            </td>
                            <td class="tdInput">
                                <input type="text" value="" runat="server" id="Po_no_Query" placeholder="请输入字符" />

                            </td>
                            <td class="tdLab">
                                <input type="button" class="selectBtn" value="查询" runat="server" onserverclick="SerchReceive_mtl_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:GridView
                ID="serch_Receive_mtl"
                runat="server"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                AllowPaging="true"
                PageSize="10"
                AutoGenerateColumns="False"
                EmptyDataText="Data Is Empty"
                OnPageIndexChanging="serch_Receive_mtl_PageIndexChanging" >
                <Columns>
                    <asp:BoundField DataField="RECEIPT_NO" HeaderText="暂收单号" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="料名" />
                    <asp:BoundField DataField="PO_NO" HeaderText="单号" />
                    <asp:BoundField DataField="vendor_name" HeaderText="厂商名称" />
                    <asp:BoundField DataField="RCV_QTY" HeaderText="暂收量" />
                    <asp:BoundField DataField="DATECODE" HeaderText="生成周期" />
                    <asp:BoundField DataField="PO_NO" HeaderText="PO单号" />
                    <asp:BoundField DataField="LINE_NUM" HeaderText="Line序号" />
                    <asp:BoundField DataField="Create_time" HeaderText="创建时间" />
                    <asp:BoundField DataField="Update_time" HeaderText="更新时间" />
                </Columns>
            </asp:GridView>
        </div>

<%--                            <asp:BoundField DataField="PO_HEADER_ID" HeaderText="PO单头" />
                    <asp:BoundField DataField="PO_LINE_ID" HeaderText="PO单身" />--%>
    </div>
    <%--    <script type="text/javascript" src="JavaScript/jquery-2.2.4.min.js"></script>
    <script>
        function commit() {
            $("#foot_zbFirst").hidden();
            $("#foot_zbSecond").show();

        }
    </script>--%>
</asp:Content>
