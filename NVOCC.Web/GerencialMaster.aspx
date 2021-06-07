<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerencialMaster.aspx.cs" Inherits="ABAINFRA.Web.GerencialMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Gerencial Master
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Gerencial Master
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" class="btn btn-primary">Gerar CSV</button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExport" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">MBL</th>
                                            <th class="text-center" scope="col">CARRIER</th>
                                            <th class="text-center" scope="col">TIPO ESTUFAGEM - M - H</th>
                                            <th class="text-center" scope="col">CNTR 20</th>
                                            <th class="text-center" scope="col">CNTR 40</th>
                                            <th class="text-center" scope="col">ORIGEM</th>
                                            <th class="text-center" scope="col">DESTINO</th>
                                            <th class="text-center" scope="col">ETD</th>
                                            <th class="text-center" scope="col">ETA</th>
                                            <th class="text-center" scope="col">DATA CHEGADA</th>
                                            <th class="text-center" scope="col">DATA PAGAMENTO</th>
                                            <th class="text-center" scope="col">VALOR PAGAMENTO</th>
                                            <th class="text-center" scope="col">TX CAMBIO PAGTO</th>
                                            <th class="text-center" scope="col">DATA RECEBIMENTO</th>
                                            <th class="text-center" scope="col">VALOR RECEBIMENTO</th>
                                            <th class="text-center" scope="col">TX CAMBIO RECEBTO</th>
                                            <th class="text-center" scope="col">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrier">
                                       
                                    </tbody>
                                </table>
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
                                                        <label class="control-label">Data da Retirada</label>
                                                        <input id="dtRetirada" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Retirado Por</label>
                                                        <input id="receptor" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Nº da Fatura</label>
                                                        <input id="nrFatura" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnEditarCourrier" class="btn btn-success">Editar Courrier</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
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
</asp:Content>
