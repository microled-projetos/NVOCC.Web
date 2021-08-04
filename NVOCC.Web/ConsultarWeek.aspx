<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultarWeek.aspx.cs" Inherits="ABAINFRA.Web.ConsultarWeek" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-8 col-lg-offset-2 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Consolidados
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="alert alert-success text-center" id="msgSuccessWeek">
                                Registro cadastrado/atualizado com sucesso!
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrWeek">
                                Erro ao cadastrar/atualizar.
                            </div>
                        </div>
                    </div>
                    <div class="row topMarg">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <button type="button" id="btnNovaWeek" class="btn btn-primary" data-toggle="modal" data-target="#modalWeek">Cadastrar Week</button>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="modal fade bd-example-modal-lg" id="modalWeek" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalWeekTitle">Cadastrar Week</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                     <div class="row" id="listWeek">
                                        <div class="text-center col-sm-6 col-sm-offset-3">
                                            <label class="control-label text-center" style="font-size: 14px;">Cód. Week</label><br>
                                            <select id="ddlWeek" onchange="BuscarWeek(this.value)" class="labelTaxa form-control">
                                            </select>
                                        </div>
                                     </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="control-label">Parceiro <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlParceiroWeek" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Referência <span class="required">*</span></label>
                                                <asp:TextBox ID="txtReferencia" runat="server" required="True" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto Origem Local <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlPortoLocal" runat="server" CssClass="form-control" DataValueField="ID_PORTO" DataTextField="NM_PORTO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto Origem Destino <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlPortoDestino" runat="server" CssClass="form-control" DataValueField="ID_PORTO" DataTextField="NM_PORTO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">MBL</label>
                                                <asp:TextBox ID="txtMBL" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-9">
                                            <div class="form-group">
                                                <label class="control-label">Vessel</label>
                                                <asp:TextBox ID="txtVessel" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Cut Off <span class="required">*</span></label>
                                                <asp:TextBox ID="dtCutOff" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">ETD</label>
                                                <asp:TextBox ID="dtETD" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">ETA</label>
                                                <asp:TextBox ID="dtETA" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Freight</label>
                                                <asp:TextBox ID="nrFreight" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Freetime</label>
                                                <asp:TextBox ID="nrFreetime" runat="server" TextMode="Number" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnSalvarWeek" class="btn btn-success" data-dismiss="modal">Cadastrar Week</button>
                                    <button type="button" id="btnEditarWeek" class="btn btn-success">Editar Week</button>
                                    <button type="button" id="btnSalvarEditWeek" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                    <button type="button" id="btnCancelWeek" class="btn btn-danger">Cancelar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--<div class="modal fade bd-example-modal-lg" id="modalContainer" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalContainerTitle">Week <span id="setWeek"></span></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-3" id="idContainer">
                                            <div class="form-group">
                                                <label class="control-label">Id Container</label>
                                                <select id="ddlIdContainer" class="form-control">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" id="idWeek">
                                            <div class="form-group">
                                                <label class="control-label">Id Week</label>
                                                <select id="ddlIdWeek" class="form-control">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Tipo Container</label>
                                                <asp:DropDownList ID="ddlTipoContainer" runat="server" CssClass="form-control" DataValueField="ID_TIPO_CONTAINER" DataTextField="NM_TIPO_CONTAINER">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Nº Container <span class="required">*</span></label>
                                                <input type="text" id="nrContainer" name="nrcont" class="form-control" onchange="Calculo_Digito_Conteiner()">
                                                <span class="erroNrContainer">Número do Container inválido</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Peso Máximo <span class="required">*</span></label>
                                                <asp:TextBox ID="vlPesoMax" runat="server" TextMode="Number" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">Cubagem <span class="required">*</span></label>
                                                <asp:TextBox ID="vlCubagem" runat="server" TextMode="Number" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="alert alert-success text-center popup" id="msgSuccessContWeek">
                                        Registro cadastrado/atualizado com sucesso!
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrContWeek">
                                        Erro ao cadastrar/atualizar.
                                    </div>
                                    <div class="table-responsive tableFixHead">
                                        <table class="table tablecont">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" scope="col">Tipo Container</th>
                                                    <th class="text-center" scope="col">Nº Container</th>
                                                    <th class="text-center" scope="col">Peso Máximo</th>
                                                    <th class="text-center" scope="col">Cubagem</th>
                                                    <th class="text-center" scope="col"></th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdWeekContainer">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnCadastrarContainer" class="btn btn-success" onclick="Calculo_Digito_Conteiner_Enviar()">Cadastrar Container</button>
                                    <button type="button" id="btnEditarContainer" class="btn btn-warning">Editar Container</button>
                                    <button type="button" id="btnSalvarEditContainer" class="btn btn-success">Salvar Edição</button>
                                    <button type="button" id="btnCancelEdit" class="btn btn-danger">Cancelar</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>-->

                    <div class="modal fade bd-example-modal-xl" id="modalInsideWeek" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalInsideWeekTitle">Week <span id="setWeek"></span></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-10 col-sm-offset-1">
                                            <div class="alert alert-success text-center" id="msgSuccessContEditFCA">
                                                Successfully Binded.
                                            </div>
                                            <div class="alert alert-danger text-center" id="msgErrContEditFCA">
                                                An error has ocurred.
                                            </div>
                                            <div class="alert alert-success text-center" id="msgSuccessEditBlFCA">
                                                Successfully Updated.
                                            </div>
                                            <div class="alert alert-success text-center" id="msgSuccessRemoveBlFCA">
                                                Successfully Removed.
                                            </div>
                                            <div class="alert alert-success text-center" id="msgSuccessRegContBlFCA">
                                                Successfully Registered.
                                            </div>
                                            <div class="alert alert-success text-center" id="msgSuccessBindContBlFCA">
                                                Successfully Container Binded
                                            </div>
                                            <div class="alert alert-danger text-center" id="msgErrBindContBlFCA">
                                                An error has ocurred in bind process.
                                            </div>
                                            <div class="alert alert-danger text-center" id="msgerrselectcont">
                                                Select Container
                                            </div>
                                            <div class="alert alert-danger text-center" id="msgerrselectblfca">
                                                Select at least one process.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active" id="tabprocessoExpectGrid">
                                                <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                                    <i class="fa fa-edit" style="padding-right: 8px;"></i>FCA LOG
                                                </a>
                                            </li>
                                            <li id="tabprocessoRealGrid">
                                                <a href="#processoRealGrid" id="linkprocessoRealGrid" role="tab" data-toggle="tab">
                                                    <i class="fa fa-edit" style="padding-right: 8px;"></i>WORLDWIDE
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane fade active in" id="processoExpectGrid">
                                                <div class="table-responsive tableFixHead">
                                                    <table class="table table-striped tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col"></th>
                                                                <th class="text-center" scope="col"></th>
                                                                <th class="text-center" scope="col">System Reference</th>
                                                                <th class="text-center" scope="col">Sales</th>                                                              
                                                                <th class="text-center" scope="col">Broker(Cnee)</th>
                                                                <th class="text-center" scope="col">Importer(Real Cnee)</th>
                                                                <th class="text-center" scope="col">Shipper</th>
                                                                <th class="text-center" scope="col">O/F</th>
                                                                <th class="text-center" scope="col">EXW rates</th>
                                                                <th class="text-center" scope="col">Weight(kg)</th>
                                                                <th class="text-center" scope="col">M3</th>
                                                                <th class="text-center" scope="col">PCS</th>
                                                                <th class="text-center" scope="col">REF. CNEE</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="infoProcessoFCA">
                                                        </tbody>
                                                        <tfoot id="infoSomaProcessoFCA">
                                                        </tfoot>
                                                    </table>
                                                </div>
                                                <div class="row topMarg">
                                                    <div id="boxPesoFCATOTAL" class="col-sm-2 text-center"  style="font-size: 14px;">Total Weight: <span id="pesoTotal" style="font-weight:bold"></span> Kg</div>
                                                    <div id="boxVolumeFCATOTAL" class="col-sm-2 text-center"  style="font-size: 14px;">Total M³: <span id="cubTotal" style="font-weight:bold"></span> m³</div>
                                                    <div id="boxMercadoriaFCATOTAL" class="col-sm-2 text-center"  style="font-size: 14px;">Total PCS: <span id="mercadoriaTotal" style="font-weight:bold"></span></div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade" id="processoRealGrid">
                                                <div class="row">
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <div class="alert alert-success text-center" id="msgSuccessContEditPartner">
                                                            Successfully Updated
                                                        </div>
                                                        <div class="alert alert-danger text-center" id="msgErrContEditPartner">
                                                            Update Error.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="table-responsive tableFixHead">
                                                    <table class="table table-striped tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col"></th>
                                                                <th class="text-center" scope="col">Weight(kg)</th>
                                                                <th class="text-center" scope="col">M3</th>
                                                                <th class="text-center" scope="col">PCS</th>
                                                                <th class="text-center" scope="col">Packaging</th>
                                                                <th class="text-center" scope="col">Terms</th>
                                                                <th class="text-center" scope="col">Cargo Ready Date</th>
                                                                <th class="text-center" scope="col">Delivery Forecast in WH</th>
                                                                <th class="text-center" scope="col">Date of Arrive in WH</th>
                                                                <th class="text-center" scope="col">Draft Cut Off</th>
                                                                <th class="text-center" scope="col">Cargo Cut Off</th>
                                                                <th class="text-center" scope="col">HBL</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="infoProcessoPartner">
                                                        </tbody>
                                                        <tfoot id="infoSomaProcessoPartner">
                                                        </tfoot>
                                                    </table>
                                                </div>
                                                <div class="row topMarg">
                                                    <div id="boxPesoPartnerTotal" class="col-sm-2 text-center" style="font-size: 14px;">Total Weight: <span id="pesoTotalPartner" style="font-weight:bold"></span> Kg</div>
                                                    <div id="boxVolumePartnerTotal" class="col-sm-2 text-center" style="font-size: 14px;" >Total M³: <span id="cubTotalPartner" style="font-weight:bold"></span> m³</div>
                                                    <div id="boxMercadoriaPartnerTotal" class="col-sm-2 text-center" style="font-size: 14px;" >Total PCS: <span id="mercadoriaTotalPartner" style="font-weight:bold"></span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" id="SelectionContainer" onclick="listarContainer()">Select Container</button>
                                    <button type="button" class="btn btn-danger" id="cancelSelectionBL" onclick="checkboxHide()">Cancel</button>
                                    <button type="button" class="btn btn-primary" id="selectionBL" onclick="checkboxPop()">Bind Process</button>
                                    <button type="button" class="btn btn-primary" onclick="listarContainerWeek()">Container Week List </button>
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalAdicionarProcesso">Add Process</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-md" id="modalAdicionarProcesso" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalAdicionarProcessoTitle">Add Process</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                     <div class="row">
                                          <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="control-label">Process</label>
                                                <select id="ddlProcessoWeek" class="form-control">
                                                </select>
                                            </div>
                                        </div>
                                     </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="VincularProcesso()">Bind</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    

                    <div class="modal fade bd-example-modal-md" id="modalDeleteProcessoWeekFCA" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDeleteProcessoWeekFCATitle">Remove Week Process</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                     <h4>Are you sure you want to remove the processo from the week?</h4>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="RemoverProcessoWeek()" data-dismiss="modal">Yes</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalEditarProcessoFCA" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalEditarProcessoFCATitle">Edit Process</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                     <div class="row">
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Sales</label>
                                                 <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="form-control" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Broker(Cnee)</label>
                                                 <asp:DropDownList ID="ddlBroker" runat="server" CssClass="form-control" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Importer(Brazil)</label>
                                                 <asp:DropDownList ID="ddlImporter" runat="server" CssClass="form-control" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Shipper</label>
                                                 <asp:DropDownList ID="ddlShipper" runat="server" CssClass="form-control" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                     </div>
                                    <div class="row">
                                         <div class="col-sm-4">
                                             <div class="form-group">
                                                 <label>Weight(kg)</label>
                                                 <input type="text" id="vlWeight" class="form-control"/>
                                             </div>
                                         </div>
                                        <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>M3</label>
                                                 <input type="text" id="vlm3" class="form-control"/>
                                             </div>
                                         </div>
                                        <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>PCS</label>
                                                 <input type="text" id="vlPCS" class="form-control"/>
                                             </div>
                                         </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="EditarBLFCA()" data-dismiss="modal">Save</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-lg" id="modalEditarProcessoPartner" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalEditarProcessoPartnerTitle">Edit Process</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                     <div class="row">
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Weight(kg) *Confirmed</label>
                                                 <input type="text" id="vlWeightConfirmed" class="form-control"/>
                                             </div>
                                         </div>
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>M3 *Confirmed</label>
                                                 <input type="text" id="vlM3Confirmed" class="form-control"/>
                                             </div>
                                         </div>
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>PCS *Confirmed</label>
                                                 <input type="text" id="vlPCSConfirmed" class="form-control" />
                                             </div>
                                         </div>
                                         <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Packaging</label>
                                                 <asp:DropDownList ID="ddlTipoMercadoria" runat="server" CssClass="form-control" DataValueField="ID_MERCADORIA" DataTextField="NM_MERCADORIA">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                     </div>
                                    <div class="row">
                                         <div class="col-sm-4">
                                             <div class="form-group">
                                                 <label>Weight(kg)</label>
                                                 <asp:DropDownList ID="ddlIncoterm" runat="server" CssClass="form-control" DataValueField="ID_INCOTERM" DataTextField="CD_INCOTERM">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                        <div class="col-sm-4">
                                             <div class="form-group">
                                                 <label>Cargo Ready Date</label>
                                                 <input type="date" id="dtCargoReadyDate" class="form-control"/>
                                             </div>
                                         </div>
                                        <div class="col-sm-4">
                                             <div class="form-group">
                                                 <label>Delivery Forecast in WH</label>
                                                 <input type="date" id="dtDeliveryForecastWH" class="form-control"/>
                                             </div>
                                         </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Date of Arrive in WH</label>
                                                 <input type="date" id="dtArriveWH" class="form-control"/>
                                             </div>
                                         </div>
                                        <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Draft Cut Off</label>
                                                 <input type="date" id="dtCutoff" class="form-control"/>
                                             </div>
                                         </div>
                                        <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>Cargo Cut Off</label>
                                                 <input type="date" id="dtCargoCutoff" class="form-control"/>
                                             </div>
                                         </div>
                                        <div class="col-sm-3">
                                             <div class="form-group">
                                                 <label>HBL</label>
                                                 <input type="text" id="nrHBL" class="form-control"/>
                                             </div>
                                         </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="EditarBLPartner()" data-dismiss="modal">Save</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-md" id="modalContainer" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalContainerTitle">Bind Container/Process</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="table-responsive tableFixHead">
                                            <table class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">Nº Container</th>
                                                        <th class="text-center" scope="col">Type</th>                                                              
                                                        <th class="text-center" scope="col">Max Weight</th>
                                                        <th class="text-center" scope="col">Max Volume</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="infoContainerWeek">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="vincularContainer()">Bind Container</button>
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalCadastrarConteiner">Register Container</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-md" id="modalCadastrarConteiner" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalCadastrarConteinerTitle">Container Register </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="alert alert-danger text-center" id="msgErrRegContBlFCA">
                                            Invalid Container Code.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErr2RegContBlFCA">
                                            Invalid Container Code.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessRegContBl">
                                            Successfully Registered.
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-sm-4 col-sm-offset-3">
                                            <div class="form-group">
                                                <label class="control-label">Nº Container</label>
                                                <input type="text" id="nrContainer" class="form-control" onchange="Calculo_Digito_Conteiner()"/>
                                            </div>
                                        </div>
                                         <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Container Type</label>
                                                <asp:DropDownList ID="ddlTipoConteiner" CssClass="form-control" runat="server" DataTextField="NM_TIPO_CONTAINER" DataValueField="ID_TIPO_CONTAINER"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="CadastrarContainer()">Register</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-md" id="modalListarContainerWeek" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalListarContainerWeekTitle">Containers Week</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="table-responsive tableFixHead">
                                            <table class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">Nº Container</th>
                                                        <th class="text-center" scope="col">Type</th>                                                              
                                                        <th class="text-center" scope="col">Max Weight</th>
                                                        <th class="text-center" scope="col">Max Volume</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblContainerWeek">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalListarProcessosContainerWeek" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalListarProcessosContainerWeekTitle">Process - Containers Week</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="alert alert-success text-center" id="msgSuccessRemoveBlFCACont">
                                            Successfully Removed.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="table-responsive tableFixHead">
                                            <table class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">System Reference</th>
                                                        <th class="text-center" scope="col">Sales</th>                                                              
                                                        <th class="text-center" scope="col">Broker(Cnee)</th>
                                                        <th class="text-center" scope="col">Importer(Real Cnee)</th>
                                                        <th class="text-center" scope="col">Shipper</th>
                                                        <th class="text-center" scope="col">O/F</th>
                                                        <th class="text-center" scope="col">EXW rates</th>
                                                        <th class="text-center" scope="col">Weight(kg)</th>
                                                        <th class="text-center" scope="col">M3</th>
                                                        <th class="text-center" scope="col">PCS</th>
                                                        <th class="text-center" scope="col">REF. CNEE</th>
                                                        <th class="text-center" scope="col">Weight(kg)</th>
                                                        <th class="text-center" scope="col">M3</th>
                                                        <th class="text-center" scope="col">PCS</th>
                                                        <th class="text-center" scope="col">Packaging</th>
                                                        <th class="text-center" scope="col">Terms</th>
                                                        <th class="text-center" scope="col">Cargo Ready Date</th>
                                                        <th class="text-center" scope="col">Delivery Forecast in WH</th>
                                                        <th class="text-center" scope="col">Date of Arrive in WH</th>
                                                        <th class="text-center" scope="col">Draft Cut Off</th>
                                                        <th class="text-center" scope="col">Cargo Cut Off</th>
                                                        <th class="text-center" scope="col">HBL</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblProcessosContainerWeek">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row topMarg">
                                        <div id="boxPesoLimit" class="col-sm-2 text-center"  style="font-size: 14px;">Weight Limit: <span id="pesoLimit" style="font-weight:bold"></span> Kg</div>
                                        <div id="boxVolumeLimit" class="col-sm-2 text-center"  style="font-size: 14px;">M³ Limit: <span id="cubLimit" style="font-weight:bold"></span> m³</div>
                                        <div id="boxPesoContTotal" class="col-sm-2 text-center" style="font-size: 14px;">Total Weight: <span id="pesoTotalCont" style="font-weight:bold"></span> Kg</div>
                                        <div id="boxVolumeContTotal" class="col-sm-2 text-center" style="font-size: 14px;" >Total M³: <span id="cubTotalCont" style="font-weight:bold"></span> m³</div>
                                        <div id="boxMercadoriaContTotal" class="col-sm-2 text-center" style="font-size: 14px;" >Total PCS: <span id="mercadoriaTotalCont" style="font-weight:bold"></span></div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-md" id="modalDeleteProcessoContainerFCA" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDeleteProcessoContainerFCATitle">Remove Container Process</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                     <h4>Are you sure you want to remove the process from the container?</h4>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="RemoverProcessoContainer()" data-dismiss="modal">Yes</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive tableFixHead">
                        <table class="table table-striped tablecont">
                            <thead>
                                <tr>
                                    <th class="text-center" scope="col">Referencia</th>
                                    <th class="text-center" scope="col">POL</th>
                                    <th class="text-center" scope="col">POD</th>
                                    <th class="text-center" scope="col">ETD</th>
                                    <th class="text-center" scope="col">CUTOFF</th>
                                    <th class="text-center" scope="col"></th>
                                </tr>
                            </thead>
                            <tbody id="grdWeek">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script src="Content/js/viacep.js"></script>
    <script src="Content/js/jquery.dataTables.min.js"></script>
    <link href="Content/css/select2.css" rel="stylesheet" />
    <script src="Content/ScrollableGridPlugin.js"></script>
    <script>
        var week = 0;
        var processoID = 0;
        var cntr = 0;
        var processoIDcntr = 0;
        $("#btnNovaWeek").click(function () {
            $("#btnSalvarWeek").show();
            $("#btnEditarWeek").hide();
            $("#btnSalvarEditWeek").hide();
            $("#btnCancelWeek").hide();
            $("#listWeek").hide();
            var forms = ['MainContent_txtReferencia',
                'MainContent_ddlPortoLocal',
                'MainContent_ddlPortoDestino',
                'MainContent_txtMBL',
                'MainContent_txtVessel',
                'MainContent_dtCutOff',
                'MainContent_dtETD',
                'MainContent_dtETA',
                'MainContent_nrFreight',
                'MainContent_nrFreetime',
                'MainContent_ddlParceiroWeek'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                
                    
                    aux.removeAttribute("disabled");
                
                aux.value = "";
            }
        })
        $("#btnSalvarWeek").click(function () {
            var dado = {
                "NM_WEEK": document.getElementById('MainContent_txtReferencia').value,
                "ID_PORTO_ORIGEM_LOCAL": document.getElementById('MainContent_ddlPortoLocal').value,
                "ID_PORTO_ORIGEM_DESTINO": document.getElementById('MainContent_ddlPortoDestino').value,
                "NM_MBL": document.getElementById('MainContent_txtMBL').value,
                "NM_VESSEL": document.getElementById('MainContent_txtVessel').value,
                "DT_CUTOFF": document.getElementById('MainContent_dtCutOff').value,
                "DT_ETD": document.getElementById('MainContent_dtETD').value,
                "DT_ETA": document.getElementById('MainContent_dtETA').value,
                "NR_FRIGHT": document.getElementById('MainContent_nrFreight').value,
                "NR_FREETIME": document.getElementById('MainContent_nrFreetime').value,
                "ID_PARCEIRO": document.getElementById('MainContent_ddlParceiroWeek').value
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarWeek",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        listarWeek();
                        $("#msgSuccessWeek").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    else {
                        $("#msgErrWeek").fadeIn(500).delay(1000).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrWeek").fadeIn(500).delay(1000).fadeOut(500);
                }
            })
        })
        $("#btnCancelWeek").click(function () {
            $("#btnEditarWeek").show();
            $("#btnSalvarEditWeek").hide();
            $("#btnCancelWeek").hide();
            var forms = ['MainContent_ddlParceiroWeek',
                'MainContent_txtReferencia',
                'MainContent_ddlPortoLocal',
                'MainContent_ddlPortoDestino',
                'MainContent_txtMBL',
                'MainContent_txtVessel',
                'MainContent_dtCutOff',
                'MainContent_dtETD',
                'MainContent_dtETA',
                'MainContent_nrFreight',
                'MainContent_nrFreetime'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                $(aux).attr("disabled", "true");
            }
        })
        $("#btnEditarWeek").click(function () {
            $("#btnEditarWeek").hide();
            $("#btnSalvarEditWeek").show();
            $("#btnCancelWeek").show();
            var forms = ['MainContent_ddlParceiroWeek','MainContent_txtReferencia',
                'MainContent_ddlPortoLocal',
                'MainContent_ddlPortoDestino',
                'MainContent_txtMBL',
                'MainContent_txtVessel',
                'MainContent_dtCutOff',
                'MainContent_dtETD',
                'MainContent_dtETA',
                'MainContent_nrFreight',
                'MainContent_nrFreetime'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
            }
        })
        $("#btnSalvarEditWeek").click(function () {
            var dadoEdit = {
                "ID_PARCEIRO": document.getElementById('MainContent_ddlParceiroWeek').value,
                "ID_WEEK": document.getElementById('ddlWeek').value,
                "NM_WEEK": document.getElementById('MainContent_txtReferencia').value,
                "ID_PORTO_ORIGEM_LOCAL": document.getElementById('MainContent_ddlPortoLocal').value,
                "ID_PORTO_ORIGEM_DESTINO": document.getElementById('MainContent_ddlPortoDestino').value,
                "NM_MBL": document.getElementById('MainContent_txtMBL').value,
                "NM_VESSEL": document.getElementById('MainContent_txtVessel').value,
                "DT_CUTOFF": document.getElementById('MainContent_dtCutOff').value,
                "DT_ETD": document.getElementById('MainContent_dtETD').value,
                "DT_ETA": document.getElementById('MainContent_dtETA').value,
                "NR_FRIGHT": document.getElementById('MainContent_nrFreight').value,
                "NR_FREETIME": document.getElementById('MainContent_nrFreetime').value
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarWeek",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        listarWeek();
                        $("#msgSuccessWeek").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    else {
                        listarWeek();
                        $("#msgErrWeek").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            });
        })


        $(document).ready(function () {
            listarWeek();
        });

        function listarWeek() {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarWeek",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdWeek").empty();
                    $("#grdWeek").append("<tr><td colspan='6'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#msgEmptyWeek").hide();
                        $("#grdWeek").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdWeek").append("<tr>" +
                                "<td class='text-center'>" + dado[i]["NM_WEEK"] + "</td><td class='text-center'>" + dado[i]["NMPORTOORIGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NMPORTODESTINO"] + "</td><td class='text-center'>" + dado[i]["DT_ETD"] + "</td><td class='text-center'>" + dado[i]["DT_CUTOFF"] + "</td>" +
                                "<td class='text-center'>" +
                                "<div class='btn btn-primary' data-toggle='modal' data-target='#modalInsideWeek' title='Verificar Week' onclick='ListarWeekInside(" + dado[i]["ID_WEEK"] + ")' ><i class='fas fa-eye'></i></div>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalWeek' title='Editar Week' onclick='BuscarWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-edit'></i></div>" +                               
                                "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdWeek").empty();
                        $("#grdWeek").append("<tr id='msgEmptyWeek'><td colspan='6' class='alert alert-light text-center'>Não foi encontrada nenhuma Week.</td></tr>");
                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarWeekList",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlWeek").empty();
                            for (let i = 0; i < data.length; i++) {
                                $("#ddlWeek").append("<option value='" + data[i]["ID_WEEK"] + "'>" + data[i]["NM_WEEK"] + " - " + data[i]["NMPORTOORIGEM"] + " até " + data[i]["NMPORTODESTINO"] + "</option>");
                            }
                        },
                        error: function (data) {

                        },
                    });
                }
            })
        }

        function listarProcessos(idweek) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/listarProcessos",
                data: '{week: "' + idweek + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#ddlProcessoWeek").empty();
                    if (data != null) {
                        for (let i = 0; i < data.length; i++) {
                            $("#ddlProcessoWeek").append("<option value='"+data[i]["ID_BL"]+"'>"+data[i]["NR_PROCESSO"]+"</option>");
                        }
                    } 
                }
            });
        }

        function CadastrarContainer() {
            var nrCont = document.getElementById("nrContainer").value;
            var tipo = document.getElementById("MainContent_ddlTipoConteiner").value;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarContainer",
                data: '{nrCont:"' + nrCont + '", tipo: "' + tipo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        listarContainer();
                        $("#msgSuccessRegContBl").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            });
        }

        function listarContainer() {
            cntr = 0;
            $("#modalContainer").modal("show");
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/listarContainer",
                data: '{week: "' + week + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#infoContainerWeek").empty();
                    $("#infoContainerWeek").append("<tr><td colspan='5'><div class='loader'></div></td></tr>");
                },
                success: function (data) {
                    var dado = data.d;
                    dado = $.parseJSON(dado);
                    $("#infoContainerWeek").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#infoContainerWeek").append("<tr data-id='" + dado[i]["ID_CNTR_BL"] + "'>" +
                                "<td class='text-center'>" +
                                "<div class='btn btn-primary' onclick='setId(" + dado[i]["ID_CNTR_BL"] + ")' >Select</div>" +
                                "<div class='btn btn-primary' style='margin-left: 5px;' title='Verificar Container' onclick='ListarProcessosContainer(" + dado[i]["ID_CNTR_BL"] + ")' ><i class='fas fa-eye'></i></div>" +
                                "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                "<td class='text-center'>" + parseFloat(dado[i]["VL_PESO_MAX"]) * 1000 + " Kg</td><td class='text-center'>" + dado[i]["VL_VOLUME_M3"] + "m³</td>" +
                                "</tr>");
                        }
                    } else {
                        $("#infoContainerWeek").append("<tr><td colspan='6' class='alert alert-light text-center'>Empty Container List</td></tr>");
                    }
                }
            });
        }

        function listarContainerWeek() {
            cntr = 0;
            $("#modalListarContainerWeek").modal("show");
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/listarContainer",
                data: '{week: "' + week + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var dado = data.d;
                    dado = $.parseJSON(dado);
                    $("#tblContainerWeek").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblContainerWeek").append("<tr>" +
                                "<td class='text-center'>" +
                                "<div class='btn btn-primary' style='margin-left: 5px;' title='Verificar Container' onclick='ListarProcessosContainer(" + dado[i]["ID_CNTR_BL"] + ")' ><i class='fas fa-eye'></i></div>" +
                                "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                "<td class='text-center'>" + parseFloat(dado[i]["VL_PESO_MAX"]) * 1000 + " Kg</td><td class='text-center'>" + dado[i]["VL_VOLUME_M3"] + "m³</td>" +
                                "</tr>");
                        }
                    }
                }
            });
        }

        function checkboxPop() {
            $(".bindBL").show();
            $("#selectionBL").hide();
            $("#cancelSelectionBL").show();
            $("#SelectionContainer").show();
        }

        function checkboxHide() {
            $("#SelectionContainer").hide();
            $(".bindBL").hide();
            $("#selectionBL").show();
            $("#cancelSelectionBL").hide();
            var bind = document.querySelectorAll(".bindBL");
            bind.forEach(function(e){
                e.checked = false;
            })
        }

        function setId(Id) {
            cntr = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function vincularContainer() {
            pacote = document.querySelectorAll('[name=vincular]:checked');
            values = [];
            for (let i = 0; i < pacote.length; i++) {
                values.push(pacote[i].value);
            }
            if (values.length > 0) {
                if (cntr != 0 && cntr != "") {
                    for (let i = 0; i < values.length; i++) {
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/vincularContainer",
                            data: '{bl:"' + values[i] + '",cntr:"' + cntr + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado == "ok") {
                                    $("#msgSuccessBindContBlFCA").fadeIn(500).delay(1000).fadeOut(500);
                                    ListarWeekInside(week);
                                } else {
                                    $("#msgErrBindContBlFCA").fadeIn(500).delay(1000).fadeOut(500);
                                }
                            }
                        })
                    }
                }
                else {
                    $("#msgerrselectcont").fadeIn(500).delay(1000).fadeOut(500);
                    ListarWeekInside(week);
                }
            } else {
                $("#msgerrselectblfca").fadeIn(500).delay(1000).fadeOut(500);
                ListarWeekInside(week);
            }
        }

        function ListarWeekInside(idweek) {
            document.getElementById("pesoTotal").textContent = "";
            document.getElementById("cubTotal").textContent = "";
            document.getElementById("mercadoriaTotal").textContent = "";
            document.getElementById("pesoTotalPartner").textContent = "";
            document.getElementById("cubTotalPartner").textContent = "";
            document.getElementById("mercadoriaTotalPartner").textContent = "";
            var pesobrutoS = 0;
            var cubagemS = 0;
            var mercadoriaS = 0;
            var pesobrutoSA = 0;
            var cubagemSA = 0;
            var mercadoriaSA = 0;   
            var oftm3S = 0;
            var exwratesS = 0;
            checkboxHide();
            listarProcessos(idweek);
            week = idweek;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarWeekInside",
                data: '{week:"' + idweek + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#infoProcessoFCA").empty();
                    $("#infoProcessoFCA").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                    $("#infoProcessoPartner").empty();
                    $("#infoProcessoPartner").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                },
                success: function (data) {
                    var OF = 0;
                    var OFTM3 = 0;
                    var EXW = 0;
                    var kg = 0;
                    var dado = data.d;
                    dado = $.parseJSON(dado);
                    $("#infoProcessoFCA").empty();
                    $("#infoProcessoPartner").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            kg = (parseFloat(dado[i]["VL_PESO_BRUTO_AGENTE"]) / 1000); 
                            if (kg > parseFloat(dado[i]["VL_M3_AGENTE"])){
                                OF = kg * parseFloat(dado[i]["VL_FRETE_VENDA"]);
                                if (OF > parseFloat(dado[i]["VL_FRETE_VENDA_MIN"])){
                                    OFTM3 = OF
                                } else {
                                    OFTM3 = dado[i]["VL_FRETE_VENDA_MIN"];
                                }
                            } else {
                                OF = parseFloat(dado[i]["VL_M3_AGENTE"]) * parseFloat(dado[i]["VL_FRETE_VENDA"]);
                                if (OF > parseFloat(dado[i]["VL_FRETE_VENDA_MIN"])){
                                    OFTM3 = OF
                                } else {
                                    OFTM3 = dado[i]["VL_FRETE_VENDA_MIN"];
                                }
                            }
                            $("#infoProcessoFCA").append("<tr>" +
                                "<td class='text-center'>" +
                                "<div><input type='checkbox' class='bindBL' value='" + dado[i]["ID_BL"] + "' name='vincular'/></div>" +
                                "</td>" +
                                "<td class='text-center'>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalEditarProcessoFCA' onclick='BuscarBLFCA(" + dado[i]["ID_BL"] + ")' ><i class='fas fa-edit'></i></div>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalDeleteProcessoWeekFCA' onclick='setRemoverProcessoWeek(" + dado[i]["ID_BL"] + ")' ><i class='fas fa-trash'></i></div>" +
                                "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["VENDEDOR"] + "'> " + dado[i]["VENDEDOR"] + "</td> " +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["AGENTE"] + "'>" + dado[i]["AGENTE"] + "</td>" +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["IMPORTADOR"] + "'> " + dado[i]["IMPORTADOR"] + "</td>" +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["EXPORTADOR"] + "'>" + dado[i]["EXPORTADOR"] + "</td>" +
                                "<td class='text-center oftm3'>" + OFTM3 +"</td>" +
                                "<td class='text-center exwrates'>" + dado[i]["VL_TAXA_VENDA"]+"</td>" +
                                "<td class='text-center pesobruto'>" + dado[i]["VL_PESO_BRUTO"] + "</td>" +
                                "<td class='text-center cubagem'>" + dado[i]["VL_M3"] + "</td>" +
                                "<td class='text-center mercadoria'>" + dado[i]["QT_MERCADORIA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["REF_AUX"] + " - " + dado[i]["REF_COM"] +"</td> " +
                                "</td></tr>");
                            $("#infoProcessoPartner").append("<tr>" +
                                "<td class='text-center'>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalEditarProcessoPartner' onclick='BuscarBLPartner(" + dado[i]["ID_BL"] + ")' ><i class='fas fa-edit'></i></div>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalDeleteProcessoWeekFCA' onclick='setRemoverProcessoWeek(" + dado[i]["ID_BL"] + ")' ><i class='fas fa-trash'></i></div>" +
                                "</td>" +
                                "<td class='text-center pesobrutoagente'>" + dado[i]["VL_PESO_BRUTO_AGENTE"] + "</td>" +
                                "<td class='text-center cubagemagente'> " + dado[i]["VL_M3_AGENTE"] + "</td> " +
                                "<td class='text-center mercadoriaagente'>" + dado[i]["QT_MERCADORIA_AGENTE"] + "</td>" +
                                "<td class='text-center'> " + dado[i]["NM_MERCADORIA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CD_INCOTERM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_READY_DATE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_FORECAST_WH"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_ARRIVE_WH"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DRAFT_CUTOFF"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CUTOFF"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_BL"] + "</td>" +
                                "</td></tr>");
                        }
                        var pesobrutoQ = document.querySelectorAll(".pesobruto");
                        var cubagemQ = document.querySelectorAll(".cubagem");
                        var mercadoriaQ = document.querySelectorAll(".mercadoria");
                        var pesobrutoQA = document.querySelectorAll(".pesobrutoagente");
                        var cubagemQA = document.querySelectorAll(".cubagemagente");
                        var mercadoriaQA = document.querySelectorAll(".mercadoriaagente");
                        var oftm3Q = document.querySelectorAll(".oftm3");
                        var exwratesQ = document.querySelectorAll(".exwrates");

                        for (let i = 0; i < pesobrutoQ.length; i++) {

                            pesobrutoS = pesobrutoS + parseFloat(pesobrutoQ[i].textContent);
                            cubagemS = cubagemS + parseFloat(cubagemQ[i].textContent);
                            mercadoriaS = mercadoriaS + parseFloat(mercadoriaQ[i].textContent);
                            pesobrutoSA = pesobrutoSA + parseFloat(pesobrutoQA[i].textContent);
                            cubagemSA = cubagemSA + parseFloat(cubagemQA[i].textContent);
                            mercadoriaSA = mercadoriaSA + parseFloat(mercadoriaQA[i].textContent);
                            oftm3S = oftm3S + parseFloat(oftm3Q[i].textContent);
                            exwratesS = exwratesS + parseFloat(exwratesQ[i].textContent)
                        }

                        document.getElementById("pesoTotal").textContent = pesobrutoS.toString().replace(".", ",");
                        document.getElementById("cubTotal").textContent = cubagemS.toString().replace(".",",");
                        document.getElementById("mercadoriaTotal").textContent = mercadoriaS;
                        document.getElementById("pesoTotalPartner").textContent = pesobrutoSA.toString().replace(".", ",");
                        document.getElementById("cubTotalPartner").textContent = cubagemSA.toString().replace(".", ",");
                        document.getElementById("mercadoriaTotalPartner").textContent = mercadoriaSA
                        $("#selectionBL").show();
                    } else {
                        $("#infoProcessoFCA").empty();
                        $("#infoProcessoFCA").append("<tr><td colspan='14' class='alert alert-light text-center'>Empty Week</td></tr>");
                        $("#infoProcessoPartner").empty();
                        $("#infoProcessoPartner").append("tr><td colspan='12' class='alert alert-light text-center'>Empty Week</td></tr>");
                        document.getElementById("pesoTotal").textContent = "";
                        document.getElementById("cubTotal").textContent = "";
                        document.getElementById("mercadoriaTotal").textContent = "";
                        document.getElementById("pesoTotalPartner").textContent = "";
                        document.getElementById("cubTotalPartner").textContent = "";
                        document.getElementById("mercadoriaTotalPartner").textContent = ""
                        $("#selectionBL").hide();
                    }
                },
            });
            
        }


        function ListarProcessosContainer(cntrID) {
            $("#modalListarProcessosContainerWeek").modal("show");
            cntr = cntrID;
            var pesobrutoSACont = 0;
            var cubagemSACont = 0;
            var mercadoriaSACont = 0;
            var pesoTotalCont = document.getElementById("pesoTotalCont");
            var volumeTotalCont = document.getElementById("cubTotalCont");
            var mercadoriaTotalCont = document.getElementById("mercadoriaTotalCont");
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/limitesContainer",
                data: '{cntr:"' + cntr + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        document.getElementById("pesoLimit").textContent = parseFloat(dado[0]["VL_PESO_MAX"]) * 1000;
                        document.getElementById("cubLimit").textContent = dado[0]["VL_VOLUME_M3"];
                    }
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarProcessoContainer",
                data: '{week:"' + week + '", cntr: "' + cntrID+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblProcessosContainerWeek").empty();
                    $("#tblProcessosContainerWeek").append("<tr><td colspan='22'><div class='loader'></div></td></tr>");
                },
                success: function (data) {
                    var OF = 0;
                    var OFTM3 = 0;
                    var EXW = 0;
                    var kg = 0;
                    var dado = data.d;
                    dado = $.parseJSON(dado);
                    $("#tblProcessosContainerWeek").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblProcessosContainerWeek").append("<tr>" +
                                "<td class='text-center'>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalDeleteProcessoContainerFCA' onclick='setRemoverProcessoContainer(" + dado[i]["ID_BL"] + ")' ><i class='fas fa-trash'></i></div>" +
                                "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'> " + dado[i]["VENDEDOR"] + "</td> " +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["AGENTE"] + "'>" + dado[i]["AGENTE"] + "</td>" +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["IMPORTADOR"] + "'> " + dado[i]["IMPORTADOR"] + "</td>" +
                                "<td class='text-center' style='max-width: 14ch;' title = '" + dado[i]["EXPORTADOR"] + "'>" + dado[i]["EXPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + OFTM3 + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_TAXA_VENDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_PESO_BRUTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_M3"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["QT_MERCADORIA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["REF_AUX"] + " - " + dado[i]["REF_COM"] + "</td> " +
                                "<td class='text-center pesobrutoagenteCont'>" + dado[i]["VL_PESO_BRUTO_AGENTE"] + "</td>" +
                                "<td class='text-center cubagemagenteCont'> " + dado[i]["VL_M3_AGENTE"] + "</td> " +
                                "<td class='text-center mercadoriaagenteCont'>" + dado[i]["QT_MERCADORIA_AGENTE"] + "</td>" +
                                "<td class='text-center'> " + dado[i]["NM_MERCADORIA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CD_INCOTERM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_READY_DATE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_FORECAST_WH"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_ARRIVE_WH"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DRAFT_CUTOFF"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CUTOFF"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_BL"] + "</td>" +
                                "</td></tr>");
                        }
                        var pesobrutoQACont = document.querySelectorAll(".pesobrutoagenteCont");
                        var cubagemQACont = document.querySelectorAll(".cubagemagenteCont");
                        var mercadoriaQACont = document.querySelectorAll(".mercadoriaagenteCont");
                        
                        for (let i = 0; i < pesobrutoQACont.length; i++) {
                            pesobrutoSACont = pesobrutoSACont + parseFloat(pesobrutoQACont[i].textContent);
                            cubagemSACont = cubagemSACont + parseFloat(cubagemQACont[i].textContent);
                            mercadoriaSACont = mercadoriaSACont + parseFloat(mercadoriaQACont[i].textContent);
                        }
                        pesoTotalCont.textContent = pesobrutoSACont;
                        volumeTotalCont.textContent = cubagemSACont;
                        mercadoriaTotalCont.textContent = mercadoriaSACont;
                        if (parseFloat(pesoTotalCont.textContent) > parseFloat(pesoLimit.textContent)) {
                            document.getElementById("boxPesoContTotal").style.background = 'rgba(255,0,0,0.3)';
                        } else {
                            document.getElementById("boxPesoContTotal").style.background = 'rgba(255,255,255,1)';
                        }

                        if (parseFloat(volumeTotalCont.textContent) > parseFloat(cubLimit.textContent)) {
                            document.getElementById("boxVolumeContTotal").style.background = 'rgba(255,0,0,0.3)';
                        } else {
                            document.getElementById("boxVolumeContTotal").style.background = 'rgba(255,255,255,1)';
                        }
                    } else {
                        $("#tblProcessosContainerWeek").empty();
                        $("#tblProcessosContainerWeek").append("tr><td colspan='22' class='alert alert-light text-center'>Empty Container</td></tr>");
                        /*document.getElementById("pesoTotal").textContent = "";
                        document.getElementById("cubTotal").textContent = "";
                        document.getElementById("mercadoriaTotal").textContent = "";
                        document.getElementById("pesoTotalPartner").textContent = "";
                        document.getElementById("cubTotalPartner").textContent = "";
                        document.getElementById("mercadoriaTotalPartner").textContent = ""*/
                    }
                },
            });

        }

        function VincularProcesso() {
            var processo = document.getElementById("ddlProcessoWeek").value;    
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/VincularProcessoWeek",
                data: '{processo: "' + processo + '", week: "' + week + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != "null") {
                        $("#modalAdicionarProcesso").modal("hide");
                        $("#msgSuccessContEditFCA").fadeIn(500).delay(1000).fadeOut(500);
                        ListarWeekInside(week);
                    } else {
                        $("#modalAdicionarProcesso").modal("hide");
                        $("#msgSuccessContEditFCA").fadeIn(500).delay(1000).fadeOut(500);
                        ListarWeekInside(week);
                    }
                }
            });
        }

        function setRemoverProcessoWeek(processo) {
            processoID = processo;            
        }

        function setRemoverProcessoContainer(processo) {
            processoIDcntr = processo;
        }



        function RemoverProcessoWeek() {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/RemoverProcessoWeek",
                data: '{processo:"' + processoID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados == "ok") {
                        $("#modalDeleteProcessoWeekFCA").modal("hide");
                        $("#msgSuccessRemoveBlFCA").fadeIn(500).delay(1000).fadeOut(500);
                        ListarWeekInside(week);
                        
                    }
                }
            })
        }

        function RemoverProcessoContainer() {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/RemoverProcessoContainer",
                data: '{processo:"' + processoIDcntr + '", cntr: "' + cntr + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados == "ok") {
                        $("#modalDeleteProcessoContainer").modal("hide");
                        $("#msgSuccessRemoveBlFCACont").fadeIn(500).delay(1000).fadeOut(500);
                        ListarWeekInside(week);
                        ListarProcessosContainer(cntr);
                    }
                }
            })
        }

        $("#btnSalvarEditContainer").click(function () {
            var week = document.getElementById("setWeek").innerHTML;
            $("#btnSalvarEditContainer").hide();
            $("#btnCancelEdit").hide();
            $("#btnEditarContainer").hide();
            $("#idWeek").hide();
            $("#idContainer").hide();
            $("#btnCadastrarContainer").show();
            var dadoEdit = {
                "ID_WEEK_CONTAINER": document.getElementById('ddlIdContainer').value,
                "ID_WEEK": document.getElementById('ddlIdWeek').value,
                "NR_CONTAINER": document.getElementById('nrContainer').value,
                "ID_TIPO_CONTAINER": document.getElementById('MainContent_ddlTipoContainer').value,
                "VL_PESO_MAX": document.getElementById('MainContent_vlPesoMax').value,
                "VL_CUBAGEM": document.getElementById('MainContent_vlCubagem').value
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarContainer",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        $("#msgSuccessContWeek").fadeIn(500).delay(1000).fadeOut(500);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarContainerWeek",
                            data: '{Id:"' + week + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdWeekContainer").empty();
                                $("#grdWeekContainer").append("<tr><td class='text-center' colspan='6'><div class='loader'></div></td></tr>");
                            },
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                if (data != null) {
                                    $("#msgEmptyWeekContainer").hide();
                                    $("#grdWeekContainer").empty();
                                    for (let i = 0; i < data.length; i++) {
                                        $("#grdWeekContainer").append("<tr><td class='text-center'>" + data[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + data[i]["NR_CONTAINER"] + "</td>" +
                                            "<td class='text-center'>" + data[i]["VL_PESO_MAX"] + "</td><td class='text-center'>" + data[i]["VL_CUBAGEM"] + "</td>" +
                                            "<td><div class='btn btn-primary pad' onclick='EditCont(" + data[i]["ID_WEEK_CONTAINER"] + ")'><i class='fas fa-edit'></i></div></td></tr> ");
                                    }
                                }
                                else {
                                    $("#grdWeekContainer").empty();
                                    $("#grdWeekContainer").append("<tr id='msgEmptyWeekContainer'><td colspan='6' class='alert alert-light text-center'>Week vazia.</td></tr>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {

                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarIdContainer",
                        data: '{week:"' + week + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var dadoId = data.d;
                            dadoId = $.parseJSON(dadoId);
                            $("#ddlIdContainer").empty();
                            if (dadoId != null) {
                                for (let i = 0; i < dadoId.length; i++) {
                                    $("#ddlIdContainer").append("<option value = '" + dadoId[i]["ID_WEEK_CONTAINER"] + "' > " + dadoId[i]["NR_CONTAINER"] + "</option >");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                }
            })
            var forms = ['ddlIdContainer',
                'MainContent_ddlTipoContainer',
                'nrContainer',
                'MainContent_vlPesoMax',
                'MainContent_vlCubagem',
                'ddlIdWeek'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.value = "";
            }
        });

        function BuscarBLFCA(processo) {
            processoID = processo;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscarBLFCA",
                data: '{processo:"' + processo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados != null) {
                        document.getElementById('MainContent_ddlVendedor').value = dados[0]["ID_PARCEIRO_VENDEDOR"];
                        document.getElementById('MainContent_ddlBroker').value = dados[0]["ID_PARCEIRO_AGENTE"];
                        document.getElementById('MainContent_ddlImporter').value = dados[0]["ID_PARCEIRO_IMPORTADOR"];
                        document.getElementById('MainContent_ddlShipper').value = dados[0]["ID_PARCEIRO_EXPORTADOR"];
                        document.getElementById('vlWeight').value = dados[0]["VL_PESO_BRUTO"].toString().replace(".",",");
                        document.getElementById('vlm3').value = dados[0]["VL_M3"].toString().replace(".", ",");
                        document.getElementById('vlPCS').value = dados[0]["QT_MERCADORIA"].toString().replace(".", ",");
                    }

                }
            })
        }

        function EditarBLFCA() {
            var vendedor = document.getElementById('MainContent_ddlVendedor').value;
            var broker = document.getElementById('MainContent_ddlBroker').value;
            var importer = document.getElementById('MainContent_ddlImporter').value;
            var shipper = document.getElementById('MainContent_ddlShipper').value;
            var weight = document.getElementById('vlWeight').value;
            var m3 = document.getElementById('vlm3').value;
            var pcs = document.getElementById('vlPCS').value;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarBLFCA",
                data: '{vendedor: "' + vendedor + '", agente:"' + broker + '", importador: "' + importer + '", exportador: "' + shipper + '", pesobruto: "' + weight + '", m3: "' + m3 + '", qtmercadoria: "' + pcs+'",processo:"' + processoID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados == "ok") {
                        $("#msgSuccessEditBlFCA").fadeIn(500).delay(1000).fadeOut(500);
                        ListarWeekInside(week);
                    }
                }
            })
        }

        function BuscarBLPartner(processo) {
            processoID = processo;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscarBLParceiro",
                data: '{processo:"' + processo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados != null) {
                        document.getElementById('vlWeightConfirmed').value = dados[0]["VL_PESO_BRUTO_AGENTE"];
                        document.getElementById('vlM3Confirmed').value = dados[0]["VL_M3_AGENTE"];
                        document.getElementById('vlPCSConfirmed').value = dados[0]["QT_MERCADORIA_AGENTE"];
                        document.getElementById('MainContent_ddlTipoMercadoria').value = dados[0]["ID_MERCADORIA"];
                        document.getElementById('MainContent_ddlIncoterm').value = dados[0]["ID_INCOTERM"];
                        document.getElementById('dtCargoReadyDate').value = dados[0]["DT_READY_DATE"];
                        document.getElementById('dtDeliveryForecastWH').value = dados[0]["DT_FORECAST_WH"];
                        document.getElementById('dtArriveWH').value = dados[0]["DT_ARRIVE_WH"];
                        document.getElementById('dtCutoff').value = dados[0]["DT_DRAFT_CUTOFF"];
                        document.getElementById('dtCargoCutoff').value = dados[0]["DT_CUTOFF"];
                        document.getElementById('nrHBL').value = dados[0]["NR_BL"];
                    }
                }
            })
        }

        function EditarBLPartner() {
            var pesobrutoagente = document.getElementById('vlWeightConfirmed').value;
            var m3agente = document.getElementById('vlM3Confirmed').value;
            var pcsagente = document.getElementById('vlPCSConfirmed').value;
            var packaging = document.getElementById('MainContent_ddlTipoMercadoria').value;
            var incoterm = document.getElementById('MainContent_ddlIncoterm').value;
            var cargoreadydate = document.getElementById('dtCargoReadyDate').value;
            var deliveryforecastwh = document.getElementById('dtDeliveryForecastWH').value;
            var datearrivewh = document.getElementById('dtArriveWH').value;
            var draftcutoff = document.getElementById('dtCutoff').value;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarBLPartner",
                data: '{pesobrutoagente: "' + pesobrutoagente + '", m3agente:"' + m3agente + '", pcsagente: "' + pcsagente + '", packaging: "' + packaging + '", incoterm: "' + incoterm + '", cargoreadydate: "' + cargoreadydate + '", deliveryforecastwh: "' + deliveryforecastwh + '",datearrivewh:"' + datearrivewh + '", draftcutoff: "' + draftcutoff + '",processo:"' + processoID+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    if (dados == "ok") {
                        $("#msgSuccessEditBlFCA").fadeIn(500).delay(1000).fadeOut(500);
                        ListarWeekInside(week);
                    }
                }
            })
        }

        function EditCont(Id) {
            var week = document.getElementById("setWeek").innerHTML;
            $("#idContainer").show();
            $("#idWeek").show();
            $("#btnCancelEdit").show();
            $("#btnEditarContainer").show();
            $("#btnCadastrarContainer").hide();

            $("#btnEditarContainer").click(function () {
                $("#btnSalvarEditContainer").show();
                $("#btnCancelEdit").show();
                $("#btnEditarContainer").hide();
                var forms = ['ddlIdContainer',
                    'ddlIdWeek',
                    'MainContent_ddlTipoContainer',
                    'nrContainer',
                    'MainContent_vlPesoMax',
                    'MainContent_vlCubagem']
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            });

            

            $("#btnCancelEdit").click(function () {
                $("#btnSalvarEditContainer").hide();
                $("#btnEditarContainer").hide();
                $("#btnCadastrarContainer").show();
                $("#btnCancelEdit").hide();
                $("#idContainer").hide();
                $("#idWeek").hide();
                var forms = ['ddlIdContainer',
                    'ddlIdWeek',
                    'MainContent_ddlTipoContainer',
                    'nrContainer',
                    'MainContent_vlPesoMax',
                    'MainContent_vlCubagem']
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                    aux.value = "";
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaContainer",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dados = dado.d;
                    dados = $.parseJSON(dados);
                    document.getElementById('ddlIdContainer').value = dados.ID_WEEK_CONTAINER;
                    document.getElementById('ddlIdWeek').value = dados.ID_WEEK;
                    document.getElementById('MainContent_vlPesoMax').value = parseFloat(dados.VL_PESO_MAX);
                    document.getElementById('MainContent_vlCubagem').value = parseFloat(dados.VL_CUBAGEM);
                    document.getElementById('nrContainer').value = dados.NR_CONTAINER;
                    document.getElementById('MainContent_ddlTipoContainer').value = dados.ID_TIPO_CONTAINER;



                    var forms = ['ddlIdContainer','MainContent_ddlTipoContainer','nrContainer','MainContent_vlPesoMax','MainContent_vlCubagem','ddlIdWeek'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        }


        $("#nrContainer").focus(function () {
            $("#nrContainer").css('background', '#fff');
            $(".erroNrContainer").hide();
            $("#btnCadastrarContainer").show();
        })


        function Calculo_Digito_Conteiner() {
            var Conteiner = document.getElementById("nrContainer").value;
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
            if (calcula == cleanConteiner.substr(cleanConteiner.length - 1, 1)) {
                $("#nrContainer").css('background', '#fff');
                $(".erroNrContainer").hide();
                CadastrarContainer();
            }
            else {
                $("#nrContainer").css('background', '#ffdfd4');
                $(".erroNrContainer").show();
                $("#msgErrRegContBlFCA").fadeIn(500).delay(1000).fadeOut(500);
            }
        }

        function BuscarWeek(Id) {
            $("#btnSalvarWeek").hide();
            $("#btnEditarWeek").show();
            $("#btnSalvarEditWeek").hide();
            $("#btnCancelWeek").hide();
            $("#listWeek").show();
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaWeek",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('MainContent_ddlParceiroWeek').value = data.ID_PARCEIRO;
                    document.getElementById('ddlWeek').value = Id;
                    document.getElementById('MainContent_txtReferencia').value = data.NM_WEEK;
                    document.getElementById('MainContent_ddlPortoLocal').value = data.ID_PORTO_ORIGEM_LOCAL;
                    document.getElementById('MainContent_ddlPortoDestino').value = data.ID_PORTO_ORIGEM_DESTINO;
                    document.getElementById('MainContent_txtMBL').value = data.NM_MBL;
                    document.getElementById('MainContent_txtVessel').value = data.NM_VESSEL;
                    document.getElementById('MainContent_dtCutOff').value = data.DT_CUTOFF;
                    document.getElementById('MainContent_dtETD').value = data.DT_ETD;
                    document.getElementById('MainContent_dtETA').value = data.DT_ETA;
                    document.getElementById('MainContent_nrFreight').value = parseFloat(data.NR_FRIGHT);
                    document.getElementById('MainContent_nrFreetime').value = data.NR_FREETIME;


                    var forms = ['MainContent_ddlParceiroWeek','MainContent_txtReferencia',
                                'MainContent_ddlPortoLocal',
                                'MainContent_ddlPortoDestino',
                                'MainContent_txtMBL',
                                'MainContent_txtVessel',
                                'MainContent_dtCutOff',
                                'MainContent_dtETD',
                                'MainContent_dtETA',
                                'MainContent_nrFreight',
                                'MainContent_nrFreetime'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
    </script>
</asp:Content>
