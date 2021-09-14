<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CaixaSaida.aspx.cs" Inherits="ABAINFRA.Web.CaixaSaida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Financeiro
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Caixa de Saída
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg">
                                <div class="alert alert-danger text-center" id="msgErrRemoveEmail">
                                    Somente emails não enviado podem ser removidos.
                                </div>
                                <div class="alert alert-success text-center" id="msgSuccessRemoveEmail">
                                    Email removido com sucesso.
                                </div>
                                <div class="alert alert-danger text-center" id="msgErrReenvioEmail">
                                    Esse email não pode ser reenviado.
                                </div>
                                <div class="alert alert-success text-center" id="msgSuccessReenvioEmail">
                                    Email reenviado com sucesso.
                                </div>
                            </div>
                            <div class="row" style="display: flex;margin:auto">
                                <div style="margin:auto">
                                    <button type="button" id="btnVisualizarEmail" onclick="verificarEmail()" class="btn btn-primary btn-ok">Visualizar E-mail</button>
                                    <button type="button" id="btnRemoverEmail" onclick="verificarRemocaoEmail()" class="btn btn-primary btn-ok">Remover E-mail selecionado</button>
                                    <button type="button" id="btnReenviarEmail" onclick="verificarReenvioEmail()" class="btn btn-primary btn-ok">Reenviar E-mail</button>
                                    <button type="button" id="btnCaixaAgendamento" onclick="listarAgendamento()" class="btn btn-primary btn-ok">Caixa de Agendamento</button>
                                </div>
                            </div>
                            <div class="row topMarg" style="display:flex; align-items: flex-end;">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <select class="form-control" id="ddlFiltroCaixaSaida">
                                            <option value="">Selecione</option>
                                            <option value="1">PROCESSO</option>
                                            <option value="2">CLIENTE</option>
                                            <option value="3">TIPO EMAIL</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"><span class="required">&nbsp</span></label>
                                        <input id="txtConsulta" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div>
                                    <input type="checkbox" id="enviado" name="email_situacao" checked>
                                    <label for="enviado">A Enviar</label><br>
                                    <input type="checkbox" id="todos" name="email_situacao">
                                    <label for="todos">Enviados</label><br>
                                </div>
                                    <div class="col-sm-2">
                                    <div class="form-group">
                                        <label for="ddlGerados">GERADOS</label><br>
                                        <select class="form-control" id="ddlGerados">
                                            <option value="1">HOJE</option>
                                            <option value="2">7 DIAS</option>
                                            <option value="3">30 DIAS</option>
                                            <option value="4">60 DIAS</option>
                                            <option value="5">90 DIAS</option>
                                        </select>
                                    </div>
                                </div>
                                     
                                <div class="form-group">
                                    <button type="button" id="btnFiltrarCaixaSaida" onclick="caixaSaida()" class="btn btn-primary btn-ok">Filtrar</button>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="tblCaixaSaida" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col"></th>
                                            <th class="text-center" scope="col">PROCESSO</th>
                                            <th class="text-center" scope="col">TIPO E-MAIL</th>
                                            <th class="text-center" scope="col">DATA GERAÇÃO</th>
                                            <th class="text-center" scope="col">PREVISÃO ENVIO</th>
                                            <th class="text-center" scope="col">DATA ENVIO</th>
                                            <th class="text-center" scope="col">NOME CLIENTE</th>
                                            <th class="text-center" scope="col">TERMINAL</th>
                                            <th class="text-center" scope="col">PARCEIRO</th>
                                            <th class="text-center" scope="col">OCORRÊNCIA</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblCaixaSaidaBody">
                                                            
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
                                                        <label class="control-label">DATA SOLICITAÇÃO</label>
                                                        <input type="date" id="dtSolicitacao" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">INSIDE</label>
                                                        <asp:DropDownList ID="ddlInside" CssClass="form-control" runat="server" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO">

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">VENDEDOR</label>
                                                        <asp:DropDownList ID="ddlVendedor" CssClass="form-control" runat="server" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" >

                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">CLIENTE PRINCIPAL</label>
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
                                                        <label class="control-label">STATUS</label>
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

                        <div class="modal fade bd-example-modal-xl" id="modalVisualizarEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalDevolucaoTitle">Visualização de E-mail</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body" id="visualizarEmail" style="padding:30px;">
                                        
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade bd-example-modal-xl" id="modalCaixaAgendamento" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalCaixaAgendamentoTitle">Caixa de Agendamento</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="alert alert-success text-center" id="msgSuccessCancelEmail">
                                            Agendamento cancelado com sucesso
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrSelectEmail">
                                            Selecione um Email.
                                        </div>
                                        <div class="alert alert-danger text-center" id="msgErrorCancelEmail">
                                            Falha ao cancelar agendamento
                                        </div>
                                        <div class="alert alert-success text-center" id="msgSuccessAgendEmail">
                                            Agendamento Reativado com sucesso.
                                        </div>
                                        <div class="row" style="display: flex;margin:auto">
                                            <div style="margin:auto">
                                                <button type="button" id="btnCancelarAgendamento" onclick="cancelarAgendamento()" class="btn btn-primary btn-ok">Cancelar E-mail Agendado</button>                                                
                                            </div>
                                        </div>
                                        <div class="row topMarg" style="display:flex; align-items: flex-end;">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Consultar por:<span class="required">*</span></label>
                                                    <select class="form-control" id="ddlFiltroCaixaAgendamento">
                                                        <option value="">Selecione</option>
                                                        <option value="1">PROCESSO</option>
                                                        <option value="2">CLIENTE</option>
                                                        <option value="3">TIPO EMAIL</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label"><span class="required">&nbsp</span></label>
                                                    <input id="txtConsultaAgendamento" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div>
                                                <input type="checkbox" id="enviadoAg" name="email_situacao" checked>
                                                <label for="enviado">A Enviar</label><br>
                                                <input type="checkbox" id="todosAg" name="email_situacao">
                                                <label for="todos">Enviados</label><br>
                                            </div>
                                             <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label for="ddlGerados">GERADOS</label><br>
                                                    <select class="form-control" id="ddlGeradosAg">
                                                        <option value="1">HOJE</option>
                                                        <option value="2">7 DIAS</option>
                                                        <option value="3">30 DIAS</option>
                                                        <option value="4">90 DIAS</option>
                                                        <option value="5">180 DIAS</option>
                                                    </select>
                                                </div>
                                            </div>
                                     
                                            <div class="form-group">
                                                <button type="button" id="btnFiltrarCaixaAgendamento" onclick="listarAgendamento()" class="btn btn-primary btn-ok">Filtrar</button>
                                            </div>
                                        </div>
                                        <div class="table-responsive tableFixHead topMarg" style="height: 300px;">
                                            <table id="tblCaixaAgendamento" class="table tablecont">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" scope="col"></th>
                                                        <th class="text-center" scope="col">PROCESSO</th>
                                                        <th class="text-center" scope="col">MBL</th>
                                                        <th class="text-center" scope="col">TIPO E-MAIL</th>
                                                        <th class="text-center" scope="col">DATA GERAÇÃO</th>
                                                        <th class="text-center" scope="col">PREVISÃO ENVIO</th>
                                                        <th class="text-center" scope="col">DATA ENVIO</th>
                                                        <th class="text-center" scope="col">DATA CANCELAMENTO</th>
                                                        <th class="text-center" scope="col">MOTIVO CANCELAMENTO</th>
                                                        <th class="text-center" scope="col">CLIENTE</th>
                                                        <th class="text-center" scope="col">TERMINAL</th>
                                                        <th class="text-center" scope="col">PARCEIRO</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblCaixaAgendamentoBody">
                                                            
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalCancelamentoEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalCancelamentoEmailTitle">Cancelar Agendamento</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgCancelamento">Deseja Cancelar esse agendamento?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnExcluirS" onclick="cancelarAgen()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalReativarEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalReativarEmailTitle">Reativar Agendamento</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgReativar">Deseja Reativar esse agendamento?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnReativarS" onclick="reatviarAgend()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalRemoverEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalRemoverEmailTitle">Remover Email</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgRemover">Deseja Remover esse email?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnRemoverS" onclick="removerEmail()" class="btn btn-primary btn-ok">Sim</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade bd-example-modal-lg" id="modalReenvioEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalReenvioEmailTitle">Reenviar Email</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <h3 id="msgReenvio">Deseja Reenviar esse email?</h3>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnReenvioS" onclick="reenviarEmail()" class="btn btn-primary btn-ok">Sim</button>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script>

        $(document).ready(function () {
            caixaSaida();
        });

        var id = 0;
        var idEmailCaixa = 0;
        var idEmailAgendamento = 0;

        function caixaSaida() {
            $("#modalCaixaSaida").modal("show");
            var filtro = document.getElementById("ddlFiltroCaixaSaida").value;
            var consulta = document.getElementById("txtConsulta").value;
            var enviado = document.getElementById("enviado");
            var nenviado = document.getElementById("todos");
            var dtgerado = document.getElementById("ddlGerados").value;
            var enviadov;
            var enviadon;

            if (enviado.checked) {
                enviadov = "1";
            } else {
                enviadov = "0";
            }

            if (nenviado.checked) {
                enviadon = "1";
            } else {
                enviadon = "0";
            }

            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarEmailFinanceiro",
                data: '{filtro: "' + filtro + '", consulta: "' + consulta + '", enviado: "' + enviadov + '", nenviado: "' + enviadon + '", dtgerado: "' + dtgerado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblCaixaSaidaBody").empty();
                    $("#tblCaixaSaidaBody").append("<tr><td colspan='10'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#tblCaixaSaidaBody").empty();
                    if (dado != null) {
                        for (var i = 0; i < dado.length; i++) {
                            $("#tblCaixaSaidaBody").append("<tr data-id='" + dado[i]["IDEMAIL"] + "'><td class='text-center'>" +
                                "<div class='btn btn-primary select' onclick='setIdListaEmailCaixaSaida(" + dado[i]["IDEMAIL"] + ")'>Selecionar</div></td >" +
                                "<td class='text-center'>" + dado[i]["PROCESSO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NMTIPOAVISO"] + "' style='max-width: 20ch;'>" + dado[i]["NMTIPOAVISO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_GERACAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PREVISAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_ENVIO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 20ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["IDARMAZEM"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["PARCEIRO"] + "' style='max-width: 20ch;'>" + dado[i]["PARCEIRO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["OCORRENCIA"] + "</td>" +
                                "</tr> ");
                        }
                    } else {
                        $("#tblCaixaSaidaBody").append("<tr><td id='msgEmptyDemurrageContainer' colspan='10' class='alert alert-light text-center'>Email não encontrado</td></tr>");
                    }
                }
            })
        }

        function cancelarAgen() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/cancelarAgendamento",
                data: '{idEmail: "' + idEmailAgendamento + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    if (dado.d == "ok") {
                        $("#modalCancelamentoEmail").modal("hide");
                        $("#msgSuccessCancelEmail").fadeIn(500).delay(1000).fadeOut(500);
                        listarAgendamento();
                    } else {
                        $("#modalCancelamentoEmail").modal("hide");
                        $("#msgErrorCancelEmail").fadeIn(500).delay(1000).fadeOut(500);
                        listarAgendamento();
                    }
                }
            })
        }

        function reatviarAgend() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/reativarAgendamento",
                data: '{idEmail: "' + idEmailAgendamento + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    if (dado.d == "ok") {
                        $("#modalReativarEmail").modal("hide");
                        $("#msgSuccessAgendEmail").fadeIn(500).delay(1000).fadeOut(500);
                        listarAgendamento();
                    }
                }
            })
        }

        function verificarRemocaoEmail() {
            if (idEmailCaixa != 0) {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/verificarRemocao",
                    data: '{idProcesso: "' + idEmailCaixa + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado == "ok") {
                            $("#modalRemoverEmail").modal('show');
                        } else {
                            $("#msgErrRemoveEmail").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function verificarEmail() {
            if (idEmailCaixa != 0) {
                $("#modalVisualizarEmail").modal("show");
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/visualizarEmail",
                    data: '{idProcesso: "' + idEmailCaixa + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        $("#visualizarEmail").empty();
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            console.log(dado[0]["ASSUNTO"]);
                            document.getElementById("visualizarEmail").insertAdjacentHTML('afterbegin', dado[0]["ASSUNTO"] + "<br>" + dado[0]["CORPO"]);
                        }
                    }
                })
            }
        }

        function verificarReenvioEmail() {
            if (idEmailCaixa != 0) {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/verificarReenvio",
                    data: '{idProcesso: "' + idEmailCaixa + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado == "ok") {
                            $("#modalReenvioEmail").modal('show');
                        } else {
                            $("#msgErrReenvioEmail").fadeIn(500).delay(1000).fadeOut(500);
                        }
                    }
                })
            }
        }

        function reenviarEmail() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/reenviarEmail",
                data: '{idProcesso: "' + idEmailCaixa + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        $("#modalReenvioEmail").modal("hide");
                        $("#msgSuccessReenvioEmail").fadeIn(500).delay(1000).fadeOut(500);
                        caixaSaida();
                    }
                }
            })
        }

        function removerEmail() {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/removerEmail",
                data: '{idProcesso: "' + idEmailCaixa + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado == "ok") {
                        $("#modalRemoverEmail").modal('hide');
                        $("#msgSuccessRemoveEmail").fadeIn(500).delay(1000).fadeOut(500);
                        caixaSaida();
                    }
                }
            })
        }

        function listarAgendamento() {
            $("#modalCaixaAgendamento").modal("show");
            var filtro = document.getElementById("ddlFiltroCaixaAgendamento").value;
            var consulta = document.getElementById("txtConsultaAgendamento").value;
            var enviado = document.getElementById("enviadoAg");
            var aenviar = document.getElementById("todosAg");
            var dtgerado = document.getElementById("ddlGeradosAg").value;
            var enviadoAg;
            var aenviarAg;

            if (enviado.checked) {
                enviadoAg = "1";
            } else {
                enviadoAg = "0";
            }

            if (aenviar.checked) {
                aenviarAg = "1";
            } else {
                aenviarAg = "0";
            }
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarEmailAgendadoFinanceiro",
                data: '{filtro: "' + filtro + '", consulta: "' + consulta + '", enviado: "' + enviadoAg + '", nenviado: "' + aenviarAg + '", dtgerado: "' + dtgerado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#tblCaixaAgendamentoBody").empty();
                    $("#tblCaixaAgendamentoBody").append("<tr><td colspan='12'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#tblCaixaAgendamentoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#tblCaixaAgendamentoBody").append("<tr data-id='" + dado[i]["ID_SOLICITACAO_EMAIL"] + "'><td class='text-center'>" +
                                "<div class='btn btn-primary select' onclick='setIdListaEmailCaixaAgendamento(" + dado[i]["ID_SOLICITACAO_EMAIL"] + ")'>Selecionar</div></td >" +
                                "<td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NR_BL"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["NMTIPOAVISO"] + "' style='max-width: 20ch;'>" + dado[i]["NMTIPOAVISO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_SOLICITACAO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_START"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_GERACAO_EMAIL"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DT_CANCELAMENTO"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["OB_CANCELAMENTO"] + "</td>" +
                                "<td class='text-center' title='" + dado[i]["CLIENTE"] + "' style='max-width: 15ch;'>" + dado[i]["CLIENTE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["IDARMAZEM"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["PARCEIRO"] + "</td></tr > ");
                        }
                    } else {
                        $("#tblCaixaAgendamentoBody").append("<tr><td id='msgEmptyDemurrageContainer' colspan='12' class='alert alert-light text-center'>Email não encontrado</td></tr>");
                    }
                }
            })
        }

        function setIdListaEmailCaixaSaida(Id) {
            idEmailCaixa = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }

        function setIdListaEmailCaixaAgendamento(Id) {
            idEmailAgendamento = Id;
            $('[data-id]').removeClass("colorir");
            if ($('[data-id="' + Id + '"]').hasClass('colorir')) {
                $('[data-id="' + Id + '"]').removeClass("colorir");
            }
            else {
                $('[data-id="' + Id + '"]').addClass("colorir");
            }
        }
    </script>
</asp:Content>
