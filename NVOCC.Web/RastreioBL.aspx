<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RastreioBL.aspx.vb" Inherits="NVOCC.Web.RastreioBL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--    COLOCAR SEU CODIGO AQUI--%>

    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h4>NUMERO DA BL:
                    <asp:Label runat="server" ID="nr_bl"></asp:Label></h4>
            </div>
        </div>
    </div>
    <!--TRANSPORTE E LOGISTTICA -->
    <div class="col-lg-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>Transporte/Logistica</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <h5>Pais Procedencia:<asp:Label runat="server" Id="pais_procedencia"></asp:Label></h5>
                        <p class="card-text">Porto Embarque:</p>
                        <p class="card-text">Porto Origem:  </p>
                    </div>
                    <div class="col-lg-6">
                        <h5>Pais Destino: </h5>
                        <p class="card-text">Porto Descarga:</p>
                        <p class="card-text">Porto Porto Destino:  </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--ADUANA -->
    <div class="col-lg-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>PRINCIPAL</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <article>
                            <p>Status:</p>
                            <p>Bl:</p>
                            <p>Consignatario Informado:</p>
                            <p>Fluxo:</p>
                            <p>Navio:</p>
                            <p>Imo:</p>
                            <p>Conta:</p>
                            <p>Booking:</p>
                        </article>
                    </div>
                    <div class="col-lg-6">
                        <article>
                            <p>EMBARQUE:</p>
                            <p>TIPO:</p>
                            <p>TIPO CARGA:</p>
                            <p>DATA OPERACAO:</p>
                            <p>VIAGEM:</p>
                            <p>IDENTIFICADOR/TOKEN:</p>
                            <p>SITUAÇÃO:</p>
                            <p>ETA:</p>
                        </article>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
