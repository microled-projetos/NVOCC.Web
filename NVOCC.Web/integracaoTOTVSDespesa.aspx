<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTOTVSDespesa.aspx.cs" Inherits="ABAINFRA.Web.integracaoTOTVSDespesa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TOTVS Nota Despesa
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Integração TOTVS Nota Despesa
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                        <div class="alert alert-danger text-center" id="msgErrDespCli">
                                            Tabela Despesa CLI Vazia.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrDespRec">
                                            Tabela Despesa REC Vazia.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessDespCli">
                                            Tabela Despesa CLI exportada com sucesso.
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessDespRec">
                                            Tabela Despesa REC exportada com sucesso.
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
                                            <button type="button" id="btnExportTotusDespesa" class="btn btn-primary" onclick="exportTableToCSVDespesa('CLI_FCA.csv','RA_FCA.csv')">Exportar Grid - CSV</button>
                                        </div>
                                    </div>
                                    <div class="row flexdiv topMarg" style="padding: 0 15px">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Emissão Inicial:</label>
                                                <input id="txtDtEmissaoInicial" class="form-control" type="date" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Emissão Final:</label>
                                                <input id="txtDtEmissaoFinal" class="form-control" type="date" />
                                            </div>
                                        </div>
                                        <div style="font-size: 12px">
                                            <input type="radio" id="todos" name="exportDespesa" value="1" >
                                            <label for="venda">Todos os Registros</label><br>
                                            <input type="radio" id="naoExportados" name="exportDespesa" value="2" checked>
                                            <label for="compra">Registros Não Exportados</label><br>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Nº Nota Despesa</label>
                                                <input id="txtNotaDespesa" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultar" onclick="TOTVSNotaDespesa()" class="btn btn-primary">Consultar</button>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnLimparExportDespesa" style="margin-left: 10px;" onclick="LimparExportDespesa()" class="btn btn-primary">Zerar Exportação</button>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnMarcarDesmcarcar" style="margin-left: 10px;" onclick="MarcarDesmarcar()" class="btn btn-primary">Marcar Todos</button>
                                        </div>
                                    </div> 
                                    <div class="table-responsive tableFixHead topMarg">
                                        <table id="grdEstimativa" class="table tablecont">
                                            <thead>
                                                <tr>                                                
                                                    <th class="text-center" scope="col">&nbsp;</th>
                                                    <th class="text-center" scope="col">Nº Nota</th>
                                                    <th class="text-center" scope="col">Tipo Nota</th>
                                                    <th class="text-center" scope="col">Data Emissão</th>
                                                    <th class="text-center" scope="col">Valor</th>
                                                    <th class="text-center" scope="col">Nome da Empresa</th>
                                                    <th class="text-center" scope="col">Data Vencimento</th>
                                                    <th class="text-center" scope="col">Data Exportação</th>
                                                    <th class="text-center" scope="col">Referencia FCA</th>
                                                    <th class="text-center" scope="col">Referencia Cliente</th>
                                                </tr>
                                            </thead>
                                            <tbody id="grdEstimativaBody">

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
                document.getElementById("txtDtEmissaoInicial").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinal").value = formatDate(currentDate);
                break;
            case "6":
                yesterday.setDate(yesterday.getDate() - 1);
                document.getElementById("txtDtEmissaoInicial").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinal").value = formatDate(currentDate);
            default:
                document.getElementById("txtDtEmissaoInicial").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinal").value = formatDate(currentDate);
                break;
        }

        function LimparExportDespesa() {
            var exp = document.querySelectorAll("[name=export]:checked");
            values = [];
            var dataI = document.getElementById("txtDtEmissaoInicial").value;
            var dataF = document.getElementById("txtDtEmissaoFinal").value;
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            if (values.length > 0) {
                for (let i = 0; i < values.length; i++) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/ZerarExportTOTVSDespesa",
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
            var exporta = document.getElementById("todos");
            exporta.checked = false;
            var nexporta = document.getElementById("naoExportados");
            nexporta.checked;
            TOTVSNotaDespesa();
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

        function TOTVSNotaDespesa() {
            $("#modalDespesa").modal("show");

            var exporta = document.getElementById("todos");
            var dataI = document.getElementById("txtDtEmissaoInicial").value;
            var dataF = document.getElementById("txtDtEmissaoFinal").value;
            var nota = document.getElementById("txtNotaDespesa").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaDespesa",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota:"' + nota + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdEstimativaBody").empty();
                    $("#grdEstimativaBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdEstimativaBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdEstimativaBody").append("<tr><td class='text-center'><input type='checkbox' value='" + dado[i]["ID_CONTA_PAGAR_RECEBER"] + "' name='export' class='check'></td><td class='text-center'> " + dado[i]["NR_NOTA"] + "</td><td class='text-center'>" + dado[i]["TP_NOTA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td><td class='text-center'>" + dado[i]["VL_NOTA"] + "</td><td class='text-center'>" + dado[i]["NM_PARCEIRO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_VENCIMENTO"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_TOTVS_DESPESA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_REFERENCIA_CLIENTE"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdEstimativaBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function exportTableToCSVDespesa(clif, recf) {
            var exporta = document.getElementById("todos");
            var dataI = document.getElementById("txtDtEmissaoInicial").value;
            var dataF = document.getElementById("txtDtEmissaoFinal").value;
            var nota = document.getElementById("txtNotaDespesa").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaDespesa",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota:"' + nota + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        DespesaCLI(dataI, dataF, situacao, nota, clif, recf)
                    }
                    else {
                        $("#msgErrDespCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function DespesaCLI(dataI, dataF, situacao, nota, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaDespesaCLI",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var cli = [["A1_COD;A1_LOJA;A1_NOME;A1_NREDUZ;A1_PESSOA;A1_TIPO;A1_END;A1_EST;A1_COD_MUN;A1_MUN;A1_NATUREZ;A1_BAIRRO;A1_CEP;A1_ATIVIDA;A1_TEL;A1_TELEX;A1_FAX;A1_CONTATO;A1_CGC;A1_INSCR;A1_INSCRM;A1_CONTA;A1_RECISS;A1_CONT;A1_PAIS"]];
                        for (let i = 0; i < dado.length; i++) {
                            cli.push([dado[i]]);
                        }
                        DespesaREC(dataI, dataF, cli, situacao, nota, clif, recf)
                    }
                    else {
                        $("#msgErrDespCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function DespesaREC(dataI, dataF, cli, situacao, nota, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaDespesaREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var rec = [["E1_PREFIXO;E1_NUM;E1_PARCELA;E1_TIPO;E1_NATUREZ;E1_CLIENTE;E1_LOJA;E1_EMISSAO;E1_VENCTO;E1_VENCREA;E1_VALOR;E1_IRRF;E1_ISS;E1_HIST;E1_INSS;E1_COFINS;E1_CSLL;E1_PIS;E1_CONTROL;E1_ITEMCTA;E1_XPROD;E1_CONTA"]];
                        for (let i = 0; i < dado.length; i++) {
                            rec.push([dado[i]]);
                        }
                        updateContaPagarReceberDespesa(dataI, dataF, cli, rec, situacao, nota, clif, recf)
                    }
                    else {
                        $("#msgErrDespCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function updateContaPagarReceberDespesa(dataI, dataF, cli, rec, situacao, nota, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/integrarTOTVSDespesa",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota:"' + nota + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        downloadCSVDespesa(cli.join("\n"), rec.join("\n"), clif, recf);
                        $("#msgSuccessDespCli").fadeIn(500).delay(1000).fadeOut(500);
                        $("#msgSuccessDespRec").fadeIn(500).delay(1000).fadeOut(500);
                    }
                    else {
                        alert("erro");
                    }
                }
            })
        }

        function downloadCSVDespesa(cli, rec, clif, recf) {
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
            TOTVSNotaDespesa();
        }


    </script>
</asp:Content>