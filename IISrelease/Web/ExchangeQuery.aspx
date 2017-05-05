<%@ Page Title="调拨单查询" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="ExchangeQuery.aspx.cs" Inherits="WMS_v1._0.Web.ExchangeQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="CSS/DealDetial.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../stylesheets/normalize.css" />
    <link type="text/css" rel="stylesheet" href="../stylesheets/stylesheet.css" />
    <link type="text/css" rel="stylesheet" href="../stylesheets/github-light.css" />
    <link type="text/css" rel="stylesheet" href="../jedate/skin/jedate.css" />
    <script type="text/javascript" src="../jedate/jedate.js"></script>

    <div class="list">
        <div class="list_top">
             <div class="list_top_picture">
                <button class="btn_style" id="Button3" type="button" runat="server" onserverclick="cleanMassage">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />
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
                        <asp:Button ID="Button4" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>


            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td>
                            <label>Form</label>
                        </td>
                        <td colspan="2" class="datep">
                            <input class="datainp" id="inpend" type="text" placeholder="开始日期" readonly="readonly" runat="server" />
                        </td>
                        <script type="text/javascript">

                            var start = {
                                dateCell: '#inpstart',
                                format: 'YYYY-MM-DD hh:mm:ss',
                                //minDate: jeDate.now(0), //设定最小日期为当前日期
                                isinitVal: true,
                                festival: true,
                                ishmsVal: false,
                                //maxDate: '2099-06-30 23:59:59', //最大日期
                                choosefun: function (elem, datas) {
                                    //console.log(datas)
                                    end.minDate = datas; //开始日选好后，重置结束日的最小日期
                                }
                            };
                            var end = {
                                dateCell: '#inpend',
                                format: 'YYYY-MM-DD hh:mm:ss',
                                //minDate: jeDate.now(0), //设定最小日期为当前日期
                                festival: true,
                                //maxDate: '2099-06-16 23:59:59', //最大日期
                                choosefun: function (elem, datas) {
                                    //console.log(end.minDate)
                                    start.maxDate = datas; //将结束日的初始值设定为开始日的最大日期
                                }
                            };
                            jeDate(start);
                            jeDate(end);


                            jeDate({
                                dateCell: '#date01',
                                isClear: true,
                                format: 'YYYY年MM月DD日 hh:mm:ss'
                            });
                            jeDate({
                                dateCell: '#date02',
                                isinitVal: true,
                                format: 'YYYY-MM-DD hh:mm'
                            });
                            jeDate({
                                dateCell: '#date03',
                                format: 'YYYY-MM-DD'
                            });
                            jeDate({
                                dateCell: '#date04',
                                format: 'YYYY-MM'
                            });
                            jeDate({
                                dateCell: '#date05',
                                isinitVal: true,
                                format: 'hh:mm:ss'
                            });
                            jeDate({
                                dateCell: '#date06',
                                format: 'hh:mm'
                            });
                        </script>
                        <td>
                            <label>调拨单号</label>
                        </td>
                       <%-- <td colspan="2">
                            <asp:DropDownList ID="DropDownList_invoice_no" runat="server" />
                        </td>--%>
                         <td class="tdInput">
                            <input id="invoice_no" type="text" runat="server"  />
                        </td>
                        <td>
                            <label>料号</label>
                        </td>
                       <%-- <td colspan="2">
                            <asp:DropDownList ID="DropDownList_item_name" runat="server" />
                        </td>--%>
                         <td class="tdInput">
                            <input id="item_name" type="text" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>To</label>
                        </td>
                        <td colspan="2" class="datep">
                            <input class="datainp" id="inpstart" type="text" placeholder="结束日期" readonly="readonly" runat="server" />
                            <script type="text/javascript">

                                var start = {
                                    dateCell: '#inpstart',
                                    format: 'YYYY-MM-DD hh:mm:ss',
                                    // minDate: jeDate.now(0), //设定最小日期为当前日期
                                    isinitVal: true,
                                    festival: true,
                                    ishmsVal: false,
                                    //maxDate: '2099-06-30 23:59:59', //最大日期
                                    choosefun: function (elem, datas) {
                                        //console.log(datas)
                                        end.minDate = datas; //开始日选好后，重置结束日的最小日期
                                    }
                                };
                                var end = {
                                    dateCell: '#inpend',
                                    format: 'YYYY-MM-DD hh:mm:ss',
                                    // minDate: jeDate.now(0), //设定最小日期为当前日期
                                    festival: true,
                                    //maxDate: '2099-06-16 23:59:59', //最大日期
                                    choosefun: function (elem, datas) {
                                        //console.log(end.minDate)
                                        start.maxDate = datas; //将结束日的初始值设定为开始日的最大日期
                                    }
                                };
                                jeDate(start);
                                jeDate(end);


                                jeDate({
                                    dateCell: '#date01',
                                    isClear: true,
                                    format: 'YYYY年MM月DD日 hh:mm:ss'
                                });
                                jeDate({
                                    dateCell: '#date02',
                                    isinitVal: true,
                                    format: 'YYYY-MM-DD hh:mm'
                                });
                                jeDate({
                                    dateCell: '#date03',
                                    format: 'YYYY-MM-DD'
                                });
                                jeDate({
                                    dateCell: '#date04',
                                    format: 'YYYY-MM'
                                });
                                jeDate({
                                    dateCell: '#date05',
                                    isinitVal: true,
                                    format: 'hh:mm:ss'
                                });
                                jeDate({
                                    dateCell: '#date06',
                                    format: 'hh:mm'
                                });
                            </script>
                        </td>

                        <td>
                            <label></label>
                        </td>
                        <td>
                            <button id="Button1" type="button" runat="server" onserverclick="Select">查询</button>

                        </td>
                        <td>
                            <label></label>
                        </td>
                        <td>
                            <button id="Button2" type="button" runat="server" onserverclick="export">导出</button>

                        </td>

                    </tr>


                </table>
            </div>
        </div>
        <div class="list_foot">
            <span>单头:</span>
            <asp:GridView
                ID="GridView_header"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                runat="server"
                EmptyDataText="Data Is Empty"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="invoice_no" HeaderText="调拨单据号" />
                    <asp:BoundField DataField="flex_value" HeaderText="部门编号" />
                    <asp:BoundField DataField="description" HeaderText="部门名称" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="list_foot">
            <span>单身:</span>
            <asp:GridView
                ID="GridView_line"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                runat="server"
                EmptyDataText="Data Is Empty"
                AutoGenerateColumns="False">
                <Columns>     
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />
                    <asp:BoundField DataField="operation_seq_num_name" HeaderText="制程代号" />
                    <asp:BoundField DataField="out_subinventory_name" HeaderText="调出库别" />
                    <asp:BoundField DataField="out_frame_key" HeaderText="调出料架" />
                    <asp:BoundField DataField="in_subinventory_name" HeaderText="调入库别" />
                    <asp:BoundField DataField="in_frame_key" HeaderText="调入料架" />
                    <asp:BoundField DataField="required_qty" HeaderText="需求量" />
                    <asp:BoundField DataField="exchanged_qty" HeaderText="调拨数量" />
                    <asp:BoundField DataField="create_time" HeaderText="创建时间" />
                    <asp:BoundField DataField="create_man" HeaderText="创建人" />
                    <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                    <asp:BoundField DataField="update_man" HeaderText="更新人" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script src="JavaScript/PrintJS.js"></script>
</asp:Content>
