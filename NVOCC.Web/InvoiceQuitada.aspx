<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceQuitada.aspx.cs" Inherits="ABAINFRA.Web.InvoiceQuitada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Invoices
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Invoices Quitadas
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
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Invoice.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintPagamentoRecebimento" class="btn btn-primary" onclick="printPagamentosRecebimentos()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Quitação Inicial:</label>
                                            <input id="txtDtInicialVencimentoInvoice" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Quitação Final:</label>
                                            <input id="txtDtFinalVencimentoInvoice" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterInvoice" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Agente</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtInvoice" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarInvoice" onclick="listarInvoices()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdInvoice" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">DATA QUITAÇÃO</th>
                                                <th class="text-center" scope="col">Nº CONTRATO</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">HBL</th>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">INVOICE</th>
                                                <th class="text-center" scope="col">DATA INVOICE</th>
                                                <th class="text-center" scope="col">TAXA INVOICE</th>
                                                <th class="text-center" scope="col">VALOR INVOICE</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                                <th class="text-center" scope="col">VALOR R$</th>
                                                <th class="text-center" scope="col">TAXA RECEBIMENTO</th>
                                                <th class="text-center" scope="col">DATA RECEBIMENTO</th>
                                                <th class="text-center" scope="col">CNEE</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdInvoiceBody">

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="row topMarg">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label" style="font-weight: normal">TOTAL:</label><label class="control-label" style="font-weight: bold" id="totalInvoice"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label" style="font-weight: normal">TOTAL BRL:</label><label class="control-label" style="font-weight: bold" id="totalInvoiceBrl"></label>
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
        var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();

        var arrayInvoice = [];

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

        function listarInvoices() {
            var dtInicial = document.getElementById("txtDtInicialVencimentoInvoice").value;
            var dtFinal = document.getElementById("txtDtFinalVencimentoInvoice").value;
            var nota = document.getElementById("txtInvoice").value;
            var filter = document.getElementById("ddlFilterInvoice").value;
            var totalInvoice = 0;
            var totalInvoiceBrl = 0;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarInvoicesQuitadas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdInvoiceBody").empty();
                        $("#grdInvoiceBody").append("<tr><td colspan='15'><div class='loader'></div></td></tr>");
                        $("#totalInvoice").empty();
                        $("#totalInvoiceBrl").empty();
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdInvoiceBody").empty();
                        $("#totalInvoice").empty();
                        $("#totalInvoiceBrl").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                arrayInvoice.push(dado[i]["ID_ACCOUNT_INVOICE"])
                                totalInvoice = totalInvoice + parseFloat(dado[i]["VLINVOICE"].toFixed(2));
                                totalInvoiceBrl = totalInvoiceBrl + parseFloat(dado[i]["VLINVOICEBRL"].toFixed(2));
                                $("#grdInvoiceBody").append("<tr><td class='text-center' style='max-width: 20ch;' title='" + dado[i]["NM_AGENTE"]+"'> " + dado[i]["NM_AGENTE"] + "</td><td class='text-center'>" + dado[i]["DT_QUITACAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_CONTRATO"] + "</td><td class='text-center'>" + dado[i]["NR_MBL"] + "</td><td class='text-center'>" + dado[i]["NR_HBL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NR_INVOICE"] + "</td><td class='text-center'>" + dado[i]["DT_INVOICE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TX_INVOICE"] + "</td><td class='text-center'>" + dado[i]["VLINVOICE"] + "</td><td class='text-center'>" + dado[i]["SIGLA"] + "</td><td class='text-center'>" + dado[i]["VLINVOICEBRL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TX_RECEBIMENTO"] + "</td><td class='text-center'>" + dado[i]["DT_RECEBIMENTO"] + "</td><td class='text-center' style='max-width: 20ch;' title='" + dado[i]["NM_IMPORTADOR"] +"'>" + dado[i]["NM_IMPORTADOR"] + "</td></tr>");
                            }
                            $("#totalInvoice").append(totalInvoice.toLocaleString('en-us', { style: 'currency', currency: 'USD' }));
                            $("#totalInvoiceBrl").append(totalInvoiceBrl.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                        }
                        else {
                            $("#grdInvoiceBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='15' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
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
                        doc.text("MBL", h + 63, v);
                        doc.text("HBL", h + 93, v);
                        doc.text("Processo", h + 116, v);
                        doc.text("Invoice/Act Nº", h + 135 , v);
                        doc.text("Invoice Date", h + 165, v);
                        doc.text("Tx.", h + 183, v);
                        doc.text("Valor Inv.", h + 195, v);
                        doc.text("BRL", h + 210, v);
                        doc.text("Tx. Receb.", h + 230, v);
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
                                doc.text("MBL", h + 63, v);
                                doc.text("HBL", h + 93, v);
                                doc.text("Processo", h + 116, v);
                                doc.text("Invoice/Act Nº", h + 135, v);
                                doc.text("Invoice Date", h + 165, v);
                                doc.text("Tx.", h + 183, v);
                                doc.text("Valor Inv.", h + 195, v);
                                doc.text("BRL", h + 210, v);
                                doc.text("Tx. Receb.", h + 230, v);
                                doc.text("Dt. Receb.", h + 250, v);
                                doc.text("Cnee", h + 265, v);
                                doc.setFontSize(7);
                                doc.setFontStyle("normal    ");
                                v = v + 5;
                                doc.text(dado[i]["NM_AGENTE"].substring(0, 15), h, v);
                                doc.text(dado[i]["DT_QUITACAO"], h + 25, v);
                                doc.text(dado[i]["NR_CONTRATO"], h + 40, v);
                                doc.text(dado[i]["NR_MBL"], h + 63, v);
                                doc.text(dado[i]["NR_HBL"], h + 93, v);
                                doc.text(dado[i]["NR_PROCESSO"], h + 116, v);
                                doc.text(dado[i]["NR_INVOICE"], h + 135, v);
                                doc.text(dado[i]["DT_INVOICE"], h + 165, v);
                                doc.text(dado[i]["TX_INVOICE"], h + 183, v);
                                doc.text(dado[i]["VLINVOICE"] + ' ' + dado[i]["SIGLA"], h + 195, v);
                                doc.text(dado[i]["VLINVOICEBRL"].toFixed(2), h + 210, v);
                                doc.text(dado[i]["TX_RECEBIMENTO"], h + 230, v);
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
                                doc.text(dado[i]["NR_MBL"], h + 63, v);
                                doc.text(dado[i]["NR_HBL"], h + 93, v);
                                doc.text(dado[i]["NR_PROCESSO"], h + 116, v);
                                doc.text(dado[i]["NR_INVOICE"], h + 135, v);
                                doc.text(dado[i]["DT_INVOICE"], h + 165, v);
                                doc.text(dado[i]["TX_INVOICE"], h + 183, v);
                                doc.text(dado[i]["VLINVOICE"] + ' ' + dado[i]["SIGLA"], h + 195, v);
                                doc.text(dado[i]["VLINVOICEBRL"].toFixed(2), h + 210, v);
                                doc.text(dado[i]["TX_RECEBIMENTO"], h + 230, v);
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