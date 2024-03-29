﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation="False" MasterPageFile="~/Site.Master" CodeBehind="CadastrarEmbarqueHouse.aspx.vb" Inherits="NVOCC.Web.CadastrarEmbarqueHouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: right; display: none">
        <a id="ajuda" href="#" title="Ajuda">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
                <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z" />
            </svg></a>
    </div>
    <style>
        .compra {
            color: red;
            font-family: verdana;
            font-size: 8pt;
            background-color: #f0e4da;
        }

        .venda {
            color: green;
            font-family: verdana;
            font-size: 8pt;
            background-color: #f0e4da;
        }

        .inativa {
            color: black;
            font-family: verdana;
            font-size: 8pt;
            color: gray;
            text-decoration: line-through;
        }

        input[type='checkbox'][disabled][checked] {
            height: 0px;
        }

        input[type='checkbox'][disabled][checked]:after {
            content: '\e013';
            position: absolute;
            margin-top: -10px;
            opacity: 1 !important;
            font-family: 'Glyphicons Halflings';
            background-color: #428bff;
            color: white;
        }

    </style>
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">MÓDULO OPERACIONAL - 
                   <asp:Label ID="lblTipoModulo" runat="server" />
                        <asp:Label ID="lblHouse_Titulo" runat="server" />
                    </h3>

                    <asp:LinkButton ID="lkProximo" runat="server" BackColor="White" ForeColor="Black" Style="float: right; margin-top: -25PX; position: static;"
                        CssClass="btn btn-default">
                        <i class="glyphicon glyphicon-step-forward"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lkAnterior" runat="server" BackColor="White" ForeColor="Black" Style="float: right; margin-top: -25PX; position: static;"
                        CssClass="btn btn-default">
                        <i class="glyphicon glyphicon-step-backward"></i>
                    </asp:LinkButton>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-offset-9 col-sm-3">
                            <asp:LinkButton ID="btnCapaMaritimo" runat="server" Visible="False" CssClass="btn btn-success btn-block" Text="Imprimir Capa do Processo" href="#" OnClientClick="CapaMaritimo()" />
                            <asp:LinkButton ID="btnCapaAereo" runat="server" Visible="False" CssClass="btn btn-success btn-block" Text="Imprimir Capa do Processo" href="#" OnClientClick="CapaAereo()" />
                        </div>
                    </div>
                    <div id="tabs">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="active">
                                <a href="#Maritimo" role="tab" data-toggle="tab">
                                    <i class="fa fa-edit" style="padding-right: 8px;"></i>Maritimo
                                </a>
                            </li>
                            <li>
                                <a href="#Aereo" role="tab" data-toggle="tab">
                                    <i class="fa fa-edit" style="padding-right: 8px;"></i>Aéreo
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="tab-content">
                        <br />

                        <div class="tab-pane fade active in" id="Maritimo">


                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active">
                                    <a href="#BasicoMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Informações Básicas
                                    </a>
                                </li>
                                <li>
                                    <a href="#CargaMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Carga
                                    </a>
                                </li>
                                <li>
                                    <a href="#TaxasMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas
                                    </a>
                                </li>
                                <li>
                                    <a href="#DocMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Anexos(documentação)
                                    </a>
                                </li>
                                <li>
                                    <a href="#ObsMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Observações
                                    </a>
                                </li>
                                <li>
                                    <a href="#RefMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Referências
                                    </a>
                                </li>
                            </ul>

                            <div class="tab-content">

                                <div class="tab-pane fade active in" id="BasicoMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_BasicoMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_BasicoMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_BasicoMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_BasicoMaritimo" runat="server"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Master:</label>
                                                        <asp:TextBox ID="txtIDMaster_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Código:</label>
                                                        <asp:TextBox ID="txtID_CotacaoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtID_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do Processo:</label>
                                                        <asp:TextBox ID="txtProcesso_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do HBL:</label>
                                                        <asp:TextBox ID="txtHBL_BasicoMaritimo" runat="server" CssClass="form-control BL"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Serviço:</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlServico_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServicoMaritimo" DataValueField="ID_SERVICO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do MBL:</label>
                                                        <asp:TextBox ID="txtMBL_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label" style="color: white">:</label>
                                                        <asp:Button ID="btnVisualizarMBL_Maritimo" runat="server" CssClass="btn btn-info btn-block" Text="Visualizar MBL" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label"></label>
                                                        <asp:CheckBox ID="ckTrakingAutomaticoMaritimo" runat="server" CssClass="form-control" Checked="true" Text="&nbsp;&nbsp;Traking Automatico"></asp:CheckBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-1" id="divDocConferidoMaritimo" runat="server">
                                                    <div class="form-group">
                                                        <center><label class="control-label">Doc. Conferido?</label><br />
                                                    <asp:CheckBox ID="ckDocConferidosMaritimo" runat="server" ></asp:CheckBox></center>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo BL:</label>
                                                        <asp:TextBox ID="txtTipoBLMaritimo" runat="server" CssClass="form-control" Enabled="false" ToolTip="Campo preenchido na cotação"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Cliente Final:</label>
                                                        <asp:TextBox ID="txtClienteFinalMaritimo" runat="server" CssClass="form-control" Enabled="false" ToolTip="Campo preenchido na cotação"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Transportador:</label>
                                                        <asp:TextBox ID="txtCodTransportador_Maritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca:</label>
                                                        <asp:TextBox ID="txtNomeTransportador_Maritimo" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Transportador:</label>
                                                        <asp:DropDownList ID="ddlTransportador_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsTransportador_Maritimo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Nº Contrato Armador:</label>
                                                        <asp:TextBox ID="txtContratoArmador_BasicoMaritimo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Porto de Origem:</label>
                                                        <asp:DropDownList ID="ddlOrigem_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Porto de Destino:</label>
                                                        <asp:DropDownList ID="ddlDestino_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%--<div class="col-sm-2">
                                                                        <div class="form-group">
                                                                            <label class="control-label">Place of Receipt:</label>
                                                                            <asp:DropDownList ID="ddlPlaceReceipt_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>--%>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Final Destination:</label>
                                                        <asp:DropDownList ID="ddlFinalDestination_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CIDADE" DataSourceID="dsFinalDestination" DataValueField="ID_CIDADE">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Incoterm:</label>
                                                        <asp:DropDownList ID="ddlIncoterm_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Status Frete Agente:</label>
                                                <asp:DropDownList ID="ddlStatusFreteAgente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_FRETE_AGENTE" DataSourceID="dsStatusFreteAgente" DataValueField="ID_STATUS_FRETE_AGENTE" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                            </div>
                                                </div>
                                            <div class="row">
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Cliente:</label>
                                                        <asp:TextBox ID="txtCodCliente_Maritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Cliente:</label>
                                                        <asp:TextBox ID="txtNomeCliente_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Nome Cliente:</label>
                                                        <asp:DropDownList ID="ddlCliente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="Descricao" DataSourceID="dsCliente_Maritimo">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Importador:</label>
                                                        <asp:TextBox ID="txtCodImportador_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Importador:</label>
                                                        <asp:TextBox ID="txtNomeImportador_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Nome Importador:</label>
                                                        <asp:DropDownList ID="ddlImportador_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="Descricao" DataSourceID="dsImportador_Maritimo">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Exportador:</label>
                                                        <asp:TextBox ID="txtCodExportador_Maritimo" runat="server" placeholder="Nome" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Exportador:</label>
                                                        <asp:TextBox ID="txtNomeExportador_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Exportador:</label>
                                                        <asp:DropDownList ID="ddlExportador_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsExportador_Maritimo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Comissária:</label>
                                                        <asp:TextBox ID="txtCodComissaria_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Comissária:</label>
                                                        <asp:TextBox ID="txtNomeComissaria_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Comissária:</label>
                                                        <asp:DropDownList ID="ddlComissaria_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsComissaria_Maritimo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Indicador:</label>
                                                        <asp:TextBox ID="txtCodIndicador_Maritimo" runat="server" placeholder="Nome" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Indicador:</label>
                                                        <asp:TextBox ID="txtNomeIndicador_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Indicador:</label>
                                                        <asp:DropDownList ID="ddlIndicador_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsIndicador_Maritimo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Internacional:</label>
                                                        <asp:TextBox ID="txtCodAgente_Maritimo" runat="server" placeholder="Nome" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca:</label>
                                                        <asp:TextBox ID="txtNomeAgente_Maritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Agente Internacional:</label>
                                                        <asp:DropDownList ID="ddlAgente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsAgente_Maritimo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Pagamento:</label>
                                                        <asp:DropDownList ID="ddlTipoPagamento_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Estufagem:</label>
                                                        <asp:DropDownList ID="ddlEstufagem_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div id="divFlagMaritimo" runat="server">
                                                    <div class="col-sm-1">
                                                        <div class="form-group">
                                                            <label class="control-label" style="color: white">X</label>
                                                            <asp:CheckBox ID="ckbLTL_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control" Text="&nbsp;&nbsp;LTL"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <div class="form-group">
                                                            <label class="control-label" style="color: white">X</label>
                                                            <asp:CheckBox ID="ckbDtaHub_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control" Text="&nbsp;&nbsp;DTA HUB"></asp:CheckBox>
                                                        </div>
                                                    </div>



                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label" style="color: white">X</label>
                                                            <asp:CheckBox ID="ckbTranspDedicado_BasicoMaritimo" Enabled="false" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;TRANSP. DEDICADO"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label"></label>
                                                        <asp:CheckBox ID="ckbFreeHand_BasicoMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Free Hand" AutoPostBack="true"></asp:CheckBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Carga:</label>
                                                        <asp:DropDownList ID="ddlTipoCarga_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCargas" DataValueField="ID_TIPO_CARGA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Valor da Carga:</label>
                                                        <asp:TextBox ID="txtValorCarga_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Número CE:</label>
                                                        <asp:TextBox ID="txtCE_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Data CE:</label>
                                                        <asp:TextBox ID="txtDataCE_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Transp. Rodoviário:</label>
                                                        <asp:TextBox ID="txtCodTranspRodoviario_Maritimo" runat="server" placeholder="Nome" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca:</label>
                                                        <asp:TextBox ID="txtNomeTranspRodoviario_BasicoMaritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Transp. Rodoviário:</label>
                                                        <asp:DropDownList ID="ddlTranspRodoviario_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsTranspRodoviario_Maritimo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>



                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Divisão Profit:</label>
                                                        <asp:DropDownList ID="ddlDivisaoProfit_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_DIVISAO_PROFIT" DataTextField="NM_TIPO_DIVISAO_PROFIT" DataSourceID="dsDivisaoProfit">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Valor Divisão Profit:</label>
                                                        <asp:TextBox ID="txtValorDivisaoProfit_BasicoMaritimo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Profit Calculado:</label>
                                                        <asp:TextBox ID="txtProfitCalculado_BasicoMaritimo" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row" style="display: none">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Comercial:</label>
                                                        <asp:TextBox ID="txtRefComercial_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Auxiliar:</label>
                                                        <asp:TextBox ID="txtRefAuxiliar_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%--  <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Shipper:</label>
                                                        <asp:TextBox ID="txtRefShipper_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="row" id="divMercadoriaBL_Maritimo" runat="server" style="display: none">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Resumo Mercadoria:</label>
                                                        <asp:TextBox ID="txtResumoMercadoria_BasicoMaritimo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label" style="color: white">X</label>
                                                        <asp:CheckBox ID="ckbEmailCotacao_BasicoMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Avisos Automáticos para o Parceiro"></asp:CheckBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-10">
                                                    <div class="form-group">
                                                        <label class="control-label">Endereços de e-mail do Processo:</label>
                                                        <asp:TextBox ID="txtEmailCotacao_BasicoMaritimo" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_BasicoMaritimo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_BasicoMaritimo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlEstufagem_BasicoMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_BasicoMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_BasicoMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeCliente_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeTransportador_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeAgente_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeIndicador_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeImportador_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeExportador_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnVisualizarMBL_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeComissaria_Maritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeTranspRodoviario_BasicoMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlStatusFreteAgente_BasicoMaritimo" /> 
                                            <asp:AsyncPostBackTrigger ControlID="ckbFreeHand_BasicoMaritimo" />  
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="CargaMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Nova Carga" ID="btnNovaCargaMaritimo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_CargaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_CargaMaritimo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_CargaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_CargaMaritimo1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive tableFixHead" id="divGrid" runat="server">
                                                        <asp:GridView ID="dgvCargaMaritimo" DataKeyNames="ID_CARGA_BL" DataSourceID="dsCargaMaritimo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_CARGA_BL" HeaderText="#" SortExpression="ID_CARGA_BL" />
                                                                <asp:BoundField DataField="CONTAINER" HeaderText="Container" SortExpression="CONTAINER" />
                                                                <asp:BoundField DataField="TIPO_CNTR" HeaderText="Tipo Container" SortExpression="TIPO_CNTR" />
                                                                <asp:BoundField DataField="QT_DIAS_FREETIME" HeaderText="FreeTime" SortExpression="QT_DIAS_FREETIME" />
                                                                <asp:BoundField DataField="ID_TIPO_CARGA" HeaderText="Tipo Carga" SortExpression="ID_TIPO_CARGA" />
                                                                <asp:BoundField DataField="QT_MERCADORIA" HeaderText="Qtd. Volume" SortExpression="QT_MERCADORIA" />
                                                                <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso Bruto" SortExpression="VL_PESO_BRUTO" />
                                                                <asp:BoundField DataField="VL_M3" HeaderText="M3" SortExpression="VL_M3" />
                                                                <asp:BoundField DataField="NCM" HeaderText="NCM" SortExpression="NCM" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                            Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                            Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i>
                                </div>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_CARGA_BL") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCargaMaritimo" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaMaritimo" />
                                                </Triggers>
                                            </asp:UpdatePanel>




                                            <ajaxToolkit:ModalPopupExtender ID="mpeCargaMaritimo" runat="server" PopupControlID="Panel2" TargetControlID="btnNovaCargaMaritimo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                    <ContentTemplate>
                                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title">Carga - Maritimo</h5>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="alert alert-success" id="divSuccess_CargaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblSuccess_CargaMaritimo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger" id="divErro_CargaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblErro_CargaMaritimo2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-3" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_CargaMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo de Carga:</label>
                                                                                <asp:DropDownList ID="ddlMercadoria_CargaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCargas" DataValueField="ID_TIPO_CARGA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Busca NCM:</label>
                                                                                <asp:TextBox ID="txtIDNCM_CargaMaritimo" runat="server" Style="display: none"></asp:TextBox>
                                                                                <asp:TextBox ID="txtNCMFiltro_CargaMaritimo" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">NCM:</label>
                                                                                <asp:DropDownList ID="ddlNCM_CargaMaritimo" runat="server" CssClass="form-control" FontSize="11px" DataTextField="Descricao" DataSourceID="dsNCM_CargaMaritimo" DataValueField="ID_NCM"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Grupo NCM:</label>
                                                                                <asp:TextBox ID="txtGrupoNCM_CargaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Quantidade de Volumes:</label>
                                                                                <asp:TextBox ID="txtQtdVolumes_CargaMaritimo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Peso Bruto:</label>
                                                                                <asp:TextBox ID="txtPesoBruto_CargaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Peso Volumétrico:</label>
                                                                                <asp:TextBox ID="txtPesoVolumetrico_CargaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Embalagem:</label>
                                                                                <asp:DropDownList ID="ddlEmbalagem_CargaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo Container:</label>
                                                                                <asp:DropDownList ID="ddlTipoContainer_CargaMaritimo" AutoPostBack="True" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Número do Container:</label>
                                                                                <asp:TextBox ID="txtNumeroContainer_CargaMaritimo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlNumeroCNTR_CargaMaritimo" AutoPostBack="True" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NR_CNTR" DataSourceID="dsCNTR" DataValueField="ID_CNTR_BL"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Número do Lacre:</label>
                                                                                <asp:TextBox ID="txtNumeroLacre_CargaMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Tara:</label>
                                                                                <asp:TextBox ID="txtValorTara_CargaMaritimo" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="divMercadoriaCNTR_Maritimo" runat="server" style="display: block">
                                                                        <div class="col-sm-12">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Resumo Mercadoria:</label>
                                                                                <asp:TextBox ID="txtDescMercadoriaCNTR_Maritimo" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar" ID="btnSalvar_CargaMaritimo" CssClass="btn btn-success" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_CargaMaritimo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <%--<ajaxToolkit:ModalPopupExtender ID="mpeNCM_CargaMaritimo" runat="server" PopupControlID="PanelNCM_CargaMaritimo" TargetControlID="ddlNCM_CargaMaritimo" CancelControlID="btnFecharNCM_CargaMaritimo"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNCM_CargaMaritimo" runat="server" CssClass="modalPopup" Style="display: none;">
                                       
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" >Selecione um NCM</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />

                                                            <asp:Label ID="Label1" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNCMFiltro_CargaMaritimo" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNCM_CargaMaritimo" runat="server" DataTextField="NCM" DataSourceID="dsNCM_CargaMaritimo" DataValueField="ID_NCM" Style="text-align:justify;font-size:12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNCM_CargaMaritimo" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNCM_CargaMaritimo" Text="Salvar NCM" />
                                                        </div>


                                                    </div>
                                                </div>
                                           
                                    </asp:Panel>--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtNCMFiltro_CargaMaritimo" />
                                                        <%--                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarNCM_CargaMaritimo" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoContainer_CargaMaritimo" />
                                                        <%--                                                        <asp:AsyncPostBackTrigger ControlID="ddlNCM_CargaMaritimo" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNumeroCNTR_CargaMaritimo" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaMaritimo" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="TaxasMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" AllowCustomErrorsRedirect="True">
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Nova Taxa" ID="btnNovaTaxaMaritimo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_TaxaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_TaxaMaritimo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_TaxaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_TaxaMaritimo1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive tableFixHead" id="div7" runat="server">
                                                        <br />
                                                        COMPRAS:                                          
                                                        <asp:GridView ID="dgvTaxaMaritimoCompras" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasMaritimoCompras" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID_BL_TAXA" SortExpression="ID_BL_TAXA" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID_BL_TAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                                <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="PARCEIRO" SortExpression="PARCEIRO_EMPRESA" />
                                                                <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                                <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                                <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                                <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                                <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                                                <asp:BoundField DataField="DECLARADO" HeaderText="DECLARADO" SortExpression="DECLARADO" />
                                                                <asp:BoundField DataField="PROFIT" HeaderText="PROFIT" SortExpression="PROFIT" />
                                                                <asp:TemplateField HeaderText="ORIGEM" SortExpression="ORIGEM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblORIGEM" runat="server" Text='<%# Eval("ORIGEM") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ATIVA?" SortExpression="ATIVA">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAtiva" runat="server" Text='<%# Eval("ATIVA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="HISTÓRICO" SortExpression="HISTORICO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTemHistorico" runat="server" Text='<%# Eval("HISTORICO") %>' Visible="false"></asp:Label>
                                                                        <asp:ImageButton ID="ImageButton1" src="Content/imagens/hist.png" runat="server" CommandArgument='<%# Eval("ID_BL_TAXA") %>' ToolTip="Histórico" CommandName="Historico" />
                                                                        <asp:Label ID="lblTaxa" Visible="False" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i>
                        </div>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>


                                                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-block btn-primary" Style="display: none" />
                                                        <ajaxToolkit:ModalPopupExtender ID="mpeHistoricoMaritimo" runat="server" PopupControlID="pnHistoricoMaritimo" TargetControlID="Button1" CancelControlID="btnFecharHistoricoMaritimo"></ajaxToolkit:ModalPopupExtender>
                                                        <asp:Panel ID="pnHistoricoMaritimo" runat="server" CssClass="modalPopup" Style="display: none;">
                                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Historico de Status</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-12"> 
                                                                       <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">
                                                                            <asp:GridView ID="dgvHistoricoMaritimo" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="ID_INATIVACAO" DataSourceID="dsHistorico" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_INATIVACAO" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                 <asp:BoundField DataField="STATUS" HeaderText="Ativo?" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NOME" HeaderText="Usuário" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="DT_INATIVACAO" HeaderText="Data" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NM_MOTIVO_INATIVACAO" HeaderText="Motivo" ItemStyle-HorizontalAlign="Center" />
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="Historico" />
                                                                        </asp:GridView>

                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharHistoricoMaritimo" text="Close" />                                                                
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                                        </asp:Panel>

                                                        VENDAS:
                                                        <asp:GridView ID="dgvTaxaMaritimoVendas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasMaritimoVendas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID_BL_TAXA" SortExpression="ID_BL_TAXA" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID_BL_TAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                                <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="PARCEIRO" SortExpression="PARCEIRO_EMPRESA" />
                                                                <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                                <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                                <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                                <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                                <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                                                <asp:BoundField DataField="DECLARADO" HeaderText="DECLARADO" SortExpression="DECLARADO" />
                                                                <asp:BoundField DataField="PROFIT" HeaderText="PROFIT" SortExpression="PROFIT" />
                                                                <asp:TemplateField HeaderText="ORIGEM" SortExpression="ORIGEM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblORIGEM" runat="server" Text='<%# Eval("ORIGEM") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ATIVA?" SortExpression="ATIVA">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAtiva" runat="server" Text='<%# Eval("ATIVA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span>
                    </div>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i>
                </div>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>




                                                        <div>
                                                            <asp:Label ID="lblDiferencaMaritimo" runat="server" Style="color: blue"></asp:Label>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaMaritimoVendas" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoVendas" />
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaMaritimoCompras" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoCompras" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaMaritimo" />
                                                </Triggers>
                                            </asp:UpdatePanel>




                                            <ajaxToolkit:ModalPopupExtender ID="mpeTaxaMaritimo" runat="server" PopupControlID="Panel3" TargetControlID="btnNovaTaxaMaritimo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                    <ContentTemplate>
                                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title">Taxa - Maritimo</h5>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="alert alert-success" id="divSuccess_TaxaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblSuccess_TaxaMaritimo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger" id="divErro_TaxaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblErro_TaxaMaritimo2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="row" style="display: none">
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_TaxaMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo Despesa:</label>
                                                                                <asp:DropDownList ID="ddlDespesa_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" AutoPostBack="true" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>

                                                                                <asp:CheckBox ID="ckbPremiacao_TaxaMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Premiação"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Status do pagamento:</label>
                                                                                <asp:DropDownList ID="ddlStatusPagamento_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_PAGAMENTO" DataSourceID="dsStatusPagamento" Enabled="false" DataValueField="ID_STATUS_PAGAMENTO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>

                                                                                <asp:CheckBox ID="ckbDeclarado_TaxaMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Declarado"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>
                                                                                <asp:CheckBox ID="ckbProfit_TaxaMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;PROFIT"></asp:CheckBox>
                                                                            </div>
                                                                        </div>

                                                                    </div>


                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Base de cálculo:</label>
                                                                                <asp:DropDownList ID="ddlBaseCalculo_TaxaMaritimo" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Qtd. Base de cálculo:</label>
                                                                                <asp:TextBox ID="txtQtdBaseCalculo_TaxaMaritimo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Origem Serviço:</label><label runat="server" style="color: red">*</label>
                                                                                <asp:DropDownList ID="ddlOrigemPagamento_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO"></asp:DropDownList>

                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo de Pagamento:</label><label runat="server" style="color: red">*</label>
                                                                                <asp:DropDownList ID="ddlTipoPagamento_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Destinatario de Cobrança:</label>
                                                                                <asp:DropDownList ID="ddlDestinatarioCob_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_DESTINATARIO_COBRANCA" DataSourceID="dsDestinatarioCobranca" DataValueField="ID_DESTINATARIO_COBRANCA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <strong>
                                                                                    <asp:Label CssClass="control-label" runat="server" ID="lblTipoEmpresa_Maritimo" Text="Fornecedor:" /></strong>
                                                                                <asp:DropDownList ID="ddlEmpresa_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsFornecedorMaritimo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="row" runat="server" id="divCompraMaritimo">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Moeda de Compra:</label>
                                                                                <asp:DropDownList ID="ddlMoedaCompra_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Mínimo de Compra:</label>
                                                                                <asp:TextBox ID="txtMinCompra_TaxaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Compra:</label>
                                                                                <asp:TextBox ID="txtValorCompra_TaxaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" runat="server" id="divVendaMaritimo">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Moeda de Venda:</label>
                                                                                <asp:DropDownList ID="ddlMoedaVenda_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Mínimo de Venda:</label>
                                                                                <asp:TextBox ID="txtMinVenda_TaxaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Venda:</label>
                                                                                <asp:TextBox ID="txtValorVenda_TaxaMaritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Observações:</label>
                                                                                <asp:TextBox ID="txtObs_TaxaMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar Taxa" ID="btnSalvar_TaxaMaritimo" CssClass="btn btn-success" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_TaxaMaritimo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlDespesa_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtValorVenda_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlBaseCalculo_TaxaMaritimo" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoVendas" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoCompras" />
                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="DocMaritimo">

                                    <div class="alert alert-danger" id="divErroUploadMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblErroUploadMaritimo" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-success" id="divSuccessUploadMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblSuccessUploadMaritimo" runat="server">
                                             Ação realizada com sucesso!
                                        </asp:Label>
                                    </div>
                                    <br />
                                    <div class="row">


                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Tipo de arquivo:</label>
                                                <asp:DropDownList ID="ddlTipoArquivoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_ARQUIVO" DataSourceID="dsTipoArquivo" DataValueField="ID_TIPO_ARQUIVO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:FileUpload ID="FileUploadMaritimo" CssClass="form-control" runat="server" Visible="true" Style="display: block" onchange="Javascript: VerificaTamanhoArquivoM();"></asp:FileUpload>
                                            </div>
                                        </div>

                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtUPMaritimo" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:TextBox ID="txtArquivoSelecionadoMaritimo" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:GridView ID="dgvArquivosMaritimo" runat="server" AutoGenerateColumns="false" EmptyDataText="Nenhum arquivo enviado" DataKeyNames="ID_ARQUIVO" DataSourceID="dsUploadsMaritimo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" Style="max-height: 400px; overflow: auto;" AllowSorting="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID_ARQUIVO" runat="server" Text='<%# Eval("ID_ARQUIVO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nome do Arquivo" HeaderStyle-ForeColor="#337ab7">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNM_ARQUIVO" runat="server" Text='<%# Eval("NM_ARQUIVO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NM_TIPO_ARQUIVO" HeaderText="Tipo do Arquivo" SortExpression="NM_TIPO_ARQUIVO" />
                                                            <asp:BoundField DataField="NOME" HeaderText="Usuário" SortExpression="NOME" />
                                                            <asp:BoundField DataField="DT_UPLOAD" HeaderText="Data/Hora" SortExpression="DT_UPLOAD" />
                                                            <asp:TemplateField HeaderText="Ativo para clientes?" HeaderStyle-ForeColor="#337ab7">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckAtivoClientes" Checked='<%# Eval("FL_ATIVO_CLIENTES") %>' runat="server" AutoPostBack="true" OnCheckedChanged="ckAtivoClientes_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href="VisualizarUpload.aspx?id=<%# Eval("ID_ARQUIVO") %>" target="_blank" style="font-size: medium" data-toggle="tooltip" data-placement="top" title="Visualizar">
                                                                        <asp:Label ID="lblBotaoVisualizar" runat="server" Text="Visualizar" /></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandName="Download" CommandArgument='<%# Eval("CAMINHO_ARQUIVO") %>' runat="server" Font-Size="medium"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDeleta" Text="Deletar" OnClientClick="javascript:return confirm('Deseja realmente excluir este arquivo?');" CommandName="Excluir" CommandArgument='<%# Eval("ID_ARQUIVO") & "|" & Eval("CAMINHO_ARQUIVO") %>' runat="server" Font-Size="medium" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 col-sm-offset-6" style="display: none">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimparUploadMaritimo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-sm-offset-9">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnUploadMaritimo" OnClientClick="javascript:return confirm('Deseja realmente realizar o upload?');" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>

                                            </div>


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnUploadMaritimo" />
                                            <asp:PostBackTrigger ControlID="dgvArquivosMaritimo" />
                                            <asp:PostBackTrigger ControlID="btnLimparUploadMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>
                                <div class="tab-pane fade" id="ObsMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_ObsMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_ObsMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_ObsMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_ObsMaritimo" runat="server"></asp:Label>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Operacional Interna:</label>
                                                        <asp:TextBox ID="txtObsoperacional_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Comercial:</label>
                                                        <asp:TextBox ID="txtObsComercial_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Agente Internacional:</label>
                                                        <asp:TextBox ID="txtObsAgente_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Cliente:</label>
                                                        <asp:TextBox ID="txtObsCliente_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Obs. Cliente (COTAÇÃO):</label>
                                                        <asp:TextBox ID="txtObsCliente_CotacaoMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Obs. Operacional (COTAÇÃO):</label>
                                                        <asp:TextBox ID="txtObsOper_CotacaoMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_ObsMaritimo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_ObsMaritimo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_ObsMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_ObsMaritimo" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="RefMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <div class="row linhabotao">

                                                <div>
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Tipo:</label>
                                                            <asp:DropDownList ID="ddlTipoRefMaritimo" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                                <asp:ListItem Value="CNEE">CNEE</asp:ListItem>
                                                                <asp:ListItem Value="SHIPPER">SHIPPER</asp:ListItem>
                                                                <asp:ListItem Value="AUXILIAR">AUXILIAR</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>

                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label class="control-label">Referência:</label>
                                                            <asp:TextBox ID="txtRefMaritimo" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>

                                                        </div>

                                                    </div>
                                                    <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                        <div class="form-group">
                                                            <asp:Button runat="server" Text="Gravar" ID="btnGravar_RefMaritimo" CssClass="btn btn-success" />


                                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar_RefMaritimo" CssClass="btn btn-danger" />

                                                        </div>
                                                    </div>





                                                </div>
                                            </div>
                                            <br />
                                            <asp:TextBox ID="txtID_RefMaritimo" Style="display: none" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>


                                            <div class="alert alert-success" id="divSuccess_RefMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_RefMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_RefMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_RefMaritimo" runat="server"></asp:Label>
                                            </div>

                                            <div class="table-responsive tableFixHead" id="div10" runat="server">

                                                <asp:GridView ID="dgvRefMaritimo" DataKeyNames="ID_REFERENCIA_CLIENTE" DataSourceID="dsRefMaritimo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>

                                                        <asp:BoundField DataField="ID_REFERENCIA_CLIENTE" ReadOnly="true" HeaderText="#" SortExpression="ID_REFERENCIA_CLIENTE" />
                                                        <asp:BoundField DataField="TIPO" ReadOnly="true" HeaderText="TIPO" SortExpression="TIPO" />
                                                        <asp:BoundField DataField="NR_REFERENCIA_CLIENTE" HeaderText="REFERENCIA CLIENTE" SortExpression="NR_REFERENCIA_CLIENTE" />
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditarParcela" runat="server" CausesValidation="False" CommandName="visualizar" CssClass="btn btn-info" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>'><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            <ControlStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <br />

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvRefMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar_RefMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_RefMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="Aereo">

                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active">
                                    <a href="#BasicoAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Informações Básicas
                                    </a>
                                </li>
                                <li>
                                    <a href="#CargaAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Carga
                                    </a>
                                </li>
                                <li>
                                    <a href="#TaxasAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas
                                    </a>
                                </li>
                                <li>
                                    <a href="#DocAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Anexos(documentação)
                                    </a>
                                </li>
                                <li>
                                    <a href="#ObsAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Observações
                                    </a>
                                </li>
                                <li>
                                    <a href="#RefAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Referências
                                    </a>
                                </li>
                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="BasicoAereo">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_BasicoAereo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_BasicoAereo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_BasicoAereo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_BasicoAereo" runat="server"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Código:</label>
                                                        <asp:TextBox ID="txtID_CotacaoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtID_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Master:</label>Armazém de Desembaraço:
                                                        <asp:TextBox ID="txtIDMaster_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do Processo:</label>
                                                        <asp:TextBox ID="txtProcesso_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">HAWB:</label>
                                                        <asp:TextBox ID="txtHBL_BasicoAereo" runat="server" CssClass="form-control BL"></asp:TextBox>
                                                    </div>
                                                </div>


                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">MAWB:</label>
                                                        <asp:TextBox ID="txtMBL_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label" style="color: white">:</label>
                                                        <asp:Button ID="btnVisualizarMBL_Aereo" runat="server" CssClass="btn btn-info btn-block" Text="Visualizar MBL" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-2" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label"></label>
                                                        <asp:CheckBox ID="ckTrakingAutomaticoAereo" runat="server" CssClass="form-control" Checked="true" Text="&nbsp;&nbsp;Traking Automatico"></asp:CheckBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-1" id="divDocConferidoAereo" runat="server">
                                                    <div class="form-group">
                                                        <center><label class="control-label">Doc. Conferido?</label><br />
                                                    <asp:CheckBox ID="ckDocConferidosAereo" runat="server" ></asp:CheckBox></center>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Serviço:</label>
                                                        <asp:DropDownList ID="ddlServico_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServicoAereo" DataValueField="ID_SERVICO"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo BL:</label>
                                                        <asp:TextBox ID="txtTipoBLAereo" runat="server" CssClass="form-control" Enabled="false" ToolTip="Campo preenchido na cotação"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Cliente Final:</label>
                                                        <asp:TextBox ID="txtClienteFinalAereo" runat="server" CssClass="form-control" Enabled="false" ToolTip="Campo preenchido na cotação"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Pagamento:</label>
                                                        <asp:DropDownList ID="ddlTipoPagamento_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Aeronave:</label>
                                                        <asp:DropDownList ID="ddlTipoAeronave_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_AERONAVE" DataSourceID="dsTipoAeronave" DataValueField="ID_TIPO_AERONAVE"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Estufagem:</label>
                                                        <asp:DropDownList ID="ddlEstufagem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                               
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Data CE:</label>
                                                        <asp:TextBox ID="txtDataCE_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Número CE:</label>
                                                        <asp:TextBox ID="txtNumeroCE_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                          
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Incoterm:</label>
                                                        <asp:DropDownList ID="ddlIncoterm_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                               
                                                
                                                    <div id="divFlagAereo" runat="server">
                                                    <div class="col-sm-1">
                                                        <div class="form-group">
                                                            <label class="control-label" style="color: white">X</label>
                                                            <asp:CheckBox ID="ckbLTL_BasicoAereo" runat="server" Enabled="false" CssClass="form-control" Text="&nbsp;&nbsp;LTL"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <div class="form-group">
                                                            <label class="control-label" style="color: white">X</label>
                                                            <asp:CheckBox ID="ckbDtaHub_BasicoAereo" runat="server" Enabled="false" CssClass="form-control" Text="&nbsp;&nbsp;DTA HUB"></asp:CheckBox>
                                                        </div>
                                                    </div>



                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label" style="color: white">X</label>
                                                            <asp:CheckBox ID="ckbTranspDedicado_BasicoAereo" Enabled="false" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;TRANSP. DEDICADO"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <label class="control-label"></label>
                                                        <asp:CheckBox ID="ckbFreeHand_BasicoAereo" runat="server" CssClass="form-control" Text="&nbsp;Free Hand" AutoPostBack="true"></asp:CheckBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <label class="control-label" style="color: white">X</label>
                                                        <asp:CheckBox ID="ckbTC4_BasicoAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;TC4"></asp:CheckBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <label class="control-label" style="color: white">X</label>
                                                        <asp:CheckBox ID="ckbTC6_BasicoAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;TC6"></asp:CheckBox>
                                                    </div>
                                                </div>

                                                </div>
                                            <div class="row">
                                                  <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto de Origem:</label>
                                                        <asp:DropDownList ID="ddlOrigem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto de Destino:</label>
                                                        <asp:DropDownList ID="ddlDestino_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Final Destination:</label>
                                                        <asp:DropDownList ID="ddlFinalDestination_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CIDADE" DataSourceID="dsFinalDestination" DataValueField="ID_CIDADE">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Valor da Carga:</label>
                                                        <asp:TextBox ID="txtValorCarga_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
</div>
                                             <div class="row">
                                                 <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Status Frete Agente:</label>
                                                <asp:DropDownList ID="ddlStatusFreteAgente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_FRETE_AGENTE" DataSourceID="dsStatusFreteAgente" DataValueField="ID_STATUS_FRETE_AGENTE" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Armazém de Desembaraço:</label>
                                                        <asp:DropDownList ID="ddlArmazem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsArmazemDesembaraco" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Transportador:</label>
                                                        <asp:TextBox ID="txtCodTransportador_Aereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Transportador:</label>
                                                        <asp:TextBox ID="txtNomeTransportador_Aereo" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Transportador:</label>
                                                        <asp:DropDownList ID="ddlTransportador_BasicoAereo" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsTransportador_Aereo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%-- <div class="col-sm-2">
                                                                        <div class="form-group">
                                                                            <label class="control-label">Nº Contrato Armador:</label>
                                                                            <asp:TextBox ID="txtContratoArmador_BasicoAereo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                        </div>
                                                                    </div>--%>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Agente:</label>
                                                        <asp:TextBox ID="txtCodAgente_Aereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Agente:</label>
                                                        <asp:TextBox ID="txtNomeAgente_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Agente:</label>
                                                        <asp:DropDownList ID="ddlAgente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsAgente_Aereo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Indicador:</label>
                                                        <asp:TextBox ID="txtCodIndicador_Aereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Indicador:</label>
                                                        <asp:TextBox ID="txtNomeIndicador_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Indicador:</label>
                                                        <asp:DropDownList ID="ddlIndicador_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsIndicador_Aereo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Cliente:</label>
                                                        <asp:TextBox ID="txtCodCliente_Aereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Cliente:</label>
                                                        <asp:TextBox ID="txtNomeCliente_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Cliente:</label>
                                                        <asp:DropDownList ID="ddlCliente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="Descricao" DataSourceID="dsCliente_Aereo">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Importador:</label>
                                                        <asp:TextBox ID="txtCodImportador_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Importador:</label>
                                                        <asp:TextBox ID="txtNomeImportador_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Importador:</label>
                                                        <asp:DropDownList ID="ddlImportador_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="Descricao" DataSourceID="dsImportador_Aereo">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Exportador:</label>
                                                        <asp:TextBox ID="txtCodExportador_Aereo" runat="server" placeholder="Nome" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Exportador:</label>
                                                        <asp:TextBox ID="txtNomeExportador_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Exportador:</label>
                                                        <asp:DropDownList ID="ddlExportador_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsExportador_Aereo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Comissária:</label>
                                                        <asp:TextBox ID="txtCodComissaria_Aereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca Comissária:</label>
                                                        <asp:TextBox ID="txtNomeComissaria_Aereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Comissária:</label>
                                                        <asp:DropDownList ID="ddlComissaria_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsComissaria_Aereo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>




                                            </div>
                                            <div class="row">

                                                <div class="col-sm-1" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Cód Transp. Rodoviário:</label>
                                                        <asp:TextBox ID="txtCodTranspRodoviario_Aereo" runat="server" placeholder="Nome" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Busca:</label>
                                                        <asp:TextBox ID="txtNomeTranspRodoviario_BasicoAereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Transp. Rodoviário:</label>
                                                        <asp:DropDownList ID="ddlTranspRodoviario_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsTranspRodoviario_Aereo" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>


                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Divisão Profit:</label>
                                                        <asp:DropDownList ID="ddlDivisaoProfit_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_DIVISAO_PROFIT" DataTextField="NM_TIPO_DIVISAO_PROFIT" DataSourceID="dsDivisaoProfit">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Valor Divisão Profit:</label>
                                                        <asp:TextBox ID="txtValorDivisaoProfit_BasicoAereo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Profit Calculado:</label>
                                                        <asp:TextBox ID="txtProfitCalculado_BasicoAereo" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="display: none">



                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Comercial:</label>
                                                        <asp:TextBox ID="txtRefComercial_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Auxiliar:</label>
                                                        <asp:TextBox ID="txtRefAuxiliar_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%--<div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Shipper:</label>
                                                        <asp:TextBox ID="txtRefShipper_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>--%>
                                            </div>

                                            <div class="row" id="divMercadoriaBL_Aereo" runat="server" style="display: none">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Resumo Mercadoria:</label>
                                                        <asp:TextBox ID="txtResumoMercadoria_BasicoAereo" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label" style="color: white">X</label>
                                                        <asp:CheckBox ID="ckbEmailCotacao_BasicoAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Avisos Automáticos para o Parceiro"></asp:CheckBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-10">
                                                    <div class="form-group">
                                                        <label class="control-label">Endereços de e-mail do Processo::</label>
                                                        <asp:TextBox ID="txtEmailCotacao_BasicoAereo" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_BasicoAereo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_BasicoAereo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlEstufagem_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeCliente_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeTransportador_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeAgente_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeIndicador_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeImportador_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeExportador_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnVisualizarMBL_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeComissaria_Aereo" />
                                            <asp:AsyncPostBackTrigger ControlID="txtNomeTranspRodoviario_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="ckbFreeHand_BasicoAereo" />                                            
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="CargaAereo">
                                    <br />
                                    <div class="row">

                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Button runat="server" Text="Nova Carga" ID="btnNovaCargaAereo" CssClass="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_CargaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_CargaAereo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_CargaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblErro_CargaAereo1" runat="server"></asp:Label>
                                            </div>
                                            <div class="table-responsive tableFixHead" id="div3" runat="server">

                                                <asp:GridView ID="dgvCargaAereo" DataKeyNames="ID_CARGA_BL" DataSourceID="dsCargaAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_CARGA_BL" HeaderText="#" SortExpression="ID_CARGA_BL" />
                                                        <asp:BoundField DataField="ID_TIPO_CARGA" HeaderText="Tipo Carga" SortExpression="ID_TIPO_CARGA" />
                                                        <asp:BoundField DataField="QT_MERCADORIA" HeaderText="Qtd. Volume" SortExpression="QT_MERCADORIA" />
                                                        <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso Bruto" SortExpression="VL_PESO_BRUTO" />
                                                        <asp:BoundField DataField="VL_M3" HeaderText="M3" SortExpression="VL_M3" />
                                                        <asp:BoundField DataField="NCM" HeaderText="NCM" SortExpression="NCM" />
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                    Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                    Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i>
                                </div>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_CARGA_BL") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCargaAereo" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaAereo" />

                                        </Triggers>
                                    </asp:UpdatePanel>




                                    <ajaxToolkit:ModalPopupExtender ID="mpeCargaAereo" runat="server" PopupControlID="Panel1" TargetControlID="btnNovaCargaAereo"></ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Carga - Aereo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccess_CargaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccess_CargaAereo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErro_CargaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblErro_CargaAereo2" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3" style="display: none">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código:</label>
                                                                        <asp:TextBox ID="txtID_CargaAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo de Carga:</label>
                                                                        <asp:DropDownList ID="ddlMercadoria_CargaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCargas" DataValueField="ID_TIPO_CARGA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Embalagem:</label>
                                                                        <asp:DropDownList ID="ddlEmbalagem_CargaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Qtd. Mercadoria:</label>
                                                                        <asp:TextBox ID="txtQtdVolume_CargaAereo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Peso Bruto:</label>
                                                                        <asp:TextBox ID="txtPesoBruto_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Peso Cubado:</label>
                                                                        <asp:TextBox ID="txtPesoVolumetrico_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Peso Taxado:</label>
                                                                        <asp:TextBox ID="txtPesoTaxado_CargaAereo" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">


                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Busca NCM:</label>
                                                                        <asp:TextBox ID="txtIDNCM_CargaAereo" runat="server" Style="display: none"></asp:TextBox>
                                                                        <asp:TextBox ID="txtNCMFiltro_CargaAereo" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">NCM:</label>
                                                                        <asp:DropDownList ID="ddlNCM_CargaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsNCM_CargaAereo" DataValueField="ID_NCM">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Grupo NCM:</label>
                                                                        <asp:TextBox ID="txtGrupoNCM_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>


                                                            <div class="row" style="display: none">

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Comprimento:</label><small style="color: gray"> (cm)</small>
                                                                        <asp:TextBox ID="txtComprimento_CargaAereo" runat="server" CssClass="form-control valores"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Largura:</label><small style="color: gray"> (cm)</small>
                                                                        <asp:TextBox ID="txtLargura_CargaAereo" runat="server" CssClass="form-control valores"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Altura:</label><small style="color: gray"> (cm)</small>
                                                                        <asp:TextBox ID="txtAltura_CargaAereo" runat="server" CssClass="form-control valores"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" runat="server" id="divMedidasAereo" style="display: none">
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Qtd. Embalagem:</label>
                                                                        <asp:TextBox ID="txtQtdCaixasAereo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Comprimento:</label><small style="color: gray"> (cm)</small>
                                                                        <asp:TextBox ID="txtComprimentoMercadoriaAereo" runat="server" CssClass="form-control valores" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Largura:</label><small style="color: gray"> (cm)</small>
                                                                        <asp:TextBox ID="txtLarguraMercadoriaAereo" runat="server" CssClass="form-control valores" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Altura:</label><small style="color: gray"> (cm)</small>
                                                                        <asp:TextBox ID="txtAlturaMercadoriaAereo" runat="server" CssClass="form-control valores" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <div class="form-group">
                                                                        <label class="control-label" style="color: white">Adicionar:</label>
                                                                        <asp:ImageButton ID="btnAdicionarMedidasAereo" src="Content/imagens/plus.png" runat="server" Visible="false" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="table-responsive tableFixHead" style="max-height: 200px">
                                                                        <asp:GridView ID="dgvMedidasAereo" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="Id" DataSourceID="dsMedidasAereo" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                <asp:BoundField DataField="QTD_CAIXA" HeaderText="Qtd. Caixas" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="VL_COMPRIMENTO" HeaderText="Comprimento (cm)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="VL_LARGURA" HeaderText="Largura (cm)" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="VL_ALTURA" HeaderText="Altura (cm)" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" Text="Excluir" ID="ButtonExcluir" CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="divMercadoriaCNTR_Aereo" runat="server" style="display: block">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Descrição da Mercadoria:</label>
                                                                        <asp:TextBox ID="txtDescMercadoria_CargaAereo" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar" ID="btnSalvar_CargaAereo" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_CargaAereo" Text="Close" />
                                                        </div>

                                                    </div>

                                                </div>

                                                <%--                                                 <ajaxToolkit:ModalPopupExtender ID="mpeNCM_CargaAereo" runat="server" PopupControlID="PanelNCM_CargaAereo" TargetControlID="ddlNCM_CargaAereo" CancelControlID="btnFecharNCM_CargaAereo"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNCM_CargaAereo" runat="server" CssClass="modalPopup" Style="display: none;">
                                      
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" >Selecione um NCM</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />

                                                            <asp:Label ID="Label2" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNCMFiltro_CargaAereo" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNCM_CargaAereo" runat="server" AutoPostBack="true" DataTextField="NCM" DataSourceID="dsNCM_CargaAereo" DataValueField="ID_NCM" Style="text-align:justify;font-size:12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNCM_CargaAereo" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNCM_CargaAereo" Text="Salvar NCM" />
                                                        </div>


                                                    </div>
                                                </div>
                               
                                    </asp:Panel>--%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtNCMFiltro_CargaAereo" />
                                                <%--                                                <asp:AsyncPostBackTrigger ControlID="btnSalvarNCM_CargaAereo" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedidasAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlEstufagem_BasicoAereo" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaAereo" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvMedidasAereo" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>


                                </div>
                                <div class="tab-pane fade" id="TaxasAereo">
                                    <br />
                                    <div class="row">

                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Button runat="server" Text="Nova Taxa" ID="btnNovaTaxaAereo" CssClass="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>

                                            <div class="alert alert-success" id="divSuccess_TaxaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_TaxaAereo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_TaxaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblErro_TaxaAereo1" runat="server"></asp:Label>
                                            </div>

                                            <div class="table-responsive tableFixHead" id="div12" runat="server">
                                                <br />
                                                COMPRAS:
                                                                                                <asp:GridView ID="dgvTaxaAereoCompras" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasAereoCompras" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="ID_BL_TAXA" SortExpression="ID_BL_TAXA" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblID_BL_TAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                                                                        <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="PARCEIRO" SortExpression="PARCEIRO_EMPRESA" />
                                                                                                        <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                                                                        <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                                                                        <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                                                                        <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                                                        <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                                                                                        <asp:BoundField DataField="DECLARADO" HeaderText="DECLARADO" SortExpression="DECLARADO" />
                                                                                                        <asp:BoundField DataField="PROFIT" HeaderText="PROFIT" SortExpression="PROFIT" />
                                                                                                        <asp:TemplateField HeaderText="ORIGEM" SortExpression="ORIGEM">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblORIGEM" runat="server" Text='<%# Eval("ORIGEM") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="ATIVA?" SortExpression="ATIVA">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblAtiva" runat="server" Text='<%# Eval("ATIVA") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <asp:TemplateField HeaderText="HISTÓRICO" SortExpression="HISTORICO">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblTemHistorico" runat="server" Text='<%# Eval("HISTORICO") %>' Visible="false"></asp:Label>
                                                                                                                <asp:ImageButton ID="ImageButton1" src="Content/imagens/hist.png" runat="server" CommandArgument='<%# Eval("ID_BL_TAXA") %>' ToolTip="Histórico" CommandName="Historico" />
                                                                                                                <asp:Label ID="lblTaxa" Visible="False" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                                                                    Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                                                                    Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i>
    </div>
                                                                                                                </asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                                                </asp:GridView>


                                                <asp:Button runat="server" ID="Button2" CssClass="btn btn-block btn-primary" Style="display: none" />
                                                <ajaxToolkit:ModalPopupExtender ID="mpeHistoricoAereo" runat="server" PopupControlID="pnHistoricoAereo" TargetControlID="Button2" CancelControlID="btnFecharHistoricoAereo"></ajaxToolkit:ModalPopupExtender>
                                                <asp:Panel ID="pnHistoricoAereo" runat="server" CssClass="modalPopup" Style="display: none;">
                                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Historico de Status</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-12"> 
                                                                       <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">
                                                                            <asp:GridView ID="dgvHistoricoAereo" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="ID_INATIVACAO" DataSourceID="dsHistorico" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_INATIVACAO" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                 <asp:BoundField DataField="STATUS" HeaderText="Ativo?" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NOME" HeaderText="Usuário" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="DT_INATIVACAO" HeaderText="Data" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NM_MOTIVO_INATIVACAO" HeaderText="Motivo" ItemStyle-HorizontalAlign="Center" />
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="Historico" />
                                                                        </asp:GridView>

                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharHistoricoAereo" text="Close" />                                                                
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                                </asp:Panel>


                                                VENDAS:
                                                <asp:GridView ID="dgvTaxaAereoVendas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasAereoVendas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID_BL_TAXA" SortExpression="ID_BL_TAXA" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID_BL_TAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                        <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="PARCEIRO" SortExpression="PARCEIRO_EMPRESA" />
                                                        <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                        <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                        <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                        <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                        <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                                        <asp:BoundField DataField="DECLARADO" HeaderText="DECLARADO" SortExpression="DECLARADO" />
                                                        <asp:BoundField DataField="PROFIT" HeaderText="PROFIT" SortExpression="PROFIT" />
                                                        <asp:TemplateField HeaderText="ORIGEM" SortExpression="ORIGEM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblORIGEM" runat="server" Text='<%# Eval("ORIGEM") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ATIVA?" SortExpression="ATIVA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAtiva" runat="server" Text='<%# Eval("ATIVA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                    Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                    Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>

                                                <div>
                                                    <asp:Label ID="lblDiferencaAereo" runat="server" Style="color: blue"></asp:Label>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaAereoVendas" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoVendas" />
                                            <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaAereoCompras" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoCompras" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>




                                    <ajaxToolkit:ModalPopupExtender ID="mpeTaxaAereo" runat="server" PopupControlID="Panel4" TargetControlID="btnNovaTaxaAereo"></ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Taxa - Aereo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccess_TaxaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccess_TaxaAereo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErro_TaxaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblErro_TaxaAereo2" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row" style="display: none">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código:</label>
                                                                        <asp:TextBox ID="txtID_TaxaAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo de despesa:</label>
                                                                        <asp:DropDownList ID="ddlDespesa_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" AutoPostBack="true" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4" style="display: none">
                                                                    <div class="form-group">
                                                                        <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbPremiacao_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Premiação"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbProfit_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;PROFIT"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbDeclarado_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Declarado"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo Pagamento:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlTipoPagamento_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base de cálculo:</label>
                                                                        <asp:DropDownList ID="ddlBaseCalculo_TaxaAereo" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Qtd. Base de cálculo:</label>
                                                                        <asp:TextBox ID="txtQtdBaseCalculo_TaxaAereo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Origem Serviço:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlOrigemPagamento_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Destinatário de cobrança:</label>
                                                                        <asp:DropDownList ID="ddlDestinatarioCob_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_DESTINATARIO_COBRANCA" DataSourceID="dsDestinatarioCobranca" DataValueField="ID_DESTINATARIO_COBRANCA"></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <strong>
                                                                            <asp:Label CssClass="control-label" runat="server" ID="lblTipoEmpresa_Aereo" Text="Fornecedor:" /></strong>
                                                                        <asp:DropDownList ID="ddlEmpresa_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsFornecedorAereo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row" runat="server" id="divCompraAereo">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda da compra:</label>
                                                                        <asp:DropDownList ID="ddlMoedaCompra_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Mínimo de Compra:</label>
                                                                        <asp:TextBox ID="txtMinCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor da Compra:</label>
                                                                        <asp:TextBox ID="txtValorCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" runat="server" id="divVendaAereo">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda de Venda:</label>
                                                                        <asp:DropDownList ID="ddlMoedaVenda_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Mínimo de Venda:</label>
                                                                        <asp:TextBox ID="txtMinVenda_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor da Venda:</label>
                                                                        <asp:TextBox ID="txtValorVenda_TaxaAereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Observações:</label>
                                                                        <asp:TextBox ID="txtObs_TaxaAereo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="1000"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar Taxa" ID="btnSalvar_TaxaAereo" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_TaxaAereo" Text="Close" />
                                                        </div>



                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoCompras" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoVendas" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlDespesa_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="txtValorVenda_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlBaseCalculo_TaxaAereo" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </div>

                                <div class="tab-pane fade" id="DocAereo">

                                    <div class="alert alert-danger" id="divErroUploadAereo" runat="server" visible="false">
                                        <asp:Label ID="lblErroUploadAereo" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-success" id="divSuccessUploadAereo" runat="server" visible="false">
                                        <asp:Label ID="lblSuccessUploadAereo" runat="server">
                                             Ação realizada com sucesso!
                                        </asp:Label>
                                    </div>
                                    <br />
                                    <div class="row">


                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Tipo de arquivo:</label>
                                                <asp:DropDownList ID="ddlTipoArquivoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_ARQUIVO" DataSourceID="dsTipoArquivo" DataValueField="ID_TIPO_ARQUIVO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:FileUpload ID="FileUploadAereo" CssClass="form-control" runat="server" Visible="true" Style="display: block" onchange="Javascript: VerificaTamanhoArquivoA();"></asp:FileUpload>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtUPAereo" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:TextBox ID="txtArquivoSelecionadoAereo" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:GridView ID="dgvArquivosAereo" runat="server" AutoGenerateColumns="false" EmptyDataText="Nenhum arquivo enviado" DataKeyNames="ID_ARQUIVO" DataSourceID="dsUploadsAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" Style="max-height: 400px; overflow: auto;" AllowSorting="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID_ARQUIVO" runat="server" Text='<%# Eval("ID_ARQUIVO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nome do Arquivo" HeaderStyle-ForeColor="#337ab7">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNM_ARQUIVO" runat="server" Text='<%# Eval("NM_ARQUIVO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NM_TIPO_ARQUIVO" HeaderText="Tipo do Arquivo" SortExpression="NM_TIPO_ARQUIVO" />
                                                            <asp:BoundField DataField="NOME" HeaderText="Usuário" SortExpression="NOME" />
                                                            <asp:BoundField DataField="DT_UPLOAD" HeaderText="Data/Hora" SortExpression="DT_UPLOAD" />
                                                            <asp:TemplateField HeaderText="Ativo para clientes?" HeaderStyle-ForeColor="#337ab7">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckAtivoClientes" Checked='<%# Eval("FL_ATIVO_CLIENTES") %>' runat="server" AutoPostBack="true" OnCheckedChanged="ckAtivoClientes_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href="VisualizarUpload.aspx?id=<%# Eval("ID_ARQUIVO") %>" target="_blank" style="font-size: medium" data-toggle="tooltip" data-placement="top" title="Visualizar">
                                                                        <asp:Label ID="lblBotaoVisualizar" runat="server" Text="Visualizar" /></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandName="Download" CommandArgument='<%# Eval("CAMINHO_ARQUIVO") %>' runat="server" Font-Size="medium"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDeleta" Text="Deletar" OnClientClick="javascript:return confirm('Deseja realmente excluir este arquivo?');" CommandName="Excluir" CommandArgument='<%# Eval("ID_ARQUIVO") & "|" & Eval("CAMINHO_ARQUIVO") %>' runat="server" Font-Size="medium" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 col-sm-offset-6" style="display: none">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimparUploadAereo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-sm-offset-9">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnUploadAereo" OnClientClick="javascript:return confirm('Deseja realmente realizar o upload?');" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnLimparUploadAereo" />
                                            <asp:PostBackTrigger ControlID="btnUploadAereo" />
                                            <asp:PostBackTrigger ControlID="dgvArquivosAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                </div>

                                <div class="tab-pane fade" id="ObsAereo">
                                    <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_ObsAereo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_ObsAereo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_ObsAereo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_ObsAereo" runat="server"></asp:Label>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação operacional interna:</label>
                                                        <asp:TextBox ID="txtObsOperacional_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação comercial:</label>
                                                        <asp:TextBox ID="txtObsComercial_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação agente internacional:</label>
                                                        <asp:TextBox ID="txtObsAgente_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação cliente:</label>
                                                        <asp:TextBox ID="txtObsCliente_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Obs. Cliente (COTAÇÃO):</label>
                                                        <asp:TextBox ID="txtObsCliente_CotacaoAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Obs. Operacional (COTAÇÃO):</label>
                                                        <asp:TextBox ID="txtObsOper_CotacaoAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_ObsAereo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_ObsAereo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_ObsAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_ObsAereo" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="RefAereo">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>

                                            <br />
                                            <div class="row linhabotao text-center" style="margin-left: 20px">

                                                <div>
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Tipo:</label>
                                                            <asp:DropDownList ID="ddlTipoRefAereo" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                                <asp:ListItem Value="CNEE">CNEE</asp:ListItem>
                                                                <asp:ListItem Value="SHIPPER">SHIPPER</asp:ListItem>
                                                                <asp:ListItem Value="AUXILIAR">AUXILIAR</asp:ListItem>
                                                                <asp:ListItem Value="COMERCIAL">COMERCIAL</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>

                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label class="control-label">Referência:</label>
                                                            <asp:TextBox ID="txtRefAereo" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>

                                                        </div>

                                                    </div>


                                                    <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                        <div class="form-group">
                                                            <asp:Button runat="server" Text="Gravar" ID="btnGravar_RefAereo" CssClass="btn btn-success" />

                                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar_RefAereo" CssClass="btn btn-danger" />

                                                        </div>
                                                    </div>


                                                    <asp:TextBox ID="txtID_RefAereo" Style="display: none" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>


                                                </div>
                                            </div>
                                            <br />

                                            <div class="table-responsive tableFixHead" id="div15" runat="server">

                                                <div class="alert alert-success" id="divSuccess_RefAereo" runat="server" visible="false">
                                                    <asp:Label ID="lblSuccess_RefAereo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                </div>
                                                <div class="alert alert-danger" id="divErro_RefAereo" runat="server" visible="false">
                                                    <asp:Label ID="lblErro_RefAereo" runat="server"></asp:Label>
                                                </div>
                                                <asp:GridView ID="dgvRefAereo" DataKeyNames="ID_REFERENCIA_CLIENTE" DataSourceID="dsRefAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>

                                                        <asp:BoundField DataField="ID_REFERENCIA_CLIENTE" HeaderText="#" SortExpression="ID_REFERENCIA_CLIENTE" />
                                                        <asp:BoundField DataField="ID_BL" HeaderText="ID_BL" SortExpression="ID_BL" />
                                                        <asp:BoundField DataField="NR_REFERENCIA_CLIENTE" HeaderText="NR_REFERENCIA_CLIENTE" SortExpression="NR_REFERENCIA_CLIENTE" />
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditarParcela" runat="server" CausesValidation="False" CommandName="visualizar" CssClass="btn btn-info" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>'><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            <ControlStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                            <br />


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvRefAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar_RefAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_RefAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                </div>
                            </div>
                        </div>

                    </div>





                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-ajuda">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Sobre NVOCC:</h4>
                </div>
                <div class="modal-body">
                    <strong>Objetivo:</strong>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT 0, 'Selecione' FROM [dbo].[TB_ORIGEM_PAGAMENTO] ORDER BY ID_ORIGEM_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNCM_CargaMaritimo1" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NCM,CD_NCM +' - '+ NM_NCM AS NCM FROM [dbo].[TB_NCM] 
        WHERE  (ID_NCM =  @ID_NCM ) or (NM_NCM like '%' + @Nome + '%' Or CD_NCM like '%' + @Nome + '%')
        union SELECT  0, '      Selecione' ORDER BY ID_NCM ">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNCMFiltro_CargaMaritimo" />
            <asp:ControlParameter Name="ID_NCM" Type="Int32" ControlID="txtIDNCM_CargaMaritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsNCM_CargaAereo1" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NCM,CD_NCM +' - '+ NM_NCM AS NCM FROM [dbo].[TB_NCM] WHERE (NM_NCM like '%' + @Nome + '%') or (CD_NCM like '%' + @Nome + '%' ) or (ID_NCM =  @ID_NCM ) union SELECT  0, '      Selecione' ORDER BY ID_NCM ">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNCMFiltro_CargaAereo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_NCM" Type="Int32" ControlID="txtIDNCM_CargaAereo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNCM_CargaAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NCM,CD_NCM +' - '+ NM_NCM as Descricao FROM TB_NCM WHERE NM_NCM  like '%' + @NM_NCM + '%'
            union SELECT ID_NCM,CD_NCM +' - '+ NM_NCM as Descricao FROM TB_NCM WHERE  CD_NCM  like '%' + @NM_NCM + '%' 
            union SELECT ID_NCM,CD_NCM +' - '+ NM_NCM as Descricao FROM TB_NCM WHERE ID_NCM =  @ID
           union SELECT  0, ' Selecione' ORDER BY Descricao">
        <SelectParameters>
            <asp:ControlParameter Name="ID" Type="Int32" ControlID="txtIDNCM_CargaAereo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_NCM" Type="String" ControlID="txtNCMFiltro_CargaAereo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNCM_CargaMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NCM,CD_NCM +' - '+ NM_NCM as Descricao FROM TB_NCM WHERE NM_NCM  like '%' + @NM_NCM + '%'
            union SELECT ID_NCM,CD_NCM +' - '+ NM_NCM as Descricao FROM TB_NCM WHERE  CD_NCM  like '%' + @NM_NCM + '%' 
            union SELECT ID_NCM,CD_NCM +' - '+ NM_NCM as Descricao FROM TB_NCM WHERE ID_NCM =  @ID
           union SELECT  0, ' Selecione' ORDER BY Descricao">
        <SelectParameters>
            <asp:ControlParameter Name="ID" Type="Int32" ControlID="txtIDNCM_CargaMaritimo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_NCM" Type="String" ControlID="txtNCMFiltro_CargaMaritimo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM [dbo].[TB_ITEM_DESPESA]
union SELECT 0, ' Selecione' FROM [dbo].[TB_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT 0, '      Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY NM_BASE_CALCULO_TAXA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPortoMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = 1 union SELECT  0, '      Selecione' ORDER BY NM_PORTO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPortoAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO, CONVERT(VARCHAR,CD_PORTO) + ' - ' + NM_PORTO AS NM_PORTO FROM [dbo].[TB_PORTO]  WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = 4 union SELECT  0, '       Selecione' ORDER BY NM_PORTO "></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsComex" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM [dbo].[TB_TIPO_COMEX]
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_COMEX] ORDER BY ID_TIPO_COMEX"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTransportador_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_TRANSPORTADOR)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_TRANSPORTADOR" Type="Int32" ControlID="txtCodTransportador_Maritimo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeTransportador_Maritimo" DefaultValue="NULL" />

        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTransportador_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_TRANSPORTADOR)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_TRANSPORTADOR" Type="Int32" ControlID="txtCodTransportador_Aereo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeTransportador_Aereo" DefaultValue="NULL" />

        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsRodoviario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_RODOVIARIO = 1
        union SELECT 0, 'Selecione'   ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsArmazemDesembaraco" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_ARMAZEM_DESEMBARACO  = 1
union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFornecedorMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN((SELECT ID_PARCEIRO_EXPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_CLIENTE FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_IMPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_TRANSPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_INDICADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_COMISSARIA FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_AGENTE_INTERNACIONAL FROM TB_BL WHERE ID_BL = @ID_BL 
UNION
SELECT ID_PARCEIRO_RODOVIARIO FROM TB_BL WHERE ID_BL = @ID_BL
UNION
SELECT DISTINCT ID_PARCEIRO_EMPRESA FROM TB_BL_TAXA WHERE ID_BL = @ID_BL AND ID_PARCEIRO_EMPRESA IS NOT NULL)) OR FL_PRESTADOR = 1 
union SELECT 0, ' Selecione'  
ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFornecedorAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN((SELECT ID_PARCEIRO_EXPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_CLIENTE FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_IMPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_TRANSPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_ARMAZEM_DESEMBARACO FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_RODOVIARIO FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_COMISSARIA FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_INDICADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION
SELECT ID_PARCEIRO_AGENTE_INTERNACIONAL FROM TB_BL WHERE ID_BL = @ID_BL
UNION
SELECT ID_PARCEIRO_RODOVIARIO FROM TB_BL WHERE ID_BL = @ID_BL
UNION
SELECT DISTINCT ID_PARCEIRO_EMPRESA FROM TB_BL_TAXA WHERE ID_BL = @ID_BL AND ID_PARCEIRO_EMPRESA IS NOT NULL)) OR FL_PRESTADOR = 1 
union SELECT 0, ' Selecione'  
ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INCOTERM, cast((CD_INCOTERM)as varchar)+ ' - '+ NM_INCOTERM as NM_INCOTERM FROM TB_INCOTERM 
union SELECT  0, 'Selecione' ORDER BY ID_INCOTERM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissaria_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_COMISSARIA)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_COMISSARIA" Type="Int32" ControlID="txtCodComissaria_Maritimo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeComissaria_Maritimo" DefaultValue="NULL" />

        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissaria_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_COMISSARIA)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_COMISSARIA" Type="Int32" ControlID="txtCodComissaria_Aereo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeComissaria_Aereo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsExportador_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_EXPORTADOR)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_EXPORTADOR" Type="Int32" ControlID="txtCodExportador_Aereo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeExportador_Aereo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsExportador_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_EXPORTADOR)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_EXPORTADOR" Type="Int32" ControlID="txtCodExportador_Maritimo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeExportador_Maritimo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgente_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_AGENTE)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_AGENTE" Type="Int32" ControlID="txtCodAgente_Maritimo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeAgente_Maritimo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgente_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE   (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_AGENTE)
union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_AGENTE" Type="Int32" ControlID="txtCodAgente_Aereo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeAgente_Aereo" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFrequencia" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_FREQUENCIA, NM_TIPO_FREQUENCIA FROM [dbo].[TB_TIPO_FREQUENCIA]
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_FREQUENCIA] ORDER BY ID_TIPO_FREQUENCIA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCargas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_CARGA] ORDER BY ID_TIPO_CARGA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDestinatarioCobranca" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_DESTINATARIO_COBRANCA,NM_DESTINATARIO_COBRANCA from TB_DESTINATARIO_COBRANCA
union SELECT  0, 'Selecione' ORDER BY ID_DESTINATARIO_COBRANCA
"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsServicoMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO NOT IN (2,5)
union SELECT  0, 'Selecione' ORDER BY ID_SERVICO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsServicoAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO IN (2,5)
union SELECT  0, 'Selecione' ORDER BY ID_SERVICO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsStatusPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_STATUS_PAGAMENTO, NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO
union SELECT  0, 'Selecione' ORDER BY ID_STATUS_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' ORDER BY ID_TIPO_CONTAINER"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCNTR" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CNTR_BL, NR_CNTR FROM TB_CNTR_BL WHERE ID_TIPO_CNTR = 0
union SELECT 0, 'Selecione' ORDER BY ID_CNTR_BL">
        <%--<SelectParameters>
            <asp:ControlParameter Name="TIPO_CNTR" Type="Int32" ControlID="ddlNumeroCNTR_CargaMaritimo" />
        </SelectParameters>--%>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMercadoria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MERCADORIA, NM_MERCADORIA FROM [dbo].[TB_MERCADORIA]
union SELECT 0, 'Selecione' ORDER BY ID_MERCADORIA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM]
union SELECT 0, 'Selecione' ORDER BY ID_TIPO_ESTUFAGEM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_PAGAMENTO, NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCliente_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_CLIENTE)
union SELECT  0,'', ' Selecione' ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeCliente_Maritimo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_CLIENTE" Type="Int32" ControlID="txtCodCliente_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTranspRodoviario_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_RODOVIARIO=1 AND ((NM_RAZAO  like '%' + @NM_RAZAO + '%' and FL_ATIVO = 1) or (ID_PARCEIRO =  @ID_PARCEIRO_TRANSP_RODOVIARIO))
union SELECT  0,'', ' Selecione' ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeTranspRodoviario_BasicoMaritimo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_TRANSP_RODOVIARIO" Type="Int32" ControlID="txtCodTranspRodoviario_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTranspRodoviario_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_RODOVIARIO=1 AND ((NM_RAZAO  like '%' + @NM_RAZAO + '%' and FL_ATIVO = 1) or (ID_PARCEIRO =  @ID_PARCEIRO_TRANSP_RODOVIARIO))
union SELECT  0,'', ' Selecione' ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeTranspRodoviario_BasicoAereo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_TRANSP_RODOVIARIO" Type="Int32" ControlID="txtCodTranspRodoviario_Aereo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsImportador_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_IMPORTADOR)
union SELECT  0,'', ' Selecione' ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeImportador_Maritimo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_IMPORTADOR" Type="Int32" ControlID="txtCodImportador_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsCliente_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_CLIENTE)
union SELECT  0,'', ' Selecione' ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeCliente_Aereo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_CLIENTE" Type="Int32" ControlID="txtCodCliente_Aereo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsImportador_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_IMPORTADOR)
union SELECT  0,'', ' Selecione' ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_IMPORTADOR" Type="Int32" ControlID="txtCodImportador_Aereo" DefaultValue="0" />
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeImportador_Aereo" DefaultValue="NULL" />

        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsIndicador_Maritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_INDICADOR)
union SELECT  0, '',' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeIndicador_Maritimo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_INDICADOR" Type="Int32" ControlID="txtCodIndicador_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsIndicador_Aereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO_INDICADOR)
union SELECT  0, '',' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeIndicador_Aereo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO_INDICADOR" Type="Int32" ControlID="txtCodIndicador_Aereo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsRefMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_REFERENCIA_CLIENTE,ID_BL,NR_REFERENCIA_CLIENTE,ID_COTACAO,TIPO FROM TB_REFERENCIA_CLIENTE WHERE ID_BL = @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsRefAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_REFERENCIA_CLIENTE,ID_BL,NR_REFERENCIA_CLIENTE,ID_COTACAO,TIPO FROM TB_REFERENCIA_CLIENTE WHERE ID_BL = @ID_BL
">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:SqlDataSource ID="dsCargaMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CARGA_BL,
ID_CNTR_BL,
(SELECT NR_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = A.ID_CNTR_BL)CONTAINER,
(SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = A.ID_TIPO_CNTR)TIPO_CNTR,
(SELECT QT_DIAS_FREETIME FROM TB_CNTR_BL WHERE ID_CNTR_BL = A.ID_CNTR_BL)QT_DIAS_FREETIME,
ID_MERCADORIA,QT_MERCADORIA,
(SELECT NM_TIPO_CARGA FROM TB_TIPO_CARGA WHERE ID_TIPO_CARGA = A.ID_TIPO_CARGA)ID_TIPO_CARGA,
VL_PESO_BRUTO,
VL_M3,
ID_NCM,
DS_GRUPO_NCM,
CASE WHEN DS_GRUPO_NCM IS NULL THEN 
(SELECT NM_NCM FROM TB_NCM WHERE ID_NCM = A.ID_NCM) ELSE DS_GRUPO_NCM END NCM 
FROM TB_CARGA_BL A WHERE ID_BL = @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCargaAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CARGA_BL,
ID_CNTR_BL,
(SELECT NR_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = A.ID_CNTR_BL)CONTAINER,
(SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = A.ID_TIPO_CNTR)TIPO_CNTR,
(SELECT QT_DIAS_FREETIME FROM TB_CNTR_BL WHERE ID_CNTR_BL = A.ID_CNTR_BL)QT_DIAS_FREETIME,
ID_MERCADORIA,QT_MERCADORIA,
(SELECT NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE ID_TIPO_CARGA = A.ID_TIPO_CARGA)ID_TIPO_CARGA,
VL_PESO_BRUTO,
VL_M3,
ID_NCM,
DS_GRUPO_NCM,
CASE WHEN DS_GRUPO_NCM IS NULL THEN 
(SELECT NM_NCM FROM TB_NCM WHERE ID_NCM = A.ID_NCM) ELSE DS_GRUPO_NCM END NCM 
FROM TB_CARGA_BL A WHERE ID_BL = @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasAereoCompras" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [FN_TAXAS_BL](@ID_BL,'P') order by ID_BL_TAXA desc ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasAereoVendas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [FN_TAXAS_BL](@ID_BL,'R') order by ID_BL_TAXA desc ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasMaritimoVendas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [FN_TAXAS_BL](@ID_BL,'R') order by ID_BL_TAXA desc ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasMaritimoCompras" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [FN_TAXAS_BL](@ID_BL,'P') order by ID_BL_TAXA desc ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsDivisaoProfit" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_DIVISAO_PROFIT,NM_TIPO_DIVISAO_PROFIT FROM [dbo].TB_TIPO_DIVISAO_PROFIT
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_DIVISAO_PROFIT"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsFinalDestination" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CIDADE,upper( NM_CIDADE) + ' - ' + (SELECT SIGLA_ESTADO FROM TB_ESTADO B WHERE B.ID_ESTADO = A.ID_ESTADO) AS NM_CIDADE FROM [dbo].[TB_CIDADE] A  union SELECT  0 as Id, '  Selecione' as Descricao FROM [dbo].[TB_CIDADE] A Order by NM_CIDADE"></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsMedidasAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID
      ,ID_BL
	  ,ID_CARGA_BL
      ,ID_COTACAO_MERCADORIA
      ,ID_COTACAO_MERCADORIA_DIMENSAO
      ,QTD_CAIXA
      ,VL_LARGURA
      ,VL_ALTURA
      ,VL_COMPRIMENTO
  FROM TB_CARGA_BL_DIMENSAO 
    WHERE ID_CARGA_BL = @ID_CARGA_BL ORDER BY ID DESC ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_CARGA_BL" Type="Int32" ControlID="txtID_CargaAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoAeronave" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_TIPO_AERONAVE,NM_TIPO_AERONAVE from TB_TIPO_AERONAVE
union 
SELECT  0, '      Selecione' ORDER BY ID_TIPO_AERONAVE "></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsUploadsMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand=" SELECT A.ID_ARQUIVO,A.NM_ARQUIVO,C.NOME,B.NM_TIPO_ARQUIVO,A.DT_UPLOAD,A.FL_ATIVO_CLIENTES,A.ID_BL,A.ID_COTACAO,A.CAMINHO_ARQUIVO FROM TB_UPLOADS  A
 INNER JOIN TB_TIPO_ARQUIVO B ON A.ID_TIPO_ARQUIVO = B.ID_TIPO_ARQUIVO
INNER JOIN TB_USUARIO C ON A.ID_USUARIO = C.ID_USUARIO
    WHERE A.ID_BL = @ID_BL ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsUploadsAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand=" SELECT A.ID_ARQUIVO,A.NM_ARQUIVO,C.NOME,B.NM_TIPO_ARQUIVO,A.DT_UPLOAD,A.FL_ATIVO_CLIENTES,A.ID_BL,A.ID_COTACAO,A.CAMINHO_ARQUIVO FROM TB_UPLOADS  A
 INNER JOIN TB_TIPO_ARQUIVO B ON A.ID_TIPO_ARQUIVO = B.ID_TIPO_ARQUIVO
INNER JOIN TB_USUARIO C ON A.ID_USUARIO = C.ID_USUARIO
    WHERE A.ID_BL = @ID_BL ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoArquivo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ARQUIVO,NM_TIPO_ARQUIVO FROM TB_TIPO_ARQUIVO
union 
SELECT  0, '      Selecione' ORDER BY ID_TIPO_ARQUIVO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsHistorico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INATIVACAO,CASE WHEN ISNULL(FL_TAXA_INATIVA,0) = 0 THEN 'ATIVO' ELSE 'INATIVO' END STATUS,NOME,DT_INATIVACAO,CASE WHEN ISNULL(C.FL_PRECISA_DESCR,0) = 1 THEN
C.NM_MOTIVO_INATIVACAO + ': ' +A.DS_MOTIVO_INATIVACAO ELSE C.NM_MOTIVO_INATIVACAO END NM_MOTIVO_INATIVACAO,A.DS_MOTIVO_INATIVACAO FROM TB_INATIVACAO_TAXAS A INNER JOIN TB_USUARIO B ON A.ID_USUARIO_INATIVACAO = B.ID_USUARIO INNER JOIN TB_MOTIVO_INATIVACAO C ON C.ID_MOTIVO_INATIVACAO = A.ID_MOTIVO_INATIVACAO  WHERE A.ID_BL_TAXA = @ID_BL_TAXA ORDER BY DT_INATIVACAO DESC">
        <SelectParameters>
            <asp:Parameter Name="ID_BL_TAXA" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsStatusFreteAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_STATUS_FRETE_AGENTE, NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE 
union SELECT 0, 'Selecione' FROM TB_STATUS_FRETE_AGENTE ORDER BY ID_STATUS_FRETE_AGENTE"></asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>


        $(window).load(function () {
            var url = window.location.href;
            var Qtd = url.indexOf("&s=A");
            if (Qtd != -1) {
                console.log("A");
                var UP = document.getElementById('<%= txtUPAereo.ClientID %>').value;
                if (UP == 1) {
                    $('.nav-tabs a[href="#Aereo"]').tab('show');
                    $('.nav-tabs a[href="#DocAereo"]').tab('show');
                    document.getElementById('<%= txtUPAereo.ClientID %>').value = 0;
                }
                else {
                    $('.nav-tabs a[href="#Aereo"]').tab('show');

                }

            } else {
                console.log("M");
                var UP = document.getElementById('<%= txtUPMaritimo.ClientID %>').value;

                if (UP == 1) {
                    $('.nav-tabs a[href="#DocMaritimo"]').tab('show');

                    document.getElementById('<%= txtUPMaritimo.ClientID %>').value = 0;
                }
                else {
                    $('.nav-tabs a[href="#Maritimo"]').tab('show');

                }
            }
        });



        $('#ajuda').on("click", function () {
            $('#modal-ajuda').modal('show');
        });

        function MBLMaritimo() {

            var IDMaster_BasicoMaritimo = document.getElementById('<%= txtIDMaster_BasicoMaritimo.ClientID %>').value;

            window.open('CadastrarMaster.aspx?id=' + IDMaster_BasicoMaritimo + "&s=M", '_blank');
        }

        function MBLAereo() {

            var IDMaster_BasicoAereo = document.getElementById('<%= txtIDMaster_BasicoAereo.ClientID %>').value;

            window.open('CadastrarMaster.aspx?id=' + IDMaster_BasicoAereo + "&s=A", '_blank');
        }


        function CapaMaritimo() {

            var ID_BasicoMaritimo = document.getElementById('<%= txtID_BasicoMaritimo.ClientID %>').value;
            console.log(ID_BasicoMaritimo);

            window.open('CapaProcesso.aspx?id=' + ID_BasicoMaritimo, '_blank');
        }


        function CapaAereo() {


            var ID_BasicoAereo = document.getElementById('<%= txtID_BasicoAereo.ClientID %>').value;
            console.log(ID_BasicoAereo);

            window.open('CapaProcesso.aspx?id=' + ID_BasicoAereo, '_blank');

        }


        function Func() {
            alert("O valor de venda é menor que o valor de compra!");
        }

        function VerificaTamanhoArquivoM() {
            var btn = document.getElementById('<%= btnUploadMaritimo.ClientID %>');
            var fi = document.getElementById('<%= FileUploadMaritimo.ClientID %>');
            var maxFileSize = 15728640; // 15MB -> 15 * 1024 * 1024
            if (fi.files.length > 0) {
                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize < maxFileSize) {
                        btn.style.display = 'block';
                    }
                    else {
                        alert("Arquivo excede tamanho permitido!");
                        fi.value = null;
                        btn.style.display = 'none';
                    }
                }
            }
            var valido = /^[\x00-\x7F]*$/.test(fi.value);
            if (valido) {
                btn.style.display = 'block';
                console.log("arquivo permitido!");
            }
            else {
                alert("O arquivo selecionado não é permitido!(caracteres inválidos)");
                fi.value = null;
                btn.style.display = 'none';
            }
        }

        function VerificaTamanhoArquivoA() {
            var btn = document.getElementById('<%= btnUploadAereo.ClientID %>');
            var fi = document.getElementById('<%= FileUploadAereo.ClientID %>');
            var maxFileSize = 15728640; // 15MB -> 15 * 1024 * 1024
            if (fi.files.length > 0) {
                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize < maxFileSize) {
                        btn.style.display = 'block';
                    }
                    else {
                        alert("Arquivo excede tamanho permitido!");
                        fi.value = null;
                        btn.style.display = 'none';
                    }
                }
            }
            var valido = /^[\x00-\x7F]*$/.test(fi.value);
            if (valido) {
                btn.style.display = 'block';
                console.log("arquivo permitido!");
            }
            else {
                alert("O arquivo selecionado não é permitido!(caracteres inválidos)");
                fi.value = null;
                btn.style.display = 'none';
            }
        }


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);

        function InIEvent() {
            $(".valores").on("keypress keyup blur", function (e) {
                console.log("entrou")
                var chr = String.fromCharCode(e.which);
                if ("1234567890,".indexOf(chr) < 0)
                    return false;
            });
        }


        $(document).ready(InIEvent);


    </script>
</asp:Content>
