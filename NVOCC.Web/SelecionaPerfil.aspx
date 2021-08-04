<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SelecionaPerfil.aspx.vb" Inherits="NVOCC.Web.SelecionaPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary"  >
                <div class="panel-heading">
                    <h3 class="panel-title">SELECIONAR PERFIL
                    </h3>
                </div>
                <div class="panel-body">
    <asp:RadioButtonList ID="rdTipoUsuario" runat="server" DataSourceID="dsTipoUsuario" DataTextField="NM_TIPO_USUARIO" DataValueField="ID_TIPO_USUARIO">
       
    </asp:RadioButtonList>
                     <div class="col-sm-3 pull-right">
                            <div class="form-group">
<asp:Button runat="server" ID="btnAcessar" Text="Acessar" CssClass="btn btn-primary btn-block" />
                                </div>
                         </div>
                    </div>
                </div>
            </div>
         </div>
    <asp:SqlDataSource ID="dsTipoUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>" SelectCommand="SELECT C.NM_TIPO_USUARIO,A.ID_TIPO_USUARIO FROM TB_VINCULO_USUARIO A 
LEFT JOIN TB_TIPO_USUARIO C ON C.ID_TIPO_USUARIO = A.ID_TIPO_USUARIO
WHERE A.ID_USUARIO = @ID_USUARIO AND ID_PARCEIRO =  @ID_EMPRESA">

         <SelectParameters>
            <asp:SessionParameter Name="ID_USUARIO" SessionField="ID_USUARIO" Type="Int32" />
            <asp:SessionParameter Name="ID_EMPRESA" SessionField="ID_EMPRESA" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
