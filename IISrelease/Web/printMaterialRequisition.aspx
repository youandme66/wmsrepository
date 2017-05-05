<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printMaterialRequisition.aspx.cs" Inherits="WMS_v1._0.Web.printMaterialRequisition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0" />
    <link href="CSS/materialequisitionPrint.css" rel="stylesheet" />
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
                        <span class="font2" >部门编号  </span>
                        <input type="text" class="txt" value="none" id="flex_value" runat="server"/>
                    </div>
                    <div class="link3">
                        <span class="font2" >部门名称  </span>
                        <input type="text" class="txt" value="none" id="description" runat="server"/>
                    </div>
                      <div class="link4">
                        <span class="font2">备注 </span>
                        <input type="text" class="txt" value="none" id="remark" runat="server"/>
                    </div>
                </div>
                <div class="headerMiddle">
                    <div class="link1">
                        <p class="font1">亞旭電子科技（江蘇）有限公司</p>
                    </div>
                    <div class="link1">
                        <p class="font1">领料單</p>
                    </div>
                </div>
                <div class="headerRight">
                    <div class="link4">
                        <span class="font2">领料单号 </span>
                        <input type="text" class="txt" value="none" id="select_text" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">领料类型 </span>
                        <input type="text" class="txt" value="none" id="issue_type" runat="server"/>
                    </div>
                    <div class="link4">
                        <span class="font2">是否完成领料 </span>
                        <input type="text" class="txt" value="none" id="status" runat="server"/>
                    </div>
                  
               
                </div>

                <div class="headerFoot">
                   <%-- <div class="link5">
                        <span class="font2">機種名稱 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link5">
                        <span class="font2">制令號碼 </span>
                        <input type="text" class="txt" value="none" />
                    </div>--%>
                   <%-- <div class="link5">
                        <span class="font2">制程 </span>
                        <input type="text" class="txt" value="none" id="header_operation_seq_num" runat="server"/>
                    </div>--%>
                </div>
            </div>
            <div class="content">
                <asp:Repeater ID="printMaterialRequisitionRepeater" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table" class="mGrid" border="1">
                            <tr>
                                <th>Line_num</th> 
                                <th>领料工单号</th>
                                <th>制程代号</th>
                                <th>料號</th>
                                <th>庫別</th>                             
                                <th>料架</th>
                                <th>需求量</th>
                                <th>发料量</th>
                                <th>領料量</th>
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
                            <td><%#Eval("wo_no") %></td>
                            <td><%#Eval("operation_seq_num_name") %></td>
                            <td><%#Eval("ITEM_NAME") %></td>
                            <td><%#Eval("issued_sub") %></td>
                            <td><%#Eval("FRAME_KEY") %></td>
                            <td><%#Eval("REQUIRED_QTY") %></td>
                            <td><%#Eval("SIMULATED_QTY") %></td>
                            <td><%#Eval("ISSUED_QTY") %></td>
                            <td><%#Eval("status") %></td>
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
                   <%-- <div class="link6">
                        <span class="font2">產總量 </span>
                        <input type="text" class="txt" value="none" />
                        <span class="font2">板/件數 </span>
                        <input type="text" class="txt" value="none" />
                    </div>
                    <div class="link7">
                        <div class="dote1">
                            <span class="font2">資材 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">廠庫經辦 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">領用經辦 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                    </div>
                    <div class="link7">
                        <div class="dote1">
                            <span class="font2">財務 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">廠庫主管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                        <div class="dote1">
                            <span class="font2">領用主管 </span>
                            <input type="text" class="txt" value="none" />
                        </div>
                    </div>--%>
                </div>
                <div class="footBottom">
                    <div class="link8">
                        <%--<p class="txt">注:1.原因碼禁止領保稅倉材料（特殊狀況需經財務財務簽核）；2.原因碼領料需填寫領用部門，機種料號PU別，客戶代碼（財務簽核）</p>--%>
                        <p class="txt txtStyle">注:1.原材料採購簽核，成半品生管簽核；2.工單制令生產領料無需財務簽核；3.其他需要信息可顯示在備註欄</p>
                    </div>
                   <%-- <div class="link9">
                        <input type="text" class="txt" value="none" />
                        <input type="text" class="txt txtStyle" value="none" />
                    </div>--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
