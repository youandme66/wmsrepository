<%@ Page Title="交易明细查询" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="DealDetial.aspx.cs" Inherits="WMS_v1._0.Web.DealDetial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/DealDetial.css" rel="stylesheet" type="text/css" />
    <link  type="text/css" rel="stylesheet" href="../stylesheets/normalize.css"/>
    <link  type="text/css" rel="stylesheet" href="../stylesheets/stylesheet.css"/>
    <link  type="text/css" rel="stylesheet" href="../stylesheets/github-light.css"/>
    <link  type="text/css" rel="stylesheet" href="../jedate/skin/jedate.css"/>
    <script type="text/javascript" src="../jedate/jedate.js"></script>

    <div class="list">
        <div class="list_top">
            <div class="list_item">
            <fieldset>
               <legend>交易类别</legend>
               <input  type="radio" id="radio1" name="transaction_type"  value=""  runat="server" checked=""/>   <label >全部</label>
               <input type ="radio" id="radio8" name="transaction_type"  value="PoSuspense"  runat="server" />   <label >PO暂收</label>
               <input type ="radio" id="radio2" name="transaction_type"  value="PoInStorage" runat="server" />    <label >PO入库</label>
               <input type ="radio" id="radio3" name="transaction_type"  value="Issue_Debit" runat="server" />  <label >领料单扣账</label>
               <input type ="radio" id="radio4" name="transaction_type"  value="Return_Debit"    runat="server" />     <label >退料单扣账</label>
               <input type ="radio" id="radio5" name="transaction_type"  value="Exchange_Debit"  runat="server" />   <label >调拨单扣账</label>
              <%-- <input type ="radio" id="radio6" name="transaction_type"  value="PoReturn"    runat="server" />     <label >PO退收</label>
               <input type ="radio" id="radio7" name="transaction_type"  value="Issue"  runat="server" /><label >发料</label>   --%>  
            </fieldset>
            </div>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td>
                            <label>Form</label>
                        </td>
                        <td colspan="2" class="datep">
                            <input class="datainp" id="inpend" type="text" placeholder="开始日期" readonly="readonly"  runat="server"/>
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
                            <label>料号</label>
                        </td>
                        <td colspan="2">
                            <input id="item_name" type="text" runat="server" />
                        </td>
                        <td>
                            <label>单据号</label>
                        </td>
                        <td colspan="2">
                            <input id="invoice_no" type="text" runat="server"   />
                        </td>
                        <td>
                            <label>Po_no</label>
                        </td>
                        <td colspan="2">
                            <input id="po_no" type="text" runat="server"   />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>To</label>
                        </td>
                        <td colspan="2" class="datep">
                            <input class="datainp" id="inpstart" type="text" placeholder="结束日期" readonly="readonly"  runat="server"/>
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
            <asp:GridView
                ID="GridView1"
                CssClass="mGrid" 
                PagerStyle-CssClass="pgr"
                 AlternatingRowStyle-CssClass="alt"
                 runat="server" 
                EmptyDataText="Data Is Empty"
                 AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="INVOICE_NO" HeaderText="单据号" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />
                    <asp:BoundField DataField="PO_NO" HeaderText="Po_no" />
                    <asp:BoundField DataField="transaction_type" HeaderText="交易类型" />
                    <asp:BoundField DataField="transaction_qty" HeaderText="交易数量" />
                    <asp:BoundField DataField="transaction_time" HeaderText="交易时间" />
                    <asp:BoundField DataField="create_user" HeaderText="交易人员" />
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

