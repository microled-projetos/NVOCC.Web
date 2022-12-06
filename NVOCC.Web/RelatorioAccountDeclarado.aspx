<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioAccountDeclarado.aspx.cs" Inherits="ABAINFRA.Web.RelatorioAccountDeclarado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relatório Account Declarado
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatórios Taxas por Processo
                            </a>
                        </li>
                        <li id="tabprocessoEstimativaGrid">
                            <a href="#processoEstimativaGrid" id="linkprocessoEstimativaGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatório Processos por Agente
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
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('TaxasPorProcesso.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque Inicial:</label>
                                            <input id="txtDtInicialEmbarque" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque Final:</label>
                                            <input id="txtDtFinalEmbarque" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div> 
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterPagamentoRecebimento" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Agente</option>
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
                                        <button type="button" id="btnConsultarPagamentoRecebimento" onclick="TaxasPorProcessos()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">DATA EMBARQUE</th>
                                                <th class="text-center" scope="col">DATA CHEGADA</th>
                                                <th class="text-center" scope="col">NR MASTER</th>
                                                <th class="text-center" scope="col">NR HOUSE</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">TIPO</th>
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

                        <div class="tab-pane fade" id="processoEstimativaGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px; flex-direction: column; align-items:center ">
                                    <div>
                                        <button type="button" id="btnExportEstimativaPagamentoRecebimento" class="btn btn-primary" onclick="exportEstimativaCSV('ProcessoPorAgente.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque Inicial:</label>
                                            <input id="txtDtInicialEstimativaPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque Final:</label>
                                            <input id="txtDtFinalEstimativaPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterEstimativaPagamentoRecebimento" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Agente</option>
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
                                        <button type="button" id="btnConsultarEstimativaPagamentoRecebimento" onclick="ProcessosPorAgente()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdEstimativaPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">DATA EMBARQUE</th>
                                                <th class="text-center" scope="col">DATA CHEGADA</th>
                                                <th class="text-center" scope="col">NR MASTER</th>
                                                <th class="text-center" scope="col">NR HOUSE</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">TIPO</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdEstimativaPagamentoRecebimentoBody">

                                        </tbody>
                                        <tfoot id="grdEstimativaPagamentoRecebimentoFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
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
       function printRelatorioTaxasPorProcessos() {
            var dtInicial = document.getElementById("txtDtInicialPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalPagamentoRecebimento").value;
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

        function ProcessosPorAgente() {
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            if (dtInicial == "" && dtFinal == "") {
                dtInicial = "1900-01-01";
                dtFinal = "2900-01-01";
            }
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/ProcessosPorAgente",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdEstimativaPagamentoRecebimentoBody").empty();
                        $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdEstimativaPagamentoRecebimentoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdEstimativaPagamentoRecebimentoBody").append("<tr>" +
                                    "<td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_RAZAO"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["DT_EMBARQUE"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["DT_CHEGADA"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["NR_MASTER"] + "</td>" +
                                    "<td class='text-center'> " + dado[i]["NR_HOUSE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["SIGLA_MOEDA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["VALOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TIPO_MOVIMENTO"] + "</td></tr>");                                
                            }
                        }
                        else {
                            $("#grdEstimativaPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }

        function TaxasPorProcessos() {
            var dtInicial = document.getElementById("txtDtInicialEmbarque").value;
            var dtFinal = document.getElementById("txtDtFinalEmbarque").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            if (dtInicial == "" && dtFinal == "") {
                dtInicial = "1900-01-01";
                dtFinal = "2900-01-01";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/TaxaProcesso",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdPagamentoRecebimentoBody").empty();
                    $("#grdPagamentoRecebimentoBody").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    var liqrec = 0;
                    var liqpag = 0;
                    $("#grdPagamentoRecebimentoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdPagamentoRecebimentoBody").append("<tr>" +
                                "<td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_RAZAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EMBARQUE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_HOUSE"] + "</td>" +
                                "<td class='text-center'> " + dado[i]["NM_ITEM_DESPESA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["SIGLA_MOEDA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_TAXA_CALCULADO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TIPO_MOVIMENTO"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
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
            var rows = document.querySelectorAll("#grdPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdPagamentoRecebimento td, #grdPagamentoRecebimento th");

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
