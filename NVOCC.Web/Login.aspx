<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="NVOCC.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NVOCC</title>
</head>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/signup.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css">

    <!-- Google Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css'>
    
<body>

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
                     <div id="msgErro" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblMsg" Text="" runat="server" /></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" AutoPostBack="True" Style="text-transform: none !important" MaxLength="50" placeholder="Nome de usuário" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">

                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-key fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control senha" MaxLength="128" placeholder="Senha" TextMode="Password" Style="text-transform: none !important"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="DivEmpresa" runat="server" style="display:none">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-building"></i></span>
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" DataSourceID="dsEmpresa"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-lg btn-block login-button" />
                            </div>
                        </div>
                    </div>

                    <div class="row" runat="server" >
                        <div class="col-md-12">
                            <div class="login-registro">
                                <a href="Registrar.aspx" runat="server" id="lnkCadastre_se">Cadastre-se</a> <br />
                                <a href="RecuperarSenha.aspx">Esqueci minha senha</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form> 
    
    <script src="Content/js/jquery.min.js"></script>
    <script src="Content/js/bootstrap.min.js"></script>
    <asp:SqlDataSource ID="dsEmpresa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_PARCEIRO,(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_PARCEIRO)NM_RAZAO FROM TB_VINCULO_USUARIO A WHERE A.ID_USUARIO = @ID_USUARIO AND ID_PARCEIRO IS NOT NULL union
 SELECT  0, ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
                <asp:Parameter Name="ID_USUARIO" Type="Int32"  />
            </SelectParameters>
</asp:SqlDataSource>
</body>
</html>