<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printTransFerringOrder.aspx.cs" Inherits="WMS_v1._0.Web.printTransFerringOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0" />
    <link href="CSS/printTransFerringOrder.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
                <div class="transfer_headerLeft">
                    <div class="link2">
                        <p class="font2">亞旭電子科技（江蘇）有限公司</p>
                    </div>                
                    <div class="link3">
                        <span class="font2">部门编号:</span>
                        <input type="text" class="txt" value="none" id="flex_value" runat="server"/>
                    </div>
                    <div class="link3">
                        <span class="font2">部门名称:</span>
                        <input type="text" class="txt" value="none" id="description" runat="server"/>
                    </div>
                </div>
                <%--<div class="transfer_headerCenter">
                    <div class="transfer_link3">
                        <p class="font2">非保稅</p>
                    </div>
                    <div class="transfer_link3">
                        <p class="font2">非保稅</p>
                    </div>
                </div>--%>
                <div class="transfer_headerMiddle">
                    <div class="link1">
                        <p class="font1">亞旭電子科技（江蘇）有限公司</p>
                    </div>
                    <div class="link1">
                        <p class="font1">調撥單</p>
                    </div>
                </div>
                <div class="transfer_headerRight">
                    <div class="link4">
                        <span class="font2">调拨单号：</span>
                        <input type="text" class="txt" value="none" id="select_text" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">填單日期：</span>
                        <input type="text" class="txt" value="none" id="create_time" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">填單人員：</span>
                        <input type="text" class="txt" value="none" id="create_man" runat="server"/>
                    </div>
             
                </div>
            </div>
            <div class="content">
                <asp:Repeater ID="printTransFerringOrderRepeater" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table" class="mGrid" border="1">
                            <tr>
                                <th>料號</th>
                                <th>製程代号</th>
                                <th>调出库别</th>
                                <th>调出料架</th>
                                <th>调入库别</th>
                                <th>调入料架</th>
                                <th>需求量</th>
                                <th>调拨量</th>
                                <th>创建时间</th>                              
                                <th>创建人员</th>
                                <th>更新时间</th>
                                <th>更新人员</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ITEM_NAME") %></td>
                            <td><%#Eval("operation_seq_num_name") %></td>
                            <td><%#Eval("out_subinventory_name") %></td>
                            <td><%#Eval("out_frame_key") %></td>
                            <td><%#Eval("in_subinventory_name") %></td>
                            <td><%#Eval("in_frame_key") %></td>
                            <td><%#Eval("required_qty") %></td>
                            <td><%#Eval("exchanged_qty") %></td>
                            <td><%#Eval("create_time") %></td>
                            <td><%#Eval("create_man") %></td>
                            <td><%#Eval("update_time") %></td>
                            <td><%#Eval("update_man") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>

            </div>
            <div class="header">
               <%-- <div class="footTop">
                    <div class="transferlink6">
                        <span class="font2">責任部門主管：</span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="returnlink6">
                        <span class="font2">匯總數量：</span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link7">
                        <div class="returndote1">
                            <span class="font2">財務： </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="returndote1">
                            <span class="font2">資材： </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="returndote1">
                            <span class="font2">調入倉庫經編:</span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="returndote1">
                            <span class="font2">調出倉庫經編:</span>
                            <input type="text" class="txt" value="none" />
                        </div>
                    </div>
                    <div class="link7">
                        <div class="returndote1">
                            <span class="font2">關務: </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="returndote1">
                            <span class="font2">品管: </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="returndote1">
                            <span class="font2">調入倉庫主管: </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="returndote1">
                            <span class="font2">調出倉庫主管： </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                    </div>
                </div>--%>
                <div class="footBottom">
                    <div class="link8">
                        <p class="txt">注1：保税与非保税會調發需要責任部門級主管發核并關務及財務簽核；2：保稅調發非保稅時間每月1號-5號，特殊</p>
                        <p class="txt txtStyle">狀況以關務通知為准；3：非質材會需品管檢驗簽核，調入不良品會需對商品管 簽核確認.（原材料IQC檢驗；半成</p>
                        <p class="txt txtStyle">（品IPOC;成品OQC檢驗）4:調入報廢會需顯示報廢單號；5：資材檔位原材料採購簽核，成半品生管簽核6：其他需要可顯示備註欄；</p>
                    </div>
                  <%--  <div class="link9">
                        <input type="text" class="txt" value="none" />
                        <input type="text" class="txt txtStyle" value="none" />
                    </div>--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
