<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WMS_v1._0.Web.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maxinum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/register.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <h2>WMS REGISTER</h2>
            <div class="login-top">
                <h1>注&nbsp;册</h1>
                <table id="Table1" class="tablestyle1" runat="server">
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">用户名</label>
                        </td>
                        <td class="td2">
                            <input runat="server" class="inputStyle" placeholder="请输入用户名" id="user_name1" maxlength="20" />
                        </td>
                        <td class="td3">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Style="color: red" ControlToValidate="user_name1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">密码</label>
                        </td>
                        <td class="td2">
                            <input runat="server" placeholder="请输入密码" class="inputStyle" id="password" maxlength="20" />
                        </td>

                    </tr>
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">部门</label>
                        </td>
                        <td class="td2">
                            <input runat="server" placeholder="请输入部门" class="inputStyle" id="department" maxlength="20" />
                        </td>
                    </tr>
                </table>
                <div class="forgot">
                    <asp:Button ID="Button1" class="buttonstyle " runat="server" Text="注册" OnClick="Button1_Click" />


                    <asp:Button ID="Button2" class="buttonstyle " runat="server" Text="取消" CausesValidation="False" OnClick="Button2_Click" />

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
