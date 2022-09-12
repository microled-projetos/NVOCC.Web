<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTOTVSServico.aspx.cs" Inherits="ABAINFRA.Web.integracaoTOTVSServico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TOTVS Nota Serviço
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Integração TOTVS Nota Serviço
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    <div class="alert alert-danger text-center" id="msgErrServCli">
                                        Tabela Serviço CLI Vazia.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrServNota">
                                        Tabela Serviço NOTA Vazia.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrServNotite">
                                        Tabela Serviço NOTITE Vazia.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrServRec">
                                        Tabela Serviço REC Vazia.
                                    </div>
                                        <div class="alert alert-success text-center" id="msgSuccessServCli">
                                        Tabela Serviço CLI exportada com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessServNota">
                                        Tabela Serviço NOTA exportada com sucesso.
                                    </div>
                                        <div class="alert alert-success text-center" id="msgSuccessServNotite">
                                        Tabela Serviço NOTITE exportada com sucesso.
                                    </div>
                                    <div class="alert alert-success text-center" id="msgSuccessServREC">
                                        Tabela Serviço REC exportada com sucesso.
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
                                        <button type="button" id="btnExportTotusServ" class="btn btn-primary" onclick="exportTableToCSVServico('CLI_FCA.csv','NOTA_FCA.csv','NOTITE_FCA.csv','REC_FCA.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Emissão Inicial:</label>
                                            <input id="txtDtEmissaoInicialServ" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Emissão Final:</label>
                                            <input id="txtDtEmissaoFinalServ" class="form-control" type="date" />
                                        </div>
                                    </div>
                                    <div style="font-size: 12px">
                                        <input type="radio" id="todosServ" name="exportServ" value="1" >
                                        <label for="venda">Todos os Registros</label><br>
                                        <input type="radio" id="naoExportadosServ" name="exportServ" value="2" checked>
                                        <label for="compra">Registros Não Exportados</label><br>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Nº Nota Servico Inicial</label>
                                            <input id="txtNotaServicoInicio" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Nº Nota Servico Final</label>
                                            <input id="txtNotaServicoFim" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarServico" style="margin-left: 10px" onclick="TOTVSNotaServico()" class="btn btn-primary">Consultar</button>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnLimparExportServico" style="margin-left: 10px;" onclick="LimparExportServico()" class="btn btn-primary">Zerar Exportação</button>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnMarcarDesmcarcar" style="margin-left: 10px;" onclick="MarcarDesmarcar()" class="btn btn-primary">Marcar Todos</button>
                                    </div>
                                </div> 
                                <div class="table-responsive tableFixHead topMarg">
                                    <table id="grdServico" class="table tablecont">
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
                                        <tbody id="grdServicoBody">

                                        </tbody>
                                    </table>
                                </div>
                                <div id="ntFiscal" style="margin-left: 20px;margin-top: 20px;">
                                   
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
                document.getElementById("txtDtEmissaoInicialServ").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalServ").value = formatDate(currentDate);
                break;
            case "6":
                yesterday.setDate(yesterday.getDate() - 1);
                document.getElementById("txtDtEmissaoInicialServ").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalServ").value = formatDate(currentDate);
            default:
                document.getElementById("txtDtEmissaoInicialServ").value = formatDate(yesterday);
                document.getElementById("txtDtEmissaoFinalServ").value = formatDate(currentDate);
                break;
        }

        function LimparExportServico() {
            var exp = document.querySelectorAll("[name=export]:checked");
            values = [];
            var dataI = document.getElementById("txtDtEmissaoInicialServ").value;
            var dataF = document.getElementById("txtDtEmissaoFinalServ").value;
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            if (values.length > 0) {
                for (let i = 0; i < values.length; i++) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/ZerarExportTOTVSServico",
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
            var exporta = document.getElementById("todosServ");
            exporta.checked = false;
            var nexporta = document.getElementById("naoExportadosServ");
            nexporta.checked;
            TOTVSNotaServico();
        }

        function TOTVSNotaServico() {
            $("#modalServico").modal("show");
            var notai = document.getElementById("txtNotaServicoInicio").value;
            var notafi = document.getElementById("txtNotaServicoFim").value;
            var exporta = document.getElementById("todosServ");
            var dataI = document.getElementById("txtDtEmissaoInicialServ").value;
            var dataF = document.getElementById("txtDtEmissaoFinalServ").value;
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServico",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", notai: "' + notai + '", notaf: "' + notafi + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdServicoBody").empty();
                    $("#grdServicoBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                    $("#ntFiscal").empty();
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdServicoBody").empty();
                    if (dado != null) 
					{$("#ntFiscal").append("<span style='background-color: bisque;padding: 10px;'><b>" + dado.length + "</b> Notas Fiscais</span>");
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdServicoBody").append("<tr><td class='text-center'><input type='checkbox' value='" + dado[i]["ID_CONTA_PAGAR_RECEBER"] + "' name='export' class='check'></td><td class='text-center'> " + dado[i]["NR_NOTA"] + "</td><td class='text-center'>" + dado[i]["TP_NOTA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td><td class='text-center'>" + dado[i]["VL_NOTA"] + "</td><td class='text-center'>" + dado[i]["NM_PARCEIRO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_VENCIMENTO"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_TOTVS_SERVICO"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_REFERENCIA_CLIENTE"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdServicoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
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

        function exportTableToCSVServico(clif, notaf, notitef, recf) {
            var notai = document.getElementById("txtNotaServicoInicio").value;
            var notafi = document.getElementById("txtNotaServicoFim").value;
            var dataI = document.getElementById("txtDtEmissaoInicialServ").value;
            var dataF = document.getElementById("txtDtEmissaoFinalServ").value;
            var exporta = document.getElementById("todosServ");
            var exp = document.querySelectorAll("[name=export]:checked");
            console.log(notaf);
            console.log(notitef);
            values = [];
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            for (let x = 0; x < values.length; x++) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarTOTVSNotaServico",
                    data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", values:"' + values[x] + '",notai: "' + notai + '", notaf: "' + notafi +'" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            if (x == values.length - 1) {
                                ServicoCliF(dataI, dataF, situacao, clif, notaf, notitef, recf);
                            }
                        }
                        else {
                            $("#msgErrServCli").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
            // Download CSV file*/

        }

        function ServicoCliF(dataI, dataF, situacao, clif, notaf, notitef, recf) {
            var exp = document.querySelectorAll("[name=export]:checked");
            console.log(notaf);
            console.log(notitef);
            var cli = [["A1_COD;A1_LOJA;A1_NOME;A1_NREDUZ;A1_PESSOA;A1_TIPO;A1_END;A1_EST;A1_COD_MUN;A1_MUN;A1_NATUREZ;A1_BAIRRO;A1_CEP;A1_ATIVIDA;A1_TEL;A1_TELEX;A1_FAX;A1_CONTATO;A1_CGC;A1_INSCR;A1_INSCRM;A1_CONTA;A1_RECISS;A1_CONT;A1_PAIS"]];
            values = [];
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServicoCLI",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao + '", values:"' + values +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            cli.push([dado[i]]);
                        }
                            ServicoNotaF(dataI, dataF, situacao, cli, clif, notaf, notitef, recf);
                    }
                    else {
                        $("#msgErrServCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function ServicoNotaF(dataI, dataF, situacao, cli, clif, notaf, notitef, recf) {
            var exp = document.querySelectorAll("[name=export]:checked");
            console.log(notaf);
            console.log(notitef);
            var nota = [["F2_DOC;F2_SERIE;F2_CLIENTE;F2_LOJA;F2_COND;F2_DUPL;F2_EMISSAO;F2_EST;F2_FRETE;F2_SEGURO;F2_TIPOCLI;F2_VALBRUT;F2_VALIPI;F2_BASEIPI;F2_VALMERC;F2_NFORI;F2_SERIORI;F2_TIPO;F2_ESPEC1;F2_VOLUME1;F2_ICMSRET;F2_PLIQUI;F2_PBRUTO;F2_TRANSP;F2_FILIAL;F2_BASEISS;F2_VALISS;F2_VALFAT;F2_ESPECIE;F2_PREFIXO;F2_BASIMP5;F2_BASIMP6;F2_VALIMP5;F2_VALIMP6;F2_VALINSS;F2_HORA;F2_BASEINS;F2_MOEDA;F2_VALCOFI;F2_VALCSLL;F2_VALPIS;F2_DTDIGIT;F2_RECISS;F2_NFELETR;F2_EMINFE;F2_CREDNFE;F2_CODNFE;F2_TPNFEXP;F2_CLIENT;F2_LOJENT;F2_CHVNFE;F2_TPFRETE;F2_HORNFE;F2_XCNPJ;"]];
            values = [];
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            for (let x = 0; x < values.length; x++) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarTOTVSNotaServicoNOTA",
                    data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao + '", values:"' + values[x] +'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                nota.push([dado[i]]);
                            }
                            if (x == values.length - 1) {
                                console.log(nota);
                                ServicoNotiteF(dataI, dataF, situacao, cli, nota, clif, notaf, notitef, recf);
                            }
                        }
                        else {
                            $("#msgErrServNota").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function ServicoNotiteF(dataI, dataF, situacao, cli, nota, clif, notaf, notitef, recf) {
            var exp = document.querySelectorAll("[name=export]:checked");
            console.log(notaf);
            console.log(notitef);
            var notite = [["D2_FILIAL;D2_COD;D2_UM;D2_SEGUM;D2_QUANT;D2_PRCVEN;D2_TOTAL;D2_VALIPI;D2_VALICM;D2_TES;D2_CF;D2_IPI;D2_PESO;D2_CONTA;D2_PEDIDO;D2_ITEMPV;D2_CLIENTE;D2_LOJA;D2_LOCAL;D2_DOC;D2_EMISSAO;D2_GRUPO;D2_TP;D2_SERIE;D2_CUSTO1;D2_EST;D2_TIPO;D2_NFORI;D2_SERIORI;D2_QTDEDEV;D2_ITEM;D2_CODISS;D2_CLASFIS;D2_BASIMP5;D2_BASIMP6;D2_VALIMP5;D2_VALIMP6;D2_ITEMORI;D2_ALIQINS;D2_ALIQISS;D2_BASEINS;D2_BASEIPI;D2_BASEISS;D2_CCUSTO;D2_ITEMCC;D2_ALQIMP5;D2_DTDIGIT;D2_VALISS;D2_ALQIMP6;"]];
            values = [];
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            for (let x = 0; x < values.length; x++) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarTOTVSNotaServicoNOTITE",
                    data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao + '", values:"' + values[x] +'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                notite.push([dado[i]]);
                            }
                            if (x == values.length - 1) {
                                console.log(notite);
                                ServicoRECF(dataI, dataF, situacao, cli, nota, notite, clif, notaf, notitef, recf);
                            }
                        }
                        else {
                            $("#msgErrServNotite").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function ServicoRECF(dataI, dataF, situacao, cli, nota, notite, clif, notaf, notitef, recf) {
            var exp = document.querySelectorAll("[name=export]:checked");
            console.log(notaf);
            console.log(notitef);
            var rec = [["E1_PREFIXO;E1_NUM;E1_PARCELA;E1_TIPO;E1_NATUREZ;E1_CLIENTE;E1_LOJA;E1_EMISSAO;E1_VENCTO;E1_VENCREA;E1_VALOR;E1_IRRF;E1_ISS;E1_HIST;E1_INSS;E1_COFINS;E1_CSLL;E1_PIS;E1_CONTROL;E1_ITEMCTA;E1_CONTA"]];
            values = [];
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            for (let x = 0; x < values.length; x++) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarTOTVSNotaServicoREC",
                    data: '{dataI:"' + dataI + '",dataF:"' + dataF + '", situacao: "' + situacao + '", values:"' + values[x] +'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) { 
                            for (let i = 0; i < dado.length; i++) {
                                rec.push([dado[i]]);
                            }
                            if (x == values.length -1) {
                                console.log(rec);
                                updateContaPagarReceberServico(dataI, dataF, situacao, cli, nota, notite, rec, clif, notaf, notitef, recf);
                            }
                            


                        }
                        else {
                            $("#msgErrServRec").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function updateContaPagarReceberServico(dataI, dataF, situacao, cli, nota, notite, rec, clif, notaf, notitef, recf) {
            var dataI = document.getElementById("txtDtEmissaoInicialServ").value;
            var dataF = document.getElementById("txtDtEmissaoFinalServ").value;
            var exp = document.querySelectorAll("[name=export]:checked");
            console.log(notaf);
            console.log(notitef);
            values = [];
            for (let i = 0; i < exp.length; i++) {
                if (values.indexOf(exp[i].value) === -1) {
                    values.push(exp[i].value);
                }
            }
            var exporta = document.getElementById("todosServ");
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            for (let x = 0; x < values.length; x++) {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/integrarTOTVSServico",
                    data: '{ dataI: "'+dataI+'", dataF:"'+dataF+'", situacao:"'+situacao+'", dado:"'+values[x]+'" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado == "ok") {
                            if (x == values.length - 1) {
                                downloadCSVServico(cli.join("\n"), nota.join("\n"), notite.join("\n"), rec.join("\n"), clif, notaf, notitef, recf);
                                alert("sucesso");
                            }
                        } else {
                            alert("falha");
                        }
                    }
                });
            }
        }

        function downloadCSVServico(cli, nota, notite, rec, clif, notaf, notitef, recf) {
            console.log(notitef);
            console.log(notaf);
            var csvFile;
            var csvFile2;
            var csvFile3;
            var csvFile4;
            var downloadLink;
            var downloadLink2;
            var downloadLink3;
            var downloadLink4;

            // CSV file
            csvFile = new Blob(["\uFEFF" + cli], { type: "text/csv;charset=utf-8;" });
            csvFile2 = new Blob(["\uFEFF" + nota], { type: "text/csv;charset=utf-8;" });
            csvFile3 = new Blob(["\uFEFF" + notite], { type: "text/csv;charset=utf-8;" });
            csvFile4 = new Blob(["\uFEFF" + rec], { type: "text/csv;charset=utf-8;" });
            // Download link
            downloadLink = document.createElement("a");
            downloadLink2 = document.createElement("a");
            downloadLink3 = document.createElement("a");
            downloadLink4 = document.createElement("a");

            // File name
            downloadLink.download = clif;
            downloadLink2.download = notaf;
            downloadLink3.download = notitef;
            downloadLink4.download = recf;

            // Create a link to the file
            downloadLink.href = window.URL.createObjectURL(csvFile);
            downloadLink2.href = window.URL.createObjectURL(csvFile2);
            downloadLink3.href = window.URL.createObjectURL(csvFile3);
            downloadLink4.href = window.URL.createObjectURL(csvFile4);

            // Hide download link
            downloadLink.style.display = "none";
            downloadLink2.style.display = "none";
            downloadLink3.style.display = "none";
            downloadLink4.style.display = "none";


            // Add the link to DOM
            document.body.appendChild(downloadLink);
            document.body.appendChild(downloadLink2);
            document.body.appendChild(downloadLink3);
            document.body.appendChild(downloadLink4);


            // Click download link
            downloadLink.click();
            downloadLink2.click();
            downloadLink3.click();
            downloadLink4.click();
            TOTVSNotaServico();
        }


    </script>
</asp:Content>