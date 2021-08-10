<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="integracaoTotus.aspx.cs" Inherits="ABAINFRA.Web.integracaoTotus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading"> 
                    <h3 class="panel-title">Tabela Demurrage
                    </h3>
                </div>
                <div class="panel-body">
                    
                    <div class="functionBar">
                        <div class="oFunc">
                            <button type="button" id="btnDespesa" class="btn btn-primary" data-toggle="modal" onclick="TOTVSNotaDespesa()">Despesa</button> 
                            <button type="button" id="btnServiço" class="btn btn-primary" data-toggle="modal" onclick="TOTVSNotaServico()">Serviço</button> 
                            <button type="button" id="btnCredit" class="btn btn-primary" data-toggle="modal" onclick="TOTVSInvCredit()">Credit</button> 
                            <button type="button" id="btnDebit" class="btn btn-primary" data-toggle="modal" onclick="TOTVSInvDebit()">Debit</button> 
                            <button type="button" id="btnPA" class="btn btn-primary" data-toggle="modal" onclick="TOTVSPA()">PA</button> 
                            <button type="button" id="btnPagamentosRecebimentos" class="btn" data-toggle="modal" onclick="PagamentosRecebimentos()">Pagamentos e Recebimentos</button> 
                        </div>
                    </div>                    
                    <div class="modal fade bd-example-modal-xl" id="modalDespesa" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDespesaTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalServico" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalEstimativaTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalInvCredit" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalInvCreditTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalInvDebit" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalInvDebitTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalPA" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalPATitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalPagamentoRecebimento" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalPagamentoRecebimentoTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                        <div style="margin: auto">
                                            <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Pagamento_Recebimento.csv')">Exportar Grid - CSV</button>
                                            <button type="button" id="btnPrintPagamentoRecebimento" class="btn btn-primary" onclick="printPagamentosRecebimentos()">Imprimir</button>
                                        </div>
                                    </div>
                                    <div class="row flexdiv topMarg">
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
                                                    <option value="2">Cliente</option>
                                                    <option value="3">Fornecedor</option>
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
                                            <button type="button" id="btnConsultarPagamentoRecebimento" onclick="PagamentosRecebimentos()" class="btn btn-primary">Consultar</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">DATA (REC)</th>
                                                <th class="text-center" scope="col">CLIENTE (REC)</th>
                                                <th class="text-center" scope="col">DEVIDO (REC)</th>
                                                <th class="text-center" scope="col">MOEDA (REC)</th>
                                                <th class="text-center" scope="col">CAMBIO (REC)</th>
                                                <th class="text-center" scope="col">LIQUIDADO (REC)</th>
                                                <th class="text-center" scope="col">DATA (PAG)</th>
                                                <th class="text-center" scope="col">FORNECEDOR (PAG)</th>
                                                <th class="text-center" scope="col">DEVIDO (PAG)</th>
                                                <th class="text-center" scope="col">MOEDA (PAG)</th>
                                                <th class="text-center" scope="col">CAMBIO (PAG)</th>
                                                <th class="text-center" scope="col">LIQUIDADO (PAG)</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdPagamentoRecebimentoBody">

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
    <p>versão 01/07/2021 17:42</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script> 
</script>
    <script>
        var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();
        document.getElementById("txtDtEmissaoInicial").value = '2021-07-12';
        document.getElementById("txtDtEmissaoFinal").value = dataAtual = ano + '-' + mes + '-' + dia;

        document.getElementById("txtDtEmissaoInicialServ").value = '2021-07-12';
        document.getElementById("txtDtEmissaoFinalServ").value = dataAtual = ano + '-' + mes + '-' + dia;

        document.getElementById("txtDtEmissaoInicialCredit").value = '2021-07-12';
        document.getElementById("txtDtEmissaoFinalCredit").value = dataAtual = ano + '-' + mes + '-' + dia;

        document.getElementById("txtDtEmissaoInicialDebit").value = '2021-07-12';
        document.getElementById("txtDtEmissaoFinalDebit").value = dataAtual = ano + '-' + mes + '-' + dia;

        document.getElementById("txtDtEmissaoInicialPA").value = '2021-07-12';
        document.getElementById("txtDtEmissaoFinalPA").value = dataAtual = ano + '-' + mes + '-' + dia;


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
                            $("#grdEstimativaBody").append("<tr><td class='text-center'> " + dado[i]["NR_NOTA"] + "</td><td class='text-center'>" + dado[i]["TP_NOTA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td><td class='text-center'>" + dado[i]["VL_NOTA"] + "</td><td class='text-center'>" + dado[i]["NM_PARCEIRO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_VENCIMENTO"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO_TOTVS_DESPESA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_REFERENCIA_CLIENTE"] + "</td></tr> ");
                        }
                    }
                    else{
                        $("#grdEstimativaBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function TOTVSNotaServico() {
            $("#modalServico").modal("show");

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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdServicoBody").empty();
                    $("#grdServicoBody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdServicoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdServicoBody").append("<tr><td class='text-center'> " + dado[i]["NR_NOTA"] + "</td><td class='text-center'>" + dado[i]["TP_NOTA"] + "</td>" +
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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "'+ nota +'"}',
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
                            $("#grdCreditBody").append("<tr><td class='text-center'> " + dado[i]["NR_CONTRATO"] + "</td><td class='text-center'>" + dado[i]["TP_CONTRATO"] + "</td>" +
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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '", nota: "' + nota + '", filter: "' + filter+ '"}',
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
                            $("#grdDebitBody").append("<tr><td class='text-center'> " + dado[i]["DT_PAGAMENTO"] + "</td><td class='text-center'>" + dado[i]["ID_BL_MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NM_FORNECEDOR"] + "</td><td class='text-center'>" + dado[i]["DT_EMISSAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["VL_LIQUIDO"] + "</td><td class='text-center'>" + dado[i]["NR_DOCUMENTO"] + "</td></tr> ");
                        }
                    }
                    else {
                        $("#grdDebitBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
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
                    $("#grdPABody").append("<tr><td colspan='9'><div class='loader'></div></td></tr>");
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
                        $("#grdPABody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='9' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                    }
                }
            })
        }

        function consistir() {
            var linha = document.querySelectorAll("#grdEstimativaBody tr");
            var coluna = document.querySelectorAll("#grdEstimativaBody tr td");
            for (let i = 0; i < linha.length; i++) {
                for (let j = 0; j < coluna.length; j++) {
                    console.log(coluna[j].textContent);
                    if (validaDat(coluna[2].textContent) == true && validaDat(coluna[5].textContent) == true) {
                        if (compareDates(coluna[2].textContent, coluna[5].textContent) == true) {
                            console.log("ok");
                        } else {
                            console.log("erro");
                        }
                    } else {
                        console.log("erro v");
                    }
                }
            }
        }

        function CSV() {
            const head = [
                ["A1_COD;", "A1_LOJA;", "A1_NOME;", "A1_NREDUZ;", "A1_PESSOA;", "A1_TIPO;", "A1_END;", "A1_EST;", "A1_COD_MUN;", "A1_MUN;", "A1_NATUREZ;", "A1_BAIRRO;", "A1_CEP;", "A1_ATIVIDA;", "A1_TEL;", "A1_TELEX;", "A1_FAX;", "A1_CONTATO;", "A1_CGC;", "A1_INSCR;", "A1_INSCRM;", "A1_CONTA;", "A1_RECISS;", "A1_CONT"],
            ]

            let csvContent = new Blob(["\uFEFF" + head], { type: "text/csv;charset=utf-8;" });

            head.forEach(function (rowArray) {
                let row = rowArray.join(",");
                csvContent += row + "\r\n";
            });

            var encodedUri = encodeURI(csvContent);
            window.open(encodedUri);
        }

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

        function compareDates(dateE,dateV) {
            let emissao = dateE.split('/') // separa a data pelo caracter '/'
            let vencimento = dateV.split('/')      // pega a data atual

            dateE = new Date(emissao[2], emissao[1] - 1, emissao[0]) // formata 'date'
            dateV = new Date(vencimento[2], vencimento[1] - 1, vencimento[0])

            // compara se a data informada é maior que a data atual
            // e retorna true ou false
            return dateE > dateV ? false : true
        }

        //Despesa

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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var cli = [["A1_COD;A1_LOJA;A1_NOME;A1_NREDUZ;A1_PESSOA;A1_TIPO;A1_END;A1_EST;A1_COD_MUN;A1_MUN;A1_NATUREZ;A1_BAIRRO;A1_CEP;A1_ATIVIDA;A1_TEL;A1_TELEX;A1_FAX;A1_CONTATO;A1_CGC;A1_INSCR;A1_INSCRM;A1_CONTA;A1_RECISS;A1_CONT"]];
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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var rec = [["E1_PREFIXO;E1_NUM;E1_PARCELA;E1_TIPO;E1_NATUREZ;E1_CLIENTE;E1_LOJA;E1_EMISSAO;E1_VENCTO;E1_VENCREA;E1_VALOR;E1_IRRF;E1_ISS;E1_HIST;E1_INSS;E1_COFINS;E1_CSLL;E1_PIS;E1_CONTROL;E1_ITEMCTA;E1_XPROD;"]];
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
                        alert("Sucesso");
                    }
                    else {

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


        //Servico

        function exportTableToCSVServico(clif, notaf, notitef, recf) {
            var dataI = document.getElementById("txtDtEmissaoInicialServ").value;
            var dataF = document.getElementById("txtDtEmissaoFinalServ").value;
            var exporta = document.getElementById("todosServ");
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServico",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        ServicoCliF(dataI, dataF, situacao, clif, notaf, notitef, recf);
                    }
                    else {
                        $("#msgErrServCli").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })

            
            


            
            // Download CSV file*/

        }

        function ServicoCliF(dataI, dataF, situacao, clif, notaf, notitef, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServicoCLI",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var cli = [["A1_COD;A1_LOJA;A1_NOME;A1_NREDUZ;A1_PESSOA;A1_TIPO;A1_END;A1_EST;A1_COD_MUN;A1_MUN;A1_NATUREZ;A1_BAIRRO;A1_CEP;A1_ATIVIDA;A1_TEL;A1_TELEX;A1_FAX;A1_CONTATO;A1_CGC;A1_INSCR;A1_INSCRM;A1_CONTA;A1_RECISS;A1_CONT"]];
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
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServicoNOTA",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var nota = [["F2_DOC;F2_SERIE;F2_CLIENTE;F2_LOJA;F2_COND;F2_DUPL;F2_EMISSAO;F2_EST;F2_FRETE;F2_SEGURO;F2_TIPOCLI;F2_VALBRUT;F2_VALIPI;F2_BASEIPI;F2_VALMERC;F2_NFORI;F2_SERIORI;F2_TIPO;F2_ESPEC1;F2_VOLUME1;F2_ICMSRET;F2_PLIQUI;F2_PBRUTO;F2_TRANSP;F2_FILIAL;F2_BASEISS;F2_VALISS;F2_VALFAT;F2_ESPECIE;F2_PREFIXO;F2_BASIMP5;F2_BASIMP6;F2_VALIMP5;F2_VALIMP6;F2_VALINSS;F2_HORA;F2_BASEINS;F2_MOEDA;F2_VALCOFI;F2_VALCSLL;F2_VALPIS;F2_DTDIGIT;F2_RECISS;F2_NFELETR;F2_EMINFE;F2_CREDNFE;F2_CODNFE;F2_TPNFEXP;F2_CLIENT;F2_LOJENT;F2_CHVNFE;F2_TPFRETE;F2_HORNFE;F2_XCNPJ;"]];
                        for (let i = 0; i < dado.length; i++) {
                            nota.push([dado[i]]);
                        }
                        ServicoNotiteF(dataI, dataF, situacao, cli, nota, clif, notaf, notitef, recf);
                    }
                    else {
                        $("#msgErrServNota").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function ServicoNotiteF(dataI, dataF, situacao, cli, nota, clif, notaf, notitef, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServicoNOTITE",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var notite = [["D2_FILIAL;D2_COD;D2_UM;D2_SEGUM;D2_QUANT;D2_PRCVEN;D2_TOTAL;D2_VALIPI;D2_VALICM;D2_TES;D2_CF;D2_IPI;D2_PESO;D2_CONTA;D2_PEDIDO;D2_ITEMPV;D2_CLIENTE;D2_LOJA;D2_LOCAL;D2_DOC;D2_EMISSAO;D2_GRUPO;D2_TP;D2_SERIE;D2_CUSTO1;D2_EST;D2_TIPO;D2_NFORI;D2_SERIORI;D2_QTDEDEV;D2_ITEM;D2_CODISS;D2_CLASFIS;D2_BASIMP5;D2_BASIMP6;D2_VALIMP5;D2_VALIMP6;D2_ITEMORI;D2_ALIQINS;D2_ALIQISS;D2_BASEINS;D2_BASEIPI;D2_BASEISS;D2_CCUSTO;D2_ITEMCC;D2_ALQIMP5;D2_DTDIGIT;D2_VALISS;D2_ALQIMP6;"]];
                        for (let i = 0; i < dado.length; i++) {
                            notite.push([dado[i]]);
                        }
                        ServicoRECF(dataI, dataF, situacao, cli, nota, notite, clif, notaf, notitef, recf)
                    }
                    else {
                        $("#msgErrServNotite").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function ServicoRECF(dataI, dataF, situacao, cli, nota, notite, clif, notaf, notitef, recf) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSNotaServicoREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var rec = [["E1_PREFIXO;E1_NUM;E1_PARCELA;E1_TIPO;E1_NATUREZ;E1_CLIENTE;E1_LOJA;E1_EMISSAO;E1_VENCTO;E1_VENCREA;E1_VALOR;E1_IRRF;E1_ISS;E1_HIST;E1_INSS;E1_COFINS;E1_CSLL;E1_PIS;E1_CONTROL;E1_ITEMCTA;E1_XPROD;"]];
                        for (let i = 0; i < dado.length; i++) {
                            rec.push([dado[i]]);
                        }
                        updateContaPagarReceberServico(dataI, dataF, situacao, cli, nota, notite, rec, clif, notaf, notitef, recf);
                        

                    }
                    else {
                        $("#msgErrServRec").fadeIn(500).delay(1000).fadeOut(500);
                    }
                }
            })
        }

        function updateContaPagarReceberServico(dataI, dataF, situacao, cli, nota, notite, rec, clif, notaf, notitef, recf) {
            var dataI = document.getElementById("txtDtEmissaoInicialServ").value;
            var dataF = document.getElementById("txtDtEmissaoFinalServ").value;
            var exporta = document.getElementById("todosServ");
            var situacao;
            if (exporta.checked) {
                situacao = 0
            } else {
                situacao = 1
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/integrarTOTVSServico",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '",situacao:"' + situacao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        downloadCSVServico(cli.join("\n"), nota.join("\n"), notite.join("\n"), rec.join("\n"), clif, notaf, notitef, recf);
                        alert("sucesso");
                    } else {
                        alert("falha");
                    }
                }
            });
        }

        function downloadCSVServico(cli, nota, notite, rec, clif, notaf, notitef, recf) {
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

        //Credit

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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
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

        //Debit

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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
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
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null && dado != "erro") {
                        var rec = [["E2_FILIAL;E2_PREFIXO;E2_NUM;E2_PARCELA;E2_TIPO;E2_FORNECE;E2_LOJA;E2_NATUREZ;E2_EMISSAO;E2_VENCTO;E2_VENCREA;E2_VALOR;E2_HIST;E2_ITEMCTA;E2_USERS;E2_XPROD;"]];
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

        //Pa

        function exportTableToCSVPA(fornec, recf) {
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

        function PAFornec() {
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

        function PAREC(dataI, dataF) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTOTVSInvCreditREC",
                data: '{dataI:"' + dataI + '",dataF:"' + dataF + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var rec = [["E1_PREFIXO;E1_NUM;E1_PARCELA;E1_TIPO;E1_NATUREZ;E1_CLIENTE;E1_LOJA;E1_EMISSAO;E1_VENCTO;E1_VENCREA;E1_VALOR;E1_IRRF;E1_ISS;E1_HIST;E1_INSS;E1_COFINS;E1_CSLL;E1_PIS;E1_CONTROL;E1_ITEMCTA;E1_XPROD"]];
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

        function updateContaPagarReceberPA() {
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
                        alert("sucesso");
                    } else {
                        alert("erro");
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


        //PagamentosRecebimentos

        function PagamentosRecebimentos() {
            $("#modalPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalPagamentoRecebimento").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdPagamentoRecebimentoBody").empty();
                        $("#grdPagamentoRecebimentoBody").append("<tr><td colspan='14'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdPagamentoRecebimentoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdPagamentoRecebimentoBody").append("<tr><td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO_REC"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE_REC"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_REC"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO_PAG"] + "</td><td class='text-center'>" + dado[i]["NM_FORNECEDOR_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_PAG"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_PAG"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
            } else {
                
            }
        }

        function exportCSV (filename){
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

        function exportTableToCSVPagamentosRecebimentos(csv,filename) {
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

        function printPagamentosRecebimentos() {
            var dtInicial = document.getElementById("txtDtInicialPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalPagamentoRecebimento").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            var position = 45;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarContasRecebidasPagas",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var doc = new jsPDF('l');
                        doc.setFontSize(7);
                        doc.setFontStyle("bold");
                        doc.text("NR PROCESSO", 4, 45);
                        doc.setLineWidth(0.2);
                        doc.rect(3, 37, 290, 10);
                        doc.text("ITEM DESPESA", 27, 45);
                        doc.text("RECEBIMENTO", 110, 40);
                        doc.text("DATA", 70, 45);
                        doc.text("CLIENTE", 90, 45);
                        doc.text("DEVIDO", 120, 45);
                        doc.text("MOEDA", 135, 45);
                        doc.text("CAMBIO", 147, 45);
                        doc.text("LIQUIDADO", 162, 45);
                        doc.text("PAGAMENTO", 230, 40);
                        doc.text("DATA", 185, 45);
                        doc.text("FORNECEDOR", 200, 45);
                        doc.text("DEVIDO", 230, 45);
                        doc.text("MOEDA", 245, 45);
                        doc.text("CAMBIO", 260, 45);
                        doc.text("LIQUIDADO", 275, 45);
                        for (let i = 0; i < dado.length; i++) {
                            position = position + 5;
                            doc.setFontStyle("normal");
                            doc.text(dado[i]["NR_PROCESSO"], 4, position);
                            doc.text(dado[i]["NM_ITEM_DESPESA"], 27, position);
                            doc.text(dado[i]["DT_LIQUIDACAO_REC"], 70, position);
                            doc.text(dado[i]["NM_CLIENTE_REC"].substring(0, 15), 90, position);
                            doc.text(dado[i]["VL_DEVIDO_REC"], 120, position);
                            doc.text(dado[i]["MOEDA_REC"], 135, position);
                            doc.text(dado[i]["VL_CAMBIO_REC"], 147, position);
                            doc.text(dado[i]["VL_LIQUIDO_REC"], 162, position);
                            doc.text(dado[i]["DT_LIQUIDACAO_PAG"], 185, position);
                            doc.text(dado[i]["NM_FORNECEDOR_PAG"].substring(0, 15), 200, position);
                            doc.text(dado[i]["VL_DEVIDO_PAG"], 230, position);
                            doc.text(dado[i]["MOEDA_PAG"], 245, position);
                            doc.text(dado[i]["VL_CAMBIO_PAG"], 260, position);
                            doc.text(dado[i]["VL_LIQUIDO_PAG"], 275, position);
                        }
                        doc.output("dataurlnewwindow");
                    }
                    else {
                    }
                }
            })
            
        }
    </script>

</asp:Content>

