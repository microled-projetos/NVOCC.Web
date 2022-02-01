<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="ABAINFRA.Web.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Módulo Gerencial
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabdashboard">
                            <a href="#dashboard" id="linkdashboard" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>DashBoard
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="dashboard">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <select id="ddlVendedor" class="form-control">
                                            <option value="0">Selecione</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Mês - Inicial<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlMesInicial" runat="server" CssClass="form-control"></asp:DropDownList>      
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Ano - Inicial<span class="required">&nbsp</span></label>
                                        <select id="ddlAnoInicial" class="form-control">
                                            <option value="">Selecione</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-1" id="ddlMesFinalBox">
                                    <div class="form-group">
                                        <label class="control-label">Mês - Final<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlMesFinal" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1" id="ddlAnoFinalBox">
                                    <div class="form-group">
                                        <label class="control-label">Ano - Final<span class="required">&nbsp</span></label>
                                        <select id="ddlAnoFinal" class="form-control">
                                            <option value="">Selecione</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Operação<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlTipoOperacao" runat="server" CssClass="form-control"></asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Cores<span class="required">&nbsp</span></label>
                                        <select id="ddlCores" class="form-control">
                                            <option value="azul">Tons de Azul</option>
                                            <option value="vermelho">Tons de Vermelho</option>
                                            <option value="verde">Tons de Verde</option>
                                            <option value="amarelo">Tons de Amarelo</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Estilo<span class="required">&nbsp</span></label>
                                        <select id="ddlEstilo" class="form-control" onchange="dataChange()">
                                            <option value="bar">Barra</option>
                                            <option value="line">Linha</option>
                                            <option value="pie">Pizza</option>
                                            <option value="doughnut">Donuts</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="gerarGrafico()" class="btn btn-primary">Carregar Gráfico</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row topMarg">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:CheckBox ID="chkInstrEmbarque" runat="server" CssClass="form-control noborder" Text="&nbsp;Incluir instruções de embarque" Checked="true"></asp:CheckBox>
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
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active" id="tabprocessoExpectGrid">
                                    <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Gráficos
                                    </a>
                                </li>
                                <li id="tabprocessoRealGrid">
                                    <a href="#processoRealGrid" id="linkprocessoRealGrid" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Indicadores
                                    </a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="processoExpectGrid">
                                    <div id="graph">
                                        
                                    </div>
                                    <div style="display:flex; justify-content:space-between">
                                        <div id="processGraph" style="width:100%"></div>
                                        <div id="cntrGraph" style="width:100%"></div>
                                        <div id="teusGraph" style="width:100%"></div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="processoRealGrid">
                                    <div class="boxMainIndicador">
                                        <h3>Importação</h3>
                                        <div class="boxImpo">
                                            <div class="boxProcImpo">
                                                <div class="table-responsive tableFixHead" style="max-height: 300px">
                                                    <table id="tblIndicadorProcImpo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. Proc.</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorProcImpoBody">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="boxTeusImpo">
                                                <div class="table-responsive tableFixHead" style="max-height: 300px">
                                                    <table id="tblIndicadorTeusImpo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. Teus</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorTeusImpoBody">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="boxCntrImpo">
                                                <div class="table-responsive tableFixHead" style="max-height: 300px">
                                                    <table id="tblIndicadorCNTRImpo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. CNTR</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorCNTRImpoBody">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <h3>Exportação</h3>
                                        <div class="boxExpo">
                                            <div class="boxProcExpo">
                                                <div class="table-responsive tableFixHead"  style="max-height: 300px">
                                                    <table id="tblIndicadorProcExpo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. Proc.</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorProcExpoBody">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="boxTeusExpo">
                                                <div class="table-responsive tableFixHead"  style="max-height: 300px">
                                                    <table id="tblIndicadorTeusExpo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. Teus.</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorTeusExpoBody">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="boxCntrExpo">
                                                <div class="table-responsive tableFixHead"  style="max-height: 300px">
                                                    <table id="tblIndicadorCNTRExpo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. CNTR</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorCNTRExpoBody">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <h3>Aéreo</h3>
                                        <div class="boxAereo">
                                            <div>
                                                <div class="table-responsive tableFixHead"  style="max-height: 300px">
                                                    <table id="tblIndicadorProcAereo" class="table tablecont">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" scope="col">Vendedor</th>
                                                                <th class="text-center" scope="col">Qtd. CNTR</th>
                                                                <th class="text-center" scope="col">% Partc</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblIndicadorProcAereoBody">
                                                        </tbody>
                                                    </table>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script src="Content/chart.js-3.2.0/package/dist/chart.js"></script>
    <script>
        var rgb = [];
        var myChart;
        var myChart2;
        var myChart3;
        var myChart4;
        var anoInicial = document.getElementById("ddlAnoInicial");
        var anoFinal = document.getElementById("ddlAnoFinal");
        var mesInicial = document.getElementById("MainContent_ddlMesInicial");
        var mesFinal = document.getElementById("MainContent_ddlMesFinal");
        var vendedor = document.getElementById("ddlVendedor");
        var tipoEstufagem = document.getElementById("MainContent_ddlTipoOperacao");
        var type = document.getElementById("ddlEstilo").value;
        const graph = document.getElementById("graph");
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/CarregarVendedores",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    for (let i = 0; i < data.length; i++) {
                        $("#ddlVendedor").append("<option value='" + data[i]["ID_PARCEIRO"] + "'>" + data[i]["NM_RAZAO"] + "</option>");
                    }

                },
                error: function (data) {

                },
            });

        });

        var ano = new Date().getFullYear();
        for (var i = ano; i >= 2018; i--) {
            var value = i.toString().substr(2, 2);
            $("#ddlAnoInicial").append("<option value='" + value + "'>" + i + "</option>")
            $("#ddlAnoFinal").append("<option value='" + value + "'>" + i + "</option>")
        }

        function dataChange() {
            var estilo = document.getElementById("ddlEstilo").value;
            if (estilo == "pie") {
                $("#ddlMesFinalBox").hide();
                $("#ddlAnoFinalBox").hide();
            } else if (estilo == "bar") {
                $("#ddlMesFinalBox").show();
                $("#ddlAnoFinalBox").show();
            }
            type = estilo;
        }

        function colorChange() {
            var cor = document.getElementById("ddlCores").value;
            switch (cor) {
                case "azul":
                    rgb = ["rgb(0,151,172)","rgb(60,214,230)","rgb(151,234,244)"];
                    break;
                case "vermelho":
                    rgb = ["rgb(217,90,87)", "rgb(117,76,73)","rgb(167,61,58)"];
                    break;
                case "verde":
                    rgb = ["rgb(98,192,105)", "rgb(190,245,123)","rgb(144,197,88)"];
                    break;
                case "amarelo":
                    rgb = ["rgb(252,232,94)", "rgb(218,190,106)","rgb(231,210,30)"];
                    break;
                default:
                    rgb = ["rgb(0,151,172)", "rgb(60,214,230)", "rgb(151,234,244)"];
                    break;
            }
            console.log(rgb);
        }

        function processosIndicador() {
            var pctPI;
            var pctTI;
            var pctCI;
            var pctPE;
            var pctTE;
            var pctCE;
            var pctPA;
            var instrEmbarque = document.getElementById("MainContent_chkInstrEmbarque");
            if (instrEmbarque.checked) {
                instrEmbarque = "1";
            } else {
                instrEmbarque = "0";
            }
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/ProcessosIndicador",
                data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '", embarque:"' + instrEmbarque + '"  }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);                   
                    $("#tblIndicadorProcImpoBody").empty();
                    $("#tblIndicadorCNTRImpoBody").empty();
                    $("#tblIndicadorTeusImpoBody").empty();
                    $("#tblIndicadorProcExpoBody").empty();
                    $("#tblIndicadorCNTRExpoBody").empty();
                    $("#tblIndicadorTeusExpoBody").empty();
                    $("#tblIndicadorProcAereoBody").empty();
                    if (data != null) {
                        
                        for (var i = 0; i < data.length; i++) {

                            if (data[i]["PROC_IMP"] * 100 == 0) {
                                pctPI = 0.0;
                            } else {
                                pctPI = (data[i]["PROC_IMP"] * 100 / data[0]["TOTAL_PROC_IMP"]).toFixed(2).replace(".",",");
                            }

                            if (data[i]["CNTR_IMP"] * 100 == 0) {
                                pctTI = 0.0;
                            } else {
                                pctTI = (data[i]["CNTR_IMP"] * 100 / data[0]["TOTAL_CNTR_IMP"]).toFixed(2).replace(".", ",");
                            }

                            if (data[i]["TEUS_IMP"] * 100 == 0) {
                                pctCI = 0.0;
                            } else {
                                pctCI = (data[i]["TEUS_IMP"] * 100 / data[0]["TOTAL_TEUS_IMP"]).toFixed(2).replace(".", ",");
                            }

                            if (data[i]["PROC_EXP"] * 100 == 0) {
                                pctPE = 0.0;
                            } else {
                                pctPE = (data[i]["PROC_EXP"] * 100 / data[0]["TOTAL_PROC_EXP"]).toFixed(2).replace(".", ",");
                            }

                            if (data[i]["CNTR_EXP"] * 100 == 0) {
                                pctTE = 0.0;
                            } else {
                                pctTE = (data[i]["CNTR_EXP"] * 100 / data[0]["TOTAL_CNTR_EXP"]).toFixed(2).replace(".", ",");
                            }

                            if (data[i]["TEUS_EXP"] * 100 == 0) {
                                pctCE = 0.0;
                            } else {
                                pctCE = (data[i]["TEUS_EXP"] * 100 / data[0]["TOTAL_TEUS_EXP"]).toFixed(2).replace(".", ",");
                            }

                            if (data[i]["PROC_AR"] * 100 == 0) {
                                pctPA = 0.0;
                            } else {
                                pctPA = (data[i]["PROC_AR"] * 100 / data[0]["TOTAL_PROC_AR"]).toFixed(2).replace(".", ",");
                            }


                            $("#tblIndicadorProcImpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_IMP"] + "</td>" +
                                "<td class='text-center'>" + pctPI + " %</td></tr>");
                            $("#tblIndicadorCNTRImpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CNTR_IMP"] + "</td>" +
                                "<td class='text-center'>" + pctTI + " %</td></tr>");
                            $("#tblIndicadorTeusImpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["TEUS_IMP"] + "</td>" +
                                "<td class='text-center'>" + pctCI + " %</td></tr>");
                            $("#tblIndicadorProcExpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_EXP"] + "</td>" +
                                "<td class='text-center'>" + pctPE + " %</td></tr>");
                            $("#tblIndicadorCNTRExpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CNTR_EXP"] + "</td>" +
                                "<td class='text-center'>" + pctTE + " %</td></tr>");
                            $("#tblIndicadorTeusExpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["TEUS_EXP"] + "</td>" +
                                "<td class='text-center'>" + pctCE + " %</td></tr>");
                            $("#tblIndicadorProcAereoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] +"' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_AR"] + "</td>" +
                                "<td class='text-center'>" + pctPA + " %</td></tr>");
                        }
                    }
                    else {
                        $("#tblIndicadorProcImpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorCNTRImpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorTeusImpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorProcExpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorCNTRExpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorTeusExpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorProcAereoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            });
        }
        function processosIndicadorPizza() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/ProcessosIndicadorPizza",
                data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#tblIndicadorProcImpoBody").empty();
                    $("#tblIndicadorCNTRImpoBody").empty();
                    $("#tblIndicadorTeusImpoBody").empty();
                    $("#tblIndicadorProcExpoBody").empty();
                    $("#tblIndicadorCNTRExpoBody").empty();
                    $("#tblIndicadorTeusExpoBody").empty();
                    $("#tblIndicadorProcAereoBody").empty();
                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            $("#tblIndicadorProcImpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_IMP"] + "</td>" +
                                "<td class='text-center'>" + data[0]["TOTAL"] * data[i]["PROC_IMP"] / 100 +" %</td></tr>");
                            $("#tblIndicadorCNTRImpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CNTR_IMP"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CNTR_IMP"] + " %</td></tr>");
                            $("#tblIndicadorTeusImpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["TEUS_IMP"] + "</td>" +
                                "<td class='text-center'>" + data[i]["TEUS_IMP"] + " %</td></tr>");
                            $("#tblIndicadorProcExpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_EXP"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_EXP"] + " %</td></tr>");
                            $("#tblIndicadorCNTRExpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CNTR_EXP"] + "</td>" +
                                "<td class='text-center'>" + data[i]["CNTR_EXP"] + " %</td></tr>");
                            $("#tblIndicadorTeusExpoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["TEUS_EXP"] + "</td>" +
                                "<td class='text-center'>" + data[i]["TEUS_EXP"] + " %</td></tr>");
                            $("#tblIndicadorProcAereoBody").append("<tr style='padding: 5px 10px !important;'><td class='text-center' title='" + data[i]["VENDEDOR"] + "' style='max-width: 10ch'> " + data[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_AR"] + "</td>" +
                                "<td class='text-center'>" + data[i]["PROC_AR"] + " %</td></tr>");
                        }
                    }
                    else {
                        $("#tblIndicadorProcImpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorCNTRImpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorTeusImpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorProcExpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorCNTRExpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorTeusExpoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        $("#tblIndicadorProcAereoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            });
        }

        function graficoGeral(processo,cntr,teus,data) {
            var ctx = document.getElementById('mainGraph').getContext('2d');
            myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: data,
                    datasets: [
                        {
                            barThickness: 30,
                            label: 'TEUS',
                            data: teus,
                            backgroundColor: rgb[0],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'Processos',
                            data: processo,
                            backgroundColor: rgb[1],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'Containers',
                            data: cntr,
                            backgroundColor: rgb[2],
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    },
                    layout: {
                        padding: 10
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Teus, Processos e Qtde. de CNTR por Vendedor',
                            font: { size: 20 }
                        }
                    }
                }
            });            
        }

        function graficoProcessos(processoIMP, processoEXP, processoAR, data) {
            console.log(processoIMP, processoEXP, processoAR);
            var ctx2 = document.getElementById('pgraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'bar',
                data: {
                    labels: data,
                    datasets: [
                        {
                            barThickness: 30,
                            label: 'IMP',
                            data: processoIMP,
                            backgroundColor: [rgb[0]],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'EXP',
                            data: processoEXP,
                            backgroundColor: [rgb[1]],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'AEREO',
                            data: processoAR,
                            backgroundColor: [rgb[2]],
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    layout: {
                        padding: 10
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde. de Processos',
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoCntr(cntrIMP, cntrEXP, data) {
                var ctx2 = document.getElementById('cgraph').getContext('2d');
                var myChart2 = new Chart(ctx2, {
                    type: 'bar',
                    data: {
                        labels: data,
                        datasets: [
                            {
                                barThickness: 30,
                                label: 'IMP',
                                data: cntrIMP,
                                backgroundColor: [rgb[0]],
                                borderWidth: 1
                            },
                            {
                                barThickness: 30,
                                label: 'EXP',
                                data: cntrEXP,
                                backgroundColor: [rgb[1]],
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        layout: {
                            padding: 10
                        },
                        plugins: {
                            title: {
                                display: true,
                                text: 'Qtde. de CNTR',
                                font: { size: 20 }
                            }
                        }
                    }
                });            
        }

        function graficoTeus(processo,cntr,data) {
                var ctx2 = document.getElementById('tGraph').getContext('2d');
                var myChart2 = new Chart(ctx2, {
                    type: 'bar',
                    data: {
                        labels: data,
                        datasets: [
                            {
                                barThickness: 30,
                                label: 'IMP',
                                data: processo,
                                backgroundColor: [rgb[0]],
                                borderWidth: 1
                            },
                            {
                                barThickness: 30,
                                label: 'EXP',
                                data: cntr,
                                backgroundColor: [rgb[1]],
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        layout: {
                            padding: 10
                        },
                        plugins: {
                            title: {
                                display: true,
                                text: 'Teus',
                                font: { size: 20 }
                            }
                        }
                    }
                });
        }

        function graficoGeralPizza(processo, cntr, teus, data) {
            var ctx = document.getElementById('mainGraph').getContext('2d');
            myChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['PROCESSO', 'CNTR', 'TEUS'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [processo[0], cntr[0], teus[0]],
                            backgroundColor: rgb,
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Teus, Processos e Qtde. de CNTR por Vendedor em ' + data ,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoProcessosPizza(processoIMP, processoEXP, processoAR, data) {              
            var ctx2 = document.getElementById('pgraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'pie',
                data: {
                    labels: ['IMP', 'EXP', 'AEREO'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [processoIMP[0], processoEXP[0], processoAR[0]],
                            backgroundColor: rgb,
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde. de Processos em '+data,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoCntrPizza(cntrIMP, cntrEXP, data) {
            var ctx2 = document.getElementById('cgraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'pie',
                data: {
                    labels: ['IMP', 'EXP'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [cntrIMP[0], cntrEXP[0]],
                            backgroundColor: rgb,
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde. de CNTR em ' +data,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoTeusPizza(processo, cntr, data) {
            var ctx2 = document.getElementById('tGraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'pie',
                data: {
                    labels: ['IMP', 'EXP'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [processo[0], cntr[0]],
                            backgroundColor: rgb,
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Teus em '+data,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function gerarGrafico() {
            colorChange();
            var instrEmbarque = document.getElementById("MainContent_chkInstrEmbarque");
            if (instrEmbarque.checked) {
                instrEmbarque = "1";
            } else {
                instrEmbarque = "0";
            }
            if (type == "bar") {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarEstatistica",
                    data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '", embarque:"' + instrEmbarque + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartPROC = [];
                        var lineChartCNTR = [];
                        var lineChartTEUS = [];
                        var lineChartPROCIMP = [];
                        var lineChartPROCAR = [];
                        var lineChartPROCEXP = [];
                        var lineChartCNTRIMP = [];
                        var lineChartCNTREXP = [];
                        var lineChartTEUSIMP = [];
                        var lineChartTEUSEXP = [];
                        var label = [];
                        $("#graph").empty();
                        $("#processGraph").empty();
                        $("#cntrGraph").empty();
                        $("#teusGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartPROC.push(data[i]["PROC_TOTAL"]);
                                lineChartCNTR.push(data[i]["CNTR_TOTAL"]);
                                lineChartTEUS.push(data[i]["TEUS_TOTAL"]);
                                lineChartPROCIMP.push(data[i]["PROC_IMP"]);
                                lineChartPROCAR.push(data[i]["PROC_AR"]);
                                lineChartPROCEXP.push(data[i]["PROC_EXP"]);
                                lineChartCNTRIMP.push(data[i]["CNTR_IMP"]);
                                lineChartCNTREXP.push(data[i]["CNTR_EXP"]);
                                lineChartTEUSIMP.push(data[i]["TEUS_IMP"]);
                                lineChartTEUSEXP.push(data[i]["TEUS_EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoGeral(lineChartPROC, lineChartCNTR, lineChartTEUS, label);
                            graficoCntr(lineChartCNTRIMP, lineChartCNTREXP, label);
                            graficoProcessos(lineChartPROCIMP, lineChartPROCEXP, lineChartPROCAR, label);
                            graficoTeus(lineChartTEUSIMP, lineChartTEUSEXP, label);
                            processosIndicador();
                        }
                        else {
                            lineChartPROC.push(0);
                            lineChartCNTR.push(0);
                            lineChartTEUS.push(0);
                            lineChartPROCIMP.push(0);
                            lineChartPROCAR.push(0);
                            lineChartPROCEXP.push(0);
                            lineChartCNTRIMP.push(0);
                            lineChartCNTREXP.push(0);
                            lineChartTEUSIMP.push(0);
                            lineChartTEUSEXP.push(0);
                            label.push(0);
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoGeral(lineChartPROC, lineChartCNTR, lineChartTEUS, label);
                            graficoCntr(lineChartCNTRIMP, lineChartCNTREXP, label);
                            graficoProcessos(lineChartPROCIMP, lineChartPROCEXP, lineChartPROCAR, label);
                            graficoTeus(lineChartTEUSIMP, lineChartTEUSEXP, label)
                            processosIndicador();
                        }
                    }
                });
            } else if (type == "pie") {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarEstatisticaPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '", embarque: "' + instrEmbarque + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartPROC = [];
                        var lineChartCNTR = [];
                        var lineChartTEUS = [];
                        var lineChartPROCIMP = [];
                        var lineChartPROCAR = [];
                        var lineChartPROCEXP = [];
                        var lineChartCNTRIMP = [];
                        var lineChartCNTREXP = [];
                        var lineChartTEUSIMP = [];
                        var lineChartTEUSEXP = [];
                        var label = [];
                        $("#graph").empty();
                        $("#processGraph").empty();
                        $("#cntrGraph").empty();
                        $("#teusGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartPROC.push(data[i]["PROC_TOTAL"]);
                                lineChartCNTR.push(data[i]["CNTR_TOTAL"]);
                                lineChartTEUS.push(data[i]["TEUS_TOTAL"]);
                                lineChartPROCIMP.push(data[i]["PROC_IMP"]);
                                lineChartPROCAR.push(data[i]["PROC_AR"]);
                                lineChartPROCEXP.push(data[i]["PROC_EXP"]);
                                lineChartCNTRIMP.push(data[i]["CNTR_IMP"]);
                                lineChartCNTREXP.push(data[i]["CNTR_EXP"]);
                                lineChartTEUSIMP.push(data[i]["TEUS_IMP"]);
                                lineChartTEUSEXP.push(data[i]["TEUS_EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoGeralPizza(lineChartPROC, lineChartCNTR, lineChartTEUS, label);
                            graficoCntrPizza(lineChartCNTRIMP, lineChartCNTREXP, label);
                            graficoProcessosPizza(lineChartPROCIMP, lineChartPROCEXP, lineChartPROCAR, label);
                            graficoTeusPizza(lineChartTEUSIMP, lineChartTEUSEXP, label)
                            processosIndicadorPizza();
                        }
                        else {
                            lineChartPROC.push(0);
                            lineChartCNTR.push(0);
                            lineChartTEUS.push(0);
                            lineChartPROCIMP.push(0);
                            lineChartPROCAR.push(0);
                            lineChartPROCEXP.push(0);
                            lineChartCNTRIMP.push(0);
                            lineChartCNTREXP.push(0);
                            lineChartTEUSIMP.push(0);
                            lineChartTEUSEXP.push(0);
                            label.push(0);
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoGeralPizza(lineChartPROC, lineChartCNTR, lineChartTEUS, label);
                            graficoCntrPizza(lineChartCNTRIMP, lineChartCNTREXP, label);
                            graficoProcessosPizza(lineChartPROCIMP, lineChartPROCEXP, lineChartPROCAR,label);
                            graficoTeusPizza(lineChartTEUSIMP, lineChartTEUSEXP, label)
                            processosIndicadorPizza();
                        }
                    }
                });

                /*$.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarProcessosPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartIMP = [];
                        var lineChartAR = [];
                        var lineChartEXP = [];
                        var label = [];
                        $("#processGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartIMP.push(data[i]["IMP"]);
                                lineChartAR.push(data[i]["AR"]);
                                lineChartEXP.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height:300px;max-height: 300px''></canvas>");
                            graficoProcessosPizza(lineChartIMP, lineChartEXP, lineChartAR, label);
                        }
                        else {
                            lineChartIMP.push(0);
                            lineChartAR.push(0);
                            lineChartEXP.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoProcessosPizza(lineChartIMP, lineChartEXP, lineChartAR, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarQtdCntrPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartIMP = [];
                        var lineChartEXP = [];
                        var label = [];
                        $("#cntrGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartIMP.push(data[i]["IMP"]);
                                lineChartEXP.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoCntrPizza(lineChartIMP, lineChartEXP, label);
                        }
                        else {
                            lineChartIMP.push(0);
                            lineChartEXP.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoCntrPizza(lineChartIMP, lineChartEXP, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarTeusPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartProcesso = [];
                        var lineChartCntr = [];
                        var label = [];
                        $("#teusGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartProcesso.push(data[i]["IMP"]);
                                lineChartCntr.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px'></canvas>");
                            graficoTeusPizza(lineChartProcesso, lineChartCntr, label)
                        }
                        else {
                            lineChartProcesso.push(0);
                            lineChartCntr.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoTeusPizza(lineChartProcesso, lineChartCntr, label)
                        }
                    }
                });*/
            }
        }
    </script>
</asp:Content>
