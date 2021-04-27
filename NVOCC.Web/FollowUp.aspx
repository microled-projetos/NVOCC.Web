<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FollowUp.aspx.vb" Inherits="NVOCC.Web.FollowUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row no-gutter">
                                <div class="col-sm-12">

                                    <div id="smartwizard">
                                        <ul>
                                            <li><a href="#step-1">Passo 1<br />
                                                <small>Importação</small></a></li>
                                            <li><a href="#step-2">Passo 2<br />
                                                <small>Informações Iniciais</small></a></li>
                                            <li><a href="#step-3">Passo 3<br />
                                                <small>Local de Despacho</small></a></li>
                                            <li><a href="#step-4">Passo 4<br />
                                                <small>Local de Embarque</small></a></li>
                                            <li><a href="#step-5">Passo 5<br />
                                                <small>Observações</small></a></li>
                                            <li><a href="#step-6">Passo 6<br />
                                                <small>Lançamento dos Itens</small></a></li>
                                        </ul>

                                        <div>
                                            </div>
                                        </div>
                                    </div>
         </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script src="Content/js/jquery.smartWizard.js"></script>
    <script src="Content/js/select2.min.js"></script>
</asp:Content>
