<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuloOperacional.aspx.cs" Inherits="ABAINFRA.Web.ModuloOperacional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Módulo Operacional
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Módulo Operacional
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="alert alert-success text-center" id="msgSuccessDemu">
                                Registro cadastrado/atualizado com sucesso!
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrSelect">
                                Selecione um Processo
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrDemu">
                                Erro ao atualizar.
                            </div>
                            <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                <div style="margin: auto;">
                                    <button type="button" id="btnVincularWeek" class="btn btn-primary" onclick="dadosProcesso()">Editar Dados</button>                    
                                    <button type="button" id="btnUploadArquivo" class="btn btn-primary" onclick="dadosUpload()">Upload de Arquivos</button>
                                    <button type="button" id="btnGerarEmail" class="btn btn-primary" onclick="montagemEmail()">Montagem de E-mail MBL</button>  
                                    <button type="button" id="btnCaixaSaida" class="btn btn-primary" onclick="caixaSaida()">Caixa de Saída</button> 
                                    <button type="button" id="btnGerarCSV" class="btn btn-primary" onclick="exportTableToCSVAtual('ModuloOperacional.csv')">Gerar CSV</button>  
                                </div>
                            </div>
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Via</></label>
                                        <asp:DropDownList ID="ddlVia" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Etapa</label>
                                        <asp:DropDownList ID="ddlEtapa" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label">Serviço</label>
                                        <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control"></asp:DropDownList>  
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label">Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <button type="button" id="btnConsulta" onclick="listarProcessosOperacional()" class="btn btn-primary">Consultar</button>
                                    <button type="button" id="btnFiltroAvancado" class="btn btn-primary" data-toggle="modal" data-target="#modalFiltroAvancado">Filtro Avançado</button>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="tblModuloOperacional" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col"></th>
                                            <th class="text-center" scope="col">PROCESSO</th>
                                            <th class="text-center" scope="col">CLIENTE</th>
                                            <th class="text-center" scope="col">ORIGEM</th>
                                            <th class="text-center" scope="col">DESTINO</th>
                                            <th class="text-center" scope="col">TIPO FRETE</th>
                                            <th class="text-center" scope="col">TIPO ESTUFAGEM</th>
                                            <th class="text-center" scope="col">AGENTE</th>
                                            <th class="text-center" scope="col">PREVISÃO EMBARQUE</th>
                                            <th class="text-center" scope="col">DATA EMBARQUE</th>
                                            <th class="text-center" scope="col">PREVISÃO CHEGADA</th>
                                            <th class="text-center" scope="col">DATA CHEGADA</th>
                                            <th class="text-center" scope="col">FREE TIME</th>
                                            <th class="text-center" scope="col">TRANSPORTADOR</th>
                                            <th class="text-center" scope="col">BL MASTER</th>
                                            <th class="text-center" scope="col">BL HOUSE</th>
                                            <th class="text-center" scope="col">CE MASTER</th>
                                            <th class="text-center" scope="col">CE HOUSE</th>
                                            <th class="text-center" scope="col">DATA REDESTINAÇÃO</th>
                                            <th class="text-center" scope="col">DATA DESCONSOLIDAÇÃO</th>
                                            <th class="text-center" scope="col">WEEK</th>
                                            <th class="text-center" scope="col">NAVIO</th>
                                            <th class="text-center" scope="col">TERMO</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblModuloOperacionalBody">
                                       
                                    </tbody>
                                </table>
                            </div>
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
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Processo</label>
                                                    <input id="nrProcessoFilter" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Cliente</label>
                                                    <asp:DropDownList ID="ddlClienteFilter" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Porto Origem</label>
                                                    <asp:DropDownList ID="ddlPortoOrigemFilter" runat="server" CssClass="form-control" DataTextField="NM_PORTO" DataValueField="ID_PORTO"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Porto Destino</label>
                                                    <asp:DropDownList ID="ddlPortoDestinoFilter" runat="server" CssClass="form-control" DataTextField="NM_PORTO" DataValueField="ID_PORTO"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Tipo Frete</label>
                                                    <asp:DropDownList ID="ddlTipoFrete" runat="server" CssClass="form-control" DataTextField="NM_TIPO_PAGAMENTO" DataValueField="ID_TIPO_PAGAMENTO"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Tipo Estufagem</label>
                                                    <asp:DropDownList ID="ddlTipoEstufagem" runat="server" CssClass="form-control" DataTextField="NM_TIPO_ESTUFAGEM" DataValueField="ID_TIPO_ESTUFAGEM"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Agente</label>
                                                    <asp:DropDownList ID="ddlAgenteFilter" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Previsão de Embarque Inicio</label>
                                                    <input id="dtPrevisaoEmbarqueInicioFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Previsão de Embarque Fim</label>
                                                    <input id="dtPrevisaoEmbarqueFimFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Data de Embarque Inicio</label>
                                                    <input id="dtEmbarqueInicioFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Data de Embarque Fim</label>
                                                    <input id="dtEmbarqueFimFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Previsão de Chegada Inicio</label>
                                                    <input id="dtPrevisaoChegadaInicioFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Previsão de Chegada Fim</label>
                                                    <input id="dtPrevisaoChegadaFimFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Data de Chegada Inicio</label>
                                                    <input id="dtChegadaInicioFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Data de Chegada Fim</label>
                                                    <input id="dtChegadaFimFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Free Time</label>
                                                    <input id="dtFreetimeFilter" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Transportador</label>
                                                    <asp:DropDownList ID="ddlTransportadorFilter" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">BL Master</label>
                                                    <input id="nrMasterFilter" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">BL House</label>
                                                    <input id="nrHouseFilter" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">CE Master</label>
                                                    <input id="nrCeMasterFilter" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">CE House</label>
                                                    <input id="nrCeHouseFilter" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Data Redestinação Inicio</label>
                                                    <input id="dtRedestinacaoInicioFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Data Redestinação Fim</label>
                                                    <input id="dtRedestinacaoFimFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Data Desconsolidação Inicio</label>
                                                    <input id="dtDesconsolidacaoInicioFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Data Desconsolidação Fim</label>
                                                    <input id="dtDesconsolidacaoFimFilter" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Week</label>
                                                    <asp:DropDownList ID="ddlWeekFilter" runat="server" CssClass="form-control" DataTextField="NM_WEEK" DataValueField="ID_WEEK"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Navio</label>
                                                    <asp:DropDownList ID="ddlNavioFilter" runat="server" CssClass="form-control" DataTextField="NM_NAVIO" DataValueField="ID_NAVIO"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnConsultarFilter" onclick="listarProcessosOperacional()" data-dismiss="modal" class="btn btn-primary btn-ok">Consultar</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade" id="modalUploadArquivo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalDeleteDemurrageTitle"> E-MAILS AUTOMÁTICOS <span style="color:darkred;" id="nrProcessoMaster"></span></h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-6 col-sm-offset-3 text-center">
                                                <label style="font-size: 18px">NR MASTER</label>
                                                <input type="text" id="nrMasterBL" class="form-control nobox" disabled="disabled" style="text-align:center;font-size: 20px"/>
                                            </div>
                                        </div>
                                        <div class="row topMarg">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label class="control-label">TIPO DE AVISO</label>
                                                    <select id="ddlTipoAviso" class="form-control" onchange="listDocumento()">
                                                        <option value="">SELECIONE</option>
                                                        <option value="1">DESCONSOLIDAÇÃO</option>
                                                        <option value="2">PREVISÃO DE CHEGADA AÉREO (PRÉ-ALERTA)</option>
                                                        <option value="3">REDESTINAÇÃO CONSOLIDADA</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label">DOCUMENTO</label>
                                                    <select id="ddlDocumento" class="form-control"></select>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row topMarg">
                                            <input style="margin:auto;" type="file" name="file" id="file"/>
                                            <div style="display: flex; align-items: center;" class="topMarg">
                                                <button style="margin:auto;" type="button" id="btnUpload" class="btn btn-primary btn-ok">EXCECUTAR UPLOAD</button>
                                            </div>
                                        </div>

                                        <label class="topMarg">DOCUMENTO ARQUIVADOS</label>
                                        <div class="table-responsive tableFixHead topMarg">
                                            <table id="tblUploadArquivo" class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">DATA POSTAGEM</th>
                                                        <th class="text-center" scope="col">NOME DOCUMENTO</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblUploadArquivoBody">
                                       
                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="padding: 10px" class="row topMarg">
                                            <div style="display: flex; align-items: center;">
                                                <button style="margin:auto;" type="button" id="btnExcluir" class="btn btn-primary btn-ok">EXCLUIR</button>
                                                <button style="margin:auto;" type="button" id="btnCaixasaida" class="btn btn-primary btn-ok">Ver Caixa de Saída</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade" id="modalVincularWeek" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalDemurrageTitle">Editar Dados <span id="nrProcessoWeek"></span></h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Data Redestinação</label>
                                                    <input id="dtRedestinacao" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Data Desconsolidação</label>
                                                    <input id="dtDesconsolidacao" class="form-control" type="date"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Week</label>
                                                    <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control" DataTextField="NM_WEEK" DataValueField="ID_WEEK"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label class="control-label">Termo</label>
                                                    <input id="dsTermo" class="form-control" type="text"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnVincular" onclick="vincularWeek()" data-dismiss="modal" class="btn btn-primary btn-ok">Editar</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade bd-example-modal-xl" id="modalMontagemEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalFaturaTitle">Montagem E-mail</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-6 text-center" style="display: flex;">
                                                <span style="font-size: 18px; font-weight: bold;">MBL: </span>
                                                <span id="nrMasterBLemail" style="text-align:center;font-size: 18px"> </span>
                                            </div>
                                        </div>
                                        <div style="width:100%; display: flex;">
                                            <div>
                                                <div class="table-responsive tableFixHead topMarg" style="height: 300px;">
                                                    <table id="tblListaProcessoHouse" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col"></th>
                                                                <th class="text-center" scope="col">PROCESSO</th>
                                                                <th class="text-center" scope="col">CLIENTE</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblListaProcessoHouseBody">
                                                            
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <button style="margin-left: 40px" type="button" id="" onclick="" data-dismiss="modal" class="btn btn-primary btn-ok">Excluir Processo Selecionado</button>
                                            </div>
                                            <div id="caixaEmail" class="caixaEmail" style=" margin-left:20px">
                                                <p id="assuntoEmail"></p>
                                                <textarea id="corpoEmail"></textarea>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="" onclick="" data-dismiss="modal" class="btn btn-primary btn-ok">Enviar</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade bd-example-modal-xl" id="modalCaixaSaida" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalEditContInfoTitle">Caixa de Saída</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnEditarInfoCont" onclick="atualizarContainer()" data-dismiss="modal" class="btn btn-primary btn-ok">Atualizar</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
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
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script>

        $(document).ready(function () {
            listarProcessosOperacional();
        });        

        var id = 0;

        function listarProcessosOperacional() {
            var dadosFiltro = {
                "via": document.getElementById("MainContent_ddlVia").value,
                "etapa": document.getElementById("MainContent_ddlEtapa").value,
                "servico": document.getElementById("MainContent_ddlServico").value,
                "status": document.getElementById("MainContent_ddlStatus").value,
                "processo": document.getElementById("nrProcessoFilter").value,
                "cliente": document.getElementById("MainContent_ddlClienteFilter").value,
                "origem": document.getElementById("MainContent_ddlPortoOrigemFilter").value,
                "destino": document.getElementById("MainContent_ddlPortoDestinoFilter").value,
                "frete": document.getElementById("MainContent_ddlTipoFrete").value,
                "estufagem": document.getElementById("MainContent_ddlTipoEstufagem").value,
                "agente": document.getElementById("MainContent_ddlAgenteFilter").value,
                "pembarqueinicio": document.getElementById("dtPrevisaoEmbarqueInicioFilter").value,
                "pembarquefim": document.getElementById("dtPrevisaoEmbarqueFimFilter").value,
                "dtembarqueinicio": document.getElementById("dtEmbarqueInicioFilter").value,
                "dtembarquefim": document.getElementById("dtEmbarqueFimFilter").value,
                "pchegadainicio": document.getElementById("dtPrevisaoChegadaInicioFilter").value,
                "pchegadafim": document.getElementById("dtPrevisaoChegadaFimFilter").value,
                "dtchegadainicio": document.getElementById("dtPrevisaoChegadaInicioFilter").value,
                "dtchegadafim": document.getElementById("dtPrevisaoChegadaFimFilter").value,
                "freetime": document.getElementById("dtFreetimeFilter").value,
                "transportador": document.getElementById("MainContent_ddlTransportadorFilter").value,
                "blmaster": document.getElementById("nrMasterFilter").value,
                "blhouse": document.getElementById("nrHouseFilter").value,
                "cemaster": document.getElementById("nrCeMasterFilter").value,
                "cehouse": document.getElementById("nrCeHouseFilter").value,
                "dtredestinacaoinicio": document.getElementById("dtRedestinacaoInicioFilter").value,
                "dtredestinacaofim": document.getElementById("dtRedestinacaoFimFilter").value,
                "dtdesconsolidacaoinicio": document.getElementById("dtDesconsolidacaoInicioFilter").value,
                "dtdesconsolidacaofim": document.getElementById("dtDesconsolidacaoFimFilter").value,
                "week": document.getElementById("MainContent_ddlWeekFilter").value,
                "navio": document.getElementById("MainContent_ddlNavioFilter").value,
            }
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarProcessosOperacional",
                data: JSON.stringify({ dados: (dadosFiltro) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblModuloOperacionalBody").empty();
                    $("#tblModuloOperacionalBody").append("<tr><td colspan='23'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#tblModuloOperacionalBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblModuloOperacionalBody").append("<tr data-id='" + dado[i]["HOUSE"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["HOUSE"] + ")'>Selecionar</div></td><td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"]+"' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TPAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["TESTUFAGEM"] + "</td><td class='text-center'>" + dado[i]["AGENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PEMBARQUE"] + "</td><td class='text-center'>" + dado[i]["EMBARQUE"] + "</td><td class='text-center'>" + dado[i]["PCHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CHEGADA"] + "</td><td class='text-center'></td><td class='text-center' title='" + dado[i]["TRANSPORTADOR"] + "' style='max-width: 8ch;'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["BLMASTER"] + "</td > <td class='text-center'>" + dado[i]["BLHOUSE"] + "</td><td class='text-center'>" + dado[i]["CEMASTER"] + "</td><td class='text-center'>" + dado[i]["CEHOUSE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["REDESTINACAO"] + "</td><td class='text-center'>" + dado[i]["DESCONSOLIDACAO"] + "</td><td class='text-center'>" + dado[i]["WEEK"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NAVIO"] +"' style='max-width: 10ch;'>" + dado[i]["NAVIO"] + "</td><td class='text-center' title='" + dado[i]["TERMO"] + "' style='max-width: 25ch;'>" + dado[i]["TERMO"] + "</td></tr> ");

                        }
                    }

                    else {
                        $("#tblModuloOperacionalBody").empty();
                        $("#tblModuloOperacionalBody").append("<tr id='msgEmptyWeek'><td colspan='23' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function dadosProcesso() {
            if (id != 0) {
                $("#modalVincularWeek").modal("show");

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/NumeroProcesso",
                    data: '{idProcesso:"' + id + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.getElementById("nrProcessoWeek").textContent = dado[0]["NR_PROCESSO"];
                            document.getElementById("dtRedestinacao").value = dado[0]["DT_REDESTINACAO"];
                            document.getElementById("dtDesconsolidacao").value = dado[0]["DT_DESCONSOLIDACAO"];
                            document.getElementById("MainContent_ddlWeek").value = dado[0]["ID_WEEK"];
                            document.getElementById("dsTermo").value = dado[0]["TERMO"];
                        }
                    }
                })
            } else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function vincularWeek() {
            var week = document.getElementById("MainContent_ddlWeek").value;
            var redestinacao = document.getElementById("dtRedestinacao").value;
            var desconsolidacao = document.getElementById("dtDesconsolidacao").value;
            var termo = document.getElementById("dsTermo").value;
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/inserirDados",
                data: '{idProcesso:"' + id + '",week:"' + week + '",dtRedestinacao: "' + redestinacao + '", dtDesconsolidacao: "' + desconsolidacao + '",termo: "' + termo +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == null) {
                        $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                        listarProcessosOperacional();
                        id = 0;
                    } else{
                        $("#msgErrDemu").fadeIn(500).delay(1000).fadeOut(500);
                        listarProcessosOperacional();
                        id = 0;
                    }
                }
            })
        }

        function dadosUpload() {
            if (id != 0) {
                $("#modalUploadArquivo").modal("show");
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/dadosUpload",
                    data: '{idProcesso:"' + id + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.querySelector("#nrMasterBL").value = dado[0]["NRMASTER"];
                        }
                    }
                })
            }
            else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function listDocumento() {
            var aviso = document.getElementById("ddlTipoAviso").value;
            var opt = document.createElement('option');
            var documento = document.getElementById("ddlDocumento");
            $("#ddlDocumento").empty();
            if (aviso == 1) {
                opt.appendChild(document.createTextNode('DOCUMENTOS PARA DESCONSOLIDAÇÃO'));
                opt.value = '4';
                documento.appendChild(opt);
            } else if (aviso == 2) {
                opt.appendChild(document.createTextNode('DOCUMENTOS DO PRÉ-ALERTA AÉREO'));
                opt.value = '5';
                documento.appendChild(opt);
            }
            else if (aviso == 3) {
                opt.appendChild(document.createTextNode('DOCUMENTOS PARA REDESTINAÇÃO CONSOLIDADA'));
                opt.value = '6';
                documento.appendChild(opt);
            }
        }
        var tipoaviso = document.getElementById("ddlTipoAviso").value;

        function uploadArquivo() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/verificarProcesso",
                data: '{idProcesso:"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        
                    }
                }
            })
        }

        function caixaSaida() {
            $("#modalCaixaSaida").modal("show");

        }

        function montagemEmail() {
            if (id != 0) {
                $("#modalMontagemEmail").modal("show");
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/dadosUpload",
                    data: '{idProcesso:"' + id + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            document.querySelector("#nrMasterBLemail").textContent = dado[0]["NRMASTER"];
                            $.ajax({
                                type: "POST",
                                url: "Gerencial.asmx/listarProcessosEmail",
                                data: '{idProcessoMaster:"' + dado[0]["NRMASTER"] + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        $("#tblListaProcessoHouseBody").empty();
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#tblListaProcessoHouseBody").append("<tr data-id='" + dado[i]["HOUSE"] + "'><td class='text-center'>" +
                                                "<div class='btn btn-primary select' onclick='setIdListaEmail(" + dado[i]["HOUSE"] + ")'>O</div></td >" +
                                                "<td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                                "<td class='text-center' title='" + dado[i]["CLIENTE"]+ "' style='max-width: 25ch;'>" + dado[i]["CLIENTE"] + "</td></tr>");
                                        }
                                    }
                                }
                            })
                            $.ajax({
                                type: "POST",
                                url: "Gerencial.asmx/escreverEmail",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        
                                    }
                                }
                            })
                        }
                    }
                })
            } else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function escreverEmail(id) {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/escreverAssuntoEmail",
                data: '{idProcesso:"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#assuntoEmail").textContent = "" + dado[0]["PROCESSO + ""
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

        function setIdListaEmail(Id) {
            id = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
            escreverEmail(id);
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
            var rows = document.querySelectorAll("#tblModuloOperacional tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#tblModuloOperacional td, #tblModuloOperacional th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVAtual(csv.join("\n"), filename);
        }
    </script>
</asp:Content>
