﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" EnableEventValidation="False" CodeBehind="CadastrarMaster.aspx.vb" Inherits="NVOCC.Web.CadastrarMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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

        .imgHistDatas {
            margin-top: 8px !important;
        }
    </style>
    <div style="float: right; display: none">
        <a id="ajuda" href="#" title="Ajuda">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
                <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z" />
            </svg></a>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">MÓDULO OPERACIONAL - MASTER 
                        <asp:Label ID="lblMaster_Titulo" runat="server" />
                    </h3>
                </div>
                <div class="panel-body">

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
                                    <a href="#ContainerMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Containers
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
                                    <a href="#VinculosMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Vincular Houses
                                    </a>
                                </li>                                
                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="BasicoMaritimo">
                                     <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                    <div class="alert alert-success" id="divSuccess_BasicoMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblSuccess_BasicoMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                    </div>
                                    <div class="alert alert-danger" id="divErro_BasicoMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblErro_BasicoMaritimo" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-info" id="divinfo_BasicoMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblmsginfo_BasicoMaritimo" runat="server" Text="Favor revisar taxas locais do armador importadas!"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="display: none">
                                            <div class="form-group">
                                                <label class="control-label">Código:</label>
                                                <asp:TextBox ID="txtID_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número BL:</label>
                                                <asp:TextBox ID="txtNumeroBL_BasicoMaritimo" runat="server" CssClass="form-control BL"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" style="display: none">
                                            <div class="form-group">
                                                <label class="control-label">Número do Processo:</label>
                                                <asp:TextBox ID="txtProcesso_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
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
                                                <label class="control-label"></label>
                                                <asp:CheckBox ID="ckTrakingAutomaticoMaritimo" runat="server" CssClass="form-control" Checked="false" Visible="false" Text="&nbsp;&nbsp;Traking Automatico"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-offset-5 col-sm-1">
                                            <div class="form-group">
                                                <center> <label class="control-label">Doc. Conferido?</label><br />
                                                    <asp:CheckBox ID="ckDocConferidosMaritimo" runat="server" ></asp:CheckBox></center>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Transportador:</label>
                                                <asp:DropDownList ID="ddlTransportador_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                <asp:TextBox Style="display: none" ID="txtCodTransportador_Maritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto de Origem:</label>
                                                <asp:DropDownList ID="ddlOrigem_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto de Destino:</label>
                                                <asp:DropDownList ID="ddlDestino_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="true" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Week:</label>
                                                <asp:DropDownList ID="ddlWeekMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_WEEK" DataSourceID="dsWeekMaritimo" DataValueField="ID_WEEK"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Agente Internacional:</label>
                                                <asp:DropDownList ID="ddlAgente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Tipo de Pagamento:</label>
                                                <asp:DropDownList ID="ddlTipoPagamento_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Tipo de Estufagem:</label>
                                                <asp:DropDownList ID="ddlEstufagem_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data de emissão do BL:</label>
                                                <asp:TextBox ID="txtEmissaoBL_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Armazém de Atracação:</label>
                                                <asp:DropDownList ID="ddlArmazemAtracacao_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsArmazemAtracacao" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtCodArmazemAtracacao_Maritimo" runat="server" Style="display: none" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Armazém de Descarga:</label>
                                                <asp:DropDownList ID="ddlArmazemDescarga_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsArmazemDescarga" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtCodArmazemDescarga_Maritimo" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Agência Marítima:</label>
                                                <asp:DropDownList ID="ddlAgenciaMaritima_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgenciaMaritima" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtCodAgenciaMaritima_Maritimo" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Tarifa master mínima:</label>
                                                <asp:TextBox ID="txtTarifaMasterMin_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Previsão de Embarque:</label>
                                                <asp:TextBox ID="txtPrevisaoEmbarque_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <br />
                                                <asp:ImageButton ID="imgPrevisaoEmbarque_BasicoMaritimo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data de Embarque:</label>
                                                <asp:TextBox ID="txtEmbarque_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <br />
                                                <asp:ImageButton ID="imgEmbarque_BasicoMaritimo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Previsão de Chegada:</label>
                                                <asp:TextBox ID="txtPrevisaoChegada_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <br />
                                                <asp:ImageButton ID="imgPrevisaoChegada_BasicoMaritimo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data de Chegada:</label>
                                                <asp:TextBox ID="txtChegada_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <br />
                                                <asp:ImageButton ID="imgChegada_BasicoMaritimo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Navio:</label>
                                                <asp:DropDownList ID="ddlNavio_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número da Viagem:</label>
                                                <asp:TextBox ID="txtNumeroViagem_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número CE:</label>
                                                <asp:TextBox ID="txtCE_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Status Frete Agente:</label>
                                                <asp:DropDownList ID="ddlStatusFreteAgente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_FRETE_AGENTE" DataSourceID="dsStatusFreteAgente" DataValueField="ID_STATUS_FRETE_AGENTE" enabled="false">
                                                </asp:DropDownList>
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
                                        <div class="linha-colorida">1° Transbordo</div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Navio:</label>
                                                <asp:DropDownList ID="ddlNavio1_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data:</label>
                                                <asp:TextBox ID="txtData1_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto:</label>
                                                <asp:DropDownList ID="ddlPorto1_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Viagem:</label>
                                                <asp:TextBox ID="txtViagem1_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="linha-colorida">2° Transbordo</div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Navio:</label>
                                                <asp:DropDownList ID="ddlNavio2_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data:</label>
                                                <asp:TextBox ID="txtData2_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto:</label>
                                                <asp:DropDownList ID="ddlPorto2_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Viagem:</label>
                                                <asp:TextBox ID="txtViagem2_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="linha-colorida">3° Transbordo</div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Navio:</label>
                                                <asp:DropDownList ID="ddlNavio3_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data:</label>
                                                <asp:TextBox ID="txtData3_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto:</label>
                                                <asp:DropDownList ID="ddlPorto3_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoMaritimo" DataValueField="ID_PORTO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Viagem:</label>
                                                <asp:TextBox ID="txtViagem3_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
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

                                    <ajaxToolkit:ModalPopupExtender ID="mpeNavio" runat="server" PopupControlID="PanelNavio" TargetControlID="ddlNavio_BasicoMaritimo" CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNavio" runat="server" CssClass="modalPopup" Style="display: none;">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>


                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Selecione um navio</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />

                                                            <asp:Label ID="Label1" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNavioFiltro" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNavios" runat="server" AutoPostBack="true" DataSourceID="dsNavios" DataTextField="NM_NAVIO" DataValueField="ID_NAVIO" Style="text-align: justify; font-size: 12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNavio" Text="Salvar navio" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtNavioFiltro" />
                                                <asp:PostBackTrigger ControlID="btnFechar" />
                                                <asp:PostBackTrigger ControlID="btnSalvarNavio" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>


                                    <ajaxToolkit:ModalPopupExtender ID="mpeNavio1" runat="server" PopupControlID="PanelNavio1" TargetControlID="ddlNavio1_BasicoMaritimo" CancelControlID="btnFechar1"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNavio1" runat="server" CssClass="modalPopup" Style="display: none;">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>


                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Selecione um navio para 1º Transbordo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />
                                                            <div class="alert alert-danger" id="divErroNavio1" runat="server" visible="false">
                                                                <asp:Label ID="lblErroNavio1" runat="server" Text="Navio não encontrado!"></asp:Label>
                                                            </div>
                                                            <asp:Label ID="Label3" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNavioFiltro1" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNavios1" runat="server" AutoPostBack="true" DataSourceID="dsNavios1" DataTextField="NM_NAVIO" DataValueField="ID_NAVIO" Style="text-align: justify; font-size: 12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar1" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNavio1" Text="Salvar navio" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtNavioFiltro1" />
                                                <asp:PostBackTrigger ControlID="btnFechar1" />
                                                <asp:PostBackTrigger ControlID="btnSalvarNavio1" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:ModalPopupExtender ID="mpeNavio2" runat="server" PopupControlID="PanelNavio2" TargetControlID="ddlNavio2_BasicoMaritimo" CancelControlID="btnFechar2"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNavio2" runat="server" CssClass="modalPopup" Style="display: none;">
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>


                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Selecione um navio para 2º Transbordo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />
                                                            <div class="alert alert-danger" id="divErroNavio2" runat="server" visible="false">
                                                                <asp:Label ID="lblErroNavio2" runat="server" Text="Navio não encontrado!"></asp:Label>
                                                            </div>
                                                            <asp:Label ID="Label2" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNavioFiltro2" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNavios2" runat="server" AutoPostBack="true" DataSourceID="dsNavios2" DataTextField="NM_NAVIO" DataValueField="ID_NAVIO" Style="text-align: justify; font-size: 12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar2" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNavio2" Text="Salvar navio" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtNavioFiltro2" />
                                                <asp:PostBackTrigger ControlID="btnFechar2" />
                                                <asp:PostBackTrigger ControlID="btnSalvarNavio2" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:ModalPopupExtender ID="mpeNavio3" runat="server" PopupControlID="PanelNavio3" TargetControlID="ddlNavio3_BasicoMaritimo" CancelControlID="btnFechar3"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNavio3" runat="server" CssClass="modalPopup" Style="display: none;">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>


                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Selecione um navio para 3º Transbordo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />
                                                            <div class="alert alert-danger" id="divErroNavio3" runat="server" visible="false">
                                                                <asp:Label ID="lblErroNavio3" runat="server" Text="Navio não encontrado!"></asp:Label>
                                                            </div>
                                                            <asp:Label ID="Label4" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNavioFiltro3" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNavios3" runat="server" AutoPostBack="true" DataSourceID="dsNavios3" DataTextField="NM_NAVIO" DataValueField="ID_NAVIO" Style="text-align: justify; font-size: 12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar3" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNavio3" Text="Salvar navio" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtNavioFiltro3" />
                                                <asp:PostBackTrigger ControlID="btnFechar3" />
                                                <asp:PostBackTrigger ControlID="btnSalvarNavio3" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <asp:Button runat="server" ID="Button3" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender ID="mpeHistoricoDatasMaritimo" runat="server" PopupControlID="pnlHistoricoDatasMaritimo" TargetControlID="Button3" CancelControlID="btnFecharHistoricoDatasMaritimo"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlHistoricoDatasMaritimo" runat="server" CssClass="modalPopup" Style="display: none;">
                                        <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Histórico de <asp:Label runat="server" ID="lblTipoHistoricoMaritimo"></asp:Label></h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                             <asp:TextBox ID="txtTipoDataMaritimo" runat="server" Enabled="false" CssClass="form-control" Style="display:none"></asp:TextBox>

                                                               <div class="row"> <div class="col-sm-12"> <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">

                             <asp:GridView ID="dgvHistoricoDatasMaritimo" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="ID_LOG_DATA_BL" DataSourceID="dsHistoricoDatas" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_LOG_DATA_BL" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                 <asp:BoundField DataField="DT_LOG_HIST" HeaderText="Data Anterior" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:dd/MM/yyyy}"/>
                                                                                <asp:BoundField DataField="NOME" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="UPDATED_AT" HeaderText="Data/Hora gravação" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="HistoricoDoc" />
                                                                        </asp:GridView>
                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnCSVHistoricoDatasMaritimo" text="Exportar CSV" />                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharHistoricoDatasMaritimo" text="Close" />                           
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                    </asp:Panel>

                                </ContentTemplate>
                            <Triggers>
								<asp:PostBackTrigger ControlID="btnCSVHistoricoDatasMaritimo" />							
                                <asp:AsyncPostBackTrigger ControlID="btnDesvincular" />
                                <asp:AsyncPostBackTrigger ControlID="btnVincular" />
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoPagamento_BasicoMaritimo" />
                                <asp:AsyncPostBackTrigger ControlID="btnGravar_BasicoMaritimo" /> 
                                <asp:AsyncPostBackTrigger ControlID="imgChegada_BasicoMaritimo" />
                                <asp:AsyncPostBackTrigger ControlID="imgEmbarque_BasicoMaritimo" />
                                <asp:AsyncPostBackTrigger ControlID="imgPrevisaoChegada_BasicoMaritimo" />
                                <asp:AsyncPostBackTrigger ControlID="imgPrevisaoEmbarque_BasicoMaritimo" />
                            </Triggers>
                        </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="ContainerMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Novo Container" ID="btnNovoCNTRMaritimo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_CNTRMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_CNTRMaritimo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_CNTRMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_CNTRMaritimo1" runat="server"></asp:Label>
                                                    </div>

                                                    <div class="table-responsive tableFixHead">
                                                        <asp:GridView ID="dgvContainer" DataKeyNames="ID_CNTR_BL" DataSourceID="dsContainer" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_CNTR_BL" HeaderText="#" SortExpression="ID_CNTR_BL" />
                                                                <asp:BoundField DataField="TIPO_CNTR" HeaderText="Tipo" SortExpression="TIPO_CNTR" />
                                                                <asp:BoundField DataField="NR_CNTR" HeaderText="Número" SortExpression="NR_CNTR" />
                                                                <asp:BoundField DataField="NR_LACRE" HeaderText="Lacre" SortExpression="NR_LACRE" />
                                                                <asp:BoundField DataField="VL_PESO_TARA" HeaderText="Tara" SortExpression="VL_PESO_TARA" />
                                                                <asp:BoundField DataField="TARIFA_SPOT" HeaderText="Acordo Comercial" SortExpression="TARIFA_SPOT" />
                                                                <asp:BoundField DataField="QT_DIAS_FREETIME" HeaderText="FreeTime" SortExpression="QT_DIAS_FREETIME" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_CNTR_BL") %>'
                                                                            Text="Visualizar" ToolTip="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_CNTR_BL") %>'
                                                                            Text="Visualizar" ToolTip="Duplicar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Ao deletar este registro todos os vínculos deste container em cargas do house serão deletados também. Deseja continuar?');" CommandArgument='<%# Eval("ID_CNTR_BL") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>
                                                    </div>


                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvContainer" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvContainer" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_CNTRMaritimo" />
                                                </Triggers>
                                            </asp:UpdatePanel>




                                            <ajaxToolkit:ModalPopupExtender ID="mpeCNTRMaritimo" runat="server" PopupControlID="Panel2" TargetControlID="btnNovoCNTRMaritimo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                    <ContentTemplate>
                                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title" id="modalFCLimpoTitleNovo">Container - Maritimo</h5>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="alert alert-success" id="divSuccess_CNTRMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblSuccess_CNTRMaritimo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger" id="divErro_CNTRMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblErro_CNTRMaritimo2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-3" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_CNTRMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo Container:</label>
                                                                                <asp:DropDownList ID="ddlTipoContainer_CNTRMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsTipoContainer" DataValueField="ID_TIPO_CONTAINER" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Número Container:</label>
                                                                                <asp:TextBox ID="txtNumeroContainer_CNTRMaritimo" onchange="Calculo_Digito_Conteiner()" runat="server" CssClass="txtNumeroContainer_CNTRMaritimo form-control" MaxLength="12"></asp:TextBox>
                                                                                <asp:TextBox ID="txtControle" Style="display: none" runat="server" CssClass="txtControle form-control"></asp:TextBox>

                                                                                <%-- <input type="text" id="nrContainer"  style="display:none" name="nrcont" class="form-control" onchange="Calculo_Digito_Conteiner()">--%>
                                                                                <span runat="server" id="spancntr" class="erroNrContainer">Número do Container inválido</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Número do Lacre:</label>
                                                                                <asp:TextBox ID="txtLacre_CNTRMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Tara:</label>
                                                                                <asp:TextBox ID="txtTara_CNTRMaritimo" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Qtd. Dias Freetime:</label>
                                                                                <asp:TextBox ID="txtFreeTime_CNTRMaritimo" runat="server" MaxLength="2" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar" ID="btnSalvar_CNTRMaritimo" CssClass="btn btn-success" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_CNTRMaritimo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_CNTRMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_CNTRMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoContainer_CNTRMaritimo" />

                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvContainer" />
                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_CNTRMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_CNTRMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="TaxasMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">

                                        <ContentTemplate>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Nova Taxa" ID="btnNovaTaxasMaritimo" CssClass="btn btn-primary" />
                                                        <asp:Button runat="server" Text="Importar Taxa" ID="btnImportarTaxasMaritimo" CssClass="btn btn-success" />
                                                        <asp:Button runat="server" Text="Selecionar Tudo" ID="btnSelecionarTudoMaritimo" CssClass="btn btn-warning" />
                                                        <asp:Button runat="server" Text="Deletar Taxas" ID="btnDeletarTaxasMaritimo" CssClass="btn btn-danger" Visible="true" OnClientClick="javascript:return confirm('Deseja realmente excluir?');" />
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_TaxasMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_TaxasMaritimo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_TaxasMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_TaxasMaritimo1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive tableFixHead">
                                                        <asp:GridView ID="dgvTaxasMaritimo" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasMaritimo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckSelecionar" runat="server"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ID_BL_TAXA" SortExpression="ID_BL_TAXA" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID_BL_TAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                                <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                                <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                                <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                                <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                                <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                                                <asp:BoundField DataField="DECLARADO" HeaderText="DECLARADO" SortExpression="DECLARADO" />
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
                                                    </div>

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

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxasMaritimo" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxasMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnImportarTaxasMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSelecionarTudoMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnDeletarTaxasMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnGravar_BasicoMaritimo" />    
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <ajaxToolkit:ModalPopupExtender ID="mpeTaxasMaritimo" runat="server" PopupControlID="Panel1" TargetControlID="btnNovaTaxasMaritimo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                    <ContentTemplate>
                                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title">Taxas - Maritimo</h5>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="alert alert-success" id="divSuccess_TaxasMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblSuccess_TaxasMaritimo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger" id="divErro_TaxasMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblErro_TaxasMaritimo2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-3" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_TaxasMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Item de Despesa:</label>
                                                                                <asp:DropDownList ID="ddlDespesa_TaxasMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" AutoPostBack="true" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>
                                                                                <asp:CheckBox ID="ckbProfit_TaxasMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;PROFIT"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>

                                                                                <asp:CheckBox ID="ckbPremiacao_TaxasMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Premiação"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-1" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Cód Empresa:</label>
                                                                                <asp:TextBox ID="txtCodEmpresa_TaxasMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Busca Fornecedor:</label>
                                                                                <asp:TextBox ID="txtNomeEmpresa_TaxasMaritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Fornecedor:</label>
                                                                                <asp:DropDownList ID="ddlEmpresa_TaxasMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsFornecedorMaritimo" DataValueField="ID_PARCEIRO">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" runat="server" id="divCompraMaritimo">
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Moeda de Compra:</label>
                                                                                <asp:DropDownList ID="ddlMoedaCompra_TaxasMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Mínimo de Compra:</label>
                                                                                <asp:TextBox ID="txtMinimoCompra_TaxasMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Taxa de Compra:</label>
                                                                                <asp:TextBox ID="txtTaxaCompra_TaxasMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Cálculo de Compra:</label>
                                                                                <asp:TextBox ID="txtCalculoCompra_TaxasMaritimo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Base de Cálculo:</label>
                                                                                <asp:DropDownList ID="ddlBaseCalculo_TaxasMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo de Pagamento:</label>
                                                                                <asp:DropDownList ID="ddlTipoPagamento_TaxasMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Origem Serviço:</label>
                                                                                <asp:DropDownList ID="ddlOrigemPagamento_TaxasMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Status de pagamento:</label>
                                                                                <asp:DropDownList ID="ddlStatusPagamento_TaxasMaritimo" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_PAGAMENTO" DataSourceID="dsStatusPagamento" DataValueField="ID_STATUS_PAGAMENTO">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar" ID="btnSalvar_TaxasMaritimo" CssClass="btn btn-success" OnClientClick="MouseWaitMaritimo(); return true;" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_TaxasMaritimo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxasMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxasMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlDespesa_TaxasMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNomeEmpresa_TaxasMaritimo" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxasMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxasMaritimo" />
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
                                                            <asp:BoundField DataField="ID_ARQUIVO" HeaderText="#" SortExpression="ID_ARQUIVO" Visible="false" />
                                                            <asp:TemplateField HeaderText="Nome do Arquivo" HeaderStyle-ForeColor="#337ab7">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNM_ARQUIVO" runat="server" Text='<%# Eval("NM_ARQUIVO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NM_TIPO_ARQUIVO" HeaderText="Tipo do Arquivo" SortExpression="NM_TIPO_ARQUIVO" />
                                                            <asp:BoundField DataField="NOME" HeaderText="Usuário" SortExpression="NOME" />
                                                            <asp:BoundField DataField="DT_UPLOAD" HeaderText="Data/Hora" SortExpression="DT_UPLOAD" />
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
                                            <asp:PostBackTrigger ControlID="dgvArquivosMaritimo" />
                                            <asp:PostBackTrigger ControlID="btnUploadMaritimo" />
                                            <asp:PostBackTrigger ControlID="btnLimparUploadMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="VinculosMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <asp:TextBox ID="txtCotacao_BasicoMaritimo" runat="server" CssClass="form-control" Style="display: none" />
                                            <div class="alert alert-success" id="divSuccess_Vinculo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_Vinculo" runat="server" Text="Registro atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_Vinculo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_Vinculo" runat="server"></asp:Label>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        HOUSES EM INSTRUÇÃO DE EMBARQUE
                                                        <asp:GridView ID="dgvNaoVinculados"
                                                            Style="max-height: 600px; overflow: auto;"
                                                            CssClass="table table-hover table-condensed table-bordered"
                                                            runat="server"
                                                            DataKeyNames="ID_BL"
                                                            AutoGenerateColumns="false"
                                                            BorderStyle="None"
                                                            BorderWidth="0px"
                                                            GridLines="None"
                                                            DataSourceID="dsNaoVinculados"
                                                            EmptyDataText="Nenhum registro encontrado.">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Processo">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="PROCESSO" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" />
                                                                <asp:BoundField DataField="NM_RAZAO" HeaderText="Cliente" />
                                                                <asp:BoundField DataField="PORTOS" HeaderText="Origem/Destino" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <a href="CadastrarEmbarqueHouse.aspx?tipo=e&id=<%# Eval("ID_BL") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" target="_blank" title="Editar"><span class="glyphicon glyphicon-edit"></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group text-center">
                                                        <asp:Button runat="server" Text="Vincular" ID="btnVincular" CssClass="btn btn-success" />
                                                        <asp:Button runat="server" Text="Desvincular" ID="btnDesvincular" CssClass="btn btn-danger" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        HOUSES VINCULADOS AO MASTER
                                                        <asp:GridView ID="dgvVinculados"
                                                            Style="max-height: 600px; overflow: auto;"
                                                            CssClass="table table-hover table-condensed table-bordered"
                                                            runat="server"
                                                            DataKeyNames="ID_BL"
                                                            AutoGenerateColumns="false"
                                                            BorderStyle="None"
                                                            BorderWidth="0px"
                                                            GridLines="None"
                                                            DataSourceID="dsVinculados"
                                                            EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>' />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Processo">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="PROCESSO" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" />
                                                                <asp:BoundField DataField="NM_RAZAO" HeaderText="Cliente" />
                                                                <asp:BoundField DataField="PORTOS" HeaderText="Origem/Destino" />
                                                                <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso" />
                                                                <asp:BoundField DataField="VL_M3" HeaderText="Cubagem" />
                                                                <asp:BoundField DataField="QT_MERCADORIA" HeaderText="Qtd. Volumes" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <a href="CadastrarEmbarqueHouse.aspx?tipo=h&id=<%# Eval("ID_BL") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" target="_blank" title="Editar"><span class="glyphicon glyphicon-edit"></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <br />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnVincular" />
                                            <asp:AsyncPostBackTrigger ControlID="btnDesvincular" />
    
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
                                    <a href="#MasterVinculosAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Vincular Houses
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
                                            <div class="alert alert-info" id="divinfo_BasicoAereo" runat="server" visible="false">
                                                <asp:Label ID="lblmsginfo_BasicoAereo" runat="server" Text="Favor revisar taxas locais do armador importadas!"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Código:</label>
                                                        <asp:TextBox ID="txtID_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">MAWB:</label>
                                                        <asp:TextBox ID="txtNumeroBL_BasicoAereo" runat="server" CssClass="form-control BL"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do Processo:</label>
                                                        <asp:TextBox ID="txtProcesso_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Estufagem:</label>
                                                        <asp:DropDownList ID="ddlEstufagem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label"></label>
                                                        <asp:CheckBox ID="ckTrakingAutomaticoAereo" runat="server" CssClass="form-control" Checked="false" Visible="false" Text="&nbsp;&nbsp;Traking Automatico"></asp:CheckBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-offset-5 col-sm-1">
                                                    <div class="form-group">
                                                        <center><label class="control-label">Doc. Conferido?</label><br />
                                                    <asp:CheckBox ID="ckDocConferidosAereo" runat="server" ></asp:CheckBox></center>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Numero do Voo:</label>
                                                        <asp:TextBox ID="txtNumeroVoo_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Serviço:</label>
                                                        <asp:DropDownList ID="ddlServico_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServicoAereo" DataValueField="ID_SERVICO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Agente Internacional:</label>
                                                        <asp:DropDownList ID="ddlAgente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Status Frete Agente:</label>
                                                        <asp:DropDownList ID="ddlStatusFreteAgente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_FRETE_AGENTE" DataSourceID="dsStatusFreteAgente" DataValueField="ID_STATUS_FRETE_AGENTE" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Transportador:</label>
                                                        <asp:DropDownList ID="ddltransportador_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto de Origem:</label>
                                                        <asp:DropDownList ID="ddlOrigem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto de Destino:</label>
                                                        <asp:DropDownList ID="ddlDestino_BasicoAereo" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Week:</label>
                                                        <asp:DropDownList ID="ddlWeekAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_WEEK" DataSourceID="dsWeekAereo" DataValueField="ID_WEEK"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Valor M3:</label>
                                                        <asp:TextBox ID="txtM3_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tarifa Master:</label>
                                                        <asp:TextBox ID="txtTarifaMaster_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Moeda de Frete:</label>
                                                        <asp:DropDownList ID="ddlMoedaFrete_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Pagamento:</label>
                                                        <asp:DropDownList ID="ddlTipoPagamento_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO"  AutoPostBack="true" >
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data de emissão do conhecimento:</label>
                                                        <asp:TextBox ID="txtDataConhecimento_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Previsão de Embarque:</label>
                                                        <asp:TextBox ID="txtPrevisaoEmbarque_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:ImageButton ID="imgPrevisaoEmbarque_BasicoAereo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Data de Embarque:</label>
                                                        <asp:TextBox ID="txtEmbarque_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:ImageButton ID="imgEmbarque_BasicoAereo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Previsão de Chegada:</label>
                                                        <asp:TextBox ID="txtPrevisaoChegada_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:ImageButton ID="imgPrevisaoChegada_BasicoAereo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Data de Chegada:</label>
                                                        <asp:TextBox ID="txtChegada_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:ImageButton ID="imgChegada_BasicoAereo" runat="server" src="Content/imagens/hist.png" CssClass="imgHistDatas" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="linha-colorida">1° Escala</div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto:</label>
                                                        <asp:DropDownList ID="ddlAeroporto1_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Prevista:</label>
                                                        <asp:TextBox ID="txtDataPrevista1_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número voo:</label>
                                                        <asp:TextBox ID="txtVoo1_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="linha-colorida">2° Transbordo</div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto:</label>
                                                        <asp:DropDownList ID="ddlAeroporto2_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Prevista:</label>
                                                        <asp:TextBox ID="txtDataPrevista2_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número voo:</label>
                                                        <asp:TextBox ID="txtVoo2_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="linha-colorida">3° Transbordo</div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Aeroporto:</label>
                                                        <asp:DropDownList ID="ddlAeroporto3_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoAereo" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Prevista:</label>
                                                        <asp:TextBox ID="txtDataPrevista3_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número voo:</label>
                                                        <asp:TextBox ID="txtVoo3_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
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

                                            <asp:Button runat="server" ID="Button4" Style="display: none" />

                                            <ajaxToolkit:ModalPopupExtender ID="mpeHistoricoDatasAereo" runat="server" PopupControlID="pnlHistoricoDatasAereo" TargetControlID="Button4" CancelControlID="btnFecharHistoricoDatasAereo"></ajaxToolkit:ModalPopupExtender>
                                            <asp:Panel ID="pnlHistoricoDatasAereo" runat="server" CssClass="modalPopup" Style="display: none;">
                                                <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Histórico de <asp:Label runat="server" ID="lblTipoHistoricoAereo"></asp:Label></h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:TextBox ID="txtTipoDataAereo" runat="server" Enabled="false" CssClass="form-control" Style="display:none"></asp:TextBox>
 
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-12"> <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">

                             <asp:GridView ID="dgvHistoricoDatasAereo" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="ID_LOG_DATA_BL" DataSourceID="dsHistoricoDatas" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_LOG_DATA_BL" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                 <asp:BoundField DataField="DT_LOG_HIST" HeaderText="Data Anterior" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:dd/MM/yyyy}"/>
                                                                                <asp:BoundField DataField="NOME" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="UPDATED_AT" HeaderText="Data/Hora gravação" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="HistoricoDoc" />
                                                                        </asp:GridView>
                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnCSVHistoricoDatasAereo" text="Exportar CSV" />                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharHistoricoDatasAereo" text="Close" />                           
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                            </asp:Panel>



                                        </ContentTemplate>
                                        <Triggers>
										    <asp:PostBackTrigger ControlID="btnCSVHistoricoDatasAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlWeekAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnVincularAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlTipoPagamento_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="imgChegada_BasicoAereo" />
											<asp:AsyncPostBackTrigger ControlID="imgEmbarque_BasicoAereo" />
											<asp:AsyncPostBackTrigger ControlID="imgPrevisaoChegada_BasicoAereo" />
											<asp:AsyncPostBackTrigger ControlID="imgPrevisaoEmbarque_BasicoAereo" />   
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="TaxasAereo">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Nova Taxa" ID="btnNovaTaxaAereo" CssClass="btn btn-primary" />
                                                        <asp:Button runat="server" Text="Importar Taxa" ID="btnImportarTaxasAereo" CssClass="btn btn-success" />
                                                        <asp:Button runat="server" Text="Selecionar Tudo" ID="btnSelecionarTudoAereo" CssClass="btn btn-warning" />
                                                        <asp:Button runat="server" Text="Deletar Taxas" ID="btnDeletarTaxasAereo" CssClass="btn btn-danger" Visible="true" OnClientClick="javascript:return confirm('Deseja realmente excluir?');" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_TaxaAereo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_TaxaAereo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_TaxaAereo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_TaxaAereo1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive tableFixHead">
                                                        <asp:GridView ID="dgvTaxasAereo" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckSelecionar" runat="server"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ID_BL_TAXA" SortExpression="ID_BL_TAXA" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID_BL_TAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                                <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                                <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                                <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                                <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                                <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                                                <asp:BoundField DataField="DECLARADO" HeaderText="DECLARADO" SortExpression="DECLARADO" />
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
                                                    </div>

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

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxasAereo" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasAereo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnImportarTaxasAereo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSelecionarTudoAereo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnDeletarTaxasAereo" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            <ajaxToolkit:ModalPopupExtender ID="mpeTaxaAereo" runat="server" PopupControlID="Panel4" TargetControlID="btnNovaTaxaAereo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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
                                                                    <div class="row">
                                                                        <div class="col-sm-3" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_TaxaAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Item de Despesa:</label>
                                                                                <asp:DropDownList ID="ddlDespesa_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" AutoPostBack="true" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>
                                                                                <asp:CheckBox ID="ckbProfit_TaxasAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;PROFIT"></asp:CheckBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>

                                                                                <asp:CheckBox ID="ckbPremiacao_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Premiação"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-1" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Cód Empresa:</label>
                                                                                <asp:TextBox ID="txtCodEmpresa_TaxasAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Busca Fornecedor:</label>
                                                                                <asp:TextBox ID="txtNomeEmpresa_TaxasAereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Fornecedor:</label>
                                                                                <asp:DropDownList ID="ddlEmpresa_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsFornecedorAereo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" runat="server" id="divCompraAereo">
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Moeda de Compra:</label>
                                                                                <asp:DropDownList ID="ddlMoedaCompra_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Mínimo de Compra:</label>
                                                                                <asp:TextBox ID="txtMinimoCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor de Compra:</label>
                                                                                <asp:TextBox ID="txtTaxaCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Cálculo de Compra:</label>
                                                                                <asp:TextBox ID="txtCalculoCompra_TaxaAereo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>


                                                                    </div>


                                                                    <div class="row">



                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Base de Cálculo:</label>
                                                                                <asp:DropDownList ID="ddlBaseCalculo_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo de Pagamento:</label>
                                                                                <asp:DropDownList ID="ddlTipoPagamento_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Origem Serviço:</label>
                                                                                <asp:DropDownList ID="ddlOrigemPagamento_TaxasAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO">
                                                                                </asp:DropDownList>


                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Status de pagamento:</label>
                                                                                <asp:DropDownList ID="ddlStatusPagamento_TaxaAereo" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_PAGAMENTO" DataSourceID="dsStatusPagamento" DataValueField="ID_STATUS_PAGAMENTO">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar" ID="btnSalvar_TaxaAereo" CssClass="btn btn-success" OnClientClick="MouseWaitAereo(); return true;" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_TaxaAereo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaAereo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlDespesa_TaxaAereo" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNomeEmpresa_TaxasAereo" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                                                <div class="colz-sm-12">
                                                    <asp:TextBox ID="txtUPAereo" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:TextBox ID="txtArquivoSelecionadoAereo" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:GridView ID="dgvArquivosAereo" runat="server" AutoGenerateColumns="false" EmptyDataText="Nenhum arquivo enviado" DataKeyNames="ID_ARQUIVO" DataSourceID="dsUploadsAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" Style="max-height: 400px; overflow: auto;" AllowSorting="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID_ARQUIVO" HeaderText="#" SortExpression="ID_ARQUIVO" Visible="false" />
                                                            <asp:TemplateField HeaderText="Nome do Arquivo" HeaderStyle-ForeColor="#337ab7">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNM_ARQUIVO" runat="server" Text='<%# Eval("NM_ARQUIVO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NM_TIPO_ARQUIVO" HeaderText="Tipo do Arquivo" SortExpression="NM_TIPO_ARQUIVO" />
                                                            <asp:BoundField DataField="NOME" HeaderText="Usuário" SortExpression="NOME" />
                                                            <asp:BoundField DataField="DT_UPLOAD" HeaderText="Data/Hora" SortExpression="DT_UPLOAD" />
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
                                            <asp:PostBackTrigger ControlID="btnUploadAereo" />
                                            <asp:PostBackTrigger ControlID="dgvArquivosAereo" />
                                            <asp:PostBackTrigger ControlID="btnLimparUploadAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>


                                <div class="tab-pane fade" id="MasterVinculosAereo">
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <asp:TextBox ID="txtCotacao_BasicoAereo" runat="server" CssClass="form-control" Style="display: none" />
                                            <div class="alert alert-success" id="divSuccess_VinculoAereo" runat="server" visible="false">
                                                <asp:Label ID="Label5" runat="server" Text="Registro atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_VinculoAereo" runat="server" visible="false">
                                                <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        HOUSES EM INSTRUÇÃO DE EMBARQUE
                                                        <asp:GridView ID="dgvNaoVinculadosAereos"
                                                            Style="max-height: 600px; overflow: auto;"
                                                            CssClass="table table-hover table-condensed table-bordered"
                                                            runat="server"
                                                            DataKeyNames="ID_BL"
                                                            AutoGenerateColumns="false"
                                                            BorderStyle="None"
                                                            BorderWidth="0px"
                                                            GridLines="None"
                                                            DataSourceID="dsNaoVinculadosAereos"
                                                            EmptyDataText="Nenhum registro encontrado.">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Processo">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="PROCESSO" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" />
                                                                <asp:BoundField DataField="NM_RAZAO" HeaderText="Cliente" />
                                                                <asp:BoundField DataField="PORTOS" HeaderText="Origem/Destino" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <a href="CadastrarEmbarqueHouse.aspx?tipo=e&id=<%# Eval("ID_BL") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" target="_blank" title="Editar"><span class="glyphicon glyphicon-edit"></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group text-center">
                                                        <asp:Button runat="server" Text="Vincular" ID="btnVincularAereo" CssClass="btn btn-success" />
                                                        <asp:Button runat="server" Text="Desvincular" ID="btnDesvincularAereo" CssClass="btn btn-danger" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        HOUSES VINCULADOS AO MASTER
                                                        <asp:GridView ID="dgvVinculadosAereos"
                                                            Style="max-height: 600px; overflow: auto;"
                                                            CssClass="table table-hover table-condensed table-bordered"
                                                            runat="server"
                                                            DataKeyNames="ID_BL"
                                                            AutoGenerateColumns="false"
                                                            BorderStyle="None"
                                                            BorderWidth="0px"
                                                            GridLines="None"
                                                            DataSourceID="dsVinculadosAereos"
                                                            EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>' />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Processo">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="PROCESSO" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" />
                                                                <asp:BoundField DataField="NM_RAZAO" HeaderText="Cliente" />
                                                                <asp:BoundField DataField="PORTOS" HeaderText="Origem/Destino" />
                                                                <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso" />
                                                                <asp:BoundField DataField="VL_M3" HeaderText="Cubagem" />
                                                                <asp:BoundField DataField="QT_MERCADORIA" HeaderText="Qtd. Volumes" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <a href="CadastrarEmbarqueHouse.aspx?tipo=h&id=<%# Eval("ID_BL") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" target="_blank" title="Editar"><span class="glyphicon glyphicon-edit"></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <br />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnDesvincularAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnVincularAereo" />
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
                    <h4 class="modal-title">Sobre NVOCC: Cadastro Master</h4>
                </div>
                <div class="modal-body">
                    <strong>Objetivo:</strong> Cadastrar, alterar e visualizar um MBL, isso inclui suas informações basicas, taxas, container e vinculos com HBLs.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
  
    <asp:SqlDataSource ID="dsArmazemDescarga" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE (FL_ARMAZEM_DESCARGA = 1 or ID_PARCEIRO =  @ID_PARCEIRO)
union SELECT 0, ' Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO" Type="Int32" ControlID="txtCodArmazemDescarga_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsArmazemAtracacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_ARMAZEM_ATRACACAO = 1
union SELECT 0, ' Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO" Type="Int32" ControlID="txtCodArmazemAtracacao_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE (FL_TRANSPORTADOR = 1 or ID_PARCEIRO =  @ID_PARCEIRO_TRANSPORTADOR)
union SELECT 0, ' Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_TRANSPORTADOR" Type="Int32" ControlID="txtCodTransportador_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsStatusPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_STATUS_PAGAMENTO, NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO
union SELECT 0, 'Selecione' FROM TB_STATUS_PAGAMENTO ORDER BY ID_STATUS_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_PAGAMENTO, NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO
union SELECT 0, 'Selecione' FROM TB_TIPO_PAGAMENTO ORDER BY ID_TIPO_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')'  AS NM_RAZAO  FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT 0, ' Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPortoMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = 1 union SELECT  0, '       Selecione' ORDER BY NM_PORTO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPortoAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO, CONVERT(VARCHAR,CD_PORTO) + ' - ' + NM_PORTO AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = 4 union SELECT  0, '        Selecione' ORDER BY NM_PORTO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT 0, '    Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY NM_BASE_CALCULO_TAXA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT 0, 'Selecione' FROM [dbo].[TB_ORIGEM_PAGAMENTO] ORDER BY ID_ORIGEM_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM]
union SELECT 0, 'Selecione' FROM [dbo].[TB_MERCADORIA] ORDER BY ID_TIPO_ESTUFAGEM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM [dbo].[TB_ITEM_DESPESA]
union SELECT 0, ' Selecione' FROM [dbo].[TB_TIPO_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CNTR_BL,ID_TIPO_CNTR, CASE WHEN ISNULL(C.FL_TARIFA_SPOT,0) > 0 THEN 'FRETE SPOT' ELSE '' END TARIFA_SPOT ,
(SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = A.ID_TIPO_CNTR)TIPO_CNTR,NR_CNTR,NR_LACRE,VL_PESO_TARA,QT_DIAS_FREETIME 
FROM TB_CNTR_BL A 
LEFT JOIN TB_BL B ON A.ID_BL_MASTER = B.ID_BL 
LEFT JOIN TB_COTACAO C ON B.id_cotacao= C.id_cotacao
WHERE A.ID_BL_MASTER = @ID_BL_MASTER ORDER BY ID_CNTR_BL DESC">

        <SelectParameters>
            <asp:ControlParameter Name="ID_BL_MASTER" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_CONTAINER] ORDER BY ID_TIPO_CONTAINER"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [FN_TAXAS_BL](@ID_BL,'P') order by ID_BL_TAXA desc ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsTaxasAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [FN_TAXAS_BL](@ID_BL,'P') order by ID_BL_TAXA desc ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsVinculados" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL, NR_BL, NR_PROCESSO, NM_RAZAO, PORTOS, VL_PESO_BRUTO, VL_M3 , QT_MERCADORIA FROM [FN_HOUSES_VINCULADOS](@ID_BL) ORDER BY ID_BL_MASTER DESC ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNaoVinculados" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL, NR_BL, NR_PROCESSO, NM_RAZAO, PORTOS FROM [FN_HOUSES_NAO_VINCULADOS](@ORIGEM,@DESTINO) ORDER BY NR_PROCESSO">
        <SelectParameters>
            <asp:ControlParameter Name="ORIGEM" Type="Int32" ControlID="ddlOrigem_BasicoMaritimo" />
            <asp:ControlParameter Name="DESTINO" Type="Int32" ControlID="ddlDestino_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>





    <asp:SqlDataSource ID="dsVinculadosAereos" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL, NR_BL, NR_PROCESSO, NM_RAZAO, PORTOS, VL_PESO_BRUTO, VL_M3 , QT_MERCADORIA FROM [FN_HOUSES_VINCULADOS](@ID_BL) ORDER BY ID_BL_MASTER DESC">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNaoVinculadosAereos" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL, NR_BL, NR_PROCESSO, NM_RAZAO, PORTOS FROM [FN_HOUSES_NAO_VINCULADOS](@ORIGEM,@DESTINO) ORDER BY NR_PROCESSO">
        <SelectParameters>
            <asp:ControlParameter Name="ORIGEM" Type="Int32" ControlID="ddlOrigem_BasicoAereo" />
            <asp:ControlParameter Name="DESTINO" Type="Int32" ControlID="ddlDestino_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:SqlDataSource ID="dsWeekAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE ID_PORTO_ORIGEM_DESTINO = 0 AND ID_PORTO_ORIGEM_LOCAL = 0
union SELECT 0, 'Selecione' ORDER BY ID_WEEK"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsWeekMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE ID_PORTO_ORIGEM_DESTINO = 0 AND ID_PORTO_ORIGEM_LOCAL = 0
union SELECT 0, 'Selecione' ORDER BY ID_WEEK"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFornecedorMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO)
union SELECT  0, '',' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeEmpresa_TaxasMaritimo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO" Type="Int32" ControlID="txtCodEmpresa_TaxasMaritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsFornecedorAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE (NM_RAZAO  like '%' + @NM_RAZAO + '%' or ID_PARCEIRO =  @ID_PARCEIRO)
union SELECT  0, '',' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="NM_RAZAO" Type="String" ControlID="txtNomeEmpresa_TaxasAereo" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_PARCEIRO" Type="Int32" ControlID="txtCodEmpresa_TaxasAereo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsServicoMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO NOT IN (2,5)
union SELECT 0, 'Selecione' FROM TB_SERVICO ORDER BY ID_SERVICO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsServicoAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO IN (2,5)
union SELECT 0, 'Selecione' FROM TB_SERVICO ORDER BY ID_SERVICO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgenciaMaritima" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ((FL_TRANSPORTADOR = 1 OR FL_AGENCIA = 1) or (ID_PARCEIRO =  @ID_PARCEIRO))
union SELECT 0, ' Selecione' ORDER BY NM_RAZAO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO" Type="Int32" ControlID="txtCodAgenciaMaritima_Maritimo" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsNavios" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NAVIO, NM_NAVIO FROM [dbo].[TB_NAVIO] where (NM_NAVIO like '%' + @Nome + '%' Or @Nome = '0')">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNavioFiltro" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNavios1" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NAVIO, NM_NAVIO FROM [dbo].[TB_NAVIO] where (NM_NAVIO like '%' + @Nome + '%' Or @Nome = '0')">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNavioFiltro1" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNavios2" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NAVIO, NM_NAVIO FROM [dbo].[TB_NAVIO] where (NM_NAVIO like '%' + @Nome + '%' Or @Nome = '0')">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNavioFiltro2" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNavios3" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NAVIO, NM_NAVIO FROM [dbo].[TB_NAVIO] where (NM_NAVIO like '%' + @Nome + '%' Or @Nome = '0')">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNavioFiltro3" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsStatusFreteAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_STATUS_FRETE_AGENTE, NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE 
union SELECT 0, 'Selecione' FROM TB_STATUS_FRETE_AGENTE ORDER BY ID_STATUS_FRETE_AGENTE"></asp:SqlDataSource>

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
C.NM_MOTIVO_INATIVACAO + ': ' +A.DS_MOTIVO_INATIVACAO ELSE C.NM_MOTIVO_INATIVACAO END NM_MOTIVO_INATIVACAO,A.DS_MOTIVO_INATIVACAO FROM TB_INATIVACAO_TAXAS A INNER JOIN TB_USUARIO B ON A.ID_USUARIO_INATIVACAO = B.ID_USUARIO INNER JOIN TB_MOTIVO_INATIVACAO C ON C.ID_MOTIVO_INATIVACAO = A.ID_MOTIVO_INATIVACAO WHERE A.ID_BL_TAXA = @ID_BL_TAXA ORDER BY DT_INATIVACAO DESC">
        <SelectParameters>
            <asp:Parameter Name="ID_BL_TAXA" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsHistoricoDatas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_LOG_DATA_BL, DT_LOG_HIST, UPDATED_AT, NOME FROM TB_LOG_DATA_BL A LEFT JOIN TB_USUARIO B ON A.ID_USUARIO = B.ID_USUARIO WHERE A.ID_BL = @ID_BL AND CD_TIPO_DATA = @CD_TIPO_DATA ORDER BY UPDATED_AT DESC">
        <SelectParameters>
            <asp:Parameter Name="ID_BL" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="CD_TIPO_DATA" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

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

        function Calculo_Digito_Conteiner() {
            var Conteiner = document.getElementById('<%= txtNumeroContainer_CNTRMaritimo.ClientID %>').value;
            var Alpha = [];
            var Valores_Alpha = [];
            var parcelas = [];
            var somatorio = 0;
            var cleanConteiner = "";
            var ok = true;
            Alpha[0] = "A"; Alpha[1] = "B"; Alpha[2] = "C"; Alpha[3] = "D"; Alpha[4] = "E"; Alpha[5] = "F"; Alpha[6] = "G"
            Alpha[7] = "H"; Alpha[8] = "I"; Alpha[9] = "J"; Alpha[10] = "K"; Alpha[11] = "L"; Alpha[12] = "M"
            Alpha[13] = "N"; Alpha[14] = "O"; Alpha[15] = "P"; Alpha[16] = "Q"; Alpha[17] = "R"; Alpha[18] = "S"
            Alpha[19] = "T"; Alpha[20] = "U"; Alpha[21] = "V"; Alpha[22] = "W"; Alpha[23] = "X"; Alpha[24] = "Y"; Alpha[25] = "Z";
            Valores_Alpha[0] = 10; Valores_Alpha[1] = 12; Valores_Alpha[2] = 13; Valores_Alpha[3] = 14;
            Valores_Alpha[4] = 15; Valores_Alpha[5] = 16; Valores_Alpha[6] = 17; Valores_Alpha[7] = 18;
            Valores_Alpha[8] = 19; Valores_Alpha[9] = 20; Valores_Alpha[10] = 21; Valores_Alpha[11] = 23;
            Valores_Alpha[12] = 24; Valores_Alpha[13] = 25; Valores_Alpha[14] = 26; Valores_Alpha[15] = 27;
            Valores_Alpha[16] = 28; Valores_Alpha[17] = 29; Valores_Alpha[18] = 30; Valores_Alpha[19] = 31;
            Valores_Alpha[20] = 32; Valores_Alpha[21] = 34; Valores_Alpha[22] = 35; Valores_Alpha[23] = 36;
            Valores_Alpha[24] = 37; Valores_Alpha[25] = 38;
            for (var i = 0; i < Conteiner.length; i++) {
                if (Conteiner.substr(i, 1) != "") {
                    if ((Conteiner.substr(i, 1)).charCodeAt(0) >= 48 && (Conteiner.substr(i, 1)).charCodeAt(0) <= 57 || (Conteiner.substr(i, 1)).charCodeAt(0) >= 65 && (Conteiner.substr(i, 1)).charCodeAt(0) <= 90) {
                        cleanConteiner = cleanConteiner + Conteiner.substr(i, 1);
                    }
                }
            }
            if (cleanConteiner == "") {
                return;
            }
            for (var i = 0; i < 4; i++) {
                if ((cleanConteiner.substr(i, 1)).charCodeAt(0) < 65 || (cleanConteiner.substr(i, 1)).charCodeAt(0) > 90) {
                    ok = false;
                }
            }
            for (var i = 4; i < 10; i++) {
                if ((cleanConteiner.substr(i, 1)).charCodeAt(0) < 48 || (cleanConteiner.substr(i, 1)).charCodeAt(0) > 57) {
                    ok = false;
                }
            }
            if (cleanConteiner.length != 10 && cleanConteiner.length != 11) {
                ok = false;
            }

            for (var i = 0; i < 10; i++) {
                if (i < 4) {
                    for (var j = 0; j < 26; j++) {
                        if (Alpha[j] == cleanConteiner.substr(i, 1)) {
                            break;
                        }
                    }
                    somatorio = somatorio + Valores_Alpha[j] * Math.pow(2, i);
                }
                else {
                    somatorio = somatorio + parseInt(cleanConteiner.substr(i, 1)) * Math.pow(2, i);
                }

            }
            var calcula = (somatorio % 11).toString();
            calcula = (calcula.substr(calcula.length - 1, 1));
            if (calcula == cleanConteiner.substr(cleanConteiner.length - 1, 1)) {
                $(".txtNumeroContainer_CNTRMaritimo").css('background', '#fff');
                $(".erroNrContainer").hide();
                $('.txtControle').val("1");



            }
            else {
                $(".txtNumeroContainer_CNTRMaritimo").css('background', '#ffdfd4');
                $(".erroNrContainer").show();
                $('.txtControle').val("0");


            }
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


        function MouseWait() {
            document.body.style.cursor = "wait";
        };

        function MouseDefault() {
            document.body.style.cursor = "default";
        };

        $(document).ready(function () {
            Cursor();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Cursor);

        function Cursor() {
            $('#<%= ddlTipoPagamento_BasicoMaritimo.ClientID %>').change(function () {
                MouseWait();
            });
            $('#<%= ddlTipoPagamento_BasicoAereo.ClientID %>').change(function () {
                MouseWait();
            });

        }

        function InIEvent() {
            Cursor();
        }


        function MouseWaitMaritimo() {
            document.getElementById('<%= btnSalvar_TaxasMaritimo.ClientID %>').style.display = 'none';
            console.log("wait");
            document.body.style.cursor = "wait";
        };
        function MouseDefaultMaritimo() {
            document.getElementById('<%= btnSalvar_TaxasMaritimo.ClientID %>').style.display = 'block';
            console.log("default");
            document.body.style.cursor = "default";
        };
        function MouseWaitAereo() {
            document.getElementById('<%= btnSalvar_TaxaAereo.ClientID %>').style.display = 'none';
            console.log("wait");
            document.body.style.cursor = "wait";
        };
        function MouseDefaultAereo() {
            document.getElementById('<%= btnSalvar_TaxaAereo.ClientID %>').style.display = 'block';
            console.log("default");
            document.body.style.cursor = "default";
        };

    </script>
 
</asp:Content>
