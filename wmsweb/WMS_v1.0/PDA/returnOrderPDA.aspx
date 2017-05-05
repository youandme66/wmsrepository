<%@ Page Title="returnOrderPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="returnOrderPDA.aspx.cs" Inherits="WMS_v1._0.PDA.returnOrderPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/returnOrderPDA.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" type="button" id="btn_clear" runat="server" onserverclick="clear">
                    <%--  <img src="icon/clear.png" /><br />--%>
                    清空
                </button>
                <button class="btn_style" type="button" id="btn_insert" runat="server" onserverclick="num_no">
                    <%--   <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
                <input type="button" name="Submit" value="打印" class="btn_style picture_width" id="Print" runat="server" />
            </div>

            <%-- 打印按钮的弹框--%>
            <div id="zindex3">
                <div id="div_togger3">
                    <div class="toggerHeader">
                        <label>打印退料单</label>
                    </div>
                    <div class="toggerBody">
                        <div>
                            <input class="input_text" type="text" id="select_text_print" runat="server" placeholder="请输入退料单号" />
                        </div>
                    </div>
                    <div class="toggerFooter">
                        <asp:Button ID="Button2" runat="server" OnClick="transPrint" Text="确定" />
                        <asp:Button ID="btn_close" runat="server" OnClick="CleanInsertMessage" Text="取消" />
                    </div>
                </div>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>单据号</label>
                        </td>
                        <td class="tdInput">
                            <input id="invoice_no2" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>退料类型</label>
                        </td>
                        <td class="tdInput">
                            <select id="quit_type1" style="height: 26px;" runat="server">
                                <option>退料类型一</option>
                                <option>退料类型二</option>
                                <option>退料类型三</option>
                                <option>退料类型四</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>状态</label>
                        </td>
                        <td class="tdInput">
                            <select id="Enabled1" runat="server" style="height: 26px;">
                                <option>0</option>
                                <option>1</option>
                            </select>
                        </td>
                        <td class="tdLab">
                            <label>库别</label>
                        </td>
                        <td class="tdInput">
                            <asp:DropDownList ID="Subinventory1" runat="server" Height="25px" AutoPostBack="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>退料工单号</label>
                        </td>
                        <td class="tdInput">
                            <input id="return_wo_no1" type="text" runat="server" />
                        </td>
                        <td class="tdLab">
                            <label>备注</label>
                        </td>
                        <td class="tdInput">
                            <textarea id="remark1" runat="server" style="resize: none;"></textarea>
                        </td>
                        <td class="tdLab" style="margin-left: 40px;">
                            <input id="Button1" type="button" runat="server" value="提交单头" onserverclick="insert1" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <div class="selectSection">
                <label class="footLab">单据号</label>
                <input id="invoice_no4" class="footInput" type="text" runat="server" />
                <button id="select3" type="button" runat="server" onserverclick="select1" style="width: auto;">查询单头</button>
                <label class="footLab">工单号</label>
                <input id="return_wo_no4" class="footInput" type="text" runat="server" />
                <button id="select4" type="button" runat="server" onserverclick="select2" style="width: auto;">查询单身</button>
            </div>
            <asp:Repeater ID="Line_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table1" class="mGrid" border="1">
                        <tr>
                            <th >退料单头ID</th>
                            <th>退料单据号</th>
                            <th>退料类型</th>
                            <th>库别</th>
                            <th>状态</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                            <th>退料工单号</th>
                            <th>退料人员</th>
                            <th>备注</th>
                            <th>更新</th>
                            <th>删除</th>
                            <th>生成单身</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("return_header_id") %></td>
                        <td><%#Eval("INVOICE_NO") %></td>
                        <td><%#Eval("RETURN_TYPE") %></td>
                        <td><%#Eval("return_sub_name") %></td>
                        <td><%#Eval("STATUS") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                        <td><%#Eval("return_wo_no_name") %></td>
                        <td><%#Eval("RETURN_MAN") %></td>
                        <td><%#Eval("REMARK") %></td>
                        <td>
                            <button id="update1" class="btn_update1" type="button">更新</button></td>

                        <td>
                            <button id="delete1" class="btn_delete1" type="button">删除</button></td>
                        <td>
                            <button id="insert1" class="btn_insert1" type="button">生成单身</button>
                        </td>
                        <td hidden="hidden"><%#Eval("return_header_id") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>

            <div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table id="Line_Table2" class="mGrid" border="1">
                            <tr>
                                <th>退料单头ID</th>
                                <th>Line_Num</th>
                                <th>退料工单号</th>
                                <th>制程</th>
                                <th>料号</th>
                                <th>退料数量</th>
                                <th>单位</th>
                                <th>区域</th>
                                <th>库别</th>
                                <th>退料时间</th>
                                <th>修改时间</th>
                                <th hidden="hidden">退料单身</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("RETURN_HEADER_ID") %></td>
                            <td><%#Eval("LINE_NUM") %></td>
                            <td><%#Eval("RETURN_WO_NO") %></td>
                            <td><%#Eval("OPERATION_SEQ_NUM_name") %></td>
                            <td><%#Eval("ITEM_NAME") %></td>
                            <td><%#Eval("RETURN_QTY") %></td>
                            <td><%#Eval("UOM") %></td>
                            <td><%#Eval("LOCATOR") %></td>
                            <td><%#Eval("RETURN_SUB_name") %></td>
                            <td><%#Eval("RETURN_TIME") %></td>
                            <td><%#Eval("UPDATE_TIME") %></td>
                            <td hidden="hidden"><%#Eval("return_line_id") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </div>

        </div>
    </div>
    <div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader">
                <label>生成单身</label>
            </div>
            <div class="toggerBody">
                <div hidden="hidden">
                    <label>return_header_id:</label>
                    <input id="Text1" class="input_text" readonly="readonly" type="text" runat="server" />
                </div>
                <div>
                    <label>退料单据号:</label>
                    <input id="invoice_no" class="input_text" readonly="readonly" type="text" runat="server" />
                </div>
                <div>
                    <label>退料工单号:</label>
                    <input id="return_wo_no" name="return_wo_no" class="input_text" type="text" runat="server" value=""/>
                </div>
                <div>
                    <label>料号:</label>
                    <input id="item_name_Insert" class="input_text" type="text" runat="server"  placeholder="注意：该料号是对应工单下的料号"/>
                    <asp:Button ID="Button" runat="server" Text="获取料号" OnClick="getItem_nameByReturn_wo_no" />
                </div>
                <div>
                    <label>制程:</label>
                    <asp:DropDownList ID="seq_operation_num1" runat="server" Height="22px" Width="50%"></asp:DropDownList>
                </div>
                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <label>库别:</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div>
                            <label>区域:</label>
                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                            <label>注意：该区域是与上选库别相关联的</label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div>
                    <label>退料数量:</label>
                    <input id="quit_num" class="input_text" type="text" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" Text="确定" OnClick="insert2" />
                <button class="btn_close1" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>退料单据号:</label>
                    <input id="quit_invoice_no2" readonly="readonly" class="input_text" runat="server" type="text" />
                </div>
                <div>
                    <label>退料类型:</label>
                    <select id="quit_type2" class="select_text" runat="server">
                        <option>退料类型一</option>
                        <option>退料类型二</option>
                        <option>退料类型三</option>
                        <option>退料类型四</option>
                    </select>
                </div>
                <div>
                    <label>库别:</label>
                    <asp:DropDownList ID="Subinventory4" runat="server" Height="22px" Width="136px" AutoPostBack="false"></asp:DropDownList>
                </div>
                <div>
                    <label>状态:</label>
                    <select id="status2" class="select_text" name="status" runat="server">
                        <option>0</option>
                        <option>1</option>
                    </select>
                </div>
                <div>
                    <label>退料工单号:</label>
                    <input id="quit_wo_no2" class="input_text" runat="server" type="text" />
                </div>
                <div>
                    <label>备注:</label>
                    <textarea id="remark2" class="input_text" runat="server"></textarea>
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button5" runat="server" Text="确定" OnClick="update1" />
                <button class="btn_close1" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger6">
            <div class="toggerHeader">
                <label>删除</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>退料单据号:</label>
                    <input type="text" class="input_text" readonly="readonly" id="lab1" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button6" runat="server" Text="确定" OnClick="delete1" />
                <button class="btn_close1" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/returnOrder.js"></script>
    <script src="JavaScript/printMaterialReturningJS.js"></script>
</asp:Content>
