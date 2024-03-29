﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstimativaContaPagarReceber.aspx.cs" Inherits="ABAINFRA.Web.EstimativaContaPagarReceber" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Contas Pagar e Receber
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Contas Pagar e Receber
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
                                        <button type="button" id="btnExportEstimativaPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Pagamento_Recebimento.csv')">Exportar Grid - CSV</button>
                                            <button type="button" id="btnPrintEstimativaPagamentoRecebimento" class="btn btn-primary" onclick="PrintEstimativaPagamentosRecebimentos()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Inicial:</label>
                                            <input id="txtDtInicialEstimativaPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Final:</label>
                                            <input id="txtDtFinalEstimativaPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterEstimativaPagamentoRecebimento" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Cliente</option>
                                                <option value="3">Fornecedor</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtEstimativaPagamentoRecebimento" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarEstimativaPagamentoRecebimento" onclick="EstimativaPagamentosRecebimentos()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdEstimativaPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">CHEGADA OU EMBARQUE</th>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">IMP / EXP</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">CLIENTE (REC)</th>
                                                <th class="text-center" scope="col">DEVIDO (REC)</th>
                                                <th class="text-center" scope="col">MOEDA (REC)</th>
                                                <th class="text-center" scope="col">CAMBIO (REC)</th>
                                                <th class="text-center" scope="col">DATA CAMBIO (REC)</th>
                                                <th class="text-center" scope="col">RECEBER (R$)</th>
                                                <th class="text-center" scope="col">FORNECEDOR (PAG)</th>
                                                <th class="text-center" scope="col">DEVIDO (PAG)</th>
                                                <th class="text-center" scope="col">MOEDA (PAG)</th>
                                                <th class="text-center" scope="col">CAMBIO (PAG)</th>
                                                <th class="text-center" scope="col">DATA CAMBIO (PAG)</th>
                                                <th class="text-center" scope="col">PAGAR (R$)</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdEstimativaPagamentoRecebimentoBody">

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
        var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();

        function validaDat(data) {
            var date = data;
            var ardt = new Array;
            var ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
            ardt = date.split("/");
            erro = false;
            if (date.search(ExpReg) == -1) {
                erro = true;
            }
            else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
                erro = true;
            else if (ardt[1] == 2) {
                if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
                    erro = true;
                if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
                    erro = true;
            }
            if (erro) {
                return false;
            }
            return true;
        }

        function compareDates(dateE, dateV) {
            let emissao = dateE.split('/') // separa a data pelo caracter '/'
            let vencimento = dateV.split('/')      // pega a data atual

            dateE = new Date(emissao[2], emissao[1] - 1, emissao[0]) // formata 'date'
            dateV = new Date(vencimento[2], vencimento[1] - 1, vencimento[0])

            // compara se a data informada é maior que a data atual
            // e retorna true ou false
            return dateE > dateV ? false : true
        }


        //PagamentosRecebimentos

        function EstimativaPagamentosRecebimentos() {
            $("#modalEstimativaPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarEstimativaContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdEstimativaPagamentoRecebimentoBody").empty();
                        $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdEstimativaPagamentoRecebimentoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td class='text-center'> " + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TP_SERVICO"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE_REC"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_REC"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_FORNECEDOR_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_PAG"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_PAG"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdEstimativaPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='16' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
            } else {

            }
        }

        function exportEstimativaCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdEstimativaPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdEstimativaPagamentoRecebimento td, #grdEstimativaPagamentoRecebimento th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVEstimativaPagamentosRecebimentos(csv.join("\n"), filename);
        }

        function exportTableToCSVEstimativaPagamentosRecebimentos(csv, filename) {
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

        function PrintEstimativaPagamentosRecebimentos() {
            $("#modalEstimativaPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            var position = 45;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarEstimativaContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            var doc = new jsPDF('l');
                            doc.setFontSize(7);
                            doc.setFontStyle("bold");
                            doc.text("DATA CHEGADA", 4, 41);
                            doc.text("EMBARQUE", 4, 45);
                            doc.setLineWidth(0.2);
                            doc.line(3.7, 38, 295, 38);
                            doc.line(3.7, 38, 3.7, 46);
                            doc.line(295, 46, 295, 46);
                            doc.line(295, 38, 295, 46);
                            doc.line(3.7, 46, 295, 46);
                            doc.line(3.7, 42, 295, 42);
                            doc.line(94, 38, 94, 46);
                            doc.line(119, 42, 119, 46);
                            doc.line(134, 42, 134, 46);
                            doc.line(146, 42, 146, 46);
                            doc.line(159, 42, 159, 46);
                            doc.line(174, 42, 174, 46);
                            doc.line(195, 38, 195, 46);
                            doc.line(225, 42, 225, 46);
                            doc.line(238, 42, 238, 46);
                            doc.line(250, 42, 250, 46);
                            doc.line(262, 42, 262, 46);
                            doc.line(277, 42, 277, 46);
                            doc.text("NR PROCESSO", 27, 45);
                            doc.text("RECEBIMENTO", 110, 41);
                            doc.text("IMP / EXP", 50, 45);
                            doc.text("ITEM DESPESA", 65, 45);
                            doc.text("CLIENTE", 95, 45);
                            doc.text("DEVIDO", 120, 45);
                            doc.text("MOEDA", 135, 45);
                            doc.text("CAMBIO", 147, 45);
                            doc.text("DATA", 160, 41);
                            doc.text("CAMBIO", 160, 45);
                            doc.text("PAGAMENTO", 215, 41);
                            doc.text("RECEBER", 175, 45);
                            doc.text("FORNECEDOR", 196, 45);
                            doc.text("DEVIDO", 226, 45);
                            doc.text("MOEDA", 239, 45);
                            doc.text("CAMBIO", 251, 45);
                            doc.text("DATA", 263, 41);
                            doc.text("CAMBIO", 263, 45);
                            doc.text("PAGAR", 278, 45);
                            for (let i = 0; i < dado.length; i++) {
                                position = position + 5;
                                doc.setFontStyle("normal");
                                doc.text(dado[i]["DT_CHEGADA"], 4, position);
                                doc.text(dado[i]["NR_PROCESSO"], 27, position);
                                doc.text(dado[i]["TP_SERVICO"], 50, position);
                                doc.text(dado[i]["NM_ITEM_DESPESA"].substring(0, 15), 65, position);
                                doc.text(dado[i]["NM_CLIENTE_REC"].substring(0, 15), 95, position);
                                doc.text(dado[i]["VL_DEVIDO_REC"], 120, position);
                                doc.text(dado[i]["MOEDA_REC"], 135, position);
                                doc.text(dado[i]["VL_CAMBIO_REC"], 147, position);
                                doc.text(dado[i]["DT_CAMBIO_REC"], 160, position);
                                doc.text(dado[i]["VL_LIQUIDO_REC"].substring(0, 15), 175, position);
                                doc.text(dado[i]["NM_FORNECEDOR_PAG"].substring(0, 15), 196, position);
                                doc.text(dado[i]["VL_DEVIDO_PAG"], 226, position);
                                doc.text(dado[i]["MOEDA_PAG"], 239, position);
                                doc.text(dado[i]["VL_CAMBIO_PAG"], 251, position);
                                doc.text(dado[i]["DT_CAMBIO_PAG"], 263, position);
                                doc.text(dado[i]["VL_LIQUIDO_PAG"], 278, position);
                            }
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