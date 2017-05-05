<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printWoNo.aspx.cs" Inherits="WMS_v1._0.Web.printWoNo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <link href="CSS/printMaterialReturning.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                    <div class="link3">
                        <span class="font2">模拟單號  </span>
                        <input type="text" class="txt" value="none" id="select_text" runat="server"/>
                    </div>
                  <%--  <div class="link3">
                        <span class="font2">修改區域</span><input type="text" class="txt" value="none" />
                    </div>
                    <div class="link3">
                        <span class="font2">製造倉庫</span><input type="text" class="txt" value="none" />
                    </div>
                    <div class="link3">
                        <span class="font2">機種名稱</span><input type="text" class="txt" value="none" />
                    </div>--%>
                </div>
                <div class="headerMiddle">
                    <div class="link1">
                        <p class="font1">亞旭電子科技（江蘇）有限公司</p>
                    </div>
                    <div class="link1">
                        <p class="font1">制令備料單</p>
                    </div>
                   <%-- <div class="link1">
                        <p class="font2">產品代號</p>
                    </div>
                    <div class="link1">
                        <p class="font2">製程</p>
                    </div>--%>
                </div>
                <div class="headerRight">
                    <%--<div class="link4">
                        <span class="font2">總量 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link4">
                        <span class="font2">預留量 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link4">
                        <span class="font2">BOM版本 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link4">
                        <span class="font2">機種代碼 </span>
                        <input type="text" class="txt" value="none" />
                    </div>--%>
                </div>


            </div>
        </div>
        <div class="content">
            <asp:Repeater ID="printSinglePreparationRepeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <%--<th>項次</th>--%>
                            <th>料号</th>
                            <th>需求量</th>
                           <%-- <th>涉免3C</th>--%>
                            <%-- <th>品名/規格</th>
                            <th>機種料號</th>
                            <th>PU別</th>--%>
                            <%-- <th>客戶代碼</th>--%>
                            <th>模拟量</th>
                            <th>料存</th>
                            <th>料架</th>
                            <th>D/C</th>
                            <th>更新时间</th>
                            <%--<th>儲位</th>
                            <th>庫存</th>--%>
                          <%--  <th>需求量</th>
                            <th>扣賬量</th>
                            <th>主倉</th>
                            <th>庫別</th>--%>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("item_name") %></td>
                        <td><%#Eval("requirement_qty") %></td>
                        <td><%#Eval("simulated_qty") %></td>
                        <td><%#Eval("left_qty") %></td>
                        <td><%#Eval("frame_name") %></td>
                        <td><%#Eval("datecode") %></td>
                        <td><%#Eval("update_time") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>

</html>

