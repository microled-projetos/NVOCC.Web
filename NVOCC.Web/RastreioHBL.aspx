﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RastreioHBL.aspx.vb" Inherits="NVOCC.Web.RastreioHBL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--    COLOCAR SEU CODIGO AQUI--%>
    <style>
        .panel-heading {
            background: #d5dce6 !important;
            border-color: #d5dce6 !important;
            color: #003663 !important;
            padding: 1px !important;
            padding-left: 10px !important;
        }

        .titulo {
            background: #003663 !important;
            border-color: #003663 !important;
            color: white !important;
        }
    </style>
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="panel panel-primary">
            <div class="panel-heading titulo">
                <h4>NUMERO DA BL:<asp:Label runat="server" ID="nr_bl"></asp:Label>
                    <asp:Button ID="btnAtualizar" runat="server" BackColor="White" ForeColor="Black" Text="ATUALIZAR" style="margin-left:50px" CssClass="btn btn-default" />
                </h4>
            </div>
        </div>
    </div>
    <!--TRANSPORTE E LOGISTTICA -->
    <div class="col-lg-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>TRANSPORTE/LOGISTICA</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <h5>PAIS PROCEDENCIA:<asp:Label Font-Bold="true" runat="server" ID="pais_procedencia"></asp:Label></h5>
                        <p class="card-text">PORTO EMBARQUE:<asp:Label Font-Bold="true" runat="server" ID="porto_embarque"></asp:Label></p>
                        <p class="card-text">
                            PORTO ORIGEM:
                            <asp:Label Font-Bold="true" runat="server" ID="porto_origem"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-6">
                        <h5>PAIS DESTINO:
                            <asp:Label Font-Bold="true" runat="server" ID="pais_destino"></asp:Label></h5>
                        <p class="card-text">
                            TERMINAL DESCARGA:
                            <asp:Label Font-Bold="true" runat="server" ID="porto_descarga"></asp:Label>
                        </p>
                        <p class="card-text">
                            PORTO DESTINO:
                            <asp:Label Font-Bold="true" runat="server" ID="porto_destino"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <%--PRINCIPAL--%>
         <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>PRINCIPAL</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <article>
                            <p>
                                STATUS:
                                <span class="badge badge-secondary">
                                    <asp:Label Font-Bold="true" runat="server" ID="status"></asp:Label></span>
                            </p>
                            <p>
                                Bl:
                                <asp:Label Font-Bold="true" runat="server" ID="bl"></asp:Label>
                            </p>
                            <p>
                                CONSIGNATARIO INFORMADO:
                                <asp:Label Font-Bold="true" runat="server" ID="consig_informado"></asp:Label>
                            </p>
                            <p>
                                FLUXO:
                                <asp:Label Font-Bold="true" runat="server" ID="fluxo"></asp:Label>
                            </p>
                            <p>
                                NAVIO:
                                <asp:Label Font-Bold="true" runat="server" ID="navio"></asp:Label>
                            </p>
                            <p>
                                IMO:
                                <asp:Label Font-Bold="true" runat="server" ID="imo"></asp:Label>
                            </p>
                            <p>
                                CONTA:
                                <asp:Label Font-Bold="true" runat="server" ID="conta"></asp:Label>
                            </p>

                        </article>
                    </div>
                    <div class="col-lg-6">
                        <article>
                            <p>
                                EMBARQUE:
                                <asp:Label Font-Bold="true" runat="server" ID="embarque"></asp:Label>
                            </p>
                            <p>
                                TIPO:
                                <asp:Label Font-Bold="true" runat="server" ID="tipo"></asp:Label>
                            </p>
                            <p>
                                TIPO CARGA:
                                <asp:Label Font-Bold="true" runat="server" ID="tipo_carga"></asp:Label>
                            </p>
                            <p>
                                DATA OPERACAO:
                                <asp:Label Font-Bold="true" runat="server" ID="data_operacao"></asp:Label>
                            </p>
                            <p>
                                VIAGEM:
                                <asp:Label Font-Bold="true" runat="server" ID="viagem"></asp:Label>
                            </p>
                            <p>
                                IDENTIFICADOR/TOKEN:
                                <asp:Label Font-Bold="true" runat="server" ID="identificador_token"></asp:Label>
                            </p>
                            <p>
                                SITUAÇÃO:
                                <asp:Label Font-Bold="true" runat="server" ID="situacao"></asp:Label>
                            </p>
                            <p>
                                ETA:
                                <asp:Label Font-Bold="true" runat="server" ID="eta"></asp:Label>
                            </p>
                        </article>
                    </div>
                </div>
            </div>
        </div>

        <!--ADUANA-->
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>ADUANA</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <p>
                            CE:
                            <asp:Label Font-Bold="true" runat="server" ID="ce"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-6">
                        <p>
                            MANIFESTO:
                            <asp:Label Font-Bold="true" runat="server" ID="manifesto"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!-- MERCADORIA -->
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>MERCADORIA</h5>
            </div>
            <div class="panel-body">
                <div class="panel-heading">
                    <h6>TOTAIS / VOLUME</h6>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <p>
                            VOLUME (M3):
                            <asp:Label runat="server" Font-Bold="true" Font-Italic="true" ID="volume_m3"></asp:Label>
                        </p>
                        <p>
                            PESO BRUTO:
                            <asp:Label runat="server" Font-Bold="true" ID="peso_bruto"></asp:Label>
                        </p>
                        <p>
                            TEUS:
                            <asp:Label runat="server" Font-Bold="true" ID="teus"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-6">
                        <p>
                            CNTR(S):
                            <asp:Label runat="server" Font-Bold="true" ID="cntrs"></asp:Label>
                        </p>
                        <p>
                            TOTAL CNTR(S):
                            <asp:Label runat="server" Font-Bold="true" ID="total_cntrs"></asp:Label>
                        </p>

                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="panel-heading">
                    <h6>ITENS</h6>
                </div>
                <div class="row">
                      <div id="divCNTR" runat="server" class="col-lg-12">
                    </div>

                </div>
            </div>
        </div>
        <!-- ENVOLVIDOS -->
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>ENVOLVIDOS</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <p>
                            CONSIGNATARIO:
                            <asp:Label runat="server" Font-Bold="true" Font-Italic="true" ID="consignatario"></asp:Label>
                        </p>
                        <p>
                            ARMADOR:
                            <asp:Label runat="server" Font-Bold="true" ID="armador"></asp:Label>
                        </p>
                        <p>
                            AGENTE DE CARGA:
                            <asp:Label runat="server" Font-Bold="true" ID="agente_carga"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-6">
                        <p>
                            AGENCIA MARITIMA:
                            <asp:Label runat="server" Font-Bold="true" ID="agencia_maritima"></asp:Label>
                        </p>
                        <p>
                            ARMADOR INFORMADO:
                            <asp:Label runat="server" Font-Bold="true" ID="armador_informado"></asp:Label>
                        </p>
                        <p>
                            AGENTE INTERNACIONAL:
                            <asp:Label runat="server" Font-Bold="true" ID="agente_internacional"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!-- DATAS -->
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>DATAS</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <p>
                            DATA CADASTRO:
                            <asp:Label Font-Bold="true" runat="server" ID="data_cadastro"></asp:Label>
                        </p>
                        <p>
                            DATA EMISSÃO BL:
                            <asp:Label Font-Bold="true" runat="server" ID="data_emissao_bl"></asp:Label>
                        </p>
                        <p>
                            DATA EMBARQUE:
                            <asp:Label Font-Bold="true" runat="server" ID="data_embarque"></asp:Label>
                        </p>
                        <p>
                            DATA OPERAÇÃO:
                            <asp:Label Font-Bold="true" runat="server" ID="data_operacao2"></asp:Label>
                        </p>
                        <p>
                            DATA ETA ARMADOR:
                            <asp:Label Font-Bold="true" runat="server" ID="data_eta_armador"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-6">
                        <p>
                            DATA ULTIMA ATUALIZAÇÃO:
                            <asp:Label Font-Bold="true" runat="server" ID="data_ultima_atualizacao"></asp:Label>
                        </p>
                        <p>
                            DATA EMISSÃO CE:
                            <asp:Label Font-Bold="true" runat="server" ID="data_emissao_ce"></asp:Label>
                        </p>
                        <p>
                            DATA MANIFESTO:
                            <asp:Label Font-Bold="true" runat="server" ID="data_manifesto"></asp:Label>
                        </p>
                        <p>
                            DATA PRESENÇA DE CARGA:
                            <asp:Label Font-Bold="true" runat="server" ID="data_presenca_carga"></asp:Label>
                        </p>
                        <p>
                            ETA:
                            <asp:Label Font-Bold="true" runat="server" ID="eta2"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>

            <!-- DOCUMENTOS -->
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>DOCUMENTOS</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div id="divConteudoDinamico" runat="server" class="col-lg-12">
                    </div>
                </div>
            </div>
        </div>
    <!-- FOLLOWUP -->
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>FOLLOW UP</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div id="followup" runat="server" class="col-lg-12">
                    </div>
                </div>
            </div>
        </div>
    </div>
 
         <!-- MAPA -->
       <div class="col-lg-6">
                       <iframe  width="900" height="400" runat="server" id="mapa"></iframe>
         </div>
   
    <!--WORKFLOW -->
    <div class="col-lg-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h5>WORKFLOW</h5>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12 col-lg-12">
                        <div id="tracking-pre"></div>
                        <div id="tracking">
                            <div class="text-center tracking-status-intransit" style="background: #d5dce6 !important">
                                <p class="tracking-status text-tight" style="color:#003663 !important">historico</p>
                            </div>
                            <div id="trakinglist" runat="server" class="tracking-list">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
