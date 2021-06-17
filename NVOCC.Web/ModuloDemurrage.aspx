﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuloDemurrage.aspx.cs" Inherits="ABAINFRA.Web.ModuloDemurrage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Tabela Demurrage
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="alert alert-danger text-center" id="msgErrExistDemu">
                                Registro já existente!
                            </div>
                            <div class="alert alert-success text-center" id="msgSuccessDemu">
                                Registro cadastrado/atualizado com sucesso!
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrDemu">
                                Erro ao cadastrar/atualizar.
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrSelect">
                                Selecione um Processo
                            </div>
                        </div>
                    </div>
                    <div class="functionBar">
                        <div class="btnBoxFunc">
                            <button type="button" id="btnNovaDemurrage" class="btn btn-primary" data-toggle="modal" data-target="#modalDemurrage">Cadastro Tabela de Demurrage</button>
                            <button type="button" id="btnEditarInfoCntr" class="btn btn-primary" data-toggle="modal" data-target="#modalEditContInfo" onclick="infoContainer()">Editar Info. CNTR</button>
                            <button type="button" id="btnAtualizarDataDevo" class="btn btn-primary" data-toggle="modal" data-target="#modalDevolucao" onclick="DevolucaoContainer()">Atualizar Data Devol</button>
                        </div>
                        <div class="btnDemuFunc">
                            <div>
                                <input type="radio" id="venda" name="type" value="1" checked>
                                <label for="venda">Venda</label><br>
                                <input type="radio" id="compra" name="type" value="2">
                                <label for="compra">Compra</label><br>
                            </div>
                            <div class="demuFunc">
                                <button type="button" id="btnCalcularDemurrage" class="btn btn-primary" onclick="CalculoDemurrage()">Cálculo Demurrage</button>           
                                <button type="button" id="btnImprimirCalc" onclick="imprimirDadosCalculo()" class="btn btn-primary" data-toggle="modal" data-target="#modalPrint">Imprimir Cálculo</button>                
                                <button type="button" id="btnFatura" class="btn btn-primary" data-toggle="modal" data-target="#modalFaturas" onclick="listarFatura()">Faturas</button>
                            </div>
                        </div>
                        <div class="oFunc">
                            <button type="button" id="btnEstimativa" class="btn btn-primary" onclick="estimativaCV()" data-toggle="modal" data-target="#modalEstimativa">Estimativa Compra e Venda</button> 
                            <button type="button" id="btnExportGridAtual" class="btn btn-primary" onclick="exportTableToCSVAtual('members.csv')">Exportar Grid - CSV</button>
                        </div>
                    </div>
                    <div class="row topMarg flexdiv">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Consultar por:<span class="required">*</span></label>
                                <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label"><span class="required">&nbsp</span></label>
                                <input id="txtConsulta" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <button type="button" id="btnConsulta" onclick="consultaFiltrada()" class="btn btn-primary">Consultar</button>
                            </div>
                        </div>
                    </div>
                    <div class="row topMarg">
                        
                        <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                            <div>
                                <asp:CheckBox ID="chkFinalizado" runat="server" CssClass="form-control noborder" Text="&nbsp;Finalizados"></asp:CheckBox>
                            </div>
                            <div>
                                <asp:CheckBox ID="chkAtivo" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Ativos"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    
                    <div class="modal fade bd-example-modal-lg" id="modalEditContInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalEditContInfoTitle">Editar Informações Container</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcesso" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Container</label>
                                                <input id="nrContainer" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmCliente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status</label>
                                                <input id="dtStatus" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-9">
                                            <div class="form-group">
                                                <label class="control-label">Status</label>
                                                <asp:DropDownList ID="dsStatus" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Quantidade Dias de FreeTime<span id="faturado">(Faturado)</span><span id="nFaturado">(Sem Faturamento)</span></label>
                                                <input id="qtDiasFreeTime" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Observações</label>
                                                <textarea id="obsInfoCont" name="obsInfoCont" class="form-control"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnEditarInfoCont" onclick="atualizarContainer()" data-dismiss="modal" class="btn btn-primary btn-ok">Atualizar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalDevolucao" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDevolucaoTitle">Devolução Conteiner</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoDevolucao" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>                                      
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteDevolucao" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Devolução</label>
                                                <input id="dtDevolucao" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Status</label>
                                                <asp:DropDownList ID="ddlStatusDevolucao" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status</label>
                                                <input id="dtStatusDevolucao" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive tableModalFix">
                                        <table id="grdDevolucaoContainer" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">Nº Container</th>
                                                    <th class="text-center" scope="col">Data Devolução</th>
                                                    <th class="text-center" scope="col">Status</th>
                                                    <th class="text-center" scope="col">Data Status</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdDevolucaoContainerBody">
                                
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnDevolucao" onclick="atualizarDevolucao()" class="btn btn-primary btn-ok">Atualizar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalCaluclo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalCalculoTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="alert alert-danger text-center" id="msgErrSelectCalc">
                                            Selecione ao menos um Container
                                        </div>
                                     </div>
                                    <div class="row">

                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoCalculo" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteCalculo" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Transportador</label>
                                                <asp:DropDownList ID="ddlTransportador" runat="server" CssClass="form-control nobox" type="text" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    
                                    <div class="table-responsive tableFixHead">
                                        <table id="grdCalculoContainer" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">Nº Container</th>
                                                    <th class="text-center" scope="col">Tipo</th>
                                                    <th class="text-center" scope="col">Data Chegada</th>
                                                    <th class="text-center" scope="col">Free Time</th>
                                                    <th class="text-center" scope="col">Data Limite</th>
                                                    <th class="text-center" scope="col">Data Devolução</th>
                                                    <th class="text-center" scope="col">Dias Demurrage</th>
                                                    <th class="text-center" scope="col">Moeda Demu Compra</th>
                                                    <th class="text-center" scope="col">Valor Demu Compra</th>
                                                    <th class="text-center" scope="col">Moeda Demu Venda</th>
                                                    <th class="text-center" scope="col">Valor Demu Venda</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdCalculoDemurrageBody">

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnCalcular" class="btn btn-primary btn-ok" onclick="obterMarcados()">Calcular</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalCalucloSelecionados" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalCalculoTitleSelecionados">Calcular Demurrage</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="alert alert-danger text-center" id="msgErrTable">
                                            Tabela Demurrage não encontrada
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Conteiner</label>
                                                <input id="nrConteinerCalculo" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Tipo</label>
                                                <input id="nmTipoCont" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Dias Demu</label>
                                                <input id="qtDiasDemu" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Tabela de Calculo</label>
                                                <input id="nmTabelaCalculo" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Moeda</label>
                                                <input id="nmMoeda" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Status</label>
                                                <asp:DropDownList ID="ddlStatusCalculoSelecionado" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status</label>
                                                <input id="dtStatusCalculoSelecionado" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Valor Taxa a Ser Aplicada</label>
                                                <input id="vlTaxa" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnZerarCalculo" onclick="zerarCalculo()" class="btn btn-primary btn-ok">Zerar Cálculo</button>
                                    <button type="button" id="btnIgnorar" onclick="ignorar()" class="btn btn-primary btn-ok">Ignorar</button>
                                    <button type="button" id="btnCalcularSelecionado" onclick="calcularSelecionados()" class="btn btn-primary btn-ok">Calcular</button>
                                    <button type="button" id="btnProximo" onclick="proximo()" class="btn btn-primary btn-ok">Proximo</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalDemurrage" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDemurrageTitle">Cadastrar Demurrage</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row" id="listDemurrage">
                                        <div class="text-center col-sm-6 col-sm-offset-3">
                                            <label class="control-label text-center" style="font-size: 14px;">Código Demurrage</label><br>
                                            <select id="ddlDemurrage" onchange="BuscarDemurrage(this.value)" class="labelTaxa form-control">
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Transportador <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlParceiroTransportador" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Tipo Container <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlTipoContainer" runat="server" CssClass="form-control" DataTextField="NM_TIPO_CONTAINER" DataValueField="ID_TIPO_CONTAINER"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Data Validade Inicial<span class="required">*</span></label>
                                                <input id="dtValidade" type="date" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Free Time<span class="required">*</span></label>
                                                <input id="qtFreetime" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Moeda<span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" DataValueField="ID_MOEDA" DataTextField="NM_MOEDA">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group dflex">
                                                <label class="control-label">&nbsp;</label>
                                                <asp:CheckBox ID="checkEsc" runat="server" CssClass="form-control noborder" Text="&nbsp;&nbsp;Escalonada"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group dflex">
                                                <label class="control-label">&nbsp;</label>
                                                <asp:CheckBox ID="checkInicioFreetime" runat="server" CssClass="form-control noborder" Text="&nbsp;&nbsp;Iniciar Free Time na Data de Chegada
"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label">#</label>
                                                <input type="text" class="form-control noborder text-center" disabled="disabled" value="1">
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label">Dias<span class="required">*</span></label>
                                                <input id="dtDemurrage1" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label">Valor Venda <span class="required">*</span><span style="font-size: 10px; font-weight: bold;">(Seguir a ordem do indice à esquerda)</span></label>
                                                <input id="vlDemurrage1" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div id="boxescala5" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label">#</label>
                                                <input id="escala5" type="text" class="form-control noborder text-center" disabled="disabled" value="5">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage5" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label">Dias<span class="required"></span></label>
                                                <input id="dtDemurrage5" class="form-control" value="0" type="text">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage5" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label">Valor Venda<span class="required"></span></label>
                                                <input id="vlDemurrage5" class="form-control" value="0" type="text">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="boxescala2" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label"></label>
                                                <input id="escala2" type="text" class="form-control noborder text-center" disabled="disabled" value="2">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage2" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="dtDemurrage2" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage2" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="vlDemurrage2" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxescala6" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label"></label>
                                                <input id="escala6" type="text" class="form-control noborder text-center" disabled="disabled" value="6">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage6" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="dtDemurrage6" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage6" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="vlDemurrage6" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row ">
                                        <div id="boxescala3" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label"></label>
                                                <input id="escala3" type="text" class="form-control noborder text-center" disabled="disabled" value="3">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage3" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="dtDemurrage3" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage3" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="vlDemurrage3" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxescala7" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label"></label>
                                                <input id="escala7" type="text" class="form-control noborder text-center" disabled="disabled" value="7">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage7" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="dtDemurrage7" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage7" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="vlDemurrage7" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="boxescala4" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label"></label>
                                                <input id="escala4" type="text" class="form-control noborder text-center" disabled="disabled" value="4">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage4" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="dtDemurrage4" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage4" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="vlDemurrage4" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxescala8" class="col-sm-1">
                                            <div class="form-group text-center margBot">
                                                <label class="control-label"></label>
                                                <input id="escala8" type="text" class="form-control noborder text-center" disabled="disabled" value="8">
                                            </div>
                                        </div>
                                        <div id="boxdtDemurrage8" class="col-sm-1">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input id="dtDemurrage8" class="form-control" type="text" value="0">
                                            </div>
                                        </div>
                                        <div id="boxvlDemurrage8" class="col-sm-4">
                                            <div class="form-group margBot">
                                                <label class="control-label"><span class="required"></span></label>
                                                <input value="0" id="vlDemurrage8" class="form-control" type="text">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnSalvarDemurrage" class="btn btn-success" onclick="CadastrarDemurrageContainer()">Cadastrar Demurrage</button>
                                    <button type="button" id="btnEditarDemurrage" class="btn btn-success">Editar Demurrage</button>
                                    <button type="button" id="btnSalvarEditDemurrage" class="btn btn-success" onclick="EditarDemurrage()">Salvar Edição</button>
                                    <button type="button" id="btnCancelDemurrage" class="btn btn-danger">Cancelar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalPrint" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalPrintTitle">Imprimir Calculo</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div id="referPDF" class="modal-body">
                                    <div class="row" style="display: flex; justify-content: space-around; align-items:center; padding: 10px">
                                        <div>
                                            <img src="Content/imagens/FCA-Log(deitado).png" />
                                        </div>
                                        <span style="font-size: 20px">
                                            DEMONSTRATIVO DE CÁLCULO DEMURRAGE
                                        </span>
                                    </div>
                                    <div class="row" style="margin-top: 10px">
                                        <div class="col-sm-12">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">RAZÃO SOCIAL</label>
                                                <input id="txtRazaoC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">ENDEREÇO</label>
                                                <input id="txtEnderecoC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">MUNICIPIO</label>
                                                <input id="txtMunicipioC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">BAIRRO</label>
                                                <input id="txtBairroC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">UF</label>
                                                <input id="txtUFC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">CEP</label>
                                                <input id="txtCEPC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">CNPJ</label>
                                                <input id="txtCNPJC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">INSCRIÇÃO ESTADUAL</label>
                                                <input id="txtInscricaoEstadualC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row topMarg">
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">PROCESSO</label>
                                                <input id="txtProcessoC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">SERVIÇO</label>
                                                <input id="txtServicoC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">REF CLIENTE</label>
                                                <input id="txtRefClienteC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">ORIGEM</label>
                                                <input id="txtOrigemC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">SHIPPER</label>
                                                <input id="txtShipperC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">DESTINO</label>
                                                <input id="txtDestinoC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">MASTER</label>
                                                <input id="txtMasterC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">PESO(TON)</label>
                                                <input id="txtPesoC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">HOUSE</label>
                                                <input id="txtHouseC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">VOLUMES</label>
                                                <input id="txtVolumesC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">NAVIO</label>
                                                <input id="txtNavioC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group flexdiv">
                                                <label class="control-label">CUBAGEM</label>
                                                <input id="txtCubagemC" class="form-control noboxP" style="margin-left: 10px" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px">
                                        <div class="table-responsive">
                                            <table id="grdPrint" style="border-collapse: collapse; border-spacing: 0">
                                                <thead>
                                                    <tr>
                                                        <th rowspan="2" class="text-center">CONTÊINER</th>
                                                        <th rowspan="2" class="text-center">TIPO</th>
                                                        <th colspan="3" class="text-center">FREETIME</th>
                                                        <th colspan="3" class="text-center">DEMURRAGE</th>
                                                        <th rowspan="2" class="text-center">MOEDA</th>
                                                        <th rowspan="2" class="text-center">DIARIA</th>
                                                        <th rowspan="2" class="text-center">TOTAL</th>
                                                    </tr>
                                                    <tr>
                                                        <th class="text-center">INICIO</th>
                                                        <th class="text-center">FINAL</th>
                                                        <th class="text-center">DIAS</th>
                                                        <th class="text-center">INICIO</th>
                                                        <th class="text-center">FINAL</th>
                                                        <th class="text-center">DIAS</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="grdPrintBody">             
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnImprimir" class="btn btn-primary btn-ok">Imprimir Calculo</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalFaturas" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalFaturaTitle">Fatura Vendas</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="alert alert-success text-center" id="msgSuccessProcess">
                                            Fatura processada com sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrCancelar">
                                            Erro ao cancelar Fatura
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessCancelar">
                                            Fatura cancelada com sucesso.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgUpdtSuccess">
                                            Atualizado com Sucesso.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgExportSuccess">
                                            Fatura Exportada com Sucesso.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgDeleteSucess">
                                            Fatura Deletada com Sucesso.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgDeleteErr">
                                            Erro ao deletar Fatura.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgExportErr">
                                            Erro ao Exportar Fatura.
                                        </div>
                                    </div>
                                     <div class="functionFaturaBar">
                                        <div>
                                            <label>Fatura</label>
                                            <div class="btnFaturaFunc">
                                                <button type="button" id="btnNovaFatura" onclick="limparCampos()" class="btn btn-primary" data-toggle="modal" data-target="#modalNovaFatura">Nova</button>
                                                <button type="button" id="btnExcluirFatura" onclick="confirmExcluirFatura()" class="btn btn-primary">Excluir</button>
                                                <button type="button" id="btnCancelarFatura" onclick="infoCancelar()" class="btn btn-primary">Cancelar</button>
                                            </div>
                                        </div>
                                        <div class="boxFaturaFuncSec">
                                            <label>&nbsp;</label>
                                            <div class="btnFaturaFuncSec">
                                                <button type="button" id="btnAtualizacaoCambial" onclick="listarAtualizacaoCambial()" class="btn btn-primary">Atualização Cambial</button>           
                                                <button type="button" id="btnImprimirFatura" class="btn btn-primary" onclick="exportTableToCSVAtual('members.csv')">Imprimir Fatura</button>                
                                                <button type="button" id="btnExportarContaCorrente" class="btn btn-primary" onclick="exportarCC()">Exportar Conta Corrente</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg flexdiv">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Consultar por:<span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlFaturaFiltro" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label"><span class="required">&nbsp</span></label>
                                                <input id="txtConsultaFatura" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <button type="button" id="btnConsultaFatura" onclick="consultaFiltradaFatura()" class="btn btn-primary">Consultar</button>
                                            </div>
                                        </div>
                                    </div> 
                                    <div class="table-responsive tableFixHead">
                                        <table id="grdFatura" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">ID Fatura</th>
                                                    <th class="text-center" scope="col">Nº Processo</th>
                                                    <th class="text-center" scope="col">Cliente</th>
                                                    <th class="text-center" scope="col">Armador</th>
                                                    <th class="text-center" scope="col">Data Exportação</th>
                                                    <th class="text-center" scope="col">Data Liquidação</th>
                                                    <th class="text-center" scope="col">Data Cancelamento</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdFaturaBody">

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalNovaFatura" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalNovaFaturaTitle">Devolução Conteiner</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row flexdiv">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFatura" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <button type="button" id="btnEnviar" onclick="listarProcessoFatura()" class="btn btn-primary">Enviar</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive tableModalFix">
                                        <table id="grdProcessosFatura" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">Nº Container</th>
                                                    <th class="text-center" scope="col">Tipo</th>
                                                    <th class="text-center" scope="col">Moeda</th>
                                                    <th class="text-center" scope="col">Taxa Diária</th>
                                                    <th class="text-center" scope="col">Data Inicial Demurrage</th>
                                                    <th class="text-center" scope="col">Data Final Demurrage</th>
                                                    <th class="text-center" scope="col">Dias Demurrage</th>
                                                    <th class="text-center" scope="col">Valor Demurrage</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdProcessosFaturaBody">
                                
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnProcessar" onclick="processarFatura()" class="btn btn-primary btn-ok">Processar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalExcluirFatura" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalExcluirFaturaTitle">Excluir Fatura</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <h3>Tem certeza que deseja excluir a fatura?</h3>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnExcluirS" onclick="excluirFatura()" class="btn btn-primary btn-ok">Sim</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalCancelar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalCancelarTitle">Calcular Demurrage</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura</label>
                                                <input id="idFaturaCancelar" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFaturaCancelar" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteFaturaCancelar" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="control-label">Motivo do Cancelamento</label>
                                                <input id="dsMotivoCancelamento" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnCancelar" onclick="cancelarFatura()" class="btn btn-primary btn-ok">Cancelar Fatura</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalAtualizacaoCambial" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalAtualizacaoCambialTitle">Atualização Cambial</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura</label>
                                                <input id="idFaturaAtualizacaoCambial" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFaturaAtualizacaoCambial" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteFaturaAtualizacaoCambial" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Vencimento</label>
                                                <input id="dtVencimentoAtualizacaoCambial" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Cambio</label>
                                                <input id="dtCambioAtualizacao" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Valor Câmbio</label>
                                                <input id="vlCambioAtualizacao" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Conta Bancária</label>
                                                <asp:DropDownList ID="ddlContaBancaria" runat="server" CssClass="form-control" DataTextField="NM_CONTA_BANCARIA" DataValueField="ID_CONTA_BANCARIA"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive tableFixHead">
                                        <table id="grdAtualizacaoCambial" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">Nº Conteiner</th>
                                                    <th class="text-center" scope="col">Moeda</th>
                                                    <th class="text-center" scope="col">Valor Demurrage</th>
                                                    <th class="text-center" scope="col">Desconto</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdAtualizacaoCambialBody">

                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Desconto BRL</label>
                                                <input id="vlDescontoBr" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">&nbsp</label>
                                            <div class="form-group">
                                                <button type="button" id="btnAplicarDesconto" onclick="aplicarDesconto()" class="btn btn-primary btn-ok">Aplicar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnAtualizarCambio" onclick='atualizacaoCambial()' class="btn btn-primary btn-ok">Atualizar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalExportarContaCorrente" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalExportarContaCorrenteTitle">Exportar Conta Corrente</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura</label>
                                                <input id="idFaturaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFaturaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteFaturaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Liquidação</label>
                                                <input id="dtLiquidacaoFaturaContaCorrente" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Status</label>
                                                <asp:DropDownList ID="ddlStatusFaturaContaCorrente" runat="server" CssClass="form-control" DataTextField="DS_STATUS_DEMURRAGE" DataValueField="ID_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status</label>
                                                <input id="dtStatusFaturaContaCorrente" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnFaturaExportarContaCorrente" onclick="exportarConta()" class="btn btn-primary btn-ok">Exportar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalEstimativa" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalEstimativaTitle">Fatura Vendas</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="functionFaturaBar" style="justify-content:center">
                                        <div class="btnFaturaFunc">
                                            <button type="button" id="btnExportarEstimativaCSV" onclick="exportTableToCSVEstimativa('expectativaCompraVenda.csv')" class="btn btn-primary" >Exportar Grid para CSV</button>
                                        </div>
                                    </div>
                                    <div class="row flexdiv">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Consultar por:<span class="required">*</span></label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label"><span class="required">&nbsp</span></label>
                                                <input id="txtConsultaEstimativa" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <button type="button" id="btnConsultaEstimativa" onclick="consultaFiltradaFatura()" class="btn btn-primary">Consultar</button>
                                            </div>
                                        </div>
                                    </div> 
                                    <div class="table-responsive tableFixHead">
                                        <table id="grdEstimativa" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">Nº Processo</th>
                                                    <th class="text-center" scope="col">Nº CNTR</th>
                                                    <th class="text-center" scope="col">Tipo CNTR</th>
                                                    <th class="text-center" scope="col">Cliente</th>
                                                    <th class="text-center" scope="col">Transportador</th>
                                                    <th class="text-center" scope="col">Data Chegada</th>
                                                    <th class="text-center" scope="col">Free Time</th>
                                                    <th class="text-center" scope="col">Data Limite</th>
                                                    <th class="text-center" scope="col">Data Devolução</th>
                                                    <th class="text-center" scope="col">Qtd Dias Demurrage</th>
                                                    <th class="text-center" scope="col">Valor Compra Estimado</th>
                                                    <th class="text-center" scope="col">Moeda Compra</th>
                                                    <th class="text-center" scope="col">Valor Compra</th>
                                                    <th class="text-center" scope="col">Valor Compra R$</th>
                                                    <th class="text-center" scope="col">Data Pagamento</th>
                                                    <th class="text-center" scope="col">Valor Venda Estimado</th>
                                                    <th class="text-center" scope="col">Moeda Venda</th>
                                                    <th class="text-center" scope="col">Valor Venda</th>
                                                    <th class="text-center" scope="col">Valor Venda R$</th>
                                                    <th class="text-center" scope="col">Data Recebimento</th>
                                                    <th class="text-center" scope="col">Status</th>
                                                    <th class="text-center" scope="col">Data Status</th>
                                                    <th class="text-center" scope="col">Observação</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdEstimativaBody">

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="modal fade" id="modalDeleteDemurrage" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDeleteDemurrageTitle">Excluir Registro <span id="idTabela"></span></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    Tem certeza que deseja excluir o registro?
                                </div>
                                <div class="modal-footer">
                                    <input type="hidden" id="deletar-id">
                                    <button type="button" id="btnDeletar" onclick="DeletarDemurrage()" data-dismiss="modal" class="btn btn-primary btn-ok">Sim</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive tableFixHead">
                        <table id="grdDemurrageAtual" class="table tablecont">
                            <thead>
                                <tr>
                                    <th class="text-center" scope="col">#</th>
                                    <th class="text-center" scope="col">Nº Container</th>
                                    <th class="text-center" scope="col">Tipo Container</th>
                                    <th class="text-center" scope="col">Nº Processo</th>
                                    <th class="text-center" scope="col">Cliente</th>
                                    <th class="text-center" scope="col">Transportador</th>
                                    <th class="text-center" scope="col">Data Chegada</th>
                                    <th class="text-center" scope="col">Data Limite Chegada</th>
                                    <th class="text-center" scope="col">FreeTime</th>
                                    <th class="text-center" scope="col">Data Devolução</th>
                                    <th class="text-center" scope="col">Qtd Dias Demurrage</th>
                                    <th class="text-center" scope="col">Status</th>
                                    <th class="text-center" scope="col">Data Status</th>
                                    <th class="text-center" scope="col">Observação</th>
                                    <th class="text-center" scope="col">Data Calculo Demurrage Compra</th>
                                    <th class="text-center" scope="col">Demurrage Compra R$</th>
                                    <th class="text-center" scope="col">Data Pagamento</th>
                                    <th class="text-center" scope="col">Data Calculo Demurrage Venda</th>
                                    <th class="text-center" scope="col">Demurrage Venda R$</th>
                                    <th class="text-center" scope="col">Data Recebimento</th>
                                </tr>
                            </thead>
                            <tbody id="grdModuloDemurrage">             
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script> 
        window.onload = function () {
            document.getElementById("btnImprimir")
                .addEventListener("click", () => {
                    const pdf = this.document.getElementById("referPDF");
                    var opt = {
                        margin: 1,
                        filename: 'myfile.pdf',
                        image: { type: 'jpeg', quality: 1 },
                        html2canvas: { scale: 1 },
                        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
                    };
                    html2pdf().from(pdf).set(opt).save();
                })
        }
    </script>
    <script>
       
        var id = 0;
        var idFatura = 0;
        var idFaturaItens = 0;
        var values;
        var faturaV;
        var faturaItens;
        var contFaturaItens;
        var conter = 0;
        var pacote;
        var contFatura;
        var checkV = document.getElementById("venda");
        var checkC = document.getElementById("compra");
        var vlCheck;
        var cbs = document.getElementsByClassName('select');
        for (var i in cbs) {
            cbs[i].onclick = cbClick;
        }
        var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();
        var vlTaxa;
        var transportador; 

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTabela",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdDemurrageAtualBody").empty();
                    $("#grdDemurrageAtualBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdModuloDemurrage").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                            else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")' data-id='(" + dado[i]["ID_CNTR"] + "'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                            else {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                        }
                    }
                    else {
                        $("#grdModuloDemurrage").empty();
                        $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='20' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        });

        function cbClick() {
            var input = document.querySelector('input[data-id="' + this.getAttribute('data-id') + '"]:not([type="checkbox"])');
            input.disabled = !this.checked;

            // parentNode.parentNode = td > tr subindo a hierarquia
            if (this.checked) {
                // muda a cor do fudo quando for marcado
                input.parentNode.parentNode.style.background = '#e1e1e1';
            } else {
                // remove a cor do fundo ao desmarcar
                input.parentNode.parentNode.style.background = '';
            }
        }

        function DevolucaoContainer() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/infoContainerDevolucao",
                data: '{idCont:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        document.getElementById('nrProcessoDevolucao').value = dado[0]['NR_PROCESSO'];
                        document.getElementById('nmClienteDevolucao').value = dado[0]['CLIENTE'];
                        document.getElementById('dtStatusDevolucao').value = dataAtual = ano + '-' + mes + '-' + dia;
                        if (dado[0]['DT_DEVOLUCAO_CNTR'] == null) {
                            dado[0]['DT_DEVOLUCAO_CNTR'] = "";
                        }
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/listarContainerDevolucao",
                            data: '{nrProcesso:"' + dado[0]['NR_PROCESSO'] + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdDevolucaoContainerBody").empty();
                                $("#grdDevolucaoContainerBody").append("<tr><td colspan='5'><div class='loader'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdDevolucaoContainerBody").empty();
                                if (dado != null) {
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#grdDevolucaoContainerBody").append("<tr><td class='text-center'><div><input type='checkbox' class='cntr' value='" + dado[i]["ID_CNTR"] + "' name='cntr'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td></tr>");
                                    }
                                }
                            }
                        })
                    }
                }
            })
        }

        function atualizarDevolucao() {
            var pacote = document.querySelectorAll('[name=cntr]:checked');
            var values = [];
            for (var i = 0; i < pacote.length; i++) {
                values.push(pacote[i].value);
            }
            var dtStatus = document.getElementById('dtStatusDevolucao').value;
            var dsStatus = document.getElementById('MainContent_ddlStatusDevolucao').value;
            var dtDevolucao = document.getElementById('dtDevolucao').value;
            if (values.length > 0) {
                for (var c = 0; c < values.length; c++) {
                    $("#modalDevolucao").modal("hide");
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/atualizarDevolucao",
                        data: '{idCont:"' + values[c] + '",dtStatus:"' + dtStatus + '",dsStatus:"' + dsStatus + '",dtDevolucao:"' + dtDevolucao + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/infoContainerDevolucao",
                                data: '{idCont:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        document.getElementById('nrProcessoDevolucao').value = dado[0]['NR_PROCESSO'];
                                        document.getElementById('nmClienteDevolucao').value = dado[0]['CLIENTE'];
                                        document.getElementById('dtStatusDevolucao').value = dataAtual = ano + '-' + mes + '-' + dia;
                                        if (dado[0]['DT_DEVOLUCAO_CNTR'] == null) {
                                            dado[0]['DT_DEVOLUCAO_CNTR'] = "";
                                        }
                                        $.ajax({
                                            type: "POST",
                                            url: "DemurrageService.asmx/listarContainerDevolucao",
                                            data: '{nrProcesso:"' + dado[0]['NR_PROCESSO'] + '" }',
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            beforeSend: function () {
                                                $("#grdDevolucaoContainerBody").empty();
                                                $("#grdDevolucaoContainerBody").append("<tr><td colspan='5'><div class='loader'></div></td></tr>");
                                            },
                                            success: function (dado) {
                                                var dado = dado.d;
                                                dado = $.parseJSON(dado);
                                                $("#grdDevolucaoContainerBody").empty();
                                                if (dado != null) {
                                                    for (let i = 0; i < dado.length; i++) {
                                                        $("#grdDevolucaoContainerBody").append("<tr><td class='text-center'><div><input type='checkbox' class='cntr' value='" + dado[i]["ID_CNTR"] + "' name='cntr'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                                            "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td></tr>");
                                                    }
                                                }
                                            }
                                        })
                                    }
                                }
                            })
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarTabela",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                beforeSend: function () {
                                    $("#grdDemurrageAtualBody").empty();
                                    $("#grdDemurrageAtualBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                                },
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        $("#grdModuloDemurrage").empty();
                                        for (let i = 0; i < dado.length; i++) {
                                            if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                            }
                                            else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")' data-id='(" + dado[i]["ID_CNTR"] + "'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                            }
                                            else {
                                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                            }
                                        }
                                    }
                                    else {
                                        $("#grdModuloDemurrage").empty();
                                        $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                                    }
                                }
                            })
                        }
                    })
                }
            }
            else {
                values = [];
            }
        }

        function infoContainer() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/infoContainer",
                data: '{idCont:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        document.getElementById('nrProcesso').value = dado[0]['NR_PROCESSO'];
                        document.getElementById('nrContainer').value = dado[0]['NR_CNTR'];
                        document.getElementById('nmCliente').value = dado[0]['CLIENTE'];
                        document.getElementById('MainContent_dsStatus').value = dado[0]["ID_STATUS_DEMURRAGE"];
                        document.getElementById('dtStatus').value = dado[0]['DATA_STATUS_DEMURRAGE'];
                        document.getElementById('qtDiasFreeTime').value = dado[0]['QT_DIAS_FREETIME'];
                        if (dado[0]['DS_OBSERVACAO'] == null) {
                            document.getElementById('obsInfoCont').value = "";
                        }
                        else {
                            document.getElementById('obsInfoCont').value = dado[0]['DS_OBSERVACAO'];
                        }

                        if (dado[0]["ID_DEMURRAGE_FATURA_PAGAR"] == null && dado[0]["ID_DEMURRAGE_FATURA_RECEBER"] == null) {
                            document.getElementById('qtDiasFreeTime').disabled = false;
                            document.getElementById("nFaturado").style.display = "block";
                            document.getElementById("faturado").style.display = "none";
                        }
                        else {
                            document.getElementById('qtDiasFreeTime').disabled = true;
                            document.getElementById("nFaturado").style.display = "none";
                            document.getElementById("faturado").style.display = "block";
                        }
                    }
                }
            })
        }

        function atualizarContainer() {
            var dtStatus = document.getElementById("dtStatus").value;
            var dsStatus = document.getElementById("MainContent_dsStatus").value;
            var qtDiasFreeTime = document.getElementById("qtDiasFreeTime").value;
            var obsInfoCont = document.getElementById("obsInfoCont").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/atualizarContainer",
                data: '{idCont:"' + id + '",dtStatus:"' + dtStatus + '",qtDias:"' + qtDiasFreeTime + '",dsStatus: "' + dsStatus + '" ,dsObs:"' + obsInfoCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/listarTabela",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdDemurrageAtualBody").empty();
                            $("#grdDemurrageAtualBody").append("<tr><td colspan='19'><div class='loader'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                $("#grdModuloDemurrage").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    if (dado[i]["QT_DIAS_DEMURRAGE"] <= 10 && dado[i]["QT_DIAS_DEMURRAGE"] >= 1) {
                                        $("#grdModuloDemurrage").append("<tr style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                    }
                                    else if (dado[i]["QT_DIAS_DEMURRAGE"] < 1) {
                                        $("#grdModuloDemurrage").append("<tr style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                    }
                                    else {
                                        $("#grdModuloDemurrage").append("<tr><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                    }
                                }
                            }
                            else {
                                $("#grdModuloDemurrage").empty();
                                $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                            }
                        }
                    })
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/listarTabela",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdDemurrageAtualBody").empty();
                            $("#grdDemurrageAtualBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                $("#grdModuloDemurrage").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                        $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                    }
                                    else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                        $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")' data-id='(" + dado[i]["ID_CNTR"] + "'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                    }
                                    else {
                                        $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                    }
                                }
                            }
                            else {
                                $("#grdModuloDemurrage").empty();
                                $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                            }
                        }
                    })
                }
            })
        }
        
        function CalculoDemurrage() {
            if (checkV.checked) {
                vlCheck = checkV.value;
            }
            else {
                vlCheck = checkC.value;
            }
            if (vlCheck == 1) {
                document.getElementById("modalCalculoTitle").textContent = "Calcular Demurrage - Venda";
            } else {
                document.getElementById("modalCalculoTitle").textContent = "Calcular Demurrage - Compra";
            }
            if (id != 0) {
                $("#modalCaluclo").modal("show");
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoCalculo",
                    data: '{idCont:"' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("nrProcessoCalculo").value = dado[0]["PROCESSO"];
                            document.getElementById("nmClienteCalculo").value = dado[0]["CLIENTE"];
                            document.getElementById("MainContent_ddlTransportador").value = dado[0]["TRANSPORTADOR"];
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarCalculoDemurrage",
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                beforeSend: function () {
                                    $("#grdCalculoDemurrageBody").empty();
                                    $("#grdCalculoDemurrageBody").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                                },
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    $("#grdCalculoDemurrageBody").empty();
                                    if (dado != null) {
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#grdCalculoDemurrageBody").append("<tr><td class='text-center'><div><input type='checkbox' class='teste' value='" + dado[i]["ID_CNTR"] + "' name='checks'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td></tr>");
                                        }
                                    }
                                    else {
                                        $("#grdCalculoDemurrageBody").append("<tr><td colspan='12' class='text-center'>Não há Containers</td></tr>");
                                    }
                                }
                            })
                        }
                    }
                })
            }
            else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500); 
            }
        }

        function obterMarcados() {
            pacote = document.querySelectorAll('[name=checks]:checked');
            document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
            values = [];
            for (var i = 0; i < pacote.length; i++) {
                values.push(pacote[i].value);
            }

            if (values.length > 0) {
                if (vlCheck == 1) {
                    $("#modalCalucloSelecionados").modal("show");
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoVendaTaxa",
                        data: '{idCont:"' + values[0] + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                document.getElementById('nmTabelaCalculo').value = "FCA LOG";
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);
                            }
                        }
                    })
                }
            
            else {
                $("#modalCalucloSelecionados").modal("show");
                transportador = document.getElementById("MainContent_ddlTransportador").value;

                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoCalculoMarcadoCompraTaxa",
                    data: '{idCont:"' + values[0] + '", transportador: "' + transportador + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#btnCalcularSelecionado").prop('disabled', true);
                        $("#btnIgnorar").prop('disabled', true);
                        $("#btnZerarCalculo").prop('disabled', true);
                    },
                    success: function (dado) {
                        $("#btnCalcularSelecionado").prop('disabled', false);
                        $("#btnIgnorar").prop('disabled', false);
                        $("#btnZerarCalculo").prop('disabled', false);
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                            document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                            document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                            document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                            document.getElementById('nmTabelaCalculo').value = dado[0]['TABELA'];
                            document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                            if (dado[0]["FL_ESCALONADA"] != 0) {
                                $("#vlTaxa").prop('disabled', true);
                                document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                            } else {
                                $("#vlTaxa").prop('disabled', false);
                                document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                            }
                        }
                        else {
                            $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            document.getElementById('nrConteinerCalculo').value = "";
                            document.getElementById('nmTipoCont').value = "";
                            document.getElementById('qtDiasDemu').value = "";
                            document.getElementById('nmTabelaCalculo').value = "";
                            document.getElementById('nmMoeda').value = "";
                            $("#vlTaxa").prop('disabled', true);
                        }
                    }
                })
            }
        }
            else {
                values = [];
                $("#msgErrSelectCalc").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function calcularSelecionados() {
            vlTaxa = document.getElementById("vlTaxa").value;
            transportador = document.getElementById("MainContent_ddlTransportador").value;
            var idStatus = document.getElementById("MainContent_ddlStatusCalculoSelecionado").value;
            var dtStatus = document.getElementById("dtStatusCalculoSelecionado").value;
            if (dtStatus != "" && idStatus != 0) {
                if (vlCheck == 1) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/calcularDemurrageVenda",
                        data: '{idCont:"' + values[conter] + '",vlTaxa: "' + vlTaxa + '", idStatus: "' + idStatus + '", dtStatus: "' + dtStatus + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").hide();
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnProximo").show();
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                        }
                    })
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/calcularDemurrageCompra",
                        data: '{idCont:"' + values[conter] + '",vlTaxa: "' + vlTaxa + '",transportador: "' + transportador + '" ,idStatus: "' + idStatus + '", dtStatus: "' + dtStatus + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").hide();
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnProximo").show();
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                        }
                    })
                }
            }
            else {

            }
        }

        function proximo() {
            $("#btnCalcularSelecionado").show();
            $("#btnProximo").hide();
            conter++;
            transportador = document.getElementById("MainContent_ddlTransportador").value;
            if (conter < values.length) {
                if (vlCheck == 1) {
                     $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoVendaTaxa",
                         data: '{idCont:"' + values[conter] + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                document.getElementById('nmTabelaCalculo').value = "FCA LOG";
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);
                            }
                        }
                    })
                }
                else {               
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoCompraTaxa",
                        data: '{idCont:"' + values[conter] + '", transportador: "' + transportador + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                document.getElementById('nmTabelaCalculo').value = dado[0]['TABELA'];
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);
                            }
                        }
                    })   
                }
            }
            else {
                conter = 0;
                values = [];
                $('#modalCalucloSelecionados').modal('hide');
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoCalculo",
                    data: '{idCont:"' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("nrProcessoCalculo").value = dado[0]["PROCESSO"];
                            document.getElementById("nmClienteCalculo").value = dado[0]["CLIENTE"];
                            document.getElementById("MainContent_ddlTransportador").value = dado[0]["TRANSPORTADOR"];
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarCalculoDemurrage",
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                beforeSend: function () {
                                    $("#grdCalculoDemurrageBody").empty();
                                    $("#grdCalculoDemurrageBody").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                                },
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    $("#grdCalculoDemurrageBody").empty();
                                    if (dado != null) {
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#grdCalculoDemurrageBody").append("<tr><td class='text-center'><div><input type='checkbox' class='teste' value='" + dado[i]["ID_CNTR"] + "' name='checks'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td></tr>");
                                        }
                                    }
                                    else {
                                        $("#grdCalculoDemurrageBody").append("<tr><td colspan='12' class='text-center'>Não há Containers</td></tr>");
                                    }
                                }
                            })
                        }
                    }
                })
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarTabela",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdDemurrageAtualBody").empty();
                        $("#grdDemurrageAtualBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            $("#grdModuloDemurrage").empty();
                            for (let i = 0; i < dado.length; i++) {
                                if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                    $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                }
                                else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                    $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")' data-id='(" + dado[i]["ID_CNTR"] + "'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                }
                                else {
                                    $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                        "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                }
                            }
                        }
                        else {
                            $("#grdModuloDemurrage").empty();
                            $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                        }
                    }
                })
            }
        }
         
        function ignorar() {
            transportador = document.getElementById("MainContent_ddlTransportador").value;
            conter++;
            if (conter < values.length) {
                if (vlCheck == 1) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoVendaTaxa",
                        data: '{idCont:"' + values[conter] + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById('nmTabelaCalculo').value = "FCA LOG";
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);

                            }
                        }
                    })
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoCompraTaxa",
                        data: '{idCont:"' + values[conter] + '", transportador: "' + transportador +'"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                document.getElementById('nmTabelaCalculo').value = dado[0]['TABELA'];
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);
                                
                            }
                        }
                    })
                }
            }
            else {
                conter = 0;
                values = [];
                $('#modalCalucloSelecionados').modal('hide');
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoCalculo",
                    data: '{idCont:"' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("nrProcessoCalculo").value = dado[0]["PROCESSO"];
                            document.getElementById("nmClienteCalculo").value = dado[0]["CLIENTE"];
                            document.getElementById("MainContent_ddlTransportador").value = dado[0]["TRANSPORTADOR"];
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarCalculoDemurrage",
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                beforeSend: function () {
                                    $("#grdCalculoDemurrageBody").empty();
                                    $("#grdCalculoDemurrageBody").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                                },
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    $("#grdCalculoDemurrageBody").empty();
                                    if (dado != null) {
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#grdCalculoDemurrageBody").append("<tr><td class='text-center'><div><input type='checkbox' class='teste' value='" + dado[i]["ID_CNTR"] + "' name='checks'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td></tr>");
                                        }
                                    }
                                    else {
                                        $("#grdCalculoDemurrageBody").append("<tr><td colspan='12' class='text-center'>Não há Containers</td></tr>");
                                    }
                                }
                            })
                        }
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/listarTabela",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdDemurrageAtualBody").empty();
                                $("#grdDemurrageAtualBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#grdModuloDemurrage").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                            $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                        }
                                        else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                            $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")' data-id='(" + dado[i]["ID_CNTR"] + "'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                        }
                                        else {
                                            $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                        }
                                    }
                                }
                                else {
                                    $("#grdModuloDemurrage").empty();
                                    $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                                }
                            }
                        })
                    }
                })
            }
        }

        function zerarCalculo() {
            transportador = document.getElementById("MainContent_ddlTransportador").value;
            console.log(transportador);
            if (vlCheck == 1) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/zerarCalculoVenda",
                    data: '{idCont:"' + values[conter] + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#btnCalcularSelecionado").prop('disabled', true);
                        $("#btnIgnorar").prop('disabled', true);
                        $("#btnZerarCalculo").prop('disabled', true);
                    },
                    success: function () {
                        document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                    }
                })
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/zerarCalculoCompra",
                    data: '{idCont:"' + values[conter] + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#btnCalcularSelecionado").prop('disabled', true);
                        $("#btnIgnorar").prop('disabled', true);
                        $("#btnZerarCalculo").prop('disabled', true);
                    },
                    success: function () {
                        document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                    }
                })
            }
            conter++;
            if (conter < values.length) {
                if (vlCheck == 1) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoVendaTaxa",
                        data: '{idCont:"' + values[conter] + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                document.getElementById('nmTabelaCalculo').value = "FCA LOG";
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);
                            }
                        }
                    })        
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/infoCalculoMarcadoCompraTaxa",
                        data: '{idCont:"' + values[conter] + '", transportador: "' + transportador + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#btnCalcularSelecionado").prop('disabled', true);
                            $("#btnIgnorar").prop('disabled', true);
                            $("#btnZerarCalculo").prop('disabled', true);
                        },
                        success: function (dado) {
                            $("#btnCalcularSelecionado").prop('disabled', false);
                            $("#btnIgnorar").prop('disabled', false);
                            $("#btnZerarCalculo").prop('disabled', false);
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                document.getElementById('nrConteinerCalculo').value = dado[0]['NR_CNTR'];
                                document.getElementById('nmTipoCont').value = dado[0]['NM_TIPO_CONTAINER'];
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE'];
                                document.getElementById('nmTabelaCalculo').value = dado[0]['TABELA'];
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"].replace(",", ".");
                                }
                            }
                            else {
                                $("#msgErrTable").fadeIn(500).delay(1000).fadeOut(500);
                                $("#btnCalcularSelecionado").prop('disabled', true);
                                document.getElementById('nrConteinerCalculo').value = "";
                                document.getElementById('nmTipoCont').value = "";
                                document.getElementById('qtDiasDemu').value = "";
                                document.getElementById('nmTabelaCalculo').value = "";
                                document.getElementById('nmMoeda').value = "";
                                $("#vlTaxa").prop('disabled', true);
                            }
                        }
                    })
                }     
            }
            else {
                conter = 0;
                values = [];
                $('#modalCalucloSelecionados').modal('hide');
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoCalculo",
                    data: '{idCont:"' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("nrProcessoCalculo").value = dado[0]["PROCESSO"];
                            document.getElementById("nmClienteCalculo").value = dado[0]["CLIENTE"];
                            document.getElementById("MainContent_ddlTransportador").value = dado[0]["TRANSPORTADOR"];
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarCalculoDemurrage",
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                beforeSend: function () {
                                    $("#grdCalculoDemurrageBody").empty();
                                    $("#grdCalculoDemurrageBody").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                                },
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    $("#grdCalculoDemurrageBody").empty();
                                    if (dado != null) {
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#grdCalculoDemurrageBody").append("<tr><td class='text-center'><div><input type='checkbox' class='teste' value='" + dado[i]["ID_CNTR"] + "' name='checks'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td></tr>");
                                        }
                                    }
                                    else {
                                        $("#grdCalculoDemurrageBody").append("<tr><td colspan='12' class='text-center'>Não há Containers</td></tr>");
                                    }
                                }
                            })
                        }
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/listarTabela",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdDemurrageAtualBody").empty();
                                $("#grdDemurrageAtualBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#grdModuloDemurrage").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                            $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                        }
                                        else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                            $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")' data-id='(" + dado[i]["ID_CNTR"] + "'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                        }
                                        else {
                                            $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                                "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                                        }
                                    }
                                }
                                else {
                                    $("#grdModuloDemurrage").empty();
                                    $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                                }
                            }
                        })
                    }
                })
            }
        }

        function imprimirDadosCalculo() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/imprimirDadosCalc",
                data: '{id:"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdProcessosFaturaBody").empty();
                    $("#grdProcessosFaturaBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        document.getElementById("txtRazaoC").value = dado[0]["NM_RAZAO"];
                        document.getElementById("txtEnderecoC").value = dado[0]["ENDERECO"] + dado[0]["NR_ENDERECO"];
                        document.getElementById("txtMunicipioC").value = dado[0]["NM_CIDADE"];
                        document.getElementById("txtBairroC").value = dado[0]["BAIRRO"];
                        document.getElementById("txtUFC").value = dado[0]["NM_ESTADO"];
                        document.getElementById("txtCEPC").value = dado[0]["CEP"];
                        document.getElementById("txtCNPJC").value = dado[0]["CNPJ"];
                        document.getElementById("txtProcessoC").value = dado[0]["NR_PROCESSO"];
                        document.getElementById("txtServicoC").value = dado[0]["NM_SERVICO"];
                        document.getElementById("txtOrigemC").value = dado[0]["ORIGEM"];
                        document.getElementById("txtDestinoC").value = dado[0]["DESTINO"];
                        

                    }
                    else {
                        
                    }
                    
                }
            })
        }

        function listarFatura() {
            idFatura = 0;
            if (checkV.checked) {
                vlCheck = checkV.value;
            }
            else {
                vlCheck = checkC.value;
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarFaturas",
                data: '{check:"' + vlCheck + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdFaturaBody").empty();
                    $("#grdFaturaBody").append("<tr><td colspan='8'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdFaturaBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdFaturaBody").append("<tr data-id='" + dado[i]["ID_DEMURRAGE_FATURA"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setIdFatura(" + dado[i]["ID_DEMURRAGE_FATURA"] + ")'>Selecionar</div></td>" +
                                "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_FATURA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_TRANSPORTADOR"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO"] + "</td><td class='text-center'>" + dado[i]["DT_CANCELAMENTO"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdFaturaBody").empty();
                        $("#grdFaturaBody").append("<tr id='msgEmptyWeek'><td colspan='8' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function limparCampos() {
            $("#grdProcessosFaturaBody").empty();
        }

        function listarProcessoFatura() {
            var processoFatura = document.getElementById("nrProcessoFatura").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/verificarProcesso",
                data: '{processo:"' + processoFatura + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#btnEnviar").prop('disabled', true);
                },
                success: function (dado) {
                    console.log(dado);
                    if (dado != null) {
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/listarProcessoFaturas",
                            data: '{processo:"' + processoFatura + '",check: "' + vlCheck + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdProcessosFaturaBody").empty();
                                $("#grdProcessosFaturaBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#grdProcessosFaturaBody").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#grdProcessosFaturaBody").append("<tr><td class='text-center'><div><input type='checkbox' class='fatura' value='" + dado[i]["ID_CNTR_BL"] + "' name='fatura'/></div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NM_MOEDA"] + "</td><td class='text-center'>" + dado[i]["TAXA_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DT_INICIAL_DEMURRAGE"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_FINAL_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE"] + "</td></tr>");
                                    }
                                }
                                else {
                                    $("#grdProcessosFaturaBody").empty();
                                    $("#grdProcessosFaturaBody").append("<tr id='msgEmptyWeek'><td colspan='9' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                                }
                                $("#btnEnviar").prop('disabled', false);
                            }
                        })
                    }
                    else {

                    }
                }
            })

        }

        function processarFatura() {
            var processoFatura = document.getElementById("nrProcessoFatura").value;
            contFatura = document.querySelectorAll('[name=fatura]:checked');
            faturaV = [];
            for (var i = 0; i < contFatura.length; i++) {
                faturaV.push(contFatura[i].value);
            }
            if (faturaV.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/processarFatura",
                    data: '{processo:"' + processoFatura + '",check: "' + vlCheck + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function (dado) {
                        for (var x = 0; x < faturaV.length; x++) {
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/processarFaturaItens",
                                data: '{idcntr:"' + faturaV[x] + '",check: "' + vlCheck + '",processo: "'+processoFatura+'"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    
                                }
                            })
                        }
                        listarFatura();
                        $("#modalNovaFatura").modal('hide');
                        $("#msgSuccessProcess").fadeIn(500).delay(1000).fadeOut(500);
                    }
                })
            }
        }

        function infoCancelar() {
            if (idFatura != 0) {
                $("#modalCancelar").modal("show");
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoCancelar",
                    data: '{idFatura:"' + idFatura + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("idFaturaCancelar").value = dado[0]["ID_DEMURRAGE_FATURA"];
                            document.getElementById("nrProcessoFaturaCancelar").value = dado[0]["NR_PROCESSO"];
                            document.getElementById("nmClienteFaturaCancelar").value = dado[0]["NM_CLIENTE"];
                        }
                        else {

                        }
                    }
                })
            }
        }

        function confirmExcluirFatura() {
            if (idFatura != 0) {
                $("#modalExcluirFatura").modal('show');
            }
        }

        function excluirFatura() {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/excluirFatura",
                    data: '{idFatura:"' + idFatura + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success: function (dado) {
                        $("#msgDeleteSucess").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        $("#modalExcluirFatura").modal('hide');
                    },
                    error: function (err) {
                        $("#msgDeleteErr").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        $("#modalExcluirFatura").modal('hide');
                    }

                })
            
        }

        function cancelarFatura() {
            motivoCancelamento = document.getElementById("dsMotivoCancelamento").value; 
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/cancelarFatura",
                data: '{idFatura:"' + idFatura + '", motivoCancelamento: "'+motivoCancelamento+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#msgSuccessCancelar").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    else {
                        $("#msgErrCancelar").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
            $("#modalCancelar").modal("hide");
        }

        function listarAtualizacaoCambial() {
            if (idFatura != 0) {
                $("#modalAtualizacaoCambial").modal("show");
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoAtualizacao",
                    data: '{idFatura: "' + idFatura + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("idFaturaAtualizacaoCambial").value = dado[0]["ID_DEMURRAGE_FATURA"];
                            document.getElementById("nrProcessoFaturaAtualizacaoCambial").value = dado[0]["NR_PROCESSO"];
                            document.getElementById("nmClienteFaturaAtualizacaoCambial").value = dado[0]["CLIENTE"];
                            document.getElementById("dtCambioAtualizacao").value = dataAtual = ano + '-' + mes + '-' + dia;
                            document.getElementById("dtVencimentoAtualizacaoCambial").value = dataAtual = ano + '-' + mes + '-' + dia;
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarFaturasAtualizacaoCambial",
                                data: '{idFatura: "' + dado[0]["ID_DEMURRAGE_FATURA"] + '",check: "' + vlCheck + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        $("#grdAtualizacaoCambialBody").empty();
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#grdAtualizacaoCambialBody").append("<tr id='r" + dado[i]["ID_CNTR_BL"] + "s' data-id='" + dado[i]["ID_CNTR_BL"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setIdFaturaItens(" + dado[i]["ID_CNTR_BL"] +")'>Selecionar</div></td>" +
                                                "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_MOEDA"] + "</td><td class='text-center vlDemurrage'>" + dado[i]["VL_DEMURRAGE"] + "</td>" +
                                                "<td class='text-center desconto'>" + dado[i]["DESCONTO"] + "</td></tr> ");
                                        }
                                    }
                                    else {
                                        $("#grdAtualizacaoCambialBody").empty();
                                        $("#grdAtualizacaoCambialBody").append("<tr id='msgEmptyWeek'><td colspan='8' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                                    }
                                }
                            })
                        }
                        else {
                        }
                    }
                })
            }
        }

        function atualizacaoCambial() {
            var idFaturaAtualizacaoCambial = document.getElementById("idFaturaAtualizacaoCambial").value;
            var dtVencimento = document.getElementById("dtVencimentoAtualizacaoCambial").value;
            var dtCambio = document.getElementById("dtCambioAtualizacao").value;
            var vlCambio = document.getElementById("vlCambioAtualizacao").value;
            var idContaBancaria = document.getElementById("MainContent_ddlContaBancaria").value;
            var linhas = document.querySelectorAll(".desconto");
            var linhas2 = document.querySelectorAll(".vlDemurrage");
            var desc = document.querySelectorAll("#grdAtualizacaoCambialBody tr");
            var valorCambio = new String(vlCambio);
            var vcambio = valorCambio.replace(",", ".");
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/atualizacaoCambialFatura",
                data: '{idFatura:"' + idFaturaAtualizacaoCambial + '", dtVencimento: "' + dtVencimento + '", idContaBancaria: "' + idContaBancaria + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    for (var ind = 0; ind < linhas.length; ind++) {
                        var vlDemurrage = linhas2[ind].textContent.toString().replace(",", ".");
                        var descontoBRL = linhas[ind].textContent.toString().replace(",", ".");
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/atualizacaoCambialContainer",
                            data: '{idCntr:"' + desc[ind].attributes[1].value + '",dtCambio: "' + dtCambio + '", vlCambio: "' + vcambio + '",vlDemurrage:"' + vlDemurrage +'" ,descontoBRL: "' + descontoBRL + '",check:"'+vlCheck+'"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {
                                $("#modalAtualizacaoCambial").modal("hide");
                                $("#msgUpdtSuccess").fadeIn(500).delay(1000).fadeOut(500);
                            }
                        })
                    }
                }
            })

        }

        function aplicarDesconto() {
            var desc = document.querySelector("#r" + idFaturaItens + "s td:nth-child(5)");
            desc.textContent = document.getElementById("vlDescontoBr").value;
        }

        function exportarCC() {
            if (idFatura != 0) {
                $("#modalExportarContaCorrente").modal("show");
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoExportCC",
                    data: '{idFatura:"' + idFatura + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("idFaturaContaCorrente").value = dado[0]["ID_DEMURRAGE_FATURA"];
                            document.getElementById("nrProcessoFaturaContaCorrente").value = dado[0]["NR_PROCESSO"];
                            document.getElementById("nmClienteFaturaContaCorrente").value = dado[0]["CLIENTE"];
                            document.getElementById("dtLiquidacaoFaturaContaCorrente").value = dataAtual = ano + '-' + mes + '-' + dia;
                            document.getElementById("MainContent_ddlStatusFaturaContaCorrente").value = dado[0]["ID_STATUS_DEMURRAGE"];
                            document.getElementById("dtStatusFaturaContaCorrente").value = dataAtual = ano + '-' + mes + '-' + dia;
                        }
                        else {

                        }
                    }
                })
            }
        }

        function exportarConta() {
            var dtLiquidacao = document.getElementById("dtLiquidacaoFaturaContaCorrente").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/exportarCC",
                data: '{idFatura:"' + idFatura + '",dtLiquidacao: "' + dtLiquidacao +'", check: "'+vlCheck+'"}',
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                },
                success: function (dado) {
                    if (dado != null) {
                        $("#modalExportarContaCorrente").modal("hide");
                        $("#msgExportSuccess").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                    }
                    else {
                        $("#modalExportarContaCorrente").modal("hide");
                        $("#msgExportErr").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                    }
                }
            })
        }

        function estimativaCV() {
            var estimadoCompra;
            var estimadoVenda;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarEstimativa",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdEstimativaBody").empty();
                    $("#grdEstimativaBody").append("<tr><td colspan='23'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdEstimativaBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdEstimativaBody").append("<tr><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_FREETIME"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'><input type='checkbox' id='chkCompra"+i+"' name='vlEstimadoCompra["+i+"]'></td><td class='text-center'>" + dado[i]["MOEDA_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VALOR_COMPRA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VALOR_COMPRA_REAL"] + "</td><td class='text-center'>" + dado[i]["DATA_PAGAMENTO"] + "</td><td class='text-center'><input type='checkbox' id='chkVenda" + i + "' name='vlEstimadoVenda[" + i +"]'></td>" +
                                "<td class='text-center'>" + dado[i]["MOEDA_VENDA"] + "</td><td class='text-center'>" + dado[i]["VALOR_VENDA"] + "</td><td class='text-center'>" + dado[i]["VALOR_VENDA_REAL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DATA_RECEBIMENTO"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DT_STATUS_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td></tr > ");
                            if (dado[i]["VALOR_COMPRA_ESTIMADO"] == 1) {
                                $("#chkCompra" + i + "").prop('checked', true)
                            }
                            else {
                                $("#chkCompra" + i + "").prop('checked',false);
                            }

                            if (dado[i]["VALOR_VENDA_ESTIMADO"] == 1) {
                                $("#chkVenda" + i + "").prop('checked', true)
                            }
                            else {
                                $("#chkVenda" + i + "").prop('checked', false);
                            }
                        }
                    }
                    else {                        
                        $("#grdEstimativaBody").empty();
                        $("#grdEstimativaBody").append("<tr id='msgEmptyWeek'><td colspan='23' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function setId(Id){
            id = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdFatura(Id) {
            idFatura = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdFaturaItens(Id) {
            idFaturaItens = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

    </script>
    <script>
        $("#btnNovaDemurrage").click(function () {
            $("#btnSalvarDemurrage").show();
            $("#btnSalvarDemurrage").removeProp("disabled");
            $("#btnEditarDemurrage").hide();
            $("#btnSalvarEditDemurrage").hide();
            $("#btnCancelDemurrage").hide();
            $("#listDemurrage").hide();
            var forms = ['MainContent_ddlParceiroTransportador',
                'MainContent_ddlTipoContainer',
                'dtValidade',
                'qtFreetime',
                'MainContent_ddlMoeda',
                'MainContent_checkEsc',
                'MainContent_checkInicioFreetime',
                'dtDemurrage1',
                'vlDemurrage1',
                'dtDemurrage2',
                'vlDemurrage2',
                'dtDemurrage3',
                'vlDemurrage3',
                'dtDemurrage4',
                'vlDemurrage4',
                'dtDemurrage5',
                'vlDemurrage5',
                'dtDemurrage6',
                'vlDemurrage6',
                'dtDemurrage7',
                'vlDemurrage7',
                'dtDemurrage8',
                'vlDemurrage8'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
            }
            var formsempty = ['MainContent_ddlParceiroTransportador',
                'MainContent_ddlTipoContainer',
                'dtValidade',
                'qtFreetime',
                'MainContent_ddlMoeda',
                'dtDemurrage1',
                'vlDemurrage1']
            for (let i = 0; i < formsempty.length; i++) {
                var aux = document.getElementById(formsempty[i]);
                aux.value = "";
            }

            var emptyvalues = ['dtDemurrage2',
                'vlDemurrage2',
                'dtDemurrage3',
                'vlDemurrage3',
                'dtDemurrage4',
                'vlDemurrage4',
                'dtDemurrage5',
                'vlDemurrage5',
                'dtDemurrage6',
                'vlDemurrage6',
                'dtDemurrage7',
                'vlDemurrage7',
                'dtDemurrage8',
                'vlDemurrage8']
            for (let i = 0; i < emptyvalues.length; i++) {
                var x = document.getElementById(emptyvalues[i]);
                x.value = "0";
            }
        })

        function CadastrarDemurrageContainer() {
            var checkbox = document.getElementById("MainContent_checkEsc");
            var checkboxvalue;
            var checkboxfreetime = document.getElementById("MainContent_checkInicioFreetime");
            var checkboxfreetimevalue;
            if (checkbox.checked) {
                checkboxvalue = "1";
            }
            else {
                checkboxvalue = "0";
            }
            if (checkboxfreetime.checked) {
                checkboxfreetimevalue = "1";
            }
            else {
                checkboxfreetimevalue = "0";
            }
            var dado = {
                "ID_PARCEIRO_TRANSPORTADOR": document.getElementById("MainContent_ddlParceiroTransportador").value,
                "ID_TIPO_CONTAINER": document.getElementById("MainContent_ddlTipoContainer").value,
                "DT_VALIDADE_INICIAL": document.getElementById("dtValidade").value,
                "QT_DIAS_FREETIME": document.getElementById("qtFreetime").value,
                "ID_MOEDA": document.getElementById("MainContent_ddlMoeda").value,
                "FL_ESCALONADA": checkboxvalue,
                "FL_INICIO_CHEGADA": checkboxfreetimevalue,
                "QT_DIAS_01": document.getElementById("dtDemurrage1").value,
                "VL_VENDA_01": document.getElementById("vlDemurrage1").value,
                "QT_DIAS_02": document.getElementById("dtDemurrage2").value,
                "VL_VENDA_02": document.getElementById("vlDemurrage2").value,
                "QT_DIAS_03": document.getElementById("dtDemurrage3").value,
                "VL_VENDA_03": document.getElementById("vlDemurrage3").value,
                "QT_DIAS_04": document.getElementById("dtDemurrage4").value,
                "VL_VENDA_04": document.getElementById("vlDemurrage4").value,
                "QT_DIAS_05": document.getElementById("dtDemurrage5").value,
                "VL_VENDA_05": document.getElementById("vlDemurrage5").value,
                "QT_DIAS_06": document.getElementById("dtDemurrage6").value,
                "VL_VENDA_06": document.getElementById("vlDemurrage6").value,
                "QT_DIAS_07": document.getElementById("dtDemurrage7").value,
                "VL_VENDA_07": document.getElementById("vlDemurrage7").value,
                "QT_DIAS_08": document.getElementById("dtDemurrage8").value,
                "VL_VENDA_08": document.getElementById("vlDemurrage8").value
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CadastrarDemurrageContainer",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#btnSalvarDemurrage").prop("disabled", "disabled");
                },
                success: function (dado) {
                    $("#modalDemurrage").modal('hide');
                    console.log(dado.d);
                    if (dado.d == "1") {
                        $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "0") {
                        $("#msgErrDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "2") {
                        $("#msgErrExistDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                },
                error: function () {
                    $("#modalDemurrage").modal('hide');
                    $("#msgErrDemu").fadeIn(500).delay(1000).fadeOut(500);
                }
            })
        }
        $("#btnCancelDemurrage").click(function () {
            $("#btnEditarDemurrage").show();
            $("#btnSalvarEditDemurrage").hide();
            $("#btnCancelDemurrage").hide();
            var forms = ['MainContent_ddlParceiroTransportador',
                'MainContent_ddlTipoContainer',
                'dtValidade',
                'qtFreetime',
                'MainContent_ddlMoeda',
                'MainContent_checkEsc',
                'MainContent_checkInicioFreetime',
                'dtDemurrage1',
                'vlDemurrage1',
                'dtDemurrage2',
                'vlDemurrage2',
                'dtDemurrage3',
                'vlDemurrage3',
                'dtDemurrage4',
                'vlDemurrage4',
                'dtDemurrage5',
                'vlDemurrage5',
                'dtDemurrage6',
                'vlDemurrage6',
                'dtDemurrage7',
                'vlDemurrage7',
                'dtDemurrage8',
                'vlDemurrage8'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                $(aux).attr("disabled", "true");
            }
        })
        $("#btnEditarDemurrage").click(function () {
            $("#btnEditarDemurrage").hide();
            $("#btnSalvarEditDemurrage").show();
            $("#btnCancelDemurrage").show();
            var forms = ['MainContent_ddlParceiroTransportador',
                'MainContent_ddlTipoContainer',
                'dtValidade',
                'qtFreetime',
                'MainContent_ddlMoeda',
                'MainContent_checkEsc',
                'MainContent_checkInicioFreetime',
                'dtDemurrage1',
                'vlDemurrage1',
                'dtDemurrage2',
                'vlDemurrage2',
                'dtDemurrage3',
                'vlDemurrage3',
                'dtDemurrage4',
                'vlDemurrage4',
                'dtDemurrage5',
                'vlDemurrage5',
                'dtDemurrage6',
                'vlDemurrage6',
                'dtDemurrage7',
                'vlDemurrage7',
                'dtDemurrage8',
                'vlDemurrage8'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
            }
        })

        function EditarDemurrage() {
            var checkbox = document.getElementById("MainContent_checkEsc");
            var checkboxvalue;
            var checkboxfreetime = document.getElementById("MainContent_checkInicioFreetime");
            var checkboxfreetimevalue;
            if (checkbox.checked) {
                checkboxvalue = "1";
            }
            else {
                checkboxvalue = "0";
            }
            if (checkboxfreetime.checked) {
                checkboxfreetimevalue = "1";
            }
            else {
                checkboxfreetimevalue = "0";
            }
            var dadosEdit = {
                "ID_TABELA_DEMURRAGE": document.getElementById("ddlDemurrage").value,
                "ID_PARCEIRO_TRANSPORTADOR": document.getElementById("MainContent_ddlParceiroTransportador").value,
                "ID_TIPO_CONTAINER": document.getElementById("MainContent_ddlTipoContainer").value,
                "DT_VALIDADE_FINAL": document.getElementById("dtValidade").value,
                "QT_DIAS_FREETIME": document.getElementById("qtFreetime").value,
                "ID_MOEDA": document.getElementById("MainContent_ddlMoeda").value,
                "FL_ESCALONADA": checkboxvalue,
                "FL_INICIO_CHEGADA": checkboxfreetimevalue,
                "QT_DIAS_01": document.getElementById("dtDemurrage1").value,
                "VL_VENDA_01": document.getElementById("vlDemurrage1").value,
                "QT_DIAS_02": document.getElementById("dtDemurrage2").value,
                "VL_VENDA_02": document.getElementById("vlDemurrage2").value,
                "QT_DIAS_03": document.getElementById("dtDemurrage3").value,
                "VL_VENDA_03": document.getElementById("vlDemurrage3").value,
                "QT_DIAS_04": document.getElementById("dtDemurrage4").value,
                "VL_VENDA_04": document.getElementById("vlDemurrage4").value,
                "QT_DIAS_05": document.getElementById("dtDemurrage5").value,
                "VL_VENDA_05": document.getElementById("vlDemurrage5").value,
                "QT_DIAS_06": document.getElementById("dtDemurrage6").value,
                "VL_VENDA_06": document.getElementById("vlDemurrage6").value,
                "QT_DIAS_07": document.getElementById("dtDemurrage7").value,
                "VL_VENDA_07": document.getElementById("vlDemurrage7").value,
                "QT_DIAS_08": document.getElementById("dtDemurrage8").value,
                "VL_VENDA_08": document.getElementById("vlDemurrage8").value
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/EditarDemurrageContainer",
                data: JSON.stringify({ dadosEdit: (dadosEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#btnSalvarEditDemurrage").prop("disabled", "disabled");
                },
                success: function (dado) {
                    $("#modalDemurrage").modal('hide');
                    if (dado.d == "1") {
                        $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/ListarDemurrageContainer",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdDemurrageContainer").empty();
                                $("#grdDemurrageContainer").append("<tr><td colspan='3'><div class='loader'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#grdDemurrageContainer").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#grdDemurrageContainer").append("<tr><td class='text-center'> " + dado[i]["NM_TIPO_CONTAINER"] + "</td > <td class='text-center'>" + dado[i]["DT_VALIDADE_FINAL_FORMAT"] + "</td>" +
                                            "<td class='text-center'><div class='btn btn-primary pad' data-toggle='modal' data-target='#modalDemurrage' onclick='BuscarDemurrage(" + dado[i]["ID_TABELA_DEMURRAGE"] + ")'><i class='fas fa-eye'></i></div>" +
                                            "<div class='deleteDemurrage btn btn-primary pad' onclick='DeletarDemurrage(" + dado[i]["ID_TABELA_DEMURRAGE"] + ")'><i class='fas fa-trash'></i></div></td ></tr > ");
                                    }
                                }
                                else {
                                    $("#grdDemurrageContainer").empty();
                                    $("#grdDemurrageContainer").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                                }
                            }
                        })
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/DemurrageList",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#ddlDemurrage").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#ddlDemurrage").append("<option value='" + dado[i]["ID_TABELA_DEMURRAGE"] + "'>" + dado[i]["NM_TIPO_CONTAINER"] + "</option>");
                                    }
                                }
                            }
                        })
                    }
                    if (dado.d == "0") {
                        $("#msgErrDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "2") {
                        $("#msgErrExistDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function DeletarDemurrage() {
            var Id = document.getElementById("deletar-id").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/DeletarDemurrage",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        $("#msgSuccessDeletDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    else {
                        $("#msgErrDeletDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/ListarDemurrageContainer",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdDemurrageContainer").empty();
                            $("#grdDemurrageContainer").append("<tr><td colspan='3'><div class='loader'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != null) {
                                $("#grdDemurrageContainer").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdDemurrageContainer").append("<tr><td class='text-center'> " + dado[i]["NM_TIPO_CONTAINER"] + "</td > <td class='text-center'>" + dado[i]["DT_VALIDADE_FINAL_FORMAT"] + "</td>" +
                                        "<td class='text-center'><div class='btn btn-primary pad' data-toggle='modal' data-target='#modalDemurrage' onclick='BuscarDemurrage(" + dado[i]["ID_TABELA_DEMURRAGE"] + ")'><i class='fas fa-eye'></i></div>" +
                                        "<div class='deleteDemurrage btn btn-primary pad' data-id='" + dado[i]["ID_TABELA_DEMURRAGE"] + "' onclick='SetId(" + dado[i]["ID_TABELA_DEMURRAGE"] + ")'><i class='fas fa-trash'></i></div></td ></tr > ");
                                }
                            }
                            else {
                                $("#grdDemurrageContainer").empty();
                                $("#grdDemurrageContainer").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                            }
                        }
                    })
                }
            })
        }

        function SetId(Id) {
            $("#modalDeleteDemurrage").modal('show');
            $("#deletar-id").val(Id);
        }
        function data_valida(date) {
            var matches = /(\d{4})[-.\/](\d{2})[-.\/](\d{2})/.exec(date);
            if (matches == null) {
                return false;
            }
            var dia = matches[3];
            var mes = matches[2] - 1;
            var ano = matches[1];
            var data = new Date(ano, mes, dia);
            return data.getDate() == dia && data.getMonth() == mes && data.getFullYear() == ano;
                }

        function ValidaData(data) {
            reg = /[^\d\/\.]/gi;                  // Mascara = dd/mm/aaaa | dd.mm.aaaa
            var valida = data.replace(reg, '');    // aplica mascara e valida só numeros
            if (valida && valida.length == 10) {  // é válida, então ;)
                var ano = data.substr(6),
                    mes = data.substr(3, 2),
                    dia = data.substr(0, 2),
                    M30 = ['04', '06', '09', '11'],
                    v_mes = /(0[1-9])|(1[0-2])/.test(mes),
                    v_ano = /(19[1-9]\d)|(20\d\d)|2100/.test(ano),
                    rexpr = new RegExp(mes),
                    fev29 = ano % 4 ? 28 : 29;

                if (v_mes && v_ano) {
                    if (mes == '02') return (dia >= 1 && dia <= fev29);
                    else if (rexpr.test(M30)) return /((0[1-9])|([1-2]\d)|30)/.test(dia);
                    else return /((0[1-9])|([1-2]\d)|3[0-1])/.test(dia);
                }
            }
            return false                           // se inválida :(
        }

        function consultaFiltrada() {
            var idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            var stringConsulta = document.getElementById("txtConsulta").value;
            var finalizado = document.getElementById("MainContent_chkFinalizado");
            var finalizadoValue;
            var ativo = document.getElementById("MainContent_chkAtivo");
            var ativoValue;
            if (ativo.checked) {
                ativoValue = "1";
            }
            else {
                ativoValue = "0";
            }

            if (finalizado.checked) {
                finalizadoValue = "1";
            }
            else {
                finalizadoValue = "0";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/filtrarTabela",
                data: '{idFilter:"' + idFiltro + '", Filter:"' + stringConsulta + '", Finalizado: "' + finalizadoValue + '",Ativo: "' + ativoValue + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdModuloDemurrage").empty();
                    $("#grdModuloDemurrage").append("<tr><td colspan='20'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdModuloDemurrage").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["QT_DIAS_DEMURRAGE"] <= -10 && dado[i]["QT_DIAS_DEMURRAGE"] >= -1) {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR_BL"] + "' style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR_BL"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                            else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR_BL"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR_BL"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                            else {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR_BL"] + "'><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR_BL"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                        }
                    }
                    else {
                        $("#grdModuloDemurrage").empty();
                        $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='20' class='alert alert-light text-center'>Resultado não encontrado</td></tr>");
                    }
                }
            });
        }

        function downloadCSVAtual(csv, filename) {
            var csvFile;
            var downloadLink;

            // CSV file
            csvFile = new Blob(["\uFEFF"+csv], { type: "text/csv;charset=utf-8;" });

            // Download link
            downloadLink = document.createElement("a");

            // File name
            downloadLink.download = filename;

            // Create a link to the file
            downloadLink.href = window.URL.createObjectURL(csvFile);

            // Hide download link
            downloadLink.style.display = "none";

            // Add the link to DOM
            document.body.appendChild(downloadLink);

            // Click download link
            downloadLink.click();

        }

        function exportTableToCSVAtual(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdModuloDemurrage tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdModuloDemurrage td, #grdModuloDemurrage th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVAtual(csv.join("\n"), filename);
        }

        function downloadCSVEstimativa(csv, filename) {
            var csvFile;
            var downloadLink;

            // CSV file
            csvFile = new Blob(["\uFEFF" + csv], { type: "text/csv;charset=utf-8;" });

            // Download link
            downloadLink = document.createElement("a");

            // File name
            downloadLink.download = filename;

            // Create a link to the file
            downloadLink.href = window.URL.createObjectURL(csvFile);

            // Hide download link
            downloadLink.style.display = "none";

            // Add the link to DOM
            document.body.appendChild(downloadLink);

            // Click download link
            downloadLink.click();

        }

        function exportTableToCSVEstimativa(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdEstimativa tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdEstimativa td, #grdEstimativa th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVEstimativa(csv.join("\n"), filename);
        }
                
    </script>

</asp:Content>
