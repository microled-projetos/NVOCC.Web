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
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="expRelatorioInvoice()">Exportar Grid - CSV</button>
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
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">Nº INVOICE</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">DATA REC</th>
                                                <th class="text-center" scope="col">TAXA REC</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">FRETE MBL</th>
                                                <th class="text-center" scope="col">HBL</th>
                                                <th class="text-center" scope="col">FRETE HBL</th>
                                                <th class="text-center" scope="col">ESTUFAGEM</th>
                                                <th class="text-center" scope="col">ORIGEM</th>
                                                <th class="text-center" scope="col">DESTINO</th>
                                                <th class="text-center" scope="col">EMBARQUE</th>
                                                <th class="text-center" scope="col">PREVISAO CHEGADA</th>
                                                <th class="text-center" scope="col">CHEGADA</th>
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.5.13/jspdf.plugin.autotable.min.js"></script>
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
            let result = "";
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
                                result += "<tr>";
                                result += "<td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["NR_INVOICE"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["AGENTE"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["VL_CAMBIO"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["MBL"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["FRETE_MASTER"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["HBL"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["FRETE_HOUSE"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["ESTUFAGEM"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["ORIGEM"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["DESTINO"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["DT_EMBARQUE"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>";
                                result += "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>";
                                result += "</tr>";

                            }
                            $("#grdInvoiceBody").append(result);
                        }
                        else {
                            $("#grdInvoiceBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
            } else {

            }
        }





        


        function expRelatorioInvoice(file) {
            var dtInicial = document.getElementById("txtDtInicialVencimentoInvoice").value;
            var dtFinal = document.getElementById("txtDtFinalVencimentoInvoice").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/imprimirInvoiceExp",
                data: JSON.stringify({ dataI: (dtInicial), dataF: (dtFinal), invoices: (arrayInvoice) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var invoices = [["PROCESSO;INVOICE;DT REC;TX REC;MBL;FRETE MBL;HBL;FRETE HBL;ESTUFAGEM;ORIGEM;DESTINO;EMBARQUE;PREVISÃO CHEGADA;CHEGADA"]];
                        for (let i = 0; i < dado.length; i++) {
                            invoices.push([dado[i]])
                        }
                        exportar(file, invoices.join("\n"));
                    }
                }
            })
        }

        function exportar(file, array) {
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
            var position = 37;
            var positionv = 38;
            var datetime = new Date().toLocaleString('pt-BR');
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarInvoices",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
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
                        doc.text("EMBARQUE POR PERÍODO", 115, 13);
                        doc.setFontSize(11);
                        doc.text("AGENTE: " + dado[0]["AGENTE"], 3, 27);
                        doc.setFontSize(8);
                        doc.text("INVOICES ENTRE: " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 3, 31);
                        doc.addImage(imgData, 'png', 10, 5, 60, 15);
                        doc.setFontSize(7);
                        doc.setLineWidth(0.2);
                        doc.line(3, 34, 296, 34);
                        doc.line(3, 38, 296, 38);
                        doc.line(3, 34, 3, 38);
                        doc.line(20, 34, 20, 38);
                        doc.line(67, 34, 67, 38);
                        doc.line(84, 34, 84, 38);
                        doc.line(97, 34, 97, 38);
                        doc.line(127, 34, 127, 38);
                        doc.line(145, 34, 145, 38);
                        doc.line(175, 34, 175, 38);
                        doc.line(195, 34, 195, 38);
                        doc.line(215, 34, 215, 38);
                        doc.line(230, 34, 230, 38);
                        doc.line(245, 34, 245, 38);
                        doc.line(262, 34, 262, 38);
                        doc.line(280, 34, 280, 38);
                        doc.line(296, 34, 296, 38);

                        doc.text("PROCESSO", 4, 37);
                        doc.text("INVOICE", 21, 37);
                        doc.text("DT REC", 68, 37);
                        doc.text("TX REC", 85, 37);
                        doc.text("MBL", 98, 37);
                        doc.text("FRETE MBL", 128, 37);
                        doc.text("HBL", 146, 37);
                        doc.text("FRETE HBL", 176, 37);
                        doc.text("ESTUFAGEM", 196, 37);
                        doc.text("ORIGEM", 216, 37);
                        doc.text("DESTINO", 231, 37);
                        doc.text("EMBARQUE", 246, 37);
                        doc.text("P.CHEGADA", 263, 37);
                        doc.text("CHEGADA", 281, 37);
                        for (let i = 0; i < dado.length; i++) {
                            if (position >= pageHeight - 10) {
                                doc.line(3, 38, 3, positionv);
                                doc.line(20, 38, 20, positionv);
                                doc.line(67, 38, 67, positionv);
                                doc.line(84, 38, 84, positionv);
                                doc.line(97, 38, 97, positionv);
                                doc.line(127, 38, 127, positionv);
                                doc.line(145, 38, 145, positionv);
                                doc.line(175, 38, 175, positionv);
                                doc.line(195, 38, 195, positionv);
                                doc.line(215, 38, 215, positionv);
                                doc.line(230, 38, 230, positionv);
                                doc.line(245, 38, 245, positionv);
                                doc.line(262, 38, 262, positionv);
                                doc.line(280, 38, 280, positionv);
                                doc.line(296, 38, 296, positionv);
                                doc.line(3, 38, 3, positionv);
                                doc.addPage();
                                doc.setFontStyle("bold");
                                doc.setFontSize(18);
                                doc.text("EMBARQUE POR PERÍODO", 115, 13);
                                doc.setFontSize(11);
                                doc.text("AGENTE: " + dado[0]["AGENTE"], 3, 27);
                                doc.setFontSize(8);
                                doc.text("INVOICES ENTRE: " + diaI + "/" + mesI + "/" + anoI + " e " + diaF + "/" + mesF + "/" + anoF, 3, 31);
                                doc.addImage(imgData, 'png', 10, 5, 60, 15);
                                doc.setFontSize(7);
                                doc.setLineWidth(0.2);
                                doc.line(3, 34, 296, 34);
                                doc.line(3, 38, 296, 38);
                                doc.line(3, 34, 3, 38);
                                doc.line(20, 34, 20, 38);
                                doc.line(67, 34, 67, 38);
                                doc.line(84, 34, 84, 38);
                                doc.line(97, 34, 97, 38);
                                doc.line(127, 34, 127, 38);
                                doc.line(145, 34, 145, 38);
                                doc.line(175, 34, 175, 38);
                                doc.line(195, 34, 195, 38);
                                doc.line(215, 34, 215, 38);
                                doc.line(230, 34, 230, 38);
                                doc.line(245, 34, 245, 38);
                                doc.line(262, 34, 262, 38);
                                doc.line(280, 34, 280, 38);
                                doc.line(296, 34, 296, 38);


                                doc.text("PROCESSO", 4, 37);
                                doc.text("INVOICE", 21, 37);
                                doc.text("DT REC", 68, 37);
                                doc.text("TX REC", 85, 37);
                                doc.text("MBL", 98, 37);
                                doc.text("FRETE MBL", 128, 37);
                                doc.text("HBL", 146, 37);
                                doc.text("FRETE HBL", 176, 37);
                                doc.text("ESTUFAGEM", 196, 37);
                                doc.text("ORIGEM", 216, 37);
                                doc.text("DESTINO", 231, 37);
                                doc.text("EMBARQUE", 246, 37);
                                doc.text("P.CHEGADA", 263, 37);
                                doc.text("CHEGADA", 281, 37);
                                position = 37 + 5;
                                positionv = 38 + 5;
                                doc.setFontStyle("normal");
                                doc.line(3, positionv, 296, positionv);
                                doc.text(dado[i]["NR_PROCESSO"], 4, position);
                                doc.text(dado[i]["NR_INVOICE"], 21, position);
                                doc.text(dado[i]["DT_LIQUIDACAO"], 68, position);
                                doc.text(dado[i]["VL_CAMBIO"], 85, position);
                                doc.text(dado[i]["MBL"], 98, position);
                                doc.text(dado[i]["FRETE_MASTER"], 128, position);
                                doc.text(dado[i]["HBL"], 146, position);
                                doc.text(dado[i]["FRETE_HOUSE"], 176, position);
                                doc.text(dado[i]["ESTUFAGEM"], 196, position);
                                doc.text(dado[i]["ORIGEM"], 216, position);
                                doc.text(dado[i]["DESTINO"], 231, position);
                                doc.text(dado[i]["DT_EMBARQUE"], 246, position);
                                doc.text(dado[i]["DT_PREVISAO_CHEGADA"], 263, position);
                                doc.text(dado[i]["DT_CHEGADA"], 281, position);
                            } else {
                                position = position + 5;
                                positionv = positionv + 5;
                                doc.setFontStyle("normal");
                                doc.line(3, positionv, 296, positionv);
                                doc.text(dado[i]["NR_PROCESSO"], 4, position);
                                doc.text(dado[i]["NR_INVOICE"], 21, position);
                                doc.text(dado[i]["DT_LIQUIDACAO"], 68, position);
                                doc.text(dado[i]["VL_CAMBIO"], 85, position);
                                doc.text(dado[i]["MBL"], 98, position);
                                doc.text(dado[i]["FRETE_MASTER"], 128, position);
                                doc.text(dado[i]["HBL"], 146, position);
                                doc.text(dado[i]["FRETE_HOUSE"], 176, position);
                                doc.text(dado[i]["ESTUFAGEM"], 196, position);
                                doc.text(dado[i]["ORIGEM"], 216, position);
                                doc.text(dado[i]["DESTINO"], 231, position);
                                doc.text(dado[i]["DT_EMBARQUE"], 246, position);
                                doc.text(dado[i]["DT_PREVISAO_CHEGADA"], 263, position);
                                doc.text(dado[i]["DT_CHEGADA"], 281, position);
                            }
                        }
                        doc.text("Gerado: " + datetime, 260, 205);
                        doc.line(3, positionv, 296, positionv);
                        doc.line(3, 38, 3, positionv);
                        doc.line(20, 38, 20, positionv);
                        doc.line(67, 38, 67, positionv);
                        doc.line(84, 38, 84, positionv);
                        doc.line(97, 38, 97, positionv);
                        doc.line(127, 38, 127, positionv);
                        doc.line(145, 38, 145, positionv);
                        doc.line(175, 38, 175, positionv);
                        doc.line(195, 38, 195, positionv);
                        doc.line(215, 38, 215, positionv);
                        doc.line(230, 38, 230, positionv);
                        doc.line(245, 38, 245, positionv);
                        doc.line(262, 38, 262, positionv);
                        doc.line(280, 38, 280, positionv);
                        doc.line(296, 38, 296, positionv);
                        doc.line(3, 38, 3, positionv);
                        doc.output("dataurlnewwindow");
                    }
                    else {
                    }
                }
            })

        }







        
    </script>
</asp:Content>