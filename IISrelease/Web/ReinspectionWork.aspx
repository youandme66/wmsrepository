<%@ Page Title="" Language="C#" MasterPageFile="~/Web/headerFooter.Master" AutoEventWireup="true" CodeBehind="ReinspectionWork.aspx.cs" Inherits="WMS_v1._0.Web.ReinspectionWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <table class="tab">
            <tr>
                <td class="tdLab">
                    料号
                </td>
                <td class="tdInput">
                    <asp:TextBox runat="server" ID="item_name_input" />
                </td>
                <td class="tdLab">
                    datecode
                </td>
                <td class="tdInput">
                   <asp:TextBox runat="server" ID="datecode_input" />
                </td>
                <td class="tdLab">
                    库别
                </td>
                <td class="tdInput">
                    <asp:DropDownList ID="subinventory_select" runat="server" Height="25px" AutoPostBack="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdLab">
                    复验结果
                </td>
                <td class="tdInput">
                     <asp:DropDownList ID="reinspect_result_select" runat="server" Height="25px" AutoPostBack="false"></asp:DropDownList>
                </td>
                <td class="tdLab" >
                    <label>备注</label>
                </td>
                <td class="tdInput" colspan="3">
                    <textarea id="remark_input" runat="server" style="resize: none;"></textarea>
                </td>
                
            </tr>
             <tr>
                <td class="tdLab" colspan="3">
                    <asp:Button ID="commit_button" runat="server" OnClick="commit_button_Click" Text="commit"/>
                </td>
                
                <td class="tdLab" colspan="3">
                    <asp:Button ID="clear_button" runat="server" OnClick="clear_button_Click"  Text ="clear"/>
                </td>
                
            </tr>
        </table>
    </div>
</asp:Content>
