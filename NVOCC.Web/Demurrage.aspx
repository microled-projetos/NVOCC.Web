<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Demurrage.aspx.cs" Inherits="ABAINFRA.Web.Demurrage" %>
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
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="alert alert-success text-center" id="msgSuccessUpdate">
                                Base atualizada com sucesso.
                            </div>
                            <div class="alert alert-danger text-center" id="msgErrUpdate">
                                Erro ao atualizar base.
                            </div>
                        </div>
                    </div>
                    <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                        <div style="margin: auto;">
                            <button type="button" id="btnDevolveCont" class="btn btn-primary" data-toggle="modal" data-target="#modalDemurrage">Devolução Container</button>                    
                            <button type="button" id="btnExportCSV" class="btn btn-primary" data-toggle="modal" data-target="#modalExportarCSV" onclick="csvExportTab()">Exportação CSV Terceirizada</button>                    
                            <button type="button" id="btnImportExc" class="btn btn-primary" data-toggle="modal" data-target="#modalImportExcel">Importação Excel</button>               
                            <button type="button" id="btnExportGridAtual" class="btn btn-primary" onclick="exportTableToCSVAtual('members.csv')">Exportação Grid Completo Atual CSV</button>                 
                        </div>
                    </div>
                    <div class="row topMarg flexdiv">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Consultar por:<span class="required">*</span></label>
                                <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label"><span class="required">&nbsp</span></label>
                                <input id="txtConsulta" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <button type="button" id="btnConsulta" onclick="consultaFiltrada()" class="btn btn-primary">Consultar</button>
                            </div>
                        </div>
                    </div>
                    <div class="row topMarg">
                        
                        <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                            <div>
                                <asp:CheckBox ID="chkFinalizado" runat="server" CssClass="form-control noborder" Text="&nbsp;Finalizados"></asp:CheckBox>
                            </div>
                            <div>
                                <asp:CheckBox ID="chkAtivo" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;Ativos"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="modal fade bd-example-modal-xl" id="modalExportarCSV" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalDemurrageTitle">Exportar CSV</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Embarque Inicial<span class="required">*</span></label>
                                                <input id="dtEmbarqueInicial" class="form-control" type="date" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Data Embarque Final<span class="required">*</span></label>
                                                <input id="dtEmbarqueFinal" class="form-control" type="date" />
                                            </div>
                                        </div>
                                    </div>      
                                    <div class="row">  
                                        <div class="col-sm-6">
                                            <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px;">
                                                <div>
                                                    <asp:CheckBox ID="regExpo" Checked="true" runat="server" CssClass="form-control noborder" Text="&nbsp;Registros Exportados"></asp:CheckBox>
                                                </div>
                                                <div>
                                                    <asp:CheckBox ID="regNExpo" runat="server" CssClass="form-control noborder" Text="&nbsp;Registros Não Exportados"></asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row topMarg" style="padding: 0px 15px">
                                        <div class="table-responsive tableFixHead" >
                                            <table id="tblExport" class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" >Data Embarque</th>
                                                        <th class="text-center" >Referência</th>
                                                        <th class="text-center" >Nº Container</th>
                                                        <th class="text-center" >Tipo</th>
                                                        <th class="text-center" >Free Time</th>
                                                        <th class="text-center" >Data Devolução</th>
                                                        <th class="text-center" >Origem</th>
                                                        <th class="text-center" >Destino</th>
                                                        <th class="text-center" >Navio</th>
                                                        <th class="text-center" >Viagem</th>
                                                        <th class="text-center" >Previsão Chegada</th>
                                                        <th class="text-center" >House BL</th>
                                                        <th class="text-center" >Master BL</th>
                                                        <th class="text-center" >Transportador</th>
                                                        <th class="text-center" >Consignatário</th>
                                                        <th class="text-center" >CNPJ Consignatário</th>
                                                        <th class="text-center" >Exportador</th>
                                                        <th class="text-center" >CNPJ Exportador</th>
                                                        <th class="text-center" >Referência do Cliente</th>
                                                        <th class="text-center" >Data Exportação</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="grdExportDemurrage">
                                                                             
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnConsultarDemurrage" onclick="csvExportTabFilter()" class="btn btn-success">Consultar</button>
                                    <button type="button" id="btnExportarDemurrage" class="btn btn-success" onclick="exportTableToCSVTerc('members.csv')">Exportar CSV</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalImportExcel" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalImportExcelTitle">Importação Excel</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div id="boxTableImport" class="table-responsive tableFixHead" >
                                        <table id="grdImportDemurrage">

                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <label class="btn btn-success" for="btnImportarDemurrage">Importar Excel</label>
                                    <input type="file" id="btnImportarDemurrage" name="excel" accept=".xls, .xlsx, .csv" class="btn btn-success" />
                                    <button type="button" id="btnConsistirDemurrage" onclick="percorrerLinha()" class="btn btn-success">Consistir</button>
                                    <button type="button" id="btnGerarCSV" onclick="GerarCSV('membersImport.csv')" class="btn btn-success">Gerar CSV</button>
                                    <button type="button" id="btnAtualizarBaseFCA" class="btn btn-success">Atualizar Base FCA</button>                                    
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive tableFixHead">
                        <table id="grdDemurrageAtual" class="table tablecont">
                            <thead>
                                <tr>
                                    <th class="text-center" scope="col">Nº Processo</th>
                                    <th class="text-center" scope="col">Nº Container</th>
                                    <th class="text-center" scope="col">Tipo Container</th>
                                    <th class="text-center" scope="col">Cliente</th>
                                    <th class="text-center" scope="col">Transportador</th>
                                    <th class="text-center" scope="col">Data Chegada</th>
                                    <th class="text-center" scope="col">FreeTime</th>
                                    <th class="text-center" scope="col">Data Limite</th>
                                    <th class="text-center" scope="col">Data Devolução</th>
                                    <th class="text-center" scope="col">Qtd Dias Demurrage</th>
                                    <th class="text-center" scope="col">Observação</th>
                                    <th class="text-center" scope="col">Status Terc</th>
                                    <th class="text-center" scope="col">Data Status</th>
                                    <th class="text-center" scope="col">Valor Faturado Terc</th>
                                    <th class="text-center" scope="col">Vencimento Fatura</th>
                                    <th class="text-center" scope="col">Recebimento Fatura</th>
                                </tr>
                            </thead>
                            <tbody id="grdDemurrageAtualBody">
                                
                            </tbody>
                        </table>
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
    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTabela",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdDemurrageAtualBody").empty();
                    $("#grdDemurrageAtualBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdDemurrageAtualBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdDemurrageAtualBody").append("<tr><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["CONSIGNATARIO"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_LIMITE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO_DEMUR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DS_STATUS_TERC"] + "</td><td class='text-center'>" + dado[i]["DT_STATUS_TERC"] + "</td><td class='text-center'>" + dado[i]["VL_FATURA_TERC"] + "</td>"+
                                "<td class='text-center'>" + dado[i]["DT_VENCIMENTO_FATURA_TERC"] + "</td > <td class='text-center'>" + dado[i]["DT_PAGAMENTO_FATURA_TERC"] + "</td>");
                        }
                    }
                    else {
                        $("#grdDemurrageAtualBody").empty();
                        $("#grdDemurrageAtualBody").append("<tr id='msgEmptyWeek'><td colspan='16' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        });
    </script>
    <script>
        function percorrerLinha() {
            verificaProcesso();
        }

        function verificaProcesso() {
            var id;
            var dadoProcesso;
            var nrProcesso;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoProcesso = document.querySelector(".idReferenciaImport" + id + "");
                nrProcesso = dadoProcesso.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                (function (campo, data, dadoObs) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/consultarProcesso",
                        data: '{nrProcesso:"' + data + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado == 1) {
                                campo.classList.add("verified");
                            }
                            else {
                                campo.classList.add("errorVerified");
                                dadoObs.textContent = "Dado Ínvalido";
                                dadoObs.classList.add("errorVerified");
                            }
                        }
                    })
                })
                    (dadoProcesso, nrProcesso, dadoObs);
            }
            verificaContainer();
        }     
        function verificaContainer() {
            var id;
            var dadoProcesso;
            var nrProcesso;
            var dadoContainer;
            var nrContainer;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoProcesso = document.querySelector(".idReferenciaImport" + id + "");
                nrProcesso = dadoProcesso.textContent;
                dadoContainer = document.querySelector(".nrContainerImport" + id + "");
                nrContainer = dadoContainer.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                (function (campo, data, dadoObs, processo) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/consultarNumeroContainer",
                        data: '{nrContainer:"' + data + '", nrProcesso: "'+ processo +'" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            if (dado == 1) {
                                campo.classList.add("verified");
                            }
                            else {
                                campo.classList.add("errorVerified");
                                dadoObs.textContent = "Dado Ínvalido";
                                dadoObs.classList.add("errorVerified");
                            }
                        }
                    })
                })
                    (dadoContainer, nrContainer, dadoObs, nrProcesso);
            }
           verificaStatus()
        }
        function verificaStatus() {
            var id;
            var dadoReportDate;
            var reportDate;
            var dadoPagamento;
            var pagamento;
            var dadoVencimentoFatura;
            var vencimentoFatura;
            var dadoValorFatura;
            var valorFatura;
            var dadoDemurrage;
            var demurrage;
            var dadoDevolucao;
            var devolucao;
            var dadoStatus;
            var status;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoReportDate = document.querySelector(".dtReportDateImport" + id + "");
                reportDate = dadoReportDate.textContent;
                dadoPagamento = document.querySelector(".dtPagamentoFaturaImport" + id + "");
                pagamento = dadoPagamento.textContent;
                dadoVencimentoFatura = document.querySelector(".dtVencimentoFaturaImport" + id + "");
                vencimentoFatura = dadoVencimentoFatura.textContent;
                dadoValorFatura = document.querySelector(".vlFaturadoImport" + id + "");
                valorFatura = dadoValorFatura.textContent;
                dadoDemurrage = document.querySelector(".dtDiasDemurrageImport" + id + "");
                demurrage = dadoDemurrage.textContent;
                dadoDevolucao = document.querySelector(".dtDevolucaoImport" + id + "");
                devolucao = dadoDevolucao.textContent;
                dadoStatus = document.querySelector(".dsStatusImport" + id + "");
                status = dadoStatus.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                if (status == "Aberto") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (devolucao != "-") {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    if (demurrage != "-") {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    if (valorFatura != "-") {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    if (vencimentoFatura != "-") {
                        dadoVencimentoFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    if (pagamento != "-") {
                        dadoPagamento.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                }
                else if (status == "Devolvido Sem Detention") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (devolucao == "" && devolucao == "-") {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (demurrage != "-") {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (valorFatura != "-") {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (vencimentoFatura != "-") {
                        dadoVencimentoFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (pagamento != "-") {
                        dadoPagamento.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else {
                        var validate = ValidaData(devolucao);
                        if (validate == true) {
                            dadoDevolucao.classList.add("verified");
                        }
                        else {
                            dadoDevolucao.classList.add("errorVerified");
                            dadoObs.textContent = "Dado Ínvalido";
                            dadoObs.classList.add("errorVerified");
                        }
                    }
                }
                else if (status == "Devolvido sem demurrage") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (devolucao == "" && devolucao == "-") {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (demurrage != "-") {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (valorFatura != "-") {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (vencimentoFatura != "-") {
                        dadoVencimentoFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (pagamento != "-") {
                        dadoPagamento.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else {
                        var validate = ValidaData(devolucao);
                        if (validate == true) {
                            dadoDevolucao.classList.add("verified");
                        }
                        else {
                            dadoDevolucao.classList.add("errorVerified");
                            dadoObs.textContent = "Dado Ínvalido";
                            dadoObs.classList.add("errorVerified");
                        }
                    }
                }
                else if (status == "Fase final de faturamento") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                }
                else if (status == "Em cobrança") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    //Devolução

                    if (devolucao == "" && devolucao == "-") {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textCoswntent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    //Demurrage

                    if (demurrage == "" && demurrage == "-") {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (demurrage < 0) {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else {
                        dadoDemurrage.classList.add("verified");
                    }

                    //Valor Fatura

                    if (valorFatura == "" && valorFatura == "-") {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (valorFatura < 0) {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else {
                        dadoValorFatura.classList.add("verified");
                    }

                    //Vencimento Fatura

                    if (vencimentoFatura == "" && vencimentoFatura == "-") {
                        dadoVencimentoFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    //Data Pagamento
                    var validateVencimento = ValidaData(vencimentoFatura);
                    var validateDevolucao = ValidaData(devolucao);

                    if (pagamento != "-") {
                        dadoPagamento.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    
                    else if (validateDevolucao == true) {
                            dadoDevolucao.classList.add("verified");
                        }
                    else {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                        if (validateVencimento == true) {
                            dadoVencimentoFatura.classList.add("verified");
                        }
                        else {
                            dadoVencimentoFatura.classList.add("errorVerified");
                            dadoObs.textContent = "Dado Ínvalido";
                            dadoObs.classList.add("errorVerified");
                        }

                        if (demurrage < 0) {
                            dadoDemurrage.classList.add("errorVerified");
                            dadoObs.textContent = "Dado Ínvalido";
                            dadoObs.classList.add("errorVerified");
                        }

                        if (valorFatura < 0) {
                            dadoValorFatura.classList.add("errorVerified");
                            dadoObs.textContent = "Dado Ínvalido";
                            dadoObs.classList.add("errorVerified");
                        }
                    }
                else if (status == "Baixado") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    //Devolução
                    if (devolucao == "" && devolucao == "-") {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    //Demurrage
                    if (demurrage == "" && demurrage == "-") {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if (demurrage < 0) {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else {
                        dadoDemurrage.classList.add("verified");
                    }

                    //Valor Fatura
                    if (valorFatura == "" && valorFatura == "-") {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else if(valorFatura < 0){
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                    else {
                        dadoValorFatura.classList.add("verified");
                    }


                    if (vencimentoFatura == "" && vencimentoFatura == "-") {
                        dadoVencimentoFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (pagamento == "" && pagamento == "-") {
                        dadoPagamento.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }


                   
                    var validateVencimento = ValidaData(vencimentoFatura);
                    var validateDevolucao = ValidaData(devolucao);
                    var validatePagamento = ValidaData(pagamento);

                    //Valida Datas
                    if (validateDevolucao == true) {
                        dadoDevolucao.classList.add("verified");
                    }
                    else {
                        dadoDevolucao.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (validateVencimento == true) {
                        dadoVencimentoFatura.classList.add("verified");
                    }
                    else {
                        dadoVencimentoFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (demurrage < 0) {
                        dadoDemurrage.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (valorFatura < 0) {
                        dadoValorFatura.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                    if (validatePagamento == true) {
                        dadoPagamento.classList.add("verified");

                    }
                    else {
                        dadoPagamento.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }

                }
                
                else if (status == "Cobrança cancelada") {
                    dadoStatus.classList.add("verified");
                    var validateReport = ValidaData(reportDate);
                    if (validateReport == true) {
                        dadoReportDate.classList.add("verified");
                    }
                    else {
                        dadoReportDate.classList.add("errorVerified");
                        dadoObs.textContent = "Dado Ínvalido";
                        dadoObs.classList.add("errorVerified");
                    }
                }
                else {
                    dadoStatus.classList.add("errorVerified");
                    dadoObs.textContent = "Dado Ínvalido";
                    dadoObs.classList.add("errorVerified");
                }
            }
        }
        function verificaDevolucao() {
            var id;
            var dadoDevolucao;
            var devolucao;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoDevolucao = document.querySelector(".dtDevolucaoImport" + id + "");
                devolucao = dadoDevolucao.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                var validate = ValidaData(devolucao);
                if (validate == true) {
                    dadoDevolucao.classList.add("verified");
                }
                else {
                    dadoDevolucao.classList.add("errorVerified");
                    dadoObs.textContent = "Dado Ínvalido";
                    dadoObs.classList.add("errorVerified");
                }
            }
            verificaDemurrage();
        }
        function verificaDemurrage() {
            var id;
            var dadoDemurrage;
            var demurrage;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoDemurrage = document.querySelector(".dtDiasDemurrageImport" + id + "");
                demurrage = dadoDemurrage.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                if (parseInt(demurrage) > 0 || demurrage != "-") {
                    dadoDemurrage.classList.add("verified");
                }
                else {
                    dadoDemurrage.classList.add("errorVerified");
                    dadoObs.textContent = "Dado Ínvalido";
                    dadoObs.classList.add("errorVerified");
                }
            }
            verificaValorFatura();
        }
        function verificaValorFatura() {
            var id;
            var dadoValorFatura;
            var valorFatura;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoValorFatura = document.querySelector(".vlFaturadoImport" + id + "");
                valorFatura = dadoValorFatura.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                if (parseFloat(valorFatura) > 0 || valorFatura != "-") {
                    dadoValorFatura.classList.add("verified");
                }
                else {
                    dadoValorFatura.classList.add("errorVerified");
                    dadoObs.textContent = "Dado Ínvalido";
                    dadoObs.classList.add("errorVerified");
                }
            }
            verificaVencimentoFatura();
        }
        function verificaVencimentoFatura() {
            var id;
            var dadoVencimentoFatura;
            var vencimentoFatura;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoVencimentoFatura = document.querySelector(".dtVencimentoFaturaImport" + id + "");
                vencimentoFatura = dadoVencimentoFatura.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                var validate = ValidaData(vencimentoFatura);
                if (validate == true) {
                    dadoVencimentoFatura.classList.add("verified");
                }
                else {
                    dadoVencimentoFatura.classList.add("errorVerified");
                    dadoObs.textContent = "Dado Ínvalido";
                    dadoObs.classList.add("errorVerified");
                }
            }
            verificaPagamento();
        }
        function verificaPagamento() {
            var id;
            var dadoPagamento;
            var pagamento;
            var dadoObs;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoPagamento = document.querySelector(".dtPagamentoFaturaImport" + id + "");
                pagamento = dadoPagamento.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                var validate = ValidaData(pagamento);
                if (validate == true) {
                    dadoPagamento.classList.add("verified");
                }
                else {
                    dadoPagamento.classList.add("errorVerified");
                    dadoObs.textContent = "Dado Ínvalido";
                    dadoObs.classList.add("errorVerified");
                }
            }
        }


        $("#btnAtualizarBaseFCA").click(function(){
            var id;
            var dadoReportDate;
            var reportDate;
            var dadoContainer;
            var nrContainer;
            var dadoProcesso;
            var nrProcesso;
            var dadoStatus;
            var status;
            var dadoDevolucao;
            var devolucao;
            var dadoDemurrage;
            var demurrage;
            var dadoValorFatura;
            var valorFatura;
            var dadoVencimentoFatura;
            var vencimentoFatura;
            var dadoPagamento;
            var pagamento;
            var dadoObs;
            var dadoInput;
            var linhas = document.querySelectorAll("tbody tr");
            for (id = 1; id < linhas.length; id++) {
                dadoReportDate = document.querySelector(".dtReportDateImport" + id + "");
                reportDate = dadoReportDate.textContent;
                dadoContainer = document.querySelector(".idReferenciaImport" + id + "");
                nrContainer = dadoContainer.textContent;
                dadoProcesso = document.querySelector(".nrContainerImport" + id + "");
                nrProcesso = dadoProcesso.textContent;
                dadoStatus = document.querySelector(".dsStatusImport" + id + "");
                status = dadoStatus.textContent;
                dadoDevolucao = document.querySelector(".dtDevolucaoImport" + id + "");
                devolucao = dadoDevolucao.textContent;
                dadoDemurrage = document.querySelector(".dtDiasDemurrageImport" + id + "");
                demurrage = dadoDemurrage.textContent;
                dadoValorFatura = document.querySelector(".vlFaturadoImport" + id + "");
                valorFatura = dadoValorFatura.textContent;
                dadoVencimentoFatura = document.querySelector(".dtVencimentoFaturaImport" + id + "");
                vencimentoFatura = dadoVencimentoFatura.textContent;
                dadoPagamento = document.querySelector(".dtPagamentoFaturaImport" + id + "");
                pagamento = dadoPagamento.textContent;
                dadoObs = document.querySelector(".obsImport" + id + "");
                dadoInput = {
                    "DT_REPORT_DATE": reportDate.split('/').reverse().join('-'),
                    "NR_CNTR": nrContainer,
                    "NR_PROCESSO": nrProcesso,
                    "DS_STATUS": status,
                    "DT_DEVOLUCAO_CNTR": devolucao.split('/').reverse().join('-'),
                    "QT_DIAS_DEMURRAGE": demurrage,
                    "VL_FATURA": valorFatura,
                    "DT_VENCIMENTO_FATURA": vencimentoFatura.split('/').reverse().join('-'),
                    "DT_PAGAMENTO": pagamento.split('/').reverse().join('-')
                };
                (function (dadoInput, dadoObs, id) {
                    if ($(dadoObs).hasClass("errorVerified") == false) {
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/AtualizarBaseFCA",
                            data: JSON.stringify({ dadosEdit: (dadoInput) }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado == "1") {
                                    $("#msgSuccessUpdate").fadeIn(500).delay(3500).fadeOut(500);
                                    $.ajax({
                                        type: "POST",
                                        url: "DemurrageService.asmx/listarTabela",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        beforeSend: function () {
                                            $("#grdDemurrageAtualBody").empty();
                                            $("#grdDemurrageAtualBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                                        },
                                        success: function (dado) {
                                            var dado = dado.d;
                                            dado = $.parseJSON(dado);
                                            if (dado != null) {
                                                $("#grdDemurrageAtualBody").empty();
                                                for (let i = 0; i < dado.length; i++) {
                                                    $("#grdDemurrageAtualBody").append("<tr><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                                        "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["CONSIGNATARIO"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                                        "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_LIMITE"] + "</td>" +
                                                        "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO_DEMUR"] + "</td>" +
                                                        "<td class='text-center'>" + dado[i]["DS_STATUS_TERC"] + "</td><td class='text-center'>" + dado[i]["DT_STATUS_TERC"] + "</td><td class='text-center'>" + dado[i]["VL_FATURA_TERC"] + "</td>" +
                                                        "<td class='text-center'>" + dado[i]["DT_VENCIMENTO_FATURA_TERC"] + "</td > <td class='text-center'>" + dado[i]["DT_PAGAMENTO_FATURA_TERC"] + "</td>");
                                                }
                                            }
                                            else {
                                                $("#grdDemurrageAtualBody").empty();
                                                $("#grdDemurrageAtualBody").append("<tr id='msgEmptyWeek'><td colspan='16' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                                            }
                                        }
                                    })
                                }
                                else {
                                    $("#msgErrUpdate").fadeIn(500).delay(3500).fadeOut(500);
                                }
                            }
                        })
                    }
                    
                })
                    (dadoInput, dadoObs, id);
            }
            $("#modalImportExcel").modal("hide");
        })
        function data_valida(date) {
            var matches = /(\d{4})[-.\/](\d{2})[-.\/](\d{2})/.exec(date);
            if (matches == null) {
                return false;
            }
            var dia = matches[3];
            var mes = matches[2] - 1;
            var ano = matches[1];
            var data = new Date(ano, mes, dia);
            return data.getDate() == dia && data.getMonth() == mes && data.getFullYear() == ano;
                }

        function ValidaData(data) {
            reg = /[^\d\/\.]/gi;                  // Mascara = dd/mm/aaaa | dd.mm.aaaa
            var valida = data.replace(reg, '');    // aplica mascara e valida só numeros
            if (valida && valida.length == 10) {  // é válida, então ;)
                var ano = data.substr(6),
                    mes = data.substr(3, 2),
                    dia = data.substr(0, 2),
                    M30 = ['04', '06', '09', '11'],
                    v_mes = /(0[1-9])|(1[0-2])/.test(mes),
                    v_ano = /(19[1-9]\d)|(20\d\d)|2100/.test(ano),
                    rexpr = new RegExp(mes),
                    fev29 = ano % 4 ? 28 : 29;

                if (v_mes && v_ano) {
                    if (mes == '02') return (dia >= 1 && dia <= fev29);
                    else if (rexpr.test(M30)) return /((0[1-9])|([1-2]\d)|30)/.test(dia);
                    else return /((0[1-9])|([1-2]\d)|3[0-1])/.test(dia);
                }
            }
            return false                           // se inválida :(
        }
        function atualizarBaseFCA() {
            var coluna2 = document.querySelectorAll(".idReferenciaImport");
            var coluna3 = document.querySelectorAll(".nrContainerImport");
            var coluna4 = document.querySelectorAll(".dsStatusImport");
            var coluna5 = document.querySelectorAll(".dtDevolucaoImport");
            var coluna6 = document.querySelectorAll(".dtDiasDemurrageImport");
            var coluna7 = document.querySelectorAll(".vlFaturadoImport");
            var coluna8 = document.querySelectorAll(".dtVencimentoFaturaImport");
            var coluna9 = document.querySelectorAll(".dtPagamentoFaturaImport");
            var obs = document.querySelectorAll(".obsImport");

            obs.forEach(obs => {
                if (obs == "") {
                    coluna2.forEach(coluna2 => {
                        console.log(coluna2.innerHTML);
                    });
                    coluna3.forEach(coluna3 => {
                        console.log(coluna3.innerHTML);
                    });
                    coluna4.forEach(coluna4 => {
                        console.log(coluna4.innerHTML);
                    });
                    coluna5.forEach(coluna5 => {
                        console.log(coluna5.innerHTML);
                    });
                    coluna6.forEach(coluna6 => {
                        console.log(coluna6.innerHTML);
                    });
                    coluna7.forEach(coluna7 => {
                        console.log(coluna7.innerHTML);
                    });
                    coluna8.forEach(coluna8 => {
                        console.log(coluna8.innerHTML);
                    });
                    coluna9.forEach(coluna9 => {
                        console.log(coluna9.innerHTML);
                    });
                }
                else {
                    coluna2.forEach(coluna2 => {
                        console.log(coluna2.innerHTML);
                    });
                    coluna3.forEach(coluna3 => {
                        console.log(coluna3.innerHTML);
                    });
                    coluna4.forEach(coluna4 => {
                        console.log(coluna4.innerHTML);
                    });
                    coluna5.forEach(coluna5 => {
                        console.log(coluna5.innerHTML);
                    });
                    coluna6.forEach(coluna6 => {
                        console.log(coluna6.innerHTML);
                    });
                    coluna7.forEach(coluna7 => {
                        console.log(coluna7.innerHTML);
                    });
                    coluna8.forEach(coluna8 => {
                        console.log(coluna8.innerHTML);
                    });
                    coluna9.forEach(coluna9 => {
                        console.log(coluna9.innerHTML);
                    });
                }
            });
        }

        function consultaFiltrada() {
            var idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            var stringConsulta = document.getElementById("txtConsulta").value;
            var finalizado = document.getElementById("MainContent_chkFinalizado");
            var finalizadoValue;
            var ativo = document.getElementById("MainContent_chkAtivo");
            var ativoValue;
            if (ativo.checked) {
                ativoValue = "1";
            }
            else {
                ativoValue = "0";
            }

            if (finalizado.checked) {
                finalizadoValue = "1";
            }
            else {
                finalizadoValue = "0";
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/filtrarTabela",
                data: '{idFilter:"' + idFiltro + '", Filter:"' + stringConsulta + '", Finalizado: "' + finalizadoValue + '",Ativo: "' + ativoValue + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdDemurrageAtualBody").empty();
                    $("#grdDemurrageAtualBody").append("<tr><td colspan='16'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdDemurrageAtualBody").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdDemurrageAtualBody").append("<tr><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NR_CNTR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["CONSIGNATARIO"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td><td class='text-center'>" + dado[i]["DT_FINAL_LIMITE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_DEMURRAGE"] + "</td><td class='text-center'>" + dado[i]["DS_OBSERVACAO_DEMUR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DS_STATUS_TERC"] + "</td><td class='text-center'>" + dado[i]["DT_STATUS_TERC"] + "</td><td class='text-center'>" + dado[i]["VL_FATURA_TERC"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_VENCIMENTO_FATURA_TERC"] + "</td > <td class='text-center'>" + dado[i]["DT_PAGAMENTO_FATURA_TERC"] + "</td>");
                        }
                    }
                    else {
                        $("#grdDemurrageAtualBody").empty();
                        $("#grdDemurrageAtualBody").append("<tr id='msgEmptyWeek'><td colspan='16' class='alert alert-light text-center'>Resultado não encontrado</td></tr>");
                    }
                }
            });
        }

        function downloadCSVTerc(csv, filename) {
            var csvFile;
            var downloadLink;

            // CSV file
            csvFile = new Blob(["\uFEFF"+csv], { type: "text/csv;charset=utf-8;" });

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

        function exportTableToCSVTerc(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#tblExport tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#tblExport td, #tblExport th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVTerc(csv.join("\n"), filename);
        }

        function downloadCSVAtual(csv, filename) {
            var csvFile;
            var downloadLink;

            // CSV file
            csvFile = new Blob(["\uFEFF"+csv], { type: "text/csv;charset=utf-8;" });

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

        function exportTableToCSVAtual(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdDemurrageAtual tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdDemurrageAtual td, #grdDemurrageAtual th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVAtual(csv.join("\n"), filename);
        }


        var uploadCSV = $("#btnImportarDemurrage");

        let upload = document.getElementById("btnImportarDemurrage").addEventListener('change', () => {
            $("#grdImportDemurrage").empty();
            Papa.parse(document.getElementById("btnImportarDemurrage").files[0], {
                download: true,
                header: false,
                skipEmptyLines: true,
                complete: function (results) {
                    let i = 0;
                    results.data.map((data, index) => {
                        if (i === 0) {
                            let table = document.getElementById('grdImportDemurrage');
                            generateTableHead(table, data);
                        }
                        else if (i === 1) {
                            let table = document.getElementById('grdImportDemurrage');
                            generateTableBody(table, data,i);
                        }
                        else {
                            let table = document.getElementById('grdBodyImportEx');
                            generateTableRows(table, data,i);
                        }
                        i++
                    })
                }
            });
        });

        function generateTableHead(table, data) {
            let thead = table.createTHead();
            let row = thead.insertRow();
            let fCol = document.createElement('th');
            let tCol = document.createTextNode('Observações');
            fCol.setAttribute("id", "obsImport");
            fCol.appendChild(tCol);
            row.appendChild(fCol);
            for (let key of data) {
                let th = document.createElement('th');
                let text = document.createTextNode(key);
                th.appendChild(text);
                row.appendChild(th);
            }
        }

        function generateTableBody(table, data, i) {
            let tbody = table.createTBody();
            tbody.setAttribute("id", "grdBodyImportEx");
            let newRow = tbody.insertRow();
            let fCell = newRow.insertCell();
            let tCell = document.createTextNode('');
            fCell.setAttribute("class", "obsImport"+i+"");
            fCell.appendChild(tCell);
            data.map((row, index) => {
                if (index == 0) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtReportDateImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 1) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "idReferenciaImport"+i+"");
                    newCell.appendChild(newText);
                }
                else if (index == 2) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "nrContainerImport"+i+"");
                    newCell.appendChild(newText);
                }
                else if (index == 3) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dsStatusImport" + i + "");
                    newCell.appendChild(newText);
                }

                else if (index == 4) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtDevolucaoImport" + i + "");
                    newCell.appendChild(newText);
                }

                else if (index == 5) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtDiasDemurrageImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 6) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "vlFaturadoImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 7) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtVencimentoFaturaImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 8) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtPagamentoFaturaImport" + i + "");
                    newCell.appendChild(newText);
                }
                else {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "obsImport"+i+"");
                    newCell.appendChild(newText);
                }
            });
        }

        function generateTableRows(table, data, i) {
            let newRow = table.insertRow();
            let fCell = newRow.insertCell();
            let tCell = document.createTextNode('');
            fCell.setAttribute("class", "obsImport"+i+"");
            fCell.appendChild(tCell);
            data.map((row, index) => {
                if (index == 0) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtReportDateImport"+i+"");
                    newCell.appendChild(newText);
                }
                else if (index == 1) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "idReferenciaImport"+i+"");
                    newCell.appendChild(newText);
                }
                else if (index == 2) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "nrContainerImport"+i+"");
                    newCell.appendChild(newText);
                }
                else if (index == 3) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dsStatusImport" + i + "");
                    newCell.appendChild(newText);
                }

                else if (index == 4) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtDevolucaoImport" + i + "");
                    newCell.appendChild(newText);
                }

                else if (index == 5) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtDiasDemurrageImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 6) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "vlFaturadoImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 7) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtVencimentoFaturaImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 8) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "dtPagamentoFaturaImport" + i + "");
                    newCell.appendChild(newText);
                }
                else if (index == 9) {
                    let newCell = newRow.insertCell();
                    let newText = document.createTextNode(row);
                    newCell.setAttribute("class", "obsImport" + i + "");
                    newCell.appendChild(newText);
                }
            });
        }

        function downloadCSVImport(csv, filename) {
            var csvFile;
            var downloadLink;
            // CSV file
            csvFile = new Blob(["\uFEFF"+csv], { type: "text/csv;charset=utf-8;" });

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

        function GerarCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdImportDemurrage tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdImportDemurrage td, #grdImportDemurrage th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVImport(csv.join("\n"), filename);
        }

        function csvExportTab() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/exportExcel",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdExportDemurrage").empty();
                    $("#grdExportDemurrage").append("<tr><td colspan='20'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#grdExportDemurrage").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdExportDemurrage").append("<tr><td class='text-center'>" + dado[i]["DT_EMBARQUE"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NAVIO"] + "</td><td class='text-center'>" + dado[i]["VIAGEM"] + "</td><td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["HOUSE"] + "</td><td class='text-center'>" + dado[i]["MASTER"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CONSIGNATARIO"] + "</td ><td class='text-center'>" + dado[i]["CNPJ_CONSIGNATARIO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["EXPORTADOR"] + "</td><td class='text-center'>" + dado[i]["CNPJ_EXPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["REFER"] + "</td><td class='text-center'>"+dado[i]["DT_EXPORTACAO"]+"</td></tr> ");
                        }
                    }
                    else {
                        $("#grdExportDemurrage").empty();
                        $("#grdExportDemurrage").append("<tr id='msgEmptyWeek'><td colspan='20' class='alert alert-light text-center'>Tabela Vazia.</td></tr>");
                    }
                }
            });
        }

        function csvExportTabFilter() {
            var inicial = document.getElementById("dtEmbarqueInicial").value;
            var final = document.getElementById("dtEmbarqueFinal").value;
            var regExpo = document.getElementById("MainContent_regExpo");
            var regExpoValue;
            var regNExpo = document.getElementById("MainContent_regNExpo");
            var regNExpoValue;
            if (regExpo.checked) {
                regExpoValue = "1";
            }
            else {
                regExpoValue = "0";
            }

            if (regNExpo.checked) {
                regNExpoValue = "1";
            }
            else {
                regNExpoValue = "0";
            }
           
            console.log(inicial);
            console.log(final);
            if (inicial != "" && final != "") {
                console.log(data_valida(inicial));
                console.log(data_valida(final));
                if (final > inicial) {
                    if (data_valida(inicial) != false && data_valida(final) != false) {
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/exportExcelFiltrada",
                            contentType: "application/json; charset=utf-8",
                            data: "{inicial:'" + inicial + "',final: '" + final + "', regExpo: '" + regExpoValue + "', regNExpo: '" + regNExpoValue + "'}",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdExportDemurrage").empty();
                                $("#grdExportDemurrage").append("<tr><td colspan='20'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                if (dado != null) {
                                    $("#grdExportDemurrage").empty();
                                    for (let i = 0; i < dado.length; i++) {
                                        $("#grdExportDemurrage").append("<tr><td class='text-center'>" + dado[i]["DT_EMBARQUE"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["NAVIO"] + "</td><td class='text-center'>" + dado[i]["VIAGEM"] + "</td><td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["HOUSE"] + "</td><td class='text-center'>" + dado[i]["MASTER"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["CONSIGNATARIO"] + "</td ><td class='text-center'>" + dado[i]["CNPJ_CONSIGNATARIO"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["EXPORTADOR"] + "</td><td class='text-center'>" + dado[i]["CNPJ_EXPORTADOR"] + "</td>" +
                                            "<td class='text-center'>" + dado[i]["REFER"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td></tr> ");
                                    }
                                }
                                else {
                                    $("#grdExportDemurrage").empty();
                                    $("#grdExportDemurrage").append("<tr id='msgEmptyWeek'><td colspan='20' class='alert alert-light text-center'>Tabela Vazia.</td></tr>");
                                }
                            }
                        });
                    }
                    else {
                        alert("Data inválida");
                    }
                }
                else {
                    alert("Data final inferior a inicial")
                }
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/exportExcelFiltrada",
                    contentType: "application/json; charset=utf-8",
                    data: "{inicial:'" + inicial + "',final: '" + final + "', regExpo: '" + regExpoValue + "', regNExpo: '" + regNExpoValue + "'}",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdExportDemurrage").empty();
                        $("#grdExportDemurrage").append("<tr><td colspan='20'><div class='loader text-center'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            $("#grdExportDemurrage").empty();
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdExportDemurrage").append("<tr><td class='text-center'>" + dado[i]["DT_EMBARQUE"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NR_CNTR"] + "</td><td class='text-center'>" + dado[i]["NM_TIPO_CONTAINER"] + "</td><td class='text-center'>" + dado[i]["QT_DIAS_FREETIME"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_DEVOLUCAO_CNTR"] + "</td><td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NAVIO"] + "</td><td class='text-center'>" + dado[i]["VIAGEM"] + "</td><td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["HOUSE"] + "</td><td class='text-center'>" + dado[i]["MASTER"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["CONSIGNATARIO"] + "</td ><td class='text-center'>" + dado[i]["CNPJ_CONSIGNATARIO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["EXPORTADOR"] + "</td><td class='text-center'>" + dado[i]["CNPJ_EXPORTADOR"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["REFER"] + "</td><td class='text-center'>" + dado[i]["DT_EXPORTACAO"] + "</td></tr> ");
                            }
                        }
                        else {
                            $("#grdExportDemurrage").empty();
                            $("#grdExportDemurrage").append("<tr id='msgEmptyWeek'><td colspan='20' class='alert alert-light text-center'>Tabela Vazia.</td></tr>");
                        }
                    }
                });
            }

        }

                
    </script>

</asp:Content>
