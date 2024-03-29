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
                                Selecione um Processo/Container.
                            </div>
                        </div>
                    </div>
                    <div class="functionBar">
                        <div class="btnBoxFunc">
                            <button type="button" id="btnNovaDemurrage" class="btn btn-primary" data-toggle="modal" data-target="#modalTabelaDemurrage">Cadastro Tabela de Demurrage</button>
                            <button type="button" id="btnEditarInfoCntr" class="btn btn-primary" onclick="infoContainer()">Editar Info. CNTR</button>
                            <button type="button" id="btnAtualizarDataDevo" class="btn btn-primary" onclick="DevolucaoContainer()">Atualizar Data Devol</button>
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
                                <button type="button" id="btnImprimirCalc" onclick="imprimirDadosCalculo()" class="btn btn-primary">Imprimir Cálculo</button>                
                                <button type="button" id="btnFatura" class="btn btn-primary" onclick="listarFatura()">Faturas</button>
                            </div>
                        </div>
                        <div class="oFunc">
                            <a href="ConferenciaContaCorrente.aspx" id="btnEstimativa" class="btn btn-primary">Conferencia Conta Corrente</a> 
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
                        <div class="col-sm-1">
                            <div class="form-group">
                                <button type="button" id="btnConsulta" onclick="consultaFiltrada()" class="btn btn-primary">Consultar</button>
                            </div>
                        </div>
                        <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                            <div>
                                <asp:CheckBox ID="chkFinalizado" runat="server" CssClass="form-control noborder" Text="&nbsp;Finalizados"></asp:CheckBox>
                            </div>
                            <div>
                                <asp:CheckBox ID="chkAtivo" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Ativos"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                            <div>
                                <asp:CheckBox ID="chkTarifaSpot" runat="server" CssClass="form-control noborder" Text="&nbsp;Tarifa Spot"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="row topMarg">
                        
                        
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
                                    <div class="alert alert-danger text-center" id="msgErrEditCont">
                                            Preencha todos os campos Obrigatórios
                                    </div>
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
                                                <label class="control-label">Data Status<span class="required">*</span></label>
                                                <input id="dtStatus" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-9">
                                            <div class="form-group">
                                                <label class="control-label">Status<span class="required">*</span></label>
                                                <asp:DropDownList ID="dsStatus" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status Compra<span class="required">*</span></label>
                                                <input id="dtStatusCompra" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-9">
                                            <div class="form-group">
                                                <label class="control-label">Status Compra<span class="required">*</span></label>
                                                <asp:DropDownList ID="dsStatusCompra" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Quant. Dias de FreeTime</label>
                                                <input id="qtDiasFreeTime" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Quant. Dias de FreeTime Confirmado</label>
                                                <input id="qtDiasFreeTimeConfirm" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" id="divDemurrageCompra">
                                            <div class="form-group">
                                                <label class="control-label">Quant. Dias de Demurrage Compra</label>
                                                <input id="qtDiasDemurrageCompra" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" id="divDemurrageVenda">
                                            <div class="form-group">
                                                <label class="control-label">Quant. Dias de Demurrage Venda</label>
                                                <input id="qtDiasDemurrageVenda" class="form-control" type="text"/>
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
                                    <button type="button" id="btnEditarInfoCont" onclick="atualizarContainer()" class="btn btn-primary btn-ok">Atualizar</button>
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
                                    <div class="alert alert-danger text-center" id="msgErrSelectDevol">
                                        Selecione um Container
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrDados">
                                        Preencha os campos Obrigatórios
                                    </div>
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
                                                <label class="control-label">Status<span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlStatusDevolucao" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status<span class="required">*</span></label>
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
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
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
                                                    <th class="text-center" scope="col">Dias Demu Venda</th>
                                                    <th class="text-center" scope="col">Dias Demu Compra</th>
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
                                        <div class="alert alert-danger text-center" id="msgErrDadoCalc">
                                            Preencha todos os dados obrigatórios
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
                                            <label class="control-label">Status<span class="required">*</span></label>
                                            <div class="form-group"  ID="divStatusCalculoVenda">                                                
                                                <asp:DropDownList ID="ddlStatusCalculoSelecionado" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                                </div>
                                                 <div class="form-group" ID="divStatusCalculoCompra">
                                                     <asp:DropDownList ID="ddlStatusCalculoSelecionadoCompra" runat="server" class="form-control" type="text" DataValueField="ID_STATUS_DEMURRAGE" DataTextField="DS_STATUS_DEMURRAGE"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Status<span class="required">*</span></label>
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

                    <div class="modal fade bd-example-modal-lg" id="modalTabelaDemurrage" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalTabelaDemurrageTitle">Tabela Demurrage</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="alert alert-danger text-center" id="msgErrTabelaDemurrage">
                                            Erro ao cadastrar/atualizar.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessTabelaDemurrage">
                                            Registro cadastrado/atualizado com sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgExistsTabelaDemurrage">
                                            Registro já existente.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccesDeleteDemurrage">
                                            Tabela deletada com sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrDeleteDemurrage">
                                            Erro ao deletar tabela.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">ARMADOR</label>
                                                <select id="ddlfiltroTabelaDemu" class="form-control" ></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive tableFixHead">
                                        <table class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">Tipo Container</th>
                                                    <th class="text-center" scope="col">Data de Validade Inicial</th>
                                                    <th class="text-center" scope="col">Carga IMO</th>
                                                    <th class="text-center" scope="col"></th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdDemurrageContainer">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button id="btnCadastrarNovaDemurrage" type="button" class="btn btn-success" data-toggle="modal" data-target="#modalDemurrage">Novo Cadastro</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
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
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Tamanho Container <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlTamanhoContainer" runat="server" CssClass="form-control" DataTextField="CD_TAMANHO_CONTAINER" DataValueField="ID_TAMANHO_CONTAINER" Enabled="false"></asp:DropDownList>
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
                                        <div class="col-sm-2">
                                            <div class="form-group dflex">
                                                <label class="control-label">&nbsp;</label>
                                                <asp:CheckBox ID="checkIMO" runat="server" CssClass="form-control noborder" Text="&nbsp;&nbsp;Carga IMO"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group dflex">
                                                <label class="control-label">&nbsp;</label>
                                                <asp:CheckBox ID="checkInicioFreetime" runat="server" CssClass="form-control noborder" Text="&nbsp;&nbsp;Iniciar Free Time na Data de Chegada" Checked="true"></asp:CheckBox>
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
                                                <input id="dtDemurrage1" class="form-control" type="text" maxlength="4" />
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
                                                <input id="dtDemurrage5" class="form-control" value="0" type="text" maxlength="4" >
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
                                                <input id="dtDemurrage2" class="form-control" type="text" value="0" maxlength="4" >
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
                                                <input id="dtDemurrage6" class="form-control" type="text" value="0" maxlength="4" >
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
                                                <input id="dtDemurrage3" class="form-control" type="text" value="0" maxlength="4" >
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
                                                <input id="dtDemurrage7" class="form-control" type="text" value="0" maxlength="4" >
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
                                                <input id="dtDemurrage4" class="form-control" type="text" value="0" maxlength="4" >
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
                                                <input id="dtDemurrage8" class="form-control" type="text" value="0" maxlength="4" >
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
                                    <div class="topMarg">
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">Razão Social:</span>
                                            <span id="txtRazaoC" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">Endereço:</span>
                                            <span id="txtEnderecoC" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">Município:</span>
                                            <span id="txtMunicipioC" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">Bairro:</span>
                                            <span id="txtBairroC" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">UF:</span>
                                            <span id="txtUFC" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">CEP:</span>
                                            <span id="txtCEPC" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">CNPJ:</span>
                                            <span id="txtCNPJ" style="margin-left: 10px"></span>
                                        </div>
                                        <div style="display: flex;">
                                            <span style="font-weight: bold; color: black;">Inscrição Estadual:</span>
                                            <span id="txtInscricaoEstadualC" style="margin-left: 10px"></span>
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
                                    <h5 class="modal-title" id="modalFaturaTitle"></h5>
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
                                        <div class="alert alert-danger text-center" id="msgSelectErrFatura">
                                            Selecione uma Fatura.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgCambioErrFatura">
                                            Falta Atualização Cambial.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgExportErrFatura">
                                            Fatura já exportada.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgDemuFinalErrFatura">
                                            Fatura já finalizada.
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
                                        <div class="alert alert-danger text-center" id="msgDeleteErr">
                                            Erro ao deletar Fatura.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgExportErr">
                                            Erro ao Exportar Fatura.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrExportDadoConta">
                                            Selecione uma Conta Bancária na aba Atualização Cambial
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessUploadArquivo">
                                            Fatura Exportada com Sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrUploadArquivo">
                                            Fatura já Exportada.
                                        </div>
                                    </div>
                                     <div class="functionFaturaBar">
                                        <div>
                                            <label>Fatura</label>
                                            <div class="btnFaturaFunc">
                                                <button type="button" id="btnNovaFatura" onclick="limparCampos()" class="btn btn-primary" data-toggle="modal" data-target="#modalNovaFatura">Nova</button>
                                                <button type="button" id="btnExcluirFatura" onclick="confirmExcluirFatura()" class="btn btn-primary">Excluir</button>
                                                <button type="button" id="btnCancelarFatura" onclick="infoCancelar()" class="btn btn-primary">Cancelar Exportação Fatura</button>
                                            </div>
                                        </div>
                                        <div class="boxFaturaFuncSec">
                                            <label>&nbsp;</label>
                                            <div class="btnFaturaFuncSec">
                                                <button type="button" id="btnAtualizacaoCambial" onclick="listarAtualizacaoCambial()" class="btn btn-primary">Atualização Cambial</button>           
                                                <button type="button" id="btnImprimirFatura" class="btn btn-primary" onclick="imprimirFatura()">Imprimir Fatura</button>                
                                                <button type="button" id="btnExportarContaCorrente" class="btn btn-primary" onclick="exportarCC()">Exportar Conta Corrente</button>
                                                <button type="button" id="btnImprimirRecibo" class="btn btn-primary" onclick="ImprimirRecibo()">Imprimir Recibo</button>
                                                <button type="button" id="btnParcelarDemurrage" class="btn btn-primary" onclick="ParcelarDemurrage()">Parcelar Demurrage</button>
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
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <button type="button" id="btnConsultaFatura" onclick="consultaFatura()" class="btn btn-primary">Consultar</button>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                            <div>
                                                <asp:CheckBox ID="chkFaturaF" runat="server" CssClass="form-control noborder" Text="&nbsp;Finalizados"></asp:CheckBox>
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkFaturaA" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Ativos"></asp:CheckBox>
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
                                                    <th class="text-center" scope="col">Parcelas</th>
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
                                    <h5 class="modal-title" id="modalNovaFaturaTitle">Nova Fatura</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="alert alert-danger text-center" id="msgSelectErrFaturaProcessar">
                                        Selecione uma Fatura.
                                    </div>
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
                                    <h5 class="modal-title" id="modalCancelarTitle">Cancelar Fatura</h5>
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
                                    <div class="alert alert-danger text-center" id="msgSelectErrFaturaItens">
                                        Selecione uma Fatura.
                                    </div>
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
                                                    <th class="text-center" scope="col">Juros / Multas</th>
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
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Juros / Multa</label>
                                                <input id="vlMultaBr" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">&nbsp</label>
                                            <div class="form-group">
                                                <button type="button" id="btnAplicarMulta" onclick="aplicarMulta()" class="btn btn-primary btn-ok">Aplicar</button>
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

                    <div class="modal fade bd-example-modal-lg" id="modalParcelamentoDemurrage" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title modalTitle" id="modalParcelamentoDemurrageTitle">Parcelamento Demurrage</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="alert alert-success text-center msg" id="msgSuccessParcela">
                                        Registro cadastrado/atualizado com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center msg" id="msgErrParcela">
                                        Erro ao atualizar
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura</label>
                                                <input id="idFaturaParcelamentoDemurrage" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFaturaParcelamentoDemurrage" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteFaturaParcelamentoDemurrage" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <!--<div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Vencimento</label>
                                                <input id="dtVencimentoParcelamentoDemurrage" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Cambio</label>
                                                <input id="dtCambioParcelamentoDemurrage" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Valor Câmbio</label>
                                                <input id="vlCambioParcelamentoDemurrage" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Conta Bancária</label>
                                                <asp:DropDownList ID="ddlContaBancariaParcelamentoDemurrage" runat="server" CssClass="form-control" DataTextField="NM_CONTA_BANCARIA" DataValueField="ID_CONTA_BANCARIA"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>-->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Valor Parcela</label>
                                                <input id="vlParcelamentoDemurrage" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Valor Total Demurrage</label>
                                                <input id="vlRecebidoParcelamentoDemurrage" class="form-control" type="text" value="0.00"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Nº Parcelas</label>
                                                <input id="qtNumeroParcelas" class="form-control" type="text" value="1"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Data 1ª Parcela</label>
                                                <input id="dtInicialParcelamentoDemurrage" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive tableFixHead">
                                        <table id="grdParcelamentoDemurrage" class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">Nº Parcela</th>
                                                    <th class="text-center" scope="col">Data Vencimento</th>
                                                    <th class="text-center" scope="col">Valor Parcela</th>
                                                    <th class="text-center" scope="col">Juros</th>
                                                    <th class="text-center" scope="col">Valor Parcela Juros</th>
                                                    <th class="text-center" scope="col">Pago</th>
                                                    <th class="text-center" scope="col">#</th>
                                                    <th class="text-center" scope="col">#</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdParcelamentoDemurrageBody">

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnParcelamentoDemurrage" onclick='ParcelamentoDemurrage()' class="btn btn-primary btn-ok">Gerar Parcela</button>
                                    <button type="button" id="btnFinalizarParcelamentoDemurrage" onclick='finalizarFaturaDemurrage()' class="btn btn-primary btn-ok">Finalizar Demurrage</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-sm" id="modalInfoParcela" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:HiddenField id="idParcela" runat="server"/>
                                    <h5 class="modal-title modalTitle">Dados Parcela</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Valor Parcela</label>
                                                <input id="vlParcelaEdit" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Data Vencimento</label>
                                                <input id="dtVencimentoEdit" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Juros %</label>
                                                <input id="vlJurosEdit" class="form-control" type="text" value="0"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                            <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Valor Parcela Juro</label>
                                                <input id="vlParcelaJurosEdit" class="form-control" type="text"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <input type="checkbox" id="checkPago" name="Pago"/>
                                                <label for="checkTroca">Pago</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnVincular" onclick="editarParcela()" data-dismiss="modal" class="btn btn-primary btn-ok">Salvar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalDeleteParcela" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title modalTitle" id="modalExcluirParcelaTitle">Excluir Parcela - <span id="idParcelaDelete"></span></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <h3>Tem certeza que deseja excluir a parcela?</h3>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnExcluirP" onclick="excluirParcela()" class="btn btn-primary btn-ok">Sim</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalExportarParcelaContaCorrente" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title modalTitle" id="modalExportarParcelaContaCorrenteTitle">Exportar Conta Corrente</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="alert alert-danger text-center msg" id="msgErrExportParcelaDado">
                                        Preencha todos os Dados Obrigatórios
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura</label>
                                                <input id="idFaturaContaCorrenteParcela" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura Parcela</label>
                                                <input id="idFaturaParcelaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFaturaParcelaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteFaturaParcelaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Liquidação</label>
                                                <input id="dtLiquidacaoFaturaParcelaContaCorrente" class="form-control" type="date"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnFaturaParcelaExportarContaCorrente" onclick="exportarParcelaConta()" class="btn btn-primary btn-ok">Exportar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalDialogParcela" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title modalTitle" id="modalDialogParcelaTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <p>Tem certeza que deseja cancelar a exportação da parcela?</p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnConfirm" onclick="exportarParcelaConta()" class="btn btn-primary btn-ok">Exportar</button>
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
                                    <div class="alert alert-danger text-center" id="msgErrExportDado">
                                        Preencha todos os Dados Obrigatórios
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">ID Fatura</label>
                                                <input id="idFaturaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Processo</label>
                                                <input id="nrProcessoFaturaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Cliente</label>
                                                <input id="nmClienteFaturaContaCorrente" class="form-control nobox" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Liquidação</label>
                                                <input id="dtLiquidacaoFaturaContaCorrente" class="form-control" type="date"/>
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
                                    <h5 class="modal-title" id="modalEstimativaTitle"></h5>
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
                                                    <th class="text-center" scope="col">Tipo Carga</th>
                                                    <th class="text-center" scope="col">Tipo CNTR</th>
                                                    <th class="text-center" scope="col">MBL</th>
                                                    <th class="text-center" scope="col">Cliente</th>
                                                    <th class="text-center" scope="col">Transportador</th>
                                                    <th class="text-center" scope="col">Data Chegada</th>
                                                    <th class="text-center" scope="col">Free Time</th>
                                                    <th class="text-center" scope="col">FreeTime Confirmado</th>
                                                    <th class="text-center" scope="col">Data Limite</th>
                                                    <th class="text-center" scope="col">Data Devolução</th>
                                                    <th class="text-center" scope="col">Qtd Dias Demurrage</th>
                                                    <th class="text-center" scope="col">Valor Compra Estimado</th>
                                                    <th class="text-center" scope="col">Moeda Compra</th>
                                                    <th class="text-center" scope="col">Valor Compra</th>
                                                    <th class="text-center" scope="col">Valor Compra</th>
                                                    <th class="text-center" scope="col">Data Pagamento</th>
                                                    <th class="text-center" scope="col">Valor Venda Estimado</th>
                                                    <th class="text-center" scope="col">Moeda Venda</th>
                                                    <th class="text-center" scope="col">Valor Venda</th>
                                                    <th class="text-center" scope="col">Valor Venda</th>
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
                                    <th class="text-center" scope="col">Nº Processo</th>
                                    <th class="text-center" scope="col">Tipo Container</th>
                                    <th class="text-center" scope="col">Tipo Carga</th>
                                    <th class="text-center" scope="col">Nº Container</th>
                                    <th class="text-center" scope="col">MBL</th>
                                    <th class="text-center" scope="col">Cliente</th>
                                    <th class="text-center" scope="col">Transportador</th>
                                    <th class="text-center" scope="col">Data Chegada</th>
                                    <th class="text-center" scope="col">Data Limite Chegada</th>
                                    <th class="text-center" scope="col">Acordo Comercial</th>
                                    <th class="text-center" scope="col">FreeTime</th>
                                    <th class="text-center" scope="col">FreeTime Confirmado</th>
                                    <th class="text-center" scope="col">Data Devolução</th>
                                    <th class="text-center" scope="col">Dias DEM Venda</th>
                                    <th class="text-center" scope="col">Dias DEM Compra</th>
                                    <th class="text-center" scope="col">Observação</th>
                                    <th class="text-center" scope="col">Status Compra</th>                                    
                                    <th class="text-center" scope="col">Id Fatura Compra</th>
                                    <th class="text-center" scope="col">Demurrage Compra</th>
                                    <th class="text-center" scope="col">Demurrage Compra BRL</th>
                                    <th class="text-center" scope="col">Data Pagamento</th>
                                    <th class="text-center" scope="col">Status Venda</th>
                                    <th class="text-center" scope="col">Id Fatura Venda</th>                                    
                                    <th class="text-center" scope="col">Demurrage Venda</th>
                                    <th class="text-center" scope="col">Demurrage Venda BRL</th>
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
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script> 
</script>
    <script>
        var armadortabela;
        var armadorT = 0;
        var id = 0;
        var idFatura = 0;
        var atualizaCambio = 0;
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
            consultaFiltrada();
            insertListaArmador()
        });

        $("#MainContent_checkIMO").change(function () {
            var checkboxIMO = document.getElementById("MainContent_checkIMO");
            var listTamanhoContainer = document.getElementById("MainContent_ddlTamanhoContainer");
            var listTipoContainer = document.getElementById("MainContent_ddlTipoContainer");
            if (checkboxIMO.checked) {
                listTamanhoContainer.removeAttribute("Disabled");
                $(listTipoContainer).attr("disabled", "true");
                listTipoContainer.value = "";
            } else {
                $(listTamanhoContainer).attr("disabled", "true");
                listTipoContainer.removeAttribute("Disabled");
                listTamanhoContainer.value = "";
            }
        })

        $("#ddlfiltroTabelaDemu").change(function () {
            armadorT = document.getElementById("ddlfiltroTabelaDemu").value;
            listarTabelaDemurrage();
        })

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
        function insertListaArmador() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/carregarArmador",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#ddlfiltroTabelaDemu").empty();
                    $("#ddlfiltroTabelaDemu").append("<option value=''>Selecione</option>");
                    if (dado != null) {
                        for (var i = 0; i < dado.length; i++) {
                            $("#ddlfiltroTabelaDemu").append("<option value='" + dado[i]["ID_PARCEIRO"] + "'>" + dado[i]["NM_RAZAO"] + "</option>");
                        }
                    }
                }
            })
        }

        function listarTabelaDemurrage() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ListarDemurrageContainer",
                data: '{armador: "' + armadorT + '" }',
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
                            $("#grdDemurrageContainer").append("<tr><td class='text-center'>" + dado[i]["TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["DT_VALIDADE_INICIAL_FORMAT"] + "</td><td class='text-center'>" + dado[i]["CARGA_IMO"] + "</td>" +
                                "<td class='text-center'> <div class='btn btn-primary pad' data-toggle='modal' data-target='#modalDemurrage' onclick='BuscarDemurrage(" + dado[i]["ID_TABELA_DEMURRAGE"] + ")'><i class='fas fa-eye'></i></div>" +
                                "<div class='deleteDemurrage btn btn-primary pad' data-id='" + dado[i]["ID_TABELA_DEMURRAGE"] + "' onclick='SetIdDelete(" + dado[i]["ID_TABELA_DEMURRAGE"] + ")'><i class='fas fa-trash'></i></div></td></tr>");
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

        function BuscarDemurrage(Id) {
            $("#btnSalvarEditDemurrage").removeProp("disabled");
            $("#listDemurrage").show();
            $("#btnSalvarDemurrage").hide();
            $("#btnEditarDemurrage").show();
            $("#btnSalvarEditDemurrage").hide();
            $("#btnEditarDemurrage").removeProp("disabled");
            $("#btnCancelDemurrage").hide();
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscarDemurrage",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data.FL_ESCALONADA == "True") {
                        $("#MainContent_checkEsc").prop('checked', true);
                    }
                    else {
                        $("#MainContent_checkEsc").prop('checked', false);
                    }
                    if (data.FL_CARGA_IMO == "True") {
                        $("#MainContent_checkIMO").prop('checked', true);
                    }
                    else {
                        $("#MainContent_checkIMO").prop('checked', false);
                    }
                    if (data.FL_INICIO_CHEGADA == "True") {
                        $("#MainContent_checkInicioFreetime").prop('checked', true);
                    }
                    else {
                        $("#MainContent_checkInicioFreetime").prop('checked', false);
                    }
                    document.getElementById("ddlDemurrage").value = data.ID_TABELA_DEMURRAGE;
                    document.getElementById("MainContent_ddlParceiroTransportador").value = data.ID_PARCEIRO_TRANSPORTADOR;
                    document.getElementById("MainContent_ddlTipoContainer").value = data.ID_TIPO_CONTAINER;
                    document.getElementById("dtValidade").value = data.DT_VALIDADE_INICIAL;
                    document.getElementById("qtFreetime").value = data.QT_DIAS_FREETIME;
                    document.getElementById("MainContent_ddlMoeda").value = data.ID_MOEDA;
                    document.getElementById("dtDemurrage1").value = data.QT_DIAS_01;
                    document.getElementById("vlDemurrage1").value = data.VL_VENDA_01;
                    document.getElementById("dtDemurrage2").value = data.QT_DIAS_02;
                    document.getElementById("vlDemurrage2").value = data.VL_VENDA_02;
                    document.getElementById("dtDemurrage3").value = data.QT_DIAS_03;
                    document.getElementById("vlDemurrage3").value = data.VL_VENDA_03;
                    document.getElementById("dtDemurrage4").value = data.QT_DIAS_04;
                    document.getElementById("vlDemurrage4").value = data.VL_VENDA_04;
                    document.getElementById("dtDemurrage5").value = data.QT_DIAS_05;
                    document.getElementById("vlDemurrage5").value = data.VL_VENDA_05;
                    document.getElementById("dtDemurrage6").value = data.QT_DIAS_06;
                    document.getElementById("vlDemurrage6").value = data.VL_VENDA_06;
                    document.getElementById("dtDemurrage7").value = data.QT_DIAS_07;
                    document.getElementById("vlDemurrage7").value = data.VL_VENDA_07;
                    document.getElementById("dtDemurrage8").value = data.QT_DIAS_08;
                    document.getElementById("vlDemurrage8").value = data.VL_VENDA_08;
                    document.getElementById("MainContent_ddlTamanhoContainer").value = data.ID_TAMANHO_CONTAINER;


                    var forms = ['MainContent_ddlParceiroTransportador',
                        'MainContent_ddlTipoContainer',
                        'dtValidade',
                        'qtFreetime',
                        'MainContent_ddlMoeda',
                        'MainContent_checkEsc',
                        'MainContent_checkIMO',
                        'MainContent_checkInicioFreetime',
                        'MainContent_ddlTamanhoContainer',
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
                }
            })
        };

        $("#btnCadastrarNovaDemurrage").click(function () {
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
                'MainContent_checkIMO',
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
                'MainContent_ddlTamanhoContainer',
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
                'MainContent_checkIMO',
                'MainContent_checkInicioFreetime',
                'MainContent_ddlTamanhoContainer',
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
                'MainContent_checkIMO',
                'MainContent_checkInicioFreetime',
                'MainContent_ddlTamanhoContainer',
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
            var checkboxIMO = document.getElementById("MainContent_checkIMO");
            var checkboxIMOvalue;
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
            if (checkboxIMO.checked) {
                checkboxIMOvalue = "1";
            } else {
                checkboxIMOvalue = "0";
            }

            var dadosEdit = {
                "ID_TABELA_DEMURRAGE": document.getElementById("ddlDemurrage").value,
                "ID_PARCEIRO_TRANSPORTADOR": document.getElementById("MainContent_ddlParceiroTransportador").value,
                "ID_TIPO_CONTAINER": document.getElementById("MainContent_ddlTipoContainer").value,
                "DT_VALIDADE_INICIAL": document.getElementById("dtValidade").value,
                "QT_DIAS_FREETIME": document.getElementById("qtFreetime").value,
                "ID_MOEDA": document.getElementById("MainContent_ddlMoeda").value,
                "FL_ESCALONADA": checkboxvalue,
                "FL_INICIO_CHEGADA": checkboxfreetimevalue,
                "FL_CARGA_IMO": checkboxIMOvalue,
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
                "VL_VENDA_08": document.getElementById("vlDemurrage8").value,
                "ID_TAMANHO_CONTAINER": document.getElementById("MainContent_ddlTamanhoContainer").value
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
                    listarTabelaDemurrage();
                    if (dado.d == "1") {
                        $("#msgSuccessTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "0") {
                        $("#msgErrTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "2") {
                        $("#msgExistsTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
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
                    listarTabelaDemurrage();
                    if (data.d == "1") {
                        $("#msgSuccesDeleteDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    else {
                        $("#msgErrDeleteDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function SetIdDelete(Id) {
            $("#modalDeleteDemurrage").modal('show');
            $("#deletar-id").val(Id);
        }

        function DevolucaoContainer() {
            if (id != 0) {
                $("#modalDevolucao").modal("show");
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
            else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
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
            if (dsStatus != "") {
                if (values.length > 0) {

                    $("#modalDevolucao").modal("hide");
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/atualizarDevolucao",
                        data: '{idCont:"' + values + '",dtStatus:"' + dtStatus + '",dsStatus:"' + dsStatus + '",dtDevolucao:"' + dtDevolucao + '" }',
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
                            consultaFiltrada();
                        }
                    })

                }
                else {
                    values = [];
                    $("#msgErrSelectDevol").fadeIn(500).delay(1000).fadeOut(500);
                }
            }
            else {
                $("#msgErrDados").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function infoContainer() {
            if (id != 0) {
                $("#modalEditContInfo").modal("show");
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
                            document.getElementById('MainContent_dsStatusCompra').value = dado[0]["ID_STATUS_DEMURRAGE_COMPRA"];
                            document.getElementById('dtStatus').value = dado[0]['DATA_STATUS_DEMURRAGE'];
                            document.getElementById('dtStatusCompra').value = dado[0]['DATA_STATUS_DEMURRAGE_COMPRA'];
                            document.getElementById('qtDiasFreeTime').value = dado[0]['QT_DIAS_FREETIME'];
                            document.getElementById('qtDiasFreeTimeConfirm').value = dado[0]['QT_DIAS_FREETIME_CONFIRMA'];
                            document.getElementById('qtDiasDemurrageCompra').value = dado[0]['QT_DIAS_DEMURRAGE_COMPRA'];
                            document.getElementById('qtDiasDemurrageVenda').value = dado[0]['QT_DIAS_DEMURRAGE'];
                            if (dado[0]['QT_DIAS_DEMURRAGE_COMPRA'] == null) {
                                document.getElementById('divDemurrageCompra').style = 'display:none';
                            } else {
                                document.getElementById('divDemurrageCompra').style = 'display:block';
                            }

                            if (dado[0]['QT_DIAS_DEMURRAGE'] == null) {
                                document.getElementById('divDemurrageVenda').style = 'display:none';
                            } else {
                                document.getElementById('divDemurrageVenda').style = 'display:block';
                            }

                            if (dado[0]['DS_OBSERVACAO'] == null) {
                                document.getElementById('obsInfoCont').value = "";
                            }
                            else {
                                document.getElementById('obsInfoCont').value = dado[0]['DS_OBSERVACAO'];
                            }

                            if (dado[0]["ID_DEMURRAGE_FATURA_PAGAR"] == null && dado[0]["ID_DEMURRAGE_FATURA_RECEBER"] == null) {
                                document.getElementById('qtDiasFreeTime').disabled = false;

                            }
                            else {
                                document.getElementById('qtDiasFreeTime').disabled = true;
                            }
                        }
                    }
                })
            }
            else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function atualizarContainer() {
            var dtStatus = document.getElementById("dtStatus").value;
            var dsStatus = document.getElementById("MainContent_dsStatus").value;
            var dtStatusCompra = document.getElementById("dtStatusCompra").value;
            var dsStatusCompra = document.getElementById("MainContent_dsStatusCompra").value;
            var qtDiasFreeTime = document.getElementById("qtDiasFreeTime").value;
            var obsInfoCont = document.getElementById("obsInfoCont").value;
            var qtDiasFreeTimeConfirm = document.getElementById("qtDiasFreeTimeConfirm").value;
            var qtDiasDemurrageCompra = document.getElementById("qtDiasDemurrageCompra").value;
            var qtDiasDemurrageVenda = document.getElementById("qtDiasDemurrageVenda").value;
            if (dsStatus != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/atualizarContainer",
                    data: '{idCont:"' + id + '",dtStatus:"' + dtStatus + '",qtDias:"' + qtDiasFreeTime + '",dsStatus: "' + dsStatus + '" ,dsObs:"' + obsInfoCont + '", qtDiasConfirm:"' + qtDiasFreeTimeConfirm + '", qtDiasDemurrageCompra: "' + qtDiasDemurrageCompra + '", qtDiasDemurrageVenda: "' + qtDiasDemurrageVenda + '", dtStatusCompra: "' + dtStatusCompra + '", dsStatusCompra: "' + dsStatusCompra + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#modalEditContInfo").modal("hide");
                        if (dado != 2) {
                            $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                            consultaFiltrada();
                        }
                        else {

                        }
                    }
                })
            }
            else {
                $("#msgErrEditCont").fadeIn(500).delay(1000).fadeOut(500);
            }
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
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '", tipoCalculo: "' + vlCheck + '" }',
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
                                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE_COMPRA"] + "</td><td class='text-center'>" + dado[i]["COMPRA"] + "</td>" +
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
            document.getElementById("MainContent_ddlStatusCalculoSelecionado").value = 3;
            values = [];
            for (var i = 0; i < pacote.length; i++) {
                values.push(pacote[i].value);
            }

            if (values.length > 0) {
                if (vlCheck == 1) {
                    document.getElementById("divStatusCalculoVenda").style = 'display:block';
                    document.getElementById("divStatusCalculoCompra").style = 'display:none';
                    $("#modalCalucloSelecionados").modal("show");
                    document.getElementById("MainContent_ddlStatusCalculoSelecionado").value = 3;
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
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"];

                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"];

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
                    document.getElementById("divStatusCalculoCompra").style = 'display:block';
                    document.getElementById("divStatusCalculoVenda").style = 'display:none';
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
                                document.getElementById('qtDiasDemu').value = dado[0]['QT_DIAS_DEMURRAGE_COMPRA'];
                                document.getElementById("dtStatusCalculoSelecionado").value = dataAtual = ano + '-' + mes + '-' + dia;
                                document.getElementById('nmTabelaCalculo').value = dado[0]['TABELA'];
                                document.getElementById('nmMoeda').value = dado[0]['MOEDA'];
                                if (dado[0]["FL_ESCALONADA"] != 0) {
                                    $("#vlTaxa").prop('disabled', true);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"];
                                } else {
                                    $("#vlTaxa").prop('disabled', false);
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"];
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
            var idStatus = 0;
            var dtStatus = document.getElementById("dtStatusCalculoSelecionado").value;
            if (dtStatus != "") {
                if (vlCheck == 1) {

                    idStatus = document.getElementById("MainContent_ddlStatusCalculoSelecionado").value;
                    if (idStatus != 0) {
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/calcularDemurrageVenda",
                            data: '{idCont:"' + values[conter] + '",vlTaxa: "' + vlTaxa.toString().replace(",", ".") + '", idStatus: "' + idStatus + '", dtStatus: "' + dtStatus + '" }',
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
                        $("#msgErrDadoCalc").fadeIn(500).delay(1000).fadeOut(500);
                    }

                }
                else {

                    idStatus = document.getElementById("MainContent_ddlStatusCalculoSelecionadoCompra").value;
                    if (idStatus != 0) {
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/calcularDemurrageCompra",
                            data: '{idCont:"' + values[conter] + '",vlTaxa: "' + vlTaxa.toString().replace(",", ".") + '",transportador: "' + transportador + '" ,idStatus: "' + idStatus + '", dtStatus: "' + dtStatus + '" }',
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
                        $("#msgErrDadoCalc").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            }
            else {
                $("#msgErrDadoCalc").fadeIn(500).delay(1000).fadeOut(500);
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
                                    document.getElementById("vlTaxa").value = dado[0]["vlTaxa"];
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
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '",tipoCalculo: "' + vlCheck + '" }',
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
                                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE_COMPRA"] + "</td><td class='text-center'>" + dado[i]["COMPRA"] + "</td>" +
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
                consultaFiltrada();
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
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '", tipoCalculo: "' + vlCheck + '" }',
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
                        consultaFiltrada();
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
                                data: '{nrProcesso:"' + dado[0]["PROCESSO"] + '", tipoCalculo: "' + vlCheck + '"}',
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
                        consultaFiltrada();
                    }
                })
            }
        }

        function imprimirDadosCalculo() {
            if (id != 0) {
                var tipoCalculo;
                var vlc
                var position = 95;
                var positionbg = 92;
                var lineH = 91.5;
                var lineV = 96.5;
                var total = 0;
                var totalv = 0;
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/imprimirDadosCalc",
                    data: '{id:"' + id + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            var imgData = new Image();
                            imgData.src = 'Content/imagens/FCA-LOG(deitado).png';
                            var bg = new Image();
                            bg.src = 'Content/imagens/bg.png';
                            var doc = new jsPDF();
                            doc.setFontSize(8);
                            doc.addImage(imgData, 'png', 10, 0, 90, 30);

                            doc.setFontStyle("bold");
                            doc.text("Razão Social: ", 4, 40);

                            doc.setFontStyle("normal");
                            doc.addImage(bg, 'png', 24, 37, 182, 4);
                            doc.text(dado[0]["NM_RAZAO"], 25, 40);


                            doc.setFontStyle("bold");
                            doc.text("Processo: ", 4, 50);
                            doc.text("Ref. Cliente: ", 4, 55);
                            doc.text("Shipper: ", 4, 60);
                            doc.text("Master: ", 4, 65);
                            doc.text("House: ", 4, 70);
                            doc.text("Navio: ", 4, 75);

                            doc.setFontStyle("normal");
                            doc.addImage(bg, 'png', 24, 47, 78, 4);
                            doc.text(dado[0]["NR_PROCESSO"], 25, 50);
                            doc.addImage(bg, 'png', 24, 52, 78, 4);
                            doc.addImage(bg, 'png', 24, 57, 78, 4);
                            doc.text(dado[0]["TRANSPORTADOR"], 25, 60);
                            doc.addImage(bg, 'png', 24, 62, 78, 4);
                            doc.text(dado[0]["MASTER"], 25, 65);
                            doc.addImage(bg, 'png', 24, 67, 78, 4);
                            doc.text(dado[0]["HOUSE"], 25, 70);
                            doc.addImage(bg, 'png', 24, 72, 78, 4);
                            doc.text(dado[0]["NAVIO"], 25, 75);

                            doc.setFontStyle("bold");
                            doc.text("Serviço: ", 106, 50);
                            doc.text("Origem: ", 106, 55);
                            doc.text("Destino: ", 106, 60);
                            doc.text("Peso(TON): ", 106, 65);
                            doc.text("Volumes: ", 106, 70);
                            doc.text("Cubagem(M3): ", 106, 75);

                            doc.setFontStyle("normal");
                            doc.addImage(bg, 'png', 128, 47, 78, 4);
                            doc.text(dado[0]["NM_SERVICO"], 129, 50);
                            doc.addImage(bg, 'png', 128, 52, 78, 4);
                            doc.text(dado[0]["ORIGEM"] + " - " + dado[0]["DT_EMBARQUE"], 129, 55);
                            doc.addImage(bg, 'png', 128, 57, 78, 4);
                            doc.text(dado[0]["DESTINO"] + " - " + dado[0]["DT_CHEGADA"], 129, 60);
                            doc.addImage(bg, 'png', 128, 62, 78, 4);
                            doc.text(dado[0]["VL_PESO_BRUTO"], 129, 65);
                            doc.addImage(bg, 'png', 128, 67, 78, 4);
                            doc.text(dado[0]["VL_INDICE_VOLUMETRICO"], 129, 70);
                            doc.addImage(bg, 'png', 128, 72, 78, 4);
                            doc.text(dado[0]["VL_M3"], 129, 75);

                            doc.setFontStyle("bold");
                            doc.text("CONTEINER", 7, 90);
                            doc.text("TIPO", 29, 90);
                            doc.text("FREETIME", 53, 85);
                            doc.text("INICIO", 42, 90);
                            doc.text("FINAL", 56, 90);
                            doc.text("DIAS", 69, 90);
                            doc.text("DEMURRAGE", 93, 85);
                            doc.text("INICIO", 79, 90);
                            doc.text("FINAL", 93, 90);
                            doc.text("DIAS C", 106, 90);
                            doc.text("DIAS V", 119, 90);
                            doc.text("MOEDA", 132, 90);
                            doc.text("DIÁRIA", 148, 85);
                            doc.text("COMPRA", 147, 90);
                            doc.text("TOTAL", 163, 85);
                            doc.text("COMPRA", 163, 90);
                            doc.text("DIÁRIA", 178, 85);
                            doc.text("VENDA", 178, 90);
                            doc.text("TOTAL", 193, 85);
                            doc.text("VENDA", 193, 90);
                            doc.setFontStyle("normal");

                            doc.setLineWidth(0.2);
                            doc.line(5, 91.5, 205.5, 91.5);


                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/listarContainerCalculoPrint",
                                data: '{idprocess:"' + id + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        for (let i = 0; i < dado.length; i++) {
                                            lineH = lineH + 5;
                                            doc.line(5, lineH, 205.5, lineH);
                                            doc.addImage(bg, 'png', 5, positionbg, 200, 4);
                                            doc.text(dado[i]["NR_CNTR"], 6, position);
                                            doc.text(dado[i]["NM_TIPO_CONTAINER"], 27, position);
                                            doc.text(dado[i]["INICIALFT"], 40, position);
                                            doc.text(dado[i]["FINALFT"], 55, position);
                                            doc.text(dado[i]["QT_DIAS_FREETIME"].toString(), 70, position);
                                            doc.text(dado[i]["INICIALDEM"].substr(0, 8), 78, position);
                                            doc.text(dado[i]["FINALDEM"], 92, position);
                                            doc.text(dado[i]["QT_DIAS_DEMURRAGE_COMPRA"].toString(), 110, position);
                                            doc.text(dado[i]["QT_DIAS_DEMURRAGE"].toString(), 122, position);
                                            doc.text(dado[i]["SIGLA_MOEDA"], 134, position);
                                            doc.text(dado[i]["VL_TAXA_DEMURRAGE_COMPRA"], 148, position);
                                            doc.text(dado[i]["VL_DEMURRAGE_COMPRA"], 163, position);
                                            doc.text(dado[i]["VL_TAXA_DEMURRAGE_VENDA"], 178, position);
                                            doc.text(dado[i]["VL_DEMURRAGE_VENDA"], 192, position);
                                            position = position + 5;
                                            positionbg = positionbg + 5;
                                            lineV = lineV + 5;
                                            total = total + parseFloat(dado[i]["VL_DEMURRAGE_COMPRA"].toString().replace(".", ""));
                                            totalv = totalv + parseFloat(dado[i]["VL_DEMURRAGE_VENDA"].toString().replace(".", ""));
                                        }
                                        doc.line(5, 91.5, 5, lineV - 5);
                                        doc.line(26, 91.5, 26, lineV - 5);
                                        doc.line(38.5, 91.5, 38.5, lineV - 5);
                                        doc.line(52.5, 91.5, 52.5, lineV - 5);
                                        doc.line(67.5, 91.5, 67.5, lineV - 5);
                                        doc.line(76.5, 91.5, 76.5, lineV - 5);
                                        doc.line(90.5, 91.5, 90.5, lineV - 5);
                                        doc.line(104.5, 91.5, 104.5, lineV - 5);
                                        doc.line(117.5, 91.5, 117.5, lineV - 5);
                                        doc.line(129.5, 91.5, 129.5, lineV - 5);
                                        doc.line(144.5, 91.5, 144.5, lineV - 5);
                                        doc.line(160.5, 91.5, 160.5, lineV - 5);
                                        doc.line(176.5, 91.5, 176.5, lineV - 5);
                                        doc.line(190.5, 91.5, 190.5, lineV - 5);
                                        doc.line(205.5, 91.5, 205.5, lineV - 5);
                                        doc.setFontStyle("bold");
                                        doc.text("TOTAL COMPRA: ", 162, position + 10);
                                        doc.setFontStyle("normal");
                                        doc.text(total.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 188, position + 10);
                                        doc.setFontStyle("bold");
                                        doc.text("TOTAL VENDA: ", 162, position + 15);
                                        doc.setFontStyle("normal");
                                        doc.text(totalv.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 188, position + 15);
                                        doc.output("dataurlnewwindow")
                                    }
                                }
                            })
                        }
                        else {
                        }
                    }
                })
            }
            else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
        }


        function listarFatura() {
            $("#modalFaturas").modal('show');
            idFatura = 0;
            var filtroFatura = document.getElementById("MainContent_ddlFaturaFiltro").value;
            var txtFiltro = document.getElementById("txtConsultaFatura").value;
            var chkA = document.getElementById("MainContent_chkFaturaA");
            var chkF = document.getElementById("MainContent_chkFaturaF");
            chkA.checked = true;
            chkF.checked = false;
            var chkAvalue;
            var chkFvalue;
            if (chkA.checked) {
                chkAvalue = "1";
            }
            else {
                chkAvalue = "0";
            }

            if (chkF.checked) {
                chkFvalue = "1";
            }
            else {
                chkFvalue = "0";
            }
            if (checkV.checked) {
                vlCheck = checkV.value;
                document.getElementById("modalFaturaTitle").textContent = "Fatura Venda";
            }
            else {
                vlCheck = checkC.value;
                document.getElementById("modalFaturaTitle").textContent = "Fatura Compra";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarFaturas",
                data: '{check:"' + vlCheck + '", filtroFatura: "' + filtroFatura + '", txtFiltro: "' + txtFiltro + '", Ativo: "' + chkAvalue + '",Finalizado: "' + chkFvalue + '"}',
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
                            $("#grdFaturaBody").append("<tr data-id='" + dado[i]["ID_DEMURRAGE_FATURA"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setIdFatura(" + dado[i]["ID_DEMURRAGE_FATURA"] + "," + dado[i]["FALTA_ATUALIZACAO_CAMBIAL"] + ")'>Selecionar</div></td>" +
                                "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_FATURA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center' style='max-width: 14ch;' title='" + dado[i]["NM_CLIENTE"] + "'>" + dado[i]["NM_CLIENTE"] + "</td>" +
                                "<td class='text-center' style='max-width: 20ch;' title='" + dado[i]["NM_TRANSPORTADOR"] + "'>" + dado[i]["NM_TRANSPORTADOR"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO"] + "</td><td class='text-center'>" + dado[i]["DT_CANCELAMENTO"] + "</td><td class='text-center'>" + dado[i]["QT_PARCELAS"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdFaturaBody").empty();
                        $("#grdFaturaBody").append("<tr id='msgEmptyWeek'><td colspan='8' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function consultaFatura() {
            idFatura = 0;
            var filtroFatura = document.getElementById("MainContent_ddlFaturaFiltro").value;
            var txtFiltro = document.getElementById("txtConsultaFatura").value;
            var chkA = document.getElementById("MainContent_chkFaturaA");
            var chkF = document.getElementById("MainContent_chkFaturaF");
            var chkAvalue;
            var chkFvalue;
            if (chkA.checked) {
                chkAvalue = "1";
            }
            else {
                chkAvalue = "0";
            }

            if (chkF.checked) {
                chkFvalue = "1";
            }
            else {
                chkFvalue = "0";
            }
            if (checkV.checked) {
                vlCheck = checkV.value;
                document.getElementById("modalFaturaTitle").textContent = "Fatura Venda";
            }
            else {
                vlCheck = checkC.value;
                document.getElementById("modalFaturaTitle").textContent = "Fatura Compra";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarFaturas",
                data: '{check:"' + vlCheck + '", filtroFatura: "' + filtroFatura + '", txtFiltro: "' + txtFiltro + '", Ativo: "' + chkAvalue + '",Finalizado: "' + chkFvalue + '"}',
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
                            $("#grdFaturaBody").append("<tr data-id='" + dado[i]["ID_DEMURRAGE_FATURA"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setIdFatura(" + dado[i]["ID_DEMURRAGE_FATURA"] + "," + dado[i]["FALTA_ATUALIZACAO_CAMBIAL"] + ")'>Selecionar</div></td>" +
                                "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_FATURA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center' style='max-width: 14ch;' title='" + dado[i]["NM_CLIENTE"] + "'>" + dado[i]["NM_CLIENTE"] + "</td>" +
                                "<td class='text-center' style='max-width: 20ch;' title='" + dado[i]["NM_TRANSPORTADOR"] + "'>" + dado[i]["NM_TRANSPORTADOR"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO"] + "</td><td class='text-center'>" + dado[i]["DT_CANCELAMENTO"] + "</td><td class='text-center'>" + dado[i]["QT_PARCELAS"] + "</td></tr>");
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
            console.log(contFatura.length);
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
                        var dado = dado.d;
                        for (var x = 0; x < faturaV.length; x++) {
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/processarFaturaItens",
                                data: '{idcntr:"' + faturaV[x] + '",check: "' + vlCheck + '",processo: "' + processoFatura + '", fatura: "' + dado + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    consultaFiltrada();
                                    listarFatura();
                                }
                            })
                        }
                        $("#modalNovaFatura").modal('hide');
                        $("#msgSuccessProcess").fadeIn(500).delay(1000).fadeOut(500);
                    }
                })
            }
            else {
                $("#msgSelectErrFaturaProcessar").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function imprimirFatura() {
            if (idFatura != 0) {
                var tipoCalculo;
                var lineH = 111.5;
                var lineV = 116.5;
                var positionV = 115;
                var positionbgV = 112;
                var positionC = 95;
                var positionbgC = 92;
                var total = 0.00;
                var totalv = 0.00;
                var desconto = 0.00;
                var totalliquido = 0.00;
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/imprimirDadosFatura",
                    data: '{idFatura:"' + idFatura + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            var imgData = new Image();
                            imgData.src = 'Content/imagens/FCA-LOG(deitado).png';
                            var bg = new Image();
                            bg.src = 'Content/imagens/bg.png';
                            var doc = new jsPDF();
                            doc.setFontSize(8);
                            doc.addImage(imgData, 'png', 10, 0, 90, 30);
                            var nrprocesso = dado[0]["NR_PROCESSO"];
                            if (checkV.checked) {
                                doc.setFontStyle("bold");
                                doc.text("FATURA DEMURRAGE", 150, 7);
                                doc.addImage(bg, 'png', 130, 8, 70, 4);
                                doc.setFontStyle("normal");
                                doc.text(dado[0]["NR_PROCESSO"], 158, 11);
                                doc.text("ESTE DOCUMENTO NÃO TEM VALOR FISCAL, SERVE", 130, 15);
                                doc.text("SOMENTE COMO PRESTAÇÃO DE CONTAS.", 130, 18);
                                doc.setFontStyle("bold");
                                doc.text("EMISSÃO", 140, 21);
                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 130, 22, 70, 4);
                                doc.text(dado[0]["DT_LANCAMENTO"], 141, 25);
                                doc.setFontStyle("bold");
                                doc.text("VENCIMENTO", 175, 21);
                                doc.setFontStyle("normal");
                                doc.text(dado[0]["DT_VENCIMENTO"], 178, 25);


                                doc.setFontStyle("bold");
                                doc.text("Razão Social: ", 4, 40);
                                doc.text("Endereço: ", 4, 45);
                                doc.text("Município: ", 4, 50);
                                doc.text("Bairro: ", 60, 50);
                                doc.text("UF: ", 113, 50);
                                doc.text("CEP: ", 158, 50);
                                doc.text("CNPJ: ", 4, 55);
                                doc.text("INSCR. ESTADUAL: ", 80, 55);

                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 24, 37, 182, 4);
                                doc.text(dado[0]["CLIENTE"], 25, 40);
                                doc.addImage(bg, 'png', 24, 42, 182, 4);
                                doc.text(dado[0]["ENDERECO"] + "-" + dado[0]["NR_ENDERECO"], 25, 45);
                                doc.addImage(bg, 'png', 24, 47, 35, 4);
                                doc.text(dado[0]["NM_CIDADE"], 25, 50);
                                doc.addImage(bg, 'png', 71, 47, 41, 4);
                                doc.text(dado[0]["BAIRRO"], 73, 50);
                                doc.addImage(bg, 'png', 119, 47, 35, 4);
                                doc.text(dado[0]["NM_ESTADO"], 120, 50);
                                doc.addImage(bg, 'png', 167, 47, 39, 4);
                                doc.text(dado[0]["CEP"], 168, 50);
                                doc.addImage(bg, 'png', 24, 52, 50, 4);
                                doc.text(dado[0]["CNPJ"], 25, 55);
                                doc.addImage(bg, 'png', 110, 52, 96, 4);
                                doc.text(dado[0]["INSCR_ESTADUAL"], 111, 55);

                                doc.setFontStyle("bold");
                                doc.text("Processo: ", 4, 70);
                                doc.text("Ref. Cliente: ", 4, 75);
                                doc.text("Shipper: ", 4, 80);
                                doc.text("Master: ", 4, 85);
                                doc.text("House: ", 4, 90);
                                doc.text("Navio: ", 4, 95);

                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 24, 67, 78, 4);
                                doc.text(dado[0]["NR_PROCESSO"], 25, 70);
                                doc.addImage(bg, 'png', 24, 72, 78, 4);
                                doc.addImage(bg, 'png', 24, 77, 78, 4);
                                doc.text(dado[0]["TRANSPORTADOR"], 25, 80);
                                doc.addImage(bg, 'png', 24, 82, 78, 4);
                                doc.text(dado[0]["MASTER"], 25, 85);
                                doc.addImage(bg, 'png', 24, 87, 78, 4);
                                doc.text(dado[0]["HOUSE"], 25, 90);
                                doc.addImage(bg, 'png', 24, 92, 78, 4);
                                doc.text(dado[0]["NAVIO"], 25, 95);

                                doc.setFontStyle("bold");
                                doc.text("Serviço: ", 106, 70);
                                doc.text("Origem: ", 106, 75);
                                doc.text("Destino: ", 106, 80);
                                doc.text("Peso(TON): ", 106, 85);
                                doc.text("Volumes: ", 106, 90);
                                doc.text("Cubagem(M3): ", 106, 95);

                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 128, 67, 78, 4);
                                doc.text(dado[0]["NM_SERVICO"], 129, 70);
                                doc.addImage(bg, 'png', 128, 72, 78, 4);
                                doc.text(dado[0]["ORIGEM"] + " - " + dado[0]["DT_EMBARQUE"], 129, 75);
                                doc.addImage(bg, 'png', 128, 77, 78, 4);
                                doc.text(dado[0]["DESTINO"] + " - " + dado[0]["DT_CHEGADA"], 129, 80);
                                doc.addImage(bg, 'png', 128, 82, 78, 4);
                                doc.text(dado[0]["VL_PESO_BRUTO"].toString().replace(".", ","), 129, 85);
                                doc.addImage(bg, 'png', 128, 87, 78, 4);
                                doc.text(dado[0]["VL_INDICE_VOLUMETRICO"].toString().replace(".", ","), 129, 90);
                                doc.addImage(bg, 'png', 128, 92, 78, 4);
                                doc.text(dado[0]["VL_M3"].toString().replace(".", ","), 129, 95);

                                doc.setFontStyle("bold");
                                doc.text("CONTEINER", 14, 110);
                                doc.text("TIPO", 40, 110);
                                doc.text("FREETIME", 66, 105);
                                doc.text("INICIO", 56, 110);
                                doc.text("FINAL", 70, 110);
                                doc.text("DIAS", 82, 110);
                                doc.text("DEMURRAGE", 100, 105);
                                doc.text("INICIO", 92, 110);
                                doc.text("FINAL", 106, 110);
                                doc.text("DIAS", 118, 110);
                                doc.text("MOEDA", 127, 110);
                                doc.text("DIÁRIA", 141, 110);
                                doc.text("TAXA", 157, 110)
                                doc.text("TOTAL BR", 173, 110);
                                doc.setFontStyle("normal");

                                doc.setLineWidth(0.2);

                                $.ajax({
                                    type: "POST",
                                    url: "DemurrageService.asmx/listarContainerFaturaPrintVenda",
                                    data: '{idFatura:"' + idFatura + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (dado) {
                                        var dado = dado.d;
                                        dado = $.parseJSON(dado);
                                        if (dado != null) {
                                            for (let i = 0; i < dado.length; i++) {
                                                doc.line(11.7, lineH, 187.5, lineH);
                                                doc.addImage(bg, 'png', 12, positionbgV, 22, 4);
                                                doc.text(dado[i]["NR_CNTR"], 13, positionV);
                                                doc.addImage(bg, 'png', 35, positionbgV, 18, 4);
                                                doc.text(dado[i]["NM_TIPO_CONTAINER"], 36, positionV);
                                                doc.addImage(bg, 'png', 54, positionbgV, 13, 4);
                                                doc.text(dado[i]["INICIALFT"], 55, positionV);
                                                doc.addImage(bg, 'png', 68, positionbgV, 13, 4);
                                                doc.text(dado[i]["FINALFT"], 69, positionV);
                                                doc.addImage(bg, 'png', 82, positionbgV, 7, 4);
                                                doc.text(dado[i]["QT_DIAS_FREETIME"].toString(), 84, positionV);
                                                doc.addImage(bg, 'png', 90, positionbgV, 13, 4);
                                                doc.text(dado[i]["INICIALDEM"].substr(0, 8), 91, positionV);
                                                doc.addImage(bg, 'png', 104, positionbgV, 13, 4);
                                                doc.text(dado[i]["FINALDEM"], 105, positionV);
                                                doc.addImage(bg, 'png', 118, positionbgV, 7, 4);
                                                doc.text(dado[i]["QT_DIAS_DEMURRAGE"].toString(), 120, positionV);
                                                doc.addImage(bg, 'png', 126, positionbgV, 12, 4);
                                                doc.text(dado[i]["SIGLA_MOEDA"].toString(), 129, positionV);
                                                doc.addImage(bg, 'png', 139, positionbgV, 14, 4);
                                                doc.text(dado[i]["VL_TAXA_DEMURRAGE_VENDA"].toFixed(2), 141, positionV);
                                                doc.addImage(bg, 'png', 154, positionbgV, 16, 4);
                                                doc.text(dado[i]["VL_CAMBIO_DEMURRAGE_VENDA"].toFixed(5), 156, positionV);
                                                doc.addImage(bg, 'png', 171, positionbgV, 16, 4);
                                                doc.text(dado[i]["VL_DEMURRAGE_VENDA_BR"].toFixed(2), 173, positionV);
                                                positionV = positionV + 5;
                                                positionbgV = positionbgV + 5;
                                                lineV = lineV + 5;
                                                lineH = lineH + 5;
                                                console.log(dado[i]["VL_DEMURRAGE_VENDA_BR"]);
                                                if (dado[i]["VL_DEMURRAGE_VENDA_BR"].toString() != "") {
                                                    totalv = totalv + parseFloat(dado[i]["VL_DEMURRAGE_VENDA_BR"].toString().replace(",", "."));
                                                }
                                                console.log(dado[i]["VL_DESCONTO_DEMURRAGE_VENDA"]);
                                                if (dado[i]["VL_DESCONTO_DEMURRAGE_VENDA"].toString() != "") {
                                                    desconto = desconto + parseFloat(dado[i]["VL_DESCONTO_DEMURRAGE_VENDA"].toString().replace(",", "."));
                                                }
                                                if (dado[i]["VL_DEMURRAGE_LIQUIDO_VENDA"].toString() != "") {
                                                    totalliquido = totalliquido + parseFloat(dado[i]["VL_DEMURRAGE_LIQUIDO_VENDA"].toString().replace(",", "."));
                                                }

                                            }
                                            doc.line(11.7, lineH, 187.5, lineH);
                                            doc.line(11.7, 111.5, 11.7, lineV - 5);
                                            doc.line(34.5, 111.5, 34.5, lineV - 5);
                                            doc.line(53.5, 111.5, 53.5, lineV - 5);
                                            doc.line(67.5, 111.5, 67.5, lineV - 5);
                                            doc.line(81.5, 111.5, 81.5, lineV - 5);
                                            doc.line(89.5, 111.5, 89.5, lineV - 5);
                                            doc.line(103.5, 111.5, 103.5, lineV - 5);
                                            doc.line(117.5, 111.5, 117.5, lineV - 5);
                                            doc.line(125.5, 111.5, 125.5, lineV - 5);
                                            doc.line(138.5, 111.5, 138.5, lineV - 5);
                                            doc.line(153.5, 111.5, 153.5, lineV - 5);
                                            doc.line(170.5, 111.5, 170.5, lineV - 5);
                                            doc.line(187.5, 111.5, 187.5, lineV - 5);
                                            doc.setFontStyle("bold");
                                            doc.text("TOTAL DAS DESPESAS: ", 145, positionV + 15);
                                            doc.setFontStyle("normal");
                                            doc.text(totalv.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 180, positionV + 15);
                                            doc.setFontStyle("bold");
                                            doc.text("ACRÉSCIMOS/DESCONTOS: ", 139, positionV + 20);
                                            doc.setFontStyle("normal");
                                            doc.text(desconto.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 180, positionV + 20);
                                            doc.setFontStyle("bold");
                                            doc.text("TOTAL DA FATURA: ", 150, positionV + 25);
                                            doc.setFontStyle("normal");
                                            doc.text(totalliquido.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 180, positionV + 25);

                                            doc.setFontStyle("bold");
                                            doc.addImage(bg, 'png', 176, positionbgV + 33, 32, 42);
                                            doc.text("AVISO IMPORTANTE: ", 177, positionV + 34);
                                            doc.text("OS FRETES E", 177, positionV + 37);
                                            doc.text("SERVIÇO ", 177, positionV + 40);
                                            doc.text("INTERNACIONAIS: ", 177, positionV + 43);
                                            doc.text("DEVEM SER ", 177, positionV + 46);
                                            doc.text("REGISTRADOS", 177, positionV + 49);
                                            doc.text("NO SISCOSERV CASO", 177, positionV + 52);
                                            doc.text("TENHA SIDO", 177, positionV + 55);
                                            doc.text("EMPRESA", 177, positionV + 58);
                                            doc.text("TRANSPORTADORA", 177, positionV + 61);
                                            doc.text("ESTRANGEIRA NÃO", 177, positionV + 64);
                                            doc.text("DOMICILIADA NO", 177, positionV + 67);
                                            doc.text("BRASIL", 177, positionV + 70);

                                            doc.setFontStyle("normal");
                                            doc.text("Prezado Cliente, ", 4, positionV + 36);
                                            doc.text("O pagamento desta fatura deverá ser realizada até a data de vencimento, após o vencimento poderão ser ", 4, positionV + 39);
                                            doc.text("cancelados quaisquer benefícios de tarifa ou free time diferenciado por ventura existentes.", 4, positionV + 42);
                                            doc.setFontStyle("bold");
                                            doc.text("DADOS BANCÁRIOS PARA PAGAMENTO: ", 4, positionV + 48);
                                            doc.setFontStyle("normal");
                                            doc.text("BANCO (033): SANTANDER - FCA LOG - AG: 3297 - C/C: 13001071-2", 4, positionV + 51);
                                            doc.text("FCA COMERCIO EXTERIOR E LOGISTICA LTDA. ", 4, positionV + 54);
                                            doc.text("CNPJ: 00.639.367/0001-50", 4, positionV + 57);
                                            doc.text("Taxa do dólar valida até a data de vencimento desta fatura, após vencido solicitar nova fatura para:", 4, positionV + 63);
                                            doc.text("financeiro@abainfra.com.br e operacao@fcalog.com", 4, positionV + 66);
                                            doc.text("--------------------------------------------------------------------------------------------------------------------------------------", 45, positionV + 75);
                                            doc.text("RECEBEMOS OS SERVIÇOS CONSTANTES DO RECIBO PROVISÓRIOS DE SERVIÇO", 55, positionV + 78);
                                            doc.addImage(bg, 'png', 4, positionbgV + 78, 22, 10);
                                            doc.text(nrprocesso, 7, positionV + 81);
                                            doc.text("_________________________________ , _____ de _________________ de ___________  ASS _______________________________", 26, positionV + 84);
                                            doc.output("dataurlnewwindow")

                                        } else {
                                            alert("Fatura sem container relacionado!");
                                        }
                                    }
                                })

                            }
                            else {
                                doc.setFontStyle("bold");
                                doc.text("Razão Social: ", 4, 40);
                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 24, 37, 182, 4);
                                doc.text(dado[0]["CLIENTE"], 25, 40);

                                doc.setFontStyle("bold");
                                doc.text("Processo: ", 4, 50);
                                doc.text("Ref. Cliente: ", 4, 55);
                                doc.text("Shipper: ", 4, 60);
                                doc.text("Master: ", 4, 65);
                                doc.text("House: ", 4, 70);
                                doc.text("Navio: ", 4, 75);

                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 24, 47, 78, 4);
                                doc.text(dado[0]["NR_PROCESSO"], 25, 50);
                                doc.addImage(bg, 'png', 24, 52, 78, 4);
                                doc.addImage(bg, 'png', 24, 57, 78, 4);
                                doc.text(dado[0]["TRANSPORTADOR"], 25, 60);
                                doc.addImage(bg, 'png', 24, 62, 78, 4);
                                doc.text(dado[0]["MASTER"], 25, 65);
                                doc.addImage(bg, 'png', 24, 67, 78, 4);
                                doc.text(dado[0]["HOUSE"], 25, 70);
                                doc.addImage(bg, 'png', 24, 72, 78, 4);
                                doc.text(dado[0]["NAVIO"], 25, 75);

                                doc.setFontStyle("bold");
                                doc.text("Serviço: ", 106, 50);
                                doc.text("Origem: ", 106, 55);
                                doc.text("Destino: ", 106, 60);
                                doc.text("Peso(TON): ", 106, 65);
                                doc.text("Volumes: ", 106, 70);
                                doc.text("Cubagem(M3): ", 106, 75);

                                doc.setFontStyle("normal");
                                doc.addImage(bg, 'png', 128, 47, 78, 4);
                                doc.text(dado[0]["NM_SERVICO"], 129, 50);
                                doc.addImage(bg, 'png', 128, 52, 78, 4);
                                doc.text(dado[0]["ORIGEM"] + " - " + dado[0]["DT_EMBARQUE"], 129, 55);
                                doc.addImage(bg, 'png', 128, 57, 78, 4);
                                doc.text(dado[0]["DESTINO"] + " - " + dado[0]["DT_CHEGADA"], 129, 60);
                                doc.addImage(bg, 'png', 128, 62, 78, 4);
                                doc.text(dado[0]["VL_PESO_BRUTO"].toString().replace(".", ","), 129, 65);
                                doc.addImage(bg, 'png', 128, 67, 78, 4);
                                doc.text(dado[0]["VL_INDICE_VOLUMETRICO"].toString().replace(".", ","), 129, 70);
                                doc.addImage(bg, 'png', 128, 72, 78, 4);
                                doc.text(dado[0]["VL_M3"].toString().replace(".", ","), 129, 75);

                                doc.setFontStyle("bold");
                                doc.text("CONTEINER", 14, 90);
                                doc.text("TIPO", 40, 90);
                                doc.text("FREETIME", 66, 85);
                                doc.text("INICIO", 56, 90);
                                doc.text("FINAL", 70, 90);
                                doc.text("DIAS", 82, 90);
                                doc.text("DEMURRAGE", 100, 85);
                                doc.text("INICIO", 92, 90);
                                doc.text("FINAL", 106, 90);
                                doc.text("DIAS", 118, 90);
                                doc.text("MOEDA", 127, 90);
                                doc.text("DIÁRIA", 141, 90);
                                doc.text("TAXA", 157, 90)
                                doc.text("TOTAL BR", 173, 90);
                                doc.setFontStyle("normal");

                                $.ajax({
                                    type: "POST",
                                    url: "DemurrageService.asmx/listarContainerFaturaPrintCompra",
                                    data: '{idFatura:"' + idFatura + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (dado) {
                                        var dado = dado.d;
                                        dado = $.parseJSON(dado);
                                        console.log(dado);
                                        if (dado != null) {
                                            for (let i = 0; i < dado.length; i++) {
                                                doc.addImage(bg, 'png', 12, positionbgC, 22, 4);
                                                doc.text(dado[i]["NR_CNTR"], 13, positionC);
                                                doc.addImage(bg, 'png', 35, positionbgC, 18, 4);
                                                doc.text(dado[i]["NM_TIPO_CONTAINER"], 36, positionC);
                                                doc.addImage(bg, 'png', 54, positionbgC, 13, 4);
                                                doc.text(dado[i]["INICIALFT"], 55, positionC);
                                                doc.addImage(bg, 'png', 68, positionbgC, 13, 4);
                                                doc.text(dado[i]["FINALFT"], 69, positionC);
                                                doc.addImage(bg, 'png', 82, positionbgC, 7, 4);
                                                doc.text(dado[i]["QT_DIAS_FREETIME"].toString(), 84, positionC);
                                                doc.addImage(bg, 'png', 90, positionbgC, 13, 4);
                                                doc.text(dado[i]["INICIALDEM"].substr(0, 8), 91, positionC);
                                                doc.addImage(bg, 'png', 104, positionbgC, 13, 4);
                                                doc.text(dado[i]["FINALDEM"], 105, positionC);
                                                doc.addImage(bg, 'png', 118, positionbgC, 7, 4);
                                                doc.text(dado[i]["QT_DIAS_DEMURRAGE_COMPRA"].toString(), 120, positionC);
                                                doc.addImage(bg, 'png', 126, positionbgC, 12, 4);
                                                doc.text(dado[i]["SIGLA_MOEDA"].toString(), 129, positionC);
                                                doc.addImage(bg, 'png', 139, positionbgC, 14, 4);
                                                doc.text(dado[i]["VL_TAXA_DEMURRAGE_COMPRA"].toFixed(2), 141, positionC);
                                                doc.addImage(bg, 'png', 154, positionbgC, 16, 4);
                                                doc.text(dado[i]["VL_CAMBIO_DEMURRAGE_COMPRA"].toFixed(5), 156, positionC);
                                                doc.addImage(bg, 'png', 171, positionbgC, 16, 4);
                                                doc.text(dado[i]["VL_DEMURRAGE_COMPRA_BR"].toFixed(2), 173, positionC);
                                                positionC = positionC + 5;
                                                positionbgC = positionbgC + 5;

                                                if (dado[i]["VL_DEMURRAGE_COMPRA_BR"].toString() != "") {
                                                    total = total + parseFloat(dado[i]["VL_DEMURRAGE_COMPRA_BR"].toString().replace(",", "."));
                                                }
                                                if (dado[i]["VL_DESCONTO_DEMURRAGE_COMPRA"].toString() != "") {
                                                    desconto = desconto + parseFloat(dado[i]["VL_DESCONTO_DEMURRAGE_COMPRA"].toString().replace(",", "."));
                                                }
                                                if (dado[i]["VL_DEMURRAGE_LIQUIDO_COMPRA"].toString() != "") {

                                                    totalliquido = totalliquido + parseFloat(dado[i]["VL_DEMURRAGE_LIQUIDO_COMPRA"].toString().replace(",", "."));
                                                }
                                            }
                                            doc.setFontStyle("bold");
                                            doc.text("TOTAL DAS DESPESAS: ", 155, positionC + 10);
                                            doc.setFontStyle("normal");
                                            doc.text(total.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 190, positionC + 10);
                                            doc.setFontStyle("bold");
                                            doc.text("ACRÉSCIMOS/DESCONTOS: ", 149, positionC + 15);
                                            doc.setFontStyle("normal");
                                            doc.text(desconto.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 190, positionC + 15);
                                            doc.setFontStyle("bold");
                                            doc.text("TOTAL DA FATURA: ", 160, positionC + 20);
                                            doc.setFontStyle("normal");
                                            doc.text(totalliquido.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), 190, positionC + 20);

                                            doc.output("dataurlnewwindow")
                                        } else {
                                            alert("Fatura sem container relacionado!");
                                        }
                                    }
                                })

                            }
                        }
                        else {
                        }
                    }
                })
            }
            else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
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
            } else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function confirmExcluirFatura() {
            if (idFatura != 0) {
                $("#modalExcluirFatura").modal('show');
            } else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function excluirFatura() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/excluirFatura",
                data: '{idFatura:"' + idFatura + '", check:"' + vlCheck + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "1") {
                        $("#msgDeleteSucess").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        $("#modalExcluirFatura").modal('hide');
                        consultaFiltrada();
                    } else {
                        $("#msgDeleteErr").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        $("#modalExcluirFatura").modal('hide');
                        consultaFiltrada();
                    }
                },
                error: function (err) {
                    $("#msgDeleteErr").fadeIn(500).delay(1000).fadeOut(500);
                    listarFatura();
                    $("#modalExcluirFatura").modal('hide');
                    consultaFiltrada();
                }

            })

        }

        function cancelarFatura() {
            motivoCancelamento = document.getElementById("dsMotivoCancelamento").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/cancelarFatura",
                data: '{idFatura:"' + idFatura + '", motivoCancelamento: "' + motivoCancelamento + '", check:"' + vlCheck + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#msgSuccessCancelar").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        consultaFiltrada();
                    }
                    else {
                        $("#msgErrCancelar").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        consultaFiltrada();
                    }
                }
            })
            $("#modalCancelar").modal("hide");
        }

        function listarAtualizacaoCambial() {
            if (idFatura != 0) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoAtualizacaoCambial",
                    data: '{idFatura: "' + idFatura + '",check: "' + vlCheck + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        if (dado != "1") {
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
                                        document.getElementById("vlCambioAtualizacao").value = dado[0]["VL_TAXA"];
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
                                                        $("#grdAtualizacaoCambialBody").append("<tr id='r" + dado[i]["ID_CNTR_BL"] + "s' data-id='" + dado[i]["ID_CNTR_BL"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setIdFaturaItens(" + dado[i]["ID_CNTR_BL"] + ")'>Selecionar</div></td>" +
                                                            "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_MOEDA"] + "</td><td class='text-center vlDemurrage'>" + dado[i]["VL_DEMURRAGE"] + "</td>" +
                                                            "<td class='text-center desconto'>" + dado[i]["DESCONTO"] + "</td>" +
                                                            "<td class='text-center multa'>" + dado[i]["MULTA"] + "</td></tr > ");
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
                        } else {
                            $("#msgExportErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })

            } else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function ParcelarDemurrage() {
            if (idFatura != 0) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoExportFatura",
                    data: '{idFatura:"' + idFatura + '", check: "' + vlCheck + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        if (dado == "0") {
                            if (atualizaCambio != 1) {
                                $("#modalParcelamentoDemurrage").modal("show");
                                listarParcelas();
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
                                            document.getElementById("idFaturaParcelamentoDemurrage").value = dado[0]["ID_DEMURRAGE_FATURA"];
                                            document.getElementById("nrProcessoFaturaParcelamentoDemurrage").value = dado[0]["NR_PROCESSO"];
                                            document.getElementById("nmClienteFaturaParcelamentoDemurrage").value = dado[0]["CLIENTE"];
                                            document.getElementById("vlRecebidoParcelamentoDemurrage").value = dado[0]["VL_DEMURRAGE_TOTAL_BR"];
                                            document.getElementById("vlParcelamentoDemurrage").value = document.getElementById("vlRecebidoParcelamentoDemurrage").value / document.getElementById("qtNumeroParcelas").value;
                                        } else {
                                            $("#msgExportErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                                        }
                                    }
                                })
                            } else {
                                $("#msgCambioErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                            }
                        } else if (dado == "1") {
                            $("#msgCambioErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        } else if (dado == "2") {
                            $("#msgExportErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        } else {
                            $("#msgDemuFinalErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            } else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        $("#qtNumeroParcelas").change(function () {
            
            var vlTotalDem = document.getElementById("vlRecebidoParcelamentoDemurrage").value;
            var qtParcelas = document.getElementById("qtNumeroParcelas").value;

            if (qtParcelas == "") {
                qtParcelas = 1;
            }

            document.getElementById("vlParcelamentoDemurrage").value = (vlTotalDem / qtParcelas).toFixed(2);
        })

        $("#vlRecebidoParcelamentoDemurrage").change(function () {
            var vlTotalDem = document.getElementById("vlRecebidoParcelamentoDemurrage").value;
            var qtParcelas = document.getElementById("qtNumeroParcelas").value;

            document.getElementById("vlParcelamentoDemurrage").value = (vlTotalDem / qtParcelas).toFixed(2);
        })

        function ParcelamentoDemurrage() {
            var vlParcelamentoDemurrage = document.getElementById("vlParcelamentoDemurrage").value;
            var vlRecebidoParcelamentoDemurrage = document.getElementById("vlRecebidoParcelamentoDemurrage").value;
            var qtNumeroParcelas = document.getElementById("qtNumeroParcelas").value;
            var dtInicialParcelamentoDemurrage = document.getElementById("dtInicialParcelamentoDemurrage").value;
            console.log(dtInicialParcelamentoDemurrage)
            if (dtInicialParcelamentoDemurrage == "") {
                return $("#msgDeleteErr").fadeIn(500).delay(1000).fadeOut(500);;
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/parcelarDemurrage",
                data: '{idfatura: "' + idFatura + '", vlDemurrageParcela:"' + vlParcelamentoDemurrage + '", vlDemurrageTotal: "' + vlRecebidoParcelamentoDemurrage + '", qtDemurrageParcela: "' + qtNumeroParcelas + '", dtPeriodoInicial: "' + dtInicialParcelamentoDemurrage + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {

                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "OK") {
                        listarParcelas();
                    } else {
                        listarParcelas();
                    }
                },
                error: function (err) {
                    $("#msgDeleteErr").fadeIn(500).delay(1000).fadeOut(500);
                    listarFatura();
                    $("#modalExcluirFatura").modal('hide');
                    consultaFiltrada();
                }

            })
        }

        function listarParcelas() {
            console.log("Fatura " + idFatura)
            var vlParcelaJuros = 0;
            var result;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarParcelamentoDemurrage",
                data: '{fatura: "' + idFatura + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdParcelamentoDemurrageBody").empty();
                    $("#grdParcelamentoDemurrageBody").append("<tr><td colspan='6'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdParcelamentoDemurrageBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            vlParcelaJuros = dado[i]["VL_DEMURRAGE_PARCELA"] + (dado[i]["VL_DEMURRAGE_PARCELA"] * (dado[i]["VL_DEMURRAGE_PARCELA_JUROS"] / 100));

                            result = "<tr>";
                            result += dado[i]["ID_CONTA_PAGAR_RECEBER"] == null ? "<td class='text-center'><div class='btn btn-primary' onclick='BuscarParcela(" + dado[i]["ID_DEMURRAGE_FATURA_PARCELAS"] + ")'><i class='fas fa-edit'></i></div>" :"<td class='text-center'>";
                            result += "<div class='btn btn-primary' style='margin-left: 5px;' onclick='DeletarParcelaModal(" + dado[i]["ID_DEMURRAGE_FATURA_PARCELAS"] + ")'><i class='fas fa-trash'></i></div></td>";
                            result += "<td class='text-center' > " + parseFloat(i + 1) + "</td >";
                            result += "<td class='text-center'>" + dado[i]["DT_VENCIMENTO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_PARCELA"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>";
                            result += "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_PARCELA_JUROS"] + "</td>";
                            result += "<td class='text-center'>" + vlParcelaJuros.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>";
                            result += "<td class='text-center'>" + dado[i]["FL_PAGO"] + "</td>";
                            result += dado[i]["ID_CONTA_PAGAR_RECEBER"] == null ? "<td class='text-center'><div class='btn btn-primary' onclick='RegistrarTaxa(" + dado[i]["ID_DEMURRAGE_FATURA_PARCELAS"] + ")'>Registrar Taxa</div></td>" : dado[i]["ID_CONTA_PAGAR_RECEBER"] !== null && dado[i]["FL_PAGO"].toString() == 'PENDENTE' ? "<td class='text-center'><div class='btn btn-primary' onclick='ExportarParcelaCC(" + dado[i]["ID_DEMURRAGE_FATURA_PARCELAS"] + ")'>Exportar CC</div></td>" : "<td class='text-center'><div class='btn btn-primary' onclick='CancelarExportarParcelaCC(" + dado[i]["ID_DEMURRAGE_FATURA_PARCELAS"] + ")'>Cancelar Exportação</div></td>";
                            result += dado[i]["ID_CONTA_PAGAR_RECEBER"] == null ? "<td></td>" : dado[i]["DT_ENVIO_FATURAMENTO"] == null ? "<td class='text-center'><div class='btn btn-primary' onclick='EnviarFaturamento(" + dado[i]["ID_DEMURRAGE_FATURA_PARCELAS"] + ")'>Enviar Faturamento</div></td>" : "<td class='text-center'>Faturamento - " + dado[i]["DT_ENVIO_FATURAMENTO"] + "</td>";
                            result += "</tr>";
                            $("#grdParcelamentoDemurrageBody").append(result);
                        }
                    } else {
                        $("#grdParcelamentoDemurrageBody").append("<tr><td class='text-center' colspan='6'>Não há parcelas</td></tr>");
                    }
                },
                error: function (err) {
                    
                }

            })
        }

        function BuscarParcela(idParcela) {
            var btn = document.getElementById("btnVincular");
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/infoParcela",
                data: '{idParcela: "' + idParcela + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#modalInfoParcela").modal("show");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        vlParcelaJuros = dado[0]["VL_DEMURRAGE_PARCELA"] + (dado[0]["VL_DEMURRAGE_PARCELA"] * (dado[0]["VL_DEMURRAGE_PARCELA_JUROS"] / 100))
                        document.getElementById("vlParcelaEdit").value = dado[0]["VL_DEMURRAGE_PARCELA"];
                        document.getElementById("vlParcelaJurosEdit").value = vlParcelaJuros;
                        document.getElementById("vlJurosEdit").value = dado[0]["VL_DEMURRAGE_PARCELA_JUROS"] != null ? dado[0]["VL_DEMURRAGE_PARCELA_JUROS"] : 0;
                        document.getElementById("dtVencimentoEdit").value = dado[0]["DT_VENCIMENTO_DEMURRAGE_PARCELA"];
                        document.getElementById("checkPago").checked = dado[0]["FL_PAGO"];
                        document.getElementById("MainContent_idParcela").value = dado[0]["ID_DEMURRAGE_FATURA_PARCELAS"];
                        console.log(dado[0]["DT_VENCIMENTO_DEMURRAGE_PARCELA"]);
                    } else {
                        
                    }
                },
                error: function (err) {

                }

            })
        }

        $("#vlJurosEdit").change(function () {
            var parcelaJuros = document.getElementById("vlParcelaJurosEdit");
            var vlParcela = document.getElementById("vlParcelaEdit");
            var vlJuros = document.getElementById("vlJurosEdit");

            parcelaJuros.value = (parseFloat(vlParcela.value) + (parseFloat(vlParcela.value) * (parseFloat(vlJuros.value) / 100))).toFixed(2);
        })

        function editarParcela() {
            var vlParcela = document.getElementById("vlParcelaEdit").value;
            var vlParcelaJuros = document.getElementById("vlParcelaJurosEdit").value;
            var juros = document.getElementById("vlJurosEdit").value;
            var vencimento = document.getElementById("dtVencimentoEdit").value;
            var pago = document.getElementById("checkPago").checked ? 1 : 0;
            var idParcela = document.getElementById("MainContent_idParcela").value
            
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/editarParcela",
                data: '{idParcela: "' + idParcela + '", vlParcela: "' + vlParcela + '", vlJuros: "' + juros + '", vlParcelaJuros: "' + vlParcelaJuros + '", vencimento: "' + vencimento+'", pago: "'+pago+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "Success") {
                        $("#modalInfoParcela").modal("hide");
                        $("#msgSuccessParcela").fadeIn(500).delay(1000).fadeOut(500);
                        listarParcelas();
                    } else {
                        $("#modalInfoParcela").modal("hide");
                        $("#msgErrParcela").fadeIn(500).delay(1000).fadeOut(500);
                        listarParcelas();
                    }
                },
                error: function (err) {

                }

            })
        }

        function DeletarParcelaModal(idParcelaDelete) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, deletar!',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/deletarParcela",
                        data: '{idParcela:"' + idParcelaDelete + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado == "1") {
                                $("#modalDeleteParcela").modal("hide");
                                $("#modalParcelamentoDemurrage").modal("hide");
                                $("#msgExportSuccess").fadeIn(500).delay(1000).fadeOut(500);
                                listarParcelas();
                            }
                            else {
                                $("#modalDeleteParcela").modal("hide");
                                $("#modalParcelamentoDemurrage").modal("hide");
                                $("#msgErrExportDadoConta").fadeIn(500).delay(1000).fadeOut(500);
                                listarParcelas();
                            }
                        }
                    })
                }
            })
        }

        function atualizacaoCambial() {
            if (idFatura != 0) {
                var idFaturaAtualizacaoCambial = document.getElementById("idFaturaAtualizacaoCambial").value;
                var dtVencimento = document.getElementById("dtVencimentoAtualizacaoCambial").value;
                var dtCambio = document.getElementById("dtCambioAtualizacao").value;
                var vlCambio = document.getElementById("vlCambioAtualizacao").value;
                var idContaBancaria = document.getElementById("MainContent_ddlContaBancaria").value;
                var linhas = document.querySelectorAll(".desconto");
                var linhas2 = document.querySelectorAll(".vlDemurrage");
                var multa = document.querySelectorAll(".multa");
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
                            var vlDemurrage = linhas2[ind].textContent.toString();
                            var descontoBRL = linhas[ind].textContent.toString();
                            var multaBRL = multa[ind].textContent.toString();
                            $.ajax({
                                type: "POST",
                                url: "DemurrageService.asmx/atualizacaoCambialContainer",
                                data: '{idCntr:"' + desc[ind].attributes[1].value + '",dtCambio: "' + dtCambio + '", vlCambio: "' + vcambio + '",vlDemurrage:"' + vlDemurrage + '" ,descontoBRL: "' + descontoBRL + '", multaBRL: "' + multaBRL+'", check:"' + vlCheck + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () {
                                    $("#modalAtualizacaoCambial").modal("hide");
                                    $("#msgUpdtSuccess").fadeIn(500).delay(1000).fadeOut(500);
                                    listarFatura();
                                }
                            })
                        }
                    }
                })
            } else {
                $("#msgSelectErrFaturaItens").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function aplicarDesconto() {
            if (idFaturaItens != "") {
                var desc = document.querySelector("#r" + idFaturaItens + "s td:nth-child(5)");
                desc.textContent = document.getElementById("vlDescontoBr").value;
            }
            else {
                $("#msgSelectErrFaturaItens").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function aplicarMulta() {
            if (idFaturaItens != "") {
                var desc2 = document.querySelector("#r" + idFaturaItens + "s td:nth-child(6)");
                desc2.textContent = document.getElementById("vlMultaBr").value;
            }
            else {
                $("#msgSelectErrFaturaItens").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function exportarCC() {
            if (idFatura != 0) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoExportFatura",
                    data: '{idFatura:"' + idFatura + '", check: "' + vlCheck + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        if (dado == "0") {
                            if (atualizaCambio != 1) {
                                $("#modalExportarContaCorrente").modal("show");
                                $.ajax({
                                    type: "POST",
                                    url: "DemurrageService.asmx/infoExportCC",
                                    data: '{idFatura:"' + idFatura + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (dado) {
                                        var dado = dado.d;
                                        dado = $.parseJSON(dado);
                                        if (dado != null) {
                                            document.getElementById("idFaturaContaCorrente").value = dado[0]["ID_DEMURRAGE_FATURA"];
                                            document.getElementById("nrProcessoFaturaContaCorrente").value = dado[0]["NR_PROCESSO"];
                                            document.getElementById("nmClienteFaturaContaCorrente").value = dado[0]["CLIENTE"];
                                            document.getElementById("dtLiquidacaoFaturaContaCorrente").value = dataAtual = ano + '-' + mes + '-' + dia;
                                            document.getElementById("MainContent_ddlStatusFaturaContaCorrente").value = 5;
                                            document.getElementById("dtStatusFaturaContaCorrente").value = dataAtual = ano + '-' + mes + '-' + dia;
                                        }
                                        else {

                                        }
                                    }
                                })
                            } else {
                                $("#msgCambioErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                            }
                        } else if (dado == "1") {
                            $("#msgCambioErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        } else if (dado == "2") {
                            $("#msgExportErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        } else {
                            $("#msgDemuFinalErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
            else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function ExportarParcelaCC(idParcela) {
            if (idFatura != 0) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/infoExportFatura",
                    data: '{idFatura:"' + idFatura + '", check: "' + vlCheck + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        if (dado == "0") {
                            if (atualizaCambio != 1) {
                                $("#modalExportarParcelaContaCorrente").modal("show");
                                $.ajax({
                                    type: "POST",
                                    url: "DemurrageService.asmx/infoExportCC",
                                    data: '{idFatura:"' + idFatura + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (dado) {
                                        var dado = dado.d;
                                        dado = $.parseJSON(dado);
                                        if (dado != null) {
                                            document.getElementById("idFaturaContaCorrenteParcela").value = dado[0]["ID_DEMURRAGE_FATURA"];
                                            document.getElementById("idFaturaParcelaContaCorrente").value = idParcela;
                                            document.getElementById("nrProcessoFaturaParcelaContaCorrente").value = dado[0]["NR_PROCESSO"];
                                            document.getElementById("nmClienteFaturaParcelaContaCorrente").value = dado[0]["CLIENTE"];
                                            document.getElementById("dtLiquidacaoFaturaParcelaContaCorrente").value = dataAtual = ano + '-' + mes + '-' + dia;
                                            document.getElementById("MainContent_ddlStatusFaturaParcela ContaCorrente").value = 5;
                                            document.getElementById("dtStatusFaturaParcelaContaCorrente").value = dataAtual = ano + '-' + mes + '-' + dia;
                                        }
                                        else {

                                        }
                                    }
                                })
                            } else {
                                $("#msgSuccessUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);
                            }
                        } else if (dado == "1") {
                            $("#msgCambioErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        } else if (dado == "2") {
                            $("#msgExportErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        } else {
                            $("#msgDemuFinalErrFatura").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
            else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function EnviarFaturamento(idParcela) {
            Swal.fire({
                title: 'Deseja enviar para faturamento?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/EnviarFaturamento",
                        data: '{idParcela: "' + idParcela + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != "2") {
                                Swal.fire('Saved!', '', 'success')
                                //$("#modalDialogParcela").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                /*$("#msgSuccessUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);*/
                                //listarFatura();
                                //consultaFiltrada();
                                listarParcelas();
                            }
                            else {
                                Swal.fire('Changes are not saved', '', 'info')
                                //$("#modalDialogParcela").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                /*$("#msgErrUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);*/
                                //listarFatura();
                                //consultaFiltrada();
                                listarParcelas();
                            }
                        }
                    })
                }
            })
        }

        function exportarConta() {
            var dtLiquidacao = document.getElementById("dtLiquidacaoFaturaContaCorrente").value;
            var dsStatus = 0;

            console.log(vlCheck);

            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/exportarCC",
                data: '{idFatura:"' + idFatura + '",dtLiquidacao: "' + dtLiquidacao + '", check: "' + vlCheck + '"}',
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != "null" && dado != null) {
                        $("#modalExportarContaCorrente").modal("hide");
                        $("#msgExportSuccess").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        consultaFiltrada();
                    }
                    else {
                        $("#modalExportarContaCorrente").modal("hide");
                        $("#msgErrExportDadoConta").fadeIn(500).delay(1000).fadeOut(500);
                        listarFatura();
                        consultaFiltrada();
                    }
                }
            })
        }

        function finalizarFaturaDemurrage() {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, finalizar!',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/finalizarFaturaDemurrage",
                        data: '{idFatura:"' + idFatura + '", check: "' + vlCheck + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado == "OK") {
                                $("#modalParcelamentoDemurrage").modal("hide");
                                listarFatura();
                                consultaFiltrada();
                            }
                            else {
                                $("#modalParcelamentoDemurrage").modal("hide");
                                listarFatura();
                                consultaFiltrada();
                            }
                        }
                    })
                }
            })
        }

        function RegistrarTaxa(idParcela) {
            var faturaParcela = document.getElementById("idFaturaParcelaContaCorrente").value;
            var dsStatus = 0;

            console.log(vlCheck);
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/RegistrarTaxa",
                        data: '{idFatura:"' + idFatura + '", idFaturaParcela: "' + idParcela + '", check: "' + vlCheck + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != "2") {
                                Swal.fire('Saved!', '', 'success')
                                //$("#modalExportarParcelaContaCorrente").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                listarParcelas();
                            }
                            else {
                                Swal.fire('Changes are not saved', '', 'info')
                                //$("#modalExportarParcelaContaCorrente").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                listarParcelas();
                            }
                        }
                    })
                }
            })
        }
        $('#modalParcelamentoDemurrage').on('hidden.bs.modal', function () {
            listarFatura();
            consultaFiltrada();
        })

        function exportarParcelaConta() {
            var dtLiquidacao = document.getElementById("dtLiquidacaoFaturaParcelaContaCorrente").value;
            var faturaParcela = document.getElementById("idFaturaParcelaContaCorrente").value;
            var dsStatus = 0;

            console.log(vlCheck);
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/exportarParcelaCC",
                        data: '{idFatura:"' + idFatura + '", idFaturaParcela: "' + faturaParcela + '", dtLiquidacao: "' + dtLiquidacao + '", check: "' + vlCheck + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != "2") {
                                Swal.fire('Saved!', '', 'success')
                                $("#modalExportarParcelaContaCorrente").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                /*$("#msgSuccessUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);*/
                                //listarFatura();
                                //consultaFiltrada();
                                listarParcelas();
                            }
                            else {
                                Swal.fire('Changes are not saved', '', 'info')
                                $("#modalExportarParcelaContaCorrente").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                /*$("#msgErrUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);*/
                               // listarFatura();
                                //consultaFiltrada();
                                listarParcelas();
                            }
                        }
                    })
                }
            })
        }

        function CancelarExportarParcelaCC(idParcela) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/cancelarExportarParcelaCC",
                        data: '{idParcela: "' + idParcela + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado != "2") {
                                Swal.fire('Saved!', '', 'success')
                                //$("#modalDialogParcela").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                /*$("#msgSuccessUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);*/
                                //listarFatura();
                                //consultaFiltrada();
                                listarParcelas();
                            }
                            else {
                                Swal.fire('Changes are not saved', '', 'info')
                                //$("#modalDialogParcela").modal("hide");
                                //$("#modalParcelamentoDemurrage").modal("hide");
                                /*$("#msgErrUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);*/
                                //listarFatura();
                                //consultaFiltrada();
                                listarParcelas();
                            }
                        }
                    })
                }
            })
        }

        function estimativaCV() {
            var estimadoCompra;
            var estimadoVenda
            if (checkV.checked) {
                vlCheck = checkV.value;
                document.getElementById("modalEstimativaTitle").textContent = "Estimativa Compra e Venda";
            }
            else {
                vlCheck = checkC.value;
                document.getElementById("modalEstimativaTitle").textContent = "Estimativa Compra e Venda";
            }

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
                            if (dado[i]["MOEDA_COMPRA"] == 0) {
                                dado[i]["MOEDA_COMPRA"] = "TABELA";
                                dado[i]["VALOR_COMPRA"] = "TABELA";
                                dado[i]["VALOR_COMPRA_REAL"] = "TABELA";
                            }
                            if (dado[i]["MOEDA_VENDA"] == 0) {
                                dado[i]["MOEDA_VENDA"] = "TABELA";
                                dado[i]["VALOR_VENDA"] = "TABELA";
                                dado[i]["VALOR_VENDA_REAL"] = "TABELA";
                            }
                            $("#grdEstimativaBody").append("<tr><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["MBL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["QT_DIAS_FREETIME_CONFIRMA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_FINAL_FREETIME"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'><input type='checkbox' id='chkCompra" + i + "' name='vlEstimadoCompra[" + i + "]'></td>" +
                                "<td class='text-center'>" + dado[i]["MOEDA_COMPRA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VALOR_COMPRA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VALOR_COMPRA_REAL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DATA_PAGAMENTO"] + "</td>" +
                                "<td class='text-center'><input type='checkbox' id='chkVenda" + i + "' name='vlEstimadoVenda[" + i + "]'></td>" +
                                "<td class='text-center'>" + dado[i]["MOEDA_VENDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VALOR_VENDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VALOR_VENDA_REAL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DATA_RECEBIMENTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_STATUS_DEMURRAGE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td></tr > ");
                            if (dado[i]["VALOR_COMPRA_ESTIMADO"] == 1) {
                                $("#chkCompra" + i + "").prop('checked', true)
                            }
                            else {
                                $("#chkCompra" + i + "").prop('checked', false);
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

        function setId(Id) {
            id = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdFatura(Id, cambio) {
            idFatura = Id;
            atualizaCambio = cambio;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdFaturaItens(Id, cambio) {
            idFaturaItens = Id;
            atualizaCambio = cambio;
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
                'MainContent_checkIMO',
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
            var checkboxIMO = document.getElementById("MainContent_checkIMO");
            var checkboxIMOvalue;
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
            if (checkboxIMO.checked) {
                checkboxIMOvalue = "1";
            } else {
                checkboxIMOvalue = "0";
            }
            var dado = {
                "ID_PARCEIRO_TRANSPORTADOR": document.getElementById("MainContent_ddlParceiroTransportador").value,
                "ID_TIPO_CONTAINER": document.getElementById("MainContent_ddlTipoContainer").value,
                "DT_VALIDADE_INICIAL": document.getElementById("dtValidade").value,
                "QT_DIAS_FREETIME": document.getElementById("qtFreetime").value,
                "ID_MOEDA": document.getElementById("MainContent_ddlMoeda").value,
                "FL_ESCALONADA": checkboxvalue,
                "FL_CARGA_IMO": checkboxIMOvalue,
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
                "VL_VENDA_08": document.getElementById("vlDemurrage8").value,
                "ID_TAMANHO_CONTAINER": document.getElementById("MainContent_ddlTamanhoContainer").value
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
                    listarTabelaDemurrage();
                    if (dado.d == "1") {
                        $("#msgSuccessTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "0") {
                        $("#msgErrTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    if (dado.d == "2") {
                        $("#msgExistsTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
                    }
                },
                error: function () {
                    $("#modalDemurrage").modal('hide');
                    $("#msgErrTabelaDemurrage").fadeIn(500).delay(1000).fadeOut(500);
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
                'MainContent_checkIMO',
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
                'MainContent_checkIMO',
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
            idFatura = 0;
            var idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            var stringConsulta = document.getElementById("txtConsulta").value;
            var finalizado = document.getElementById("MainContent_chkFinalizado");
            var finalizadoValue;
            var ativo = document.getElementById("MainContent_chkAtivo");
            var ativoValue;
            var tarifaSpot = document.getElementById("MainContent_chkTarifaSpot");
            var tarifaSpotValue;
            var freetime = "";
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

            if (tarifaSpot.checked) {
                tarifaSpotValue = "1";
            }
            else {
                tarifaSpotValue = "0";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/filtrarTabela",
                data: '{idFilter:"' + idFiltro + '", Filter:"' + stringConsulta + '", Finalizado: "' + finalizadoValue + '",Ativo: "' + ativoValue + '", TarifaSpot: "' + tarifaSpotValue+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdModuloDemurrage").empty();
                    $("#grdModuloDemurrage").append("<tr><td colspan='22'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdModuloDemurrage").empty();
                        for (let i = 0; i < dado.length; i++) {
                            console.log(dado[i]["QT_DIAS_FREETIME"] + ' - ' + dado[i]["QT_DIAS_FREETIME_CONFIRMA"]);
                            if (parseInt(dado[i]["QT_DIAS_FREETIME"]) != parseInt(dado[i]["QT_DIAS_FREETIME_CONFIRMA"])) {
                                freetime = "<td class='text-center' style='background-color: yellow;'>" + dado[i]["QT_DIAS_FREETIME_CONFIRMA"] + "</td>";
                            } else {
                                freetime = "<td class='text-center'>" + dado[i]["QT_DIAS_FREETIME_CONFIRMA"] + "</td>"
                            }
                            if (dado[i]["QT_DIAS_DEMURRAGE"] >= -10 && dado[i]["QT_DIAS_DEMURRAGE"] <= -1) {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(153,51,153,1); font-weight: bold'>" +
                                    "<td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_CARGA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MBL"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["TRANSPORTADOR"] + "' style='max-width: 14ch;'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ACORDO_COMERCIAL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    freetime +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_OBSERVACAO"] + "' style='max-width: 14ch;'>" + dado[i]["DS_OBSERVACAO"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_STATUS_DEMURRAGE_COMPRA"] + "' style='max-width: 14ch;'>" + dado[i]["DS_STATUS_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_PAGAR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA_BR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_STATUS_DEMURRAGE"] + "' style='max-width: 14ch;'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_RECEBER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA_BR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td>" +
                                    "</tr> ");
                            }
                            else if (dado[i]["QT_DIAS_DEMURRAGE"] >= 0) {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "' style='color: rgba(255,0,0,0.8); font-weight: bold'>" +
                                    "<td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_CARGA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MBL"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["TRANSPORTADOR"] + "' style='max-width: 14ch;'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ACORDO_COMERCIAL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    freetime +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_OBSERVACAO"] + "' style='max-width: 14ch;'>" + dado[i]["DS_OBSERVACAO"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_STATUS_DEMURRAGE_COMPRA"] + "' style='max-width: 14ch;'>" + dado[i]["DS_STATUS_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_PAGAR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA_BR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_STATUS_DEMURRAGE"] + "' style='max-width: 14ch;'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_RECEBER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA_BR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td>" +
                                    "</tr> ");
                            }
                            else {
                                $("#grdModuloDemurrage").append("<tr data-id='" + dado[i]["ID_CNTR"] + "'><td class='text-center'><div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR"] + ")'>Selecionar</div></td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_CARGA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MBL"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["TRANSPORTADOR"] + "' style='max-width: 14ch;'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ACORDO_COMERCIAL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    freetime +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_OBSERVACAO"] + "' style='max-width: 14ch;'>" + dado[i]["DS_OBSERVACAO"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_STATUS_DEMURRAGE_COMPRA"] + "' style='max-width: 14ch;'>" + dado[i]["DS_STATUS_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_PAGAR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA_BR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td>" +
                                    "<td class='text-center' title='" + dado[i]["DS_STATUS_DEMURRAGE"] + "' style='max-width: 14ch;'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_RECEBER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA_BR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td>" +
                                    "</tr > ");
                            }
                        }
                    }
                    else {
                        $("#grdModuloDemurrage").empty();
                        $("#grdModuloDemurrage").append("<tr id='msgEmptyWeek'><td colspan='22' class='alert alert-light text-center'>Resultado não encontrado</td></tr>");
                    }
                }
            });
        }

        function downloadCSVAtual(csv, filename) {
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

        function exportTableToCSVAtual(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdDemurrageAtual tr");
            var f;
            for (var i = 0; i < rows.length; i++) {
                var row = [];
                if (i > 0) {
                    var cols = rows[i].querySelectorAll("#grdDemurrageAtual td, #grdDemurrageAtual th");
                } else {
                    var cols = rows[i].querySelectorAll("#grdDemurrageAtual td, #grdDemurrageAtual th");
                }
                for (var j = 1; j < cols.length; j++)
                    row.push(cols[j].innerText);
                console.log(row);
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

        function ImprimirRecibo() {
            if (idFatura != 0) {
                var ID = idFatura;
                console.log(ID);

                window.open('ReciboDemurrage.aspx?id=' + ID, '_blank');
            } else {
                $("#msgSelectErrFatura").fadeIn(500).delay(1000).fadeOut(500);
            }
        }


    </script>

</asp:Content>
