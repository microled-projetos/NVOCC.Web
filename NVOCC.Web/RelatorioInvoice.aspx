<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioInvoice.aspx.cs" Inherits="ABAINFRA.Web.RelatorioInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relatório Invoice
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatório Invoice - Aviso Embarque/Vencimento
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
                                            <label class="control-label">Data Vencimento Inicial:</label>
                                            <input id="txtDtInicialVencimentoInvoice" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Vencimento Final:</label>
                                            <input id="txtDtFinalVencimentoInvoice" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterInvoice" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Agente</option>
                                                <option value="2">Nº Processo</option>
                                                <option value="3">Nº BL</option>
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
                                                <th class="text-center" scope="col">Nº INVOICE</th>
                                                <th class="text-center" scope="col">TIPO</th>
                                                <th class="text-center" scope="col">EMISSOR</th>
                                                <th class="text-center" scope="col">DATA INVOICE</th>
                                                <th class="text-center" scope="col">DATA VENCIMENTO</th>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">Nº BL</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">CONFERIDO</th>
                                                <th class="text-center" scope="col">TIPO FATURA</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">DATA FECHAMENTO</th>
                                                <th class="text-center" scope="col">OBSERVAÇÕES</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdInvoiceBody">

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
            arrayInvoice = [];
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarInvoices",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdInvoiceBody").empty();
                        $("#grdInvoiceBody").append("<tr><td colspan='14'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdInvoiceBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                arrayInvoice.push(dado[i]["ID_ACCOUNT_INVOICE"])
                                $("#grdInvoiceBody").append("<tr><td class='text-center'> " + dado[i]["NR_INVOICE"] + "</td><td class='text-center'>" + dado[i]["TIPO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_ACCOUNT_TIPO_EMISSOR"] + "</td><td class='text-center'>" + dado[i]["DT_INVOICE"] + "</td><td class='text-center'>" + dado[i]["DT_VENCIMENTO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NR_BL"] + "</td><td class='text-center'>" + dado[i]["NM_RAZAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["CONFERIDO"] + "</td><td class='text-center'>" + dado[i]["NM_ACCOUNT_TIPO_FATURA"] + "</td><td class='text-center'>" + dado[i]["SIGLA_MOEDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VALOR"] + "</td><td class='text-center'>" + dado[i]["DT_FECHAMENTO"] + "</td><td class='text-center'>" + dado[i]["OBS"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdInvoiceBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
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
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/imprimirInvoice",
                data: JSON.stringify({ dataI: (dtInicial), dataF: (dtFinal), invoices: (arrayInvoice)}),
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
                        doc.text("AVISO DE EMBARQUE", 120, 13);
                        doc.setFontSize(8);
                        doc.text("INVOICES ENTRE " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 125, 18);
                        doc.addImage(imgData, 'png', 10, 5, 60, 15);
                        doc.setFontSize(7);
                        doc.setLineWidth(0.2);
                        doc.line(3, 24, 293, 24);
                        doc.line(3, 28, 293, 28);
                        doc.line(3, 24, 3, 28);
                        doc.line(49, 24, 49, 28);
                        doc.line(67, 24, 67, 28);
                        doc.line(102, 24, 102, 28);
                        doc.line(132, 24, 132, 28);
                        doc.line(159, 24, 159, 28);
                        doc.line(184, 24, 184, 28);
                        doc.line(199, 24, 199, 28);
                        doc.line(213, 24, 213, 28);
                        doc.line(242, 24, 242, 28);
                        doc.line(259, 24, 259, 28);
                        doc.line(277, 24, 277, 28);
                        doc.line(293, 24, 293, 28);

                        doc.text("INVOICE", 4, 27);
                        doc.text("PROCESSO", 50, 27);
                        doc.text("HBL", 68, 27);
                        doc.text("MBL", 103, 27);
                        doc.text("CLIENTE", 133, 27);
                        doc.text("AGENTE", 161, 27);
                        doc.text("ORIGEM", 186, 27);
                        doc.text("DESTINO", 201, 27);
                        doc.text("TRANSPORTADOR", 214, 27);
                        doc.text("EMBARQUE", 243, 27);
                        doc.text("P. CHEGADA", 261, 27);
                        doc.text("CHEGADA", 278, 27);
                        for (let i = 0; i < dado.length; i++) {
                            if (position >= pageHeight -10 ) {
                                doc.line(3, 28, 3, positionv);
                                doc.line(49, 28, 51, positionv);
                                doc.line(67, 28, 67, positionv);
                                doc.line(102, 28, 102, positionv);
                                doc.line(132, 28, 132, positionv);
                                doc.line(159, 28, 159, positionv);
                                doc.line(184, 28, 184, positionv);
                                doc.line(199, 28, 199, positionv);
                                doc.line(213, 28, 213, positionv);
                                doc.line(242, 28, 242, positionv);
                                doc.line(259, 28, 259, positionv);
                                doc.line(277, 28, 277, positionv);
                                doc.line(293, 28, 293, positionv);
                                doc.line(3, 28, 3, positionv);
                                doc.addPage();
                                doc.setFontStyle("bold");
                                doc.setFontSize(18);
                                doc.text("AVISO DE EMBARQUE", 120, 13);
                                doc.setFontSize(8);
                                doc.text("INVOICES ENTRE " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 125, 18);
                                doc.addImage(imgData, 'png', 10, 5, 60, 15);
                                doc.setFontSize(7);
                                doc.setLineWidth(0.2);
                                doc.line(3, 24, 293, 24);
                                doc.line(3, 28, 293, 28);
                                doc.line(3, 24, 3, 28);
                                doc.line(49, 24, 49, 28);
                                doc.line(67, 24, 67, 28);
                                doc.line(102, 24, 102, 28);
                                doc.line(132, 24, 132, 28);
                                doc.line(159, 24, 159, 28);
                                doc.line(184, 24, 184, 28);
                                doc.line(199, 24, 199, 28);
                                doc.line(213, 24, 213, 28);
                                doc.line(242, 24, 242, 28);
                                doc.line(259, 24, 259, 28);
                                doc.line(277, 24, 277, 28);
                                doc.line(293, 24, 293, 28);


                                doc.text("INVOICE", 4, 27);
                                doc.text("PROCESSO", 50, 27);
                                doc.text("HBL", 70, 27);
                                doc.text("MBL", 105, 27);
                                doc.text("CLIENTE", 135, 27);
                                doc.text("AGENTE", 161, 27);
                                doc.text("ORIGEM", 186, 27);
                                doc.text("DESTINO", 201, 27);
                                doc.text("TRANSPORTADOR", 214, 27);
                                doc.text("EMBARQUE", 243, 27);
                                doc.text("P. CHEGADA", 261, 27);
                                doc.text("CHEGADA", 279, 27);
                                position = 27;
                                positionv = 28;
                            } else {
                                position = position + 5;
                                positionv = positionv + 5;
                                doc.setFontStyle("normal");
                                doc.line(3, positionv, 289, positionv);
                                doc.text(dado[i]["NR_INVOICE"], 4, position);
                                doc.text(dado[i]["NR_PROCESSO"], 50, position);
                                doc.text(dado[i]["HBL"], 68, position);
                                doc.text(dado[i]["MBL"].substring(0, 15), 103, position);
                                doc.text(dado[i]["CLIENTE"].substring(0, 12), 133, position);
                                doc.text(dado[i]["AGENTE"].substring(0, 12), 161, position);
                                doc.text(dado[i]["ORIGEM"].substring(0,12), 186, position);
                                doc.text(dado[i]["DESTINO"], 201, position);
                                doc.text(dado[i]["TRANSPORTADOR"].substring(0, 15), 214, position);
                                doc.text(dado[i]["DT_EMBARQUE"], 243, position);
                                doc.text(dado[i]["DT_PREVISAO_CHEGADA"].substring(0, 15), 261, position);
                                doc.text(dado[i]["DT_CHEGADA"], 279, position);
                            }
                        }
                        doc.text("Gerado: " + datetime, 260, 205);
                        doc.line(3, positionv, 293, positionv);
                        doc.line(3, 28, 3, positionv);
                        doc.line(49, 28, 49, positionv);
                        doc.line(67, 28, 67, positionv);
                        doc.line(102, 28, 102, positionv);
                        doc.line(132, 28, 132, positionv);
                        doc.line(159, 28, 159, positionv);
                        doc.line(184, 28, 184, positionv);
                        doc.line(199, 28, 199, positionv);
                        doc.line(213, 28, 213, positionv);
                        doc.line(242, 28, 242, positionv);
                        doc.line(259, 28, 259, positionv);
                        doc.line(277, 28, 277, positionv);
                        doc.line(293, 28, 293, positionv);
                        doc.line(3, 28, 3, positionv);
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