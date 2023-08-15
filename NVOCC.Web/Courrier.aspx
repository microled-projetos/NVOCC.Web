<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courrier.aspx.cs" Inherits="ABAINFRA.Web.Courrier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Controle de Courrier
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Courrier
                            </a>
                        </li>
                        <li id="tabRetirada">
                            <a href="#retirada" id="linkRetirada" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Retirada
                            </a>
                        </li>
                        <li id="tabLiberacao">
                            <a href="#liberacao" id="linkLiberacao" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Liberacao
                            </a>
                        </li>
                        <li id="tabConcluido">
                            <a href="#concluido" id="linkConcluido" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Concluido
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"><span class="required">&nbsp</span></label>
                                        <input id="txtConsulta" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="listarCourrier()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSV" onclick="GerarCSV('Courrier.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                        <button type="button" id="btnConsultaAvancada" data-toggle="modal" data-target="#modalFiltroAvancado" class="btn btn-primary">Filtro Avançado</button>
                                        <button type="button" id="btnRemoverFiltros" onclick="limparFiltros()" class="btn btn-primary">Limpar Filtros</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="alert alert-success text-center" id="msgSuccessUpdate">
                                        Courrier atualizado com sucesso.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrUpdate">
                                        Erro ao atualizar courrier.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrCourrier">
                                        BL sem Master Vinculado.
                                    </div>
                                </div>
                            </div>
                            <div class="row topMarg" style="display: flex;">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:CheckBox ID="chkFCL" runat="server" CssClass="form-control noborder" Text="&nbsp;FCL"></asp:CheckBox>
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkLCL" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;LCL"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:CheckBox ID="chkCSaida" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Com Data Saida"></asp:CheckBox>
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkSSaida" runat="server" CssClass="form-control noborder" Text="&nbsp;Sem Data Saida"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:CheckBox ID="chkBranco" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Em Branco"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:CheckBox ID="chkFreehand" runat="server" CssClass="form-control noborder" Text="&nbsp;Freehand"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExport" class="table tablecont tablesorter">
                                    <thead>
                                        <tr>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PROCESSO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PREVISÃO CHEGADA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CÓD RASTREAMENTO MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RECEBIMENTO MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CÓD RASTREAMENTO HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RECEBIMENTO HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">WEEK</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">AGENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CLIENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RETIRADA PERSONAL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">OBS</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">TROCA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">FATURA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">AÇÕES</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrier">
                                       
                                    </tbody>
                                </table>
                            </div>
                            <div style="display: flex; flex-direction: row; width: auto; justify-content: center; gap: 50px; margin-top: 15px;">
                                <span class="btn-primary" style="padding: 10px; border-radius: 3px"><i class='fas fa-edit'></i> - Editar Informações</span>
                                <span class="btn-success" style="padding: 10px; border-radius: 3px"><i class='fas fa-exchange-alt'></i> - Enviar um email ao cliente informando que o BL esta disponível para troca.</span>
                                <span class="btn-warning" style="padding: 10px; border-radius: 3px"><i class='far fa-file-alt'></i> - Enviar um email ao cliente informando que os documentos estão disponíveis para retirada.</span>
                                <span class="btn-info" style="padding: 10px; border-radius: 3px"><i class='fas fa-user'></i> -  Enviar um email ao Agente Internacional solicitando o envio dos documentos originais.</span>
                            </div>

                            <div class="modal fade bd-example-modal-xl" id="modalFiltroAvancado" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalFiltroAvancadoTitle">Filtro Avançado</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL</label>
                                                        <input type="text" id="txtHBL" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO MBL (INICIO)</label>
                                                        <input type="date" id="txtDtRecebMBLinicio" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO MBL (FIM)</label>
                                                        <input type="date" id="txtDtRecebMBLfim" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CÓD RASTREAMENTO MBL</label>
                                                        <input type="text" id="txtCdRastreioMBL" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO HBL (INICIO)</label>
                                                        <input type="date" id="dtRecebHBLinicio" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO HBL (FIM)</label>
                                                        <input type="date" id="dtRecebHBLfim" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CÓD RASTREAMENTO HBL</label>
                                                        <input type="text" id="txtCdRastreioHBL" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">AGENTE</label>
                                                        <input id="txtAgente" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                 <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (INICIO)</label>
                                                        <input id="dtPrevisaoChegadainicio" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (FIM)</label>
                                                        <input id="dtPrevisaoChegadafim" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (INICIO)</label>
                                                        <input id="dtChegadainicio" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (FIM)</label>
                                                        <input id="dtChegadafim" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">FATURA</label>
                                                        <input id="txtFatura" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (INICIO)</label>
                                                        <input id="dtRetiradaPersonalInicio" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (FIM)</label>
                                                        <input id="dtRetiradaPersonalFim" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnConsultarFilter" onclick="listarCourrier()" data-dismiss="modal" class="btn btn-primary btn-ok">Consultar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal fade bd-example-modal-lg" id="modalEditCourrier" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalFCLexpoTitle">Alterar Courrier</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body" style="padding:20px">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">PROCESSO:</label>
                                                        <input id="nrprocesso" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">CLIENTE:</label>
                                                        <input id="nmcliente" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">MBL:</label>
                                                        <input id="idmbl" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL:</label>
                                                        <input id="idhbl" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <input type="checkbox" id="checkOrigem" name="OrigemDestinoLivre" onchange="flagOrigem()"/>
                                                        <label for="checkOrigem">Origem</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <input type="checkbox" id="checkDestino" name="OrigemDestinoLivre" onchange="flagDestino()"/>
                                                        <label for="checkDestino">Destino</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <input type="checkbox" id="checkTroca" name="Troca"/>
                                                        <label for="checkTroca">Troca</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Recebimento MBL</label>
                                                        <input id="dtRecebimentoMBL" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Código Rastreamento MBL</label>
                                                        <input id="cdRastreamentoMBL" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Recebimento HBL</label>
                                                        <input id="dtRecebimentoHBL" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Código Rastreamento HBL</label>
                                                        <input id="cdRastreamentoHBL" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Nº da Fatura</label>
                                                        <input id="nrFatura" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data da Retirada Personal</label>
                                                        <input id="dtRetiradaTerceiro" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação</label>
                                                        <textarea id="dsObservacao" class="form-control"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnEditarCourrier" class="btn btn-success" >Salvar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="retirada">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlFiltroRetirada" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"><span class="required">&nbsp</span></label>
                                        <input id="txtConsultaRetirada" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsultaRetirada" onclick="listarCourrierRetirada()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSVRetirada" onclick="GerarCSVRetirada('Courrier.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                        <button type="button" id="btnConsultaAvancadaRetirada" data-toggle="modal" data-target="#modalFiltroAvancadoRetirada" class="btn btn-primary">Filtro Avançado</button>
                                        <button type="button" id="btnRemoverFiltrosRetirada" onclick="limparFiltrosRetirada()" class="btn btn-primary">Limpar Filtros</button>
                                    </div>
                                </div>
                            </div>
                             <div class="row topMarg">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagemRetirada" ID="chkFCLRetirada" runat="server" CssClass="form-control noborder" Text="&nbsp;FCL"></asp:RadioButton>
                                    </div>
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagemRetirada" ID="chkLCLRetirada" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;LCL"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                             <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExportRetirada" class="table tablecont tablesorter">
                                    <thead>
                                        <tr>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PROCESSO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PREVISÃO CHEGADA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA CHEGADA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CLIENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">TROCA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RETIRADA CLIENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">RETIRADO POR</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">AÇÕES</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrierRetirada">
                                       
                                    </tbody>
                                </table>
                            </div>
                             <div class="modal fade bd-example-modal-xl" id="modalFiltroAvancadoRetirada" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalFiltroAvancadoTitleRetirada">Filtro Avançado</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL</label>
                                                        <input type="text" id="txtHBLRetirada" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">AGENTE</label>
                                                        <input id="txtAgenteRetirada" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">RETIRADO POR</label>
                                                        <input id="txtRetiradorPorRetirada" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                 <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (INICIO)</label>
                                                        <input id="dtPrevisaoChegadainicioRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (FIM)</label>
                                                        <input id="dtPrevisaoChegadafimRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (INICIO)</label>
                                                        <input id="dtChegadainicioRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (FIM)</label>
                                                        <input id="dtChegadafimRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (INICIO)</label>
                                                        <input id="dtRetiradaPersonalInicioRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (FIM)</label>
                                                        <input id="dtRetiradaPersonalFimRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA CLIENTE (INICIO)</label>
                                                        <input id="dtRetiradaClienteInicioRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA CLIENTE (FIM)</label>
                                                        <input id="dtRetiradaClienteFimRetirada" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnConsultarFilterRetirada" onclick="listarCourrierRetirada()" data-dismiss="modal" class="btn btn-primary btn-ok">Consultar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal fade bd-example-modal-lg" id="modalEditCourrierRetirada" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalFCLexpoTitleRetirada">Alterar Courrier</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body" style="padding:20px">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">PROCESSO:</label>
                                                        <input id="nrprocessoRetirada" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">CLIENTE:</label>
                                                        <input id="nmclienteRetirada" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">MBL:</label>
                                                        <input id="idmblRetirada" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL:</label>
                                                        <input id="idhblRetirada" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Data da Retirada Cliente</label>
                                                        <input id="dtRetiradaTerceiroRetirada" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="form-group">
                                                        <label class="control-label">Retirado Por</label>
                                                        <input id="nmRetiradoPorRetirada" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação</label>
                                                        <textarea id="dsObservacaoRetirada" class="form-control"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnEditarCourrierRetirada" class="btn btn-success">Salvar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="liberacao">    
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlFiltroLiberacao" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"><span class="required">&nbsp</span></label>
                                        <input id="txtConsultaLiberacao" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsultaLiberacao" onclick="listarCourrierLiberacao()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSVLiberacao" onclick="GerarCSVLiberacao('Courrier.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                        <button type="button" id="btnConsultaAvancadaLiberacao" data-toggle="modal" data-target="#modalFiltroAvancadoLiberacao" class="btn btn-primary">Filtro Avançado</button>
                                        <button type="button" id="btnRemoverFiltrosLiberacao" onclick="limparFiltrosLiberacao()" class="btn btn-primary">Limpar Filtros</button>
                                    </div>
                                </div>
                            </div>
                             <div class="row topMarg">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagemLiberacao" ID="chkFCLLiberacao" runat="server" CssClass="form-control noborder" Text="&nbsp;FCL"></asp:RadioButton>
                                    </div>
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagemLiberacao" ID="chkLCLLiberacao" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;LCL"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                             <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExportLiberacao" class="table tablecont tablesorter">
                                    <thead>
                                        <tr>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PROCESSO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA CHEGADA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CÓD RASTREAMENTO HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">LOTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CLIENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RECEBIMENTO HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">HBL DIGITAL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">TROCA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">TERMO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DESBLOQUEIO DOCUMENTAL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">OBS</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">AÇÕES</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrierLiberacao">
                                       
                                    </tbody>
                                </table>
                            </div>
                             <div class="modal fade bd-example-modal-xl" id="modalFiltroAvancadoLiberacao" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalFiltroAvancadoTitleLiberacao">Filtro Avançado</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL</label>
                                                        <input type="text" id="txtHBLLiberacao" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO MBL (INICIO)</label>
                                                        <input type="date" id="dtRecebMBLInicioLiberacao" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO MBL (FIM)</label>
                                                        <input type="date" id="dtRecebMBLFimLiberacao" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CÓD RASTREAMENTO MBL</label>
                                                        <input type="text" id="txtCdRastreioMBLLiberacao" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (INICIO)</label>
                                                        <input id="dtPrevisaoChegadaInicioLiberacao" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (FIM)</label>
                                                        <input id="dtPrevisaoChegadaFimLiberacao" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (INICIO)</label>
                                                        <input id="dtChegadaInicioLiberacao" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (FIM)</label>
                                                        <input id="dtChegadaFimLiberacao" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">AGENTE</label>
                                                        <input id="txtAgenteLiberacao" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (INICIO)</label>
                                                        <input id="dtRetiradaPersonalInicioLiberacao" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (FIM)</label>
                                                        <input id="dtRetiradaPersonalFimLiberacao" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnConsultarFilterLiberacao" onclick="listarCourrierLiberacao()" data-dismiss="modal" class="btn btn-primary btn-ok">Consultar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal fade bd-example-modal-lg" id="modalEditCourrierLiberacao" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalFCLexpoTitleliberacao">Alterar Courrier</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body" style="padding:20px">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">PROCESSO:</label>
                                                        <input id="nrprocessoliberacao" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">CLIENTE:</label>
                                                        <input id="nmclienteliberacao" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">MBL:</label>
                                                        <input id="idmblliberacao" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL:</label>
                                                        <input id="idhblliberacao" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação</label>
                                                        <textarea id="dsObservacaoliberacao" class="form-control"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnEditarCourrierliberacao" class="btn btn-success">Salvar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="concluido">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlFiltroConcluido" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"><span class="required">&nbsp</span></label>
                                        <input id="txtConsultaConcluido" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsultaConcluido" onclick="listarCourrierConcluido()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSVConcluido" onclick="GerarCSVConcluido('Courrier.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                        <button type="button" id="btnConsultaAvancadaConcluido" data-toggle="modal" data-target="#modalFiltroAvancadoConcluido" class="btn btn-primary">Filtro Avançado</button>
                                        <button type="button" id="btnRemoverFiltrosConcluido" onclick="limparFiltrosConcluido()" class="btn btn-primary">Limpar Filtros</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row topMarg">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagemConcluido" ID="chkFCLConcluido" runat="server" CssClass="form-control noborder" Text="&nbsp;FCL"></asp:RadioButton>
                                    </div>
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagemConcluido" ID="chkLCLConcluido" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;LCL"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExportConcluido" class="table tablecont tablesorter">
                                    <thead>
                                        <tr>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">&nbsp;</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PROCESSO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PREVISÃO CHEGADA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CÓD RASTREAMENTO MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RECEBIMENTO MBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CÓD RASTREAMENTO HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RECEBIMENTO HBL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">WEEK</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">AGENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CLIENTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA RETIRADA PERSONAL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">RETIRADO POR</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">OBS</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">TROCA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">FATURA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DISPONIBILIDADE DE BL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">AGENTE INTERNACIONAL</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrierConcluido">
                                       
                                    </tbody>
                                </table>
                            </div>
                            <div class="modal fade bd-example-modal-xl" id="modalFiltroAvancadoConcluido" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalFiltroAvancadoTitleConcluido">Filtro Avançado</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL</label>
                                                        <input type="text" id="txtHBLConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO MBL (INICIO)</label>
                                                        <input type="date" id="txtDtRecebMBLinicioConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO MBL (FIM)</label>
                                                        <input type="date" id="txtDtRecebMBLfimConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CÓD RASTREAMENTO MBL</label>
                                                        <input type="text" id="txtCdRastreioMBLConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO HBL (INICIO)</label>
                                                        <input type="date" id="dtRecebHBLinicioConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RECEBIMENTO HBL (FIM)</label>
                                                        <input type="date" id="dtRecebHBLfimConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CÓD RASTREAMENTO HBL</label>
                                                        <input type="text" id="txtCdRastreioHBLConcluido" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">AGENTE</label>
                                                        <input id="txtAgenteConcluido" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                 <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (INICIO)</label>
                                                        <input id="dtPrevisaoChegadainicioConcluido" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PREVISÃO CHEGADA (FIM)</label>
                                                        <input id="dtPrevisaoChegadafimConcluido" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (INICIO)</label>
                                                        <input id="dtChegadainicioConcluido" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA CHEGADA (FIM)</label>
                                                        <input id="dtChegadafimConcluido" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">FATURA</label>
                                                        <input id="txtFaturaConcluido" class="form-control" type="text"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (INICIO)</label>
                                                        <input id="dtRetiradaPersonalInicioConcluido" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA RETIRADA PERSONAL (FIM)</label>
                                                        <input id="dtRetiradaPersonalFimConcluido" class="form-control" type="date"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnConsultarFilterConcluido" onclick="listarCourrierConcluido()" data-dismiss="modal" class="btn btn-primary btn-ok">Consultar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        var idFiltro;
        var stringConsulta;
        var filter_fcl;
        var filter_lcl;
        var filter_dtsaida;
        var filter_dtsaidan;
        var filter_branco;
        var filter_freehand;
        var tipoValue;
        var idblC;    

        function BuscarCourrier(id) {
            idblC = id;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscarCourrier",
                data: '{id: "'+id+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var data = dado.d;
                    data = $.parseJSON(data);
                    if (data != "erro") {
                        $("#modalEditCourrier").modal('show');
                        document.getElementById('nrprocesso').value = data.NR_PROCESSO;
                        document.getElementById('nmcliente').value = data.NM_RAZAO;
                        document.getElementById('idmbl').value = data.NR_BL_MASTER;
                        document.getElementById('idhbl').value = data.NR_BL;
                        document.getElementById('dtRecebimentoMBL').value = data.DT_RECEBIMENTO_MBL;
                        document.getElementById('cdRastreamentoMBL').value = data.CD_RASTREAMENTO_MBL;
                        document.getElementById('dtRecebimentoHBL').value = data.DT_RECEBIMENTO_HBL;
                        document.getElementById('cdRastreamentoHBL').value = data.CD_RASTREAMENTO_HBL;
                        document.getElementById('nrFatura').value = data.NR_FATURA_COURRIER;
                        document.getElementById('dtRetiradaTerceiro').value = data.DT_RETIRADA_PERSONAL;
                        document.getElementById('checkTroca').checked = data.FL_TROCA == 'True' ? true : false;
                        document.getElementById('dtRecebimentoHBL').disabled = data.CD_RASTREAMENTO_HBL == 'ORIGEM' ? true : false;
                        document.getElementById('cdRastreamentoHBL').disabled = data.CD_RASTREAMENTO_HBL == 'ORIGEM' ? true : false;
                        document.getElementById('dtRetiradaTerceiro').disabled = data.CD_RASTREAMENTO_HBL == 'ORIGEM' ? true : false;
                        document.getElementById("checkOrigem").checked = data.CD_RASTREAMENTO_HBL == 'ORIGEM' ? true : false;
                        document.getElementById("checkDestino").checked = data.CD_RASTREAMENTO_HBL == 'ORIGEM' ? false : data.CD_RASTREAMENTO_HBL == 'DESTINO' ? true : false;
                        document.getElementById("dsObservacao").value = data.OBS_COURRIER;
                    } else {
                        $("#msgErrCourrier").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function TrocaCourrier(id) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Esta ação enviará um email ao cliente informando que o BL esta disponível para troca. Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    idblC = id;
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/TrocaCourrier",
                        data: '{id: "' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var data = dado.d;
                            data = $.parseJSON(data);
                            if (data != "False" && data != "erro") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrier();
                            } else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrier();
                            }
                        }
                    })
                }
            })
        }

        function DisponibilidadeBL(id) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Esta ação enviará um email ao cliente informando que os documentos estão disponíveis para retirada. Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    idblC = id;
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/DisponibilidadeCourrier",
                        data: '{id: "' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var data = dado.d;
                            data = $.parseJSON(data);
                            if (data != "False" && data != "erro") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrier();
                            } else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrier();
                            }
                        }
                    })
                }
            })
        }

        function AgenteInternacional(id) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Esta ação enviará um email ao Agente Internacional solicitando o envio dos documentos originais. Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    idblC = id;
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/SolicitarDocumentoAgenteInternacional",
                        data: '{id: "' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var data = dado.d;
                            data = $.parseJSON(data);
                            if (data != "False" && data != "erro") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrier();
                            } else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrier();
                            }
                        }
                    })
                }
            })
        }

        function BuscarCourrierRetirada(id) {
            idblC = id;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscarCourrier",
                data: '{id: "' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var data = dado.d;
                    data = $.parseJSON(data);
                    if (data != "erro") {
                        $("#modalEditCourrierRetirada").modal('show');
                        document.getElementById('nrprocessoRetirada').value = data.NR_PROCESSO;
                        document.getElementById('nmclienteRetirada').value = data.NM_RAZAO;
                        document.getElementById('idmblRetirada').value = data.NR_BL_MASTER;
                        document.getElementById('idhblRetirada').value = data.NR_BL;
                        document.getElementById('dtRetiradaTerceiroRetirada').value = data.DT_RETIRADA_COURRIER;
                        document.getElementById('nmRetiradoPorRetirada').value = data.NM_RETIRADO_POR_COURRIER;
                        document.getElementById('dsObservacaoRetirada').value = data.OBS_COURRIER;
                    } else {
                        $("#msgErrCourrier").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function BuscarCourrierLiberacao(id) {
            idblC = id;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscarCourrier",
                data: '{id: "' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var data = dado.d;
                    data = $.parseJSON(data);
                    if (data != "erro") {
                        $("#modalEditCourrierLiberacao").modal('show');
                        document.getElementById('nrprocessoliberacao').value = data.NR_PROCESSO;
                        document.getElementById('nmclienteliberacao').value = data.NM_RAZAO;
                        document.getElementById('idmblliberacao').value = data.NR_BL_MASTER;
                        document.getElementById('idhblliberacao').value = data.NR_BL;                       
                        document.getElementById('dsObservacaoliberacao').value = data.OBS_COURRIER;
                    } else {
                        $("#msgErrCourrier").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function BloquearDocumento(lote, idbl) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Esta ação marcara o processo com 'Bloqueio Documental'.",
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
                        url: "DemurrageService.asmx/BloquearDocumento",
                        data: '{lote: "' + lote + '", idbl: "' + idbl +'" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var data = dado.d;
                            data = $.parseJSON(data);
                            if (data != "erro") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrierLiberacao();
                            } else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrierLiberacao();
                            }
                        }
                    })
                }
            })
        }

        function DesbloquearDocumento(lote, idbl) {
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Esta ação removera a marcação 'Bloqueio Documental' do processo .",
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
                        url: "DemurrageService.asmx/DesbloquearDocumento",
                        data: '{lote: "' + lote + '", idbl: "'+idbl+'" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var data = dado.d;
                            data = $.parseJSON(data);
                            if (data != "erro") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrierLiberacao();
                            } else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrierLiberacao();
                            }
                        }
                    })
                }
            })
        }

        function flagOrigem() {
            if (document.getElementById("checkOrigem").checked) {
                document.getElementById('dtRecebimentoHBL').disabled = true;
                document.getElementById('cdRastreamentoHBL').disabled = true;
                document.getElementById('dtRetiradaTerceiro').disabled = true;
                document.getElementById('cdRastreamentoHBL').value = "ORIGEM";
                document.getElementById("checkDestino").checked = false;

            } else {
                document.getElementById('dtRecebimentoHBL').disabled = false;
                document.getElementById('cdRastreamentoHBL').disabled = false;
                document.getElementById('dtRetiradaTerceiro').disabled = false;                
                document.getElementById('cdRastreamentoHBL').value = "";
            }
        }
        function flagDestino() {
            if (document.getElementById("checkDestino").checked) {
                document.getElementById('dtRecebimentoHBL').disabled = false;
                document.getElementById('cdRastreamentoHBL').disabled = false;
                document.getElementById('dtRetiradaTerceiro').disabled = false;
                document.getElementById('cdRastreamentoHBL').value = "DESTINO";
                document.getElementById("checkOrigem").checked = false;

            } else {
                document.getElementById('dtRecebimentoHBL').disabled = false;
                document.getElementById('cdRastreamentoHBL').disabled = false;
                document.getElementById('dtRetiradaTerceiro').disabled = false;
                document.getElementById('cdRastreamentoHBL').value = "";
            }
        }
        function flagOrigemRetirada() {
            if (document.getElementById("checkOrigemRetirada").checked) {
                document.getElementById('dtRecebimentoHBLRetirada').disabled = true;
                document.getElementById('cdRastreamentoHBLRetirada').disabled = true;
                document.getElementById('dtRetiradaRetirada').disabled = true;
                document.getElementById('dtRetiradaTerceiroRetirada').disabled = true;
                document.getElementById('receptorRetirada').disabled = true;
                document.getElementById('cdRastreamentoHBLRetirada').value = "ORIGEM";
                document.getElementById("checkDestinoRetirada").checked = false;

            } else {
                document.getElementById('dtRecebimentoHBLRetirada').disabled = false;
                document.getElementById('cdRastreamentoHBLRetirada').disabled = false;
                document.getElementById('dtRetiradaRetirada').disabled = false;
                document.getElementById('dtRetiradaTerceiroRetirada').disabled = false;
                document.getElementById('receptorRetirada').disabled = false;
                document.getElementById('cdRastreamentoHBLRetirada').value = "";
            }
        }
        function flagDestinoRetirada() {
            if (document.getElementById("checkDestinoRetirada").checked) {
                document.getElementById('dtRecebimentoHBLRetirada').disabled = false;
                document.getElementById('cdRastreamentoHBLRetirada').disabled = false;
                document.getElementById('dtRetiradaRetirada').disabled = false;
                document.getElementById('dtRetiradaTerceiroRetirada').disabled = false;
                document.getElementById('receptorRetirada').disabled = false;
                document.getElementById('cdRastreamentoHBLRetirada').value = "DESTINO";
                document.getElementById("checkOrigemRetirada").checked = false;

            } else {
                document.getElementById('dtRecebimentoHBLRetirada').disabled = false;
                document.getElementById('cdRastreamentoHBLRetirada').disabled = false;
                document.getElementById('dtRetiradaRetirada').disabled = false;
                document.getElementById('dtRetiradaTerceiroRetirada').disabled = false;
                document.getElementById('receptorRetirada').disabled = false;
                document.getElementById('cdRastreamentoHBLRetirada').value = "";
            }
        }

        function flagLivre() {
            if (document.getElementById("checkLivre").checked) {
                document.getElementById('dtRecebimentoHBL').disabled = false;
                document.getElementById('cdRastreamentoHBL').disabled = false;
                document.getElementById('dtRetirada').disabled = false;
                document.getElementById('dtRetiradaTerceiro').disabled = false;
                document.getElementById('receptor').disabled = false;
                document.getElementById('cdRastreamentoHBL').value = "";
            }
        }

        function limparFiltros() {
            var dados = [
                document.getElementById("txtHBL"),
                document.getElementById("txtDtRecebMBLinicio"),
                document.getElementById("txtDtRecebMBLfim"),
                document.getElementById("txtCdRastreioMBL"),
                document.getElementById("dtRecebHBLinicio"),
                document.getElementById("dtRecebHBLfim"),
                document.getElementById("txtCdRastreioHBL"),
                document.getElementById("txtAgente"),
                document.getElementById("dtPrevisaoChegadainicio"),
                document.getElementById("dtPrevisaoChegadafim"),
                document.getElementById("dtChegadainicio"),
                document.getElementById("dtChegadafim"),
                document.getElementById("dtRetiradaPersonalInicio"),
                document.getElementById("dtRetiradaPersonalFim"),
                document.getElementById("txtFatura"),
                document.getElementById("MainContent_ddlFiltro"),
                document.getElementById("txtConsulta")
                ]
            for (let i = 0; i < dados.length; i++) {
                dados[i].value = "";
            }
            listarCourrier();
            
        }


        function listarCourrier() {
            idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            stringConsulta = document.getElementById("txtConsulta").value;
            filter_fcl = document.getElementById("MainContent_chkFCL").checked ? '1' : '0';
            filter_lcl = document.getElementById("MainContent_chkLCL").checked ? '1' : '0';
            filter_dtsaida = document.getElementById("MainContent_chkCSaida").checked ? '1' : '0';
            filter_dtsaidan = document.getElementById("MainContent_chkSSaida").checked ? '1' : '0';
            filter_branco = document.getElementById("MainContent_chkBranco").checked ? '1' : '0';
            filter_freehand = document.getElementById("MainContent_chkFreehand").checked ? '1' : '0';
            var bg = 'background-color: #ccc;';
            var checkedTroca = "";

            var dados = {
                "BLHOUSE": document.getElementById("txtHBL").value,
                "DTRECEBIMENTOMBLINICIO": document.getElementById("txtDtRecebMBLinicio").value,
                "DTRECEBIMENTOMBLFIM": document.getElementById("txtDtRecebMBLfim").value,
                "CDRASTREAMENTOMBL": document.getElementById("txtCdRastreioMBL").value,
                "DTRECEBIMENTOHBLINICIO": document.getElementById("dtRecebHBLinicio").value,
                "DTRECEBIMENTOHBLFIM": document.getElementById("dtRecebHBLfim").value,
                "CDRASTREAMENTOHBL": document.getElementById("txtCdRastreioHBL").value,
                "AGENTE": document.getElementById("txtAgente").value,
                "PREVISAOCHEGADAINICIO": document.getElementById("dtPrevisaoChegadainicio").value,
                "PREVISAOCHEGADAFIM": document.getElementById("dtPrevisaoChegadafim").value,
                "DTCHEGADAINICIO": document.getElementById("dtChegadainicio").value,
                "DTCHEGADAFIM": document.getElementById("dtChegadafim").value,
                "FATURA": document.getElementById("txtFatura").value,
                "DTRETIRADAPEROSNALINICIO": document.getElementById("dtRetiradaPersonalInicio").value,
                "DTRETIRADAPEROSNALFIM": document.getElementById("dtRetiradaPersonalFim").value,
            }
            let result = "";
            let imp = "";
            let cf = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarCourrier",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idFilter: (idFiltro), Filter: (stringConsulta), filter_fcl: (filter_fcl), filter_lcl: (filter_lcl), filter_dtsaida: (filter_dtsaida), filter_dtsaidan: (filter_dtsaidan), filter_branco: (filter_branco), filter_freehand: (filter_freehand), dados:(dados)}),
                dataType: "json",
                beforeSend: function () {
                    $("#containerCourrier").empty();
                    $("#containerCourrier").append("<tr><td colspan='18'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#containerCourrier").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["FL_TROCA"] == 1) {
                                checkedTroca = "checked='true'";
                            } else {
                                checkedTroca = "";
                            }
                            imp = (dado[i]["IMPORTADOR"] != "" ? " - IMPORTADOR: " + dado[i]["IMPORTADOR"] + " " : "");
                            cf = (dado[i]["CLIENTE_FINAL"] != "" ? " - CLIENTE FINAL: " + dado[i]["CLIENTE_FINAL"] + "" : "");
                            result += "<tr>";
                            result += "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["MASTER"] + "</td > ";
                            result += "<td class='text-center'>" + dado[i]["HOUSE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>";
                            result += "<td class='text-center' style='"+(dado[i]["CD_RASTREAMENTO_MBL"] != "" && dado[i]["CD_RASTREAMENTO_HBL"] != "" ? bg:"")+"'>" + dado[i]["CD_RASTREAMENTO_MBL"] + "</td>" ;
                            result += "<td class='text-center'> " + dado[i]["DT_RECEBIMENTO_MBL"] + "</td> ";
                            result += "<td class='text-center' title='" + dado[i]["CD_RASTREAMENTO_HBL"] + "' style='max-width: 14ch;" + bg + "'>" + dado[i]["CD_RASTREAMENTO_HBL"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_RECEBIMENTO_HBL"] + "</td>";
                            result += "<td class='text-center'></td>";
                            result += "<td class='text-center' title='" + dado[i]["AGENTE"] + "' style='max-width: 14ch;'>" + dado[i]["AGENTE"] + "</td>";
                            result += "<td class='text-center' title='" + dado[i]["CLIENTE"] + imp + cf+"' style = 'max-width: 14ch; word-break: break-all;' > " + dado[i]["CLIENTE"] + "</td > ";
                            result += "<td class='text-center'>" + dado[i]["DT_RETIRADA_PERSONAL"] + "</td>";
                            result += "<td class='text-center'>"+ dado[i]["OBS_COURRIER"] + "</td>";
                            result += "<td class='text-center'><input type='checkbox' name='' " + checkedTroca + " disabled='disable'></td>";
                            result += "<td class='text-center'>" + dado[i]["NR_FATURA_COURRIER"]+"</td>";
                            result += "<td class='text-center'>";
                            result += "<div class='btn btn-primary' onclick='BuscarCourrier(" + dado[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></div>";
                            result += dado[i]["FL_TROCA"] == 1 ? "<div class='btn btn-success' title='Troca' style='margin-left:5px;' onclick='TrocaCourrier(" + dado[i]["ID_BL"] + ")'><i class='fas fa-exchange-alt'></i></div>" : "";
                            result += dado[i]["CD_RASTREAMENTO_HBL"] != 'ORIGEM' ? "<div class='btn btn-warning' title='Disponibilidade de BL' style='margin-left: 5px;' onclick='DisponibilidadeBL(" + dado[i]["ID_BL"] + ")'><i class='far fa-file-alt'></i></div>" : "";
                            result += "<div class='btn btn-info' title='Agente Internacional' style='margin-left: 5px;' onclick='AgenteInternacional(" + dado[i]["ID_BL"] + ")'><i class='fas fa-user'></i></div>";
                            result += "</td>";
                            result += "</tr>";
                        }
                        
                        $("#containerCourrier").append(result)
                        $("table").trigger("update"); 
                    }
                    else {
                        $("#containerCourrier").empty();
                        $("#containerCourrier").append("<tr id='msgEmptyWeek'><td colspan='18' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function listarCourrierRetirada() {
            idFiltro = document.getElementById("MainContent_ddlFiltroRetirada").value;
            stringConsulta = document.getElementById("txtConsultaRetirada").value;
            tipo = document.getElementById("MainContent_chkFCLRetirada");
            var bg = 'background-color: #ccc;';
            var checked = "";
            tipoValue;
            if (tipo.checked) {
                tipoValue = "1";
            }
            else {
                tipoValue = "0";
            }

            var dados = {
                "BLHOUSE": document.getElementById("txtHBLRetirada").value,
                "AGENTE": document.getElementById("txtAgenteRetirada").value,
                "PREVISAOCHEGADAINICIO": document.getElementById("dtPrevisaoChegadainicioRetirada").value,
                "PREVISAOCHEGADAFIM": document.getElementById("dtPrevisaoChegadafimRetirada").value,
                "DTCHEGADAINICIO": document.getElementById("dtChegadainicioRetirada").value,
                "DTCHEGADAFIM": document.getElementById("dtChegadafimRetirada").value,
                "DTRETIRADAPEROSNALINICIO": document.getElementById("dtRetiradaPersonalInicioRetirada").value,
                "DTRETIRADAPEROSNALFIM": document.getElementById("dtRetiradaPersonalFimRetirada").value,
            }
            let result = "";
            let imp = "";
            let cf = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarCourrierRetirada",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idFilter: (idFiltro), Filter: (stringConsulta), tipo: (tipoValue), dados: (dados) }),
                dataType: "json",
                beforeSend: function () {
                    $("#containerCourrierRetirada").empty();
                    $("#containerCourrierRetirada").append("<tr><td colspan='18'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#containerCourrierRetirada").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["FL_TROCA"] == 1) {
                                checked = "checked='true'";
                            } else {
                                checked = "";
                            }
                            imp = (dado[i]["IMPORTADOR"] != "" ? " - IMPORTADOR: " + dado[i]["IMPORTADOR"] + " " : "");
                            cf = (dado[i]["CLIENTE_FINAL"] != "" ? " - CLIENTE FINAL: " + dado[i]["CLIENTE_FINAL"] + "" : "");
                            result += "<tr>";
                            result += "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["HOUSE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>";
                            result += "<td class='text-center' title='" + dado[i]["CLIENTE"]+ imp + cf + "' style='max-width: 14ch; word-break: break-all;'>" + dado[i]["CLIENTE"] + "</td>";
                            result += "<td class='text-center'><input type='checkbox' name='' " + checked + " disabled='disable'></td>";
                            result += "<td class='text-center'>" + dado[i]["DT_RETIRADA_COURRIER"] + "</td>";
                            result += "<td class='text-center' title='" + dado[i]["NM_RETIRADO_POR_COURRIER"] + "' style='max-width: 14ch;'>" + dado[i]["NM_RETIRADO_POR_COURRIER"] + "</td>";
                            result += "<td class='text-center'><div class='btn btn-primary' onclick='BuscarCourrierRetirada(" + dado[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></div></td>";
                            result += "</tr>";
                        }

                        $("#containerCourrierRetirada").append(result)
                        $("table").trigger("update");
                    }
                    else {
                        $("#containerCourrierRetirada").empty();
                        $("#containerCourrierRetirada").append("<tr id='msgEmptyWeek'><td colspan='18' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function listarCourrierLiberacao() {
            idFiltro = document.getElementById("MainContent_ddlFiltroLiberacao").value;
            stringConsulta = document.getElementById("txtConsultaLiberacao").value;
            tipo = document.getElementById("MainContent_chkFCLLiberacao");
            var bg = 'background-color: #ccc;';
            var checked = "";
            var checkedDigital = "";
            tipoValue;
            if (tipo.checked) {
                tipoValue = "1";
            }
            else {
                tipoValue = "0";
            }

            var dados = {
                "BLHOUSE": document.getElementById("txtHBLLiberacao").value,
                "DTRECEBIMENTOMBLINICIO": document.getElementById("dtRecebMBLInicioLiberacao").value,
                "DTRECEBIMENTOMBLFIM": document.getElementById("dtRecebMBLFimLiberacao").value,
                "CDRASTREAMENTOMBL": document.getElementById("txtCdRastreioMBLLiberacao").value,
                "AGENTE": document.getElementById("txtAgenteLiberacao").value,
                "PREVISAOCHEGADAINICIO": document.getElementById("dtPrevisaoChegadaInicioLiberacao").value,
                "PREVISAOCHEGADAFIM": document.getElementById("dtPrevisaoChegadaFimLiberacao").value,
                "DTCHEGADAINICIO": document.getElementById("dtChegadaInicioLiberacao").value,
                "DTCHEGADAFIM": document.getElementById("dtChegadaFimLiberacao").value,
            }
            let result = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarCourrierLiberacao",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idFilter: (idFiltro), Filter: (stringConsulta), tipo: (tipoValue), dados: (dados) }),
                dataType: "json",
                beforeSend: function () {
                    $("#containerCourrierLiberacao").empty();
                    $("#containerCourrierLiberacao").append("<tr><td colspan='18'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#containerCourrierLiberacao").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["FL_TROCA"] == 1) {
                                checked = "checked='true'";
                            } else {
                                checked = "";
                            }

                            if (dado[i]["HBL_DIGITAL"] == 1) {
                                checkedDigital = "checked='true'";
                            } else {
                                checkedDigital = "";
                            }

                            result += "<tr>";
                            result += "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["MASTER"] + "</td > ";
                            result += "<td class='text-center'>" + dado[i]["HOUSE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>";
                            result += "<td class='text-center' style='" + (dado[i]["CD_RASTREAMENTO_HBL"] != "" && dado[i]["CD_RASTREAMENTO_HBL"] != "" ? bg : "") + "'>" + dado[i]["CD_RASTREAMENTO_HBL"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["LOTE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["CLIENTE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_RECEBIMENTO_HBL"] + "</td>";
                            result += "<td class='text-center'><input type='checkbox' name='' " + checkedDigital + " disabled='disable'></td>";
                            result += "<td class='text-center'><input type='checkbox' name='' " + checked + " disabled='disable'></td>";
                            result += "<td class='text-center'>"+dado[i]["TERMO"]+"</td>";
                            result += "<td class='text-center'>"+(dado[i]["FL_BLOQUEIO_DOCUMENTAL"] == "1" ? "BLOQUEADO" : "DESBLOQUEADO")+"</td>";
                            result += "<td class='text-center'>" + dado[i]["OBS_COURRIER"] + "</td>";
                            result += "<td class='text-center'>";
                            result += (dado[i]["FL_BLOQUEIO_DOCUMENTAL"] == "1" ? "<div class='btn btn-success' onclick='DesbloquearDocumento(" + dado[i]['LOTE'] + "," + dado[i]["ID_BL"] +")'><i class='fas fa-lock-open'></i></div>" : "<div class='btn btn-danger' onclick='BloquearDocumento(" + dado[i]["LOTE"] +","+dado[i]["ID_BL"]+")' style='margin-left: 5px;'><i class='fas fa-lock'></i></div>");
                            result += "<div class='btn btn-primary' style='margin-left: 5px;' onclick='BuscarCourrierLiberacao(" + dado[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></div>";
                            result += "</td>";
                            result += "</tr>";
                        }

                        $("#containerCourrierLiberacao").append(result)
                        $("table").trigger("update");
                    }
                    else {
                        $("#containerCourrierLiberacao").empty();
                        $("#containerCourrierLiberacao").append("<tr id='msgEmptyWeek'><td colspan='18' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function listarCourrierConcluido() {
            idFiltro = document.getElementById("MainContent_ddlFiltroConcluido").value;
            stringConsulta = document.getElementById("txtConsultaConcluido").value;
            tipo = document.getElementById("MainContent_chkFCLConcluido");
            var bg = 'background-color: #ccc;';
            var checked = "";
            tipoValue;
            if (tipo.checked) {
                tipoValue = "1";
            }
            else {
                tipoValue = "0";
            }

            var dados = {
                "BLHOUSE": document.getElementById("txtHBLConcluido").value,
                "DTRECEBIMENTOMBLINICIO": document.getElementById("txtDtRecebMBLinicioConcluido").value,
                "DTRECEBIMENTOMBLFIM": document.getElementById("txtDtRecebMBLfimConcluido").value,
                "CDRASTREAMENTOMBL": document.getElementById("txtCdRastreioMBLConcluido").value,
                "DTRECEBIMENTOHBLINICIO": document.getElementById("dtRecebHBLinicioConcluido").value,
                "DTRECEBIMENTOHBLFIM": document.getElementById("dtRecebHBLfimConcluido").value,
                "CDRASTREAMENTOHBL": document.getElementById("txtCdRastreioHBLConcluido").value,
                "AGENTE": document.getElementById("txtAgenteConcluido").value,
                "PREVISAOCHEGADAINICIO": document.getElementById("dtPrevisaoChegadainicioConcluido").value,
                "PREVISAOCHEGADAFIM": document.getElementById("dtPrevisaoChegadafimConcluido").value,
                "DTCHEGADAINICIO": document.getElementById("dtChegadainicioConcluido").value,
                "DTCHEGADAFIM": document.getElementById("dtChegadafimConcluido").value,
                "FATURA": document.getElementById("txtFaturaConcluido").value,
                "DTRETIRADAPEROSNALINICIO": document.getElementById("dtRetiradaPersonalInicioConcluido").value,
                "DTRETIRADAPEROSNALFIM": document.getElementById("dtRetiradaPersonalFimConcluido").value,
            }
            let result = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarCourrierConcluido",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idFilter: (idFiltro), Filter: (stringConsulta), tipo: (tipoValue), dados: (dados) }),
                dataType: "json",
                beforeSend: function () {
                    $("#containerCourrierConcluido").empty();
                    $("#containerCourrierConcluido").append("<tr><td colspan='18'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#containerCourrierConcluido").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["FL_TROCA"] == 1) {
                                checked = "checked='true'";
                            } else {
                                checked = "";
                            }
                            result += "<tr>";
                            result += "<td class='text-center'><div class='btn btn-primary' onclick='BuscarCourrier(" + dado[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></div></td>";
                            result += "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["MASTER"] + "</td > ";
                            result += "<td class='text-center'>" + dado[i]["HOUSE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>";
                            result += "<td class='text-center' style='" + (dado[i]["CD_RASTREAMENTO_MBL"] != "" && dado[i]["CD_RASTREAMENTO_HBL"] != "" ? bg : "") + "'>" + dado[i]["CD_RASTREAMENTO_MBL"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["DT_RECEBIMENTO_MBL"] + "</td> ";
                            result += "<td class='text-center' title='" + dado[i]["CD_RASTREAMENTO_HBL"] + "' style='max-width: 14ch;" + bg + "'>" + dado[i]["CD_RASTREAMENTO_HBL"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_RECEBIMENTO_HBL"] + "</td>";
                            result += "<td class='text-center'></td>";
                            result += "<td class='text-center' title='" + dado[i]["AGENTE"] + "' style='max-width: 14ch;'>" + dado[i]["AGENTE"] + "</td>";
                            result += "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 14ch; word-break: break-all;'>" + dado[i]["CLIENTE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DT_RETIRADA_PERSONAL"] + "</td>";
                            result += "<td class='text-center' title='" + dado[i]["NM_RETIRADO_POR_COURRIER"] + "' style='max-width: 14ch;'>" + dado[i]["NM_RETIRADO_POR_COURRIER"] + "</td>";
                            result += "<td class='text-center' title='" + dado[i]["OBS_COURRIER"] + "' style='max-width: 14ch;'>" + dado[i]["OBS_COURRIER"] + "</td>";
                            result += "<td class='text-center'><input type='checkbox' name='' " + checked + " disabled='disable'></td>";
                            result += "<td class='text-center'>" + dado[i]["FATURA"] + "</td>";
                            result += "<td class='text-center'><input type='checkbox' name='' " + checked + " disabled='disable'></td>";
                            result += "<td class='text-center'>" + dado[i]["AGENTE"] + "</td>";
                            result += "</tr>";
                        }

                        $("#containerCourrierConcluido").append(result)
                        $("table").trigger("update");
                    }
                    else {
                        $("#containerCourrierConcluido").empty();
                        $("#containerCourrierConcluido").append("<tr id='msgEmptyWeek'><td colspan='18' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        $("#btnEditarCourrier").click(function () {
            $("#modalEditCourrier").modal("hide");
            idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            stringConsulta = document.getElementById("txtConsulta").value;
            tipo = document.getElementById("MainContent_chkFCL");
            tipoValue;
            var troca = document.getElementById("checkTroca");
            if (tipo.checked) {
                tipoValue = "1";
            }
            else {
                tipoValue = "0";
            }

            if (troca.checked) {
                troca = "1";
            } else {
                troca = "0";
            }
            var dadoEdit = {
                "ID_BL": idblC,
                "NR_BL_MASTER": document.getElementById("idmbl").value,
                "NR_BL": document.getElementById('idhbl').value,
                "DT_RECEBIMENTO_MBL": document.getElementById('dtRecebimentoMBL').value,
                "CD_RASTREAMENTO_MBL": document.getElementById('cdRastreamentoMBL').value,
                "DT_RECEBIMENTO_HBL": document.getElementById('dtRecebimentoHBL').value,
                "CD_RASTREAMENTO_HBL": document.getElementById('cdRastreamentoHBL').value,
                "NR_FATURA_COURRIER": document.getElementById('nrFatura').value,
                "DT_RETIRADA_PERSONAL": document.getElementById('dtRetiradaTerceiro').value,
                "FL_TROCA": troca,
                "OBS_COURRIER": document.getElementById('dsObservacao').value
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/editarCourrier",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "1") {
                        Swal.fire('Sucesso', '', 'success')
                        listarCourrier();
                    }
                    else {
                        Swal.fire('Erro', '', 'warning')
                        listarCourrier();
                    }
                }
            })
        })

        $("#btnEditarCourrierRetirada").click(function () {
            Swal.fire({
                title: 'Você tem certeza?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $("#modalEditCourrierRetirada").modal("hide");
                    idFiltro = document.getElementById("MainContent_ddlFiltroRetirada").value;
                    stringConsulta = document.getElementById("txtConsultaRetirada").value;
                    /*tipo = document.getElementById("MainContent_chkFCLRetirada");*/
                    tipoValue;
                    /*var troca = document.getElementById("checkTrocaRetirada");
                    if (tipo.checked) {
                        tipoValue = "1";
                    }
                    else {
                        tipoValue = "0";
                    }

                    if (troca.checked) {
                        troca = "1";
                    } else {
                        troca = "0";
                    }*/
                    var dadoEdit = {
                        "ID_BL": idblC,
                        "NR_BL_MASTER": document.getElementById("idmblRetirada").value,
                        "NR_BL": document.getElementById('idhblRetirada').value,
                        "DT_RETIRADA_COURRIER": document.getElementById('dtRetiradaTerceiroRetirada').value,
                        "NM_RETIRADO_POR_COURRIER": document.getElementById('nmRetiradoPorRetirada').value,
                        "OBS_COURRIER": document.getElementById('dsObservacaoRetirada').value
                        /*"FL_TROCA": troca,*/
                    }
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/editarCourrierRetirada",
                        data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado == "1") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrierRetirada();
                            }
                            else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrierRetirada();
                            }
                        }
                    })
                }
            })
        })

        $("#btnEditarCourrierliberacao").click(function () {
            Swal.fire({
                title: 'Você tem certeza?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $("#modalEditCourrierLiberacao").modal("hide");
                    idFiltro = document.getElementById("MainContent_ddlFiltroLiberacao").value;
                    stringConsulta = document.getElementById("txtConsultaLiberacao").value;
                    /*tipo = document.getElementById("MainContent_chkFCLLiberacao");*/
                    tipoValue;
                    /*var troca = document.getElementById("checkTrocaLiberacao");
                    if (tipo.checked) {
                        tipoValue = "1";
                    }
                    else {
                        tipoValue = "0";
                    }

                    if (troca.checked) {
                        troca = "1";
                    } else {
                        troca = "0";
                    }*/
                    var dadoEdit = {
                        "ID_BL": idblC,
                        "NR_BL_MASTER": document.getElementById("idmblliberacao").value,
                        "NR_BL": document.getElementById('idhblliberacao').value,
                        "OBS_COURRIER": document.getElementById('dsObservacaoliberacao').value
                        /*"FL_TROCA": troca,*/
                    }
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/editarCourrierLiberacao",
                        data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado == "1") {
                                Swal.fire('Sucesso', '', 'success')
                                listarCourrierLiberacao();
                            }
                            else {
                                Swal.fire('Erro', '', 'warning')
                                listarCourrierLiberacao();
                            }
                        }
                    })
                }
            })
        })

        function downloadCSVImport(csv, filename) {
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

        function GerarCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#courrierExport tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#courrierExport td, #courrierExport th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVImport(csv.join("\n"), filename);
        }
    </script>
</asp:Content>
