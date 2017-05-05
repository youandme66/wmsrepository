<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printStorageList.aspx.cs" Inherits="WMS_v1._0.Web.printStorageList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0" />
    <link href="CSS/printStorageList.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
                <div class="headerLeft">
                    <div class="link2">
                        <p class="font2">亞旭電子科技（江蘇）有限公司</p>
                    </div>
                    <%--<div class="link3">
                        <div class="l">
                            <input type="checkbox" class="font2" value="none" />半成品&nbsp;&nbsp;
		    	   	        <input type="checkbox" class="font2" value="none" />組裝 
                        </div>
                        <div>
                            <input type="checkbox" class="font2" value="none" />半成OK測試
		    	   	        <input type="checkbox" class="font2" value="none" />包裝
                        </div>
                    </div>--%>
                </div>
                <div class="headerMiddle">
                    <div class="link1">
                        <p class="font1">亞旭電子科技（江蘇）有限公司</p>
                    </div>
                    <div class="link1">
                        <p class="font1">成品（半成品）入庫單</p>
                    </div>
                </div>
                <div class="headerRight">
                    <div class="link4">
                        <span class="font2">填單日期 </span>
                        <input type="text" class="txt" value="none" id="transaction_time" runat="server"/>
                    </div>
                   <%-- <div class="link4">
                        <span class="font2">填單人員 </span>
                        <input type="text" class="txt" value="none" />
                    </div>--%>
                    <div class="link4">
                        <span class="font2">入庫員</span>
                        <input type="text" class="txt" value="none" id="transaction_create_user" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">入庫單據 </span>
                        <input type="text" class="txt" value="none" id="select_text" runat="server" />
                    </div>
                     <div class="link4">
                        <span class="font2">數量（入庫量） </span>
                        <input type="text" class="txt" value="none" id="select_text2" runat="server" />
                    </div>
                </div>

                <%--<div class="headerFoot">
                    <div class="link5">
                        <span class="font2">機種名稱 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link5">
                        <span class="font2">制令號碼 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link5">
                        <span class="font2">制程 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                </div>--%>
            </div>
            <div class="content">
                <asp:Repeater ID="printStorageListRepeater" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table" class="mGrid" border="1">
                            <tr>
                              <%--  <th>制令/訂單號碼</th>--%>
                                <th>料號</th>
                                <th>庫別</th>
                                <th>料架</th>
                                <th>Datacode</th>
                                <%--<th>數量（入庫量）</th>--%>
                               <%-- <th>備註</th>--%>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("item_name") %></td>
                            <td><%#Eval("subinventory") %></td>
                            <td><%#Eval("frame_name") %></td>
                            <td><%#Eval("datecode") %></td>
                           <%-- <td><%#Eval("transaction_qty") %></td>--%>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>

            </div>
            <div class="header">
               <%-- <div class="footTop">
                    <div class="link6">
                        <span class="font2">匯總量 </span>
                        <input type="text" class="txt" value="none" />
                        <span class="font2">板/件數 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link7">
                        <div class="dote1">
                            <span class="font2">倉庫主管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">廠庫經辦 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">品管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">申請主管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">申請經辦：</span>
                            <input type="text" class="txt" value="none" />
                        </div>
                    </div>
                </div>--%>
                <div class="footBottom">
                    <div class="link8">
                        <p class="txt">注:1.原因碼禁止領保稅倉材料（特殊狀況需經財務財務簽核）；2.原因碼領料需填寫領用部門，機種料號PU別，客戶代碼（財務簽核）</p>
                        <p class="txt txtStyle">3.原材料採購簽核，成半品生管簽核；4.工單制令生產領料無需財務簽核；5.其他需要信息可顯示在備註欄</p>
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
