<%@ Page Title="ModifyPassword" Language="C#" AutoEventWireup="true" CodeBehind="ModifyPassword.aspx.cs" Inherits="WMS_v1._0.Web.ModifyPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1,scalable=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/mpstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <h2>WMS LOGIN</h2>
            <div class="login-top">
                <h1>修改密码</h1>
                <table id="Table1" class="tablestyle1" runat="server">
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">帐号</label>
                        </td>
                        <td class="td2">
                            <input id="username" runat="server" placeholder="请输入帐号" class="inputStyle" maxlength="20" />
                        </td>
                        <td class="td3">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="username" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">原密码</label>
                        </td>
                        <td class="td2">
                            <input id="password" class="inputStyle" runat="server" placeholder="请输入原密码" maxlength="16" type="password" />
                        </td>

                    </tr>
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">新密码</label>
                        </td>
                        <td class="td2">
                            <input id="newpassword1" class="inputStyle" runat="server" placeholder="请输入新密码" type="password" />
                        </td>
                        <td class="td3">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="newpassword1" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">确认</label>
                        </td>
                        <td class="td2">
                            <input id="newpassword2" class="inputStyle" runat="server" placeholder="请再次输入新密码" maxlength="16" type="password" />
                        </td>
                        <td class="td3">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="newpassword1" ControlToValidate="newpassword2" Display="Dynamic" ErrorMessage="密码不一致" ForeColor="Red"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="newpassword2" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                </table>
                <div class="forgot">
                    <asp:Button CssClass="textstyle buttonstyle" ID="cancel" Text="返回" runat="server" OnClick="cancel_Click" CausesValidation="False" />
                    <asp:Button CssClass="textstyle buttonstyle" ID="submit" Text="确定" runat="server" OnClick="submit_Click" />
                </div>
            </div>
            <div class="login-bottom">
            </div>
        </div>

        <div class="copyright">
            <p>Copyright &copy; GYM @2016/9/28</p>
        </div>

    </form>


</body>
</html>
