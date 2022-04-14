<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioProcessos.aspx.cs" Inherits="ABAINFRA.Web.RelatorioProcessos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relatório Processos
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatório Processos s/ Saldo
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
                                        <button type="button" id="btnExportConferenciaProcesso" class="btn btn-primary" onclick="exportConferenciaProcessoCSV('Conferencia_Conta_Corrente.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacao" class="btn btn-primary" onclick="createPDF()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Inicial:</label>
                                                <input id="txtDtInicialConferenciaProcesso" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Final:</label>
                                                <input id="txtDtFinalConferenciaProcesso" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Filtro</label>
                                                <select id="ddlFilterConferenciaProcesso" class="form-control">
                                                    <option value="">Selecione</option>
                                                    <option value="2">Processo</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">*</label>
                                                <input id="txtConferenciaProcesso" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultarConferenciaProcesso" onclick="listarProcessoSaldo()" class="btn btn-primary">Consultar</button>
                                        </div>
                                        <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                            <div>
                                                <asp:CheckBox ID="chkFCL" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;FCL"></asp:CheckBox>
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkLCL" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;LCL"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                </div> 
                                <div id="tableConferenciaProcesso" class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdConferenciaProcesso" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">LCL/FCL</th>
                                                <th class="text-center" scope="col">DT_DEV_VAZIO_SALDO</th>
                                                <th class="text-center" scope="col">DT_DEMURRAGE_SALDO</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdConferenciaProcessoBody">

                                        </tbody>
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
        function listarProcessoSaldo() {
            var dtInicial = document.getElementById("txtDtInicialConferenciaProcesso").value;
            var dtFinal = document.getElementById("txtDtFinalConferenciaProcesso").value;
            var nota = document.getElementById("txtConferenciaProcesso").value;
            var filter = document.getElementById("ddlFilterConferenciaProcesso").value;
            var checkboxFCL = document.getElementById("MainContent_chkFCL");
            var checkboxFCLvalue;
            var checkboxLCL = document.getElementById("MainContent_chkLCL");
            var checkboxLCLvalue;
            if (checkboxFCL.checked) {
                checkboxFCLvalue = "1";
            }
            else {
                checkboxFCLvalue = "0";
            }
            if (checkboxLCL.checked) {
                checkboxLCLvalue = "1";
            }
            else {
                checkboxLCLvalue = "0";
            }
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarProcessoFinalizado",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", text: "' + nota + '", lcl: "' + checkboxLCLvalue+'", fcl: "'+checkboxFCLvalue+'", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdConferenciaProcessoBody").empty();
                        $("#grdConferenciaProcessoBody").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdConferenciaProcessoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdConferenciaProcessoBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_TIPO_ESTUFAGEM"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_DEMURRAGE_SALDO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_DEV_VAZIO_SALDO"] + "</td></tr> ");
                            }
                        }
                        else {
                            $("#grdConferenciaProcessoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='10' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }

        function exportConferenciaProcessoCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdConferenciaProcesso tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdConferenciaProcesso td, #grdConferenciaProcesso th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVConferenciaProcesso(csv.join("\n"), filename);
        }

        function exportTableToCSVConferenciaProcesso(csv, filename) {
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
            var sTable = document.getElementById('tableConferenciaProcesso').innerHTML;

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

    </script>
</asp:Content>

