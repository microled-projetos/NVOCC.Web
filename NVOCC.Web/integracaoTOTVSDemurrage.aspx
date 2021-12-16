<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTOTVSDemurrage.aspx.cs" Inherits="ABAINFRA.Web.integracaoTOTVSDemurrage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TOTVS Demurrage
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Integração TOTVS Demurrage
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
                                        <button type="button" id="btnExportTotusDemurrage" class="btn btn-primary" onclick="exportTableToCSVDemurrage('DEMURRAGE_PA_FORNEC.csv','DEMURRAGE_PA_REC.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Pagamento Inicial:</label>
                                            <input id="txtDtEmissaoInicialDemurrage" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Pagamento Final:</label>
                                            <input id="txtDtEmissaoFinalDemurrage" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterDemu" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Cliente</option>
                                                <option value="3">Fornecedor</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Nº Nota Despesa</label>
                                            <input id="txtNotaDemurrage" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div style="font-size: 12px">
                                        <input type="radio" id="todosDemurrage" name="exportDemurrage" value="1" >
                                        <label for="venda">Todos os Registros</label><br>
                                        <input type="radio" id="naoExportadosDemurrage" name="exportDemurrage" value="2" checked>
                                        <label for="compra">Registros Não Exportados</label><br>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" style="margin-left: 10px;" id="btnConsultarCredito" onclick="TOTVSDemurrage()" class="btn btn-primary">Consultar</button>
                                    </div>
                          
                                    <div class="form-group">
                                        <button type="button" id="btnMarcarDesmcarcar" style="margin-left: 10px;" onclick="MarcarDesmarcar()" class="btn btn-primary">Marcar Todos</button>
                                    </div>
                                </div> 
                                <div class="table-responsive tableFixHead topMarg">
                                    <table id="grdDemurrage" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">&nbsp;</th>
                                                <th class="text-center" scope="col">Data Pagamento</th>
                                                <th class="text-center" scope="col">Nº Processo</th>
                                                <th class="text-center" scope="col">Fornecedor</th>
                                                <th class="text-center" scope="col">Data Emissão</th>
                                                <th class="text-center" scope="col">Data Exportação</th>
                                                <th class="text-center" scope="col">Cliente</th>
                                                <th class="text-center" scope="col">Item Despesa</th>
                                                <th class="text-center" scope="col">Valor Liquido</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdDemurrageBody">

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
                document.getElementById("txtDtEmissaoInicialDemurrage").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalDemurrage").value = formatDate(currentDate);
                break;
            case "6":
                yesterday.setDate(yesterday.getDate() - 1);
                document.getElementById("txtDtEmissaoInicialDemurrage").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalDemurrage").value = formatDate(currentDate);
            default:
                document.getElementById("txtDtEmissaoInicialDemurrage").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalDemurrage").value = formatDate(currentDate);
                break;
        }

        function LimparExportDemurrage() {
            var exp = document.querySelectorAll("[name=export]:checked");
            values = [];
            var dataI = document.getElementById("txtDtEmissaoInicialDemurrage").value;
            var dataF = document.getElementById("txtDtEmissaoFinalDemurrage").value;
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            if (values.length > 0) {
                for (let i = 0; i < values.length; i++) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/ZerarExportTOTVSDemurrage",
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
            var exporta = document.getElementById("todosDemurrage");
            exporta.checked = false;
            var nexporta = document.getElementById("naoExportadosDemurrage");
            nexporta.checked;
            TOTVSDemurrage();
        }

        function TOTVSDemurrage() {
            $("#modalInvCredit").modal("show");
            var exporta = document.getElementById("todosDemurrage");
            var filter = document.getElementById("ddlFilterDemu").value;
            var dataI = document.getElementById("txtDtEmissaoInicialDemurrage").value;
            var dataF = document.getElementById("txtDtEmissaoFinalDemurrage").value;
            var nota = document.getElementById("txtNotaDemurrage").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSDemurrage",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "'+filter+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdDemurrageBody").empty();
                    $("#grdDemurrageBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdDemurrageBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdDemurrageBody").append("<tr><td class='text-center'><input type='checkbox' value='" + dado[i]["ID_CONTA_PAGAR_RECEBER"] + "' name='export' class='check'></td><td class='text-center'> " + dado[i]["DT_PAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_FORNECEDOR"] + "</td><td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_CLIENTE"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO"] + "</td>" +
                                "</tr> ");
                        }
                    }
                    else {
                        $("#grdDemurrageBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
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

        function exportTableToCSVDemurrage(clif, recf) {
            var dataI = document.getElementById("txtDtEmissaoInicialDemurrage").value;
            var dataF = document.getElementById("txtDtEmissaoFinalDemurrage").value;
            var filter = document.getElementById("ddlFilterDemu").value;
            var exporta = document.getElementById("todosDemurrage");
            var nota = document.getElementById("txtNotaDemurrage").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSDemurrage",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        DemurrageCLI(dataI, dataF, situacao, nota, filter, clif, recf);
                    }
                    else {
                        $("#msgErrExportCreditCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })




            // Download CSV file*/

        }

        function DemurrageCLI(dataI, dataF, situacao, nota, filter, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSDemurragePAFORNEC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var fornec = [["A2_XGRUPO;A2_COD;A2_LOJA;A2_NOME;A2_NREDUZ;A2_END;A2_BAIRRO;A2_EST;A2_COD_MUN;A2_CEP;A2_TIPO;A2_CGC;A2_TEL;A2_INSCR;A2_INSCRM;A2_EMAIL;A2_DDD;A2_NATUREZ;A2_CODPAIS;A2_CONTATO;A2_SIMPNAC"]];
                        for (let i = 0; i < dado.length; i++) {
                            fornec.push([dado[i]]);
                        }
                        DemurrageREC(dataI, dataF, situacao, nota, filter, fornec, clif, recf);
                    }
                    else {
                        $("#msgErrExportCreditCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function DemurrageREC(dataI, dataF, situacao, nota, filter, cli, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSDemurragePAREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var rec = [["E2_FILIAL;E2_PREFIXO;E2_NUM;E2_PARCELA;E2_TIPO;E2_FORNECE;E2_LOJA;E2_NATUREZ;E2_EMISSAO;E2_VENCTO;E2_VENCREA;E2_VALOR;E2_HIST;E2_ITEMCTA;E2_USERS;E2_XPROD"]];
                        for (let i = 0; i < dado.length; i++) {
                            rec.push([dado[i]]);
                        }
                        updateContaPagarReceberDemurrage(dataI, dataF, situacao, nota, filter, cli, rec, clif, recf);
                    }
                    else {
                        $("#msgErrExportCreditREC").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function updateContaPagarReceberDemurrage(dataI, dataF, situacao, nota, filter, cli, rec, clif, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/integrarTOTVSDemurragePA",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '",filter:"' + filter+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        downloadCSVDemurrage(cli.join("\n"), rec.join("\n"), clif, recf)
                        alert("sucesso");
                    } else {
                        alert("erro");
                    }
                }
            });
        }

        function downloadCSVDemurrage(cli, rec, clif, recf) {
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
            TOTVSDemurrage();
        }


    </script>
</asp:Content>
