<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioDebitCredit.aspx.cs" Inherits="ABAINFRA.Web.RelatorioDebitCredit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relatório Debit / Credit - Emissão FCA
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatório Debit / Credit
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
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('InvoiceFCADebitCredit.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Vencimento Inicial:</label>
                                            <input id="txtDtInicialVencimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Vencimento Final:</label>
                                            <input id="txtDtFinalVencimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div> 
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterPagamentoRecebimento" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">AGENTE</option>
                                                <option value="2">NR BL</option>
                                                <option value="3">PROCESSO</option>
                                                <option value="4">INVOICE</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtPagamentoRecebimento" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarPagamentoRecebimento" onclick="InvoicesFCA()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR INVOICE</th>
                                                <th class="text-center" scope="col">TIPO INVOICE</th>
                                                <th class="text-center" scope="col">TIPO EMISSOR</th>
                                                <th class="text-center" scope="col">DATA INVOICE</th>
                                                <th class="text-center" scope="col">DATA VENCIMENTO</th>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">NR BL</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">TIPO FATURA</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdPagamentoRecebimentoBody">

                                        </tbody>
                                        <tfoot id="grdPagamentoRecebimentoFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
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
        function InvoicesFCA() {
            var dtInicial = document.getElementById("txtDtInicialVencimento").value;
            var dtFinal = document.getElementById("txtDtFinalVencimento").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            if (dtInicial == "" && dtFinal == "") {
                dtInicial = "1900-01-01";
                dtFinal = "2900-01-01";
            }
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/FCADebitCreditInvoice",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", filter: "' + filter + '", text: "' + nota + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdPagamentoRecebimentoBody").empty();
                        $("#grdPagamentoRecebimentoBody").append("<tr><td colspan='11'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdPagamentoRecebimentoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdPagamentoRecebimentoBody").append("<tr>" +
                                    "<td class='text-center'> '" + dado[i]["NR_INVOICE"].toString() + "'</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_ACCOUNT_TIPO_INVOICE"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["NM_ACCOUNT_TIPO_EMISSOR"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["DT_INVOICE"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["DT_VENCIMENTO"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_BL"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_RAZAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_ACCOUNT_TIPO_FATURA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VL_TAXA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["SIGLA_MOEDA"] + "</td></tr >");                                
                            }
                        }
                        else {
                            $("#grdPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='11' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }


        function exportTableToCSVPagamentosRecebimentos(file, array) {
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
            var rows = document.querySelectorAll("#grdPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdPagamentoRecebimento td, #grdPagamentoRecebimento th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVPagamentosRecebimentos(filename, csv.join("\n"));
        }
    </script>
</asp:Content>
