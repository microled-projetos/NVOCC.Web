<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTOTVSPA.aspx.cs" Inherits="ABAINFRA.Web.integracaoTOTVSPA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TOTVS PA
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Integração TOTVS PA
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    <div class="alert alert-danger text-center" id="msgErrExportPACli">
                                        Não há dados para exportar FORNEC.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrExportPARec">
                                        Não há dados para exportar REC.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessPAFornec">
                                        Tabela Despesa FORNEC exportada com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessPARec">
                                        Tabela Despesa REC exportada com sucesso.
                                    </div>
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportTotusPA" class="btn btn-primary" onclick="exportTableToCSVPA('FORNEC_FCA.csv','REC_PA.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Emissão Inicial:</label>
                                            <input id="txtDtEmissaoInicialPA" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Emissão Final:</label>
                                            <input id="txtDtEmissaoFinalPA" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Nº Nota Despesa</label>
                                            <input id="txtNotaPA" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterPA" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Id Master</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div style="font-size: 12px">
                                        <input type="radio" id="todosPA" name="exportPA" value="1" >
                                        <label for="venda">Todos os Registros</label><br>
                                        <input type="radio" id="naoExportadosPA" name="exportPA" value="2" checked>
                                        <label for="compra">Registros Não Exportados</label><br>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarPA" style="margin-left: 10px;" onclick="TOTVSPA()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div> 
                                <div class="table-responsive tableFixHead topMarg">
                                    <table id="grdPA" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">Data Pagamento</th>
                                                <th class="text-center" scope="col">Id Master</th>
                                                <th class="text-center" scope="col">Nº Processo</th>
                                                <th class="text-center" scope="col">Fornecedor</th>
                                                <th class="text-center" scope="col">Data de Emissão</th>
                                                <th class="text-center" scope="col">Data Exportação</th>
                                                <th class="text-center" scope="col">Cliente</th>
                                                <th class="text-center" scope="col">Item</th>
                                                <th class="text-center" scope="col">Valor Liquido</th>
                                                <th class="text-center" scope="col">Valor ISS</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdPABody">

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
        function formatDate(date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [year, month, day].join('-');
        }

        var currentDate = new Date();
        var yesterday = new Date();

        yesterday.setDate(yesterday.getDate() - 1);
        currentDate.setDate(currentDate.getDate());

        var diaSemana = yesterday.getDay();

        switch (diaSemana) {
            case "0":
                yesterday.setDate(yesterday.getDate() - 2);
                document.getElementById("txtDtEmissaoInicialPA").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalPA").value = formatDate(currentDate);
                break;
            case "6":
                yesterday.setDate(yesterday.getDate() - 1);
                document.getElementById("txtDtEmissaoInicialPA").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalPA").value = formatDate(currentDate);
            default:
                document.getElementById("txtDtEmissaoInicialPA").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalPA").value = formatDate(currentDate);
                break;
        }

        function TOTVSPA() {
            $("#modalPA").modal("show");
            var exporta = document.getElementById("todosPA");
            var dataI = document.getElementById("txtDtEmissaoInicialPA").value;
            var dataF = document.getElementById("txtDtEmissaoFinalPA").value;
            var nota = document.getElementById("txtNotaPA").value;
            var filter = document.getElementById("ddlFilterPA").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSPA",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdPABody").empty();
                    $("#grdPABody").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdPABody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdPABody").append("<tr><td class='text-center'> " + dado[i]["DT_PAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["ID_BL_MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NM_FORNECEDOR"] + "</td><td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_LIQUIDO"] + "</td><td class='text-center'>" + dado[i]["VL_ISS"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdPABody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='10' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function exportTableToCSVPA(fornecf, recf) {
            var exporta = document.getElementById("todosPA");
            var dataI = document.getElementById("txtDtEmissaoInicialPA").value;
            var dataF = document.getElementById("txtDtEmissaoFinalPA").value;
            var nota = document.getElementById("txtNotaPA").value;
            var filter = document.getElementById("ddlFilterPA").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }

            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSPA",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        PAFornec(dataI, dataF, situacao, nota, filter, fornecf, recf);
                    }
                    else {
                        $("#msgErrExportPARec").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function PAFornec(dataI, dataF, situacao, nota, filter, fornecf, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSPAFORNEC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var fornec = [["A2_XGRUPO;A2_COD;A2_LOJA;A2_NOME;A2_NREDUZ;A2_END;A2_BAIRRO;A2_EST;A2_COD_MUN;A2_CEP;A2_TIPO;A2_CGC;A2_TEL;A2_INSCR;A2_INSCRM;A2_EMAIL;A2_DDD;A2_NATUREZ;A2_CODPAIS;A2_CONTATO;A2_SIMPNAC"]];
                        for (let i = 0; i < dado.length; i++) {
                            fornec.push([dado[i]]);
                        }
                        PAREC(dataI, dataF, situacao, nota, filter, fornec, fornecf, recf)
                    }
                    else {
                        $("#msgErrExportPARec").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function PAREC(dataI, dataF, situacao, nota, filter, fornec, fornecf, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSPAREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var rec = [["E2_FILIAL;E2_PREFIXO;E2_NUM;E2_PARCELA;E2_TIPO;E2_FORNECE;E2_LOJA;E2_NATUREZ;E2_EMISSAO;E2_VENCTO;E2_VENCREA;E2_VALOR;E2_HIST;E2_ITEMCTA;E2_USERS;E2_XPROD"]];
                        for (let i = 0; i < dado.length; i++) {
                            rec.push([dado[i]]);
                        }
                        updateContaPagarReceberPA(dataI, dataF, situacao, nota, filter, fornec, rec, fornecf, recf);
                    }
                    else {
                        $("#msgErrExportPACli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function updateContaPagarReceberPA(dataI, dataF, situacao, nota, filter, fornec, rec, fornecf, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/integrarTOTVSPA",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        downloadCSVPA(fornec.join("\n"), rec.join("\n"), fornecf, recf)
                        $("#msgSuccessPAFornec").fadeIn(500).delay(1000).fadeOut(500);
                    } else {
                        msgSuccessPAFornec
                    }
                }
            });
        }

        function downloadCSVPA(fornec, rec, fornecf, recf) {
            var csvFile;
            var csvFile2;

            var downloadLink;
            var downloadLink2;


            // CSV file
            csvFile = new Blob(["\uFEFF" + fornec], { type: "text/csv;charset=utf-8;" });
            csvFile2 = new Blob(["\uFEFF" + rec], { type: "text/csv;charset=utf-8;" });

            // Download link
            downloadLink = document.createElement("a");
            downloadLink2 = document.createElement("a");


            // File name
            downloadLink.download = fornecf;
            downloadLink2.download = recf;


            // Create a link to the file
            downloadLink.href = window.URL.createObjectURL(csvFile);
            downloadLink2.href = window.URL.createObjectURL(csvFile2);


            // Hide download link
            downloadLink.style.display = "none";
            downloadLink2.style.display = "none";



            // Add the link to DOM
            document.body.appendChild(downloadLink);
            document.body.appendChild(downloadLink2);



            // Click download link
            downloadLink.click();
            downloadLink2.click();
            TOTVSPA();
        }


    </script>
</asp:Content>