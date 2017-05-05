<%@ Page Title="workOrderSetting" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="workOrderSetting.aspx.cs" Inherits="WMS_v1._0.Web.workOrderSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/workOrderSetting.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style"  type="button" runat="server" onserverclick="CleanAllMeassage">
                   <%-- <img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style"  type="button" id="btn_insert">
                <%--    <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>料号</label>
                        </td>
                        <td class="tdInput">
                            <select id="Part_No_1" name="Part_No_1">
                              <option value="">ALL</option>
                                </select>

                        </td>
                        <td class="tdLab">
                            <label>是否完成</label>

                        </td>
                        <td class="tdInput">
                            <select runat="server" id="Status_1">
                                <option value="">--</option>
                                <option value="0">0</option>
                                <option value="1">1</option>
                            </select>

                        </td>

                        <td class="tdLab">
                            <label>工单号</label></td>
                        <td class="tdInput">
                            <select id="Wo_No_1" name="Wo_No_1">
                              <option value="">ALL</option>
                                </select>
                        </td>

                    </tr>
                    <tr>
                        <td class="tdLab">
                            <label>工单对应数量</label></td>
                        <td class="tdInput">
                            <input type="text" runat="server" id="Target_Qty_1" placeholder="请输入数字" />
                        </td>

                        <td class="tdLab">
                            <button id="Button1" class="selectBtn" type="button" runat="server" onserverclick="Select">查询</button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot">
            <asp:Repeater ID="workOrder" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th>自增K值</th>
                            <th>工单号</th>
                            <th>料号</th>
                            <th>是否完成</th>
                            <th>工单对应数量</th>
                            <th>入库数量</th>
                            <th>出货数量</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("WO_KEY") %></td>
                        <td><%#Eval("WO_NO") %></td>
                        <td><%#Eval("PART_NO") %></td>
                        <td><%#Eval("STATUS") %></td>
                        <td><%#Eval("TARGET_QTY") %></td>
                        <td><%#Eval("TURNIN_QTY") %></td>
                        <td><%#Eval("SHIPPED_QTY") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_TIME") %></td>
                        <td>
                            <button id="btn_update" class="btn_update" runat="server" type="button">更新</button></td>
                        <td>
                            <button id="btn_delete" class="btn_delete" runat="server" type="button">删除</button></td>
                        <td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader">
                <label>添加</label>
            </div>
            <div class="toggerBody">
                 <div>
                    <label>工单号:</label>
                    <input class="input_text" type="text" runat="server" placeholder="请输入字符" id="Wo_no" />
                </div>
               <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <label>料号:</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div>
                            <label>版本号:</label>
                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <label>是否完成:</label>
                    <select class="select_text" runat="server" id="Status">
                        <option value="0">0</option>
                        <option value="1">1</option>
                    </select>
                </div>
                <div>
                    <label>工单对应数量:</label>
                    <input class="input_text" type="text" runat="server" placeholder="请输入数字" id="Target_qty" />
                </div>

            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button2" runat="server" CssClass="btn_close" Text="确定" OnClick="Insert" />
                <asp:Button ID="Button5" runat="server" CssClass="btn_close" Text="取消" OnClick="CleanInsertMessage" />
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>工单号:</label>
                    <input class="input_text" id="Wo_no_update" type="text" readonly="readonly" runat="server" />
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <label>料号:</label>
                            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div>
                            <label>版本号:</label>
                            <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <label>是否完成:</label>
                    <select class="select_text" id="Status_update" runat="server">
                        <option value="0">0</option>
                        <option value="1">1</option>
                    </select>
                </div>
                <div>
                    <label>工单对应数量:</label>
                    <input class="input_text" type="text" id="Target_qty_update" runat="server" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button3" runat="server" OnClick="update" Text="确定" />
                <asp:Button ID="Button6" runat="server" OnClick="CleanUpdateMessage" Text="取消" />
            </div>
        </div>


        <div id="div_togger3">
            <div class="toggerHeader">
                <label>是否确定删除该工单信息？</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>工单号:</label>
                    <input id="Wo_no_Delet" class="input_text" runat="server" readonly="readonly" type="text" />
                </div>
            </div>
            <div class="toggerFooter">
                <asp:Button ID="Button4" runat="server" OnClick="delete" Text="确定" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/workOrderSetting.js"></script>
</asp:Content>
