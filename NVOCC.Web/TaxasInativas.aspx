<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaxasInativas.aspx.cs" Inherits="ABAINFRA.Web.TaxasInativas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Conferencia de Processos
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas Inativas
                            </a>
                        </li>                        
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportTaxas" class="btn btn-primary" onclick="exportGrid('TaxasInativas.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque Inicial (Processo):</label>
                                            <input id="txtDtInicial" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Embarque Final (Processo):</label>
                                            <input id="txtDtFinal" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarTaxas" onclick="ListarTaxas()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnFiltroAvancado" class="btn btn-primary" data-toggle="modal" data-target="#modalFiltroAvancado">Filtro Avançado</button>
                                        <button type="button" id="btnLimparFiltros" onclick="limparFiltro()" class="btn btn-primary">Limpar Filtro</button>
                                    </div>
                                </div> 

                                <div class="modal fade bd-example-modal-xl" id="modalFiltroAvancado" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="modalFiltroAvancadoTitle">Filtro Avançado</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Processo</label>
                                                            <input id="nrProcessoFilter" class="form-control" type="text"/>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label class="control-label">Fornecedor</label>
                                                            <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Tipo Estufagem</label>
                                                            <asp:DropDownList ID="ddlTipoEstufagem" runat="server" CssClass="form-control" DataTextField="NM_TIPO_ESTUFAGEM" DataValueField="ID_TIPO_ESTUFAGEM"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Modal</label>
                                                            <asp:DropDownList ID="ddlModal" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataValueField="ID_VIATRANSPORTE"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Serviço</label>
                                                            <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control" DataTextField="NM_SERVICO" DataValueField="ID_SERVICO"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Agente Internacional</label>
                                                            <asp:DropDownList ID="ddlAgenteInternacional" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Cliente</label>
                                                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" DataTextField="NM_RAZAO" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Item Despesa</label>
                                                            <asp:DropDownList ID="ddlItemDespesa" runat="server" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Tipo de Movimento</label>
                                                            <select id="tpMovimento" class="form-control">
                                                                <option value="" selected>Selecione</option>
                                                                <option value="P">Pagar</option>
                                                                <option value="R">Receber</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Moeda</label>
                                                            <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" DataTextField="SIGLA_MOEDA" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Base Calculo</label>
                                                            <asp:DropDownList ID="ddlBaseCalculo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label class="control-label">Usuário Inativador</label>
                                                            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control" DataTextField="NOME" DataValueField="ID_USUARIO"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" id="btnConsultarFilter" onclick="ListarTaxas()" data-dismiss="modal" class="btn btn-primary btn-ok">Consultar</button>
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdtaxasInativas" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">PROCESSO</th>
                                                <th class="text-center" scope="col">FORNECEDOR</th>
                                                <th class="text-center" scope="col">ESTUFAGEM</th>
                                                <th class="text-center" scope="col">MODAL</th>
                                                <th class="text-center" scope="col">SERVIÇO</th>
                                                <th class="text-center" scope="col">AGENTE</th>
                                                <th class="text-center" scope="col">CLIENTE</th>
                                                <th class="text-center" scope="col">TIPO MOVIMENTO</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">MOEDA</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">BASE DE CALCULO</th>
                                                <th class="text-center" scope="col">USUARIO INATIVAÇÃO</th>
                                                <th class="text-center" scope="col">DATA INATIVAÇÃO</th>
                                                <th class="text-center" scope="col">MOTIVO</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdtaxasInativasBody">

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
        function ListarTaxas() {
            var result = '';
            var dataI = document.getElementById("txtDtInicial").value; 
            var dataF = document.getElementById("txtDtFinal").value;

            var dadosFiltro = {
                "processo": document.getElementById("nrProcessoFilter").value,
                "fornecedor": document.getElementById("MainContent_ddlFornecedor").value,
                "estufagem": document.getElementById("MainContent_ddlTipoEstufagem").value,
                "modal": document.getElementById("MainContent_ddlModal").value,
                "servico": document.getElementById("MainContent_ddlServico").value,
                "agenteinter": document.getElementById("MainContent_ddlAgenteInternacional").value,
                "cliente": document.getElementById("MainContent_ddlCliente").value,
                "itemdespesa": document.getElementById("MainContent_ddlItemDespesa").value,
                "moeda": document.getElementById("MainContent_ddlMoeda").value,
                "basecalculo": document.getElementById("MainContent_ddlBaseCalculo").value,
                "usuario": document.getElementById("MainContent_ddlUsuario").value,
                "datainicial": dataI,
                "datafinal": dataF,
                "tpmovimento": document.getElementById("tpMovimento").value
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarTaxasInativas",
                data: JSON.stringify({ dados: (dadosFiltro) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdtaxasInativasBody").empty();
                    $("#grdtaxasInativasBody").append("<tr><td colspan='15'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdtaxasInativasBody").empty();
                    if (dado != null) {
                        for (var i = 0; i < dado.length; i++) {
                            result += "<tr>";
                            result += "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["FORNECEDOR"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NM_TIPO_ESTUFAGEM"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NM_VIATRANSPORTE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["TP_SERVICO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["AGENTE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["CLIENTE"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["TIPO_MOVIMENTO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["SIGLA_MOEDA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["VL_TAXA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["USUARIO_INATIVACAO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["DATA_INATIVACAO"] + "</td>";
                            result += "<td class='text-center'>" + dado[i]["MOTIVO_INATIVACAO"] + "</td>";
                            result += "</tr>";
                        }
                        $("#grdtaxasInativasBody").append(result);
                    } else {
                        $("#grdtaxasInativasBody").append("<tr><td class='text-center' colspan='15'>Sem Dados</td></tr>");
                    }
                }
            })
        }

        function limparFiltro() {
            document.getElementById("nrProcessoFilter").value = '';
            document.getElementById("MainContent_ddlFornecedor").value = '';
            document.getElementById("MainContent_ddlTipoEstufagem").value = '';
            document.getElementById("MainContent_ddlModal").value = '';
            document.getElementById("MainContent_ddlServico").value = '';
            document.getElementById("MainContent_ddlAgenteInternacional").value = '';
            document.getElementById("MainContent_ddlCliente").value = '';
            document.getElementById("MainContent_ddlItemDespesa").value = '';
            document.getElementById("MainContent_ddlMoeda").value = '';
            document.getElementById("MainContent_ddlBaseCalculo").value = '';
            document.getElementById("MainContent_ddlUsuario").value = '';
            document.getElementById("tpMovimento").value = '';
            ListarTaxas();
        }

        function exportGrid(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdtaxasInativas tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdtaxasInativas td, #grdtaxasInativas th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSV(csv.join("\n"), filename);
        }

        function exportTableToCSV(csv, filename) {
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
