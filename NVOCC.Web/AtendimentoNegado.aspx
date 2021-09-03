<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AtendimentoNegado.aspx.cs" Inherits="ABAINFRA.Web.AtendimentoNegado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Atendimento Declinado
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Atendimento
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv" style="margin: auto; justify-content: center;">
                                <button type="button" id="btnCadastrarDeclinio" data-toggle="modal" data-target="#modalCadastroDeclinio" class="btn btn-primary">Novo Cadastro</button>
                                <button type="button" id="btnGerarCSV" style="margin-left: 5px;" class="btn btn-primary" onclick="exportTableToCSVAtual('AtendimentosNegados.csv')">Gerar CSV</button>
                            </div>
                            <div class="row topMarg">
                                <div class="alert alert-success text-center" id="msgSuccess">
                                        Atendimento registrado com sucesso.
                                </div>
                                <div class="alert alert-danger text-center" id="msgErr">
                                        Preencha todos os campos Obrigatórios
                                </div>
                                <div class="alert alert-success text-center" id="msgSuccessDelete">
                                        Atendimento deletado com sucesso.
                                </div>
                                <div class="alert alert-danger text-center" id="msgErrDelete">
                                        Erro ao deletar registro
                                </div>
                            </div>
                            <div class="row flexdiv topMarg" style="padding: 0 15px">
                                <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Solicitação Inicial:</label>
                                            <input id="txtDtInicial" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Solicitação Final:</label>
                                            <input id="txtDtFinal" class="form-control" type="date" required="required"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilter" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1">Vendedor</option>
                                                <option value="2">Inside</option>
                                                <option value="3">Cliente</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtConsulta" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultar" onclick="listarAtendimentos()" class="btn btn-primary">Consultar</button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="atendimentoDeclinio" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">&nbsp;</th>
                                            <th class="text-center" scope="col">DATA SOLICITAÇÃO</th>
                                            <th class="text-center" scope="col">INSIDE</th>
                                            <th class="text-center" scope="col">SERVICO</th>
                                            <th class="text-center" scope="col">ESTUFAGEM</th>
                                            <th class="text-center" scope="col">INCOTERM</th>
                                            <th class="text-center" scope="col">CLIENTE PRINCIPAL</th>
                                            <th class="text-center" scope="col">ORIGEM</th>
                                            <th class="text-center" scope="col">DESTINO</th>
                                            <th class="text-center" scope="col">VENDEDOR</th>
                                            <th class="text-center" scope="col">STATUS</th>
                                        </tr>
                                    </thead>
                                    <tbody id="atendimentoDeclinioBody">
                                       
                                    </tbody>
                                </table>
                            </div>

                            <div class="modal fade bd-example-modal-xl" id="modalCadastroDeclinio" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalFiltroAvancadoTitle">Cadastro de Declínio</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">   
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DATA SOLICITAÇÃO<span style="color:red">*</span></label>
                                                        <input type="date" id="dtSolicitacao" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">INSIDE<span style="color:red">*</span></label>
                                                        <asp:DropDownList ID="ddlInside" CssClass="form-control" runat="server" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">VENDEDOR<span style="color:red">*</span></label>
                                                        <asp:DropDownList ID="ddlVendedor" CssClass="form-control" runat="server" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" >

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CLIENTE PRINCIPAL<span style="color:red">*</span></label>
                                                        <select id="ddlClientePrincipal" class="form-control" onchange="ClienteFinal()" >
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">TIPO SERVICO</label>
                                                        <asp:DropDownList ID="ddlTipoServico" CssClass="form-control" runat="server" DataValueField="ID_SERVICO" DataTextField="NM_SERVICO">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">TIPO ESTUFAGEM</label>
                                                        <asp:DropDownList ID="ddlTipoEstufagem" CssClass="form-control" runat="server" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">ORIGEM</label>
                                                        <asp:DropDownList ID="ddlOrigem" CssClass="form-control" runat="server" DataValueField="ID_PORTO" DataTextField="NM_PORTO">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">DESTINO</label>
                                                        <asp:DropDownList ID="ddlDestino" CssClass="form-control" runat="server" DataValueField="ID_PORTO" DataTextField="NM_PORTO">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="control-label">INCOTERM</label>
                                                        <asp:DropDownList ID="ddlIncoterm" CssClass="form-control" runat="server" DataValueField="ID_INCOTERM" DataTextField="NM_INCOTERM">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">STATUS<span style="color:red">*</span></label>
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" DataValueField="ID_STATUS_COTACAO" DataTextField="NM_STATUS_COTACAO">
                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnConsultarFilter" onclick="CadastrarAtendimento()" data-dismiss="modal" class="btn btn-primary btn-ok">Cadastrar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal fade" id="modalDeleteDeclinio" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalDeleteDemurrageTitle">Excluir Registro</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            Tem certeza que deseja excluir o registro?
                                        </div>
                                        <div class="modal-footer">
                                            <input type="hidden" id="deletar-id">
                                            <button type="button" id="btnDeletar" onclick="deletarAtendimento()" data-dismiss="modal" class="btn btn-primary btn-ok">Sim</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
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
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script>
        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate());
        var id = 0;
        $(document).ready(function () {
            document.getElementById("dtSolicitacao").value = formatDate(currentDate);
            document.getElementById("MainContent_ddlStatus").value = "11";
            document.getElementById("txtDtInicial").value = formatDate(currentDate);
            document.getElementById("txtDtFinal").value = formatDate(currentDate);
            listarAtendimentos();
        });

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

        $("#MainContent_ddlVendedor").change(function () {
            Cliente();
            $("#ddlSubCliente").empty();
        })

        function Cliente() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/CarregarCliente",
                data: '{idvendedor:"' + document.getElementById("MainContent_ddlVendedor").value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#ddlClientePrincipal").empty();
                    $("#ddlClientePrincipal").append("<option value=''>Selecione</option>");
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#ddlClientePrincipal").append("<option value='" + dado[i]["id_parceiro"] + "'>" + dado[i]["NM_RAZAO"] + "</option>");
                        }
                    }
                    else {
                        $("#ddlClientePrincipal").empty();
                    }
                }
            })
        }

        function listarAtendimentos() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarAtendimento",
                data: '{dataI: "' + document.getElementById("txtDtInicial").value + '", dataF: "' + document.getElementById("txtDtFinal").value + '", filter: "' + document.getElementById("ddlFilter").value + '", text: "' + document.getElementById("txtConsulta").value+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#atendimentoDeclinioBody").empty();
                    $("#atendimentoDeclinioBody").append("<tr><td colspan='10'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#atendimentoDeclinioBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#atendimentoDeclinioBody").append("<tr><td class='text-center'>" +
                                "<div class='btn btn-primary' data-toggle='modal' data-target='#modalDeleteDeclinio' title='Deleter Atendimento' data-id='" + dado[i]["ID_ATENDIMENTO_NEGADO"] + "' onclick='setId(" + dado[i]["ID_ATENDIMENTO_NEGADO"] + ")'><i class='fas fa-trash'></i></div>" +
                                "</td>"+
                                "<td class='text-center'>" + dado[i]["ATENDIMENTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["INSIDE"] + "</td >" +
                                "<td class='text-center'>" + dado[i]["SERVICO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ESTUFAGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["INCOTERM"] + "</td>" +
                                "<td class='text-center' style='max-width: 25ch' title='" + dado[i]["CLIENTE"] + "'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ORIGEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DESTINO"] + "</td>" +
                                "<td class='text-center' style='max-width: 25ch' title='" + dado[i]["VENDEDOR"] + "'>" + dado[i]["VENDEDOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["STATUS"] + "</td>" +
                                "</tr>");
                        }
                    }
                    else {
                        $("#atendimentoDeclinioBody").append("<tr id='msgEmptyWeek'><td colspan='10' class='alert alert-light text-center'>Resultado não encontrado</td></tr>");
                    }
                }
            })
        }

        function CadastrarAtendimento() {
            var dados = {
                "DT_ATENDIMENTO_NEGADO": document.getElementById("dtSolicitacao").value,
                "ID_PARCEIRO_INSIDE": document.getElementById("MainContent_ddlInside").value,
                "ID_VENDEDOR": document.getElementById("MainContent_ddlVendedor").value,
                "ID_PARCEIRO_CLIENTE": document.getElementById("ddlClientePrincipal").value,
                "ID_SERVICO": document.getElementById("MainContent_ddlTipoServico").value,
                "ID_TIPO_ESTUFAGEM": document.getElementById("MainContent_ddlTipoEstufagem").value,
                "ID_PORTO_ORIGEM": document.getElementById("MainContent_ddlOrigem").value,
                "ID_PORTO_DESTINO": document.getElementById("MainContent_ddlDestino").value,
                "ID_INCOTERM": document.getElementById("MainContent_ddlIncoterm").value,
                "ID_STATUS": document.getElementById("MainContent_ddlStatus").value
            }
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/CadastrarAtendimento",
                data: JSON.stringify({ dados: (dados) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != "0") {
                        $("#modalCadastroDeclinio").hide();
                        $("#msgSuccess").fadeIn(500).delay(1000).fadeOut(500);
                        listarAtendimentos();
                    }
                    else {
                        $("#modalCadastroDeclinio").hide();
                        $("#msgErr").fadeIn(500).delay(1000).fadeOut(500);
                        listarAtendimentos();
                    }
                }
            })
        }

        function deletarAtendimento() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/deletarAtendimento",
                data: '{id: "'+id+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "0") {
                        $("#modalDeleteDeclinio").hide();
                        $("#msgSuccessDelete").fadeIn(500).delay(1000).fadeOut(500);
                        listarAtendimentos();
                    }
                    else {
                        $("#modalDeleteDeclinio").hide();
                        $("#msgErrDelete").fadeIn(500).delay(1000).fadeOut(500);
                        listarAtendimentos();
                    }
                }
            })
        }

        function downloadCSV(csv, filename) {
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

        function exportTableToCSVAtual(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#atendimentoDeclinio tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#atendimentoDeclinio td, #atendimentoDeclinio th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            downloadCSV(csv.join("\n"), filename);
        }

        function setId(Id) {
            id = Id;
        }
    </script>
</asp:Content>
