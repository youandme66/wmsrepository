<%@ Page Title="worksheetIssuePDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="worksheetIssuePDA.aspx.cs" Inherits="WMS_v1._0.PDA.worksheetIssuePDA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
 <%--   <div class="list">
        <div class="list_top">
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td>
                            <label>备料单号</label>
                        </td>
                        <td>
                            <input type="text" placeholder="请输入备料单号" id="simulate_id" runat="server" />
                        </td>
                        <td style="text-align: center;">
                            <button type="button" id="Button1" runat="server" onserverclick="Select">查询</button>
                        </td>
                        
                        <td>
                            <label>确认单号</label>
                        </td>
                        <td>
                            <input type="text" placeholder="请输入备料单号" id="simulate" runat="server" />

                        </td>
                        <td style="text-align: center;">
                            <button type="button" id="Button2" runat="server" onserverclick="Store_issue">确认发料</button>
                        </td>
                    </tr>
                    </table>
                </div>
            </div>


        <div class="list_foot">
            <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" runat="server" EmptyDataText="Data Is Empty" AutoGenerateColumns="False"
                AllowPaging="true"
                PageSize="6"
                OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="SIMULATE_ID" HeaderText="备料单号" />
                    <asp:BoundField DataField="WO_NO" HeaderText="工单号" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="料号" />
                    <asp:BoundField DataField="ONHAND_QUANTIY" HeaderText="在手总量" />
                    <asp:BoundField DataField="SUBINVENTORY" HeaderText="库别" />
                    <asp:BoundField DataField="FRAME_KEY" HeaderText="料架" />
                    <asp:BoundField DataField="ONHAND_QTY" HeaderText="在手量" />
                    <asp:BoundField DataField="STATUS" HeaderText="状态" />
                    <asp:BoundField DataField="requirement_qty" HeaderText="需求量" />
                    <asp:BoundField DataField="PICKUP_QTY" HeaderText="备料数量" />
                    <asp:BoundField DataField="ISSUED_QTY" HeaderText="发料数量" />
                    <asp:BoundField DataField="issued_status" HeaderText="发料状态" />
                </Columns>
            </asp:GridView>
        </div>
        </div>--%>



    <div class="list">
        <div class="list_top">
            <%--<div class="list_top_picture">
                <button type="button" id="btn_clear" runat="server" onserverclick="Clear">
                    <img src="icon/clear.png" /><br />
                    清除
                </button>
                <button type="button" id="btn_insert">
                    <img src="icon/insert.png" />
                    <br />
                    插入
                </button>
            </div>--%>
            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>备料单号</label>
                        </td>
                        <td class="tdInput">
                            <input id="simulate_id" type="text" runat="server"  placeholder="请输入备料单号"/>
                        </td>
                        <td></td>
                       
                        <td class="tdLab" style="text-align: center;">
                            <asp:Button ID="Button3" runat="server" Text="查询"  OnClick="Select"/>
                        </td>
                        <td class="tdLab" style="text-align:center">
                            <asp:Button ID="Button4" runat="server" Text="发料" OnClick="Button4_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot" id="list_foot">
            <asp:Repeater ID="Work_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden="hidden">备料ID</th>
                            <th>备料号</th>
                            <th hidden="hidden">料号ID</th>
                            <th>料号</th>
                            <th>料架</th>
                            <th>备料量</th>
                            <th>发料量</th>
                            <th>datecode</th>
                            <th>制程</th>
                            <th>发料状态</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden="hidden"><%#Eval("SIMULATE_ID")%></td>
                        <td><%#Eval("SIMULATE_LINE_ID")%></td>
                        <td hidden="hidden"><%#Eval("ITEM_ID")%></td>
                        <td><%#Eval("ITEM_NAME")%></td>
                        <td><%#Eval("FRAME_NAME")%></td>
                        <td><%#Eval("PICKUP_QTY")%></td>
                        <td><%#Eval("ISSUED_QTY")%></td>
                        <td><%#Eval("DATECODE")%></td>
                        <td><%#Eval("OPERATION_SEQ_NUM")%></td>
                        <td><%#Eval("ISSUED_STATUS")%></td>
                        <td><%#Eval("CREATE_TIME")%></td>
                        <td><%#Eval("UPDATE_TIME")%></td>
                        <td>
                            <button id="Fitem" class="btn_update" runat="server" style="display:none" type="button">发料</button></td>
                        
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>

    </div>

    <div id="zindex">
        <div id="div_togger1">
            <div class="toggerHeader" >
                <label>确认发料</label>
            </div>
            <div class="toggerBody">
                <div>
                    <label>备料单号</label>
                    <input id="simulate" class="input_text" type="text" runat="server"   readonly="readonly"/>
                </div>
                <div style="display:none">
                    <label>料号ID</label>
                    <input id="item_id1" class="input_text" type="text" runat="server"   readonly="readonly"/>
                </div>
                <div>
                    <label>发料料号</label>
                    <input id="item_name" class="input_text" type="text" runat="server"   readonly="readonly"/>
                </div>
                <div>
                    <label>发料数量</label>
                    <input id="issued_qty" class="input_text" type="text" runat="server" />
                </div>

            </div>

            <div class="toggerFooter">
                <asp:Button ID="Insert" runat="server" OnClick="Store_issue" Text="确定" />
               <%-- <asp:Button ID="Button1" runat="server" OnClick="Inclear" Text="取消" />--%>
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/worksheetIssue.js"></script>
</asp:Content>
