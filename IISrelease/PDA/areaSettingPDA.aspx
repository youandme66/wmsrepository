<%@ Page Title="areaSettingPDA" Language="C#" MasterPageFile="~/PDA/headerFooter.Master" AutoEventWireup="true" CodeBehind="areaSettingPDA.aspx.cs" Inherits="WMS_v1._0.PDA.areaSettingPDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list_top">
            <div class="list_top_picture">
                <button id="Button1" class="btn_style" type="button " runat="server" onserverclick="Clear">
                    <%--<img src="icon/clear.png" /><br />--%>
                    清除
                </button>
                <button class="btn_style" type="button" id="btn_insert">
                    <%--<img src="icon/insert.png" /><br />--%>
                    插入
                </button>
            </div>

            <div class="list_item">
                <table class="tab">
                    <tr>
                        <td class="tdLab">
                            <label>区域名</label>

                        </td>
                        <td class="tdInput">
                            <input type="text" id="region_name" placeholder ="请输入字符" runat ="server"  />

                        </td>
                        <td class="tdLab">
                            <label>是否可用</label>

                        </td>
                        <td class="tdInput">
                            <select class="selectInput" id="enabled" runat ="server" >
                                <option >ALL</option>
                                <option>Y</option>
                                <option>N</option>                              
                            </select>

                        </td>
                        <td class="tdLab">
                            <label>库别名</label></td>
                        <td class="tdInput">
                            <select class="selectInput" id ="subinventory_name" name="subinventory_name"  >
                                <option >ALL</option>
                            </select>                         
                        </td>
                        <td class="tdLab">
                            <label>创建者</label></td>
                        <td class="tdInput">
                            <input type="text" id="create_by" placeholder ="请输入字符" runat ="server"  />
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
                            <th hidden ="hidden" >区域</th>
                            <th>区域名</th>
                            <th>库别名</th>
                            <th>创建者</th>
                            <th>创建时间</th>
                            <th>更改者</th>
                            <th>更改时间</th>
                            <th>是否可用</th>
                            <th>描述</th>
                            <th>更新</th>
                            <th>删除</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td hidden ="hidden" ><%#Eval("REGION_KEY") %></td>
                        <td><%#Eval("REGION_NAME") %></td>
                        <td><%#Eval("subinventory_name") %></td>
                        <td><%#Eval("CREATE_BY") %></td>
                         <td><%#Eval("CREATE_TIME") %></td>
                        <td><%#Eval("UPDATE_BY") %></td>
                         <td><%#Eval("UPDATE_TIME") %></td>
                         <td><%#Eval("ENABLED") %></td>
                        <td><%#Eval("DESCRIPTION") %></td>
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
                    <label>区域名:</label>
                    <input class="input_text" type="text" id="region_name1" placeholder ="请输入字符" runat ="server"  />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select class="select_text" id="enabled1" runat ="server" >
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <div>
                    <label>库别名:</label>
                    <select class ="select_text" id ="subinventory_name1" name ="subinventory_name1" >
                        <option >-----请选择库别-----</option>
                    </select>                   
                </div>                
                <div>
                    <label>描述:</label>
                    <textarea class="input_text" id="description1" placeholder ="请输入字符" runat ="server" ></textarea>
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
                    <label>区域:</label>
                    <input class="input_text" type="text" id="region_key2" name="region_key2" readonly ="readonly"   />
                </div>
                <div>
                    <label>区域名:</label>
                    <input class="input_text" type="text" id="region_name2"  name="region_name2"  readonly ="readonly"/>
                </div>
                <div >
                    <label>库别名:</label>
                    <select class ="select_text" id ="subinventory_name2" name ="subinventory_name2" >
                    </select>
                 </div>
                <div hidden ="hidden" >
                    <label>创建者:</label>
                    <input class="input_text" type="text" id ="create_by2" readonly ="readonly" />
                </div>
                <div hidden ="hidden" >
                    <label>创建时间:</label>
                    <input class="input_text" type="text" id ="create_time2" readonly ="readonly" />
                </div>
                <div>
                    <label>是否可用:</label>
                    <select class="select_text" id="enabled2" name ="enabled2"  >
                        <option>Y</option>
                        <option>N</option>
                    </select>
                </div>
                <div>
                    <label>描述:</label>
                    <textarea class="input_text" id ="description2" name="description2" ></textarea>
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
                是否删除区域：<label id="Lable1" ></label>
                <input type="text" id="lab" hidden="hidden" name="region_key3" />
                <input type ="text" id="region_name3" hidden ="hidden"  name="region_name3" />
                <br />
            </div>
            <div class="toggerFooter">
                <asp:Button CssClass ="btn_close" ID="Button5" runat="server" OnClick="Delete"   Text="删除" />
                <button class="btn_close" type="button">取消</button>
            </div>
        </div>

    </div>
    <script src="JavaScript/jquery-2.2.4.min.js"></script>
    <script src="JavaScript/areaSettingJS.js"></script>
</asp:Content>