<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroGerencial.aspx.cs" Inherits="ABAINFRA.Web.CadastroGerencial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Cadastro Gerencial
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Cadastro Gerencial
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Pais</label>
                                        <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-control" DataTextField="NM_PAIS" DataValueField="ID_PAIS"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Cidade / Provincia</label>
                                        <asp:DropDownList ID="ddlCidade" runat="server" CssClass="form-control" DataTextField="NM_CIDADE" DataValueField="ID_CIDADE"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte</label>
                                        <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataValueField="ID_VIATRANSPORTE"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Sigla Porto / Iata</label>
                                        <asp:DropDownList ID="ddlSigla" runat="server" CssClass="form-control" DataTextField="CD_SIGLA" DataValueField="ID_PORTO"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Nome Porto / Aeroporto</label>
                                        <asp:DropDownList ID="ddlPorto" runat="server" CssClass="form-control" DataTextField="NM_PORTO" DataValueField="ID_PORTO"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="listarPorto()" class="btn btn-primary">Consultar</button>
                                        <button type="button" id="btnGerarCSV" onclick="GerarCSV('Porto.csv')" class="btn btn-primary">Gerar Arquivo CSV</button>
                                        <button type="button" id="btnAdd" data-toggle="modal" data-target="#modalCadastroPorto" class="btn btn-primary">Novo Cadastro   </button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="courrierExport" class="table tablecont tablesorter">
                                    <thead>
                                        <tr>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">&nbsp;</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PAIS</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">CIDADE / PROVINCIA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">VIA TRANSPORTE</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">SIGLA PORTO / IATA</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">PORTO / AEROPORTO</th>
                                            <th class="text-center sorter" scope="col" style="cursor:pointer">ATIVO</th>
                                        </tr>
                                    </thead>
                                    <tbody id="containerCourrier">
                                       
                                    </tbody>
                                </table>
                            </div>
                            <div class="modal fade bd-example-modal-xl" id="modalCadastroPorto" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="modalFiltroAvancadoTitle">Cadastrar Porto / Aeroporto</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Cidade / Provincia</label>
                                                        <asp:DropDownList ID="ddlCidadeCadastro" runat="server" CssClass="form-control" DataTextField="NM_CIDADE" DataValueField="ID_CIDADE"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Via Transporte</label>
                                                        <asp:DropDownList ID="ddlViaTransporteCadastro" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataValueField="ID_VIATRANSPORTE"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Sigla Porto / Iata</label>
                                                        <input type="text" id="txtSiglaPortoCadastro" class="form-control"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Nome Porto / Aeroporto</label>
                                                        <input type="text" id="txtNomePortoCadastro" class="form-control"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnCadastrar" onclick="cadastrarPorto()" data-dismiss="modal" class="btn btn-primary btn-ok">Cadastrar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal fade bd-example-modal-xl" id="modalEditPorto" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalEditTitle">Edita Porto / Aeroporto</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="form-group">
                                                    <input type="hidden" id="idPortoEdit"/>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Cidade / Provincia</label>
                                                        <asp:DropDownList ID="ddlCidadeEdit" runat="server" CssClass="form-control" DataTextField="NM_CIDADE" DataValueField="ID_CIDADE"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Via Transporte</label>
                                                        <asp:DropDownList ID="ddlViaTransporteEdit" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataValueField="ID_VIATRANSPORTE"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Sigla Porto / Iata</label>
                                                        <input type="text" id="txtSiglaPortoEdit" class="form-control"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Nome Porto / Aeroporto</label>
                                                        <input type="text" id="txtNomePortoEdit" class="form-control"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnEditar" onclick="editarPorto()" data-dismiss="modal" class="btn btn-primary btn-ok">Salvar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Sair</button>
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
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function listarPorto() {
            let result = "";
            let dados = {
                "NM_PAIS": document.getElementById("MainContent_ddlPais").value,
                "NM_CIDADE": document.getElementById("MainContent_ddlCidade").value,
                "NM_VIATRANSPORTE": document.getElementById("MainContent_ddlViaTransporte").value,
                "CD_SIGLA": document.getElementById("MainContent_ddlSigla").value,
                "NM_PORTO": document.getElementById("MainContent_ddlPorto").value,
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarPorto",
                data: JSON.stringify({ dados: (dados) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#containerCourrier").empty();
                    $("#containerCourrier").append("<tr><td colspan='6'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#containerCourrier").empty();
                    if (dado.length > 0) {
                        for (let i = 0; i < dado.length; i++) {
                            result += "<tr>";
                            result += "<td class='text-center'><div class='btn btn-primary' data-toggle='modal' data-target='#modalEditPorto' onclick='BuscarPorto(" + dado[i]["ID_PORTO"] + ")'><i class='fas fa-edit'></i></div></td>";
                            result += "<td class='text-center'> " + dado[i]["NM_PAIS"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["NM_CIDADE"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["CD_SIGLA"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["VIATRANSPORTE"] + "</td>";
                            result += "<td class='text-center'> " + dado[i]["NM_PORTO"] + "</td>";
                            result += "<td class='text-center'><input type='checkbox' " + (dado[i]["FL_ATIVO"] ? 'checked': '')  + "/></td>";
                            result += "</tr>";
                        }
                    } else {
                        result += "<tr id='msgEmptyWeek'><td colspan='6' class='alert alert-light text-center'>Tabela vazia.</td></tr>"
                    }
                    $("#containerCourrier").append(result);
                }
            })
        }

        function BuscarPorto(idPorto) {
            let cidade = document.getElementById("MainContent_ddlCidadeEdit");
            let porto = document.getElementById("txtNomePortoEdit");
            let sigla = document.getElementById("txtSiglaPortoEdit");
            let viatransporte = document.getElementById("MainContent_ddlViaTransporteEdit");
            let portoId = document.getElementById("idPortoEdit");
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/buscarPorto",
                data: "{idPorto: " + idPorto + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    cidade.value = dado[0]["ID_CIDADE"];
                    porto.value = dado[0]["NM_PORTO"];
                    sigla.value = dado[0]["CD_SIGLA"];
                    viatransporte.value = dado[0]["ID_VIATRANSPORTE"];
                    portoId.value = idPorto;
                }
            })
        }

        function editarPorto() {
            let dadosEdit = {
                "ID_PORTO": document.getElementById("idPortoEdit").value,
                "NM_CIDADE": document.getElementById("MainContent_ddlCidadeEdit").value,
                "NM_VIATRANSPORTE": document.getElementById("MainContent_ddlViaTransporteEdit").value,
                "CD_SIGLA": document.getElementById("txtSiglaPortoEdit").value,
                "NM_PORTO": document.getElementById("txtNomePortoEdit").value,
            }
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Salvar!',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/EditarPorto",
                        data: JSON.stringify({ dadosEdit: (dadosEdit) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d.success) {
                                Swal.fire('Saved!', '', 'success');
                                // Adicione aqui qualquer lógica adicional após a conclusão bem-sucedida
                            } else {
                                // A operação não foi bem-sucedida, exiba a mensagem de erro
                                Swal.fire('Changes are not saved', response.message, 'info');
                                // Adicione aqui qualquer lógica adicional para tratamento de erro
                            }
                        },
                        error: function (err) {
                            Swal.fire('Changes are not saved', '', 'info')
                        }
                    })
                }
            })
        }

        function cadastrarPorto() {
            let dados = {
                "NM_CIDADE": document.getElementById("MainContent_ddlCidadeCadastro").value,
                "NM_VIATRANSPORTE": document.getElementById("MainContent_ddlViaTransporteCadastro").value,
                "CD_SIGLA": document.getElementById("txtSiglaPortoCadastro").value,
                "NM_PORTO": document.getElementById("txtNomePortoCadastro").value,
            }
            Swal.fire({
                title: 'Você tem certeza?',
                text: "Você não poderá reverter essa ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Salvar!',
                customClass: 'swal-wide'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "DemurrageService.asmx/CadastrarPorto",
                        data: JSON.stringify({ dados: (dados) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d.success) {
                                Swal.fire('Saved!', '', 'success');
                                document.getElementById("MainContent_ddlCidadeCadastro").value = 0;
                                document.getElementById("MainContent_ddlViaTransporteCadastro").value = 0;
                                document.getElementById("txtSiglaPortoCadastro").value = '';
                                document.getElementById("txtNomePortoCadastro").value = '';
                                listarPorto();
                                // Adicione aqui qualquer lógica adicional após a conclusão bem-sucedida
                            } else {
                                // A operação não foi bem-sucedida, exiba a mensagem de erro
                                Swal.fire('Changes are not saved', response.message, 'info');
                                listarPorto();
                                // Adicione aqui qualquer lógica adicional para tratamento de erro
                            }
                        },
                        error: function (err) {
                            Swal.fire('Changes are not saved', '', 'info')
                            listarPorto();
                        }
                    })
                }
            })
        }
    </script>
</asp:Content>
