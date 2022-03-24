<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioComissaoNacional.aspx.cs" Inherits="ABAINFRA.Web.RelatorioComissaoNacional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Comissao Nacional
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Comissao Nacional
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="listarComissaoNacional()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSV" onclick="GerarCSV('Comissao_Nacional.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="tblComissaoVendedor" class="table tablecont tablesorter">
                                    <thead>
                                        <tr>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">COMPETÊNCIA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">QUINZENA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA INICIAL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA FIINAL</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">VALOR COMISSÃO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">VALOR COMISSÃO CC</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA EXPORTACAO CC</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">DATA BAIXA</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyComissaoVendedor">
                                       
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
    <script>
        $(document).ready(function () {
            listarComissaoNacional();
        });

        function listarComissaoNacional() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarComissaoNacional",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tbodyComissaoVendedor").empty();
                    $("#tbodyComissaoVendedor").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#tbodyComissaoVendedor").empty();
                    if (dado != null) {
                        for (var i = 0; i < dado.length; i++) {
                            $("#tbodyComissaoVendedor").append("<tr><td class='text-center'>" + dado[i]["COMPETENCIA"] + "</td><td class='text-center'>" + dado[i]["NR_QUINZENA"] +"</td><td class='text-center'>"+dado[i]["DT_INICIAL"]+"</td><td class='text-center'>"+dado[i]["DT_FINAL"]+"</td>" +
                                "<td class='text-center'>" + dado[i]["VL_COMISSAO"] + "</td><td class='text-center'>" + dado[i]["VL_COMISSAO_CC"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_CC"] + "</td><td class='text-center'>"+dado[i]["DT_BAIXA"]+"</td></tr>"); 
                        }
                    }
                }
            });
        }

        function downloadCSVImport(csv, filename) {
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

        function GerarCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#tblComissaoVendedor tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#tblComissaoVendedor td, #tblComissaoVendedor th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVImport(csv.join("\n"), filename);
        }
    </script>
</asp:Content>
