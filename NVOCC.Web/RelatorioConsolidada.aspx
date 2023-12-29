<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioConsolidada.aspx.cs" Inherits="ABAINFRA.Web.RelatorioConsolidada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relatório Consolidada
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatório Consolidada
                            </a>
                        </li>
                        <li id="tabprocessoAnaliticoGrid">
                            <a href="#processoAnaliticoGrid" id="linkprocessoAnaliticoGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relatório Analítico
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
                                        <button type="button" id="btnExportConferenciaProcesso" class="btn btn-primary" onclick="exportRelatorioConsolidadaCSV('Relatorio_Consolidada.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacao" class="btn btn-primary" onclick="createPDF()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Previsão de Chegada Inicial:</label>
                                            <input id="txtDtInicialConferenciaProcesso" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Previsão de Chegada Final:</label>
                                            <input id="txtDtFinalConferenciaProcesso" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque</label>
                                            <input id="txtDtEmbarqueConferenciaProcesso" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Chegada</label>
                                            <input id="txtDtChegadaConferenciaProcesso" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarConferenciaProcesso" onclick="listarRelatorioConsolidada()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div>
                            </div> 
                            <div id="tableConferenciaProcesso" class="table-responsive fixedDoubleHead topMarg">
                                <table id="grdConferenciaProcesso" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">NAVIO</th>
                                            <th class="text-center" scope="col">PORTO DE ORIGEM</th>
                                            <th class="text-center" scope="col">DATA EMBARQUE</th>
                                            <th class="text-center" scope="col">PREVISAO CHEGADA</th>
                                            <th class="text-center" scope="col">CHEGADA</th>
                                            <th class="text-center" scope="col">Nº CONTAINER</th>
                                            <th class="text-center" scope="col">TIPO CONTAINER</th>
                                            <th class="text-center" scope="col">TIPO ESTUFAGEM</th>
                                            <th class="text-center" scope="col">TIPO CARGA</th>
                                            <th class="text-center" scope="col">QUANTIDADE BL</th>
                                            <th class="text-center" scope="col">PESO</th>
                                            <th class="text-center" scope="col">METRAGEM</th>
                                            <th class="text-center" scope="col">CARGA IMO</th>
                                            <th class="text-center" scope="col">LTL</th>
                                            <th class="text-center" scope="col">DTA HUB</th>
                                            <th class="text-center" scope="col">FREE HAND</th>
                                        </tr>
                                    </thead>
                                    <tbody id="grdConferenciaProcessoBody">

                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="processoAnaliticoGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportConferenciaProcessoAnalitco" class="btn btn-primary" onclick="exportRelatorioAnaliticoCSV('Relatorio_Analitico.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacaoAnalitco" class="btn btn-primary" onclick="createPDF()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Previsão de Chegada Inicial:</label>
                                                <input id="txtDtInicialConferenciaProcessoAnalitico" class="form-control" type="date" required="required"/>
                                            </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Previsão de Chegada Final:</label>
                                            <input id="txtDtFinalConferenciaProcessoAnalitico" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque</label>
                                            <input id="txtDtEmbarqueAnaliticoConferenciaProcesso" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Chegada</label>
                                            <input id="txtDtChegadaAnaliticoConferenciaProcesso" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                        <div style="padding: 0 10px">
                                            <label class="control-label text-center">LTL</label>
                                            <div style="display: flex">
                                                <asp:CheckBox ID="chkLTLS" runat="server" CssClass="form-control noborder" Text="&nbsp;Sim"></asp:CheckBox>
                                                <asp:CheckBox ID="chkLTLN" runat="server" CssClass="form-control noborder functionFaturaBar" Text="&nbsp;Não"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div style="padding: 0 10px">
                                            <label class="control-label text-center">DTA HUB</label>
                                            <div style="display: flex">
                                                <asp:CheckBox ID="chkDTAHUBS" runat="server" CssClass="form-control noborder" Text="&nbsp;Sim"></asp:CheckBox>
                                                <asp:CheckBox ID="chkDTAHUBN" runat="server" CssClass="form-control noborder functionFaturaBar" Text="&nbsp;Não"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div style="padding: 0 10px">
                                            <label class="control-label text-center">TRANSP. DEDICADO</label>
                                            <div style="display: flex">
                                                <asp:CheckBox ID="chkTranspDedicS" runat="server" CssClass="form-control noborder" Text="&nbsp;Sim"></asp:CheckBox>
                                                <asp:CheckBox ID="chkTranspDedicN" runat="server" CssClass="form-control noborder" Text="&nbsp;Não"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>                                   
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarConferenciaProcessoAnalitico" onclick="listarRelatorioConsolidadaAnalitico()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div>
                            </div> 
                            <div id="tableConferenciaProcessoAnalitico" class="table-responsive fixedDoubleHead topMarg">
                                <table id="grdConferenciaProcessoAnalitico" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">NAVIO</th>
                                            <th class="text-center" scope="col">PORTO DE ORIGEM</th>
                                            <th class="text-center" scope="col">DATA EMBARQUE</th>
                                            <th class="text-center" scope="col">PREVISAO CHEGADA</th>
                                            <th class="text-center" scope="col">CHEGADA</th>
                                            <th class="text-center" scope="col">Nº PROCESSO</th>
                                            <th class="text-center" scope="col">TIPO CARGA</th>
                                            <th class="text-center" scope="col">TIPO ESTUFAGEM</th>
                                            <th class="text-center" scope="col">METRAGEM</th>
                                            <th class="text-center" scope="col">PESO</th>
                                            <th class="text-center" scope="col">FREE HAND</th>
                                            <th class="text-center" scope="col">LTL</th>
                                            <th class="text-center" scope="col">DTA HUB</th>
                                            <th class="text-center" scope="col">TRANSP. DEDICADO</th>
                                            <th class="text-center" scope="col">TRANSPORTADORA</th>
                                        </tr>
                                    </thead>
                                    <tbody id="grdConferenciaProcessoAnaliticoBody">

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
        function listarRelatorioConsolidada() {
            var result = "";
            var dtInicial = document.getElementById("txtDtInicialConferenciaProcesso").value;
            var dtFinal = document.getElementById("txtDtFinalConferenciaProcesso").value;
            var dtChegada = document.getElementById("txtDtChegadaConferenciaProcesso").value;
            var dtEmbarque = document.getElementById("txtDtEmbarqueConferenciaProcesso").value;
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarRelatorioConsolidada",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", dataC: "' + dtChegada + '", dataE: "' + dtEmbarque+'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdConferenciaProcessoBody").empty();
                        $("#grdConferenciaProcessoBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdConferenciaProcessoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                result += "<tr style='word-break: break-word'>";
                                result += "<td class='text-center'> " + dado[i]["NM_NAVIO"] + "</td>";
                                result += "<td class='text-center'> " + dado[i]["PORTO_ORIGEM"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["EMBARQUE"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["PREVISAO_CHEGADA"] + "</td>";
                                result += "<td class='text-center'> " + dado[i]["CHEGADA"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["TIPO_CNTR"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["NM_TIPO_ESTUFAGEM"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["NM_TIPO_CARGA"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["BL"] + "</td>";
                                result += "<td class='text-center'> " + dado[i]["PESO"].toFixed(3) + "</td>";
                                result += "<td class='text-center'> " + dado[i]["METRAGEM"].toFixed(3) + "</td>";
                                result += "<td class='text-center'> " + dado[i]["IMO"] + "</td>";
                                result += "<td class='text-center'> " + dado[i]["LTL"] + "</td>";
                                result += "<td class='text-center'> " + dado[i]["DTA_HUB"] + "</td>";
                                result += "<td class='text-center'> " + dado[i]["FREEHAND"] + "</td>";
                                result += "</tr>";
                            }
                            $("#grdConferenciaProcessoBody").append(result);
                        }
                        else {
                            $("#grdConferenciaProcessoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='16' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }

        function listarRelatorioConsolidadaAnalitico() {
            var result = "";
            var dtInicial = document.getElementById("txtDtInicialConferenciaProcessoAnalitico").value;
            var dtFinal = document.getElementById("txtDtFinalConferenciaProcessoAnalitico").value;
            var dtChegada = document.getElementById("txtDtChegadaAnaliticoConferenciaProcesso").value;
            var dtEmbarque = document.getElementById("txtDtEmbarqueAnaliticoConferenciaProcesso").value;
            var checkLTL = document.getElementById("MainContent_chkLTLS").checked ? 1 : 0;
            var checkDTAHUB = document.getElementById("MainContent_chkDTAHUBS").checked ? 1 : 0;
            var checkTranspDedic = document.getElementById("MainContent_chkTranspDedicS").checked ? 1 : 0;
            var checkLTLN = document.getElementById("MainContent_chkLTLN").checked ? 1 : 0;
            var checkDTAHUBN = document.getElementById("MainContent_chkDTAHUBN").checked ? 1 : 0;
            var checkTranspDedicN = document.getElementById("MainContent_chkTranspDedicN").checked ? 1 : 0;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarRelatorioConsolidadaAnalitico",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", dataC: "' + dtChegada + '", dataE: "' + dtEmbarque + '", ltl: ' + checkLTL + ', dtahub: ' + checkDTAHUB + ', transp: ' + checkTranspDedic + ', ltln: ' + checkLTLN + ', dtahubn: ' + checkDTAHUBN + ', transpn: ' + checkTranspDedicN +'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdConferenciaProcessoAnaliticoBody").empty();
                    $("#grdConferenciaProcessoAnaliticoBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdConferenciaProcessoAnaliticoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            result += "<tr style='word-break: break-word'>";
                            result += "<td class='text-center'> " + dado[i]["NM_NAVIO"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["PORTO_ORIGEM"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["EMBARQUE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["PREVISAO_CHEGADA"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["CHEGADA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NM_TIPO_CARGA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NM_TIPO_ESTUFAGEM"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["METRAGEM"].toFixed(3) + "</td>";
                            result += "<td class='text-center'> " + dado[i]["PESO"].toFixed(3) + "</td>";
                            result += "<td class='text-center'> " + dado[i]["FREEHAND"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["LTL"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["DTA_HUB"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["TRANSP"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["TRANSPORTADORA"] + "</td>";
                            result += "</tr>";
                        }
                        $("#grdConferenciaProcessoAnaliticoBody").append(result);
                    }
                    else {
                        $("#grdConferenciaProcessoAnaliticoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='16' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function exportRelatorioConsolidadaCSV(filename) {
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

        function exportRelatorioAnaliticoCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdConferenciaProcessoAnalitico tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdConferenciaProcessoAnalitico td, #grdConferenciaProcessoAnalitico th");

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
