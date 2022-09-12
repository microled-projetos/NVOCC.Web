<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrevisibilidadeContas.aspx.cs" Inherits="ABAINFRA.Web.PrevisibilidadeContas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Relatório de Previsibilidade
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Previsibilidade Contas
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row">
                                    <div class="alert alert-danger text-center" id="msgErrExportContaPagaRecebida">
                                        Não há registros para a data informada.
                                    </div>
                                </div>
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Pagamento_Recebimento.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Inicial:</label>
                                            <input id="txtDtInicialPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Final:</label>
                                            <input id="txtDtFinalPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterPagamentoRecebimento" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Nr Processo</option>
                                                <option value="2">Parceiro</option>
                                                <option value="4">Nº BL Master</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtPagamentoRecebimento" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarPagamentoRecebimento" onclick="ContaPrevisibilidadeProcesso()" class="btn btn-primary">Consultar</button>
                                    </div>
                                    <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                        <div>
                                            <asp:CheckBox ID="chkConferidoSim" runat="server" CssClass="form-control noborder" Text="&nbsp;Conferido Sim"></asp:CheckBox>
                                        </div>
                                        <div>
                                            <asp:CheckBox ID="chkConferidoNao" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Conferido Não"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">DOC CONFERIDO HOUSE</th>
                                                <th class="text-center" scope="col">DOC CONFERIDO MASTER</th>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">MASTER</th>
                                                <th class="text-center" scope="col">HOUSE</th>
                                                <th class="text-center" scope="col">TIPO SERVICO</th>
                                                <th class="text-center" scope="col">TIPO ESTUFAGEM</th>
                                                <th class="text-center" scope="col">TIPO PAGAMENTO HOUSE</th>
                                                <th class="text-center" scope="col">TIPO PAGAMENTO MASTER</th>
                                                <th class="text-center" scope="col">CNTR 20</th>
                                                <th class="text-center" scope="col">CNTR 40</th>
                                                <th class="text-center" scope="col">ORIGEM</th>
                                                <th class="text-center" scope="col">DESTINO</th>
                                                <th class="text-center" scope="col">DATA EMBARQUE</th>
                                                <th class="text-center" scope="col">DATA PREVISÃO CHEGADA</th>
                                                <th class="text-center" scope="col">PARCEIRO</th>
                                                <th class="text-center" scope="col">CNEE</th>
                                                <th class="text-center" scope="col">INDICADOR</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">TIPO FATURAMENTO</th>
                                                <th class="text-center" scope="col">DIAS FATURADOS</th>
                                                <th class="text-center" scope="col">A RECEBER BRL</th>
                                                <th class="text-center" scope="col">A PAGAR BRL</th>
                                                <th class="text-center" scope="col">SALDO BRL</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdPagamentoRecebimentoBody">

                                        </tbody>
                                        <tfoot id="grdPagamentoRecebimentoFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
                                        </tfoot>
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
        var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();

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

        function ContaPrevisibilidadeProcesso() {
            var dtInicial = document.getElementById("txtDtInicialPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalPagamentoRecebimento").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            if (dtInicial == "" && dtFinal == "") {
                dtInicial = "1900-01-01";
                dtFinal = "2900-01-01";
            }
            var chkConferidoSim = document.getElementById("MainContent_chkConferidoSim");
            var chkConferidoSimValue;
            var chkConferidoNao = document.getElementById("MainContent_chkConferidoNao");
            var chkConferidoNaoValue;
            if (chkConferidoSim.checked) {
                chkConferidoSimValue = "1";
            }
            else {
                chkConferidoSimValue = "0";
            }

            if (chkConferidoNao.checked) {
                chkConferidoNaoValue = "1";
            }
            else {
                chkConferidoNaoValue = "0";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ContaPrevisibilidadeProcesso",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '", chkConfSim: "' + chkConferidoSimValue + '", chkConfNao: "' + chkConferidoNaoValue +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdPagamentoRecebimentoBody").empty();
                    $("#grdPagamentoRecebimentoBody").append("<tr><td colspan='25'><div class='loader'></div></td></tr>");
                    $("#grdPagamentoRecebimentoFooter").empty();
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdPagamentoRecebimentoBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdPagamentoRecebimentoBody").append("<tr><td class='text-center'>" + dado[i]["DOC_CONFERIDO_HOUSE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DOC_CONFERIDO_MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["HOUSE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TPSERVICO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TPESTUFAGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TPPAGAMENTOHOUSE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TPPAGAMENTOMASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CNTR20"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CNTR40"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ORIGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DTEMBARQUE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DTPREVISAOCHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PARCEIRO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CNEE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["INDICADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["AGENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["TIPO_FATURAMENTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DIAS_FATURADOS"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ARECEBERBR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["APAGARBR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["SALDOBR"] + "</td></tr > ");
                        }
                    } else {
                        $("#grdPagamentoRecebimentoBody").empty();
                        $("#grdPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='25' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function exportContaConferenciaProcesso(file) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ContaConferenciaProcesso",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var confProcesso = [["PROCESSO;PROCEDENCIA;ITEM;VALOR BRL;ESTUFAGEM MASTER;ESTUFAGEM HOUSE;PAGAMENTO MASTER;PAGAMENTO HOUSE;PAGAMENTO TAXA;ORIGEM;DECLARADO;FREEHAND;STATUS FRETE;DIVISAO PROFIT"]];
                        for (let i = 0; i < dado.length; i++) {
                            confProcesso.push([dado[i]]);
                        }
                        exportContaConferenciaProcessoCSV(file, confProcesso.join("\n"));
                    }
                }
            })
        }

        function exportRelatorioPagamentosRecebimentosCSV(file, array) {
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

        function exportContaPrevisibilidadeProcessoCSV(file, array)  {
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

        function exportContaConferenciaProcessoCSV(file, array) {
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

        function exportCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdPagamentoRecebimento td, #grdPagamentoRecebimento th");

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



        function EstimativaPagamentosRecebimentos() {
            $("#modalEstimativaPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var origem = document.getElementById("ddlFilterEstimativaPagamentoRecebimentoOrigem").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            var chkConferidoSim = document.getElementById("MainContent_chkConferidoSim");
            var chkConferidoSimValue;
            var chkConferidoNao = document.getElementById("MainContent_chkConferidoNao");
            var chkConferidoNaoValue;
            if (chkConferidoSim.checked) {
                chkConferidoSimValue = "1";
            }
            else {
                chkConferidoSimValue = "0";
            }

            if (chkConferidoNao.checked) {
                chkConferidoNaoValue = "1";
            }
            else {
                chkConferidoNaoValue = "0";
            }
            if (dtInicial == "" && dtFinal == "") {
                dtInicial = "1900-01-01";
                dtFinal = "2900-01-01";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarContasAReceberAPagar",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '", origem: "' + origem + '", chkConfSim: "' + chkConferidoSimValue + '", chkConfNao: "' + chkConferidoNaoValue +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdEstimativaPagamentoRecebimentoBody").empty();
                    $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td colspan='18'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    var liqrec = 0;
                    var liqpag = 0;
                    $("#grdEstimativaPagamentoRecebimentoBody").empty();
                    $("#grdEstimativaPagamentoRecebimentoFooter").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td class='text-center'>" + dado[i]["DOC_CONFERIDO_HOUSE"] + "</td><td class='text-center'>" + dado[i]["DOC_CONFERIDO_MASTER"] + "</td><td class='text-center'>" + dado[i]["PROCESSO"] + "</td><td class='text-center'> " + dado[i]["MBL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["SERVICO"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td><td class='text-center' style='max-width: 15ch;' title='" + dado[i]["CLIENTE"] + "'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["DEVIDO_REC"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["MOEDA_REC"] + "</td><td class='text-center'>" + dado[i]["CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["RECEBER"] + "</td>" +
                                "<td class='text-center' style='max-width: 15ch;' title='" + dado[i]["FORNECEDOR"] + "'>" + dado[i]["FORNECEDOR"] + "</td><td class='text-center'>" + dado[i]["DEVIDO_PAG"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["MOEDA_PAG"] + "</td><td class='text-center'>" + dado[i]["CAMBIO_PAGAR"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["PAGAR"] + "</td></tr>");
                            if (dado[i]["RECEBER"] != "") {
                                liqrec = parseFloat(liqrec) + parseFloat(dado[i]["RECEBER"]);
                            }

                            if (dado[i]["PAGAR"] != "") {
                                liqpag = parseFloat(liqpag) + parseFloat(dado[i]["PAGAR"]);
                            }
                        }
                        $("#grdEstimativaPagamentoRecebimentoFooter").append("<tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th class='text-center'>" + liqrec.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</th><th></th><th></th><th></th><th></th><th></th><th class='text-center'>" + liqpag.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</th></tr>")
                    }
                    else {
                        $("#grdEstimativaPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='18' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function exportEstimativaCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdEstimativaPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdEstimativaPagamentoRecebimento td, #grdEstimativaPagamentoRecebimento th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVEstimativaPagamentosRecebimentos(csv.join("\n"), filename);
        }

        function exportTableToCSVEstimativaPagamentosRecebimentos(csv, filename) {
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
    </script>
</asp:Content>