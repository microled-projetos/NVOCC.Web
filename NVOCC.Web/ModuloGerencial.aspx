<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuloGerencial.aspx.cs" Inherits="ABAINFRA.Web.ModuloGerencial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Módulo Gerencial
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Módulo Gerencial
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                <div style="margin: auto;">
                                    <button type="button" id="btnDevolveCont" class="btn btn-primary" data-toggle="modal" data-target="#modalDemurrage">Importar Dados Sistema TOTVS</button>                    
                                    <button type="button" id="btnExportCSV" class="btn btn-primary" data-toggle="modal" data-target="#modalExportarCSV" onclick="exportTableToCSVAtual('moduloGerencial.csv')">Exportar Arquivo CSV</button>                    
                                </div>
                            </div>
                            <div class="row" style="display: flex; margin:auto; margin-top:10px; justify-content:center">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data S.I Inicial<span class="required">*</span></label>
                                            <input id="dtSiInicio" class="form-control" type="date"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data S.I Final<span class="required">*</span></label>
                                            <input id="dtSiFinal" class="form-control" type="date"/>
                                        </div>
                                    </div>
                            </div>
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:</label>
                                        <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"></label>
                                        <input id="txtConsulta" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Estufagem</label>
                                        <asp:DropDownList ID="ddlTipoEstufagem" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label">Serviço</label>
                                        <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="listarProcessos()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="alert alert-success text-center" id="msgSuccessUpdate">
                                        Courrier atualizado com sucesso.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrUpdate">
                                        Erro ao atualizar courrier.
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="tblModuloGerencial" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">PROCESSO</th>
                                            <th class="text-center" scope="col" style="width: 200px;">CLIENTE</th>
                                            <th class="text-center" scope="col" style="width: 200px;">CARRIER</th>
                                            <th class="text-center" scope="col">TIPO ESTUFAGEM</th>
                                            <th class="text-center" scope="col">CNTR 20</th>
                                            <th class="text-center" scope="col">CNTR 40</th>
                                            <th class="text-center" scope="col">TIPO</th>
                                            <th class="text-center" scope="col">ORIGEM</th>
                                            <th class="text-center" scope="col">DESTINO</th>
                                            <th class="text-center" scope="col">STATUS COTAÇÃO</th>
                                            <th class="text-center" scope="col">DATA CANCELAMENTO</th>
                                            <th class="text-center" scope="col">S.I</th>
                                            <th class="text-center" scope="col">ETD</th>
                                            <th class="text-center" scope="col">ETA</th>
                                            <th class="text-center" scope="col">DATA CHEGADA</th>
                                            <th class="text-center" scope="col">DATA RECEBIMENTO</th>
                                            <th class="text-center" scope="col">VENDEDOR</th>
                                            <th class="text-center" scope="col">AGENTE INTERNACIONAL</th>
                                            <th class="text-center" scope="col">AGENTE DESPACHANTE</th>
                                            <th class="text-center" scope="col">WEEK</th>
                                            <th class="text-center" scope="col">PREVISÃO VENDA TOTAL</th>
                                            <th class="text-center" scope="col">PREVISÃO VENDA EM</th>
                                            <th class="text-center" scope="col">DATA PAGAMENTO</th>
                                            <th class="text-center" scope="col">VALOR PAGAMENTO</th>
                                            <th class="text-center" scope="col">DATA RECEBTO</th>
                                            <th class="text-center" scope="col">VALOR RECEBTO</th>
                                            <th class="text-center" scope="col">DEMU VALOR</th>
                                            <th class="text-center" scope="col">DEMU DATA</th>
                                            <th class="text-center" scope="col">DEMU VALOR</th>
                                            <th class="text-center" scope="col">DEMU DATA</th>
                                            <th class="text-center" scope="col">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblModuloGerencialBody">
                                       
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
    <script>

        $(document).ready(function () {
            listarProcessos();
        });


        function listarProcessos() {
            var nmfilter = document.getElementById("MainContent_ddlFiltro").value;
            var txtfilter = document.getElementById("txtConsulta").value;
            var servico = document.getElementById("MainContent_ddlServico").value;
            var estufagem = document.getElementById("MainContent_ddlTipoEstufagem").value;
            var dtSiIni = document.getElementById("dtSiInicio").value;
            var dtSiFim = document.getElementById("dtSiFinal").value;
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarProcessos",
                data: '{nmfilter:"' + nmfilter + '",txtfilter: "' + txtfilter + '",estufagem: "' + estufagem + '",servico:"' + servico + '", dtSiIni:"' + dtSiIni + '", dtSiFim:"' + dtSiFim + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblModuloGerencialBody").empty();
                    $("#tblModuloGerencialBody").append("<tr><td colspan='20'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#tblModuloGerencialBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblModuloGerencialBody").append("<tr><td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CARRIER"] + "' style='max-width: 10ch;'>" + dado[i]["CARRIER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TIPOESTUFAGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["QTDE20"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["QTDE40"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TIPO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["ORIGEM"] + "' style='max-width: 10ch;'>" + dado[i]["ORIGEM"] + "</td>" +
                                "<td class='text-center'  title='" + dado[i]["DESTINO"] + "' style='max-width: 10ch;'>" + dado[i]["DESTINO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["STATUS_COTACAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DTCANCELAMENTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DTABERTURA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ETD"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ETA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DATARECEBIMENTO"] + "</td>" +
                                "<td class='text-center'  title='" + dado[i]["VENDEDOR"] + "' style='max-width: 10ch;'>" + dado[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'  title='" + dado[i]["AGENTECARGA"] + "' style='max-width: 10ch;'>" + dado[i]["AGENTECARGA"] + "</td>" +
                                "<td class='text-center'  title='" + dado[i]["NMCOMISSARIA"] + "' style='max-width: 10ch;'>" + dado[i]["NMCOMISSARIA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["WEEK"] + "</td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center'>" + dado[i]["DT_PAGO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_PAGO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_RECEBIDO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_RECEBIDO"] + "</td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center'></td>" +
                                "<td class='text-center'></td></tr > ");

                        }
                    }

                    else {
                        $("#tblModuloGerencialBody").empty();
                        $("#tblModuloGerencialBody").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        function downloadCSVAtual(csv, filename) {
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

        function exportTableToCSVAtual(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#tblModuloGerencial tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#tblModuloGerencial td, #tblModuloGerencial th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVAtual(csv.join("\n"), filename);
        }

    </script>
</asp:Content>
