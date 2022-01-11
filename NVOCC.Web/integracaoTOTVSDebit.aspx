<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTOTVSDebit.aspx.cs" Inherits="ABAINFRA.Web.integracaoTOTVSDebit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TOTVS INV. Debit
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Integração TOTVS INV Debit
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    <div class="alert alert-danger text-center" id="msgErrExportDebitCli">
                                        Não há dados para exportar CLI.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrExportDebitREC">
                                        Não há dados para exportar REC.
                                    </div>
                                        <div class="alert alert-success text-center" id="msgSuccessDebitCli">
                                        Tabela Debit CLI exportada com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessDebitRec">
                                        Tabela Debit REC exportada com sucesso.
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
                                        <button type="button" id="btnExportTotusDebit" class="btn btn-primary" onclick="exportTableToCSVDebit('FORNEC_FCA.csv','REC_PA_FCA.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Emissão Inicial:</label>
                                            <input id="txtDtEmissaoInicialDebit" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Emissão Final:</label>
                                            <input id="txtDtEmissaoFinalDebit" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterDebit" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Id Master</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtNotaDebit" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div style="font-size: 12px">
                                        <input type="radio" id="todosDebit" name="exportDebit" value="1" >
                                        <label for="venda">Todos os Registros</label><br>
                                        <input type="radio" id="naoExportadosDebit" name="exportDebit" value="2" checked>
                                        <label for="compra">Registros Não Exportados</label><br>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarDebito" style="margin-left: 10px;" onclick="TOTVSInvDebit()" class="btn btn-primary">Consultar</button>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnLimparExportDebit" style="margin-left: 10px;" onclick="LimparExportDebit()" class="btn btn-primary">Zerar Exportação</button>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnMarcarDesmcarcar" style="margin-left: 10px;" onclick="MarcarDesmarcar()" class="btn btn-primary">Marcar Todos</button>
                                    </div>
                                </div> 
                                <div class="table-responsive tableFixHead topMarg">
                                    <table id="grdDebit" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">&nbsp;</th>
                                                <th class="text-center" scope="col">Data Pagamento</th>
                                                <th class="text-center" scope="col">ID Master</th>
                                                <th class="text-center" scope="col">Nº Processo</th>
                                                <th class="text-center" scope="col">Forncedor</th>
                                                <th class="text-center" scope="col">Data Emissão</th>
                                                <th class="text-center" scope="col">Data Exportação</th>
                                                <th class="text-center" scope="col">Cliente</th>
                                                <th class="text-center" scope="col">Item</th>
                                                <th class="text-center" scope="col">Valor Item</th>
                                                <th class="text-center" scope="col">Nº Documento</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdDebitBody">

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
                document.getElementById("txtDtEmissaoInicialDebit").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalDebit").value = formatDate(currentDate);
                break;
            case "6":
                yesterday.setDate(yesterday.getDate() - 1);
                document.getElementById("txtDtEmissaoInicialDebit").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalDebit").value = formatDate(currentDate);
            default:
                document.getElementById("txtDtEmissaoInicialDebit").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalDebit").value = formatDate(currentDate);
                break;
        }

        function LimparExportDebit() {
            var exp = document.querySelectorAll("[name=export]:checked");
            values = [];
            var dataI = document.getElementById("txtDtEmissaoInicialDebit").value;
            var dataF = document.getElementById("txtDtEmissaoFinalDebit").value;
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            if (values.length > 0) {
                for (let i = 0; i < values.length; i++) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/ZerarExportTOTVSDebit",
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
            var exporta = document.getElementById("todosDebit");
            exporta.checked = false;
            var nexporta = document.getElementById("naoExportadosDebit");
            nexporta.checked;
            TOTVSInvDebit();
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

        function TOTVSInvDebit() {
            $("#modalInvDebit").modal("show");
            var exporta = document.getElementById("todosDebit");
            var dataI = document.getElementById("txtDtEmissaoInicialDebit").value;
            var dataF = document.getElementById("txtDtEmissaoFinalDebit").value;
            var nota = document.getElementById("txtNotaDebit").value;
            var filter = document.getElementById("ddlFilterDebit").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvDebit",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdDebitBody").empty();
                    $("#grdDebitBody").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdDebitBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdDebitBody").append("<tr><td class='text-center'><input type='checkbox' value='" + dado[i]["ID_CONTA_PAGAR_RECEBER"] + "' name='export' class='check'></td><td class='text-center'> " + dado[i]["DT_PAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["ID_BL_MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NM_FORNECEDOR"] + "</td><td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_LIQUIDO"] + "</td><td class='text-center'>" + dado[i]["NR_DOCUMENTO"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdDebitBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='10' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function exportTableToCSVDebit(fornecf, recf) {
            var exporta = document.getElementById("todosDebit");
            var dataI = document.getElementById("txtDtEmissaoInicialDebit").value;
            var dataF = document.getElementById("txtDtEmissaoFinalDebit").value;
            var nota = document.getElementById("txtNotaDebit").value;
            var filter = document.getElementById("ddlFilterDebit").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvDebit",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        DebiteFornec(dataI, dataF, situacao, nota, filter, fornecf, recf);
                    }
                    else {
                        $("#msgErrExportDebitCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })




            // Download CSV file*/

        }

        function DebiteFornec(dataI, dataF, situacao, nota, filter, fornecf, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvDebitFornec",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var fornec = [["A2_XGRUPO;A2_COD;A2_LOJA;A2_NOME;A2_NREDUZ;A2_END;A2_BAIRRO;A2_EST;A2_COD_MUN;A2_CEP;A2_TIPO;A2_CGC;A2_TEL;A2_INSCR;A2_INSCRM;A2_EMAIL;A2_DDD;A2_NATUREZ;A2_CODPAIS;A2_CONTATO;A2_SIMPNAC;"]];
                        for (let i = 0; i < dado.length; i++) {
                            fornec.push([dado[i]]);
                        }
                        DebitREC(dataI, dataF, situacao, nota, filter, fornec, fornecf, recf)
                    }
                    else {
                        $("#msgErrExportDebitCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function DebitREC(dataI, dataF, situacao, nota, filter, fornec, fornecf, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvDebitREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var rec = [["E2_FILIAL;E2_PREFIXO;E2_NUM;E2_PARCELA;E2_TIPO;E2_FORNECE;E2_LOJA;E2_NATUREZ;E2_EMISSAO;E2_VENCTO;E2_VENCREA;E2_VALOR;E2_HIST;E2_ITEMCTA;E2_USERS;E2_XPROD;E2_CONTAD"]];
                        for (let i = 0; i < dado.length; i++) {
                            rec.push([dado[i]]);
                        }
                        updateContaPagarReceberDebit(dataI, dataF, situacao, nota, filter, fornec, rec, fornecf, recf);
                    }
                    else {
                        $("#msgErrExportDebitREC").fadeIn(500).delay(1000).fadeOut(500);

                    }
                }
            })
        }

        function updateContaPagarReceberDebit(dataI, dataF, situacao, nota, filter, fornec, rec, fornecf, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/integrarTOTVSDebit",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        downloadCSVDebit(fornec.join("\n"), rec.join("\n"), fornecf, recf);
                        alert("sucesso");
                    } else {
                        alert("erro");
                    }
                }
            });
        }

        function downloadCSVDebit(fornec, rec, fornecf, recf) {
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
            TOTVSInvDebit();
        }


    </script>
</asp:Content>