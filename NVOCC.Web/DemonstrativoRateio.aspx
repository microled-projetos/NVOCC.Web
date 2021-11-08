<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DemonstrativoRateio.aspx.cs" Inherits="ABAINFRA.Web.DemonstrativoRateio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Demonstrativo de Rateio
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Demonstrativo de Rateio
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
                                        <button type="button" id="btnExportDemonstrativoRateio" class="btn btn-primary" onclick="exportCSV('Invoice.csv')">Exportar Grid - CSV</button>
                                        <button type="button" id="btnPrintDemonstrativoRateio" class="btn btn-primary" onclick="printDemonstrativoRateio()">Imprimir</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterRateio" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Agente</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtDemonstrativoRateio" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarDemonstrativoRateio" onclick="listarRateio()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdRateio" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col" style="width: 100px">&nbsp;</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">DATA CHEGADA</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdRateioBody">

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

    <script>
        //PagamentosRecebimentos
        $(document).ready(function () {
            listarRateio();
        });
        var id = 0;
        function listarRateio() {
            var text = document.getElementById("txtDemonstrativoRateio").value;
            var filter = document.getElementById("ddlFilterRateio").value;
            
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarDemonstrativoRateio",
                data: '{text: "' + text + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdRateioBody").empty();
                    $("#grdRateioBody").append("<tr><td colspan='3'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdRateioBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdRateioBody").append("<tr data-id='" + dado[i]["ID_BL"] + "'><td class='text-center' style='display: flex; align-items: center'><span class='btn btn-primary select' onclick='setId(" + dado[i]["ID_BL"] + ")' style='margin-right: 10px; width: 100px'>Selecionar</span></td><td class='text-center'> " + dado[i]["MBL"] + "</td><td class='text-center'>" + dado[i]["CHEGADA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdRateioBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='3' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function setId(Id) {
            id = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function exportCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdInvoice tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdInvoice td, #grdInvoice th");

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

        function printDemonstrativoRateio() {
            var lineIv = 26;
            var lineFv = 30;
            var lineIh = 10;
            var lineFh = 63;
            var lineProc = 29;
            var lineCub = 44;
            var fieldIv = 28.5;
            var fieldIh = 11;
            var fieldCub = 30;
            var fieldHbl = 45;
            var positionV = 28;
            var position = 30;
            var itemid = [];
            var l = 0;
            var aux = 0
            var total = 0
            var totalcub = 0;
            var totalRatTot = 0;
            var totalRatNf = 0;
            var totalRatIss = 0;
            var totalRatLiq = 0;
            var auxIv = 0;
            var auxFv = 0;
            var auxFieldIv = 0;
            var auxPosV = 0;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/indiceItemDespesa",
                data: '{blmaster: "' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        for (let x = 0; x < dado.length; x++) {
                            itemid.push(dado[x]["INDICEITEM"]);
                        }
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/imprimirDemonstrativoRateio",
                            data: '{blmaster: "' + id + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (dado) {
                                var dados = dado.d;
                                dados = $.parseJSON(dados);
                                var doc = new jsPDF("l");
                                var pageHeight = doc.internal.pageSize.height;
                                var pageWidth = doc.internal.pageSize.width;
                                var imgData = new Image();
                                imgData.src = 'Content/imagens/FCA-LOG(deitado).png';
                                doc.setFontStyle("bold");
                                doc.addImage(imgData, 'png', 10, 5, 45, 12);
                                doc.setFontSize(18);
                                doc.setTextColor(50, 50, 50);
                                doc.text("RATEIO DE DESPESAS DO MASTER POR PESO CÚBICO", 70, 13);
                                doc.setFontSize(10);
                                doc.text("MBL: " + dados[0]["NR_BL"], 130, 18);
                                doc.setFontSize(7);
                                for (let z = 0; z < itemid.length; z++) {
                                    if (lineIh < pageWidth - 20) {
                                        if (l == 0) {
                                            lineFv = 30
                                            doc.line(lineIh, lineIv, lineFh, lineIv)
                                            doc.line(lineIh, lineIv, lineIh, lineFv)
                                            doc.line(lineIh, lineFv, lineFh, lineFv)
                                            doc.line(lineFh, lineIv, lineFh, lineFv)
                                            doc.line(lineProc, lineIv, lineProc, lineFv)
                                            doc.line(lineCub, lineIv, lineCub, lineFv)
                                            doc.text(itemid[z].toString().substr(0, 20), fieldIh + 9, fieldIv - 4);
                                            doc.text("PROCESSO", fieldIh, fieldIv + 0.5);
                                            doc.text("CUBAGEM", fieldCub, fieldIv + 0.5);
                                            doc.text("HBL", fieldHbl, fieldIv + 0.5);
                                            for (let i = 0; i < dados.length; i++) {
                                                if (dados[i]["ITEM"] == itemid[z]) {
                                                    position = position + 4.5;
                                                    positionV = positionV + 5;
                                                    doc.line(lineIh, lineFv, lineIh, lineFv + 5)
                                                    doc.line(lineIh, lineFv + 5, lineFh, lineFv + 5)
                                                    doc.line(lineFh, lineFv, lineFh, lineFv + 5)
                                                    doc.text(dados[i]["PROCESSO"], fieldIh, positionV)
                                                    doc.text(dados[i]["CUBAGEM"].toFixed(3), fieldCub, positionV)
                                                    doc.text(dados[i]["HBL"].toFixed(2), fieldHbl, positionV)
                                                    totalcub = totalcub + parseFloat(dados[i]["CUBAGEM"].toFixed(3));
                                                    total = total + parseFloat(dados[i]["HBL"].toFixed(2));
                                                    lineFv = lineFv + 5;
                                                }
                                            }
                                            
                                            doc.text("TOTAL", fieldIh, lineFv + 3.5)
                                            doc.text(totalcub.toFixed(3), fieldCub, lineFv + 3.5)
                                            doc.text(total.toFixed(2), fieldHbl, lineFv + 3.5)
                                            lineFv = lineFv + 5;
                                            doc.line(lineProc, lineIv + 4, lineProc, lineFv)
                                            doc.line(lineCub, lineIv + 4, lineCub, lineFv)
                                            doc.line(lineIh, lineIv + 4, lineIh, lineFv)
                                            doc.line(lineFh, lineIv + 4, lineFh, lineFv)
                                            doc.line(lineIh, lineFv, lineFh, lineFv)
                                            totalcub = 0;
                                            total = 0;
                                            fieldIh = fieldIh + 55;
                                            lineIh = lineIh + 55;
                                            lineFh = lineFh + 55;
                                            fieldCub = fieldCub + 55
                                            fieldHbl = fieldHbl + 55;
                                            lineProc = lineProc + 55;
                                            lineCub = lineCub + 55;
                                            aux = positionV;
                                            position = 30;
                                            positionV = 28;
                                        } else {                                            
                                            fieldIv = auxFieldIv;
                                            lineIv = auxIv;
                                            lineFv = auxFv;
                                            positionV = auxPosV;
                                            doc.line(lineIh, lineIv, lineFh, lineIv)
                                            doc.line(lineIh, lineIv, lineIh, lineFv)
                                            doc.line(lineIh, lineFv, lineFh, lineFv)
                                            doc.line(lineFh, lineIv, lineFh, lineFv)
                                            doc.line(lineProc, lineIv, lineProc, lineFv)
                                            doc.line(lineCub, lineIv, lineCub, lineFv)
                                            doc.text(itemid[z].toString().substr(0, 20), fieldIh + 9, fieldIv - 4);
                                            doc.text("PROCESSO", fieldIh, fieldIv + 0.5);
                                            doc.text("CUBAGEM", fieldCub, fieldIv + 0.5);
                                            doc.text("HBL", fieldHbl, fieldIv + 0.5);
                                            for (let i = 0; i < dados.length; i++) {
                                                if (dados[i]["ITEM"] == itemid[z]) {
                                                    position = position + 5;
                                                    positionV = positionV + 5;
                                                    doc.line(lineIh, lineFv, lineIh, lineFv + 5)
                                                    doc.line(lineIh, lineFv + 5, lineFh, lineFv + 5)
                                                    doc.line(lineFh, lineFv, lineFh, lineFv + 5)
                                                    doc.text(dados[i]["PROCESSO"], fieldIh, positionV)
                                                    doc.text(dados[i]["CUBAGEM"].toFixed(3), fieldCub, positionV)
                                                    doc.text(dados[i]["HBL"].toFixed(2), fieldHbl, positionV)
                                                    totalcub = totalcub + parseFloat(dados[i]["CUBAGEM"].toFixed(3));
                                                    total = total + parseFloat(dados[i]["HBL"].toFixed(2));
                                                    lineFv = lineFv + 5;
                                                }
                                                position = fieldIv + 1;
                                            }
                                            
                                            doc.text("TOTAL", fieldIh, lineFv + 3.5)
                                            doc.text(totalcub.toFixed(3), fieldCub, lineFv + 3.5)
                                            doc.text(total.toFixed(2), fieldHbl, lineFv + 3.5)
                                            lineFv = lineFv + 5;
                                            doc.line(lineProc, lineIv + 4, lineProc, lineFv)
                                            doc.line(lineCub, lineIv + 4, lineCub, lineFv)
                                            doc.line(lineIh, lineIv + 4, lineIh, lineFv)
                                            doc.line(lineFh, lineIv + 4, lineFh, lineFv)
                                            doc.line(lineIh, lineFv, lineFh, lineFv)
                                            fieldIh = fieldIh + 55;
                                            lineIh = lineIh + 55;
                                            lineFh = lineFh + 55;
                                            fieldCub = fieldCub + 55
                                            fieldHbl = fieldHbl + 55;
                                            lineProc = lineProc + 55;
                                            lineCub = lineCub + 55;
                                            totalcub = 0;
                                            total = 0;
                                        }
                                    } else {
                                        l = 1;
                                        
                                        lineIv = lineFv + 10;
                                        lineFv = lineIv + 4;
                                        lineIh = 10;
                                        lineFh = 63;
                                        lineProc = 29;
                                        lineCub = 44;
                                        fieldIv = lineIv + 2.5;
                                        fieldIh = 11;
                                        fieldCub = 30;
                                        fieldHbl = 45;
                                        positionV = lineIv + 2;
                                        position = 30;

                                        auxIv = lineIv;
                                        auxFv = lineFv;
                                        auxFieldIv = fieldIv;
                                        auxPosV = positionV;
                                        /*lineIh = 10;
                                        lineFh = 63;
                                        fieldIh = 11;
                                        fieldCub = 30;
                                        fieldHbl = 45;
                                        fieldIv = aux + 15.5;
                                        lineIv = aux + 13;
                                        lineFv = aux + 17;
                                        lineProc = 29;
                                        lineCub = 44;*/
                                        doc.line(lineIh, lineIv, lineFh, lineIv)
                                        doc.line(lineIh, lineIv, lineIh, lineFv)
                                        doc.line(lineIh, lineFv, lineFh, lineFv)
                                        doc.line(lineFh, lineIv, lineFh, lineFv)
                                        doc.line(lineProc, lineIv, lineProc, lineFv)
                                        doc.line(lineCub, lineIv, lineCub, lineFv)
                                        doc.text(itemid[z].toString().substr(0, 20), fieldIh + 9, fieldIv - 4);
                                        doc.text("PROCESSO", fieldIh, fieldIv + 0.5);
                                        doc.text("CUBAGEM", fieldCub, fieldIv + 0.5);
                                        doc.text("HBL", fieldHbl, fieldIv + 0.5);
                                        for (let i = 0; i < dados.length; i++) {
                                            if (dados[i]["ITEM"] == itemid[z]) {  
                                                position = position + 4;
                                                positionV = positionV + 5;
                                                doc.line(lineIh, lineFv, lineIh, lineFv + 5)
                                                doc.line(lineIh, lineFv + 5, lineFh, lineFv + 5)
                                                doc.line(lineFh, lineFv, lineFh, lineFv + 5)
                                                doc.text(dados[i]["PROCESSO"], fieldIh, positionV)
                                                doc.text(dados[i]["CUBAGEM"].toFixed(3), fieldCub, positionV)
                                                doc.text(dados[i]["HBL"].toFixed(2), fieldHbl, positionV)
                                                totalcub = totalcub + parseFloat(dados[i]["CUBAGEM"].toFixed(3));
                                                total = total + parseFloat(dados[i]["HBL"].toFixed(2));
                                                lineFv = lineFv + 5;

                                            }
                                            position = fieldIv + 1;
                                        }
                                        
                                        doc.text("TOTAL", fieldIh, lineFv + 3.5)
                                        doc.text(totalcub.toFixed(3), fieldCub, lineFv + 3.5)
                                        doc.text(total.toFixed(2), fieldHbl, lineFv + 3.5)
                                        lineFv = lineFv + 5;
                                        doc.line(lineProc, lineIv + 4, lineProc, lineFv)
                                        doc.line(lineCub, lineIv + 4, lineCub, lineFv)
                                        doc.line(lineIh, lineIv + 4, lineIh, lineFv)
                                        doc.line(lineFh, lineIv + 4, lineFh, lineFv)
                                        doc.line(lineIh, lineFv, lineFh, lineFv)
                                        totalcub = 0;
                                        total = 0;
                                        fieldIh = fieldIh + 55;
                                        lineIh = lineIh + 55;
                                        lineFh = lineFh + 55;
                                        fieldCub = fieldCub + 55
                                        fieldHbl = fieldHbl + 55;
                                        lineProc = lineProc + 55;
                                        lineCub = lineCub + 55;
                                    }
                                }
                                $.ajax({
                                    type: "POST",
                                    url: "DemurrageService.asmx/imprimirDemonstrativoRateioTotal",
                                    data: '{blmaster: "' + id + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (dado) {
                                        var data = dado.d;
                                        data = $.parseJSON(data);
                                        if (data != null) {
                                            lineIv = 26;
                                            lineFv = 30;
                                            lineIh = 10;
                                            lineFh = 165;
                                            lineProc = 29;
                                            lineRateioTotal = 44;
                                            lineRateioNf = 74;
                                            lineRateioIss = 104;
                                            lineNfLiquido = 134;
                                            fieldRateioTotal = 45;
                                            fieldRateioNf = 75;
                                            fieldRateioIss = 105;
                                            fieldNfLiquido = 135;
                                            fieldIv = 28.5;
                                            fieldIh = 11;
                                            fieldCub = 30;
                                            positionV = 28;
                                            position = 30;
                                            doc.addPage();
                                            doc.setFontStyle("bold");
                                            doc.setFontSize(18);
                                            doc.setTextColor(50, 50, 50);
                                            doc.text("RATEIO DE DESPESAS DO MASTER POR PESO CÚBICO", 70, 13);
                                            doc.setFontSize(7);
                                            doc.line(lineIh, lineIv, lineIh, lineFv)
                                            doc.line(lineIh, lineIv, lineFh, lineIv)
                                            doc.line(lineIh, lineFv, lineFh, lineFv)
                                            doc.line(lineFh, lineIv, lineFh, lineFv)
                                            doc.line(lineProc, lineIv, lineProc, lineFv)
                                            
                                            doc.line(lineRateioTotal, lineIv, lineRateioTotal, lineFv)
                                            doc.line(lineRateioNf, lineIv, lineRateioNf, lineFv)
                                            doc.line(lineRateioIss, lineIv, lineRateioIss, lineFv)
                                            doc.line(lineNfLiquido, lineIv, lineNfLiquido, lineFv)
                                            doc.text("PROCESSO", fieldIh, fieldIv + 0.5);
                                            doc.text("CUBAGEM", fieldCub, fieldIv + 0.5);
                                            doc.text("RATEIO TOTAL", fieldRateioTotal, fieldIv + 0.5);
                                            doc.text("RATEIO NF", fieldRateioNf, fieldIv + 0.5);
                                            doc.text("RATEIO ISS", fieldRateioIss, fieldIv + 0.5);
                                            doc.text("NF LIQUIDO", fieldNfLiquido, fieldIv + 0.5);
                                            for (let k = 0; k < data.length; k++) {
                                                fieldIv = fieldIv + 5;
                                                lineFv = lineFv + 5;

                                                doc.line(lineIh, lineFv, lineFh, lineFv)                                                
                                                doc.text(data[k]["PROCESSO"], fieldIh, fieldIv + 0.5);
                                                doc.text(data[k]["CUBAGEM"].toFixed(3), fieldCub, fieldIv + 0.5);
                                                doc.text(data[k]["RATEIO_TOTAL"].toFixed(2), fieldRateioTotal, fieldIv + 0.5);
                                                doc.text(data[k]["RATEIONF"].toFixed(2), fieldRateioNf, fieldIv + 0.5);
                                                doc.text(data[k]["RATEIOISS"].toFixed(2), fieldRateioIss, fieldIv + 0.5);
                                                doc.text(data[k]["NFLIQUIDO"].toFixed(2), fieldNfLiquido, fieldIv + 0.5);
                                                totalRatTot = totalRatTot + parseFloat(data[k]["RATEIO_TOTAL"].toFixed(2));
                                                totalRatNf = totalRatNf + parseFloat(data[k]["RATEIONF"].toFixed(2));
                                                totalRatIss = totalRatIss + parseFloat(data[k]["RATEIOISS"].toFixed(2));
                                                totalRatLiq = totalRatLiq + parseFloat(data[k]["NFLIQUIDO"].toFixed(2));
                                            }
                                            
                                            fieldIv = fieldIv + 5;
                                            lineFv = lineFv + 5;
                                            doc.line(lineProc, lineIv + 4, lineProc, lineFv - 5)
                                            doc.line(lineRateioTotal, lineIv + 4, lineRateioTotal, lineFv)
                                            doc.line(lineRateioNf, lineIv + 4, lineRateioNf, lineFv)
                                            doc.line(lineRateioIss, lineIv + 4, lineRateioIss, lineFv)
                                            doc.line(lineNfLiquido, lineIv + 4, lineNfLiquido, lineFv)
                                            doc.line(lineIh, lineFv, lineFh, lineFv)
                                            doc.line(lineIh, lineIv + 4, lineIh, lineFv)
                                            doc.line(lineFh, lineIv + 4, lineFh, lineFv)
                                            doc.text("TOTAL", fieldIh, fieldIv);
                                            doc.text(totalRatTot.toFixed(2), fieldRateioTotal, fieldIv);
                                            doc.text(totalRatNf.toFixed(2), fieldRateioNf, fieldIv);
                                            doc.text(totalRatIss.toFixed(2), fieldRateioIss, fieldIv);
                                            doc.text(totalRatLiq.toFixed(2), fieldNfLiquido, fieldIv);
                                        }
                                        doc.output("dataurlnewwindow");
                                    }
                                })
                                
                            }
                        })
                    }
                }
            })
        }
    </script>
</asp:Content>