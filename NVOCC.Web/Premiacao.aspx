<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Premiacao.aspx.cs" Inherits="ABAINFRA.Web.Premiacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Premiação Nacional
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Premiação Nacional - Rateamento
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportEstimativaCSV('Premiacao.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintPagamentoRecebimento" class="btn btn-primary" onclick="imprimirPremiacao()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Competência:</label>
                                            <input id="txtDtCompentencia" class="form-control competencia" type="text" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Quinzena</label>
                                            <input id="txtQuinzena" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarPremiacao" onclick="listarpremiacao()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div>
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdPremiacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">COMPETÊNCIA</th>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">INDICADOR</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">HBL</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">TIPO ESTUFAGEM</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                                <th class="text-center" scope="col">VALOR COMPRA</th>
                                                <th class="text-center" scope="col">TAXA CONVERSÃO</th>
                                                <th class="text-center" scope="col">PREMIAÇÃO</th>
                                                <th class="text-center" scope="col">% RATEIO</th>
                                                <th class="text-center" scope="col">TOTAL</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdPremiacaoBody">

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
    <script src="Content/js/site.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>

    <script>

        //PagamentosRecebimentos
        var dtInicial;
        var nota;

        function imprimirPremiacao() {
            dtInicial = document.getElementById("txtDtCompentencia").value;
            nota = document.getElementById("txtQuinzena").value;
            var posih = 3;
            var posiv = 25;
            var pcindicador = [];
            var idpc = "";
            var totalpremiacao = 0;
            var totalpc = 0;
            dtInicial = dtInicial.toString().replace("/", "");
            console.log(dtInicial);
            if (dtInicial != "") {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/listarPremiacao",
                    data: '{dtCompetencia:"' + dtInicial + '",quinzena: "' + nota + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        console.log(dado);
                        if (dado != null) {
                            var doc = new jsPDF('l');
                            var pageHeight = doc.internal.pageSize.height;
                            var imgData = new Image();
                            doc.setFontSize(12);
                            doc.setFontStyle("bold");
                            doc.text("RATEAMENTO DA PREMIÇÃO", 120, 7);
                            doc.setFontSize(9);
                            doc.setFontStyle("normal");
                            doc.text("COMPETÊNCIA - " + dado[0]["COMPETENCIA"].substr(0, 2) + "/" + dado[0]["COMPETENCIA"].substr(2, dado[0]["COMPETENCIA"].length), 120, 12);
                            for (let x = 0; x < dado.length; x++) {
                                if (pcindicador.indexOf(dado[x]["AGENTE"]) == -1) {
                                    pcindicador.push(dado[x]["AGENTE"]);
                                }
                            }
                            console.log(pcindicador);
                            doc.setFontSize(8);
                            doc.text("AGENTE", posih, posiv);
                            doc.text("MBL", posih + 30, posiv);
                            doc.text("HBL", posih + 65, posiv);
                            doc.text("CNEE", posih + 95, posiv);
                            doc.text("TIPO ESTUF", posih+ 130, posiv);
                            doc.text("VALOR COMPRA", posih + 153, posiv);
                            doc.text("MOEDA", posih + 180, posiv);
                            doc.text("TX. CONVERSAO", posih + 200, posiv);
                            doc.text("PREMIACAO", posih+230, posiv);
                            doc.text("MOEDA", posih + 255, posiv);
                            doc.text("% RATEIO", posih + 275, posiv);
                            doc.setFontStyle("normal");
                            for (let i = 0; i < pcindicador.length; i++) {
                                totalpremiacao = 0;
                                totalpc = 0;
                                idpc = "";
                                if (idpc == "") {
                                    idpc = pcindicador[i];
                                }
                                for (let z = 0; z < dado.length; z++) {
                                    if (idpc == dado[z]["AGENTE"]) {
                                        if (posiv < pageHeight - 10) {
                                            doc.setFontSize(8);
                                            doc.setFontStyle("normal");
                                            posiv = posiv + 5;
                                            doc.text(dado[z]["AGENTE"].substr(0, 15), posih, posiv);
                                            doc.text(dado[z]["MBL"], posih + 30, posiv);
                                            doc.text(dado[z]["HBL"], posih + 65, posiv);
                                            doc.text(dado[z]["INDICADOR"].substr(0, 15), posih + 95, posiv);
                                            doc.text(dado[z]["ESTUFAGEM"], posih + 130, posiv);
                                            doc.text(dado[z]["VALOR"].toFixed(2), posih + 153, posiv);
                                            doc.text(dado[z]["MOEDA"], posih + 180, posiv);
                                            doc.text(dado[z]["CAMBIO"].toFixed(5), posih + 200, posiv);
                                            doc.text(dado[z]["PREMIACAO"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), posih + 230, posiv);
                                            doc.text(dado[z]["RATEIO"].toString() + '%', posih + 275, posiv);
                                            totalpremiacao = totalpremiacao + parseFloat(dado[z]["PREMIACAO"].toFixed(2));
                                            totalpc = totalpc + parseFloat(dado[z]["RATEIO"].toFixed());
                                        } else {
                                            doc.addPage();
                                            doc.setFontSize(12);
                                            doc.setFontStyle("bold");
                                            doc.text("RATEAMENTO DA PREMIÇÃO", 120, 7);
                                            doc.setFontSize(9);
                                            doc.setFontStyle("normal");
                                            doc.text("COMPETÊNCIA - " + dado[0]["COMPETENCIA"].substr(0, 2) + "/" + dado[0]["COMPETENCIA"].substr(2, dado[0]["COMPETENCIA"].length), 120, 12);
                                            for (let x = 0; x < dado.length; x++) {
                                                if (pcindicador.indexOf(dado[x]["AGENTE"]) == -1) {
                                                    pcindicador.push(dado[x]["AGENTE"]);
                                                }
                                            }
                                            console.log(pcindicador);
                                            doc.setFontSize(8);
                                            doc.text("AGENTE", posih, posiv);
                                            doc.text("MBL", posih + 30, posiv);
                                            doc.text("HBL", posih + 65, posiv);
                                            doc.text("CNEE", posih + 95, posiv);
                                            doc.text("TIPO ESTUF", posih + 130, posiv);
                                            doc.text("VALOR COMPRA", posih + 153, posiv);
                                            doc.text("MOEDA", posih + 180, posiv);
                                            doc.text("TX. CONVERSAO", posih + 200, posiv);
                                            doc.text("PREMIACAO", posih + 230, posiv);
                                            doc.text("MOEDA", posih + 255, posiv);
                                            doc.text("% RATEIO", posih + 275, posiv);
                                            doc.setFontStyle("normal");
                                            posih = 3;
                                            posiv = 25;
                                            doc.setFontSize(8);
                                            doc.setFontStyle("normal");
                                            posiv = posiv + 5;
                                            doc.text(dado[z]["AGENTE"].substr(0, 15), posih, posiv);
                                            doc.text(dado[z]["MBL"], posih + 30, posiv);
                                            doc.text(dado[z]["HBL"], posih + 65, posiv);
                                            doc.text(dado[z]["INDICADOR"].substr(0, 15), posih + 95, posiv);
                                            doc.text(dado[z]["ESTUFAGEM"], posih + 130, posiv);
                                            doc.text(dado[z]["VALOR"].toFixed(2), posih + 153, posiv);
                                            doc.text(dado[z]["MOEDA"], posih + 180, posiv);
                                            doc.text(dado[z]["CAMBIO"].toFixed(5), posih + 200, posiv);
                                            doc.text(dado[z]["PREMIACAO"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), posih + 230, posiv);
                                            doc.text(dado[z]["RATEIO"].toString() + '%', posih + 275, posiv);
                                            totalpremiacao = totalpremiacao + parseFloat(dado[z]["PREMIACAO"].toFixed(2));
                                            totalpc = totalpc + parseFloat(dado[z]["RATEIO"].toFixed());
                                        }
                                    } else {
                                        if (z == dado.length - 1) {
                                            doc.setFontStyle("bold");
                                            doc.setFontSize(9);
                                            doc.text('TOTAL PREMIAÇÃO >>>>', posih + 95, posiv + 5);
                                            doc.text(totalpremiacao.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), posih + 230, posiv + 5);
                                            doc.text(totalpc.toString() + '%', posih + 275, posiv + 5);
                                            posiv = posiv + 5;
                                        }
                                    }
                                }
                            }
                            doc.setFontStyle("bold");
                            doc.setFontSize(9);
                            doc.text('TOTAL PREMIAÇÃO >>>>', posih + 95, posiv + 5);
                            doc.text(totalpremiacao.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), posih + 230, posiv + 5);
                            doc.text(totalpc.toString() + '%', posih + 275, posiv + 5);
                            doc.output("dataurlnewwindow");
                        }
                        else {
                        }
                    }
                })
            } else {

            }
        }

        function listarpremiacao() {
            dtInicial = document.getElementById("txtDtCompentencia").value;
            nota = document.getElementById("txtQuinzena").value;
            dtInicial = dtInicial.toString().replace("/", "");
            var indicador;
            var totaldiv;
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarPremiacao",
                data: '{dtCompetencia:"' + dtInicial + '",quinzena: "' + nota + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdPremiacaoBody").empty();
                    $("#grdPremiacaoBody").append("<tr><td colspan='12'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdPremiacaoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            if (indicador != dado[i]["AGENTE"]) {
                                totaldiv = "<td class='text-center'>" + dado[i]["TOTAL"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>";
                            } else {
                                totaldiv = "<td class='text-center'></td>";
                            }
                            $("#grdPremiacaoBody").append("<tr>" +
                                "<td class='text-center'>" + dado[i]["COMPETENCIA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["INDICADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["MBL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["HBL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["AGENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ESTUFAGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["MOEDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VALOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CAMBIO"].toString().replace(".", ",") + "</td>" +
                                "<td class='text-center'>" + dado[i]["PREMIACAO"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>" +                                
                                "<td class='text-center'>" + dado[i]["RATEIO"].toString().replace(".",",") + "</td>" +
                                totaldiv + "</tr>");
                            indicador = dado[i]["AGENTE"];
                        }
                    }
                    else {
                        $("#grdPremiacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='12' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            });
        }

        function exportCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdInvoice tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdInvoice td, #grdInvoice th");

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
            var dtInicial = document.getElementById("txtDtInicialVencimentoInvoice").value;
            var diaI = dtInicial.substring(8, 10);
            var mesI = dtInicial.substring(5, 7);
            var anoI = dtInicial.substring(0, 4);
            var dtFinal = document.getElementById("txtDtFinalVencimentoInvoice").value;
            var diaF = dtFinal.substring(8, 10);
            var mesF = dtFinal.substring(5, 7);
            var anoF = dtFinal.substring(0, 4);
            var nota = document.getElementById("txtInvoice").value;
            var filter = document.getElementById("ddlFilterInvoice").value;
            var position = 27;
            var positionv = 28;
            var datetime = new Date().toLocaleString('pt-BR');
            var h = 3;
            var v = 30;
            var moeda = "";
            var total = 0;
            var totalbrl = 0;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarInvoicesQuitadas",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    console.log(dado);
                    if (dado != null) {
                        var doc = new jsPDF('l');
                        var pageHeight = doc.internal.pageSize.height;
                        var imgData = new Image();
                        imgData.src = 'Content/imagens/FCA-LOG(deitado).png';
                        doc.setFontStyle("bold");
                        doc.setFontSize(18);
                        doc.text("Relatórios de Invoices Quitadas ", 110, 13);
                        doc.setFontSize(10);
                        doc.setFontStyle("normal");
                        doc.text("Quitadas de " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 130, 18);
                        doc.addImage(imgData, 'png', 10, 5, 60, 15);
                        doc.setFontSize(10);
                        doc.setLineWidth(0.2);
                        doc.setFontStyle("bold");
                        doc.line(h - 1, v + 1, h + 290, v + 1);
                        doc.setFontSize(7);
                        doc.text("Agente Account", h, v);
                        doc.text("Quitação", h + 25, v);
                        doc.text("Contrato Camb.", h + 40, v);
                        doc.text("MBL", h + 61, v);
                        doc.text("HBL", h + 88, v);
                        doc.text("Processo", h + 114, v);
                        doc.text("Invoice/Act Nº", h + 129 , v);
                        doc.text("Invoice Date", h + 173, v);
                        doc.text("Tx.", h + 191, v);
                        doc.text("Valor Inv.", h + 203, v);
                        doc.text("BRL", h + 220, v);
                        doc.text("Tx. Receb.", h + 235, v);
                        doc.text("Dt. Receb.", h + 250, v);
                        doc.text("Cnee", h + 265, v);
                        doc.setFontSize(7);
                        doc.setFontStyle("normal    ");

                        for (let i = 0; i < dado.length; i++) {
                            if (moeda == "") {
                                moeda = dado[i]["SIGLA"].toString();
                                console.log(moeda);
                            }
                            if (v > pageHeight - 10) {
                                h = 3;
                                v = 30;
                                doc.addPage();
                                doc.setFontSize(18);
                                doc.setFontStyle("bold");
                                doc.text("Relatórios de Invoices Quitadas ", 110, 13);
                                doc.setFontSize(9);
                                doc.line(h - 1, v + 1, h + 290, v + 1);
                                doc.setFontStyle("normal    ");
                                doc.text("Quitadas de " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 130, 18);
                                doc.addImage(imgData, 'png', 10, 5, 60, 15);
                                doc.setFontSize(7);
                                doc.setLineWidth(0.2);
                                doc.setFontStyle("bold");

                                doc.text("Agente Account", h, v);
                                doc.text("Quitação", h + 25, v);
                                doc.text("Contrato Camb.", h + 40, v);
                                doc.text("MBL", h + 61, v);
                                doc.text("HBL", h + 88, v);
                                doc.text("Processo", h + 114, v);
                                doc.text("Invoice/Act Nº", h + 129, v);
                                doc.text("Invoice Date", h + 173, v);
                                doc.text("Tx.", h + 191, v);
                                doc.text("Valor Inv.", h + 203, v);
                                doc.text("BRL", h + 220, v);
                                doc.text("Tx. Receb.", h + 235, v);
                                doc.text("Dt. Receb.", h + 250, v);
                                doc.text("Cnee", h + 265, v);
                                doc.setFontSize(7);
                                doc.setFontStyle("normal    ");
                                v = v + 5;
                                doc.text(dado[i]["NM_AGENTE"].substring(0, 15), h, v);
                                doc.text(dado[i]["DT_QUITACAO"], h + 25, v);
                                doc.text(dado[i]["NR_CONTRATO"], h + 40, v);
                                doc.text(dado[i]["NR_MBL"], h + 61, v);
                                doc.text(dado[i]["NR_HBL"], h + 88, v);
                                doc.text(dado[i]["NR_PROCESSO"], h + 114, v);
                                doc.text(dado[i]["NR_INVOICE"], h + 129, v);
                                doc.text(dado[i]["DT_INVOICE"], h + 173, v);
                                doc.text(dado[i]["TX_INVOICE"], h + 191, v);
                                doc.text(dado[i]["VLINVOICE"] + ' ' + dado[i]["SIGLA"], h + 203, v);
                                doc.text(dado[i]["VLINVOICEBRL"].toFixed(2), h + 220, v);
                                doc.text(dado[i]["TX_RECEBIMENTO"], h + 235, v);
                                doc.text(dado[i]["DT_RECEBIMENTO"], h + 250, v);
                                doc.text(dado[i]["NM_IMPORTADOR"].toString().substr(0, 15), h + 265, v);
                                total = total + parseFloat(dado[i]["VLINVOICE"].toFixed(2));
                                totalbrl = totalbrl + parseFloat(dado[i]["VLINVOICEBRL"].toFixed(2));
                                if (dado[i]["SIGLA"].toString() != moeda || i == dado.length - 1) {
                                    v = v + 5;
                                    doc.setFontStyle("bold");
                                    doc.setFontSize(10);
                                    doc.line(h - 1, v - 2, h + 290, v - 2);
                                    doc.text("Total Valor Invoice", h + 220, v + 3);
                                    doc.text("Total BRL", h + 220, v + 8);
                                    doc.setFontSize(9);
                                    doc.setFontStyle("normal");
                                    doc.text(total.toLocaleString('en-us', { style: 'currency', currency: 'USD' }), h + 260, v + 3);
                                    doc.text(totalbrl.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), h + 260, v + 8);
                                    doc.line(h - 1, v + 11, h + 290, v + 11);
                                }

                            } else {
                                v = v + 5;
                                doc.text(dado[i]["NM_AGENTE"].substring(0, 15), h, v);
                                doc.text(dado[i]["DT_QUITACAO"], h + 25, v);
                                doc.text(dado[i]["NR_CONTRATO"], h + 40, v);
                                doc.text(dado[i]["NR_MBL"], h + 61, v);
                                doc.text(dado[i]["NR_HBL"], h + 88, v);
                                doc.text(dado[i]["NR_PROCESSO"], h + 114, v);
                                doc.text(dado[i]["NR_INVOICE"], h + 129, v);
                                doc.text(dado[i]["DT_INVOICE"], h + 173, v);
                                doc.text(dado[i]["TX_INVOICE"], h + 191, v);
                                doc.text(dado[i]["VLINVOICE"] + ' ' + dado[i]["SIGLA"], h + 203, v);
                                doc.text(dado[i]["VLINVOICEBRL"].toFixed(2), h + 220, v);
                                doc.text(dado[i]["TX_RECEBIMENTO"], h + 235, v);
                                doc.text(dado[i]["DT_RECEBIMENTO"], h + 250, v);
                                doc.text(dado[i]["NM_IMPORTADOR"].toString().substr(0, 15), h + 265, v);
                                total = total + parseFloat(dado[i]["VLINVOICE"].toFixed(2));
                                totalbrl = totalbrl + parseFloat(dado[i]["VLINVOICEBRL"].toFixed(2));
                                if (dado[i]["SIGLA"].toString() != moeda || i == dado.length - 1) {
                                    v = v + 5;
                                    doc.setFontStyle("bold");
                                    doc.setFontSize(10);
                                    doc.line(h - 1, v - 2, h + 290, v - 2);
                                    doc.text("Total Valor Invoice", h + 220, v + 3);
                                    doc.text("Total BRL", h + 220, v + 8);
                                    doc.setFontSize(9);
                                    doc.setFontStyle("normal");
                                    doc.text(total.toLocaleString('en-us', { style: 'currency', currency: 'USD' }), h + 260, v + 3);
                                    doc.text(totalbrl.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), h + 260, v + 8);
                                    doc.line(h - 1, v + 11, h + 290, v + 11);
                                }
                            }                            
                        }
                        doc.output("dataurlnewwindow");
                    }
                    else {
                    }
                }
            })

        }

        function exportEstimativaCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdPremiacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdPremiacao td, #grdPremiacao th");

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
    </script>
</asp:Content>