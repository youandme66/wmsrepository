<%@ Page Title="" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="CheckParameter.aspx.cs" Inherits="WMS_v1._0.Web.CheckParameter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/Po_PoLine.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button class="btn_style" id="Button1" type="button" runat="server" onserverclick="Clear">
                   <%-- <img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                 <%--   <img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>Pn_head</label>
                        </td>
                        <td class="tdInput">
                            <input type="text" id="pn_head" placeholder ="请输入字符" runat ="server"  />

                        </td>
                        <td class="tdLab">
                            <label>复验周期</label></td>
                        <td class="tdInput">
                            <input type="text" id ="reinspect_week" placeholder ="请输入字符" runat ="server"  />
                        </td>
                        <td class="tdLab">
                            <button id="Button2" class="selectBtn" type="button" runat ="server" onserverclick ="Select"  >查询</button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="list_foot" id="list_foot">
            <asp:Repeater ID="Line_Repeater" runat="server">
                <HeaderTemplate>
                    <table id="Line_Table" class="mGrid" border="1">
                        <tr>
                            <th hidden ="hidden" >Unique_id</th>
                            <th>Pn_head</th>
                            <th>复验周期</th>
                            <th>创建时间</th>
                            <th>更新时间</th>
                             <th>更新</th>
                             <th>删除</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden ="hidden" ><%#Eval("Unique_id") %></td>
                        <td><%#Eval("Pn_head") %></td>
                         <td><%#Eval("REINSPECT_WEEK") %></td>
                        <td><%#Eval("CREATE_TIME") %></td>
                         <td><%#Eval("UPDATE_TIME") %></td>
                        <td>
                            <button id="Button1" class="btn_update" runat="server" type="button">更新</button></td>
                        <td>
                            <button id="Button2" class="btn_delete" runat="server" type="button">删除</button></td>
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
                    <label>Pn_head:</label>
                    <input class="input_text" type="text" id="pn_head1" placeholder ="请输入字符" runat ="server"  />
                </div>
                
                <div>
                    <label>复验周期:</label>
                    <input class="input_text" type="text" id ="reinspect_week1" placeholder ="请输入字符" runat ="server"  />
                </div>                             
            </div>
            <div class="toggerFooter">
                <asp:Button CssClass ="btn_close" ID="btn_in" runat="server" OnClick ="Insert" Text="确定"  />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

        <div id="div_togger2">
            <div class="toggerHeader">
                <label>更新</label>
            </div>
            <div class="toggerBody">
                <div hidden ="hidden" >
                    <label>Unique_id:</label>
                    <input class="input_text" type="text" id="unique_id2" name ="unique_id2" readonly ="readonly"  />
                </div>
               <div>
                    <label>Pn_head:</label>
                    <input class="input_text" type="text" id="pn_head2" placeholder ="请输入字符" name ="pn_head2" readonly="readonly" />
                </div>
                
                <div>
                    <label>复验周期:</label>
                    <input class="input_text" type="text" id ="reinspect_week2" placeholder ="请输入字符" name ="reinspect_week2"  />
                </div>
                     
            </div>
            <div class="toggerFooter">
                <asp:Button CssClass ="btn_close" ID="bnt_up" runat="server" OnClick ="Update" Text="确定"   />
               
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

       <div id="div_togger3">
            <div class ="toggerHeader">
                <label >删除</label>
            </div>
            <div style ="text-align :center " >
                <br />
                是否删除复验参数：<label id="Lable1" ></label>
                <input type="text" id="lab" hidden="hidden" name="unique_id3" />
                <input type ="text" id="pd" hidden ="hidden" name ="Pn_head3" />
                <br />
            </div>
            <div class="toggerFooter">
                <asp:Button CssClass ="btn_close" ID="Button5" runat="server" OnClick="Delete"   Text="删除" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/Check_Parameter.js"></script>
</asp:Content>
