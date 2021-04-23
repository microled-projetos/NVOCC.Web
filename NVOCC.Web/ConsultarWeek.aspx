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
                                                <label class="control-label">Referência</label>
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
                                                <asp:TextBox ID="txtMBL" runat="server" required="True" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-9">
                                            <div class="form-group">
                                                <label class="control-label">Vessel</label>
                                                <asp:TextBox ID="txtVessel" runat="server" required="True" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Cut Off <span class="required">*</span></label>
                                                <asp:TextBox ID="dtCutOff" runat="server" required="True" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">ETD</label>
                                                <asp:TextBox ID="dtETD" runat="server" required="True" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">ETA</label>
                                                <asp:TextBox ID="dtETA" runat="server" required="True" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Freight</label>
                                                <asp:TextBox ID="nrFreight" runat="server" TextMode="Number" CssClass="form-control">
                                                </asp:TextBox>
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
                    <div class="modal fade bd-example-modal-lg" id="modalContainer" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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
                    </div>
                    <div class="table-responsive tableFixHead">
                        <table class="table">
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
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarWeek",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#msgSuccessWeek").fadeIn(500).delay(1000).fadeOut(500);
                                    $("#grdWeek").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#grdWeek").append("<tr>" +
                                            "<td class='text-center'>" + dado[i]["NM_WEEK"] + "</td><td class='text-center'>" + dado[i]["NMPORTOORIGEM"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NMPORTODESTINO"] + "</td><td class='text-center'>" + dado[i]["DT_ETD"] + "</td><td class='text-center'>" + dado[i]["DT_CUTOFF"] + "</td><td class='text-center'><a href='ProcessosBL.aspx?id=" + dado[i]["ID_WEEK"] + "'><div class='btn btn-primary' data-toggle='modal' data-target='#modalWeek' onclick='BuscarWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-eye'></i></div></a>" +
                                            "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalWeek' onclick='BuscarWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-edit'></i></div>" +
                                            "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalContainer' onclick='referWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-plus'></i></div>" +
                                            "</td></tr> ");
                                    }
                                }
                                else {
                                    $("#grdWeek").empty();
                                    $("#grdWeek").append("<tr id='msgEmptyWeek'><td colspan='6' class='alert alert-light text-center'>Esse parceiro não possui taxas.</td></tr>");
                                }
                            }
                        })
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
                                    $("#ddlWeek").append("<option value='" + data[i]["ID_WEEK"] + "'>" + data[i]["NM_WEEK"] + " - " + data[i]["NMPORTOORIGEM"] + " - " + data[i]["NMPORTODESTINO"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
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
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarWeek",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#msgSuccessWeek").fadeIn(500).delay(1000).fadeOut(500);
                                    $("#grdWeek").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#grdWeek").append("<tr>" +
                                            "<td class='text-center'>" + dado[i]["NM_WEEK"] + "</td><td class='text-center'>" + dado[i]["NMPORTOORIGEM"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NMPORTODESTINO"] + "</td><td class='text-center'>" + dado[i]["DT_ETD"] + "</td><td class='text-center'>" + dado[i]["DT_CUTOFF"] + "</td>" +
                                            "<td class='text-center'><a href='ProcessosBL.aspx?id=" + dado[i]["ID_WEEK"] + "'><div class='btn btn-primary'><i class='fas fa-eye'></i></div></a><div class='btn btn-primary pad' data-toggle='modal' data-target='#modalWeek' onclick='BuscarWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-edit'></i></div>" +
                                            "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalContainer' onclick='referWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-plus'></i></div>" +
                                            "</td></tr> ");
                                    }
                                }
                                else {
                                    $("#grdWeek").empty();
                                    $("#grdWeek").append("<tr id='msgEmptyWeek'><td colspan='6' class='alert alert-light text-center'>Esse parceiro não possui taxas.</td></tr>");
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
                        });
                    }
                    else {
                        $("#msgErrWeek").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            });
        })


        $(document).ready(function () {
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
                                "<td class='text-center'><a href='ProcessosBL.aspx?id=" + dado[i]["ID_WEEK"] + "'><div class='btn btn-primary'><i class='fas fa-eye'></i></div></a><div class='btn btn-primary pad' data-toggle='modal' data-target='#modalWeek' onclick='BuscarWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-edit'></i></div>" +
                                "<div class='btn btn-primary pad' data-toggle='modal' data-target='#modalContainer' onclick='referWeek(" + dado[i]["ID_WEEK"] + ")'><i class='fas fa-plus'></i></div>" +
                                "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdWeek").empty();
                        $("#grdWeek").append("<tr id='msgEmptyWeek'><td colspan='6' class='alert alert-light text-center'>Esse parceiro não possui taxas.</td></tr>");
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
        });

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


       


        function referWeek(Id) {
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
            var week = document.getElementById("setWeek").innerHTML = Id;
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarContainerWeek",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdWeekContainer").empty();
                    $("#grdWeekContainer").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");

                },
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data != null) {
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
                                for (let i = 0; i < dadoId.length; i++) {
                                    $("#ddlIdContainer").append("<option value = '" + dadoId[i]["ID_WEEK_CONTAINER"] + "' > " + dadoId[i]["NR_CONTAINER"] + "</option >");
                                }
                            },
                            error: function (data) {
                            },
                        });
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
           

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/ListarIdWeek",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var dadoId = data.d;
                    dadoId = $.parseJSON(dadoId);
                    $("#ddlIdWeek").empty();
                    for (let i = 0; i < dadoId.length; i++) {
                        $("#ddlIdWeek").append("<option value = '" + dadoId[i]["ID_WEEK"] + "' > " + dadoId[i]["NM_WEEK"] + "</option >");
                    }
                },
                error: function (data) {

                },
            });
            
        }

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
                $("#btnCadastrarContainer").show();
            }
            else {
                $("#nrContainer").css('background', '#ffdfd4');
                $(".erroNrContainer").show();

            }
        }

        function Calculo_Digito_Conteiner_Enviar() {
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
                $("#nrContainer").focus();
                $("#nrContainer").css('background', '#ffdfd4');
                $(".erroNrContainer").show();
                $("#msgErrContWeek").fadeIn(500).delay(1000).fadeOut(500);
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
                $("#btnCadastrarContainer").show();
            }
            else {
                $("#nrContainer").css('background', '#ffdfd4');
                $(".erroNrContainer").show();

            }
            if (Conteiner != "") {
                if (calcula == cleanConteiner.substr(cleanConteiner.length - 1, 1)) {
                    $("#nrContainer").css('background', '#white');
                    $(".erroNrContainer").hide();
                    var Id = document.getElementById("setWeek").innerHTML;
                    var dado = {
                        "ID_TIPO_CONTAINER": document.getElementById('MainContent_ddlTipoContainer').value,
                        "NR_CONTAINER": document.getElementById('nrContainer').value,
                        "VL_PESO_MAX": document.getElementById('MainContent_vlPesoMax').value,
                        "VL_CUBAGEM": document.getElementById('MainContent_vlCubagem').value,
                        "ID_WEEK": Id
                    }
                    document.getElementById('MainContent_ddlTipoContainer').value = "";
                    document.getElementById('nrContainer').value = "";
                    document.getElementById('MainContent_vlPesoMax').value = "";
                    document.getElementById('MainContent_vlCubagem').value = "";
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CadastrarContainer",
                        data: JSON.stringify({ dados: (dado) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == "1") {
                                $("#msgSuccessContWeek").fadeIn(500).delay(1000).fadeOut(500);
                                $.ajax({
                                    type: "POST",
                                    url: "WebService1.asmx/ListarContainerWeek",
                                    data: '{Id:"' + Id + '" }',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    beforeSend: function () {
                                        $("#grdWeekContainer").empty();
                                        $("#grdWeekContainer").append("<tr><td class='text-center' colspan='6'><div class='text-center loader'></div></td></tr>");
                                    },
                                    success: function (data) {
                                        var data = data.d;
                                        data = $.parseJSON(data);
                                        if (data != null) {
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
                                $("#msgErrContWeek").fadeIn(500).delay(1000).fadeOut(500);
                                $.ajax({
                                    type: "POST",
                                    url: "WebService1.asmx/ListarContainerWeek",
                                    data: '{Id:"' + Id + '" }',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    beforeSend: function () {
                                        $("#grdWeekContainer").empty();
                                        $("#grdWeekContainer").append("<tr><td class='text-center loader' colspan='6'>Carregando</td></tr>");
                                    },
                                    success: function (data) {
                                        var data = data.d;
                                        data = $.parseJSON(data);
                                        if (data != null) {
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
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarIdContainer",
                                data: '{week:"' + Id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var dadoId = data.d;
                                    dadoId = $.parseJSON(dadoId);
                                    $("#ddlIdContainer").empty();
                                    for (let i = 0; i < dadoId.length; i++) {
                                        $("#ddlIdContainer").append("<option value = '" + dadoId[i]["ID_WEEK_CONTAINER"] + "' > " + dadoId[i]["NR_CONTAINER"] + "</option >");
                                    }
                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            $("#msgErrContWeek").fadeIn(500).delay(1000).fadeOut(500);
                        },
                    });
                }
                else {
                    $("#nrContainer").focus();
                    $("#nrContainer").css('background', '#ffdfd4');
                    $(".erroNrContainer").show();
                    $("#msgErrContWeek").fadeIn(500).delay(1000).fadeOut(500);
                }
            }
            else {
                $("#msgErrContWeek").fadeIn(500).delay(1000).fadeOut(500);
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
