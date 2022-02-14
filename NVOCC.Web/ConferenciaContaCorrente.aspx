<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConferenciaContaCorrente.aspx.cs" Inherits="ABAINFRA.Web.ConferenciaContaCorrente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Conferência Conta Corrente
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Conferência Conta Corrente
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
                                        <button type="button" id="btnExportConferenciaContaCorrente" class="btn btn-primary" onclick="exportConferenciaContaCorrenteCSV('Conferencia_Conta_Corrente.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacao" class="btn btn-primary" onclick="createPDF()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Inicial:</label>
                                                <input id="txtDtInicialConferenciaContaCorrente" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Final:</label>
                                                <input id="txtDtFinalConferenciaContaCorrente" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Filtro</label>
                                                <select id="ddlFilterConferenciaContaCorrente" class="form-control">
                                                    <option value="">Selecione</option>
                                                    <option value="1">Transportador/Cliente</option>
                                                    <option value="2">Processo</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">*</label>
                                                <input id="txtConferenciaContaCorrente" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultarConferenciaContaCorrente" onclick="ConferenciaContaCorrente()" class="btn btn-primary">Consultar</button>
                                        </div>
                                    </div>
                                </div> 
                                <div id="tableConferenciaContaCorrente" class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdConferenciaContaCorrente" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">CLIENTE</th>
                                                <th class="text-center" scope="col">TRANSPORTADOR</th>
                                                <th class="text-center" scope="col">VALOR COMPRA</th>
                                                <th class="text-center" scope="col">VALOR VENDA</th>
                                                <th class="text-center" scope="col">PROFIT</th>
                                                <th class="text-center" scope="col">LIQUIDAÇÃO VENDA</th>
                                                <th class="text-center" scope="col">LIQUIDAÇÃO COMPRA</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdConferenciaContaCorrenteBody">

                                        </tbody>
                                         <tfoot id="grdConferenciaContaCorrenteFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
                                        </tfoot>
                                    </table>
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
        function ConferenciaContaCorrente() {
            var dtInicial = document.getElementById("txtDtInicialConferenciaContaCorrente").value;
            var dtFinal = document.getElementById("txtDtFinalConferenciaContaCorrente").value;
            var nota = document.getElementById("txtConferenciaContaCorrente").value;
            var filter = document.getElementById("ddlFilterConferenciaContaCorrente").value;
            var totalC = 0;
            var totalV = 0;
            var profit = 0;
            var liqV;
            var liqC;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarConferenciaContaCorrente",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdConferenciaContaCorrenteBody").empty();
                        $("#grdConferenciaContaCorrenteBody").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdConferenciaContaCorrenteBody").empty();
                        $("#grdConferenciaContaCorrenteFooter").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                if (dado[i]["DT_LIQUIDACAO"] == null) {
                                    liqV = "";
                                } else {
                                    liqV = dado[i]["DT_LIQUIDACAO"];
                                }

                                if (dado[i]["DT_LIQUIDACAO_COMPRA"] == null) {
                                    liqC = "";
                                } else {
                                    liqC = dado[i]["DT_LIQUIDACAO_COMPRA"];
                                }

                                $("#grdConferenciaContaCorrenteBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["CLIENTE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["COMPRA"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VENDA"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>" +
                                    "<td class='text-center'>" + dado[i]["PROFIT"].toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>" +
                                    "<td class='text-center'>" + liqV + "</td>" +
                                    "<td class='text-center'>" + liqC + "</td></tr > ");
                                totalC = totalC + parseFloat(dado[i]["COMPRA"]);
                                totalV = totalV + parseFloat(dado[i]["VENDA"]);
                                profit = profit + parseFloat(dado[i]["PROFIT"]);
                            }
                            $("#grdConferenciaContaCorrenteFooter").append("<tr><th></th><th></th><th style='text-align:end'>Total:</th><th style='text-align:center'>" + totalC.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</th><th style='text-align:center'>" + totalV.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</th><th style='text-align:center'>" + profit.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</th><th></th><th></th></tr>");
                        }
                        else {
                            $("#grdConferenciaContaCorrenteBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='10' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                            $("#grdConferenciaContaCorrenteFooter").empty();
                        }
                    }
                })
            } else {

            }
        }

        function exportConferenciaContaCorrenteCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdConferenciaContaCorrente tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdConferenciaContaCorrente td, #grdConferenciaContaCorrente th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVConferenciaContaCorrente(csv.join("\n"), filename);
        }

        function exportTableToCSVConferenciaContaCorrente(csv, filename) {
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
            var sTable = document.getElementById('tableConferenciaContaCorrente').innerHTML;

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

        function PrintConferenciaContaCorrente() {
            $("#modalConferenciaContaCorrente").modal('show');
            var dtInicial = document.getElementById("txtDtInicialConferenciaContaCorrente").value;
            var dtFinal = document.getElementById("txtDtFinalConferenciaContaCorrente").value;
            var diaI = document.getElementById("txtDtInicialConferenciaContaCorrente").value.substring(8, 10);
            var mesI = document.getElementById("txtDtInicialConferenciaContaCorrente").value.substring(5, 7);
            var anoI = document.getElementById("txtDtInicialConferenciaContaCorrente").value.substring(0, 4);
            var diaF = document.getElementById("txtDtFinalConferenciaContaCorrente").value.substring(8, 10);
            var mesF = document.getElementById("txtDtFinalConferenciaContaCorrente").value.substring(5, 7);
            var anoF = document.getElementById("txtDtFinalConferenciaContaCorrente").value.substring(0, 4);
            var nota = document.getElementById("txtConferenciaContaCorrente").value;
            var filter = document.getElementById("ddlFilterConferenciaContaCorrente").value;
            var position = 20;
            var positionLineF = 21;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarConferenciaContaCorrente",
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
