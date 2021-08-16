<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuloOperacional.aspx.cs" Inherits="ABAINFRA.Web.ModuloOperacional" ValidateRequest="false" %>
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
                                Email enviado com sucesso.
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
                                    <button type="button" id="btnRemoverFiltros" class="btn btn-primary" onclick="limparFiltros()">Limpar Filtros</button>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="tblModuloOperacional" class="table tablecont">
                                    <thead id="tblModuloOperacionalHead">
                                        <tr>
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
                                        <div class="alert alert-success text-center" id="msgSuccessUploadArquivo">
                                            Upload finalizado com sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrUploadArquivo">
                                            Erro ao realizar Upload arquivo.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrDiretorio">
                                            Diretório não encontrado.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrAnexo">
                                            Não foi possível copiar o arquivo. O Diretório C:\UPLOADS não foi encontrado.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessDelete">
                                            Documento excluído com sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrDelete">
                                            Não foi possível excluir o documento.
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-sm-offset-3 text-center">
                                                <label id="titleUpload" style="font-size: 18px">MASTER BL</label>
                                                <input type="text" id="nrMasterBL" class="form-control nobox" disabled="disabled" style="text-align:center;font-size: 20px"/>
                                            </div>
                                        </div>
                                        <div class="row topMarg">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label class="control-label">TIPO DE AVISO</label>
                                                    <select id="ddlTipoAviso" class="form-control" onchange="listarTipoDocumento()">
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
                                            <input style="margin:auto;" type="file" name="file" id="dadoUpload"/>
                                            <div style="display: flex; align-items: center;" class="topMarg">
                                                <button style="margin:auto;" type="button" onclick="checarDiretorio()" id="btnUpload" class="btn btn-primary btn-ok">EXCECUTAR UPLOAD</button>
                                                <button style="margin:auto; display: none" type="button" onclick="uploadCaminhoArquivo()" id="btnUploadx" class="btn btn-primary btn-ok">ENVIAR ARQUIVO</button>
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
                                                <button style="margin:auto;" type="button" id="btnExcluir" data-toggle="modal" data-target="#modalDeleteDocumentoArquivado" class="btn btn-primary btn-ok">EXCLUIR</button>
                                                <button style="margin:auto;" type="button" id="btnCaixasaida" onclick="caixaSaida()" class="btn btn-primary btn-ok">Ver Caixa de Saída</button>
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
                                                
                                            </div>
                                            <div id="caixaEmail" class="caixaEmail" style="padding: 0px 10px;">
                                                <div class="row" style="margin-left: 2px;">
                                                    <div class="form-group">
                                                        <label class="control-label">Mensagem</label>
                                                        <textarea id="corpoEmail" class="form-control" rows="17" style="resize:none"></textarea>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnEnviarEmail" onclick="enviarEmail()" class="btn btn-primary btn-ok">Enviar</button>
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
                                        <div class="alert alert-danger text-center" id="msgErrRemoveEmail">
                                            Somente emails não enviado podem ser removidos.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessRemoveEmail">
                                            Email removido com sucesso.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrReenvioEmail">
                                            Esse email não pode ser reenviado.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessReenvioEmail">
                                            Email reenviado com sucesso.
                                        </div>
                                        <div class="row" style="display: flex;margin:auto">
                                            <div style="margin:auto">
                                                <button type="button" id="btnVisualizarEmail" onclick="verificarEmail()" class="btn btn-primary btn-ok">Visualizar E-mail</button>
                                                <button type="button" id="btnRemoverEmail" onclick="verificarRemocaoEmail()" class="btn btn-primary btn-ok">Remover E-mail selecionado</button>
                                                <button type="button" id="btnReenviarEmail" onclick="verificarReenvioEmail()" class="btn btn-primary btn-ok">Reenviar E-mail</button>
                                                <button type="button" id="btnCaixaAgendamento" onclick="listarAgendamento()" class="btn btn-primary btn-ok">Caixa de Agendamento</button>
                                            </div>
                                        </div>
                                        <div class="row topMarg" style="display:flex; align-items: flex-end;">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Consultar por:<span class="required">*</span></label>
                                                    <select class="form-control" id="ddlFiltroCaixaSaida">
                                                        <option value="">Selecione</option>
                                                        <option value="1">PROCESSO</option>
                                                        <option value="2">CLIENTE</option>
                                                        <option value="3">TIPO EMAIL</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label"><span class="required">&nbsp</span></label>
                                                    <input id="txtConsulta" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div>
                                                <input type="checkbox" id="enviado" name="email_situacao" checked>
                                                <label for="enviado">A Enviar</label><br>
                                                <input type="checkbox" id="todos" name="email_situacao">
                                                <label for="todos">Enviados</label><br>
                                            </div>
                                             <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label for="ddlGerados">GERADOS</label><br>
                                                    <select class="form-control" id="ddlGerados">
                                                        <option value="1">HOJE</option>
                                                        <option value="2">7 DIAS</option>
                                                        <option value="3">30 DIAS</option>
                                                        <option value="4">60 DIAS</option>
                                                        <option value="5">90 DIAS</option>
                                                    </select>
                                                </div>
                                            </div>
                                     
                                            <div class="form-group">
                                                <button type="button" id="btnFiltrarCaixaSaida" onclick="caixaSaida()" class="btn btn-primary btn-ok">Filtrar</button>
                                            </div>
                                        </div>
                                        <div class="table-responsive tableFixHead topMarg" style="height: 300px;">
                                            <table id="tblCaixaSaida" class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">PROCESSO</th>
                                                        <th class="text-center" scope="col">TIPO E-MAIL</th>
                                                        <th class="text-center" scope="col">DATA GERAÇÃO</th>
                                                        <th class="text-center" scope="col">PREVISÃO ENVIO</th>
                                                        <th class="text-center" scope="col">DATA ENVIO</th>
                                                        <th class="text-center" scope="col">OCORRÊNCIA</th>
                                                        <th class="text-center" scope="col">NOME CLIENTE</th>
                                                        <th class="text-center" scope="col">TERMINAL</th>
                                                        <th class="text-center" scope="col">PARCEIRO</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblCaixaSaidaBody">
                                                            
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        

                        <div class="modal fade bd-example-modal-xl" id="modalVisualizarEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalDevolucaoTitle">Visualização de E-mail</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body" id="visualizarEmail" style="padding:30px;">
                                        
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade bd-example-modal-xl" id="modalCaixaAgendamento" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalCaixaAgendamentoTitle">Caixa de Agendamento</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="alert alert-success text-center" id="msgSuccessCancelEmail">
                                            Agendamento cancelado com sucesso
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrSelectEmail">
                                            Selecione um Email.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrorCancelEmail">
                                            Falha ao cancelar agendamento
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessAgendEmail">
                                            Agendamento Reativado com sucesso.
                                        </div>
                                        <div class="row" style="display: flex;margin:auto">
                                            <div style="margin:auto">
                                                <button type="button" id="btnCancelarAgendamento" onclick="cancelarAgendamento()" class="btn btn-primary btn-ok">Cancelar E-mail Agendado</button>                                                
                                            </div>
                                        </div>
                                        <div class="row topMarg" style="display:flex; align-items: flex-end;">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Consultar por:<span class="required">*</span></label>
                                                    <select class="form-control" id="ddlFiltroCaixaAgendamento">
                                                        <option value="">Selecione</option>
                                                        <option value="1">PROCESSO</option>
                                                        <option value="2">CLIENTE</option>
                                                        <option value="3">TIPO EMAIL</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label"><span class="required">&nbsp</span></label>
                                                    <input id="txtConsultaAgendamento" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div>
                                                <input type="checkbox" id="enviadoAg" name="email_situacao" checked>
                                                <label for="enviado">A Enviar</label><br>
                                                <input type="checkbox" id="todosAg" name="email_situacao">
                                                <label for="todos">Enviados</label><br>
                                            </div>
                                             <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label for="ddlGerados">GERADOS</label><br>
                                                    <select class="form-control" id="ddlGeradosAg">
                                                        <option value="1">HOJE</option>
                                                        <option value="2">7 DIAS</option>
                                                        <option value="3">30 DIAS</option>
                                                        <option value="4">90 DIAS</option>
                                                        <option value="5">180 DIAS</option>
                                                    </select>
                                                </div>
                                            </div>
                                     
                                            <div class="form-group">
                                                <button type="button" id="btnFiltrarCaixaAgendamento" onclick="listarAgendamento()" class="btn btn-primary btn-ok">Filtrar</button>
                                            </div>
                                        </div>
                                        <div class="table-responsive tableFixHead topMarg" style="height: 300px;">
                                            <table id="tblCaixaAgendamento" class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">PROCESSO</th>
                                                        <th class="text-center" scope="col">MBL</th>
                                                        <th class="text-center" scope="col">TIPO E-MAIL</th>
                                                        <th class="text-center" scope="col">DATA GERAÇÃO</th>
                                                        <th class="text-center" scope="col">PREVISÃO ENVIO</th>
                                                        <th class="text-center" scope="col">DATA ENVIO</th>
                                                        <th class="text-center" scope="col">DATA CANCELAMENTO</th>
                                                        <th class="text-center" scope="col">MOTIVO CANCELAMENTO</th>
                                                        <th class="text-center" scope="col">CLIENTE</th>
                                                        <th class="text-center" scope="col">TERMINAL</th>
                                                        <th class="text-center" scope="col">PARCEIRO</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblCaixaAgendamentoBody">
                                                            
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalCancelamentoEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalCancelamentoEmailTitle">Cancelar Agendamento</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgCancelamento">Deseja Cancelar esse agendamento?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnExcluirS" onclick="cancelarAgen()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalReativarEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalReativarEmailTitle">Reativar Agendamento</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgReativar">Deseja Reativar esse agendamento?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnReativarS" onclick="reatviarAgend()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalRemoverEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalRemoverEmailTitle">Remover Email</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgRemover">Deseja Remover esse email?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnRemoverS" onclick="removerEmail()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalReenvioEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalReenvioEmailTitle">Reenviar Email</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgReenvio">Deseja Reenviar esse email?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnReenvioS" onclick="reenviarEmail()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalDeleteDocumentoArquivado" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalDeleteDocumentoArquivadoTitle">Deletar Documento Arquivado</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgRemoverDocumento">Deseja deleterar esse documento?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnRemoverSim" data-dismiss="modal" onclick="deletarDocumentoArquivado()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
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
        var idEmailCaixa = 0;
        var idEmailAgendamento = 0;
        var documentoArquivado = 0;
        var dadoUploadX = document.querySelector("#dadoUpload");
        var btnUpload = document.querySelector("#btnUpload");
        var btnUploadx = document.querySelector("#btnUploadx");

        function limparFiltros() {
            var dadosFiltro = [
                document.getElementById("MainContent_ddlVia"),
                document.getElementById("MainContent_ddlEtapa"),
                document.getElementById("MainContent_ddlServico"),
                document.getElementById("MainContent_ddlStatus"),
                document.getElementById("nrProcessoFilter"),
                document.getElementById("MainContent_ddlClienteFilter"),
                document.getElementById("MainContent_ddlPortoOrigemFilter"),
                document.getElementById("MainContent_ddlPortoDestinoFilter"),
                document.getElementById("MainContent_ddlTipoFrete"),
                document.getElementById("MainContent_ddlTipoEstufagem"),
                document.getElementById("MainContent_ddlAgenteFilter"),
                document.getElementById("dtPrevisaoEmbarqueInicioFilter"),
                document.getElementById("dtPrevisaoEmbarqueFimFilter"),
                document.getElementById("dtEmbarqueInicioFilter"),
                document.getElementById("dtEmbarqueFimFilter"),
                document.getElementById("dtPrevisaoChegadaInicioFilter"),
                document.getElementById("dtPrevisaoChegadaFimFilter"),
                document.getElementById("dtPrevisaoChegadaInicioFilter"),
                document.getElementById("dtPrevisaoChegadaFimFilter"),
                document.getElementById("dtFreetimeFilter"),
                document.getElementById("MainContent_ddlTransportadorFilter"),
                document.getElementById("nrMasterFilter"),
                document.getElementById("nrHouseFilter"),
                document.getElementById("nrCeMasterFilter"),
                document.getElementById("nrCeHouseFilter"),
                document.getElementById("dtRedestinacaoInicioFilter"),
                document.getElementById("dtRedestinacaoFimFilter"),
                document.getElementById("dtDesconsolidacaoInicioFilter"),
                document.getElementById("dtDesconsolidacaoFimFilter"),
                document.getElementById("MainContent_ddlWeekFilter"),
                document.getElementById("MainContent_ddlNavioFilter"),
            ]

            for (let i = 0; i < dadosFiltro.length; i++) {
                dadosFiltro[i].value = "";
            }
            listarProcessosOperacional();
        }

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
                "navio": document.getElementById("MainContent_ddlNavioFilter").value
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
                            $("#tblModuloOperacionalBody").append("<tr data-id='" + dado[i]["HOUSE"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setId(" + dado[i]["HOUSE"] + ")' style='margin-right: 10px'>Selecionar</div>"+ dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"]+"' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TPAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["TESTUFAGEM"] + "</td><td class='text-center' title='" + dado[i]["AGENTE"] +"' style='max-width: 14ch;'>" + dado[i]["AGENTE"] + "</td>" +
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
                $("#tblUploadArquivoBody").empty();
                $("#ddlDocumento").empty();
                listarTipoAviso();
                btnUpload.style.display = "block";
                btnUploadx.style.display = "none";
                dadoUploadX.style.display = "none";
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

        function checarDiretorio() {
            var dadoUpload = document.querySelector("#dadoUpload");
            var btnUpload = document.querySelector("#btnUpload");
            var btnUploadx = document.querySelector("#btnUploadx");
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/checarDiretorio",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    console.log(dado);
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "0") {
                        alert("O diretório C:\FCA\DOCUMENTOS\ não foi encontrado");
                    } else {
                        dadoUploadX.style.display = "block";
                        btnUpload.style.display = "none";
                        btnUploadx.style.display = "block";
                    }
                }
            })
        }

        function uploadCaminhoArquivo() {
            var btnUpload = document.querySelector("#btnUpload");
            var btnUploadx = document.querySelector("#btnUploadx");
            var dadoUpload = document.getElementById("dadoUpload").files[0].name;
            var tipoaviso = document.getElementById("ddlTipoAviso").value;
            
            var path = dadoUpload;
            var dataForm = new FormData();
            var fileUpload = $('#dadoUpload').get(0);
            var files = fileUpload.files;
            dataForm.append(files[0].name, files[0]);
            dataForm.append('id', id);
            dataForm.append('tipoaviso', tipoaviso);
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/criarDiretorio",
                data: dataForm,
                contentType: false,
                processData: false,
                success: function () {
                    upload();
                },
                error: function () {
                    $("#msgErrAnexo").fadeIn(500).delay(1000).fadeOut(500);
                    dadoUploadX.style.display = "none";
                    btnUpload.style.display = "block";
                    btnUploadx.style.display = "none";
                }
            })
        }

        function upload() {
            var documento = document.getElementById("ddlDocumento").value;
            var dadoUpload = document.getElementById("dadoUpload").files[0].name;
            var tipoaviso = document.getElementById("ddlTipoAviso").value;
            var path = dadoUpload;
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/uploadArquivo",
                data: '{idprocesso: "' + id + '", iddocumento: "' + documento + '", arquivo: "' + path + '", idtipoaviso: "' + tipoaviso + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados == "ok") {
                        $("#msgSuccessUploadArquivo").fadeIn(500).delay(1000).fadeOut(500);
                        listarDocumentosArquivados();
                        dadoUploadX.files == "";
                        btnUpload.style.display = "block";
                        btnUploadx.style.display = "none";
                        dadoUploadX.style.display = "none";
                    } else if (dados == "1") {
                        $("#msgErrAnexo").fadeIn(500).delay(1000).fadeOut(500);
                        btnUpload.style.display = "block";
                        btnUploadx.style.display = "none";
                        dadoUploadX.style.display = "none";
                        listarDocumentosArquivados();
                    }
                }
            })
        }

        function deletarDocumentoArquivado() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/deleterDocumentoArquivado",
                data: '{documentoArquivado:"' + documentoArquivado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        $("#msgSuccessDelete").fadeIn(500).delay(1000).fadeOut(500);
                        listarDocumentosArquivados();
                    } else {
                        $("#msgErrDelete").fadeIn(500).delay(1000).fadeOut(500);
                        listarDocumentosArquivados();
                    }
                }
            })
        }

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
                    if (dado == "ok") {
                        $("#modalUploadArquivo").modal("hide");
                        $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function caixaSaida() {
            $("#modalCaixaSaida").modal("show");
            var filtro = document.getElementById("ddlFiltroCaixaSaida").value;
            var consulta = document.getElementById("txtConsulta").value;
            var enviado = document.getElementById("enviado");
            var nenviado = document.getElementById("todos");
            var dtgerado = document.getElementById("ddlGerados").value;
            var enviadov;
            var enviadon;

            if (enviado.checked) {
                enviadov = "1";
            } else {
                enviadov = "0";
            }

            if (nenviado.checked) {
                enviadon = "1";
            } else {
                enviadon = "0";
            }

            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarEmail",
                data: '{filtro: "' + filtro + '", consulta: "' + consulta + '", enviado: "' + enviadov + '", nenviado: "' + enviadon +'", dtgerado: "'+ dtgerado +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblCaixaSaidaBody").empty();
                    $("#tblCaixaSaidaBody").append("<tr><td colspan='11'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#tblCaixaSaidaBody").empty();
                    if (dado != null) {
                        for (var i = 0; i < dado.length; i++) {
                            $("#tblCaixaSaidaBody").append("<tr data-id='" + dado[i]["IDEMAIL"] + "'><td class='text-center'>" +
                                "<div class='btn btn-primary select' onclick='setIdListaEmailCaixaSaida(" + dado[i]["IDEMAIL"] + ")'>Selecionar</div></td >" +
                                "<td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NMTIPOAVISO"] + "' style='max-width: 20ch;'>" + dado[i]["NMTIPOAVISO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_GERACAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PREVISAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_ENVIO"] + "</td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 20ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["IDARMAZEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PARCEIRO"] + "</td></tr > ");
                        }
                    } else{
                        $("#tblCaixaSaidaBody").append("<tr><td id='msgEmptyDemurrageContainer' colspan='11' class='alert alert-light text-center'>Email não encontrado</td></tr>");
                    }
                }
            })
        }

        function montagemEmail() {
            if (id != 0) {
                $("#modalMontagemEmail").modal("show");
                document.getElementById("corpoEmail").value = "";
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
                                data: '{idProcessoMaster:"' + id + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        $("#tblListaProcessoHouseBody").empty();
                                        for (let i = 0; i < dado.length; i++) {
                                            $("#tblListaProcessoHouseBody").append("<tr><td class='text-center'><div><input type='checkbox' class='teste' value='" + dado[i]["HOUSE"] + "' name='checks' checked/></div></td>" +
                                                "<td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                                "<td class='text-center' title='" + dado[i]["CLIENTE"]+ "' style='max-width: 25ch;'>" + dado[i]["CLIENTE"] + "</td></tr>");
                                        }
                                    }
                                }
                            })
                            $.ajax({
                                type: "POST",
                                url: "Gerencial.asmx/escreverTituloEmail",
                                data: '{idProcessoMaster:"' + id + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (dado) {
                                    var dado = dado.d;
                                    dado = $.parseJSON(dado);
                                    if (dado != null) {
                                        document.getElementById("corpoEmail").value = "ARMADOR: " + dado[0]["TRANSPORTADOR"] + "\r\nMBL Nº: " + dado[0]["NR_BL"] + "\r\nTERMINAL ATRACAÇÃO: " + dado[0]["ARMAZEM_ATRACACAO"] + "\r\n\r\n";
                                    }
                                    $.ajax({
                                        type: "POST",
                                        url: "Gerencial.asmx/escreverCorpoEmail",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (dado) {
                                            var dado = dado.d;
                                            dado = $.parseJSON(dado);
                                            if (dado != null) {
                                                document.getElementById("corpoEmail").value += dado[0]["NM_SETOR"] + "\r\n" + dado[0]["NR_TELEFONE_SETOR"] + "\r\n" + dado[0]["EMAIL_SETOR"] + "";
                                            }
                                        }
                                    })
                                }
                            })
                            
                            
                        }
                    }
                })
            } else {
                $("#msgErrSelect").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function cancelarAgendamento() {
            if (idEmailAgendamento != 0) {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/verificarCancelamento",
                    data: '{idEmail: "' + idEmailAgendamento + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        console.log(dado);
                        if (dado.d == "cancelado") {
                            $("#modalReativarEmail").modal("show");
                                                        
                        } else {
                            $("#modalCancelamentoEmail").modal("show");
                        }
                    }
                })
            } else {
                $("#msgErrSelectEmail").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function cancelarAgen() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/cancelarAgendamento",
                data: '{idEmail: "' + idEmailAgendamento + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    if (dado.d == "ok") {
                        $("#modalCancelamentoEmail").modal("hide");
                        $("#msgSuccessCancelEmail").fadeIn(500).delay(1000).fadeOut(500);
                        listarAgendamento();
                    } else {
                        $("#modalCancelamentoEmail").modal("hide");
                        $("#msgErrorCancelEmail").fadeIn(500).delay(1000).fadeOut(500);
                        listarAgendamento();
                    }
                }
            })
        }

        function reatviarAgend() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/reativarAgendamento",
                data: '{idEmail: "' + idEmailAgendamento + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    if (dado.d == "ok") {
                        $("#modalReativarEmail").modal("hide");
                        $("#msgSuccessAgendEmail").fadeIn(500).delay(1000).fadeOut(500);
                        listarAgendamento();
                    }
                }
            })
        }

        function enviarEmail() {
            pacote = document.querySelectorAll('[name=checks]:checked');
            values = [];
            var master = document.getElementById("nrMasterBLemail").textContent;
            var corpo = document.getElementById("corpoEmail").value;
            for (let i = 0; i < pacote.length; i++) {
                values.push(pacote[i].value);
            }
            if (values.length > 0) {
                for (let i = 0; i < values.length; i++) {
                    $.ajax({
                        type: "POST",
                        url: "Gerencial.asmx/enviarEmail",
                        data: '{house:"' + values[i] + '",corpo:"' + corpo +'"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {;
                            if (dado.d == "ok") {
                                $("#msgSuccessDemu").fadeIn(500).delay(1000).fadeOut(500);
                            }
                        }
                    })
                }
                $("#modalMontagemEmail").modal("hide");
            }
        }


        function listarTipoAviso() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarTipoAviso",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#ddlTipoAviso").empty();
                    $("#ddlTipoAviso").append("<option value=''>Selecione</option>");
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#ddlTipoAviso").append("<option value='" + dado[i]["IDTIPOAVISO"] + "'>" + dado[i]["NMTIPOAVISO"] + "</option>");
                        }
                    }
                    else {

                    }
                }
            }) 
        }

        function listarTipoDocumento() {
            var tipoaviso = document.getElementById("ddlTipoAviso").value;
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
                        if (tipoaviso == 3) {
                            document.querySelector("#titleUpload").textContent = "Nº PROCESSO: "
                            document.querySelector("#nrMasterBL").value = dado[0]["NRHOUSE"];
                        } else {
                            document.querySelector("#titleUpload").textContent = "NR MASTER"
                            document.querySelector("#nrMasterBL").value = dado[0]["NRMASTER"];
                        }
                    }
                }
            })
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarTipoDocumento",
                data: '{idtipoaviso: "' + tipoaviso + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#ddlDocumento").empty();
                    if (dado != null) {
                        for (let i = 0; i<dado.length; i++) {
                            $("#ddlDocumento").append("<option value='" + dado[i]["IDDOCUMENTO"] + "'>" + dado[i]["NMDOCUMENTO"] + "</option>");
                        }
                    } else {
                    }
                }
            })
            listarDocumentosArquivados();
        }

        function listarDocumentosArquivados() {
            var tipoaviso = document.getElementById("ddlTipoAviso").value;
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarDcoumentosArquivados",
                data: '{idprocesso: "' + id +'", idtipoaviso: "' + tipoaviso + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#tblUploadArquivoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblUploadArquivoBody").append("<tr data-id='" + dado[i]["AUTONUM"] + "'><td class='text-center'><div class='btn btn-primary select' onclick='setIdDocumentoArquivado(" + dado[i]["AUTONUM"] + ")'>Selecionar</div></td>" +
                                "<td class='text-center'>" + dado[i]["DTPOSTAGEM"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NMDOCUMENTO"] + "' style='max-width: 25ch;'>" + dado[i]["NMDOCUMENTO"] + "</td></tr>");
                        }
                    } else {
                        $("#tblUploadArquivoBody").append("<tr><td id='msgEmptyDemurrageContainer' colspan='3' class='alert alert-light text-center'>Documentos não encontrados</td></tr>");
                    }
                }
            })
        }

        function verificarEmail() {
            if (idEmailCaixa != 0) {
                $("#modalVisualizarEmail").modal("show");
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/visualizarEmail",
                    data: '{idProcesso: "' + idEmailCaixa + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        $("#visualizarEmail").empty();
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            console.log(dado[0]["ASSUNTO"]);
                            document.getElementById("visualizarEmail").insertAdjacentHTML('afterbegin',dado[0]["ASSUNTO"] +"<br>"+ dado[0]["CORPO"]);
                        }
                    }
                })
            }
        }

        function verificarReenvioEmail() {
            if (idEmailCaixa != 0) {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/verificarReenvio",
                    data: '{idProcesso: "' + idEmailCaixa + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado == "ok") {
                            $("#modalReenvioEmail").modal('show');
                        } else {
                            $("#msgErrReenvioEmail").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function reenviarEmail() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/verificarReenvio",
                data: '{idProcesso: "' + idEmailCaixa + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        $("#msgSuccessReenvioEmail").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function verificarRemocaoEmail() {
            if (idEmailCaixa != 0) {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/verificarRemocao",
                    data: '{idProcesso: "' + idEmailCaixa + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado == "ok") {
                            $("#modalRemoverEmail").modal('show');
                        } else {
                            $("#msgErrRemoveEmail").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function removerEmail() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/removerEmail",
                data: '{idProcesso: "' + idEmailCaixa + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        $("#modalRemoverEmail").modal('hide');
                        $("#msgSuccessRemoveEmail").fadeIn(500).delay(1000).fadeOut(500);
                        caixaSaida();
                    }
                }
            })
        }

        function listarAgendamento() {
            $("#modalCaixaAgendamento").modal("show");
            var filtro = document.getElementById("ddlFiltroCaixaAgendamento").value;
            var consulta = document.getElementById("txtConsultaAgendamento").value;
            var enviado = document.getElementById("enviadoAg");
            var aenviar = document.getElementById("todosAg");
            var dtgerado = document.getElementById("ddlGeradosAg").value;
            var enviadoAg;
            var aenviarAg;

            if (enviado.checked) {
                enviadoAg = "1";
            } else {
                enviadoAg = "0";
            }

            if (aenviar.checked) {
                aenviarAg = "1";
            } else {
                aenviarAg = "0";
            }
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarEmailAgendado",
                data: '{filtro: "' + filtro + '", consulta: "' + consulta + '", enviado: "' + enviadoAg + '", nenviado: "' + aenviarAg + '", dtgerado: "' + dtgerado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblCaixaAgendamentoBody").empty();
                    $("#tblCaixaAgendamentoBody").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#tblCaixaAgendamentoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblCaixaAgendamentoBody").append("<tr data-id='" + dado[i]["ID_SOLICITACAO_EMAIL"] + "'><td class='text-center'>" +
                                "<div class='btn btn-primary select' onclick='setIdListaEmailCaixaAgendamento(" + dado[i]["ID_SOLICITACAO_EMAIL"] + ")'>Selecionar</div></td >" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_BL"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NMTIPOAVISO"] + "' style='max-width: 20ch;'>" + dado[i]["NMTIPOAVISO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_SOLICITACAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_START"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_GERACAO_EMAIL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CANCELAMENTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["OB_CANCELAMENTO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 15ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["IDARMAZEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PARCEIRO"] + "</td></tr > ");
                        }
                    } else{
                        $("#tblCaixaAgendamentoBody").append("<tr><td id='msgEmptyDemurrageContainer' colspan='12' class='alert alert-light text-center'>Email não encontrado</td></tr>");
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
        }

        function setIdListaEmailCaixaSaida(Id) {
            idEmailCaixa = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdListaEmailCaixaAgendamento(Id) {
            idEmailAgendamento = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdDocumentoArquivado(Id) {
            documentoArquivado = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
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
