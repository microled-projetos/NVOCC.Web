<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelacaoCotacao.aspx.cs" Inherits="ABAINFRA.Web.RelacaoCotacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relação Cotação
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Relação Cotação
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
                                        <button type="button" id="btnExportRelacaoCotacao" class="btn btn-primary" onclick="exportRelacaoCotacaoCSV('Relacao_Cotacao.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintRelacaoCotacao" class="btn btn-primary" onclick="PrintRelacaoCotacao()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Inicial:</label>
                                                <input id="txtDtInicialRelacaoCotacao" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Final:</label>
                                                <input id="txtDtFinalRelacaoCotacao" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Filtro</label>
                                                <select id="ddlFilterRelacaoCotacao" class="form-control">
                                                    <option value="">Selecione</option>
                                                    <option value="1">Vendedor</option>
                                                    <option value="2">Inside</option>
                                                    <option value="3">Cliente</option>
                                                    <option value="4">Status</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">*</label>
                                                <input id="txtRelacaoCotacao" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultarRelacaoCotacao" onclick="RelacaoCotacao()" class="btn btn-primary">Consultar</button>
                                        </div>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdRelacaoCotacao" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">SOLICITAÇÃO</th>
                                                <th class="text-center" scope="col">INSIDE</th>
                                                <th class="text-center" scope="col">NR COTAÇÃO</th>
                                                <th class="text-center" scope="col">MODAL</th>
                                                <th class="text-center" scope="col">INCOTERM</th>
                                                <th class="text-center" scope="col">CLIENTE</th>
                                                <th class="text-center" scope="col">SUB CLIENTE</th>
                                                <th class="text-center" scope="col">ORIGEM</th>
                                                <th class="text-center" scope="col">DESTINO</th>
                                                <th class="text-center" scope="col">VENDEDOR</th>
                                                <th class="text-center" scope="col">STATUS DA COTAÇÃO</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdRelacaoCotacaoBody">

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
        function RelacaoCotacao() {
            var dtInicial = document.getElementById("txtDtInicialRelacaoCotacao").value;
            var dtFinal = document.getElementById("txtDtFinalRelacaoCotacao").value;
            var nota = document.getElementById("txtRelacaoCotacao").value;
            var filter = document.getElementById("ddlFilterRelacaoCotacao").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarRelacaoCotacao",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdRelacaoCotacaoBody").empty();
                        $("#grdRelacaoCotacaoBody").append("<tr><td colspan='11'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdRelacaoCotacaoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdRelacaoCotacaoBody").append("<tr><td class='text-center'> " + dado[i]["SOLICITACAO"] + "</td><td class='text-center'>" + dado[i]["INSIDE"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_COTACAO"] + "</td><td class='text-center'>" + dado[i]["MODAL"] + "</td><td class='text-center'>" + dado[i]["INCOTERM"] + "</td><td class='text-center' style='max-width: 15ch;' title='" + dado[i]["SUB_CLIENTE"] +"'>" + dado[i]["CLIENTE"] + "</td>" +
                                    "<td class='text-center' style='max-width: 14ch;' title='" + dado[i]["SUB_CLIENTE"] +"'>" + dado[i]["SUB_CLIENTE"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td><td class='text-center'>" + dado[i]["VENDEDOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["STATUS_COTACAO"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdRelacaoCotacaoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='11' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
            } else {

            }
        }

        function exportRelacaoCotacaoCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdRelacaoCotacao tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdRelacaoCotacao td, #grdRelacaoCotacao th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVRelacaoCotacao(csv.join("\n"), filename);
        }

        function exportTableToCSVRelacaoCotacao(csv, filename) {
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

        function PrintRelacaoCotacao() {
            $("#modalRelacaoCotacao").modal('show');
            var dtInicial = document.getElementById("txtDtInicialRelacaoCotacao").value;
            var dtFinal = document.getElementById("txtDtFinalRelacaoCotacao").value;
            var diaI = document.getElementById("txtDtInicialRelacaoCotacao").value.substring(8, 10);
            var mesI = document.getElementById("txtDtInicialRelacaoCotacao").value.substring(5, 7);
            var anoI = document.getElementById("txtDtInicialRelacaoCotacao").value.substring(0, 4);
            var diaF = document.getElementById("txtDtFinalRelacaoCotacao").value.substring(8, 10);
            var mesF = document.getElementById("txtDtFinalRelacaoCotacao").value.substring(5, 7);
            var anoF = document.getElementById("txtDtFinalRelacaoCotacao").value.substring(0, 4);
            var nota = document.getElementById("txtRelacaoCotacao").value;
            var filter = document.getElementById("ddlFilterRelacaoCotacao").value;
            var position = 20;
            var positionLineF = 21;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarRelacaoCotacao",
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
                            doc.text("SOLICITAÇÃO", 5, 20);
                            doc.setLineWidth(0.2);
                            doc.line(4, 17, 295, 17);
                            doc.line(4, 17, 4, 21);
                            doc.line(4, 21, 295, 21);
                            doc.line(295, 17, 295, 21);

                            doc.line(23, 17, 23, 21);
                            doc.line(48, 17, 48, 21);
                            doc.line(68, 17, 68, 21);
                            doc.line(81, 17, 81, 21);
                            doc.line(98, 17, 98, 21);
                            doc.line(168, 17, 168, 21);
                            doc.line(196, 17, 196, 21);
                            doc.line(220, 17, 220, 21);
                            doc.line(241, 17, 241, 21);
                            doc.line(268, 17, 268, 21);

                            doc.text("INSIDE", 24, 20);
                            doc.text("NR COTAÇÃO", 49, 20);
                            doc.text("MODAL", 70, 20);
                            doc.text("INCOTERM", 83, 20);
                            doc.text("CLIENTE", 100, 20);
                            doc.text("SUB CLIENTE", 170, 20);
                            doc.text("ORIGEM", 198, 20);
                            doc.text("DESTINO", 222, 20);
                            doc.text("VENDEDOR", 243, 20);
                            doc.text("STATUS COTAÇÃO", 270, 20);
                            for (let i = 0; i < dado.length; i++) {
                                if (position >= pageHeight - 10) {
                                    doc.line(4, 21, 4, positionLineF);
                                    doc.line(23, 21, 23, positionLineF);
                                    doc.line(48, 21, 48, positionLineF);
                                    doc.line(68, 21, 68, positionLineF);
                                    doc.line(81, 21, 81, positionLineF);
                                    doc.line(98, 21, 98, positionLineF);
                                    doc.line(168, 21, 168, positionLineF);
                                    doc.line(196, 21, 196, positionLineF);
                                    doc.line(220, 21, 220, positionLineF);
                                    doc.line(241, 21, 241, positionLineF);
                                    doc.line(268, 21, 268, positionLineF);
                                    doc.line(295, 21, 295, positionLineF);
                                    doc.line(4, positionLineF, 295, positionLineF);
                                    doc.addPage();
                                    doc.setFontSize(15);
                                    doc.setFontStyle("bold");
                                    doc.text("RELAÇÃO DAS COTAÇÕES ABERTAS ENTRE " + diaI + "/" + mesI + "/" + anoI + " E " + diaF + "/" + mesF + "/" + anoF, 65, 10);
                                    doc.setFontSize(7);
                                    doc.text("SOLICITAÇÃO", 5, 20);
                                    doc.setLineWidth(0.2);
                                    doc.line(4, 17, 295, 17);
                                    doc.line(4, 17, 4, 21);
                                    doc.line(4, 21, 295, 21);
                                    doc.line(295, 17, 295, 21);

                                    doc.line(23, 17, 23, 21);
                                    doc.line(48, 17, 48, 21);
                                    doc.line(68, 17, 68, 21);
                                    doc.line(81, 17, 81, 21);
                                    doc.line(98, 17, 98, 21);
                                    doc.line(168, 17, 168, 21);
                                    doc.line(196, 17, 196, 21);
                                    doc.line(220, 17, 220, 21);
                                    doc.line(241, 17, 241, 21);
                                    doc.line(268, 17, 268, 21);

                                    doc.text("INSIDE", 24, 20);
                                    doc.text("NR COTAÇÃO", 49, 20);
                                    doc.text("MODAL", 70, 20);
                                    doc.text("INCOTERM", 83, 20);
                                    doc.text("CLIENTE", 100, 20);
                                    doc.text("SUB CLIENTE", 170, 20);
                                    doc.text("ORIGEM", 198, 20);
                                    doc.text("DESTINO", 222, 20);
                                    doc.text("VENDEDOR", 243, 20);
                                    doc.text("STATUS COTAÇÃO", 270, 20);
                                    position = 20;
                                    positionLineF = 21;
                                    
                                } else {
                                    doc.line(4, positionLineF, 295, positionLineF);
                                    positionLineF = positionLineF + 5;
                                    position = position + 5;
                                    doc.setFontStyle("normal");
                                    doc.text(dado[i]["SOLICITACAO"], 5, position);
                                    doc.text(dado[i]["INSIDE"], 24, position);
                                    doc.text(dado[i]["NR_COTACAO"], 49, position);
                                    doc.text(dado[i]["MODAL"].substring(0, 15), 70, position);
                                    doc.text(dado[i]["INCOTERM"].substring(0, 15), 83, position);
                                    doc.text(dado[i]["CLIENTE"].substring(0, 40), 100, position);
                                    doc.text(dado[i]["SUB_CLIENTE"].substring(0, 15), 170, position);
                                    doc.text(dado[i]["ORIGEM"].substring(0, 15), 198, position);
                                    doc.text(dado[i]["DESTINO"].substring(0, 15), 222, position);
                                    doc.text(dado[i]["VENDEDOR"].substring(0, 15), 243, position);
                                    doc.text(dado[i]["STATUS_COTACAO"].substring(0, 15), 270, position);
                                    
                                }
                            }
                            doc.line(4, 21, 4, positionLineF);
                            doc.line(23, 21, 23, positionLineF);
                            doc.line(48, 21, 48, positionLineF);
                            doc.line(68, 21, 68, positionLineF);
                            doc.line(81, 21, 81, positionLineF);
                            doc.line(98, 21, 98, positionLineF);
                            doc.line(168, 21, 168, positionLineF);
                            doc.line(196, 21, 196, positionLineF);
                            doc.line(220, 21, 220, positionLineF);
                            doc.line(241, 21, 241, positionLineF);
                            doc.line(268, 21, 268, positionLineF);
                            doc.line(295, 21, 295, positionLineF);
                            doc.line(4, positionLineF, 295, positionLineF);
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