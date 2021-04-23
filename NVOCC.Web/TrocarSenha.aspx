<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TrocarSenha.aspx.vb" Inherits="NVOCC.Web.TrocarSenha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <div class="row principal">
        <div class="col-md-4 col-md-offset-4" style="margin-top: 100px;">

            <center><h2>Redefina sua Senha</h2></center>

            <div class="main-login main-center-login">
                 <div class="row">
                  <div class="alert alert-success" ID="divmsg" runat="server" visible="false">
                          Senha alterada com sucesso!
                   </div>
                 </div>
                <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtSenha" class="control-label">Senha:</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-key fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtNovaSenha" runat="server" required="true" CssClass="form-control senha" TextMode="Password" placeholder="Nova Senha"  ></asp:TextBox>
                                <%--TextMode="Password" --%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 20px;">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Button ID="btnTrocarSenha" runat="server" Text="Trocar Senha" CssClass="btn btn-primary btn-lg btn-block login-button"  />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
