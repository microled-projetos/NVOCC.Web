<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RecuperarSenha.aspx.vb" Inherits="NVOCC.Web.RecuperarSenha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/signup.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css">

    <!-- Google Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css'>
   <title>Recuperar Senha</title>
</head>
<body>
     <style>

    .btn-primary {
    background: #003663 !important;
    border-color: #003663 !important;
        }

    </style>
    <form runat="server">

        <div class="container"  style="max-height:1000px; overflow:auto;">
            <div class="row main">
                <div class="panel-heading">
                    <div class="panel-title text-center">
                        <h1 class="title logo">
                            <img src="Content/imagens/FCA.png" /></h1>
                    </div>
                </div>

                <div class="main-login main-center-login">

                    <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />

                    <div ID="divInfo" class="alert alert-info" runat="server" visible="false">
                        Informe seu nome Email
                    </div>
                    <div ID="divErro" class="alert alert-danger" runat="server" visible="false">
                       Erro ao enviar email
                    </div>
                    <div ID="divsucesso" class="alert alert-success" runat="server" visible="false">
                        <asp:Label ID="lblSucessoMsg" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtUsuario" class="control-label">Email:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="txtCpfEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="btnRecuperarSenha" runat="server" Text="Recuperar Senha" CssClass="btn btn-primary btn-lg btn-block login-button"/>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="login-registro">
                                <a href="Login.aspx">voltar</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </form>

    <script src="Content/js/jquery.min.js"></script>
    <script src="Content/js/bootstrap.min.js"></script>

</body>
</html>

