﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTOTVSCredit.aspx.cs" Inherits="ABAINFRA.Web.integracaoTOTVSCredit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TOTVS INV. Credit
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Integração TOTVS INV Credit
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    <div class="alert alert-danger text-center" id="msgErrExportCreditCli">
                                        Não há dados para exportar CLI
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrExportCreditREC">
                                        Não há dados para exportar REC
                                    </div>
                                        <div class="alert alert-success text-center" id="msgSuccessCreditCli">
                                        Tabela Credit CLI exportada com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessCreditRec">
                                        Tabela Credit REC exportada com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessExportDelete">
                                        Data exportação zerada.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrorExportDelete">
                                        Erro ao zerar Data exportação.
                                    </div>
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportTotusCredit" class="btn btn-primary" onclick="exportTableToCSVCredit('CLI_FCA.csv','REC_FCA.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Pagamento Inicial:</label>
                                            <input id="txtDtEmissaoInicialCredit" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Pagamento Final:</label>
                                            <input id="txtDtEmissaoFinalCredit" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Nº Nota Despesa</label>
                                            <input id="txtNotaCredit" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div style="font-size: 12px">
                                        <input type="radio" id="todosCredit" name="exportCredit" value="1" >
                                        <label for="venda">Todos os Registros</label><br>
                                        <input type="radio" id="naoExportadosCredit" name="exportCredit" value="2" checked>
                                        <label for="compra">Registros Não Exportados</label><br>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" style="margin-left: 10px;" id="btnConsultarCredito" onclick="TOTVSInvCredit()" class="btn btn-primary">Consultar</button>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnLimparExportCredit" style="margin-left: 10px;" onclick="LimparExportCredit()" class="btn btn-primary">Zerar Exportação</button>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnMarcarDesmcarcar" style="margin-left: 10px;" onclick="MarcarDesmarcar()" class="btn btn-primary">Marcar Todos</button>
                                    </div>
                                </div> 
                                <div class="table-responsive tableFixHead topMarg">
                                    <table id="grdCredit" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">&nbsp;</th>
                                                <th class="text-center" scope="col">Nº Documento</th>
                                                <th class="text-center" scope="col">Tipo</th>
                                                <th class="text-center" scope="col">Data Pagamento</th>
                                                <th class="text-center" scope="col">Valor</th>
                                                <th class="text-center" scope="col">Nome da Empresa</th>
                                                <th class="text-center" scope="col">Data Vencimento</th>
                                                <th class="text-center" scope="col">Data Exportação</th>
                                                <th class="text-center" scope="col">Referencia FCA</th>
                                                <th class="text-center" scope="col">Referencia Cliente</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdCreditBody">

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
        /*var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();*/

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
                document.getElementById("txtDtEmissaoInicialCredit").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalCredit").value = formatDate(currentDate);
                break;
            case "6":
                yesterday.setDate(yesterday.getDate() - 1);
                document.getElementById("txtDtEmissaoInicialCredit").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalCredit").value = formatDate(currentDate);
            default:
                document.getElementById("txtDtEmissaoInicialCredit").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalCredit").value = formatDate(currentDate);
                break;
        }

        function LimparExportCredit() {
            var exp = document.querySelectorAll("[name=export]:checked");
            values = [];
            var dataI = document.getElementById("txtDtEmissaoInicialCredit").value;
            var dataF = document.getElementById("txtDtEmissaoFinalCredit").value;
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            if (values.length > 0) {
                for (let i = 0; i < values.length; i++) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/ZerarExportTOTVSCredit",
                        data: '{ dataI: "' + dataI + '", dataF: "' + dataF + '", value: "' + values[i] + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            if (dado.d == "ok") {
                                $("#msgSuccessExportDelete").fadeIn(500).delay(1000).fadeOut(500);
                            } else {
                                $("#msgErrorExportDelete").fadeIn(500).delay(1000).fadeOut(500);
                            }
                        }, error: function () {
                            $("#msgErrorExportDelete").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    })
                }
            }
            var exporta = document.getElementById("todosCredit");
            exporta.checked = false;
            var nexporta = document.getElementById("naoExportadosCredit");
            nexporta.checked;
            TOTVSInvCredit();
        }

        function TOTVSInvCredit() {
            $("#modalInvCredit").modal("show");
            var exporta = document.getElementById("todosCredit");
            var dataI = document.getElementById("txtDtEmissaoInicialCredit").value;
            var dataF = document.getElementById("txtDtEmissaoFinalCredit").value;
            var nota = document.getElementById("txtNotaCredit").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvCredit",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdCreditBody").empty();
                    $("#grdCreditBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdCreditBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdCreditBody").append("<tr><td class='text-center'><input type='checkbox' value='" + dado[i]["ID_CONTA_PAGAR_RECEBER"] + "' name='export' class='check'></td><td class='text-center'> " + dado[i]["NR_CONTRATO"] + "</td><td class='text-center'>" + dado[i]["TP_CONTRATO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_PAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO"] + "</td><td class='text-center'>" + dado[i]["NM_PARCEIRO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_VENCIMENTO"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_REFERENCIA_CLIENTE"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdCreditBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function MarcarDesmarcar() {
            $(".check").each(
                function () {
                    if ($(this).prop("checked")) {
                        $(this).prop("checked", false);
                    } else {
                        $(this).prop("checked", true);
                    }
                }
            )
        }

        function exportTableToCSVCredit(clif, recf) {
            var dataI = document.getElementById("txtDtEmissaoInicialCredit").value;
            var dataF = document.getElementById("txtDtEmissaoFinalCredit").value;
            var exporta = document.getElementById("todosCredit");
            var nota = document.getElementById("txtNotaCredit").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvCredit",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        CreditCLI(dataI, dataF, situacao, nota, clif, recf);
                    }
                    else {
                        $("#msgErrExportCreditCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })




            // Download CSV file*/

        }

        function CreditCLI(dataI, dataF, situacao, nota, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvCreditCLI",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var cli = [["A1_COD;A1_LOJA;A1_NOME;A1_NREDUZ;A1_PESSOA;A1_TIPO;A1_END;A1_EST;A1_COD_MUN;A1_MUN;A1_NATUREZ;A1_BAIRRO;A1_CEP;A1_ATIVIDA;A1_TEL;A1_TELEX;A1_FAX;A1_CONTATO;A1_CGC;A1_INSCR;A1_INSCRM;A1_CONTA;A1_RECISS;A1_CONT"]];
                        for (let i = 0; i < dado.length; i++) {
                            cli.push([dado[i]]);
                        }
                        CreditREC(dataI, dataF, situacao, nota, cli, clif, recf);
                    }
                    else {
                        $("#msgErrExportCreditCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function CreditREC(dataI, dataF, situacao, nota, cli, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvCreditREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var rec = [["E1_PREFIXO;E1_NUM;E1_PARCELA;E1_TIPO;E1_NATUREZ;E1_CLIENTE;E1_LOJA;E1_EMISSAO;E1_VENCTO;E1_VENCREA;E1_VALOR;E1_IRRF;E1_ISS;E1_HIST;E1_INSS;E1_COFINS;E1_CSLL;E1_PIS;E1_CONTROL;E1_ITEMCTA;E1_XPROD;"]];
                        for (let i = 0; i < dado.length; i++) {
                            rec.push([dado[i]]);
                        }
                        updateContaPagarReceberCredit(dataI, dataF, situacao, nota, cli, rec, clif, recf);
                    }
                    else {
                        $("#msgErrExportCreditREC").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function updateContaPagarReceberCredit(dataI, dataF, situacao, nota, cli, rec, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/integrarTOTVSCredit",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        downloadCSVCredit(cli.join("\n"), rec.join("\n"), clif, recf)
                        alert("sucesso");
                    } else {
                        alert("erro");
                    }
                }
            });
        }

        function downloadCSVCredit(cli, rec, clif, recf) {
            var csvFile;
            var csvFile2;

            var downloadLink;
            var downloadLink2;


            // CSV file
            csvFile = new Blob(["\uFEFF" + cli], { type: "text/csv;charset=utf-8;" });
            csvFile2 = new Blob(["\uFEFF" + rec], { type: "text/csv;charset=utf-8;" });

            // Download link
            downloadLink = document.createElement("a");
            downloadLink2 = document.createElement("a");


            // File name
            downloadLink.download = clif;
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
            TOTVSInvCredit();
        }


    </script>
</asp:Content>
