<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessosBL.aspx.cs" Inherits="ABAINFRA.Web.ProcessosBL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Consolidado Week 44
                    </h3>
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
                            <div class="modal fade bd-example-modal-xl" id="modalContainerFCAInside" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalContainerTitle">Week</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-10 col-sm-offset-1">
                                                    <div class="alert alert-success text-center" id="msgSuccessContEditFCA">
                                                        Registro cadastrado/atualizado com sucesso!
                                                    </div>
                                                    <div class="alert alert-danger text-center" id="msgErrContEditFCA">
                                                        Erro ao cadastrar/atualizar.
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive tableFixHead">
                                                <table class="table tablecont">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center" scope="col">Status</th>
                                                            <th class="text-center" scope="col">Follow Up</th>
                                                            <th class="text-center" scope="col">System Reference</th>
                                                            <th class="text-center" scope="col">Sales</th>
                                                            <th class="text-center" scope="col">Broker(Cnee)</th>
                                                            <th class="text-center" scope="col">Importer(Brazil)</th>
                                                            <th class="text-center" scope="col">Shipper</th>
                                                            <th class="text-center" scope="col">Weight(kg)</th>
                                                            <th class="text-center" scope="col">M3</th>
                                                            <th class="text-center" scope="col">PCS</th>
                                                            <th class="text-center" scope="col"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="infoProcessoFCA">
                                                    </tbody>
                                                    <tfoot id="infoSomaProcessoFCA">
                                                    </tfoot>
                                                </table>
                                            </div>
                                            <div class="row topMarg">
                                               <div id="boxPesoFCA" class="col-sm-3 text-center"  style="font-size: 14px;">Peso Máximo Permitido: <span id="pesoMax" style="font-weight:bold"></span> Kg</div>
                                               <div id="boxVolumeFCA" class="col-sm-3 text-center"  style="font-size: 14px;">Cubagem Máxima Permitida: <span id="cubMax" style="font-weight:bold"></span> m³</div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal fade bd-example-modal-xl" id="modalContainerFCAInsideEdit" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalContainerEditTitle">Editar Processo</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Status</label>
                                                         <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataValueField="ID_STATUS_BL" DataTextField="NM_STATUS_BL">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-success" data-dismiss="modal" data-target="#modalContainerFCAInside" data-toggle="modal"  onclick="EditarProcessoFCA()">Salvar Edição</button>
                                            <button type="button" class="btn btn-warning" data-dismiss="modal" data-target="#modalContainerFCAInside" data-toggle="modal">Voltar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive tableFixHead topMarg">
                                <table class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">Tipo Container</th>
                                            <th class="text-center" scope="col">Nº Container</th>
                                            <th class="text-center" scope="col">Peso Máximo</th>
                                            <th class="text-center" scope="col">Cubagem</th>
                                            <th class="text-center" scope="col">&nbsp</th>
                                        </tr>
                                    </thead>
                                    <tbody id="container-list-FCA">
                                       
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="processoRealGrid">
                            <div class="modal fade bd-example-modal-xl" id="modalContainerPartnerInside" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalContainerPartnerTitle">Container - <span id="idContainerPartner"></span></h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-10 col-sm-offset-1">
                                                    <div class="alert alert-success text-center" id="msgSuccessContEditPartner">
                                                        Registro cadastrado/atualizado com sucesso!
                                                    </div>
                                                    <div class="alert alert-danger text-center" id="msgErrContEditPartner">
                                                        Erro ao cadastrar/atualizar.
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive tableFixHead">
                                                <table class="table table-striped tablecont">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center" scope="col">-</th>
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
                                                            <th class="text-center" scope="col"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="infoProcessoPartner">
                                                    </tbody>
                                                    <tfoot id="infoSomaProcessoPartner">
                                                    </tfoot>
                                                </table>
                                            </div>
                                            <div class="row topMarg">
                                               <div id="boxPesoPartner" class="col-sm-3 text-center" style="font-size: 14px;">Max. Weight Allowed: <span id="pesoMaxPartner" style="font-weight:bold"></span> Kg</div>
                                               <div id="boxVolumePartner" class="col-sm-3 text-center" style="font-size: 14px;" >Max. M³ Allowed: <span id="cubMaxPartner" style="font-weight:bold"></span> m³</div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal fade bd-example-modal-xl" id="modalContainerPartnerInsideEdit" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalContainerPartnerEditTitle">Editar Processo <span id="idProcesso"></span> - <span id="nrProcesso"></span></h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-3" id="boxvlWeightAgente">
                                                    <div class="form-group">
                                                        <label class="control-label">Weight(kg)</label>
                                                        <input type="text" id="vlWeightAgente" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" id="boxvlM3">
                                                    <div class="form-group">
                                                        <label class="control-label">M3</label>
                                                        <input id="vlM3" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">PCS</label>
                                                        <input type="text" id="qtPcs" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Packaging</label>
                                                        <asp:DropDownList ID="ddlMercadoria" runat="server" CssClass="form-control" DataValueField="ID_MERCADORIA" DataTextField="NM_MERCADORIA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Terms</label>
                                                        <asp:DropDownList ID="ddlTerms" runat="server" CssClass="form-control" DataValueField="ID_INCOTERM" DataTextField="DATATEXT">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Cargos Ready Date</label>
                                                        <input id="dtCargoReady" type="date" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Delivery Forecast in WH</label>
                                                        <input id="dtDeliveryForecast" type="date" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Date of Arrive in WH</label>
                                                        <input id="dtArriveWH" type="date" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Draft Cut Off</label>
                                                        <input id="dtDrafCutOff" type="date" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL</label>
                                                        <input id="txtHBL" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-success" data-dismiss="modal" data-target="#modalContainerPartnerInside" data-toggle="modal" onclick="EditarProcessoPartner()">Salvar Edição</button>
                                            <button type="button" class="btn btn-danger" data-dismiss="modal" data-target="#modalContainerPartnerInside" data-toggle="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
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
                                    <tbody id="container-list-Partner">
                                    </tbody>
                                </table>
                            </div>
                        </div>
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
    <script type="text/javascript">

        $(document).ready(function () {
            var pesoPartner;
            var cubPartner;
            var cargaPartner;
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarContainerWeek",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#container-list-FCA").empty();
                    $("#container-list-FCA").append("<tr><td class='text-center' colspan='5'><div class='loader'></div ></td ></tr > ");
                },
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != null) {
                        $("#container-list-FCA").empty();
                        $("#container-list-Partner").empty();
                        for (let i = 0; i < data.length; i++) {
                            $("#container-list-FCA").append("<tr><td class='text-center'>" + data[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + data[i]["NR_CONTAINER"] + "</td>" +
                                "<td class='text-center'>" + data[i]["VL_PESO_MAX"] + "</td><td class='text-center'>" + data[i]["VL_CUBAGEM"] + "</td>" +
                                "<td class='text-center'><div class='btn btn-primary pad' data-toggle='modal' data-target='#modalContainerFCAInside' onclick='BuscarProcessoBLFCA(" + id + "," + data[i]["ID_WEEK_CONTAINER"] + ")'><i class='fas fa-eye'></i></div></td></tr> ");
                            $("#container-list-Partner").append("<tr><td class='text-center'>" + data[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + data[i]["NR_CONTAINER"] + "</td>" +
                                "<td class='text-center'>" + data[i]["VL_PESO_MAX"] + "</td><td class='text-center'>" + data[i]["VL_CUBAGEM"] + "</td>" +
                                "<td class='text-center'><div class='btn btn-primary pad' data-toggle='modal' data-target='#modalContainerPartnerInside' onclick='BuscarProcessoBLPartner(" + id + "," + data[i]["ID_WEEK_CONTAINER"] + ")'><i class='fas fa-eye'></i></div></td></tr> ");
                        }
                    }
                    else {
                        $("#container-list-FCA").empty();
                        $("#container-list-FCA").append("<tr id='msgEmptyWeekContainer'><td colspan='6' class='alert alert-light text-center'>Week vazia.</td></tr>");
                        $("#container-list-Partner").empty();
                        $("#container-list-Partner").append("<tr id='msgEmptyWeekContainer'><td colspan='7' class='alert alert-light text-center'>Week vazia.</td></tr>");
                    }
                },
                error: function (data) {

                },
            });
           
        });

        function BuscarProcessoBLPartner(Id, IdCont) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarProcessosBL",
                data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#infoProcessoPartner").empty();
                    $("#infoProcessoPartner").append("<tr><td class='text-center' colspan='12'><div class='loader text-center'></div></td></tr>");
                    $("#boxPesoPartner").css("backgroundColor", "white");
                    $("#boxVolumePartner").css("backgroundColor", "white");
                    $("#pesoMaxPartner").empty();
                    $("#cubMaxPartner").empty();
                    $("#infoSomaProcessoPartner").empty();
                    $("#infoSomaProcessoPartner").append("<tr><td class='text-center'>Total:</td><td class='text-center' id='cargaPartner'> ---- </td><td class='text-center'> --- </td><td class='text-center'> --- </td><td></td><td></td><td></td><td></td><td></td><td></td><td><td></td><td></td>");
                },
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != null) {
                        $("#infoProcessoPartner").empty();
                        for (let i = 0; i < data.length; i++) {
                            $("#infoProcessoPartner").append("<tr><td class='text-center'>-</td><td class='text-center'>" + data[i]["VL_PESO_BRUTO_AGENTE"] + " Kg</td><td class='text-center'>" + data[i]["VL_M3_AGENTE"] + " m³</td>" +
                                "<td class='text-center'>" + data[i]["QT_MERCADORIA_AGENTE"] + "</td><td class='text-center'>" + data[i]["NM_MERCADORIA"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CD_INCOTERM"] + "</td><td class='text-center'>" + data[i]["DT_READY_DATE"] + "</td><td class='text-center'>" + data[i]["DT_FORECAST_WH"] + "</td>" +
                                "<td class='text-center'>" + data[i]["DT_ARRIVE_WH"] + "</td> <td class='text-center'>" + data[i]["DT_DRAFT_CUTOFF"] + "</td><td class='text-center'>" + data[i]["DT_CUTOFF"] + "</td><td class='text-center'>" + data[i]["NR_BL"] + "</td><td class='text-center'><div class='btn btn-primary pad' data-dismiss='modal' data-target='#modalContainerPartnerInsideEdit' data-toggle='modal' onclick='BuscarInfoProcessoPartner(" + data[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></td></div></td></tr> ");
                        }
                        SomaProcessoBLPartner(Id, IdCont);
                        PesoLimitePartner(Id, IdCont);
                    }
                    else {
                        $("#infoProcessoPartner").empty();
                        $("#infoProcessoPartner").append("<tr><td class='text-center' colspan='12'>Container Vazio</td><td></td></tr>");
                        SomaProcessoBLPartner(Id, IdCont);
                        PesoLimitePartner(Id, IdCont);
                    }
                    $("#idContainerPartner").empty();
                    $("#idContainerPartner").append(IdCont);
                },
                error: function (data) {

                },
            });
        }
        function BuscarProcessoBLFCA(Id, IdCont) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarProcessosBL",
                data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#infoProcessoFCA").empty();
                    $("#infoProcessoFCA").append("<tr><td class='text-center' colspan='11'><div class='loader text-center'></div></td></tr>");
                    $("#boxPesoFCA").css("backgroundColor", "white");
                    $("#boxVolumeFCA").css("backgroundColor", "white");
                    $("#pesoMax").empty();
                    $("#cubMax").empty();
                    $("#infoSomaProcessoFCA").empty();
                    $("#infoSomaProcessoFCA").append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td class='text-center'>Total:</td><td class='text-center' id='cargaFCA'> ---- </td><td class='text-center'> --- </td><td class='text-center'> --- </td><td></td></tr>");
                },
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != null) {
                        $("#infoProcessoFCA").empty();
                        for (let i = 0; i < data.length; i++) {
                            $("#infoProcessoFCA").append("<tr><td class='text-center'>" + data[i]["NM_STATUS_BL"] + "</td><td class='text-center'>" + data[i]["DT_FLWP_LCL"] + "</td><td class='text-center'>" + data[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + data[i]["VENDEDOR"] + "</td><td class='text-center'>" + data[i]["AGENTE"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CLIENTE"] + "</td><td class='text-center'>" + data[i]["EXPORTADOR"] + "</td><td class='text-center'>" + data[i]["VL_PESO_BRUTO"] + " Kg</td>" +
                                "<td class='text-center'>" + data[i]["VL_M3"] + " m³</td> <td class='text-center'>" + data[i]["QT_MERCADORIA"] + "</td><td class='text-center'><div class='btn btn-primary pad' data-dismiss='modal' data-target='#modalContainerFCAInsideEdit' data-toggle='modal' onclick='BuscarInfoProcessoFCA(" + data[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></td></div></tr> ");
                        }
                        SomaProcessoBLFCA(Id, IdCont);
                        PesoLimiteFCA(Id, IdCont);
                    }
                    else {
                        $("#infoProcessoFCA").empty();
                        $("#infoProcessoFCA").append("<tr><td class='text-center' colspan='11'>Container Vazio</td></tr>");
                        SomaProcessoBLFCA(Id, IdCont);
                        PesoLimiteFCA(Id, IdCont);
                    }
                    
                },
                error: function (data) {

                },
            });
        }

        function SomaProcessoBLFCA(Id, IdCont) {
            
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarSomaProcessosBL",
                data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != null) {
                        $("#infoSomaProcessoFCA").empty();
                        $("#infoSomaProcessoFCA").append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td class='text-center'>Total:</td><td class='text-center' id='cargaFCA'>" + data[0]["SUM_PESO_BRUTO"] + " Kg</td><td class='text-center'>" + data[0]["SUM_VL_M3"] + " m³</td><td class='text-center'>" + data[0]["SUM_QT_MERCADORIA"] + "</td><td></td></tr>");
                    }
                    else {
                        $("#infoSomaProcessoFCA").empty();
                        $("#infoSomaProcessoFCA").append("<tr><td class='text-center'>Total:</td><td id='cargaFCA' class='text-center'> 0 </td><td class='text-center'> 0 </td><td class='text-center'> 0 </td></tr>");
                    }
                    console.log();
                    if (document.getElementById("pesoMax").value < document.getElementById("cargaFCA")) {
                        document.getElementById("pesoMax").style("color", "red");
                    }
                },
                error: function (data) {

                },
            });
        }
        function SomaProcessoBLPartner(Id, IdCont) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarSomaProcessosBLPartner",
                data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != null) {
                        $("#infoSomaProcessoPartner").empty();
                        $("#infoSomaProcessoPartner").append("<tr><td class='text-center'>Total:</td><td class='text-center' id='cargaPartner'>" + data[0]["SUM_PESO_BRUTO_AGENTE"] + " Kg</td><td class='text-center'>" + data[0]["SUM_VL_M3_AGENTE"] + " m³</td><td class='text-center'>" + data[0]["SUM_QT_MERCADORIA_AGENTE"] + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td><td></td><td></td>");
                    }
                    else {
                        $("#infoSomaProcessoPartner").empty();
                        $("#infoSomaProcessoPartner").append("<tr><td class='text-center'>Total:</td><td class='text-center' id='cargaPartner'> 0 </td><td class='text-center'> 0 </td><td class='text-center'> 0 </td><td></td>");
                    }
                },
                error: function (data) {

                },
            });
        }

        function BuscarInfoProcessoPartner(Id) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscarInfoProcessoPartner",
                data: '{Id:"' + Id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#idProcesso").empty();
                    $("#nrProcesso").empty();
                    if (data != null) {
                        $("#idProcesso").append(data.ID_BL);
                        $("#nrProcesso").append(data.ID_WEEK_CONTAINER);
                        document.getElementById('vlWeightAgente').value = data.VL_PESO_BRUTO_AGENTE;
                        document.getElementById('vlM3').value = data.VL_M3_AGENTE;
                        document.getElementById('qtPcs').value = data.QT_MERCADORIA_AGENTE;
                        document.getElementById('MainContent_ddlMercadoria').value = data.ID_MERCADORIA;
                        document.getElementById('MainContent_ddlTerms').value = data.ID_INCOTERM;
                        document.getElementById('dtCargoReady').value = data.DT_READY_DATE;
                        document.getElementById('dtDeliveryForecast').value = data.DT_FORECAST_WH;
                        document.getElementById('dtArriveWH').value = data.DT_ARRIVE_WH;
                        document.getElementById('dtDrafCutOff').value = data.DT_DRAFT_CUTOFF;
                        document.getElementById('txtHBL').value = data.NR_BL;

                    }
                    else {
                        $("#infoSomaProcessoPartner").empty();
                        $("#infoSomaProcessoPartner").append("<tr><td class='text-center'>Total:</td><td class='text-center'> - </td><td class='text-center'> - </td><td class='text-center'> - </td><td></td>");
                    }
                },
                error: function (data) {

                },
            });
        }
        function BuscarInfoProcessoFCA(Id) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscarInfoProcessoFCA",
                data: '{Id:"' + Id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d; 
                    data = $.parseJSON(data);
                    $("#idProcesso").empty();
                    $("#nrProcesso").empty();
                    if (data != null) {
                        $("#idProcesso").append(data.ID_BL);
                        $("#nrProcesso").append(data.ID_WEEK_CONTAINER);
                        document.getElementById("MainContent_ddlStatus").value = data.ID_STATUS_BL;
                    }
                    else {
                        $("#infoSomaProcessoFCA").empty();
                        $("#infoSomaProcessoFCA").append("<tr><td class='text-center'>Total:</td><td class='text-center'> - </td><td class='text-center'> - </td><td class='text-center'> - </td><td></td>");
                    }
                },
                error: function (data) {

                },
            });
        }

        function EditarProcessoPartner() {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var idWeek = parseInt(id_parceiro);
            var Id = document.getElementById("idProcesso").innerHTML;
            var IdCont = document.getElementById("nrProcesso").innerHTML;
            var dadoEdit = {
                "ID_BL": Id,
                "VL_PESO_BRUTO_AGENTE": document.getElementById('vlWeightAgente').value,
                "VL_M3_AGENTE": document.getElementById('vlM3').value,
                "QT_MERCADORIA_AGENTE": document.getElementById('qtPcs').value,
                "ID_MERCADORIA": document.getElementById('MainContent_ddlMercadoria').value,
                "ID_INCOTERM": document.getElementById('MainContent_ddlTerms').value,
                "DT_READY_DATE": document.getElementById('dtCargoReady').value,
                "DT_FORECAST_WH": document.getElementById('dtDeliveryForecast').value,
                "DT_ARRIVE_WH": document.getElementById('dtArriveWH').value,
                "DT_DRAFT_CUTOFF": document.getElementById('dtDrafCutOff').value,
                "NR_BL": document.getElementById('txtHBL').value
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarProcessoPartner",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 1) {
                        $("#msgSuccessContEditPartner").fadeIn(500).delay(1000).fadeOut(500);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarProcessosBL",
                            data: '{Id:"' + idWeek + '",IdCont:"' + IdCont + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#infoProcessoPartner").empty();
                                $("#infoProcessoPartner").append("<tr><td class='text-center' colspan='11'><div class='loader text-center'></div></td></tr>");
                                $("#boxPesoPartner").css("backgroundColor", "white");
                                $("#boxVolumePartner").css("backgroundColor", "white");
                                $("#infoSomaProcessoPartner").empty();
                                $("#infoSomaProcessoPartner").append("<tr><td class='text-center'>Total:</td><td class='text-center' id='cargaPartner'> ---- </td><td class='text-center'> --- </td><td class='text-center'> --- </td><td></td><td></td><td></td><td></td><td></td><td></td><td><td></td><td></td>");
                            },
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                if (data != null) {
                                    $("#infoProcessoPartner").empty();
                                    for (let i = 0; i < data.length; i++) {
                                        $("#infoProcessoPartner").append("<tr><td class='text-center'>-</td><td class='text-center'>" + data[i]["VL_PESO_BRUTO_AGENTE"] + " Kg</td><td class='text-center'>" + data[i]["VL_M3_AGENTE"] + " m³</td>" +
                                            "<td class='text-center'>" + data[i]["QT_MERCADORIA_AGENTE"] + "</td><td class='text-center'>" + data[i]["NM_MERCADORIA"] + "</td>" +
                                            "<td class='text-center'>" + data[i]["CD_INCOTERM"] + "</td><td class='text-center'>" + data[i]["DT_READY_DATE"] + "</td><td class='text-center'>" + data[i]["DT_FORECAST_WH"] + "</td>" +
                                            "<td class='text-center'>" + data[i]["DT_ARRIVE_WH"] + "</td> <td class='text-center'>" + data[i]["DT_DRAFT_CUTOFF"] + "</td><td class='text-center'>" + data[i]["DT_CUTOFF"] + "</td><td class='text-center'>" + data[i]["NR_BL"] + "</td><td class='text-center'><div class='btn btn-primary pad' data-dismiss='modal' data-target='#modalContainerPartnerInsideEdit' data-toggle='modal' onclick='BuscarInfoProcessoPartner(" + data[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></td></div></td></tr> ");
                                    }
                                    SomaProcessoBLPartner(idWeek, IdCont);
                                    PesoLimitePartner(idWeek, IdCont);
                                }
                                else {
                                    $("#infoProcessoPartner").empty();
                                    $("#infoProcessoPartner").append("<tr><td class='text-center' colspan='12'>Container Vazio</td><td></td></tr>");
                                    SomaProcessoBLPartner(idWeek, IdCont);
                                    PesoLimitePartner(idWeek, IdCont);
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrContEditPartner").fadeIn(500).delay(1000).fadeOut(500);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarProcessosBL",
                            data: '{Id:"' + idWeek + '",IdCont:"' + IdCont + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#infoProcessoPartner").empty();
                                $("#infoProcessoPartner").append("<tr><td class='text-center' colspan='11'><div class='loader text-center'></div></td></tr>");
                                $("#boxPesoPartner").css("backgroundColor", "white");
                                $("#boxVolumePartner").css("backgroundColor", "white");
                                $("#infoSomaProcessoPartner").empty();
                                $("#infoSomaProcessoPartner").append("<tr><td class='text-center'>Total:</td><td class='text-center' id='cargaPartner'> ---- </td><td class='text-center'> --- </td><td class='text-center'> --- </td><td></td><td></td><td></td><td></td><td></td><td></td><td><td></td><td></td>");
                            },
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                if (data != null) {
                                    $("#infoProcessoPartner").empty();
                                    for (let i = 0; i < data.length; i++) {
                                        $("#infoProcessoPartner").append("<tr><td class='text-center'>-</td><td class='text-center'>" + data[i]["VL_PESO_BRUTO_AGENTE"] + " Kg</td><td class='text-center'>" + data[i]["VL_M3_AGENTE"] + " m³</td>" +
                                            "<td class='text-center'>" + data[i]["QT_MERCADORIA_AGENTE"] + "</td><td class='text-center'>" + data[i]["NM_MERCADORIA"] + "</td>" +
                                            "<td class='text-center'>" + data[i]["CD_INCOTERM"] + "</td><td class='text-center'>" + data[i]["DT_READY_DATE"] + "</td><td class='text-center'>" + data[i]["DT_FORECAST_WH"] + "</td>" +
                                            "<td class='text-center'>" + data[i]["DT_ARRIVE_WH"] + "</td> <td class='text-center'>" + data[i]["DT_DRAFT_CUTOFF"] + "</td><td class='text-center'>" + data[i]["DT_CUTOFF"] + "</td><td class='text-center'>" + data[i]["NR_BL"] + "</td><td class='text-center'><div class='btn btn-primary pad' data-dismiss='modal' data-target='#modalContainerPartnerInsideEdit' data-toggle='modal' onclick='BuscarInfoProcessoPartner(" + data[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></td></div></td></tr> ");
                                    }
                                    SomaProcessoBLPartner(idWeek, IdCont);
                                    PesoLimitePartner(idWeek, IdCont);
                                }
                                else {
                                    $("#infoProcessoPartner").empty();
                                    $("#infoProcessoPartner").append("<tr><td class='text-center' colspan='12'>Container Vazio</td><td></td></tr>");
                                    SomaProcessoBLPartner(idWeek, IdCont);
                                    PesoLimitePartner(idWeek, IdCont);
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                },
                error: function () {
                    $("#msgErrContEditPartner").fadeIn(500).delay(1000).fadeOut(500);
                }
            });
        }
        function EditarProcessoFCA() {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var idWeek = parseInt(id_parceiro);
            var Id = document.getElementById("idProcesso").innerHTML;
            var IdCont = document.getElementById("nrProcesso").innerHTML;
            var dadoEdit = {
                "ID_BL": Id,
                "ID_STATUS_BL": document.getElementById("MainContent_ddlStatus").value
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarProcessoFCA",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    $("#msgSuccessContEditFCA").fadeIn(500).delay(1000).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarProcessosBL",
                        data: '{Id:"' + idWeek + '",IdCont:"' + IdCont + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#infoProcessoFCA").empty();
                            $("#infoProcessoFCA").append("<tr><td class='text-center' colspan='11'><div class='loader text-center'></div></td></tr>");
                            $("#boxPesoFCA").css("backgroundColor", "white");
                            $("#boxVolumeFCA").css("backgroundColor", "white");
                            $("#infoSomaProcessoFCA").empty();
                            $("#infoSomaProcessoFCA").append("<tr><td class='text-center'>Total:</td><td class='text-center' id='cargaPartner'> ---- </td><td class='text-center'> --- </td><td class='text-center'> --- </td><td></td><td></td><td></td><td></td><td></td><td></td><td><td></td><td></td>");
                        },
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            if (data != null) {
                                $("#infoProcessoFCA").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#infoProcessoFCA").append("<tr><td class='text-center'>" + data[i]["NM_STATUS_BL"] + "</td><td class='text-center'>" + data[i]["DT_FLWP_LCL"] + "</td><td class='text-center'>" + data[i]["NR_PROCESSO"] + "</td>" +
                                        "<td class='text-center'>" + data[i]["VENDEDOR"] + "</td><td class='text-center'>" + data[i]["AGENTE"] + "</td>" +
                                        "<td class='text-center'>" + data[i]["CLIENTE"] + "</td><td class='text-center'>" + data[i]["EXPORTADOR"] + "</td><td class='text-center'>" + data[i]["VL_PESO_BRUTO"] + " Kg</td>" +
                                        "<td class='text-center'>" + data[i]["VL_M3"] + " m³</td> <td class='text-center'>" + data[i]["QT_MERCADORIA"] + "</td><td class='text-center'><div class='btn btn-primary pad' data-dismiss='modal' data-target='#modalContainerFCAInsideEdit' data-toggle='modal' onclick='BuscarInfoProcessoFCA(" + data[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></td></div></tr> ");
                                }
                                SomaProcessoBLFCA(idWeek, IdCont);
                            }
                            else {
                                $("#infoProcessoFCA").empty();
                                $("#infoProcessoFCA").append("<tr><td class='text-center' colspan='11'>Container Vazio</td></tr>");
                                SomaProcessoBLFCA(idWeek, IdCont);
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function () {
                    $("#msgErrContEditFCA").fadeIn(500).delay(1000).fadeOut(500);
                }
            });
        }

        function PesoLimitePartner(Id, IdCont) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/PesagemMax",
                data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#pesoMaxPartner").empty();
                    $("#cubMaxPartner").empty();
                },
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#pesoMaxPartner").empty();
                    $("#pesoMaxPartner").append(data[0]["VL_PESO_MAX"]);
                    $("#cubMaxPartner").empty();
                    $("#cubMaxPartner").append(data[0]["VL_CUBAGEM"]);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarSomaProcessosBLPartner",
                        data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            if (data != null) {
                                if (data[0]["SUM_PESO_BRUTO_AGENTE"] > document.getElementById("pesoMaxPartner").innerHTML) {
                                    $("#boxPesoPartner").css("backgroundColor", "rgba(255,99,71,0.5)");
                                }
                                else {
                                    $("#boxPesoPartner").css("backgroundColor", "rgba(127,255,0,0.5)");
                                }

                                if (data[0]["SUM_VL_M3_AGENTE"] > document.getElementById("cubMaxPartner").innerHTML) {
                                    $("#boxVolumePartner").css("backgroundColor", "rgba(255,99,71,0.5)");
                                }
                                else {
                                    $("#boxVolumePartner").css("backgroundColor", "rgba(127,255,0,0.5)");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {

                },
            });
            
        }
        function PesoLimiteFCA(Id, IdCont) {
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/PesagemMax",
                data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#pesoMax").empty();
                    $("#cubMax").empty();
                },
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#pesoMax").empty();
                    $("#pesoMax").append(data[0]["VL_PESO_MAX"]);
                    $("#cubMax").empty();
                    $("#cubMax").append(data[0]["VL_CUBAGEM"]);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarSomaProcessosBL",
                        data: '{Id:"' + Id + '",IdCont:"' + IdCont + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            if (data != null) {
                                if (data[0]["SUM_PESO_BRUTO"] > document.getElementById("pesoMax").innerHTML) {
                                    $("#boxPesoFCA").css("backgroundColor", "rgba(255,99,71,0.5)");
                                }
                                else {
                                    $("#boxPesoFCA").css("backgroundColor", "rgba(127,255,0,0.5)");
                                }

                                if (data[0]["SUM_VL_M3"] > document.getElementById("cubMax").innerHTML) {
                                    $("#boxVolumeFCA").css("backgroundColor", "rgba(255,99,71,0.5)");
                                }
                                else {
                                    $("#boxVolumeFCA").css("backgroundColor", "rgba(127,255,0,0.5)");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {

                },
            });
            
        }

    </script>
</asp:Content>
