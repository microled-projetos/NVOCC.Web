<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaxaParceiroAgenteInternacional.aspx.cs" Inherits="ABAINFRA.Web.TaxaParceiroAgenteInternacional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Taxas Parceiros
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv" style="margin: auto; justify-content: center;">
                                <button type="button" id="btnNovaTaxa" data-toggle="modal" data-target="#modal" class="btn btn-primary" onclick="Disable()">Novo Cadastro</button>
                                <button type="button" id="btnLimparFiltro" style="margin-left: 5px;" class="btn btn-primary" onclick="LimparFiltro()">Limpar Filtros</button>
                                <button type="button" id="btnObsGeral" data-toggle="modal" data-target="#modalObsGeral" style="margin-left: 5px;" class="btn btn-primary" onclick="ObsGeral()">Observação Geral</button>
                            </div>
                            <div class="row topMarg">
                                <div class="alert alert-success text-center" id="msgSuccess">
                                        Taxa registrada com sucesso.
                                </div>
                                <div class="alert alert-danger text-center" id="msgErr">
                                        Preencha todos os campos Obrigatórios
                                </div>
                                <div class="alert alert-success text-center" id="msgSuccessDelete">
                                        Taxa deletada com sucesso.
                                </div>
                                <div class="alert alert-danger text-center" id="msgErrDelete">
                                        Erro ao deletar taxa
                                </div>
                            </div>
                            <div class="row flex text-center">
                                <div class="form-group">
                                    <label class="control-label">Nome Parceiro</label>
                                    <!--<asp:TextBox ID="txtParceiro" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>-->
                                    <asp:Label ID="txtParceiros" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="row flexdiv topMarg" style="padding: 0 15px">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Estufagem</label>
                                        <select id="ddlFilter" class="form-control">
                                            <option value="0" selected>Selecione</option>
                                            <option value="1">FCL</option>
                                            <option value="2">LCL</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Modal</label>
                                        <select id="ddlFilterModal" class="form-control">
                                            <option value="0" selected>Selecione</option>
                                            <option value="1">Maritimo</option>
                                            <option value="4">Aereo</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Serviço</label>
                                        <select id="ddlFilterComex" class="form-control">
                                            <option value="0" selected>Selecione</option>
                                            <option value="1">Importação</option>
                                            <option value="2">Exportação</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <button type="button" id="btnConsultar" onclick="CarregarLista()" class="btn btn-primary">Consultar</button>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead">
                                <table class="table">
                                    <thead>
                                    <tr>
                                        <th class="text-align" scope="col">#</th>
                                        <th class="text-align" scope="col">Id Taxa</th>
                                        <th class="text-align" scope="col">Item Despesa</th>
                                        <th class="text-align" scope="col">Place of Receipt</th>
                                        <th class="text-align" scope="col">POL / AOL</th>
                                        <th class="text-align" scope="col">POD / AOD</th>
                                        <th class="text-align" scope="col">INCOTERM</th>
                                        <th class="text-align" scope="col">Base Cálculo</th>
                                        <th class="text-align" scope="col">Moeda Venda</th>
                                        <th class="text-align" scope="col">Valor Venda</th>
                                        <th class="text-align" scope="col">Moeda Compra</th>
                                        <th class="text-align" scope="col">Valor Compra</th>
                                        <th class="text-align" scope="col">Tipo Cobranca</th>
                                        <th class="text-align" scope="col">Via</th>
                                        <th class="text-align" scope="col">Comex</th>
                                        <th class="text-align" scope="col">Estufagem</th>
                                    </tr>
                                    </thead>
                                    <tbody id="grd">
                                    </tbody>
                                </table>
                            </div>

                            <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxa" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modalDeleteTitle">Deletar Taxa</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            Tem certeza que deseja deletar essa taxa?
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnDeletarTaxa" class="btn btn-primary">Sim</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal fade bd-example-modal-lg" id="modalObsGeral" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle">Obersavação Geral</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                           <textarea class="form-control" rows="15" style="width: 100%" id="obsGeral"></textarea>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnObsGeralSalvar" class="btn btn-primary" onclick="SalvarObs()">Salvar</button>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="modal fade bd-example-modal-lg" id="modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title modalTitle" id="modaTitle">Cadastrar Taxa</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row flex" id="listTaxa">
                                                <div class="text-center col-sm-6 col-sm-offset-3">
                                                    <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                    <select id="ddlTaxaCliente" onchange="Buscar(this.value)" class="labelTaxa form-control">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <br />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                                                    <div class="alert alert-warning" id="div3" visible="false" runat="server">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Código Item:</label><label runat="server" style="color: red">*</label>
                                                        <asp:TextBox ID="txtCodigoTipoItem" runat="server" onkeyup="MostrarItem(this)" CssClass="form-control" TextMode="Number" Required="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-9">
                                                    <div class="form-group">
                                                        <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlTipoItem" runat="server" onchange="MostrarValor(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA"  DataValueField="ID_ITEM_DESPESA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Comex</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlTipoComex" runat="server" CssClass="form-control" DataTextField="NM_TIPO_COMEX" DataValueField="ID_TIPO_COMEX">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Via Transporte</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataValueField="ID_VIATRANSPORTE">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Estufagem</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlTipoEstufagem" runat="server" CssClass="form-control" DataTextField="NM_TIPO_ESTUFAGEM" DataValueField="ID_TIPO_ESTUFAGEM">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">INCOTERM</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlIncoterm" runat="server" CssClass="form-control" DataTextField="INCOTERM" DataValueField="ID_INCOTERM" alt="Esse campo serve para saber quem é o parceiro">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="row" style="display: flex; align-items: flex-end;">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Place of Receipt</label>
                                                        <asp:DropDownList ID="ddlPortoRecebimento" runat="server" CssClass="form-control" DataTextField="NM_PORTO" DataValueField="ID_PORTO" alt="Esse campo serve para saber quem é o parceiro">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">POL / AOL</label><label runat="server" style="color: red">*</label>
                                                        <select id="MainContent_ddlPortoCarregamento" class="form-control">
                                                            <option value="">TODOS</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">POD / AOD</label><label runat="server" style="color: red">*</label>
                                                        <select id="MainContent_ddlPortoDescarga" class="form-control">                                                   
                                                            <option value="">TODOS</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div class="row" style="display: flex; align-items: flex-end;">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlBaseCalculo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Moeda Compra</label>
                                                        <asp:TextBox ID="txtMoedaCompra" runat="server" onkeyup="MostrarMoedaCompra(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">&nbsp</label>
                                                        <asp:DropDownList ID="ddlTipoMoedaCompra" runat="server" CssClass="form-control" onchange="cd_moeda_compra(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Base Compra</label>
                                                        <asp:TextBox ID="baseCompra" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tarífa Mínima Compra</label>
                                                        <asp:TextBox ID="txtTarifaMin" runat="server" CssClass="form-control numero" alt="Esse campo serve para saber quem é o parceiro">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <div>
                                                            <asp:CheckBox ID="chkTaxaTransportador" runat="server" CssClass="form-control noborder" Text="&nbsp;Taxa do Transportador"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                        <asp:TextBox ID="txtMoedaVenda" runat="server" onkeyup="MostrarMoedaVenda(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">&nbsp</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlTipoMoedaVenda" runat="server" onchange="cd_moeda_venda(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Base Venda</label><label runat="server" style="color: red">*</label>
                                                        <asp:TextBox ID="baseVenda" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tarífa Mínima Venda</label><label runat="server" style="color: red">*</label>
                                                        <asp:TextBox ID="txtTarifaMinVenda" runat="server" CssClass="form-control numero" alt="Esse campo serve para saber quem é o parceiro">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlDeclarado" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlProfit" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Cobrança:</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlTipoCobranca" runat="server" CssClass="form-control" DataTextField="NM_TIPO_COBRANCA" DataValueField="ID_TIPO_COBRANCA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlCobranca" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo Pagamento</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlTipoPagamento" runat="server" CssClass="form-control" DataTextField="NM_TIPO_PAGAMENTO" DataValueField="ID_TIPO_PAGAMENTO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="control-label">Origem</label><label runat="server" style="color: red">*</label>
                                                        <asp:DropDownList ID="ddlOrigemServico" runat="server" CssClass="form-control" DataTextField="NM_ORIGEM_PAGAMENTO" DataValueField="ID_ORIGEM_PAGAMENTO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Taxa</label>
                                                        <asp:TextBox ID="txtObsTaxa" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnSalvar" class="btn btn-success">Cadastrar Taxa</button>
                                            <button type="button" id="btnEditar" class="btn btn-success">Editar Taxa</button>
                                            <button type="button" id="btnSalvarEdit" class="btn btn-success">Salvar Edição</button>
                                            <button type="button" id="btnCancel" class="btn btn-danger">Cancelar</button>
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
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/toastify-js"></script>
    <script>
        $(document).ready(function () {
            CarregarLista();
            ListarTaxa();
            appendList();
        });

        function appendList() {
            var portoAeroporto = document.getElementById("MainContent_ddlViaTransporte").value;
            var option = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ListarPortoAeroporto",
                data: '{via:"' + portoAeroporto + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#MainContent_ddlPortoCarregamento").empty();
                    $("#MainContent_ddlPortoDescarga").empty();
                    if (data != null) {
                        option += "<option value=''>TODOS</option>";
                        for (let i = 0; i < data.length; i++) {
                            option += "<option value=" + data[i]["ID_PORTO"] + ">" + data[i]["NM_PORTO"] + " - " + data[i]["CD_PORTO"] + "</option>";
                        }
                        $("#MainContent_ddlPortoCarregamento").append(option);
                        $("#MainContent_ddlPortoDescarga").append(option);
                    }
                }
            })
        }

        $("#MainContent_ddlViaTransporte").change(function () {
            appendList()
        });

        function MostrarValor(selecionado) {

            var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItem.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItem(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItem.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compra(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompra.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_venda(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVenda.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVenda(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVenda.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompra(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompra.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

        function LimparFiltro() {
            document.getElementById("ddlFilterModal").value = 0;
            document.getElementById("ddlFilterComex").value = 0;
            document.getElementById("ddlFilter").value = 0;
            CarregarLista();
            ListarTaxa();
        }

        function Buscar(Id) {
            $("#btnEditar").show();
            $("#btnSalvarEdit").hide();
            $("#btnSalvar").hide();
            $("#btnCancel").hide();
            $("#listTaxa").show();
            $("#btnRegra").show();
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscaClienteParceiro",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaCliente').value = Id;
                    document.getElementById("MainContent_ddlViaTransporte").value = data.ID_VIATRANSPORTE == "0" ? "" : data.ID_VIATRANSPORTE;
                    document.getElementById('ddlTaxaCliente').value = Id;
                    document.getElementById("MainContent_ddlPortoDescarga").value = data.ID_PORTO_DESCARGA == "0" ? "" : data.ID_PORTO_DESCARGA;
                    document.getElementById("MainContent_ddlPortoRecebimento").value = data.ID_PORTO_RECEBIMENTO == "0" ? "" : data.ID_PORTO_RECEBIMENTO;
                    document.getElementById("MainContent_ddlPortoCarregamento").value = data.ID_PORTO_CARREGAMENTO == "0" ? "" : data.ID_PORTO_CARREGAMENTO;
                    document.getElementById("MainContent_ddlIncoterm").value = data.ID_INCOTERM == "0" ? "" : data.ID_ICOTERM;
                    document.getElementById("MainContent_ddlTipoCobranca").value = data.ID_TIPO_COBRANCA;
                    document.getElementById("MainContent_ddlTipoEstufagem").value = data.ID_TIPO_ESTUFAGEM == "0" ? "" : data.ID_TIPO_ESTUFAGEM;
                    document.getElementById("MainContent_ddlTipoComex").value = data.ID_TIPO_COMEX == "0" ? "" : data.ID_TIPO_COMEX;
                    document.getElementById('MainContent_txtCodigoTipoItem').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItem').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_txtTarifaMin').value = data.VL_TARIFA_MINIMA_COMPRA;
                    document.getElementById('MainContent_txtTarifaMinVenda').value = data.VL_TARIFA_MINIMA;
                    document.getElementById('MainContent_baseCompra').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompra').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompra').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVenda').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVenda').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVenda').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclarado').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfit').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobranca').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxa').value = data.OB_TAXAS;
                    document.getElementById('MainContent_ddlOrigemServico').value = data.ID_ORIGEM_PAGAMENTO;
                    document.getElementById('MainContent_ddlTipoPagamento').value = data.ID_TIPO_PAGAMENTO;
                    document.getElementById('MainContent_chkTaxaTransportador').value = data.FL_TAXA_TRANSPORTADOR;

                    var forms = ['MainContent_ddlPortoDescarga',
                        'MainContent_ddlTipoCobranca',
                        'MainContent_ddlTipoComex',
                        'MainContent_ddlViaTransporte',
                        'MainContent_ddlTipoEstufagem',
                        'MainContent_txtCodigoTipoItem',
                        'MainContent_ddlTipoItem',
                        'MainContent_ddlBaseCalculo',
                        'MainContent_baseCompra',
                        'MainContent_txtTarifaMin',
                        'MainContent_txtTarifaMinVenda',
                        'MainContent_txtMoedaCompra',
                        'MainContent_ddlTipoMoedaCompra',
                        'MainContent_txtMoedaVenda',
                        'MainContent_ddlTipoMoedaVenda',
                        'MainContent_baseVenda',
                        'MainContent_ddlDeclarado',
                        'MainContent_ddlProfit',
                        'MainContent_ddlCobranca',
                        'MainContent_txtObsTaxa',
                        'MainContent_ddlTipoPagamento',
                        'MainContent_ddlOrigemServico',
                        'MainContent_chkTaxaTransportador',
                        'MainContent_ddlPortoRecebimento',
                        'MainContent_ddlPortoCarregamento',
                        'MainContent_ddlPortoDescarga',
                        'MainContent_ddlIncoterm'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };

        function Copiar(Id) {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CopiarTaxa",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    LimparFiltro();
                    CarregarLista()
                    ListarTaxa();
                    Toastify({
                        text: "Cadastrado com Sucesso",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#1a8226",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();
                },
                error: function () {
                    LimparFiltro();
                    CarregarLista()
                    ListarTaxa();
                    Toastify({
                        text: "Erro ao copiar Taxa",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#921a30",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();
                }
            })
        };

        function ObsGeral() {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            var obsGeral = document.getElementById("obsGeral");
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/getObs",
                data: '{Id:"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var dado = data.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        console.log(dado[0]["OBS_GERAL"]);
                        obsGeral.value = dado[0]["OBS_GERAL"];
                    }
                },
                error: function () {
                }
            });
        }

        function SalvarObs() {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            var obsGeral = document.getElementById("obsGeral").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/UpdateObs",
                data: '{Id:"' + id + '", obsGeral: "' + obsGeral + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    LimparFiltro();
                    CarregarLista()
                    ListarTaxa();
                    $("#modalObsGeral").modal('hide');
                    Toastify({
                        text: "Cadastrado com Sucesso",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#1a8226",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();
                },
                error: function () {
                    LimparFiltro();
                    CarregarLista()
                    ListarTaxa();
                    Toastify({
                        text: "Erro ao cadastrar",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#921a30",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();
                }
            });
        }


        /*function EnviarDados() {
            const qtdinicial = document.getElementsByName("qtdinicial");
            const qtdfinal = document.getElementsByName("qtdfinal");
            const moeda = document.getElementsByName("moeda");
            const valor = document.getElementsByName("valor");
            const idVariacao = document.getElementsByName("idVariacao");
            const idBaseCalculoVariacao = document.getElementById("MainContent_ddlBaseCalculoVariacao");
            const values = [];

            for (let i = 0; i < qtdinicial.length; i++) {
                values.push({
                    idBaseCalculoVariacao: `${idBaseCalculoVariacao.value}`,
                    idVariacao: `${idVariacao[i].value}`,
                    idTaxaCliente: idTaxa,
                    qtInicial: `${qtdinicial[i].value}`,
                    qtFinal: `${qtdfinal[i].value}`,
                    moedaC: `${moeda[i].value}`,
                    valorC: `${valor[i].value}`
                });
            }

            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CadastrarRegra",
                data: JSON.stringify({ dados: (values) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    Toastify({
                        text: "Cadastrado com Sucesso",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "#3c763d",
                            background: "#dff0d8",
                            border: "#d6e9c6",
                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            fontSize: "18px"
                        }
                    }).showToast();
                    
                },
                error: function () {
                    Toastify({
                        text: "Erro ao cadastrar",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "#a94442",
                            background: "#f2dede",
                            border: "#ebccd1",
                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            alignItems: "center",
                            fontSize: "18px"
                        }
                    }).showToast();
                    RegraAvancada()
                }
            });
        }*/

        /*function duplicarCampos() {
            const moeda = [];
            var result = "";
            let option = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CarregarMoeda",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            moeda.push({
                                value: dado[i]["ID_MOEDA"],
                                name: dado[i]["NM_MOEDA"]
                            })
                        }
                        result += "<div class='row'>";
                        result += "<div class='col-sm-3' style='display: none;'> ";
                        result += "<div class='form-group'> ";
                        result += "<label class='control-label'></label> ";
                        result += "<input type='text' class='form-control' name='idVariacao'/> ";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-sm-3'>";
                        result += "<div class='form-group'>";
                        result += "<label class='control-label'>Quantidade inicial</label>";
                        result += "<input type='text' class='form-control' name='qtdinicial'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-sm-3'>";
                        result += "<div class='form-group'>";
                        result += "<label class='control-label'>Quantidade Final</label>";
                        result += "<input type='text' class='form-control' name='qtdfinal'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-sm-3'>";
                        result += "<div class='form-group'>";
                        result += "<label class='control-label'>Valor</label>";
                        result += "<input type='text' class='form-control' name='valor'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-sm-3'>";
                        result += "<div class='form-group'>";
                        result += "<label class='control-label'>Moeda</label>";
                        result += "<select class='form-control' name='moeda'>";
                        for (let i = 0; i < moeda.length; i++) {
                            console.log(moeda[i].value);
                            result += "<option value='" + moeda[i].value + "'>" + moeda[i].name + "</option>"
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "</div>";
                        $("#rows-variacao").append(result);
                    }
                },
                error: function () {
                }
            });
        }*/

        /*function removerCampos(id) {
            var node1 = document.getElementById('modalRegraInfo');
            node1.removeChild(node1.childNodes[0]);
        }*/

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        var flagTaxa = document.getElementById("MainContent_chkTaxaTransportador");
        if (flagTaxa.checked) {
            flagTaxa = 1;
        } else {
            flagTaxa = 0;
        }
        $("#btnSalvar").click(function () {
            if (document.getElementById("MainContent_baseCompra").value == "") {
                document.getElementById("MainContent_baseCompra").value = 0;
            }
            var dado = {
                "ID_PORTO_RECEBIMENTO": document.getElementById("MainContent_ddlPortoRecebimento").value,
                "ID_PORTO_CARREGAMENTO": document.getElementById("MainContent_ddlPortoCarregamento").value,
                "ID_INCOTERM": document.getElementById("MainContent_ddlIncoterm").value,
                "ID_TIPO_COBRANCA": document.getElementById("MainContent_ddlTipoCobranca").value,
                "ID_PORTO_DESCARGA": document.getElementById("MainContent_ddlPortoDescarga").value,
                "ID_TIPO_COMEX": document.getElementById("MainContent_ddlTipoComex").value,
                "ID_TIPO_ESTUFAGEM": document.getElementById("MainContent_ddlTipoEstufagem").value,
                "ID_VIATRANSPORTE": document.getElementById("MainContent_ddlViaTransporte").value,
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItem").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculo").value,
                "VL_TARIFA_MINIMA": document.getElementById('MainContent_txtTarifaMinVenda').value.replace(',', '.'),
                "VL_TARIFA_MINIMA_COMPRA": document.getElementById('MainContent_txtTarifaMin').value.replace(',', '.'),
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_ddlTipoMoedaCompra").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompra").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_ddlTipoMoedaVenda").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVenda").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclarado").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfit").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobranca").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxa").value,
                "ID_ORIGEM_PAGAMENTO": document.getElementById('MainContent_ddlOrigemServico').value,
                "ID_TIPO_PAGAMENTO": document.getElementById('MainContent_ddlTipoPagamento').value,
                "FL_TAXA_TRANSPORTADOR": flagTaxa,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CadastrarTaxaAgenteInternacional",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data.success) {
                        LimparFiltro();
                        CarregarLista()
                        ListarTaxa();
                        Toastify({
                            text: data.message,
                            close: true,
                            gravity: "bottom",
                            style: {
                                color: "white",
                                background: "#1a8226",

                                borderRadius: "4px",
                                width: "500px",
                                height: "100px",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                fontSize: "18px"
                            }
                        }).showToast();
                        $("#modal").modal('hide');
                    }
                    else {
                        Toastify({
                            text: data.message,
                            close: true,
                            gravity: "bottom",
                            style: {
                                color: "white",
                                background: "#921a30",

                                borderRadius: "4px",
                                width: "500px",
                                height: "100px",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                fontSize: "18px"
                            }
                        }).showToast();
                    }
                },
                error: function () {
                    Toastify({
                        text: "Erro ao cadastrar",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#921a30",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();
                }
            });
        });


        function CarregarLista() {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            var via = document.getElementById("ddlFilterModal").value;
            var comex = document.getElementById("ddlFilterComex").value;
            var estufagem = document.getElementById("ddlFilter").value;
            var result = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CarregarTaxaParceiro",
                data: '{Id:"' + id + '", via: "' + via + '", estufagem: "' + estufagem + '", comex: "' + comex + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grd").empty();
                    $("#grd").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grd").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            result += "<tr>";
                            result += "<td>";
                            result += "<div class='editar btn btn-primary' style='margin-right: 4px;' data-toggle='modal' data-value='" + dado[i]['ID_TAXA_CLIENTE'] + "' data-target='#modal' onclick = 'Buscar(" + dado[i]['ID_TAXA_CLIENTE'] + ")'> <i class='fas fa-eye'></i></div>";
                            result += "<div class='copiar btn btn-warning' style='margin-right: 4px;' data-value='" + dado[i]['ID_TAXA_CLIENTE'] + "' onclick = 'Copiar(" + dado[i]['ID_TAXA_CLIENTE'] + ")'> <i class='fas fa-copy'></i></div>";
                            result += "<div class='delete btn btn-danger' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div>";
                            result += "</td>";
                            result += "<td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td>";
                            result += "<td>" + dado[i]["ITEM"] + "</td>";
                            result += "<td>" + dado[i]["RECEBIMENTO"] + "</td>";
                            result += "<td>" + dado[i]["CARREGAMENTO"] + "</td>";
                            result += "<td>" + dado[i]["DESCARGA"] + "</td>";
                            result += dado[i]["INCOTERM"] == "TODOS" ? "<td>" + dado[i]["INCOTERM"] + "</td>" : "<td>" + dado[i]["CD_INCOTERM"] + " - " + dado[i]["INCOTERM"] + "</td>";
                            result += "<td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td>";
                            result += "<td>" + dado[i]["NM_MOEDA"] + "</td>";
                            result += "<td>" + dado[i]["VL_TAXA_VENDA"] + "</td>";
                            result += "<td>" + dado[i]["NM_MOEDA_COMPRA"] + "</td>";
                            result += "<td>" + dado[i]["VL_TAXA_COMPRA"] + "</td>";
                            result += "<td>" + dado[i]["NM_TIPO_COBRANCA"] + "</td>";
                            result += "<td>" + dado[i]["NM_TIPO_COMEX"] + "</td>";
                            result += "<td>" + dado[i]["NM_VIATRANSPORTE"] + "</td>";
                            result += "<td>" + dado[i]["NM_TIPO_ESTUFAGEM"] + "</td>";
                            result += "</tr>";
                        }
                        $("#grd").append(result)
                    }
                },
                error: function (data) {
                    $("#msgErr").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        }


        function ListarTaxa() {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            var via = document.getElementById("ddlFilterModal").value;
            var comex = document.getElementById("ddlFilterComex").value;
            var estufagem = document.getElementById("ddlFilter").value;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/ListarTaxaCliente",
                data: '{Id:"' + id + '", via: "' + via + '", estufagem: "' + estufagem + '", comex: "' + comex + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    $("#ddlTaxaCliente").empty();
                    if (data != null) {
                        for (let i = 0; i < data.length; i++) {
                            $("#ddlTaxaCliente").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                        }
                    }
                },
                error: function (data) {

                },
            });
        }


        /*function RegraAvancada() {
            let result = "";
            const moeda = [];
            const idBaseCalculoVariacao = document.getElementById("MainContent_ddlBaseCalculoVariacao");
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/CarregarMoeda",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        for (let k = 0; k < dado.length; k++) {
                            moeda.push({
                                value: dado[k]["ID_MOEDA"],
                                name: dado[k]["NM_MOEDA"]
                            })
                        }
                        $.ajax({
                            type: "POST",
                            url: "DemurrageService.asmx/BuscarRegraAvancada",
                            data: '{idTaxa:"' + idTaxa + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#rows-variacao").empty();
                                if (data != null) {
                                    console.log(data[0]["ID_CALCULO_VARIACAO"]);
                                    idBaseCalculoVariacao.value = data[0]["ID_CALCULO_VARIACAO"];
                                    for (let i = 0; i < data.length; i++) {
                                        result += "<div class='row'> ";
                                        result += "<div class='col-sm-3' style='display: none;'> ";
                                        result += "<div class='form-group'> ";
                                        result += "<label class='control-label'>Quantidade inicial</label> ";
                                        result += "<input type='text' class='form-control' name='idVariacao' value='" + data[i]["ID_TAXA_CLIENTE_VARIACAO"] + "'/> ";
                                        result += "</div>";
                                        result += "</div>";
                                        result += "<div class='col-sm-3'> ";
                                        result += "<div class='form-group'> ";
                                        result += "<label class='control-label'>Quantidade inicial</label> ";
                                        result += "<input type='text' class='form-control' name='qtdinicial' value='" + data[i]["QT_ITEM_INICIAL"] + "'/> ";
                                        result += "</div>";
                                        result += "</div>";
                                        result += "<div class='col-sm-3'> ";
                                        result += "<div class='form-group'> ";
                                        result += "<label class='control-label'>Quantidade Final</label> ";
                                        result += "<input type='text' class='form-control' name='qtdfinal' value='" + data[i]["QT_ITEM_FINAL"] + "'/> ";
                                        result += "</div> ";
                                        result += "</div> ";
                                        result += "<div class='col-sm-3'> ";
                                        result += "<div class='form-group'> ";
                                        result += "<label class='control-label'>Valor</label> ";
                                        result += "<input type='text' class='form-control' name='valor' value='" + data[i]["VL_COMPRA"] + "'/> ";
                                        result += "</div> ";
                                        result += "</div> ";
                                        result += "<div class='col-sm-3'> ";
                                        result += "<div class='form-group' style='display: flex;align-items:flex-end;'> ";
                                        result += "<div>";
                                        result += "<label class='control-label'>Moeda</label>";
                                        result += "<select class='form-control' name='moeda'>";
                                        for (let j = 0; j < moeda.length; j++) {
                                            if (moeda[j].value == data[i]["ID_MOEDA"]) {
                                                result += "<option value='" + moeda[j].value + "' selected>" + moeda[j].name + "</option>"
                                            } else {
                                                result += "<option value='" + moeda[j].value + "'>" + moeda[j].name + "</option>"
                                            }
                                        }
                                        result += "</select>";
                                        *//*result += "<input type='text' class='form-control' name='moeda' value='" + dado[i]["ID_MOEDA"] + "'/> ";*//*
        result += "</div>";
        result += "<div class='deleteTaxaVariacao btn btn-primary' style='margin-left: 10px;' name='id_variacao' data-toggle='modal' data-target='#modalDeleteTaxaVariacao' data-value='" + data[i]["ID_TAXA_CLIENTE_VARIACAO"] + "'> ";
        result += "<i class='fas fa-trash'></i>";
        result += "</div> ";
        result += "</div> ";
        result += "</div>";
        result += "</div>";
    }
    $("#rows-variacao").append(result);
}
else {
    result += "<div class='row'>";
    result += "<div class='col-sm-3' style='display: none;'> ";
    result += "<div class='form-group'> ";
    result += "<label class='control-label'>Quantidade inicial</label> ";
    result += "<input type='text' class='form-control' name='idVariacao'/> ";
    result += "</div>";
    result += "</div>";
    result += "<div class='col-sm-3'>";
    result += "<div class='form-group'>";
    result += "<label class='control-label'>Quantidade inicial</label>";
    result += "<input type='text' class='form-control' name='qtdinicial'/>";
    result += "</div>";
    result += "</div>";
    result += "<div class='col-sm-3'>";
    result += "<div class='form-group'>";
    result += "<label class='control-label'>Quantidade Final</label>";
    result += "<input type='text' class='form-control' name='qtdfinal'/>";
    result += "</div>";
    result += "</div>";
    result += "<div class='col-sm-3'>";
    result += "<div class='form-group'>";
    result += "<label class='control-label'>Valor</label>";
    result += "<input type='text' class='form-control' name='valor'/>";
    result += "</div>";
    result += "</div>";
    result += "<div class='col-sm-3'>";
    result += "<div class='form-group'>";
    result += "<label class='control-label'>Moeda</label>";
    result += "<select class='form-control' name='moeda'>";
    for (let i = 0; i < moeda.length; i++) {
        result += "<option value='" + moeda[i].value + "'>" + moeda[i].name + "</option>"
    }
    result += "</select>";
    result += "</div>";
    result += "</div>";
    result += "</div>";
    $("#rows-variacao").append(result);
}
},
error: function () {
$("#msgErr").fadeIn(500).delay(3500).fadeOut(500);
}
});
}
},
error: function () {
}
});
}*/


        /*function RegraAvancada() {
            let result = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/BuscarRegraAvancada",
                data: '{idTaxa:"' + idTaxa + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#rows-variacao").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            result += "<div class='row'> ";
                            result += "<div class='col-sm-3' style='display: none;'> ";
                            result += "<div class='form-group'> ";
                            result += "<label class='control-label'>Quantidade inicial</label> ";
                            result += "<input type='text' class='form-control' name='idVariacao' value='" + dado[i]["ID_TAXA_CLIENTE_VARIACAO"] + "'/> ";
                            result += "</div>";
                            result += "</div>";
                            result += "<div class='col-sm-3'> ";
                            result += "<div class='form-group'> ";
                            result += "<label class='control-label'>Quantidade inicial</label> ";
                            result += "<input type='text' class='form-control' name='qtdinicial' value='" + dado[i]["QT_ITEM_INICIAL"] + "'/> ";
                            result += "</div>";
                            result += "</div>";
                            result += "<div class='col-sm-3'> ";
                            result += "<div class='form-group'> ";
                            result += "<label class='control-label'>Quantidade Final</label> ";
                            result += "<input type='text' class='form-control' name='qtdfinal' value='" + dado[i]["QT_ITEM_FINAL"] + "'/> ";
                            result += "</div> ";
                            result += "</div> ";
                            result += "<div class='col-sm-3'> ";
                            result += "<div class='form-group'> ";
                            result += "<label class='control-label'>Valor</label> ";
                            result += "<input type='text' class='form-control' name='valor' value='" + dado[i]["VL_COMPRA"] + "'/> ";
                            result += "</div> ";
                            result += "</div> ";
                            result += "<div class='col-sm-3'> ";
                            result += "<div class='form-group' style='display: flex;align-items:flex-end;'> ";
                            result += "<div>";
                            result += "<label class='control-label'>Moeda</label>";

                            *//*result += "<input type='text' class='form-control' name='moeda' value='" + dado[i]["ID_MOEDA"] + "'/> ";*//*
result += "</div>";
result += "<div class='deleteTaxaVariacao btn btn-primary' style='margin-left: 10px;' name='id_variacao' data-toggle='modal' data-target='#modalDeleteTaxaVariacao' data-value='" + dado[i]["ID_TAXA_CLIENTE_VARIACAO"] + "'> ";
result += "<i class='fas fa-trash'></i>";
result += "</div> ";
result += "</div> ";
result += "</div>";
result += "</div>";
}
$("#rows-variacao").append(result);
}
else {
result += "<div class='row'>";
result += "<div class='col-sm-3'>";
result += "<div class='form-group'>";
result += "<label class='control-label'>Quantidade inicial</label>";
result += "<input type='text' class='form-control' name='qtdinicial'/>";
result += "</div>";
result += "</div>";
result += "<div class='col-sm-3'>";
result += "<div class='form-group'>";
result += "<label class='control-label'>Quantidade Final</label>";
result += "<input type='text' class='form-control' name='qtdfinal'/>";
result += "</div>";
result += "</div>";
result += "<div class='col-sm-3'>";
result += "<div class='form-group'>";
result += "<label class='control-label'>Valor</label>";
result += "<input type='text' class='form-control' name='valor'/>";
result += "</div>";
result += "</div>";
result += "<div class='col-sm-3'>";
result += "<div class='form-group'>";
result += "<label class='control-label'>Moeda</label>";
result += "<input type='text' class='form-control' name='moeda'/>";
result += "</div>";
result += "</div>";
result += "</div>";
$("#rows-variacao").append(result);
}
},
error: function () {
$("#msgErr").fadeIn(500).delay(3500).fadeOut(500);
}
});
}*/

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        var flagTaxa = document.getElementById("MainContent_chkTaxaTransportador");
        if (flagTaxa.checked) {
            flagTaxa = 1;
        } else {
            flagTaxa = 0;
        }
        $("#btnSalvarEdit").click(function () {
            if (document.getElementById("MainContent_baseCompra").value == "") {
                document.getElementById("MainContent_baseCompra").value = 0;
            }
            var dadoEdit = {
                "ID_PORTO_RECEBIMENTO": document.getElementById("MainContent_ddlPortoRecebimento").value,
                "ID_PORTO_CARREGAMENTO": document.getElementById("MainContent_ddlPortoCarregamento").value,
                "ID_INCOTERM": document.getElementById("MainContent_ddlIncoterm").value,
                "ID_TIPO_COBRANCA": document.getElementById("MainContent_ddlTipoCobranca").value,
                "ID_PORTO_DESCARGA": document.getElementById("MainContent_ddlPortoDescarga").value,
                "ID_TIPO_COMEX": document.getElementById("MainContent_ddlTipoComex").value,
                "ID_TIPO_ESTUFAGEM": document.getElementById("MainContent_ddlTipoEstufagem").value,
                "ID_VIATRANSPORTE": document.getElementById("MainContent_ddlViaTransporte").value,
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItem").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculo").value,
                "VL_TARIFA_MINIMA": document.getElementById('MainContent_txtTarifaMinVenda').value.replace(',', '.'),
                "VL_TARIFA_MINIMA_COMPRA": document.getElementById('MainContent_txtTarifaMin').value.replace(',', '.'),
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_ddlTipoMoedaCompra").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompra").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_ddlTipoMoedaVenda").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVenda").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclarado").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfit").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobranca").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxa").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaCliente").value,
                "ID_ORIGEM_PAGAMENTO": document.getElementById('MainContent_ddlOrigemServico').value,
                "ID_TIPO_PAGAMENTO": document.getElementById('MainContent_ddlTipoPagamento').value,
                "FL_TAXA_TRANSPORTADOR": flagTaxa,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/EditarTaxaAgenteInternacional",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    if (data.success) {
                        LimparFiltro();
                        CarregarLista()
                        ListarTaxa();
                        Toastify({
                            text: data.message,
                            close: true,
                            gravity: "bottom",
                            style: {
                                color: "white",
                                background: "#1a8226",

                                borderRadius: "4px",
                                width: "500px",
                                height: "100px",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                fontSize: "18px"
                            }
                        }).showToast();
                        $("#modal").modal('hide');
                    }
                    else {
                        Toastify({
                            text: data.message,
                            close: true,
                            gravity: "bottom",
                            style: {
                                color: "white",
                                background: "#921a30",

                                borderRadius: "4px",
                                width: "500px",
                                height: "100px",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                fontSize: "18px"
                            }
                        }).showToast();
                    }
                },
                error: function (data) {
                    console.log(data);
                    Toastify({
                        text: "Erro ao cadastrar",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#921a30",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();
                }
            });
        });

        function Disable() {
            var forms = ['MainContent_ddlTipoCobranca',
                'MainContent_ddlTipoComex',
                'MainContent_ddlViaTransporte',
                'MainContent_ddlTipoEstufagem',
                'MainContent_txtCodigoTipoItem',
                'MainContent_ddlTipoItem',
                'MainContent_ddlBaseCalculo',
                'MainContent_baseCompra',
                'MainContent_txtTarifaMin',
                'MainContent_txtTarifaMinVenda',
                'MainContent_txtMoedaCompra',
                'MainContent_ddlTipoMoedaCompra',
                'MainContent_txtMoedaVenda',
                'MainContent_ddlTipoMoedaVenda',
                'MainContent_baseVenda',
                'MainContent_ddlDeclarado',
                'MainContent_ddlProfit',
                'MainContent_ddlCobranca',
                'MainContent_txtObsTaxa',
                'MainContent_ddlTipoPagamento',
                'MainContent_ddlOrigemServico',
                'MainContent_chkTaxaTransportador',
                'MainContent_ddlPortoRecebimento',
                'MainContent_ddlPortoCarregamento',
                'MainContent_ddlPortoDescarga',
                'MainContent_ddlIncoterm'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }
            $("#btnRegra").hide();
        }

        $("#btnEditar").click(function () {
            $("#btnEditar").hide();
            $("#btnSalvarEdit").show();
            $("#btnCancel").show();

            var forms = ['MainContent_ddlPortoDescarga',
                'MainContent_ddlTipoCobranca',
                'MainContent_ddlTipoComex',
                'MainContent_ddlViaTransporte',
                'MainContent_ddlTipoEstufagem',
                'MainContent_txtCodigoTipoItem',
                'MainContent_ddlTipoItem',
                'MainContent_ddlBaseCalculo',
                'MainContent_baseCompra',
                'MainContent_txtTarifaMin',
                'MainContent_txtTarifaMinVenda',
                'MainContent_txtMoedaCompra',
                'MainContent_ddlTipoMoedaCompra',
                'MainContent_txtMoedaVenda',
                'MainContent_ddlTipoMoedaVenda',
                'MainContent_baseVenda',
                'MainContent_ddlDeclarado',
                'MainContent_ddlProfit',
                'MainContent_ddlCobranca',
                'MainContent_txtObsTaxa',
                'MainContent_ddlTipoPagamento',
                'MainContent_ddlOrigemServico',
                'MainContent_chkTaxaTransportador',
                'MainContent_ddlPortoRecebimento',
                'MainContent_ddlPortoCarregamento',
                'MainContent_ddlIncoterm'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
            }

        })
        $("#btnCancel").click(function () {
            $("#btnEditar").show();
            $("#btnCancel").hide();
            $("#btnSalvarEdit").hide();
            var forms = ['MainContent_ddlPortoDescarga',
                'MainContent_ddlTipoCobranca',
                'MainContent_ddlTipoComex',
                'MainContent_ddlViaTransporte',
                'MainContent_ddlTipoEstufagem',
                'MainContent_txtCodigoTipoItem',
                'MainContent_ddlTipoItem',
                'MainContent_ddlBaseCalculo',
                'MainContent_baseCompra',
                'MainContent_txtTarifaMin',
                'MainContent_txtTarifaMinVenda',
                'MainContent_txtMoedaCompra',
                'MainContent_ddlTipoMoedaCompra',
                'MainContent_txtMoedaVenda',
                'MainContent_ddlTipoMoedaVenda',
                'MainContent_baseVenda',
                'MainContent_ddlDeclarado',
                'MainContent_ddlProfit',
                'MainContent_ddlCobranca',
                'MainContent_txtObsTaxa',
                'MainContent_ddlTipoPagamento',
                'MainContent_ddlOrigemServico',
                'MainContent_chkTaxaTransportador',
                'MainContent_ddlPortoRecebimento',
                'MainContent_ddlPortoCarregamento',
                'MainContent_ddlIncoterm'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                $(aux).attr("disabled", "true");
            }
        })
        $("#btnNovaTaxa").click(function () {
            $("#btnSalvar").show();
            $("#btnSalvarEdit").hide();
            $("#btnEditar").hide();
            $("#btnCancel").hide();
            $("#listTaxa").hide();
            var forms = ['MainContent_ddlPortoDescarga',
                'MainContent_ddlTipoCobranca',
                'MainContent_ddlTipoComex',
                'MainContent_ddlViaTransporte',
                'MainContent_ddlTipoEstufagem',
                'MainContent_txtCodigoTipoItem',
                'MainContent_ddlTipoItem',
                'MainContent_ddlBaseCalculo',
                'MainContent_baseCompra',
                'MainContent_txtTarifaMin',
                'MainContent_txtTarifaMinVenda',
                'MainContent_txtMoedaCompra',
                'MainContent_ddlTipoMoedaCompra',
                'MainContent_txtMoedaVenda',
                'MainContent_ddlTipoMoedaVenda',
                'MainContent_baseVenda',
                'MainContent_ddlDeclarado',
                'MainContent_ddlProfit',
                'MainContent_ddlCobranca',
                'MainContent_txtObsTaxa',
                'MainContent_ddlTipoPagamento',
                'MainContent_ddlOrigemServico',
                'MainContent_chkTaxaTransportador',
                'MainContent_ddlPortoRecebimento',
                'MainContent_ddlPortoCarregamento',
                'MainContent_ddlIncoterm'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
            }
            document.getElementById("MainContent_ddlCobranca").value = 1;
            document.getElementById("MainContent_ddlTipoPagamento").value = 1;
        })

        var idTaxa;
        var idTaxaVariacao;

        $(document).on("click", ".delete", function () {
            $("#modalDeleteTaxa").modal('show');
            idTaxa = $(this).attr('data-value');
            $("#modalDeleteTaxa").val(idTaxa);
        });

        $(document).on("click", ".editar", function () {
            idTaxa = $(this).attr('data-value');
        });

        $(document).on("click", ".copiar", function () {
            idTaxa = $(this).attr('data-value');
        });

        $(document).on("click", ".deleteTaxaVariacao", function () {
            idTaxaVariacao = $(this).attr('data-value');
        });

        $("#btnDeletarTaxa").click(function () {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/DeletarTaxa",
                data: '{Id:"' + idTaxa + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grd").empty();
                    $("#grd").append("<tr><td class='text-center' colspan='9'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    Toastify({
                        text: "Cadastrado com Sucesso",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#1a8226",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();

                    CarregarLista();
                    ListarTaxa();
                },
                error: function () {
                    Toastify({
                        text: "Erro ao deletar",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "white",
                            background: "#921a30",

                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                            fontSize: "18px"
                        }
                    }).showToast();

                    CarregarLista();
                    ListarTaxa();
                }
            });
            $("#modalDeleteTaxa").modal('hide');
        });

        /*function DeletarTaxaVariacao() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/DeletarTaxaVariacao",
                data: '{Id:"' + idTaxaVariacao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    Toastify({
                        text: "Cadastrado com Sucesso",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "#3c763d",
                            background: "#dff0d8",
                            border: "#d6e9c6",
                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            display: "flex",
                            alignItems: "center",
                            fontSize: "18px"
                        }
                    }).showToast();
                   
                    CarregarLista();
                    ListarTaxa();
                },
                error: function () {
                    Toastify({
                        text: "Erro ao cadastrar",
                        close: true,
                        gravity: "bottom",
                        style: {
                            color: "#a94442",
                            background: "#f2dede",
                            border: "#ebccd1",
                            borderRadius: "4px",
                            width: "500px",
                            height: "100px",
                            alignItems: "center",
                            fontSize: "18px"
                        }
                    }).showToast();
                   
                    CarregarLista();
                    ListarTaxa();
                }
            });
            $("#modalDeleteTaxaVariacao").modal('hide');
        }*/

    </script>

</asp:Content>
