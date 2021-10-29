<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Premiacao.aspx.cs" Inherits="ABAINFRA.Web.Premiacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Premiação
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Premiação
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Invoice.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintPagamentoRecebimento" class="btn btn-primary" onclick="printPagamentosRecebimentos()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Competência:</label>
                                            <input id="txtDtCompentencia" class="form-control" type="text" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Quinzena</label>
                                            <input id="txtQuinzena" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarPremiacao" onclick="imprimirPremiacao()" class="btn btn-primary">Consultar</button>
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
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>

    <script>

        //PagamentosRecebimentos

        function imprimirPremiacao() {
            var dtInicial = document.getElementById("txtDtCompentencia").value;
            var nota = document.getElementById("txtQuinzena").value;
            var posih = 3;
            var posiv = 25;
            var pcindicador = [];
            var idpc = "";
            var totalpremiacao = 0;
            var totalpc = 0;
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
                            doc.text("COMPETÊNCIA - " + dado[0]["DT_COMPETENCIA"].substr(0, 2) + "/" + dado[0]["DT_COMPETENCIA"].substr(2, dado[0]["DT_COMPETENCIA"].length) +" - QUINZENA -" + dado[0]["NR_QUINZENA"], 120, 12);
                            for (let x = 0; x < dado.length; x++) {
                                if (pcindicador.indexOf(dado[x]["IDAGENTE"]) == -1) {
                                    pcindicador.push(dado[x]["IDAGENTE"]);
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
                                    if (idpc == dado[z]["IDAGENTE"]) {
                                        doc.setFontSize(8);
                                        doc.setFontStyle("normal");
                                        posiv = posiv + 5;
                                        doc.text(dado[z]["AGENTE"].substr(0, 15), posih, posiv);
                                        doc.text(dado[z]["MBL"], posih + 30, posiv);
                                        doc.text(dado[z]["HBL"], posih + 65, posiv);
                                        doc.text(dado[z]["CNEE"].substr(0, 15), posih + 95, posiv);
                                        doc.text(dado[z]["ESTUFAGEM"], posih + 130, posiv);
                                        doc.text(dado[z]["VL_COMPRA"].toFixed(2), posih + 153, posiv);
                                        doc.text(dado[z]["MOEDA_COMPRA"], posih + 180, posiv);
                                        doc.text(dado[z]["VL_CAMBIO"].toFixed(5), posih + 200, posiv);
                                        doc.text(dado[z]["VL_PREMIACAO"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }), posih + 230, posiv);
                                        doc.text(dado[z]["MOEDA_PREMIACAO"], posih + 255, posiv);
                                        doc.text(dado[z]["PC_RATEIO"].toString() + '%', posih + 275, posiv);
                                        totalpremiacao = totalpremiacao + parseFloat(dado[z]["VL_PREMIACAO"].toFixed(2));
                                        totalpc = totalpc + parseFloat(dado[z]["PC_RATEIO"].toFixed());
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


        function EstimativaPagamentosRecebimentos() {
            $("#modalEstimativaPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarContasAReceberAPagar",
                    data: '{filterby: "' + ddlDataFilter.value +'", dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
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
                                $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td class='text-center'> " + dado[i]["DATA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TP_SERVICO"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td><td class='text-center' style='max-width: 15ch;' title='" + dado[i]["NM_CLIENTE_REC"] + "'>" + dado[i]["NM_CLIENTE_REC"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_REC"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_REC"] + "</td>" +
                                    "<td class='text-center' style='max-width: 15ch;' title='" + dado[i]["NM_FORNECEDOR_PAG"] + "'>" + dado[i]["NM_FORNECEDOR_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_PAG"] + "</td>" +
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
    </script>
</asp:Content>