<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WMS_v1._0.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1,scalable=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/login.css" />

</head>
<body>
    <form id="Form1" runat="server">
        <div class="login">
            <h2>WMS LOGIN</h2>
            <div class="login-top">
                <h1>登&nbsp;录</h1>
                <table id="Table1" class="tablestyle1" runat="server">
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">帐号</label>
                        </td>
                        <td class="td2">
                            <input id="username" runat="server" placeholder="请输入帐号" class="inputStyle" maxlength="20" />

                        </td>
                        <td class="td3">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="username" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="rowstyle">
                        <td class="td1">
                            <label class="textstyle">密码</label>
                        </td>
                        <td class="td2">
                         
                            <input id="password" runat="server" placeholder="请输入密码" class="inputStyle" maxlength="20" type="password" />
                        </td>
                    </tr>
                </table>
                <div class="forgot">
                    <a class="linkstyle" href="ModifyPassword.aspx">修改密码</a>


                    <asp:Button CssClass="textstyle" ID="login" runat="server" OnClick="login_Click" Text="登录" />

                </div>
            </div>
            <div class="login-bottom">
                <h3>新用户 &nbsp;<a href="Register.aspx">注册</a></h3>
            </div>
         </div>
            <div class="copyright">
                <p>Copyright &copy; zyw @2017/3/19   15:26</p>
            </div>
    </form>
  
  
</body>
</html>
