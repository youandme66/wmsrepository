<%@ Page Title="退料单打印" Language="C#" AutoEventWireup="true" CodeBehind="printMaterialReturning.aspx.cs" Inherits="WMS_v1._0.Web.printMaterialReturning" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <span class="font2">部门编号  </span>
                        <input type="text" class="txt" value="none" id="flex_value" runat="server"/>
                    </div>
                    <div class="link3">
                        <span class="font2">部门名称  </span>
                        <input type="text" class="txt" value="none" id="description" runat="server"/>
                    </div>
                    <div class="link3">
                        <span class="font2">备注  </span>
                        <input type="text" class="txt" value="none" id="remark" runat="server"/>
                    </div>
                </div>
                <div class="headerMiddle">
                    <div class="link1">
                        <p class="font1">亞旭電子科技（江蘇）有限公司</p>
                    </div>
                    <div class="link1">
                        <p class="font1">退料單</p>
                    </div>
                </div>
                <div class="headerRight">
                    <div class="link4">
                        <span class="font2">退料单号 </span>
                        <input type="text" class="txt" value="none" id="select_text" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">退料类型 </span>
                        <input type="text" class="txt" value="none" id="return_type" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">退料是否已完成 </span>
                        <input type="text" class="txt" value="none" id="status" runat="server"/>
                    </div>
                 
                </div>

                <div class="headerFoot">
                </div>
            </div>
        </div>
        <div class="content">
             <asp:Repeater ID="printMaterialReturningRepeater" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table" class="mGrid" border="1">
                            <tr>
                                <th>Line_num</th> 
                                <th>退料工单号</th>
                                <th>制程代号</th>
                                <th>料號</th>
                                <th>庫別</th>                             
                                <th>料架</th>                         
                                <th>退料量</th>
                                <th>是否完成扣账</th>
                                <th>创建时间</th>
                                <th>创建人员</th>                              
                                <th>更新时间</th>
                                <th>更新人员</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("line_num") %></td>
                            <td><%#Eval("return_wo_no") %></td>
                            <td><%#Eval("operation_seq_num_name") %></td>
                            <td><%#Eval("ITEM_NAME") %></td>
                            <td><%#Eval("return_sub_name") %></td>
                            <td><%#Eval("frame_key") %></td>                         
                            <td><%#Eval("return_qty") %></td>
                            <td><%#Eval("flag") %></td>
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
            <div class="footTop">
              <%--  <div class="link6">
                    <span class="font2">匯總數量 </span>
                    <input type="text" class="txt" value="none" />
                </div>
                <div class="link7">
                    <div class="dote1">
                        <span class="font2">財務 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="dote1">
                        <span class="font2">資材 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="dote1">
                        <span class="font2">退入經辦 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="dote1">
                        <span class="font2">申請經辦 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link7">
                        <div class="dote1">
                            <span class="font2">關務 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">品管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">退入主管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">申請主管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                    </div>--%>
                    <div class="footBottom">
                        <div class="link8">
                          <%--  <p class="txt">注:1.退入資材良品倉需經品保檢驗（原材料IQC檢驗，半成品IPQC檢驗，成品OQC檢驗）；2.原因碼退料需填寫PU；3.原因碼退料</p>--%>
                            <p class="txt txtStyle">
                                注：1.原因碼退料需經關務，財務簽核；2.原材料採購簽核，半成品生管簽核；3.其他需要信息可顯示備註欄
                            </p>
                        </div>
                       <%-- <div class="link9">
                            <input type="text" class="txt" value="none" />
                            <input type="text" class="txt txtStyle" value="none" />
                        </div>--%>
                    </div>
                </div>
            </div>
       <%-- </div>--%>
    </form>
</body>

</html>
