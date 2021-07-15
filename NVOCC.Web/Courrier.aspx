<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courrier.aspx.cs" Inherits="ABAINFRA.Web.Courrier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Controle de Courrier
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Courrier
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"><span class="required">&nbsp</span></label>
                                        <input id="txtConsulta" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="listarCourrier()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSV" onclick="GerarCSV('Courrier.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="alert alert-success text-center" id="msgSuccessUpdate">
                                        Courrier atualizado com sucesso.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrUpdate">
                                        Erro ao atualizar courrier.
                                    </div>
                                </div>
                            </div>
                            <div class="row topMarg">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagem" ID="chkFCL" runat="server" CssClass="form-control noborder" Text="&nbsp;FCL"></asp:RadioButton>
                                    </div>
                                    <div>
                                        <asp:RadioButton GroupName="tipoEstufagem" ID="chkLCL" runat="server" CssClass="form-control noborder" Checked="true" Text="&nbsp;LCL"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExport" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">&nbsp;</th>
                                            <th class="text-center" scope="col">PROCESSO</th>
                                            <th class="text-center" scope="col">MBL</th>
                                            <th class="text-center" scope="col">HBL</th>
                                            <th class="text-center" scope="col">CLIENTE</th>
                                            <th class="text-center" scope="col">DATA RECEBIMENTO MBL</th>
                                            <th class="text-center" scope="col">CÓD RASTREAMENTO MBL</th>
                                            <th class="text-center" scope="col">DATA RECEBIMENTO HBL</th>
                                            <th class="text-center" scope="col">CÓD RASTREAMENTO HBL</th>
                                            <th class="text-center" scope="col">DATA RETIRADA</th>
                                            <th class="text-center" scope="col">RETIRADO POR</th>
                                            <th class="text-center" scope="col">AGENTE</th>
                                            <th class="text-center" scope="col">NAVIO</th>
                                            <th class="text-center" scope="col">PREVISÃO CHEGADA</th>
                                            <th class="text-center" scope="col">DATA CHEGADA</th>
                                            <th class="text-center" scope="col">FATURA</th>
                                            <th class="text-center" scope="col">TIPO</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrier">
                                       
                                    </tbody>
                                </table>
                            </div>
                            <div class="modal fade bd-example-modal-lg" id="modalEditCourrier" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalFCLexpoTitle">Alterar Courrier</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body" style="padding:20px">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">PROCESSO:</label>
                                                        <input id="nrprocesso" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">CLIENTE:</label>
                                                        <input id="nmcliente" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">MBL:</label>
                                                        <input id="idmbl" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">HBL:</label>
                                                        <input id="idhbl" class="nobox" type="text" disabled="disabled"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 col-sm-offset-9">
                                                    <div class="form-group">
                                                        <input type="checkbox" id="checkOrigem" name="Origem" onchange="flagOrigem()"/>
                                                        <label for="Origem">Origem</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Recebimento MBL</label>
                                                        <input id="dtRecebimentoMBL" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Código Rastreamento MBL</label>
                                                        <input id="cdRastreamentoMBL" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data Recebimento HBL</label>
                                                        <input id="dtRecebimentoHBL" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Código Rastreamento HBL</label>
                                                        <input id="cdRastreamentoHBL" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Data da Retirada</label>
                                                        <input id="dtRetirada" class="form-control" type="date" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Retirado Por</label>
                                                        <input id="receptor" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Nº da Fatura</label>
                                                        <input id="nrFatura" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnEditarCourrier" class="btn btn-success">Salvar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
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
    <script>
        var idFiltro;
        var stringConsulta;
        var tipo;
        var tipoValue;
        var idblC;
        $(document).ready(function () {
            listarCourrier();
        });

       

        function BuscarCourrier(id) {
            idblC = id;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscarCourrier",
                data: '{id: "'+id+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var data = dado.d;
                    data = $.parseJSON(data);
                    document.getElementById('nrprocesso').value = data.NR_PROCESSO;
                    document.getElementById('nmcliente').value = data.NM_RAZAO;
                    document.getElementById('idmbl').value = data.NR_BL_MASTER;
                    document.getElementById('idhbl').value = data.NR_BL;
                    document.getElementById('dtRecebimentoMBL').value = data.DT_RECEBIMENTO_MBL;
                    document.getElementById('cdRastreamentoMBL').value = data.CD_RASTREAMENTO_MBL;
                    document.getElementById('dtRecebimentoHBL').value = data.DT_RECEBIMENTO_HBL;
                    document.getElementById('cdRastreamentoHBL').value = data.CD_RASTREAMENTO_HBL;
                    document.getElementById('dtRetirada').value = data.DT_RETIRADA_COURRIER;
                    document.getElementById('receptor').value = data.NM_RETIRADO_POR_COURRIER;
                    document.getElementById('nrFatura').value = data.NR_FATURA_COURRIER;
                    if (data.CD_RASTREAMENTO_HBL == "ORIGEM") {
                        document.getElementById('dtRecebimentoHBL').disabled = true;
                        document.getElementById('cdRastreamentoHBL').disabled = true;
                        document.getElementById('dtRetirada').disabled = true;
                        document.getElementById('receptor').disabled = true;
                        document.getElementById("checkOrigem").checked = true;
                        document.getElementById("checkOrigem").disabled = true;
                    } else {
                        document.getElementById('dtRecebimentoHBL').disabled = false;
                        document.getElementById('cdRastreamentoHBL').disabled = false;
                        document.getElementById('dtRetirada').disabled = false;
                        document.getElementById('receptor').disabled = false;
                        document.getElementById("checkOrigem").checked = false;
                        document.getElementById("checkOrigem").disabled = false;
                    }
                }
            })
        }

        function flagOrigem() {
            if (document.getElementById("checkOrigem").checked) {
                document.getElementById('dtRecebimentoHBL').disabled = true;
                document.getElementById('cdRastreamentoHBL').disabled = true;
                document.getElementById('dtRetirada').disabled = true;
                document.getElementById('receptor').disabled = true;
                document.getElementById('cdRastreamentoHBL').value = "ORIGEM";
            } else{
                document.getElementById('dtRecebimentoHBL').disabled = false;
                document.getElementById('cdRastreamentoHBL').disabled = false;
                document.getElementById('dtRetirada').disabled = false;
                document.getElementById('receptor').disabled = false;
                document.getElementById('cdRastreamentoHBL').value = "";
            }
        }

        function listarCourrier() {
            idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            stringConsulta = document.getElementById("txtConsulta").value;
            tipo = document.getElementById("MainContent_chkFCL");
            tipoValue;
            if (tipo.checked) {
                tipoValue = "1";
            }
            else {
                tipoValue = "0";
            }

            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarCourrier",
                contentType: "application/json; charset=utf-8",
                data: '{idFilter:"' + idFiltro + '", Filter:"' + stringConsulta + '", tipo: "' + tipoValue + '"}',
                dataType: "json",
                beforeSend: function () {
                    $("#containerCourrier").empty();
                    $("#containerCourrier").append("<tr><td colspan='17'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        $("#containerCourrier").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#containerCourrier").append("<tr><td class='text-center'><div class='btn btn-primary' data-toggle='modal' data-target='#modalEditCourrier' onclick='BuscarCourrier(" + dado[i]["ID_BL"] + ")'><i class='fas fa-edit'></i></div></td>" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["MASTER"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["HOUSE"] + "</td><td class='text-center' title='" + dado[i]["CLIENTE"] +"' style='max-width: 14ch;'>" + dado[i]["CLIENTE"] + "</td><td class='text-center'>" + dado[i]["DT_RECEBIMENTO_MBL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["CD_RASTREAMENTO_MBL"] + "</td><td class='text-center'>" + dado[i]["DT_RECEBIMENTO_HBL"] + "</td><td class='text-center'>" + dado[i]["CD_RASTREAMENTO_HBL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_RETIRADA_COURRIER"] + "</td><td class='text-center'>" + dado[i]["NM_RETIRADO_POR_COURRIER"] + "</td><td class='text-center' title='" + dado[i]["AGENTE"] +"' style='max-width: 14ch;'>" + dado[i]["AGENTE"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NM_NAVIO"] +"' style='max-width: 14ch;'>" + dado[i]["NM_NAVIO"] + "</td><td class='text-center'>" + dado[i]["DT_PREVISAO_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["DT_CHEGADA"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_FATURA_COURRIER"] + "</td > <td class='text-center'>" + dado[i]["NM_TIPO_ESTUFAGEM"] + "</td>" +
                                "</tr>");
                        }
                    }
                    else {
                        $("#containerCourrier").empty();
                        $("#containerCourrier").append("<tr id='msgEmptyWeek'><td colspan='17' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        }

        $("#btnEditarCourrier").click(function () {
            $("#modalEditCourrier").modal("hide");
            idFiltro = document.getElementById("MainContent_ddlFiltro").value;
            stringConsulta = document.getElementById("txtConsulta").value;
            tipo = document.getElementById("MainContent_chkFCL");
            tipoValue;
            if (tipo.checked) {
                tipoValue = "1";
            }
            else {
                tipoValue = "0";
            }
            var dadoEdit = {
                "ID_BL": idblC,
                "NR_BL_MASTER": document.getElementById("idmbl").value,
                "NR_BL": document.getElementById('idhbl').value,
                "DT_RECEBIMENTO_MBL": document.getElementById('dtRecebimentoMBL').value,
                "CD_RASTREAMENTO_MBL": document.getElementById('cdRastreamentoMBL').value,
                "DT_RECEBIMENTO_HBL": document.getElementById('dtRecebimentoHBL').value,
                "CD_RASTREAMENTO_HBL": document.getElementById('cdRastreamentoHBL').value,
                "DT_RETIRADA_COURRIER": document.getElementById('dtRetirada').value,
                "NM_RETIRADO_POR_COURRIER": document.getElementById('receptor').value,
                "NR_FATURA_COURRIER": document.getElementById('nrFatura').value
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/editarCourrier",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "1") {
                        $("#msgSuccessUpdate").fadeIn(500).delay(1000).fadeOut(500);
                        listarCourrier();
                    }
                    else {
                        $("#msgErrUpdate").fadeIn(500).delay(1000).fadeOut(500);
                        $("#containerCourrier").empty();
                        listarCourrier();
                    }
                }
            })
        })

        function downloadCSVImport(csv, filename) {
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

        function GerarCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#courrierExport tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#courrierExport td, #courrierExport th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSVImport(csv.join("\n"), filename);
        }
    </script>
</asp:Content>
