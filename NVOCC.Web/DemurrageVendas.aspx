<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DemurrageVendas.aspx.cs" Inherits="ABAINFRA.Web.DemurrageVendas" %>
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
                            <div class="alert alert-success text-center" id="msgSuccessUpdate">
                                Base atualizada com sucesso.
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrUpdate">
                                Erro ao atualizar base.
                            </div>
                        </div>
                    </div>
                    <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                        <div style="margin: auto;">
                            <button type="button" id="btnNovaDemurrage" class="btn btn-primary" data-toggle="modal" data-target="#modalDemurrage">Calcular Demurrage</button>               
                            <button type="button" id="btnExportGridAtual" class="btn btn-primary" onclick="exportTableToCSVAtual('members.csv')">Faturas</button>                 
                        </div>
                    </div>
                   <br/>

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
                    <!-- Modal -->
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
                                    <th class="text-center" scope="col">Nº CNTR</th>
                                    <th class="text-center" scope="col">SEL</th>
                                    <th class="text-center" scope="col">DATA CHEGADA</th>
                                    <th class="text-center" scope="col">FREE TIME</th>
                                    <th class="text-center" scope="col">DATA LIMITE</th>
                                    <th class="text-center" scope="col">DATA DEVOLUÇÃO</th>
                                    <th class="text-center" scope="col">DIAS DEMURRAGE</th>
                                    <th class="text-center" scope="col">DATA CALC DEMURRAGE</th>
                                    <th class="text-center" scope="col">MOEDA DEMURRAGE</th>
                                    <th class="text-center" scope="col">TAXA DEMURRAGE</th>
                                    <th class="text-center" scope="col">VALOR DEMURRAGE</th>
                                    <th class="text-center" scope="col">NR FATURA</th>
                                    <th class="text-center" scope="col">DATA LIQUIDAÇÃO</th>
                                    <th class="text-center" scope="col">CLIENTE</th>
                                </tr>
                            </thead>
                            <tbody id="grdDemurrageAtualBody">
                                
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
    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarDemurrageVenda",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdDemurrageAtualBody").empty();
                    $("#grdDemurrageAtualBody").append("<tr><td colspan='13'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdDemurrageAtualBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdDemurrageAtualBody").append("<tr><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'><input type='checkbox'></td>" +
                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_VENDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ID_MOEDA_DEMURRAGE_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_TAXA_DEMURRAGE_VENDA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ID_DEMURRAGE_FATURA_RECEBER"] + "</td><td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td><td class='text-center'>" + dado[i]["NM_FANTASIA"] + "</td>" +
                                "</tr>");
                        }
                    }
                    else {
                        $("#grdDemurrageAtualBody").empty();
                        $("#grdDemurrageAtualBody").append("<tr id='msgEmptyWeek'><td colspan='13' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        });
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
                    $("#grdDemurrageAtualBody").empty();
                    $("#grdDemurrageAtualBody").append("<tr><td colspan='19'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdDemurrageAtualBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            if (dado[i]["QT_DIAS_DEMURRAGE"] <= 10 && dado[i]["QT_DIAS_DEMURRAGE"] >= 1) {
                                $("#grdDemurrageAtualBody").append("<tr style='color: rgba(153,51,153,1); font-weight: bold'><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                            else if (dado[i]["QT_DIAS_DEMURRAGE"] < 1) {
                                $("#grdDemurrageAtualBody").append("<tr style='color: rgba(255,0,0,0.4); font-weight: bold'><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                            else {
                                $("#grdDemurrageAtualBody").append("<tr><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["FINAL_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_STATUS_DEMURRAGE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DATA_STATUS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_DEMURRAGE_COMPRA"] + "</td > <td class='text-center'>" + dado[i]["PAG_DEMU"] + "</td><td class='text-center'>" + dado[i]["CALC_DEMU_COMPRA"] + "</td><td class='text-center'>" + dado[i]["VL_DEMURRAGE_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEB_DEMU"] + "</td></tr>");
                            }
                        }
                    }
                    else {
                        $("#grdDemurrageAtualBody").empty();
                        $("#grdDemurrageAtualBody").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Resultado não encontrado</td></tr>");
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
            var rows = document.querySelectorAll("#grdDemurrageAtual tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdDemurrageAtual td, #grdDemurrageAtual th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVAtual(csv.join("\n"), filename);
        }
                
    </script>

</asp:Content>
