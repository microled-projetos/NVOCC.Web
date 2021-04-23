<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registrar.aspx.vb" Inherits="NVOCC.Web.Registrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/signup.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css">

    <!-- Google Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css'>
    <title>Registrar</title>
</head>
<body>
     <style>

    .btn-primary {
    background: #003663 !important;
    border-color: #003663 !important;
        }

    </style>
    <form runat="server">

        <div class="container" style="max-height:1000px; overflow:auto;">
            <div class="row main">
                <div class="panel-heading">
                    <div class="panel-title text-center">
                        <h1 class="title"><img src="Content/imagens/FCA.png" /></h1>
                    </div>
                </div>
                <div class="table-responsive main-login main-center-registro" >                                                    
                    <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                    <div id="msgErro" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblMsg" Text="" runat="server" /></div>
                     <div class="alert alert-success" id="divmsg" runat="server" visible="false">
                                        Cadastrado realizado com sucesso!
                                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtNome" class="control-label">Nome:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-text-width"></span></span>
                                    <asp:TextBox ID="txtNome" required="true" runat="server" CssClass="form-control" placeholder="Nome completo"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtEmail" class="control-label">Email:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-comment"></span></span>
                                    <asp:TextBox ID="txtEmail" runat="server" required="true" CssClass="form-control" placeholder="E-mail"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtCPF" class="control-label">CPF:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-credit-card"></span></span>
                                    <asp:TextBox ID="txtCPF" runat="server" required="true" CssClass="form-control cpf" placeholder="___.___.___-__"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtLogin" class="control-label">Login:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtLogin" runat="server" required="true" CssClass="form-control senha" placeholder="Nome de usuário"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSenha" class="control-label">Senha:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                    <asp:TextBox ID="txtSenha" runat="server" required="true" CssClass="form-control senha" placeholder="Senha" TextMode="Password" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSenhaConfirmada" class="control-label">Confirmar Senha:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                    <asp:TextBox ID="txtSenhaConfirmada" required="true" runat="server" CssClass="form-control senha" placeholder="Senha" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-lg btn-block registrar-button" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="login-registro">
                                <a href="Login.aspx">Já possui cadastro? clique aqui e faça o login!</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </form>

    <script src="Content/js/jquery.min.js"></script>
    <script src="Content/js/bootstrap.min.js"></script>
    <script src="Content/js/jquery.mask.min.js"></script>
    <script src="Content/js/site.js?id=1234"></script>

</body>
</html>