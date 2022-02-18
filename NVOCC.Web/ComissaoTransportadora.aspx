<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComissaoTransportadora.aspx.cs" Inherits="ABAINFRA.Web.ComissaoTransportadora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Comissão Transportadoras
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Comissão Transportadoras
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
                                        <button type="button" id="btnExportComissaoTransportadora" class="btn btn-primary" onclick="exportComissaoTransportadoraCSV('Comissao_Transportadoras.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacao" class="btn btn-primary" onclick="createPDF()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterComissaoTransportadora" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Transportadora</option>
                                                <option value="2">Processo</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtComissaoTransportadora" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterComissaoTransportadoraParametro" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="0">Todas as comissões</option>
                                                <option value="1">Comissões a receber</option>
                                                <option value="2">Comissões recebidas a 90 dias</option>
                                                <option value="3">Comissões recebidas a 180 dias</option>
                                                <option value="4">Todas comissões recebidas</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarComissaoTransportadora" onclick="ComissaoTransportadora()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div>
                            </div> 
                            <div id="tableComissaoTransportadora" class="table-responsive fixedDoubleHead topMarg">
                                <table id="grdComissaoTransportadora" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">NR PROCESSO</th>
                                            <th class="text-center" scope="col">TRANSPORTADORA</th>
                                            <th class="text-center" scope="col">DESCRIÇÃO DO ITEM</th>
                                            <th class="text-center" scope="col">VALOR COMISSÃO</th>
                                            <th class="text-center" scope="col">DATA LIQUIDAÇÃO</th>
                                            <th class="text-center" scope="col">Nº NOTA FISCAL</th>
                                            <th class="text-center" scope="col">DATA NOTA FISCAL</th>
                                        </tr>
                                    </thead>
                                    <tbody id="grdComissaoTransportadoraBody">

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
        function ComissaoTransportadora() {
            var nota = document.getElementById("txtComissaoTransportadora").value;
            var filter = document.getElementById("ddlFilterComissaoTransportadora").value;
            var param = document.getElementById("ddlFilterComissaoTransportadoraParametro").value;
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarComissaoTransportadora",
                    data: '{nota: "' + nota + '", filter: "' + filter + '", parametro: "'+param+'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdComissaoTransportadoraBody").empty();
                        $("#grdComissaoTransportadoraBody").append("<tr><td colspan='7'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdComissaoTransportadoraBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdComissaoTransportadoraBody").append("<tr style='word-break: break-word'>" +
                                    "<td class='text-center'> " + dado[i]["PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TRANSPORTADORA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["ITEM"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["COMISSAO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_LIQ"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NOTA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_NOTA"] + "</td></tr > ");
                            }
                        }
                        else {
                            $("#grdComissaoTransportadoraBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='7' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
        }

        function exportComissaoTransportadoraCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdComissaoTransportadora tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdComissaoTransportadora td, #grdComissaoTransportadora th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVComissaoTransportadora(csv.join("\n"), filename);
        }

        function exportTableToCSVComissaoTransportadora(csv, filename) {
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
            var sTable = document.getElementById('tableComissaoTransportadora').innerHTML;

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
