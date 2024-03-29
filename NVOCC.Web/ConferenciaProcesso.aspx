﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConferenciaProcesso.aspx.cs" Inherits="ABAINFRA.Web.ConferenciaProcesso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Conferencia de Processos
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Totais
                            </a>
                        </li>
                        <li id="tabprocessoEstimativaGrid">
                            <a href="#processoEstimativaGrid" id="linkprocessoEstimativaGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Analíticos
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    <div class="alert alert-danger text-center" id="msgErrExportContaPagaRecebida">
                                        Não há registros para a data informada.
                                    </div>
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportSaldo" class="btn btn-primary" onclick="exportCSV('Saldo.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterSaldo" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1" selected>Nr Processo</option>
                                                <option value="2">Cliente</option>
                                                <option value="3">Fornecedor</option>
                                                <option value="4">Nº BL Master</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtSaldo" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarSaldo" onclick="Saldo()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdSaldo" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">HBL</th>
                                                <th class="text-center" scope="col">SERVIÇO</th>
                                                <th class="text-center" scope="col">ORIGEM</th>
                                                <th class="text-center" scope="col">DESTINO</th>
                                                <th class="text-center" scope="col">CHEGADA</th>
                                                <th class="text-center" scope="col">ESTUFAGEM</th>
                                                <th class="text-center" scope="col">FREEHAND</th>
                                                <th class="text-center" scope="col">RECEBER</th>
                                                <th class="text-center" scope="col">PAGAR</th>
                                                <th class="text-center" scope="col">SALDO PROCESSO</th>
                                                <th class="text-center" scope="col">CLIENTE</th>
                                                <th class="text-center" scope="col">FORNECEDOR</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdSaldoBody">

                                        </tbody>
                                        <tfoot id="grdSaldoFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane fade" id="processoEstimativaGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px; flex-direction: column; align-items:center ">
                                    <div>
                                        <button type="button" id="btnExportEstimativaSaldo" class="btn btn-primary" onclick="exportEstimativaCSV('Pagamento_Recebimento_Estimativa.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterEstimativaSaldo" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">MBL</option>
                                                <option value="3">HBL</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtEstimativaSaldo" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarEstimativaSaldo" onclick="EstimativaPagamentosRecebimentos()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdEstimativaSaldo" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">HBL</th>
                                                <th class="text-center" scope="col">CHEGADA</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">VENDA</th>
                                                <th class="text-center" scope="col">MOEDA VENDA</th>
                                                <th class="text-center" scope="col">COMPRA</th>
                                                <th class="text-center" scope="col">MOEDA COMPRA</th>
                                                <th class="text-center" scope="col">FREEHAND</th>
                                                <th class="text-center" scope="col">ESTUFAGEM MASTER</th>
                                                <th class="text-center" scope="col">ESTUFAGEM HOUSE</th>
                                                <th class="text-center" scope="col">STATUS FRETE AGENTE</th>
                                                <th class="text-center" scope="col">TIPO PROFIT</th>
                                                <th class="text-center" scope="col">PROFIT</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdEstimativaSaldoBody">

                                        </tbody>
                                        <tfoot id="grdEstimativaSaldoFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
                                        </tfoot>
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

        function mudarTitulo() {
            document.getElementById("dtContaPagarReceber").textContent = "";
            if (document.getElementById("ddlDataFilter").value == '1') {
                document.getElementById("dtContaPagarReceber").textContent = "DATA EMBARQUE";
            } else if (document.getElementById("ddlDataFilter").value == '2') {
                document.getElementById("dtContaPagarReceber").textContent = "DATA PREVISÃO CHEGADA";
            } else if (document.getElementById("ddlDataFilter").value == '3') {
                document.getElementById("dtContaPagarReceber").textContent = "DATA CHEGADA";
            }
            EstimativaPagamentosRecebimentos();
        }

        function printRelatorioPagamentosRecebimentos() {
            var dtInicial = document.getElementById("txtDtInicialSaldo").value;
            var dtFinal = document.getElementById("txtDtFinalSaldo").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarRelatorioContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        var rel = [["PROCESSO;MBL;ITEM DESPESA;DATA LIQUIDAÇÃO REC;CLIENTE REC; VALOR DEVIDO REC;MOEDA REC; CAMBIO REC;VALOR LIQUIDO REC; VALOR ISS REC; DATA LIQUIDAÇÃO PAG;FORNECEDOR PAG; VALOR DEVIDO PAG; MOEDA PAG; CAMBIO PAG; VALOR LIQUIDO PAG; VALOR ISS PAG; TIPO EXPORTAÇÃO; DATA EMISSAO; DATA VENCIMENTO; TIPO FATURAMENTO; DATA PREVISÃO DE CHEGADA;DATA CHEGADA; TIPO DE ESTUFAGEM; CLIENTE FINAL; AGENTE;TIPO DE PAGAMENTO MASTER; TIPO PAGAMENTO HOUSE"]];
                        if (dado != null) {
                            console.log(dado)
                            for (let i = 0; i < dado.length; i++) {
                                rel.push([dado[i]])
                            }
                            console.log(rel);
                            exportRelatorioPagamentosRecebimentosCSV("previsibilidade.csv", rel.join("\n"));
                        } else {
                            $("#msgErrExportContaPagaRecebida").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            } else {
                $("#msgErrExportContaPagaRecebida").fadeIn(500).delay(1000).fadeOut(500);
            }
        }
        //PagamentosRecebimentos

        function Saldo() {
            var nota = document.getElementById("txtSaldo").value;
            var filter = document.getElementById("ddlFilterSaldo").value;
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarSaldo",
                    data: '{nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdSaldoBody").empty();
                        $("#grdSaldoBody").append("<tr><td colspan='14'><div class='loader'></div></td></tr>");
                        $("#grdSaldoFooter").empty();
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        var liqrec = 0;
                        var liqpag = 0;
                        var liqsaldo = 0;
                        var chegada = "";
                        $("#grdSaldoBody").empty();
                        $("#grdSaldoFooter").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                if (dado[i]["CHEGADA"] == null) {
                                    chegada = "";
                                } else {
                                    chegada = dado[i]["CHEGADA"];
                                }
                                $("#grdSaldoBody").append("<tr><td class='text-center'> " + dado[i]["PROCESSO"] + "</td><td class='text-center'> " + dado[i]["MBL"] + "</td><td class='text-center'>" + dado[i]["HBL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TPSERVICO"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                    "<td class='text-center'>" + chegada + "</td><td class='text-center'>" + dado[i]["TPESTUFAGEM"] + "</td><td class='text-center'>" + dado[i]["FREEHAND"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["RECEBER"] + "</td><td class='text-center'>" + dado[i]["PAGAR"] + "</td><td class='text-center'>" + dado[i]["SALDO_PROCESSO"] + "</td>" +
                                    "<td class='text-center' style='max-width: 15ch;' title='" + dado[i]["CLIENTE"] + "'>" + dado[i]["CLIENTE"] + "</td><td class='text-center' style='max-width: 15ch;' title='" + dado[i]["FORNECEDOR"] + "'>" + dado[i]["FORNECEDOR"] + "</td></tr > ");
                                liqrec = liqrec + parseFloat(dado[i]["RECEBER"]);
                                liqpag = liqpag + parseFloat(dado[i]["PAGAR"]);
                                liqsaldo = liqsaldo + parseFloat(dado[i]["SALDO_PROCESSO"]);
                            }
                            $("#grdSaldoFooter").append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + liqrec.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td><td>" + liqpag.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td><td>" + liqsaldo.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td><td></td><td></td></tr>");
                        }
                        else {
                            $("#grdSaldoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }

        function exportContaPrevisibilidadeProcesso(file) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ContaPrevisibilidadeProcesso",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var previProcesso = [["PROCESSO;MASTER;HOUSE;TIPO SERVICO;TIPO ESTUFAGEM;TIPO PAGAMENTO HOUSE;TIPO PAGAMENTO MASTER;CNTR20;CNTR40;ORIGEM;DESTINO;DATA EMBARQUE;DATA PREVISAO CHEGADA;PARCEIRO;CNEE;INDICADOR;AGENTE;A RECEBER BRL;A PAGAR BRL;SALDO BRL"]];
                        for (let i = 0; i < dado.length; i++) {
                            previProcesso.push([dado[i]]);
                        }
                        exportContaPrevisibilidadeProcessoCSV(file, previProcesso.join("\n"));
                    }
                }
            })
        }

        function exportContaConferenciaProcesso(file) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ContaConferenciaProcesso",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var confProcesso = [["PROCESSO;PROCEDENCIA;ITEM;VALOR BRL;ESTUFAGEM MASTER;ESTUFAGEM HOUSE;PAGAMENTO MASTER;PAGAMENTO HOUSE;PAGAMENTO TAXA;ORIGEM;DECLARADO;FREEHAND;STATUS FRETE;DIVISAO PROFIT"]];
                        for (let i = 0; i < dado.length; i++) {
                            confProcesso.push([dado[i]]);
                        }
                        exportContaConferenciaProcessoCSV(file, confProcesso.join("\n"));
                    }
                }
            })
        }

        function exportRelatorioPagamentosRecebimentosCSV(file, array) {
            var csvFile;

            var downloadLink;


            // CSV file
            csvFile = new Blob(["\uFEFF" + array], { type: "text/csv;charset=utf-8;" });

            // Download link
            downloadLink = document.createElement("a");


            // File name
            downloadLink.download = file;


            // Create a link to the file
            downloadLink.href = window.URL.createObjectURL(csvFile);


            // Hide download link
            downloadLink.style.display = "none";



            // Add the link to DOM
            document.body.appendChild(downloadLink);



            // Click download link
            downloadLink.click();
        }

        function exportContaPrevisibilidadeProcessoCSV(file, array)  {
                var csvFile;

                var downloadLink;


                // CSV file
                csvFile = new Blob(["\uFEFF" + array], { type: "text/csv;charset=utf-8;" });

                // Download link
                downloadLink = document.createElement("a");


                // File name
                downloadLink.download = file;


                // Create a link to the file
                downloadLink.href = window.URL.createObjectURL(csvFile);


                // Hide download link
                downloadLink.style.display = "none";



                // Add the link to DOM
                document.body.appendChild(downloadLink);



                // Click download link
                downloadLink.click();
            }

        function exportContaConferenciaProcessoCSV(file, array) {
            var csvFile;

            var downloadLink;


            // CSV file
            csvFile = new Blob(["\uFEFF" + array], { type: "text/csv;charset=utf-8;" });

            // Download link
            downloadLink = document.createElement("a");


            // File name
            downloadLink.download = file;


            // Create a link to the file
            downloadLink.href = window.URL.createObjectURL(csvFile);


            // Hide download link
            downloadLink.style.display = "none";



            // Add the link to DOM
            document.body.appendChild(downloadLink);



            // Click download link
            downloadLink.click();
        }

        function exportCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdSaldo tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdSaldo td, #grdSaldo th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVPagamentosRecebimentos(csv.join("\n"), filename);
        }

        function exportTableToCSVPagamentosRecebimentos(csv, filename) {
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

        function printPagamentosRecebimentos() {
            var dtInicial = document.getElementById("txtDtInicialSaldo").value;
            var diaI = dtInicial.substring(8, 10);
            var mesI = dtInicial.substring(5, 7);
            var anoI = dtInicial.substring(0, 4);
            var dtFinal = document.getElementById("txtDtFinalSaldo").value;
            var diaF = dtFinal.substring(8, 10);
            var mesF = dtFinal.substring(5, 7);
            var anoF = dtFinal.substring(0, 4);
            var nota = document.getElementById("txtSaldo").value;
            var filter = document.getElementById("ddlFilterSaldo").value;
            var position = 27;
            var positionv = 28;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarContasRecebidasPagas",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var doc = new jsPDF('l');
                        var pageHeight = doc.internal.pageSize.height;
                        doc.setFontStyle("bold");
                        doc.setFontSize(15);
                        doc.text("CONTAS PAGAS E RECEBIDAS ENTRE " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 68, 13);
                        doc.setFontSize(7);
                        doc.text("NR PROCESSO", 4, 27);
                        doc.setLineWidth(0.2);
                        doc.line(3, 20, 295, 20);
                        doc.line(3, 24, 295, 24);
                        doc.line(3, 28, 295, 28);


                        doc.line(3, 20, 3, 28);
                        doc.line(26, 24, 26, 28);
                        doc.line(69, 20, 69, 28);
                        doc.line(89, 24, 89, 28);
                        doc.line(119, 24, 119, 28);
                        doc.line(134, 24, 134, 28);
                        doc.line(146, 24, 146, 28);
                        doc.line(161, 24, 161, 28);
                        doc.line(184, 20, 184, 28);
                        doc.line(199, 24, 199, 28);
                        doc.line(229, 24, 229, 28);
                        doc.line(244, 24, 244, 28);
                        doc.line(259, 24, 259, 28);
                        doc.line(274, 24, 274, 28);
                        doc.line(295, 20, 295, 28);

                        doc.text("ITEM DESPESA", 27, 27);
                        doc.text("RECEBIMENTO", 110, 23);
                        doc.text("DATA", 70, 27);
                        doc.text("CLIENTE", 90, 27);
                        doc.text("DEVIDO", 120, 27);
                        doc.text("MOEDA", 135, 27);
                        doc.text("CAMBIO", 147, 27);
                        doc.text("LIQUIDADO", 162, 27);
                        doc.text("PAGAMENTO", 230, 23);
                        doc.text("DATA", 185, 27);
                        doc.text("FORNECEDOR", 200, 27);
                        doc.text("DEVIDO", 230, 27);
                        doc.text("MOEDA", 245, 27);
                        doc.text("CAMBIO", 260, 27);
                        doc.text("LIQUIDADO", 275, 27);
                        for (let i = 0; i < dado.length; i++) {
                            if (position >= pageHeight -10 ) {
                                doc.line(3, positionv, 295, positionv);
                                doc.line(3, 28, 3, positionv);
                                doc.line(26, 28, 26, positionv);
                                doc.line(69, 28, 69, positionv);
                                doc.line(89, 28, 89, positionv);
                                doc.line(119, 28, 119, positionv);
                                doc.line(134, 28, 134, positionv);
                                doc.line(146, 28, 146, positionv);
                                doc.line(161, 28, 161, positionv);
                                doc.line(184, 28, 184, positionv);
                                doc.line(199, 28, 199, positionv);
                                doc.line(229, 28, 229, positionv);
                                doc.line(244, 28, 244, positionv);
                                doc.line(259, 28, 259, positionv);
                                doc.line(274, 28, 274, positionv);
                                doc.line(295, 28, 295, positionv);
                                doc.line(3, 28, 3, positionv);
                                doc.addPage();
                                doc.setFontStyle("bold");
                                doc.setFontSize(15);
                                doc.text("CONTAS PAGAS E RECEBIDAS ENTRE " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 68, 13);
                                doc.setFontSize(7);
                                doc.text("NR PROCESSO", 4, 27);
                                doc.setLineWidth(0.2);
                                doc.line(3, 20, 295, 20);
                                doc.line(3, 24, 295, 24);
                                doc.line(3, 28, 295, 28);
                                doc.line(3, 20, 3, 28);
                                doc.line(26, 24, 26, 28);
                                doc.line(69, 20, 69, 28);
                                doc.line(89, 24, 89, 28);
                                doc.line(119, 24, 119, 28);
                                doc.line(134, 24, 134, 28);
                                doc.line(146, 24, 146, 28);
                                doc.line(161, 24, 161, 28);
                                doc.line(184, 20, 184, 28);
                                doc.line(199, 24, 199, 28);
                                doc.line(229, 24, 229, 28);
                                doc.line(244, 24, 244, 28);
                                doc.line(259, 24, 259, 28);
                                doc.line(274, 24, 274, 28);
                                doc.line(295, 20, 295, 28);
                                doc.text("ITEM DESPESA", 27, 27);
                                doc.text("RECEBIMENTO", 110, 23);
                                doc.text("DATA", 70, 27);
                                doc.text("CLIENTE", 90, 27);
                                doc.text("DEVIDO", 120, 27);
                                doc.text("MOEDA", 135, 27);
                                doc.text("CAMBIO", 147, 27);
                                doc.text("LIQUIDADO", 162, 27);
                                doc.text("PAGAMENTO", 230, 23);
                                doc.text("DATA", 185, 27);
                                doc.text("FORNECEDOR", 200, 27);
                                doc.text("DEVIDO", 230, 27);
                                doc.text("MOEDA", 245, 27);
                                doc.text("CAMBIO", 260, 27);
                                doc.text("LIQUIDADO", 275, 27);
                                position = 27;
                                positionv = 28;
                            } else {
                                position = position + 5;
                                positionv = positionv + 5;
                                doc.setFontStyle("normal");
                                doc.line(3, positionv, 295, positionv);
                                doc.text(dado[i]["NR_PROCESSO"], 4, position);
                                doc.text(dado[i]["NM_ITEM_DESPESA"], 27, position);
                                doc.text(dado[i]["DT_LIQUIDACAO_REC"], 70, position);
                                doc.text(dado[i]["NM_CLIENTE_REC"].substring(0, 15), 90, position);
                                doc.text(dado[i]["VL_DEVIDO_REC"], 120, position);
                                doc.text(dado[i]["MOEDA_REC"], 135, position);
                                doc.text(dado[i]["VL_CAMBIO_REC"], 147, position);
                                doc.text(dado[i]["VL_LIQUIDO_REC"], 162, position);
                                doc.text(dado[i]["DT_LIQUIDACAO_PAG"], 185, position);
                                doc.text(dado[i]["NM_FORNECEDOR_PAG"].substring(0, 15), 200, position);
                                doc.text(dado[i]["VL_DEVIDO_PAG"], 230, position);
                                doc.text(dado[i]["MOEDA_PAG"], 245, position);
                                doc.text(dado[i]["VL_CAMBIO_PAG"], 260, position);
                                doc.text(dado[i]["VL_LIQUIDO_PAG"], 275, position);
                            }
                        }
                        doc.line(3, positionv, 295, positionv);
                        doc.line(3, 28, 3, positionv);
                        doc.line(26, 28, 26, positionv);
                        doc.line(69, 28, 69, positionv);
                        doc.line(89, 28, 89, positionv);
                        doc.line(119, 28, 119, positionv);
                        doc.line(134, 28, 134, positionv);
                        doc.line(146, 28, 146, positionv);
                        doc.line(161, 28, 161, positionv);
                        doc.line(184, 28, 184, positionv);
                        doc.line(199, 28, 199, positionv);
                        doc.line(229, 28, 229, positionv);
                        doc.line(244, 28, 244, positionv);
                        doc.line(259, 28, 259, positionv);
                        doc.line(274, 28, 274, positionv);
                        doc.line(295, 28, 295, positionv);
                        doc.line(3, 28, 3, positionv);
                        doc.output("dataurlnewwindow");
                    }
                    else {
                    }
                }
            })

        }


        function EstimativaPagamentosRecebimentos() {
            var nota = document.getElementById("txtEstimativaSaldo").value;
            var filter = document.getElementById("ddlFilterEstimativaSaldo").value;
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarConferencia",
                    data: '{nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdEstimativaSaldoBody").empty();
                        $("#grdEstimativaSaldoBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        var liqrec = 0;
                        var liqpag = 0;
                        var profit = 0;
                        $("#grdEstimativaSaldoBody").empty(); 
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                if (dado[i]["CHEGADA"] == null) {
                                    chegada = "";
                                } else {
                                    chegada = dado[i]["CHEGADA"];
                                }
                                $("#grdEstimativaSaldoBody").append("<tr><td class='text-center'>" + dado[i]["PROCESSO"] + "</td><td class='text-center'> " + dado[i]["MBL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["HBL"] + "</td><td class='text-center'>" + chegada + "</td><td class='text-center'>" + dado[i]["ITEM_DESPESA"] + "</td><td class='text-center'>" + dado[i]["VENDA"] + "</td><td class='text-center'>" + dado[i]["MOEDA_VENDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["COMPRA"] + "</td><td class='text-center'>" + dado[i]["MOEDA_COMPRA"] + "</td><td class='text-center'>" + dado[i]["FREEHAND"] + "</td><td class='text-center'>" + dado[i]["ESTUFAGEM_MASTER"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ESTUFAGEM_HOUSE"] + "</td><td class='text-center' style='max-width: 15ch;' title='"+ dado[i]["STATUS_FRETE"] +"'>" + dado[i]["STATUS_FRETE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TIPO_PROFIT"] + "</td><td class='text-center'>" + dado[i]["PROFIT"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdEstimativaSaldoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='16' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }

        function exportEstimativaCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdEstimativaSaldo tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdEstimativaSaldo td, #grdEstimativaSaldo th");

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
            $("#modalEstimativaSaldo").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaSaldo").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaSaldo").value;
            var nota = document.getElementById("txtEstimativaSaldo").value;
            var filter = document.getElementById("ddlFilterEstimativaSaldo").value;
            var position = 45;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarContasAReceberAPagar",
                    data: '{filterby: "' + ddlDataFilter.value +'", dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
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
                                doc.text(dado[i]["DATA"], 4, position);
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
