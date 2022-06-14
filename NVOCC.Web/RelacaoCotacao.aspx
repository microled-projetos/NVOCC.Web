﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelacaoCotacao.aspx.cs" Inherits="ABAINFRA.Web.RelacaoCotacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relação Cotação
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relação Cotação
                            </a>
                        </li>
                        <li id="tabTotaisCotacao">
                            <a href="#totaisCotacao" id="linkTotaisCotacao" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Totais Cotação
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportRelacaoCotacao" class="btn btn-primary" onclick="exportRelacaoCotacaoCSV('Relacao_Cotacao.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacao" class="btn btn-primary" onclick="createPDF()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Inicial:</label>
                                                <input id="txtDtInicialRelacaoCotacao" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Final:</label>
                                                <input id="txtDtFinalRelacaoCotacao" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Filtro</label>
                                                <select id="ddlFilterRelacaoCotacao" class="form-control">
                                                    <option value="">Selecione</option>
                                                    <option value="1">Vendedor</option>
                                                    <option value="2">Inside</option>
                                                    <option value="3">Cliente</option>
                                                    <option value="4">Status</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">*</label>
                                                <input id="txtRelacaoCotacao" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultarRelacaoCotacao" onclick="RelacaoCotacao()" class="btn btn-primary">Consultar</button>
                                        </div>
                                    </div>
                                </div> 
                                <div id="tableRelCot" class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdRelacaoCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">SOLICITAÇÃO</th>
                                                <th class="text-center" scope="col">INSIDE</th>
                                                <th class="text-center" scope="col">NR COTAÇÃO</th>
                                                <th class="text-center" scope="col">MODAL</th>
                                                <th class="text-center" scope="col">INCOTERM</th>
                                                <th class="text-center" scope="col">CLIENTE</th>
                                                <th class="text-center" scope="col">SUB CLIENTE</th>
                                                <th class="text-center" scope="col">ORIGEM</th>
                                                <th class="text-center" scope="col">DESTINO</th>
                                                <th class="text-center" scope="col">VENDEDOR</th>
                                                <th class="text-center" scope="col">SOLICITACAO/PRICING</th>
                                                <th class="text-center" scope="col">ANALISE</th>
                                                <th class="text-center" scope="col">INFORMAÇÕES PENDENTES</th>
                                                <th class="text-center" scope="col">LIBERADA INSIDE</th>
                                                <th class="text-center" scope="col">RENEGOCIAÇÃO</th>
                                                <th class="text-center" scope="col">AGUARDANDO APROVAÇÃO</th>
                                                <th class="text-center" scope="col">CANCELADA</th>
                                                <th class="text-center" scope="col">REJEITADA</th>
                                                <th class="text-center" scope="col">APROVADA</th>
                                                <th class="text-center" scope="col">EM UPDATE</th>
                                                <th class="text-center" scope="col">DECLINADA</th>
                                                <th class="text-center" scope="col">FINALIZADA COM PAGAMENTO</th>
                                                <th class="text-center" scope="col">EM FUP</th>
                                                <th class="text-center" scope="col">APROVADA COM EDIÇÃO</th>
                                                <th class="text-center" scope="col">MOTIVO CANCELAMENTO</th>
                                                <th class="text-center" scope="col">OBS MOTIVO CANCELAMENTO</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdRelacaoCotacaoBody">

                                        </tbody>
                                    </table>
                                </div>
                            </div>

                        <div class="tab-pane fade" id="totaisCotacao">
                            <div class="row topMarg">
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportTotaisCotacao" class="btn btn-primary" onclick="exportRelacaoCotacaoCSV('Relacao_Cotacao.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                            </div>
                            <div class="flex">
                                <div id="tableTotalStatusCot" class="table-responsive fixedDoubleHead topMarg" style="width: 100%; padding: 5px;">
                                    <h3 style="text-align:center">Total p/ Status</h3>
                                    <table id="grdTotalStatusCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">STATUS</th>
                                                <th class="text-center" scope="col">QUANTIDADE</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdTotalStatusCotacaoBody">

                                        </tbody>
                                    </table>
                                </div>
                                <div id="tableTotalModalCot" class="table-responsive fixedDoubleHead topMarg" style="width: 100%; padding: 5px;">
                                    <h3 style="text-align:center">Total p/ Modal</h3>
                                    <table id="grdTotalModalCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">MODAL</th>
                                                <th class="text-center" scope="col">QUANTIDADE</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdTotalModalCotacaoBody">

                                        </tbody>
                                    </table>
                                </div>
                                <div id="tableTotalIncotermCot" class="table-responsive fixedDoubleHead topMarg" style="width: 100%; padding: 5px;">
                                    <h3 style="text-align:center">Total p/ Incoterm</h3>
                                    <table id="grdTotalIncotermCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">INCOTERM</th>
                                                <th class="text-center" scope="col">QUANTIDADE</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdTotalIncotermCotacaoBody">

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="flex">
                                <div id="tableTotalVendedorCot" class="table-responsive fixedDoubleHead topMarg" style="width: 100%; padding: 5px;">
                                    <h3 style="text-align:center">Total p/ Vendedor</h3>
                                    <table id="grdTotalVendedorCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">VENDEDOR</th>
                                                <th class="text-center" scope="col">QUANTIDADE</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdTotalVendedorCotacaoBody">

                                        </tbody>
                                    </table>
                                </div>
                                <div id="tableTotalInsideCot" class="table-responsive fixedDoubleHead topMarg" style="width: 100%; padding: 5px;">
                                    <h3 style="text-align:center">Total p/ Inside</h3>
                                    <table id="grdTotalInsideCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">INSIDE</th>
                                                <th class="text-center" scope="col">QUANTIDADE</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdTotalInsideCotacaoBody">

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>

    <script>
        function RelacaoCotacao() {
            var dtInicial = document.getElementById("txtDtInicialRelacaoCotacao").value;
            var dtFinal = document.getElementById("txtDtFinalRelacaoCotacao").value;
            var nota = document.getElementById("txtRelacaoCotacao").value;
            var filter = document.getElementById("ddlFilterRelacaoCotacao").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarRelacaoCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdRelacaoCotacaoBody").empty();
                        $("#grdRelacaoCotacaoBody").append("<tr><td colspan='13'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdRelacaoCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdRelacaoCotacaoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["SOLICITACAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["INSIDE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_COTACAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MODAL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["INCOTERM"] + "</td>" +
                                    "<td class='text-center' style='max-width: 15ch;' title='" + dado[i]["CLIENTE"] + "'>" + dado[i]["CLIENTE"] + "</td>" +
                                    "<td class='text-center' style='max-width: 14ch;' title='" + dado[i]["SUB_CLIENTE"] + "'>" + dado[i]["SUB_CLIENTE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ORIGEM"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VENDEDOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["SOLICITACAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ANALISE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["INFO_PEND"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["LIBERADA_INSIDE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RENEG"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["AGUARD_APROV"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["CANCELADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["REJEITADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["APROVADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["EM_UPDATE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DECLINADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["FINALIZADA_PAG"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["EM_FUP"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["APROV_EDIT"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOTIVO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["OBS_MOTIVO"] + "</td></tr > ");
                            }
                        }
                        else {
                            $("#grdRelacaoCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='13' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarStatusCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdTotalStatusCotacaoBody").empty();
                        $("#grdTotalStatusCotacaoBody").append("<tr><td colspan='2'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdTotalStatusCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdTotalStatusCotacaoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["NM_STATUS_COTACAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QUANTIDADE"] + "</td>" +
                                    "</tr>");
                            }
                        }
                        else {
                            $("#grdTotalStatusCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='2' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarModalCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdTotalModalCotacaoBody").empty();
                        $("#grdTotalModalCotacaoBody").append("<tr><td colspan='2'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdTotalModalCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdTotalModalCotacaoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["MODAL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QUANTIDADE"] + "</td>" +
                                    "</tr>");
                            }
                        }
                        else {
                            $("#grdTotalModalCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='2' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarIncotermCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdTotalIncotermCotacaoBody").empty();
                        $("#grdTotalIncotermCotacaoBody").append("<tr><td colspan='2'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdTotalIncotermCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdTotalIncotermCotacaoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["CD_INCOTERM"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QUANTIDADE"] + "</td>" +
                                    "</tr>");
                            }
                        }
                        else {
                            $("#grdTotalIncotermCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='2' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarVendedorCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdTotalVendedorCotacaoBody").empty();
                        $("#grdTotalVendedorCotacaoBody").append("<tr><td colspan='2'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdTotalVendedorCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdTotalVendedorCotacaoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["NM_VENDEDOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QUANTIDADE"] + "</td>" +
                                    "</tr>");
                            }
                        }
                        else {
                            $("#grdTotalVendedorCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='2' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarInsideCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdTotalInsideCotacaoBody").empty();
                        $("#grdTotalInsideCotacaoBody").append("<tr><td colspan='2'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdTotalInsideCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdTotalInsideCotacaoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["INSIDE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["QUANTIDADE"] + "</td>" +
                                    "</tr>");
                            }
                        }
                        else {
                            $("#grdTotalInsideCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='2' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                });
            } else {

            }
        }

        function exportRelacaoCotacaoCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdRelacaoCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdRelacaoCotacao td, #grdRelacaoCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVRelacaoCotacao(csv.join("\n"), filename);
        }

        function exportTableToCSVRelacaoCotacao(csv, filename) {
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

        function exportRelacaoCotacaoCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdTotalStatusCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdTotalStatusCotacao td, #grdTotalStatusCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            var rows = document.querySelectorAll("#grdTotalModalCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdTotalModalCotacao td, #grdTotalModalCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            var rows = document.querySelectorAll("#grdTotalIncotermCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdTotalIncotermCotacao td, #grdTotalIncotermCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            var rows = document.querySelectorAll("#grdTotalVendedorCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdTotalVendedorCotacao td, #grdTotalVendedorCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            var rows = document.querySelectorAll("#grdTotalInsideCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdTotalInsideCotacao td, #grdTotalInsideCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVRelacaoCotacao(csv.join("\n"), filename);
        }

        function exportTableToCSVRelacaoCotacao(csv, filename) {
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

        function createPDF() {
            var sTable = document.getElementById('tableRelCot').innerHTML;

            var win = window.open('', '', 'height=700', 'width=700');
            var style = "<style>";
            style = style + "table {width: 100%;font: 14px Calibri;}";
            style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
            style = style + "padding: 2px 3px;text-align: center;}";
            style = style + "</style>";
            win.document.write('<html><head>');
            win.document.write('<title>PDF</title>');
            win.document.write(style);
            win.document.write('</head>');
            win.document.write('<body>');
            win.document.write(sTable);
            win.document.write('</body></html>');
            win.document.close();
            win.print();
        }

        function PrintRelacaoCotacao() {
            $("#modalRelacaoCotacao").modal('show');
            var dtInicial = document.getElementById("txtDtInicialRelacaoCotacao").value;
            var dtFinal = document.getElementById("txtDtFinalRelacaoCotacao").value;
            var diaI = document.getElementById("txtDtInicialRelacaoCotacao").value.substring(8, 10);
            var mesI = document.getElementById("txtDtInicialRelacaoCotacao").value.substring(5, 7);
            var anoI = document.getElementById("txtDtInicialRelacaoCotacao").value.substring(0, 4);
            var diaF = document.getElementById("txtDtFinalRelacaoCotacao").value.substring(8, 10);
            var mesF = document.getElementById("txtDtFinalRelacaoCotacao").value.substring(5, 7);
            var anoF = document.getElementById("txtDtFinalRelacaoCotacao").value.substring(0, 4);
            var nota = document.getElementById("txtRelacaoCotacao").value;
            var filter = document.getElementById("ddlFilterRelacaoCotacao").value;
            var position = 20;
            var positionLineF = 21;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarRelacaoCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            var doc = new jsPDF('l');
                            var pageHeight = doc.internal.pageSize.height;
                            doc.setFontSize(15);
                            doc.setFontStyle("bold");
                            doc.text("RELAÇÃO DAS COTAÇÕES ABERTAS ENTRE " + diaI + "/" + mesI + "/" + anoI + " E " + diaF + "/" + mesF + "/" + anoF, 65, 10);
                            doc.setFontSize(7);
                            doc.text("SOLICITAÇÃO", 3, 20);
                            doc.setLineWidth(0.2);
                            doc.line(2, 17, 295, 17);
                            doc.line(2, 17, 2, 21);
                            doc.line(2, 21, 295, 21);
                            doc.line(295, 17, 295, 21);

                            doc.line(21, 17, 21, 21);
                            doc.line(46, 17, 46, 21);
                            doc.line(64, 17, 64, 21);
                            doc.line(76, 17, 76, 21);
                            doc.line(94, 17, 94, 21);
                            doc.line(124, 17, 124, 21);
                            doc.line(152, 17, 152, 21);
                            doc.line(176, 17, 176, 21);
                            doc.line(197, 17, 197, 21);
                            doc.line(224, 17, 224, 21);
                            doc.line(253, 17, 253, 21);

                            doc.text("INSIDE", 22, 20);
                            doc.text("NR COTAÇÃO", 47, 20);
                            doc.text("MODAL", 65, 20);
                            doc.text("INCOTERM", 78, 20);
                            doc.text("CLIENTE", 95, 20);
                            doc.text("SUB CLIENTE", 125, 20);
                            doc.text("ORIGEM", 153, 20);
                            doc.text("DESTINO", 177, 20);
                            doc.text("VENDEDOR", 198, 20);
                            doc.text("STATUS COTAÇÃO", 225, 20);
                            doc.text("MOTIVO CANCEL.", 254, 20);
                            for (let i = 0; i < dado.length; i++) {
                                if (position >= pageHeight - 10) {
                                    doc.line(2, 21, 2, positionLineF);
                                    doc.line(21, 21, 21, positionLineF);
                                    doc.line(46, 21, 46, positionLineF);
                                    doc.line(64, 21, 64, positionLineF);
                                    doc.line(76, 21, 76, positionLineF);
                                    doc.line(94, 21, 94, positionLineF);
                                    doc.line(124, 21, 124, positionLineF);
                                    doc.line(152, 21, 152, positionLineF);
                                    doc.line(176, 21, 176, positionLineF);
                                    doc.line(197, 21, 197, positionLineF);
                                    doc.line(224, 21, 224, positionLineF);
                                    doc.line(253, 21, 253, positionLineF);
                                    doc.line(2, positionLineF, 295, positionLineF);
                                    doc.line(295, 21, 295, positionLineF);

                                    doc.addPage();
                                    doc.setFontSize(15);
                                    doc.setFontStyle("bold");
                                    doc.text("RELAÇÃO DAS COTAÇÕES ABERTAS ENTRE " + diaI + "/" + mesI + "/" + anoI + " E " + diaF + "/" + mesF + "/" + anoF, 65, 10);
                                    doc.setFontSize(7);
                                    doc.text("SOLICITAÇÃO", 3, 20);
                                    doc.setLineWidth(0.2);
                                    doc.line(2, 17, 295, 17);
                                    doc.line(2, 17, 2, 21);
                                    doc.line(2, 21, 295, 21);
                                    doc.line(295, 17, 295, 21);

                                    doc.line(21, 17, 21, 21);
                                    doc.line(46, 17, 46, 21);
                                    doc.line(64, 17, 64, 21);
                                    doc.line(76, 17, 76, 21);
                                    doc.line(94, 17, 94, 21);
                                    doc.line(124, 17, 124, 21);
                                    doc.line(152, 17, 152, 21);
                                    doc.line(176, 17, 176, 21);
                                    doc.line(197, 17, 197, 21);
                                    doc.line(224, 17, 224, 21);
                                    doc.line(253, 17, 253, 21);

                                    doc.text("INSIDE", 22, 20);
                                    doc.text("NR COTAÇÃO", 47, 20);
                                    doc.text("MODAL", 65, 20);
                                    doc.text("INCOTERM", 78, 20);
                                    doc.text("CLIENTE", 95, 20);
                                    doc.text("SUB CLIENTE", 125, 20);
                                    doc.text("ORIGEM", 153, 20);
                                    doc.text("DESTINO", 177, 20);
                                    doc.text("VENDEDOR", 199, 20);
                                    doc.text("STATUS COTAÇÃO", 225, 20);
                                    doc.text("MOTIVO CANCEL.", 254, 20);
                                    position = 20;
                                    positionLineF = 21;
                                    
                                } else {
                                    doc.setFontSize(7)
                                    doc.line(2, positionLineF, 295, positionLineF);
                                    positionLineF = positionLineF + 5;
                                    position = position + 5;
                                    doc.setFontStyle("normal");
                                    doc.text(dado[i]["SOLICITACAO"], 3, position);
                                    doc.text(dado[i]["INSIDE"], 22, position);
                                    doc.text(dado[i]["NR_COTACAO"], 47, position);
                                    doc.text(dado[i]["MODAL"].substring(0, 15), 65, position);
                                    doc.text(dado[i]["INCOTERM"].substring(0, 15), 78, position);
                                    doc.text(dado[i]["CLIENTE"].substring(0, 15), 95, position);
                                    doc.text(dado[i]["SUB_CLIENTE"].substring(0, 15), 125, position);
                                    doc.text(dado[i]["ORIGEM"].substring(0, 15), 153, position);
                                    doc.text(dado[i]["DESTINO"].substring(0, 15), 177, position);
                                    doc.text(dado[i]["VENDEDOR"].substring(0, 15), 198, position);
                                    doc.text(dado[i]["STATUS_COTACAO"].substring(0, 15), 225, position);
                                    doc.setFontSize(6)
                                    doc.text(dado[i]["MOTIVO"], 254, position);                                    
                                }
                            }
                            doc.line(2, 21, 2, positionLineF);
                            doc.line(21, 21, 21, positionLineF);
                            doc.line(46, 21, 46, positionLineF);
                            doc.line(64, 21, 64, positionLineF);
                            doc.line(76, 21, 76, positionLineF);
                            doc.line(94, 21, 94, positionLineF);
                            doc.line(124, 21, 124, positionLineF);
                            doc.line(152, 21, 152, positionLineF);
                            doc.line(176, 21, 176, positionLineF);
                            doc.line(197, 21, 197, positionLineF);
                            doc.line(224, 21, 224, positionLineF);
                            doc.line(253, 21, 253, positionLineF);
                            doc.line(2, positionLineF, 295, positionLineF);
                            doc.line(295, 21, 295, positionLineF);
                            doc.output("dataurlnewwindow");
                        }
                        else {

                        }
                    }
                })
            } else {

            }
        }

    </script>
</asp:Content>