<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaxaParceiro.aspx.cs" Inherits="ABAINFRA.Web.TaxaParceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row principal">
        <div class="col-lg-10 col-lg-offset-1 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Taxas</h3>
                </div>
                <div class="panel-body">
                    

                    <ul class="nav nav-tabs MainTabs" role="tablist">
                        <li class="active" id="tabMaritimo">
                            <a href="#maritimo" role="tab" id="linkMaritimo" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Marítimo
                            </a>
                        </li>
                        <li id="tabAereo">
                            <a href="#aereo" role="tab" id="linkaereo" data-toggle="tab">
                                <i class="fas fa-percent" style="padding-right: 8px;"></i>Aéreo
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="maritimo">
                            <ul class="nav nav-tabs SecondTabs" role="tablist">
                                <li class="active" id="tabImpo">
                                    <a href="#impo" role="tab" id="linkImpo" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Impo
                                    </a>
                                </li>
                                <li id="tabExpo">
                                    <a href="#expo" role="tab" id="linkExpo" data-toggle="tab">
                                        <i class="fas fa-percent" style="padding-right: 8px;"></i>Expo
                                    </a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="impo">
                                    <ul class="nav nav-tabs ThirdTabs" role="tablist">
                                        <li id="tabFCLimpo" class="active">
                                            <a href="#FCLimpo" role="tab" id="linkFCLimpo" data-toggle="tab">
                                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas FCL - Impo
                                            </a>
                                        </li>
                                        <li id="tabLCLimpo">
                                            <a href="#LCLimpo" role="tab" id="linkLCLimpo" data-toggle="tab">
                                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas LCL - Impo
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane fade active in" id="FCLimpo">
                                            <div class="row">
                                                <div class="alert alert-success" id="msgSuccessfclimpo">
                                                    Registro cadastrado/atualizado com sucesso!
                                                </div>
                                                <div class="alert alert-danger" id="msgErrfclimpo">
                                                    Erro ao cadastrar/atualizar.
                                                </div>
                                            </div>
                                            <div class="row flex text-center">
                                                <div class="col-sm-3 a">
                                                    <div class="form-group">
                                                        <label class="control-label">Parceiro:</label>
                                                        <asp:TextBox ID="txtParceiroFCLimpo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <button type="button" id="btnNovaTaxaFCLimpo" class="btn btn-primary" data-toggle="modal" data-target="#modalFCLimpo" onclick="DisableFCLimpo()">Nova Taxa</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxaFCLimpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalDeleteFCLimpoTitle">Deletar Taxa</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            Tem certeza que deseja deletar essa taxa?
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnDeletarTaxaFCLimpo" class="btn btn-primary">Sim</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal fade bd-example-modal-lg" id="modalFCLimpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalFCLimpoTitle">Cadastrar Taxa FCL - Importação</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row flex" id="listTaxaFCLimpo">
                                                                <div class="text-center col-sm-6 col-sm-offset-3">
                                                                    <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                                    <select id="ddlTaxaClienteFCLimpo" onchange="BuscarFCLimpo(this.value)" class="labelTaxa form-control">
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
                                                                        <asp:TextBox ID="txtCodigoTipoItemFCLimpo" runat="server" onkeyup="MostrarItemFCLimpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlTipoItemFCLimpo" runat="server" onchange="MostrarValorFCLimpo(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlBaseCalculoFCLimpo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Compra</label>
                                                                        <asp:TextBox ID="txtMoedaCompraFCLimpo" runat="server" onkeyup="MostrarMoedaCompraFCLimpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaCompraFCLimpo" runat="server" CssClass="form-control" onchange="cd_moeda_compraFCLimpo(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Compra</label>
                                                                        <asp:TextBox ID="baseCompraFCLimpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaVendaFCLimpo" runat="server" onkeyup="MostrarMoedaVendaFCLimpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaVendaFCLimpo" runat="server" onchange="cd_moeda_vendaFCLimpo(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Venda</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="baseVendaFCLimpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlDeclaradoFCLimpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlProfitFCLimpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlCobrancaFCLimpo" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Observação Taxa</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtObsTaxaFCLimpo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnSalvarFCLimpo" class="btn btn-success" data-dismiss="modal">Cadastrar Taxa</button>
                                                            <button type="button" id="btnEditarFCLimpo" class="btn btn-success">Editar Taxa</button>
                                                            <button type="button" id="btnSalvarEditFCLimpo" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                                            <button type="button" id="btnCancelFCLimpo" class="btn btn-danger">Cancelar</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive tableFixHead">
                                                <table class="table">
                                                  <thead>
                                                    <tr>
                                                      <th class="text-align" scope="col">#</th>
                                                      <th class="text-align" scope="col">Id Taxa</th>
                                                      <th class="text-align" scope="col">Item</th>
                                                      <th class="text-align" scope="col">Base Cálculo</th>
                                                      <th class="text-align" scope="col">Moeda Venda</th>
                                                      <th class="text-align" scope="col">Valor Venda</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody id="grdFCLimpo">
                                                  </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="LCLimpo">
                                            <div class="row">
                                                <div class="alert alert-success" id="msgSuccesslclimpo">
                                                    Registro cadastrado/atualizado com sucesso!
                                                </div>
                                                <div class="alert alert-danger" id="msgErrlclimpo">
                                                    Erro ao cadastrar/atualizar.
                                                </div>
                                            </div>
                                            <div class="row flex text-center">
                                                <div class="col-sm-3 a">
                                                    <div class="form-group">
                                                        <label class="control-label">Parceiro:</label>
                                                        <asp:TextBox ID="txtParceiroLCLimpo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-primary" id="btnNovaTaxaLCLimpo" data-toggle="modal" data-target="#modalLCLimpo" onclick="DisableLCLimpo()">Nova Taxa</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxaLCLimpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalDeleteLCLimpoTitle">Deletar Taxa</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            Tem certeza que deseja deletar essa taxa?
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnDeletarTaxaLCLimpo" class="btn btn-primary">Sim</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal fade bd-example-modal-lg" id="modalLCLimpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalLCLimpoTitle">Cadastrar Taxa LCL - Importação</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row flex" id="listTaxaLCLimpo">
                                                                <div class="text-center col-sm-6 col-sm-offset-3">
                                                                    <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                                    <select id="ddlTaxaClienteLCLimpo" onchange="BuscarLCLimpo(this.value)" class="labelTaxa form-control">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <br />
                                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                                                                    <div class="alert alert-warning" id="div1" visible="false" runat="server">
                                                                        <asp:Label ID="Label4" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código Item:</label>
                                                                        <asp:TextBox ID="txtCodigoTipoItemLCLimpo" runat="server" onkeyup="MostrarItemLCLimpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlTipoItemLCLimpo" runat="server" onchange="MostrarValorLCLimpo(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlBaseCalculoLCLimpo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Compra</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaCompraLCLimpo" runat="server" onkeyup="MostrarMoedaCompraLCLimpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaCompraLCLimpo" runat="server" CssClass="form-control" onchange="cd_moeda_compraLCLimpo(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Compra</label>
                                                                        <asp:TextBox ID="baseCompraLCLimpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaVendaLCLimpo" runat="server" onkeyup="MostrarMoedaVendaLCLimpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaVendaLCLimpo" runat="server" onchange="cd_moeda_vendaLCLimpo(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Venda</label>
                                                                        <asp:TextBox ID="baseVendaLCLimpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlDeclaradoLCLimpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlProfitLCLimpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlCobrancaLCLimpo" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Observação Taxa</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtObsTaxaLCLimpo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnSalvarLCLimpo" class="btn btn-success" data-dismiss="modal">Cadastrar Taxa</button>
                                                            <button type="button" id="btnEditarLCLimpo" class="btn btn-success">Editar Taxa</button>
                                                            <button type="button" id="btnSalvarEditLCLimpo" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                                            <button type="button" id="btnCancelLCLimpo" class="btn btn-danger">Cancelar</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive tableFixHead">
                                                <table class="table">
                                                  <thead>
                                                    <tr>
                                                      <th class="text-align" scope="col">#</th>
                                                      <th class="text-align" scope="col">Id Taxa</th>
                                                      <th class="text-align" scope="col">Item</th>
                                                      <th class="text-align" scope="col">Base Cálculo</th>
                                                      <th class="text-align" scope="col">Moeda Venda</th>
                                                      <th class="text-align" scope="col">Valor Venda</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody id="grdLCLimpo">
                                                  </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="expo">
                                    <ul class="nav nav-tabs ThirdTabs" role="tablist">
                                        <li id="tabFCLexpo" class="active">
                                            <a href="#FCLexpo" role="tab" id="linkFCLexpo" data-toggle="tab">
                                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas FCL - Expo
                                            </a>
                                        </li>
                                        <li id="tabLCLexpo">
                                            <a href="#LCLexpo" role="tab" id="linkLCLexpo" data-toggle="tab">
                                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas LCL - Expo
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane fade active in" id="FCLexpo">
                                            <div class="row">
                                                <div class="alert alert-success" id="msgSuccessfclexpo">
                                                    Registro cadastrado/atualizado com sucesso!
                                                </div>
                                                <div class="alert alert-danger" id="msgErrfclexpo">
                                                    Erro ao cadastrar/atualizar.
                                                </div>
                                            </div>
                                            <div class="row flex text-center">
                                                <div class="col-sm-3 a">
                                                    <div class="form-group">
                                                        <label class="control-label">Parceiro:</label>
                                                        <asp:TextBox ID="txtParceiroFCLexpo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <button type="button" id="btnNovaTaxaFCLexpo" class="btn btn-primary" data-toggle="modal" data-target="#modalFCLexpo" onclick="DisableFCLexpo()">Nova Taxa</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxaFCLexpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalDeleteFCLexpoTitle">Deletar Taxa</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            Tem certeza que deseja deletar essa taxa?
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnDeletarTaxaFCLexpo" class="btn btn-primary">Sim</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal fade bd-example-modal-lg" id="modalFCLexpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalFCLexpoTitle">Cadastrar Taxa FCL - Importação</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row flex" id="listTaxaFCLexpo">
                                                                <div class="text-center col-sm-6 col-sm-offset-3">
                                                                    <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                                    <select id="ddlTaxaClienteFCLexpo" onchange="BuscarFCLexpo(this.value)" class="labelTaxa form-control">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <br />
                                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                                                                    <div class="alert alert-warning" id="div6" visible="false" runat="server">
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código Item:</label>
                                                                        <asp:TextBox ID="txtCodigoTipoItemFCLexpo" runat="server" onkeyup="MostrarItemFCLexpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlTipoItemFCLexpo" runat="server" onchange="MostrarValorFCLexpo(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlBaseCalculoFCLexpo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Compra</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaCompraFCLexpo" runat="server" onkeyup="MostrarMoedaCompraFCLexpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaCompraFCLexpo" runat="server" CssClass="form-control" onchange="cd_moeda_compraFCLexpo(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Compra</label>
                                                                        <asp:TextBox ID="baseCompraFCLexpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaVendaFCLexpo" runat="server" onkeyup="MostrarMoedaVendaFCLexpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaVendaFCLexpo" runat="server" onchange="cd_moeda_vendaFCLexpo(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Venda</label>
                                                                        <asp:TextBox ID="baseVendaFCLexpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlDeclaradoFCLexpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlProfitFCLexpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlCobrancaFCLexpo" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Observação Taxa</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtObsTaxaFCLexpo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnSalvarFCLexpo" class="btn btn-success" data-dismiss="modal">Cadastrar Taxa</button>
                                                            <button type="button" id="btnEditarFCLexpo" class="btn btn-success">Editar Taxa</button>
                                                            <button type="button" id="btnSalvarEditFCLexpo" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                                            <button type="button" id="btnCancelFCLexpo" class="btn btn-danger">Cancelar</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive tableFixHead">
                                                <table class="table">
                                                  <thead>
                                                    <tr>
                                                      <th class="text-align" scope="col">#</th>
                                                      <th class="text-align" scope="col">Id Taxa</th>
                                                      <th class="text-align" scope="col">Item</th>
                                                      <th class="text-align" scope="col">Base Cálculo</th>
                                                      <th class="text-align" scope="col">Moeda Venda</th>
                                                      <th class="text-align" scope="col">Valor Venda</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody id="grdFCLexpo">
                                                  </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="LCLexpo">
                                            <div class="row">
                                                <div class="alert alert-success" id="msgSuccesslclexpo">
                                                    Registro cadastrado/atualizado com sucesso!
                                                </div>
                                                <div class="alert alert-danger" id="msgErrlclexpo">
                                                    Erro ao cadastrar/atualizar.
                                                </div>
                                            </div>
                                            <div class="row flex text-center">
                                                <div class="col-sm-3 a">
                                                    <div class="form-group">
                                                        <label class="control-label">Parceiro:</label>
                                                        <asp:TextBox ID="txtParceiroLCLexpo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <button type="button" id="btnNovaTaxaLCLexpo" class="btn btn-primary" data-toggle="modal" data-target="#modalLCLexpo" onclick="DisableLCLexpo()">Nova Taxa</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxaLCLexpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalDeleteLCLexpoTitle">Deletar Taxa</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            Tem certeza que deseja deletar essa taxa?
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnDeletarTaxaLCLexpo" class="btn btn-primary">Sim</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal fade bd-example-modal-lg" id="modalLCLexpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalLCLexpoTitle">Cadastrar Taxa LCL - Importação</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row flex" id="listTaxaLCLexpo">
                                                                <div class="text-center col-sm-6 col-sm-offset-3">
                                                                    <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                                    <select id="ddlTaxaClienteLCLexpo" onchange="BuscarLCLexpo(this.value)" class="labelTaxa form-control">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <br />
                                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                                                                    <div class="alert alert-warning" id="div10" visible="false" runat="server">
                                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código Item:</label>
                                                                        <asp:TextBox ID="txtCodigoTipoItemLCLexpo" runat="server" onkeyup="MostrarItemLCLexpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlTipoItemLCLexpo" runat="server" onchange="MostrarValorLCLexpo(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlBaseCalculoLCLexpo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Compra</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaCompraLCLexpo" runat="server" onkeyup="MostrarMoedaCompraLCLexpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaCompraLCLexpo" runat="server" CssClass="form-control" onchange="cd_moeda_compraLCLexpo(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Compra</label>
                                                                        <asp:TextBox ID="baseCompraLCLexpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtMoedaVendaLCLexpo" runat="server" onkeyup="MostrarMoedaVendaLCLexpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">&nbsp</label>
                                                                        <asp:DropDownList ID="ddlTipoMoedaVendaLCLexpo" runat="server" onchange="cd_moeda_vendaLCLexpo(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base Venda</label>
                                                                        <asp:TextBox ID="baseVendaLCLexpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlDeclaradoLCLexpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlProfitLCLexpo" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlCobrancaLCLexpo" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Observação Taxa</label><label runat="server" style="color: red">*</label>
                                                                        <asp:TextBox ID="txtObsTaxaLCLexpo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btnSalvarLCLexpo" class="btn btn-success" data-dismiss="modal">Cadastrar Taxa</button>
                                                            <button type="button" id="btnEditarLCLexpo" class="btn btn-success">Editar Taxa</button>
                                                            <button type="button" id="btnSalvarEditLCLexpo" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                                            <button type="button" id="btnCancelLCLexpo" class="btn btn-danger">Cancelar</button>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive tableFixHead">
                                                <table class="table">
                                                  <thead>
                                                    <tr>
                                                      <th class="text-align" scope="col">#</th>
                                                      <th class="text-align" scope="col">Id Taxa</th>
                                                      <th class="text-align" scope="col">Item</th>
                                                      <th class="text-align" scope="col">Base Cálculo</th>
                                                      <th class="text-align" scope="col">Moeda Venda</th>
                                                      <th class="text-align" scope="col">Valor Venda</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody id="grdLCLexpo">
                                                  </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="aereo">
                            <ul class="nav nav-tabs SecondTabs" role="tablist">
                                <li class="active" id="tabImpoAereo">
                                    <a href="#Impoaereo" role="tab" id="linkImpoAereo" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas Impo - Aereo
                                    </a>
                                </li>
                                <li id="tabExpoAereo">
                                    <a href="#ExpoAereo" role="tab" id="linkExpoAereo" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas Expo - Aereo
                                    </a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="Impoaereo">
                                    <div class="row">
                                        <div class="alert alert-success" id="msgSuccessimpoaereo">
                                            Registro cadastrado/atualizado com sucesso!
                                        </div>
                                        <div class="alert alert-danger" id="msgErrimpoaereo">
                                            Erro ao cadastrar/atualizar.
                                        </div>
                                    </div>
                                    <div class="row flex text-center">
                                        <div class="col-sm-3 a">
                                            <div class="form-group">
                                                <label class="control-label">Parceiro:</label>
                                                <asp:TextBox ID="txtParceiroAereoImpo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <button type="button" id="btnNovaTaxaAereoImpo" class="btn btn-primary" data-toggle="modal" data-target="#modalAereoImpo" onclick="DisableAereoImpo()">Nova Taxa</button>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxaImpoAereo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="modalDeleteImpoAereoTitle">Deletar Taxa</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    Tem certeza que deseja deletar essa taxa?
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" id="btnDeletarTaxaImpoAereo" class="btn btn-primary">Sim</button>
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal fade bd-example-modal-lg" id="modalAereoImpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="modalAereoImpoTitle">Cadastrar Taxa FCL - Importação</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row flex" id="listTaxaAereoImpo">
                                                        <div class="text-center col-sm-6 col-sm-offset-3">
                                                            <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                            <select id="ddlTaxaClienteAereoImpo" onchange="BuscarAereoImpo(this.value)" class="labelTaxa form-control">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <br />
                                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                                                            <div class="alert alert-warning" id="div11" visible="false" runat="server">
                                                                <asp:Label ID="Label5" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label class="control-label">Código Item:</label>
                                                                <asp:TextBox ID="txtCodigoTipoItemAereoImpo" runat="server" onkeyup="MostrarItemAereoImpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-9">
                                                            <div class="form-group">
                                                                <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                                <asp:DropDownList ID="ddlTipoItemAereoImpo" runat="server" onchange="MostrarValorAereoImpo(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                                <asp:DropDownList ID="ddlBaseCalculoAereoImpo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label class="control-label">Moeda Compra</label><label runat="server" style="color: red">*</label>
                                                                <asp:TextBox ID="txtMoedaCompraAereoImpo" runat="server" onkeyup="MostrarMoedaCompraAereoImpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label class="control-label">&nbsp</label>
                                                                <asp:DropDownList ID="ddlTipoMoedaCompraAereoImpo" runat="server" CssClass="form-control" onchange="cd_moeda_compraAereoImpo(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Base Compra</label>
                                                                <asp:TextBox ID="baseCompraAereoImpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                                <asp:TextBox ID="txtMoedaVendaAereoImpo" runat="server" onkeyup="MostrarMoedaVendaAereoImpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label class="control-label">&nbsp</label>
                                                                <asp:DropDownList ID="ddlTipoMoedaVendaAereoImpo" runat="server" onchange="cd_moeda_vendaAereoImpo(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Base Venda</label>
                                                                <asp:TextBox ID="baseVendaAereoImpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                                <asp:DropDownList ID="ddlDeclaradoAereoImpo" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                                <asp:DropDownList ID="ddlProfitAereoImpo" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                                <asp:DropDownList ID="ddlCobrancaAereoImpo" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <label class="control-label">Observação Taxa</label><label runat="server" style="color: red">*</label>
                                                                <asp:TextBox ID="txtObsTaxaAereoImpo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" id="btnSalvarAereoImpo" class="btn btn-success" data-dismiss="modal">Cadastrar Taxa</button>
                                                        <button type="button" id="btnEditarAereoImpo" class="btn btn-success">Editar Taxa</button>
                                                        <button type="button" id="btnSalvarEditAereoImpo" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                                        <button type="button" id="btnCancelAereoImpo" class="btn btn-danger">Cancelar</button>
                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                        <br />
                                        <div class="table-responsive tableFixHead">
                                           <table class="table">
                                                  <thead>
                                                    <tr>
                                                      <th class="text-align" scope="col">#</th>
                                                      <th class="text-align" scope="col">Id Taxa</th>
                                                      <th class="text-align" scope="col">Item</th>
                                                      <th class="text-align" scope="col">Base Cálculo</th>
                                                      <th class="text-align" scope="col">Moeda Venda</th>
                                                      <th class="text-align" scope="col">Valor Venda</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody id="grdAereoImpo">
                                                  </tbody>
                                                </table>
                                        </div>

                                    </div>
                                <div class="tab-pane fade" id="ExpoAereo">
                                        <div class="row">
                                            <div class="alert alert-success" id="msgSuccessexpoaereo">
                                                Registro cadastrado/atualizado com sucesso!
                                            </div>
                                            <div class="alert alert-danger" id="msgErrexpoaereo">
                                                Erro ao cadastrar/atualizar.
                                            </div>
                                        </div>
                                        <div class="row flex text-center">
                                            <div class="col-sm-3 a">
                                                <div class="form-group">
                                                    <label class="control-label">Parceiro:</label>
                                                    <asp:TextBox ID="txtParceiroAereoExpo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <button type="button" id="btnNovaTaxaAereoExpo" class="btn btn-primary" data-toggle="modal" data-target="#modalAereoExpo" onclick="DisableAereoExpo()">Nova Taxa</button>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    <div class="modal fade bd-example-modal-lg" id="modalDeleteTaxaExpoAereo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="modalDeleteExpoAereoTitle">Deletar Taxa</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    Tem certeza que deseja deletar essa taxa?
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" id="btnDeletarTaxaExpoAereo" class="btn btn-primary">Sim</button>
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Não</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="modal fade bd-example-modal-lg" id="modalAereoExpo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title" id="modalAereoExpoTitle">Cadastrar Taxa FCL - Importação</h5>
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="row flex" id="listTaxaAereoExpo">
                                                        <div class="text-center col-sm-6 col-sm-offset-3">
                                                            <label class="control-label text-center" style="font-size: 14px;">Cód. Taxa</label><br>
                                                            <select id="ddlTaxaClienteAereoExpo" onchange="BuscarAereoExpo(this.value)" class="labelTaxa form-control">
                                                            </select>
                                                        </div>
                                                    </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <br />
                                                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                                                                <div class="alert alert-warning" id="div12" visible="false" runat="server">
                                                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="control-label">Código Item:</label>
                                                                    <asp:TextBox ID="txtCodigoTipoItemAereoExpo" runat="server" onkeyup="MostrarItemAereoExpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <label class="control-label">Item</label><label runat="server" style="color: red">*</label>
                                                                    <asp:DropDownList ID="ddlTipoItemAereoExpo" runat="server" onchange="MostrarValorAereoExpo(this)" CssClass="form-control" DataTextField="NM_ITEM_DESPESA" DataValueField="ID_ITEM_DESPESA">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Base Cálculo</label><label runat="server" style="color: red">*</label>
                                                                    <asp:DropDownList ID="ddlBaseCalculoAereoExpo" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataValueField="ID_BASE_CALCULO_TAXA" alt="Esse campo serve para saber quem é o parceiro">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="control-label">Moeda Compra</label><label runat="server" style="color: red">*</label>
                                                                    <asp:TextBox ID="txtMoedaCompraAereoExpo" runat="server" onkeyup="MostrarMoedaCompraAereoExpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="control-label">&nbsp</label>
                                                                    <asp:DropDownList ID="ddlTipoMoedaCompraAereoExpo" runat="server" CssClass="form-control" onchange="cd_moeda_compraAereoExpo(this)" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Base Compra</label>
                                                                    <asp:TextBox ID="baseCompraAereoExpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="control-label">Moeda Venda</label><label runat="server" style="color: red">*</label>
                                                                    <asp:TextBox ID="txtMoedaVendaAereoExpo" runat="server" onkeyup="MostrarMoedaVendaAereoExpo(this)" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="control-label">&nbsp</label>
                                                                    <asp:DropDownList ID="ddlTipoMoedaVendaAereoExpo" runat="server" onchange="cd_moeda_vendaAereoExpo(this)" CssClass="form-control" DataTextField="NM_MOEDA" DataValueField="CD_MOEDA">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Base Venda</label>
                                                                    <asp:TextBox ID="baseVendaAereoExpo" runat="server" CssClass="form-control numero"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Declarado:</label><label runat="server" style="color: red">*</label>
                                                                    <asp:DropDownList ID="ddlDeclaradoAereoExpo" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Profit:</label><label runat="server" style="color: red">*</label>
                                                                    <asp:DropDownList ID="ddlProfitAereoExpo" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Cobrar do:</label><label runat="server" style="color: red">*</label>
                                                                    <asp:DropDownList ID="ddlCobrancaAereoExpo" runat="server" CssClass="form-control" DataTextField="NM_DESTINATARIO_COBRANCA" DataValueField="ID_DESTINATARIO_COBRANCA">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Observação Taxa</label><label runat="server" style="color: red">*</label>
                                                                    <asp:TextBox ID="txtObsTaxaAereoExpo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" id="btnSalvarAereoExpo" class="btn btn-success" data-dismiss="modal">Cadastrar Taxa</button>
                                                        <button type="button" id="btnEditarAereoExpo" class="btn btn-success">Editar Taxa</button>
                                                        <button type="button" id="btnSalvarEditAereoExpo" class="btn btn-success" data-dismiss="modal">Salvar Edição</button>
                                                        <button type="button" id="btnCancelAereoExpo" class="btn btn-danger">Cancelar</button>
                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="table-responsive tableFixHead">
                                            <table class="table">
                                                  <thead>
                                                    <tr>
                                                      <th class="text-align" scope="col">#</th>
                                                      <th class="text-align" scope="col">Id Taxa</th>
                                                      <th class="text-align" scope="col">Item</th>
                                                      <th class="text-align" scope="col">Base Cálculo</th>
                                                      <th class="text-align" scope="col">Moeda Venda</th>
                                                      <th class="text-align" scope="col">Valor Venda</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody id="grdAereoExpo">
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
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

    <script src="Content/js/viacep.js"></script>
    <script src="Content/js/jquery.dataTables.min.js"></script>
    <script>
        function MostrarValorFCLimpo(selecionado) {

        var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItemFCLimpo.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItemFCLimpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItemFCLimpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compraFCLimpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompraFCLimpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_vendaFCLimpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVendaFCLimpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVendaFCLimpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVendaFCLimpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompraFCLimpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompraFCLimpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

        function MostrarValorLCLimpo(selecionado) {

            var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItemLCLimpo.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItemLCLimpo(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItemLCLimpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compraLCLimpo(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompraLCLimpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_vendaLCLimpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVendaLCLimpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVendaLCLimpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVendaLCLimpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompraLCLimpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompraLCLimpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

        function MostrarValorFCLexpo(selecionado) {

            var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItemFCLexpo.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItemFCLexpo(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItemFCLexpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compraFCLexpo(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompraFCLexpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_vendaFCLexpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVendaFCLexpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVendaFCLexpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVendaFCLexpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompraFCLexpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompraFCLexpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

        function MostrarValorLCLexpo(selecionado) {

            var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItemLCLexpo.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItemLCLexpo(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItemLCLexpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compraLCLexpo(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompraLCLexpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_vendaLCLexpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVendaLCLexpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVendaLCLexpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVendaLCLexpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompraLCLexpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompraLCLexpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

        function MostrarValorAereoImpo(selecionado) {

            var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItemAereoImpo.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItemAereoImpo(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItemAereoImpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compraAereoImpo(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompraAereoImpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_vendaAereoImpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVendaAereoImpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVendaAereoImpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVendaAereoImpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompraAereoImpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompraAereoImpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

        function MostrarValorAereoExpo(selecionado) {

            var dropDown = document.getElementById(selecionado.id);
            document.getElementById("<%= txtCodigoTipoItemAereoExpo.ClientID %>").value = dropDown.value != '0' ? dropDown.value : '';
        };
        function MostrarItemAereoExpo(digitado) {

            var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoItemAereoExpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function cd_moeda_compraAereoExpo(moeda) {
            var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaCompraAereoExpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function cd_moeda_vendaAereoExpo(moeda) {
        var moeda = document.getElementById(moeda.id);
            document.getElementById("<%= txtMoedaVendaAereoExpo.ClientID %>").value = moeda.value != '0' ? moeda.value : '';
        }
        function MostrarMoedaVendaAereoExpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaVendaAereoExpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };
        function MostrarMoedaCompraAereoExpo(digitado) {
               
        var txtField = document.getElementById(digitado.id);
            document.getElementById("<%= ddlTipoMoedaCompraAereoExpo.ClientID %>").value = txtField.value != '0' ? txtField.value : '';
        };

    </script>
    <script type="text/javascript">
        function BuscarFCLimpo(Id) {
            $("#btnEditarFCLimpo").show();
            $("#btnSalvarEditFCLimpo").hide();
            $("#btnSalvarFCLimpo").hide();
            $("#btnCancelFCLimpo").hide();
            $("#listTaxaFCLimpo").show();

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaCliente",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaClienteFCLimpo').value = Id;
                    document.getElementById('MainContent_txtCodigoTipoItemFCLimpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItemFCLimpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculoFCLimpo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_baseCompraFCLimpo').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompraFCLimpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompraFCLimpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVendaFCLimpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVendaFCLimpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVendaFCLimpo').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclaradoFCLimpo').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfitFCLimpo').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobrancaFCLimpo').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxaFCLimpo').value = data.OB_TAXAS;
                   
                    var forms = ['MainContent_txtCodigoTipoItemFCLimpo',
                        'MainContent_ddlTipoItemFCLimpo',
                        'MainContent_ddlBaseCalculoFCLimpo',
                        'MainContent_baseCompraFCLimpo',
                        'MainContent_txtMoedaCompraFCLimpo',
                        'MainContent_ddlTipoMoedaCompraFCLimpo',
                        'MainContent_txtMoedaVendaFCLimpo',
                        'MainContent_ddlTipoMoedaVendaFCLimpo',
                        'MainContent_baseVendaFCLimpo',
                        'MainContent_ddlDeclaradoFCLimpo',
                        'MainContent_ddlProfitFCLimpo',
                        'MainContent_ddlCobrancaFCLimpo',
                        'MainContent_txtObsTaxaFCLimpo'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
        function BuscarLCLimpo(Id) {
            $("#btnEditarLCLimpo").show();
            $("#btnSalvarEditLCLimpo").hide();
            $("#btnSalvarLCLimpo").hide();
            $("#btnCancelLCLimpo").hide();
            $("#listTaxaLCLimpo").show();

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaCliente",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaClienteLCLimpo').value = Id;
                    document.getElementById('MainContent_txtCodigoTipoItemLCLimpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItemLCLimpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculoLCLimpo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_baseCompraLCLimpo').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompraLCLimpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompraLCLimpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVendaLCLimpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVendaLCLimpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVendaLCLimpo').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclaradoLCLimpo').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfitLCLimpo').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobrancaLCLimpo').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxaLCLimpo').value = data.OB_TAXAS;

                    var forms = ['MainContent_txtCodigoTipoItemLCLimpo',
                        'MainContent_ddlTipoItemLCLimpo',
                        'MainContent_ddlBaseCalculoLCLimpo',
                        'MainContent_baseCompraLCLimpo',
                        'MainContent_txtMoedaCompraLCLimpo',
                        'MainContent_ddlTipoMoedaCompraLCLimpo',
                        'MainContent_txtMoedaVendaLCLimpo',
                        'MainContent_ddlTipoMoedaVendaLCLimpo',
                        'MainContent_baseVendaLCLimpo',
                        'MainContent_ddlDeclaradoLCLimpo',
                        'MainContent_ddlProfitLCLimpo',
                        'MainContent_ddlCobrancaLCLimpo',
                        'MainContent_txtObsTaxaLCLimpo'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
        function BuscarFCLexpo(Id) {
            $("#btnEditarFCLexpo").show();
            $("#btnSalvarEditFCLexpo").hide();
            $("#btnSalvarFCLexpo").hide();
            $("#btnCancelFCLexpo").hide();
            $("#listTaxaFCLexpo").show();

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaCliente",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaClienteFCLexpo').value = Id;
                    document.getElementById('MainContent_txtCodigoTipoItemFCLexpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItemFCLexpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculoFCLexpo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_baseCompraFCLexpo').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompraFCLexpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompraFCLexpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVendaFCLexpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVendaFCLexpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVendaFCLexpo').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclaradoFCLexpo').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfitFCLexpo').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobrancaFCLexpo').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxaFCLexpo').value = data.OB_TAXAS;

                    var forms = ['MainContent_txtCodigoTipoItemFCLexpo',
                        'MainContent_ddlTipoItemFCLexpo',
                        'MainContent_ddlBaseCalculoFCLexpo',
                        'MainContent_baseCompraFCLexpo',
                        'MainContent_txtMoedaCompraFCLexpo',
                        'MainContent_ddlTipoMoedaCompraFCLexpo',
                        'MainContent_txtMoedaVendaFCLexpo',
                        'MainContent_ddlTipoMoedaVendaFCLexpo',
                        'MainContent_baseVendaFCLexpo',
                        'MainContent_ddlDeclaradoFCLexpo',
                        'MainContent_ddlProfitFCLexpo',
                        'MainContent_ddlCobrancaFCLexpo',
                        'MainContent_txtObsTaxaFCLexpo'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
        function BuscarLCLexpo(Id) {
            $("#btnEditarLCLexpo").show();
            $("#btnSalvarEditLCLexpo").hide();
            $("#btnSalvarLCLexpo").hide();
            $("#btnCancelLCLexpo").hide();
            $("#listTaxaLCLexpo").show();

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaCliente",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaClienteLCLexpo').value = Id;
                    document.getElementById('MainContent_txtCodigoTipoItemLCLexpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItemLCLexpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculoLCLexpo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_baseCompraLCLexpo').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompraLCLexpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompraLCLexpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVendaLCLexpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVendaLCLexpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVendaLCLexpo').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclaradoLCLexpo').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfitLCLexpo').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobrancaLCLexpo').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxaLCLexpo').value = data.OB_TAXAS;

                    var forms = ['MainContent_txtCodigoTipoItemLCLexpo',
                        'MainContent_ddlTipoItemLCLexpo',
                        'MainContent_ddlBaseCalculoLCLexpo',
                        'MainContent_baseCompraLCLexpo',
                        'MainContent_txtMoedaCompraLCLexpo',
                        'MainContent_ddlTipoMoedaCompraLCLexpo',
                        'MainContent_txtMoedaVendaLCLexpo',
                        'MainContent_ddlTipoMoedaVendaLCLexpo',
                        'MainContent_baseVendaLCLexpo',
                        'MainContent_ddlDeclaradoLCLexpo',
                        'MainContent_ddlProfitLCLexpo',
                        'MainContent_ddlCobrancaLCLexpo',
                        'MainContent_txtObsTaxaLCLexpo'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
        function BuscarAereoImpo(Id) {
            $("#btnEditarAereoImpo").show();
            $("#btnSalvarEditAereoImpo").hide();
            $("#btnSalvarAereoImpo").hide();
            $("#btnCancelAereoImpo").hide();
            $("#listTaxaAereoImpo").show();

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaCliente",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaClienteAereoImpo').value = Id;
                    document.getElementById('MainContent_txtCodigoTipoItemAereoImpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItemAereoImpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculoAereoImpo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_baseCompraAereoImpo').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompraAereoImpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompraAereoImpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVendaAereoImpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVendaAereoImpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVendaAereoImpo').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclaradoAereoImpo').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfitAereoImpo').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobrancaAereoImpo').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxaAereoImpo').value = data.OB_TAXAS;

                    var forms = ['MainContent_txtCodigoTipoItemAereoImpo',
                        'MainContent_ddlTipoItemAereoImpo',
                        'MainContent_ddlBaseCalculoAereoImpo',
                        'MainContent_baseCompraAereoImpo',
                        'MainContent_txtMoedaCompraAereoImpo',
                        'MainContent_ddlTipoMoedaCompraAereoImpo',
                        'MainContent_txtMoedaVendaAereoImpo',
                        'MainContent_ddlTipoMoedaVendaAereoImpo',
                        'MainContent_baseVendaAereoImpo',
                        'MainContent_ddlDeclaradoAereoImpo',
                        'MainContent_ddlProfitAereoImpo',
                        'MainContent_ddlCobrancaAereoImpo',
                        'MainContent_txtObsTaxaAereoImpo'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
        function BuscarAereoExpo(Id) {
            $("#btnEditarAereoExpo").show();
            $("#btnSalvarEditAereoExpo").hide();
            $("#btnSalvarAereoExpo").hide();
            $("#btnCancelAereoExpo").hide();
            $("#listTaxaAereoExpo").show();

            $.ajax({
                type: "POST",
                url: "WebService1.asmx/BuscaCliente",
                data: '{Id:"' + Id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    document.getElementById('ddlTaxaClienteAereoExpo').value = Id;
                    document.getElementById('MainContent_txtCodigoTipoItemAereoExpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlTipoItemAereoExpo').value = data.ID_ITEM_DESPESA;
                    document.getElementById('MainContent_ddlBaseCalculoAereoExpo').value = data.ID_BASE_CALCULO_TAXA;
                    document.getElementById('MainContent_baseCompraAereoExpo').value = data.VL_TAXA_COMPRA;
                    document.getElementById('MainContent_txtMoedaCompraAereoExpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_ddlTipoMoedaCompraAereoExpo').value = data.ID_MOEDA_COMPRA;
                    document.getElementById('MainContent_txtMoedaVendaAereoExpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_ddlTipoMoedaVendaAereoExpo').value = data.ID_MOEDA_VENDA;
                    document.getElementById('MainContent_baseVendaAereoExpo').value = data.VL_TAXA_VENDA;
                    document.getElementById('MainContent_ddlDeclaradoAereoExpo').value = data.FL_DECLARADO;
                    document.getElementById('MainContent_ddlProfitAereoExpo').value = data.FL_DIVISAO_PROFIT;
                    document.getElementById('MainContent_ddlCobrancaAereoExpo').value = data.ID_DESTINATARIO_COBRANCA;
                    document.getElementById('MainContent_txtObsTaxaAereoExpo').value = data.OB_TAXAS;

                    var forms = ['MainContent_txtCodigoTipoItemAereoExpo',
                        'MainContent_ddlTipoItemAereoExpo',
                        'MainContent_ddlBaseCalculoAereoExpo',
                        'MainContent_baseCompraAereoExpo',
                        'MainContent_txtMoedaCompraAereoExpo',
                        'MainContent_ddlTipoMoedaCompraAereoExpo',
                        'MainContent_txtMoedaVendaAereoExpo',
                        'MainContent_ddlTipoMoedaVendaAereoExpo',
                        'MainContent_baseVendaAereoExpo',
                        'MainContent_ddlDeclaradoAereoExpo',
                        'MainContent_ddlProfitAereoExpo',
                        'MainContent_ddlCobrancaAereoExpo',
                        'MainContent_txtObsTaxaAereoExpo'];
                    for (let i = 0; i < forms.length; i++) {
                        var aux = document.getElementById(forms[i]);
                        $(aux).attr("disabled", "true");
                    }
                }
            })
        };
    </script>
    <script>
        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarFCLimpo").click(function () {
            if (document.getElementById("MainContent_baseCompraFCLimpo").value == "") {
                document.getElementById("MainContent_baseCompraFCLimpo").value = 0;
        }
        var dado = {
        "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemFCLimpo").value,
        "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoFCLimpo").value,
        "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraFCLimpo").value,
        "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraFCLimpo").value.replace(',', '.'),
        "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaFCLimpo").value,
        "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaFCLimpo").value.replace(',', '.'),
        "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoFCLimpo").value,
        "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitFCLimpo").value,
        "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaFCLimpo").value,
        "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaFCLimpo").value,
        "ID_PARCEIRO": id
        }
        $.ajax({
        type: "POST",
        url: "WebService1.asmx/CadastrarTaxaFCLimpo",
        data: JSON.stringify({dados: (dado)}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == "1") {
                var url = window.location.search.replace("?", "");
                var itens = url.split("&");
                var id_parceiro = itens.toString().replace("id=", "");
                var id = parseInt(id_parceiro);
                $.ajax({
                    type: "POST",
                    url: "WebService1.asmx/CarregarTaxaClienteFCLimpo",
                    data: '{Id:"' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdFCLimpo").empty();
                        $("#grdFCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdFCLimpo").empty();
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdFCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLimpo' onclick='BuscarFCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLimpo btn btn-primary' data-toggle='modal' data-value='"+dado[i]["ID_TAXA_CLIENTE"]+"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                        $("#msgSuccessfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    },
                    error: function (data) {
                        $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "WebService1.asmx/ListarTaxaClienteFCLimpo",
                    data: '{Id:"' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        $("#ddlTaxaClienteFCLimpo").empty();
                        for (let i = 0; i < data.length; i++) {
                            $("#ddlTaxaClienteFCLimpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                        }
                    },
                    error: function (data) {

                    },
                });
            }
            else {
                $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
            }
        },
            error: function () {
                $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
        }
        });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarEditFCLimpo").click(function () {
            if (document.getElementById("MainContent_baseCompraFCLimpo").value == "") {
                document.getElementById("MainContent_baseCompraFCLimpo").value = 0;
            }
            var dadoEdit = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemFCLimpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoFCLimpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraFCLimpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraFCLimpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaFCLimpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaFCLimpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoFCLimpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitFCLimpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaFCLimpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaFCLimpo").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaClienteFCLimpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarTaxaFCLimpo",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteFCLimpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdFCLimpo").empty();
                                $("#grdFCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdFCLimpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdFCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLimpo' onclick='BuscarFCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteFCLimpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteFCLimpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteFCLimpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarLCLimpo").click(function () {
            if (document.getElementById("MainContent_baseCompraLCLimpo").value == "") {
                document.getElementById("MainContent_baseCompraLCLimpo").value = 0;
            }
            var dado = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemLCLimpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoLCLimpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraLCLimpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraLCLimpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaLCLimpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaLCLimpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoLCLimpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitLCLimpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaLCLimpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaLCLimpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarTaxaLCLimpo",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteLCLimpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdLCLimpo").empty();
                                $("#grdLCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdLCLimpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdLCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLimpo' onclick='BuscarLCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccesslclimpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteLCLimpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteLCLimpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteLCLimpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarEditLCLimpo").click(function () {
            if (document.getElementById("MainContent_baseCompraLCLimpo").value == "") {
                document.getElementById("MainContent_baseCompraLCLimpo").value = 0;
            }
            var dadoEdit = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemLCLimpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoLCLimpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraLCLimpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraLCLimpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaLCLimpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaLCLimpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoLCLimpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitLCLimpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaLCLimpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaLCLimpo").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaClienteLCLimpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarTaxaLCLimpo",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteLCLimpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdLCLimpo").empty();
                                $("#grdLCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdLCLimpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdLCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLimpo' onclick='BuscarLCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccesslclimpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteLCLimpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteLCLimpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteLCLimpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarFCLexpo").click(function () {
            if (document.getElementById("MainContent_baseCompraFCLexpo").value == "") {
                document.getElementById("MainContent_baseCompraFCLexpo").value = 0;
            }
            var dado = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemFCLexpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoFCLexpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraFCLexpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraFCLexpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaFCLexpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaFCLexpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoFCLexpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitFCLexpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaFCLexpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaFCLexpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarTaxaFCLexpo",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteFCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdFCLexpo").empty();
                                $("#grdFCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdFCLexpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdFCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLexpo' onclick='BuscarFCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteFCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteFCLexpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteFCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarEditFCLexpo").click(function () {
            if (document.getElementById("MainContent_baseCompraFCLexpo").value == "") {
                document.getElementById("MainContent_baseCompraFCLexpo").value = 0;
            }
            var dadoEdit = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemFCLexpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoFCLexpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraFCLexpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraFCLexpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaFCLexpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaFCLexpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoFCLexpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitFCLexpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaFCLexpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaFCLexpo").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaClienteFCLexpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarTaxaFCLexpo",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteFCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdFCLexpo").empty();
                                $("#grdFCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdFCLexpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdFCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLexpo' onclick='BuscarFCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteFCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteFCLexpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteFCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarLCLexpo").click(function () {
            if (document.getElementById("MainContent_baseCompraLCLexpo").value == "") {
                document.getElementById("MainContent_baseCompraLCLexpo").value = 0;
            }
            var dado = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemLCLexpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoLCLexpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraLCLexpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraLCLexpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaLCLexpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaLCLexpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoLCLexpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitLCLexpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaLCLexpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaLCLexpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarTaxaLCLexpo",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteLCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdLCLexpo").empty();
                                $("#grdLCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdLCLexpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdLCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLexpo' onclick='BuscarLCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccesslclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteLCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteLCLexpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteLCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarEditLCLexpo").click(function () {
            if (document.getElementById("MainContent_baseCompraLCLexpo").value == "") {
                document.getElementById("MainContent_baseCompraLCLexpo").value = 0;
            }
            var dadoEdit = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemLCLexpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoLCLexpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraLCLexpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraLCLexpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaLCLexpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaLCLexpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoLCLexpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitLCLexpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaLCLexpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaLCLexpo").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaClienteLCLexpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarTaxaLCLexpo",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteLCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdLCLexpo").empty();
                                $("#grdLCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdLCLexpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdLCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLexpo' onclick='BuscarLCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccesslclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteLCLexpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteLCLexpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteLCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarAereoImpo").click(function () {
            if (document.getElementById("MainContent_baseCompraAereoImpo").value == "") {
                document.getElementById("MainContent_baseCompraAereoImpo").value = 0;
            }
            var dado = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemAereoImpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoAereoImpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraAereoImpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraAereoImpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaAereoImpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaAereoImpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoAereoImpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitAereoImpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaAereoImpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaAereoImpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarTaxaAereoImpo",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteAereoImpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdAereoImpo").empty();
                                $("#grdAereoImpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdAereoImpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdAereoImpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoImpo' onclick='BuscarAereoImpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteImpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteAereoImpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteAereoImpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteAereoImpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarEditAereoImpo").click(function () {
            if (document.getElementById("MainContent_baseCompraAereoImpo").value == "") {
                document.getElementById("MainContent_baseCompraAereoImpo").value = 0;
            }
            var dadoEdit = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemAereoImpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoAereoImpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraAereoImpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraAereoImpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaAereoImpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaAereoImpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoAereoImpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitAereoImpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaAereoImpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaAereoImpo").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaClienteAereoImpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarTaxaAereoImpo",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteAereoImpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdAereoImpo").empty();
                                $("#grdAereoImpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdAereoImpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdAereoImpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoImpo' onclick='BuscarAereoImpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteImpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteAereoImpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteAereoImpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteAereoImpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarAereoExpo").click(function () {
            if (document.getElementById("MainContent_baseCompraAereoExpo").value == "") {
                document.getElementById("MainContent_baseCompraAereoExpo").value = 0;
            }
            var dado = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemAereoExpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoAereoExpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraAereoExpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraAereoExpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaAereoExpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaAereoExpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoAereoExpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitAereoExpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaAereoExpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaAereoExpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CadastrarTaxaAereoExpo",
                data: JSON.stringify({ dados: (dado) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteAereoExpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdAereoExpo").empty();
                                $("#grdAereoExpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdAereoExpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdAereoExpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoExpo' onclick='BuscarAereoExpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteExpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteAereoExpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteAereoExpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteAereoExpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });

        var url = window.location.search.replace("?", "");
        var itens = url.split("&");
        var id_parceiro = itens.toString().replace("id=", "");
        var id = parseInt(id_parceiro);
        $("#btnSalvarEditAereoExpo").click(function () {
            if (document.getElementById("MainContent_baseCompraAereoExpo").value == "") {
                document.getElementById("MainContent_baseCompraAereoExpo").value = 0;
            }
            var dadoEdit = {
                "ID_ITEM_DESPESA": document.getElementById("MainContent_txtCodigoTipoItemAereoExpo").value,
                "ID_BASE_CALCULO_TAXA": document.getElementById("MainContent_ddlBaseCalculoAereoExpo").value,
                "ID_MOEDA_COMPRA": document.getElementById("MainContent_txtMoedaCompraAereoExpo").value,
                "VL_TAXA_COMPRA": document.getElementById("MainContent_baseCompraAereoExpo").value.replace(',', '.'),
                "ID_MOEDA_VENDA": document.getElementById("MainContent_txtMoedaVendaAereoExpo").value,
                "VL_TAXA_VENDA": document.getElementById("MainContent_baseVendaAereoExpo").value.replace(',', '.'),
                "FL_DECLARADO": document.getElementById("MainContent_ddlDeclaradoAereoExpo").value,
                "FL_DIVISAO_PROFIT": document.getElementById("MainContent_ddlProfitAereoExpo").value,
                "ID_DESTINATARIO_COBRANCA": document.getElementById("MainContent_ddlCobrancaAereoExpo").value,
                "OB_TAXAS": document.getElementById("MainContent_txtObsTaxaAereoExpo").value,
                "ID_TAXA_CLIENTE": document.getElementById("ddlTaxaClienteAereoExpo").value,
                "ID_PARCEIRO": id
            }
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/EditarTaxaAereoExpo",
                data: JSON.stringify({ dadosEdit: (dadoEdit) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        var url = window.location.search.replace("?", "");
                        var itens = url.split("&");
                        var id_parceiro = itens.toString().replace("id=", "");
                        var id = parseInt(id_parceiro);
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/CarregarTaxaClienteAereoExpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("#grdAereoExpo").empty();
                                $("#grdAereoExpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                            },
                            success: function (dado) {
                                var dado = dado.d;
                                dado = $.parseJSON(dado);
                                $("#grdAereoExpo").empty();
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdAereoExpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoExpo' onclick='BuscarAereoExpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteExpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                                $("#msgSuccessexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            },
                            error: function (data) {
                                $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            url: "WebService1.asmx/ListarTaxaClienteAereoExpo",
                            data: '{Id:"' + id + '" }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var data = data.d;
                                data = $.parseJSON(data);
                                $("#ddlTaxaClienteAereoExpo").empty();
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteAereoExpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            },
                            error: function (data) {

                            },
                        });
                    }
                    else {
                        $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                    }
                },
                error: function () {
                    $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
        });
    </script>
    <script type="text/javascript">
        function DisableFCLimpo() {
            var forms = ['MainContent_txtCodigoTipoItemFCLimpo',
                'MainContent_ddlTipoItemFCLimpo',
                'MainContent_ddlBaseCalculoFCLimpo',
                'MainContent_txtMoedaCompraFCLimpo',
                'MainContent_ddlTipoMoedaCompraFCLimpo',
                'MainContent_baseCompraFCLimpo',
                'MainContent_txtMoedaCompraFCLimpo',
                'MainContent_ddlTipoMoedaCompraFCLimpo',
                'MainContent_txtMoedaVendaFCLimpo',
                'MainContent_ddlTipoMoedaVendaFCLimpo',
                'MainContent_baseVendaFCLimpo',
                'MainContent_ddlDeclaradoFCLimpo',
                'MainContent_ddlProfitFCLimpo',
                'MainContent_ddlCobrancaFCLimpo',
                'MainContent_txtObsTaxaFCLimpo'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }
        }
        function DisableLCLimpo() {
            var forms = ['MainContent_txtCodigoTipoItemLCLimpo',
                'MainContent_ddlTipoItemLCLimpo',
                'MainContent_ddlBaseCalculoLCLimpo',
                'MainContent_txtMoedaCompraLCLimpo',
                'MainContent_ddlTipoMoedaCompraLCLimpo',
                'MainContent_baseCompraLCLimpo',
                'MainContent_txtMoedaCompraLCLimpo',
                'MainContent_ddlTipoMoedaCompraLCLimpo',
                'MainContent_txtMoedaVendaLCLimpo',
                'MainContent_ddlTipoMoedaVendaLCLimpo',
                'MainContent_baseVendaLCLimpo',
                'MainContent_ddlDeclaradoLCLimpo',
                'MainContent_ddlProfitLCLimpo',
                'MainContent_ddlCobrancaLCLimpo',
                'MainContent_txtObsTaxaLCLimpo'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }


        }
        function DisableFCLexpo() {
            var forms = ['MainContent_txtCodigoTipoItemFCLexpo',
                'MainContent_ddlTipoItemFCLexpo',
                'MainContent_ddlBaseCalculoFCLexpo',
                'MainContent_txtMoedaCompraFCLexpo',
                'MainContent_ddlTipoMoedaCompraFCLexpo',
                'MainContent_baseCompraFCLexpo',
                'MainContent_txtMoedaCompraFCLexpo',
                'MainContent_ddlTipoMoedaCompraFCLexpo',
                'MainContent_txtMoedaVendaFCLexpo',
                'MainContent_ddlTipoMoedaVendaFCLexpo',
                'MainContent_baseVendaFCLexpo',
                'MainContent_ddlDeclaradoFCLexpo',
                'MainContent_ddlProfitFCLexpo',
                'MainContent_ddlCobrancaFCLexpo',
                'MainContent_txtObsTaxaFCLexpo'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }


        }
        function DisableLCLexpo() {
            var forms = ['MainContent_txtCodigoTipoItemLCLexpo',
                'MainContent_ddlTipoItemLCLexpo',
                'MainContent_ddlBaseCalculoLCLexpo',
                'MainContent_txtMoedaCompraLCLexpo',
                'MainContent_ddlTipoMoedaCompraLCLexpo',
                'MainContent_baseCompraLCLexpo',
                'MainContent_txtMoedaCompraLCLexpo',
                'MainContent_ddlTipoMoedaCompraLCLexpo',
                'MainContent_txtMoedaVendaLCLexpo',
                'MainContent_ddlTipoMoedaVendaLCLexpo',
                'MainContent_baseVendaLCLexpo',
                'MainContent_ddlDeclaradoLCLexpo',
                'MainContent_ddlProfitLCLexpo',
                'MainContent_ddlCobrancaLCLexpo',
                'MainContent_txtObsTaxaLCLexpo'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }


        }
        function DisableAereoImpo() {
            var forms = ['MainContent_txtCodigoTipoItemAereoImpo',
                'MainContent_ddlTipoItemAereoImpo',
                'MainContent_ddlBaseCalculoAereoImpo',
                'MainContent_txtMoedaCompraAereoImpo',
                'MainContent_ddlTipoMoedaCompraAereoImpo',
                'MainContent_baseCompraAereoImpo',
                'MainContent_txtMoedaCompraAereoImpo',
                'MainContent_ddlTipoMoedaCompraAereoImpo',
                'MainContent_txtMoedaVendaAereoImpo',
                'MainContent_ddlTipoMoedaVendaAereoImpo',
                'MainContent_baseVendaAereoImpo',
                'MainContent_ddlDeclaradoAereoImpo',
                'MainContent_ddlProfitAereoImpo',
                'MainContent_ddlCobrancaAereoImpo',
                'MainContent_txtObsTaxaAereoImpo'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }


        }
        function DisableAereoExpo() {
            var forms = ['MainContent_txtCodigoTipoItemAereoExpo',
                'MainContent_ddlTipoItemAereoExpo',
                'MainContent_ddlBaseCalculoAereoExpo',
                'MainContent_txtMoedaCompraAereoExpo',
                'MainContent_ddlTipoMoedaCompraAereoExpo',
                'MainContent_baseCompraAereoExpo',
                'MainContent_txtMoedaCompraAereoExpo',
                'MainContent_ddlTipoMoedaCompraAereoExpo',
                'MainContent_txtMoedaVendaAereoExpo',
                'MainContent_ddlTipoMoedaVendaAereoExpo',
                'MainContent_baseVendaAereoExpo',
                'MainContent_ddlDeclaradoAereoExpo',
                'MainContent_ddlProfitAereoExpo',
                'MainContent_ddlCobrancaAereoExpo',
                'MainContent_txtObsTaxaAereoExpo'];
            for (let i = 0; i < forms.length; i++) {
                var aux = document.getElementById(forms[i]);
                aux.removeAttribute("disabled");
                aux.value = "";
            }


        }
    </script>
    <script>
        $(document).ready(function () {
            $("#btnEditarFCLimpo").click(function () {
                $("#btnEditarFCLimpo").hide();
                $("#btnSalvarEditFCLimpo").show();
                $("#btnCancelFCLimpo").show();
                
                var forms = ['MainContent_txtCodigoTipoItemFCLimpo',
                    'MainContent_ddlTipoItemFCLimpo',
                    'MainContent_ddlBaseCalculoFCLimpo',
                    'MainContent_txtMoedaCompraFCLimpo',
                    'MainContent_ddlTipoMoedaCompraFCLimpo',
                    'MainContent_baseCompraFCLimpo',
                    'MainContent_txtMoedaCompraFCLimpo',
                    'MainContent_ddlTipoMoedaCompraFCLimpo',
                    'MainContent_txtMoedaVendaFCLimpo',
                    'MainContent_ddlTipoMoedaVendaFCLimpo',
                    'MainContent_baseVendaFCLimpo',
                    'MainContent_ddlDeclaradoFCLimpo',
                    'MainContent_ddlProfitFCLimpo',
                    'MainContent_ddlCobrancaFCLimpo',
                    'MainContent_txtObsTaxaFCLimpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }

            })
            $("#btnCancelFCLimpo").click(function () {
                $("#btnEditarFCLimpo").show();
                $("#btnCancelFCLimpo").hide();
                $("#btnSalvarEditFCLimpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemFCLimpo',
                    'MainContent_ddlTipoItemFCLimpo',
                    'MainContent_ddlBaseCalculoFCLimpo',
                    'MainContent_txtMoedaCompraFCLimpo',
                    'MainContent_ddlTipoMoedaCompraFCLimpo',
                    'MainContent_baseCompraFCLimpo',
                    'MainContent_txtMoedaCompraFCLimpo',
                    'MainContent_ddlTipoMoedaCompraFCLimpo',
                    'MainContent_txtMoedaVendaFCLimpo',
                    'MainContent_ddlTipoMoedaVendaFCLimpo',
                    'MainContent_baseVendaFCLimpo',
                    'MainContent_ddlDeclaradoFCLimpo',
                    'MainContent_ddlProfitFCLimpo',
                    'MainContent_ddlCobrancaFCLimpo',
                    'MainContent_txtObsTaxaFCLimpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    $(aux).attr("disabled", "true");
                }
            })
            $("#btnNovaTaxaFCLimpo").click(function () {
                $("#btnSalvarFCLimpo").show();
                $("#btnSalvarEditFCLimpo").hide();
                $("#btnEditarFCLimpo").hide();
                $("#btnCancelFCLimpo").hide();
                $("#listTaxaFCLimpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemFCLimpo',
                    'MainContent_ddlTipoItemFCLimpo',
                    'MainContent_ddlBaseCalculoFCLimpo',
                    'MainContent_txtMoedaCompraFCLimpo',
                    'MainContent_ddlTipoMoedaCompraFCLimpo',
                    'MainContent_baseCompraFCLimpo',
                    'MainContent_txtMoedaCompraFCLimpo',
                    'MainContent_ddlTipoMoedaCompraFCLimpo',
                    'MainContent_txtMoedaVendaFCLimpo',
                    'MainContent_ddlTipoMoedaVendaFCLimpo',
                    'MainContent_baseVendaFCLimpo',
                    'MainContent_ddlDeclaradoFCLimpo',
                    'MainContent_ddlProfitFCLimpo',
                    'MainContent_ddlCobrancaFCLimpo',
                    'MainContent_txtObsTaxaFCLimpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            })

            $("#btnEditarLCLimpo").click(function () {
                $("#btnEditarLCLimpo").hide();
                $("#btnSalvarEditLCLimpo").show();
                $("#btnCancelLCLimpo").show();

                var forms = ['MainContent_txtCodigoTipoItemLCLimpo',
                    'MainContent_ddlTipoItemLCLimpo',
                    'MainContent_ddlBaseCalculoLCLimpo',
                    'MainContent_txtMoedaCompraLCLimpo',
                    'MainContent_ddlTipoMoedaCompraLCLimpo',
                    'MainContent_baseCompraLCLimpo',
                    'MainContent_txtMoedaCompraLCLimpo',
                    'MainContent_ddlTipoMoedaCompraLCLimpo',
                    'MainContent_txtMoedaVendaLCLimpo',
                    'MainContent_ddlTipoMoedaVendaLCLimpo',
                    'MainContent_baseVendaLCLimpo',
                    'MainContent_ddlDeclaradoLCLimpo',
                    'MainContent_ddlProfitLCLimpo',
                    'MainContent_ddlCobrancaLCLimpo',
                    'MainContent_txtObsTaxaLCLimpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }

            })
            $("#btnCancelLCLimpo").click(function () {
                $("#btnEditarLCLimpo").show();
                $("#btnCancelLCLimpo").hide();
                $("#btnSalvarEditLCLimpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemLCLimpo',
                    'MainContent_ddlTipoItemLCLimpo',
                    'MainContent_ddlBaseCalculoLCLimpo',
                    'MainContent_txtMoedaCompraLCLimpo',
                    'MainContent_ddlTipoMoedaCompraLCLimpo',
                    'MainContent_baseCompraLCLimpo',
                    'MainContent_txtMoedaCompraLCLimpo',
                    'MainContent_ddlTipoMoedaCompraLCLimpo',
                    'MainContent_txtMoedaVendaLCLimpo',
                    'MainContent_ddlTipoMoedaVendaLCLimpo',
                    'MainContent_baseVendaLCLimpo',
                    'MainContent_ddlDeclaradoLCLimpo',
                    'MainContent_ddlProfitLCLimpo',
                    'MainContent_ddlCobrancaLCLimpo',
                    'MainContent_txtObsTaxaLCLimpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    $(aux).attr("disabled", "true");
                }
            })
            $("#btnNovaTaxaLCLimpo").click(function () {
                $("#btnSalvarLCLimpo").show();
                $("#btnSalvarEditLCLimpo").hide();
                $("#btnEditarLCLimpo").hide();
                $("#btnCancelLCLimpo").hide();
                $("#listTaxaLCLimpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemLCLimpo',
                    'MainContent_ddlTipoItemLCLimpo',
                    'MainContent_ddlBaseCalculoLCLimpo',
                    'MainContent_txtMoedaCompraLCLimpo',
                    'MainContent_ddlTipoMoedaCompraLCLimpo',
                    'MainContent_baseCompraLCLimpo',
                    'MainContent_txtMoedaCompraLCLimpo',
                    'MainContent_ddlTipoMoedaCompraLCLimpo',
                    'MainContent_txtMoedaVendaLCLimpo',
                    'MainContent_ddlTipoMoedaVendaLCLimpo',
                    'MainContent_baseVendaLCLimpo',
                    'MainContent_ddlDeclaradoLCLimpo',
                    'MainContent_ddlProfitLCLimpo',
                    'MainContent_ddlCobrancaLCLimpo',
                    'MainContent_txtObsTaxaLCLimpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            })

            $("#btnEditarFCLexpo").click(function () {
                $("#btnEditarFCLexpo").hide();
                $("#btnSalvarEditFCLexpo").show();
                $("#btnCancelFCLexpo").show();

                var forms = ['MainContent_txtCodigoTipoItemFCLexpo',
                    'MainContent_ddlTipoItemFCLexpo',
                    'MainContent_ddlBaseCalculoFCLexpo',
                    'MainContent_txtMoedaCompraFCLexpo',
                    'MainContent_ddlTipoMoedaCompraFCLexpo',
                    'MainContent_baseCompraFCLexpo',
                    'MainContent_txtMoedaCompraFCLexpo',
                    'MainContent_ddlTipoMoedaCompraFCLexpo',
                    'MainContent_txtMoedaVendaFCLexpo',
                    'MainContent_ddlTipoMoedaVendaFCLexpo',
                    'MainContent_baseVendaFCLexpo',
                    'MainContent_ddlDeclaradoFCLexpo',
                    'MainContent_ddlProfitFCLexpo',
                    'MainContent_ddlCobrancaFCLexpo',
                    'MainContent_txtObsTaxaFCLexpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }

            })
            $("#btnCancelFCLexpo").click(function () {
                $("#btnEditarFCLexpo").show();
                $("#btnCancelFCLexpo").hide();
                $("#btnSalvarEditFCLexpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemFCLexpo',
                    'MainContent_ddlTipoItemFCLexpo',
                    'MainContent_ddlBaseCalculoFCLexpo',
                    'MainContent_txtMoedaCompraFCLexpo',
                    'MainContent_ddlTipoMoedaCompraFCLexpo',
                    'MainContent_baseCompraFCLexpo',
                    'MainContent_txtMoedaCompraFCLexpo',
                    'MainContent_ddlTipoMoedaCompraFCLexpo',
                    'MainContent_txtMoedaVendaFCLexpo',
                    'MainContent_ddlTipoMoedaVendaFCLexpo',
                    'MainContent_baseVendaFCLexpo',
                    'MainContent_ddlDeclaradoFCLexpo',
                    'MainContent_ddlProfitFCLexpo',
                    'MainContent_ddlCobrancaFCLexpo',
                    'MainContent_txtObsTaxaFCLexpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    $(aux).attr("disabled", "true");
                }
            })
            $("#btnNovaTaxaFCLexpo").click(function () {
                $("#btnSalvarFCLexpo").show();
                $("#btnSalvarEditFCLexpo").hide();
                $("#btnEditarFCLexpo").hide();
                $("#btnCancelFCLexpo").hide();
                $("#listTaxaFCLexpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemFCLexpo',
                    'MainContent_ddlTipoItemFCLexpo',
                    'MainContent_ddlBaseCalculoFCLexpo',
                    'MainContent_txtMoedaCompraFCLexpo',
                    'MainContent_ddlTipoMoedaCompraFCLexpo',
                    'MainContent_baseCompraFCLexpo',
                    'MainContent_txtMoedaCompraFCLexpo',
                    'MainContent_ddlTipoMoedaCompraFCLexpo',
                    'MainContent_txtMoedaVendaFCLexpo',
                    'MainContent_ddlTipoMoedaVendaFCLexpo',
                    'MainContent_baseVendaFCLexpo',
                    'MainContent_ddlDeclaradoFCLexpo',
                    'MainContent_ddlProfitFCLexpo',
                    'MainContent_ddlCobrancaFCLexpo',
                    'MainContent_txtObsTaxaFCLexpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            })

            $("#btnEditarLCLexpo").click(function () {
                $("#btnEditarLCLexpo").hide();
                $("#btnSalvarEditLCLexpo").show();
                $("#btnCancelLCLexpo").show();

                var forms = ['MainContent_txtCodigoTipoItemLCLexpo',
                    'MainContent_ddlTipoItemLCLexpo',
                    'MainContent_ddlBaseCalculoLCLexpo',
                    'MainContent_txtMoedaCompraLCLexpo',
                    'MainContent_ddlTipoMoedaCompraLCLexpo',
                    'MainContent_baseCompraLCLexpo',
                    'MainContent_txtMoedaCompraLCLexpo',
                    'MainContent_ddlTipoMoedaCompraLCLexpo',
                    'MainContent_txtMoedaVendaLCLexpo',
                    'MainContent_ddlTipoMoedaVendaLCLexpo',
                    'MainContent_baseVendaLCLexpo',
                    'MainContent_ddlDeclaradoLCLexpo',
                    'MainContent_ddlProfitLCLexpo',
                    'MainContent_ddlCobrancaLCLexpo',
                    'MainContent_txtObsTaxaLCLexpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }

            })
            $("#btnCancelLCLexpo").click(function () {
                $("#btnEditarLCLexpo").show();
                $("#btnCancelLCLexpo").hide();
                $("#btnSalvarEditLCLexpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemLCLexpo',
                    'MainContent_ddlTipoItemLCLexpo',
                    'MainContent_ddlBaseCalculoLCLexpo',
                    'MainContent_txtMoedaCompraLCLexpo',
                    'MainContent_ddlTipoMoedaCompraLCLexpo',
                    'MainContent_baseCompraLCLexpo',
                    'MainContent_txtMoedaCompraLCLexpo',
                    'MainContent_ddlTipoMoedaCompraLCLexpo',
                    'MainContent_txtMoedaVendaLCLexpo',
                    'MainContent_ddlTipoMoedaVendaLCLexpo',
                    'MainContent_baseVendaLCLexpo',
                    'MainContent_ddlDeclaradoLCLexpo',
                    'MainContent_ddlProfitLCLexpo',
                    'MainContent_ddlCobrancaLCLexpo',
                    'MainContent_txtObsTaxaLCLexpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    $(aux).attr("disabled", "true");
                }
            })
            $("#btnNovaTaxaLCLexpo").click(function () {
                $("#btnSalvarLCLexpo").show();
                $("#btnSalvarEditLCLexpo").hide();
                $("#btnEditarLCLexpo").hide();
                $("#btnCancelLCLexpo").hide();
                $("#listTaxaLCLexpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemLCLexpo',
                    'MainContent_ddlTipoItemLCLexpo',
                    'MainContent_ddlBaseCalculoLCLexpo',
                    'MainContent_txtMoedaCompraLCLexpo',
                    'MainContent_ddlTipoMoedaCompraLCLexpo',
                    'MainContent_baseCompraLCLexpo',
                    'MainContent_txtMoedaCompraLCLexpo',
                    'MainContent_ddlTipoMoedaCompraLCLexpo',
                    'MainContent_txtMoedaVendaLCLexpo',
                    'MainContent_ddlTipoMoedaVendaLCLexpo',
                    'MainContent_baseVendaLCLexpo',
                    'MainContent_ddlDeclaradoLCLexpo',
                    'MainContent_ddlProfitLCLexpo',
                    'MainContent_ddlCobrancaLCLexpo',
                    'MainContent_txtObsTaxaLCLexpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            })

            $("#btnEditarAereoImpo").click(function () {
                $("#btnEditarAereoImpo").hide();
                $("#btnSalvarEditAereoImpo").show();
                $("#btnCancelAereoImpo").show();

                var forms = ['MainContent_txtCodigoTipoItemAereoImpo',
                    'MainContent_ddlTipoItemAereoImpo',
                    'MainContent_ddlBaseCalculoAereoImpo',
                    'MainContent_txtMoedaCompraAereoImpo',
                    'MainContent_ddlTipoMoedaCompraAereoImpo',
                    'MainContent_baseCompraAereoImpo',
                    'MainContent_txtMoedaCompraAereoImpo',
                    'MainContent_ddlTipoMoedaCompraAereoImpo',
                    'MainContent_txtMoedaVendaAereoImpo',
                    'MainContent_ddlTipoMoedaVendaAereoImpo',
                    'MainContent_baseVendaAereoImpo',
                    'MainContent_ddlDeclaradoAereoImpo',
                    'MainContent_ddlProfitAereoImpo',
                    'MainContent_ddlCobrancaAereoImpo',
                    'MainContent_txtObsTaxaAereoImpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }

            })
            $("#btnCancelAereoImpo").click(function () {
                $("#btnEditarAereoImpo").show();
                $("#btnCancelAereoImpo").hide();
                $("#btnSalvarEditAereoImpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemAereoImpo',
                    'MainContent_ddlTipoItemAereoImpo',
                    'MainContent_ddlBaseCalculoAereoImpo',
                    'MainContent_txtMoedaCompraAereoImpo',
                    'MainContent_ddlTipoMoedaCompraAereoImpo',
                    'MainContent_baseCompraAereoImpo',
                    'MainContent_txtMoedaCompraAereoImpo',
                    'MainContent_ddlTipoMoedaCompraAereoImpo',
                    'MainContent_txtMoedaVendaAereoImpo',
                    'MainContent_ddlTipoMoedaVendaAereoImpo',
                    'MainContent_baseVendaAereoImpo',
                    'MainContent_ddlDeclaradoAereoImpo',
                    'MainContent_ddlProfitAereoImpo',
                    'MainContent_ddlCobrancaAereoImpo',
                    'MainContent_txtObsTaxaAereoImpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    $(aux).attr("disabled", "true");
                }
            })
            $("#btnNovaTaxaAereoImpo").click(function () {
                $("#btnSalvarAereoImpo").show();
                $("#btnSalvarEditAereoImpo").hide();
                $("#btnEditarAereoImpo").hide();
                $("#btnCancelAereoImpo").hide();
                $("#listTaxaAereoImpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemAereoImpo',
                    'MainContent_ddlTipoItemAereoImpo',
                    'MainContent_ddlBaseCalculoAereoImpo',
                    'MainContent_txtMoedaCompraAereoImpo',
                    'MainContent_ddlTipoMoedaCompraAereoImpo',
                    'MainContent_baseCompraAereoImpo',
                    'MainContent_txtMoedaCompraAereoImpo',
                    'MainContent_ddlTipoMoedaCompraAereoImpo',
                    'MainContent_txtMoedaVendaAereoImpo',
                    'MainContent_ddlTipoMoedaVendaAereoImpo',
                    'MainContent_baseVendaAereoImpo',
                    'MainContent_ddlDeclaradoAereoImpo',
                    'MainContent_ddlProfitAereoImpo',
                    'MainContent_ddlCobrancaAereoImpo',
                    'MainContent_txtObsTaxaAereoImpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            })

            $("#btnEditarAereoExpo").click(function () {
                $("#btnEditarAereoExpo").hide();
                $("#btnSalvarEditAereoExpo").show();
                $("#btnCancelAereoExpo").show();

                var forms = ['MainContent_txtCodigoTipoItemAereoExpo',
                    'MainContent_ddlTipoItemAereoExpo',
                    'MainContent_ddlBaseCalculoAereoExpo',
                    'MainContent_txtMoedaCompraAereoExpo',
                    'MainContent_ddlTipoMoedaCompraAereoExpo',
                    'MainContent_baseCompraAereoExpo',
                    'MainContent_txtMoedaCompraAereoExpo',
                    'MainContent_ddlTipoMoedaCompraAereoExpo',
                    'MainContent_txtMoedaVendaAereoExpo',
                    'MainContent_ddlTipoMoedaVendaAereoExpo',
                    'MainContent_baseVendaAereoExpo',
                    'MainContent_ddlDeclaradoAereoExpo',
                    'MainContent_ddlProfitAereoExpo',
                    'MainContent_ddlCobrancaAereoExpo',
                    'MainContent_txtObsTaxaAereoExpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }

            })
            $("#btnCancelAereoExpo").click(function () {
                $("#btnEditarAereoExpo").show();
                $("#btnCancelAereoExpo").hide();
                $("#btnSalvarEditAereoExpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemAereoExpo',
                    'MainContent_ddlTipoItemAereoExpo',
                    'MainContent_ddlBaseCalculoAereoExpo',
                    'MainContent_txtMoedaCompraAereoExpo',
                    'MainContent_ddlTipoMoedaCompraAereoExpo',
                    'MainContent_baseCompraAereoExpo',
                    'MainContent_txtMoedaCompraAereoExpo',
                    'MainContent_ddlTipoMoedaCompraAereoExpo',
                    'MainContent_txtMoedaVendaAereoExpo',
                    'MainContent_ddlTipoMoedaVendaAereoExpo',
                    'MainContent_baseVendaAereoExpo',
                    'MainContent_ddlDeclaradoAereoExpo',
                    'MainContent_ddlProfitAereoExpo',
                    'MainContent_ddlCobrancaAereoExpo',
                    'MainContent_txtObsTaxaAereoExpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    $(aux).attr("disabled", "true");
                }
            })
            $("#btnNovaTaxaAereoExpo").click(function () {
                $("#btnSalvarAereoExpo").show();
                $("#btnSalvarEditAereoExpo").hide();
                $("#btnEditarAereoExpo").hide();
                $("#btnCancelAereoExpo").hide();
                $("#listTaxaAereoExpo").hide();
                var forms = ['MainContent_txtCodigoTipoItemAereoExpo',
                    'MainContent_ddlTipoItemAereoExpo',
                    'MainContent_ddlBaseCalculoAereoExpo',
                    'MainContent_txtMoedaCompraAereoExpo',
                    'MainContent_ddlTipoMoedaCompraAereoExpo',
                    'MainContent_baseCompraAereoExpo',
                    'MainContent_txtMoedaCompraAereoExpo',
                    'MainContent_ddlTipoMoedaCompraAereoExpo',
                    'MainContent_txtMoedaVendaAereoExpo',
                    'MainContent_ddlTipoMoedaVendaAereoExpo',
                    'MainContent_baseVendaAereoExpo',
                    'MainContent_ddlDeclaradoAereoExpo',
                    'MainContent_ddlProfitAereoExpo',
                    'MainContent_ddlCobrancaAereoExpo',
                    'MainContent_txtObsTaxaAereoExpo'];
                for (let i = 0; i < forms.length; i++) {
                    var aux = document.getElementById(forms[i]);
                    aux.removeAttribute("disabled");
                }
            })
        })

        

        $(document).ready(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CarregarTaxaClienteFCLimpo",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdFCLimpo").empty();
                    $("#grdFCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdFCLimpo").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdFCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLimpo' onclick='BuscarFCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdFCLimpo").empty();
                        $("#grdFCLimpo").append("<tr id='msgEmptyfclimpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarTaxaClienteFCLimpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlTaxaClienteFCLimpo .remove").empty();
                            if (data != null) {
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteFCLimpo").append("<option class='remove' value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                    },
                error: function (data) {
                    alert(data);
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CarregarTaxaClienteLCLimpo",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdLCLimpo").empty();
                    $("#grdLCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdLCLimpo").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdLCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLimpo' onclick='BuscarLCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdLCLimpo").empty();
                        $("#grdLCLimpo").append("<tr id='msgEmptylclimpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarTaxaClienteLCLimpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlTaxaClienteLCLimpo").empty();
                            if (data != null) {
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteLCLimpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {
                    alert(data);
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CarregarTaxaClienteFCLexpo",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdFCLexpo").empty();
                    $("#grdFCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdFCLexpo").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdFCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLexpo' onclick='BuscarFCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdFCLexpo").empty();
                        $("#grdFCLexpo").append("<tr id='msgEmptyfclexpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarTaxaClienteFCLexpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlTaxaClienteFCLexpo").empty();
                            if (data != null) {
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteFCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {
                    alert(data);
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CarregarTaxaClienteLCLexpo",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdLCLexpo").empty();
                    $("#grdLCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdLCLexpo").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdLCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLexpo' onclick='BuscarLCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdLCLexpo").empty();
                        $("#grdLCLexpo").append("<tr id='msgEmptylclexpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");

                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarTaxaClienteLCLexpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlTaxaClienteLCLexpo").empty();
                            if (data != null) {
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteLCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            }

                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {
                    alert(data);
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CarregarTaxaClienteAereoImpo",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdAereoImpo").empty();
                    $("#grdAereoImpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdAereoImpo").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdAereoImpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoImpo' onclick='BuscarAereoImpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteImpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdAereoImpo").empty();
                        $("#grdAereoImpo").append("<tr id='msgEmptyimpoaereo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");

                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarTaxaClienteAereoImpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlTaxaClienteAereoImpo").empty();
                            if (data != null) {
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteAereoImpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {
                    alert(data);
                }
            });
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CarregarTaxaClienteAereoExpo",
                data: '{Id:"' + id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdAereoExpo").empty();
                    $("#grdAereoExpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdAereoExpo").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdAereoExpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoExpo' onclick='BuscarAereoExpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteExpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] +"'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                        }
                    }
                    else {
                        $("#grdAereoExpo").empty();
                        $("#grdAereoExpo").append("<tr id='msgEmptyexpoaereo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                    }
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/ListarTaxaClienteAereoExpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var data = data.d;
                            data = $.parseJSON(data);
                            $("#ddlTaxaClienteAereoExpo").empty();
                            if (data != null) {
                                for (let i = 0; i < data.length; i++) {
                                    $("#ddlTaxaClienteAereoExpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                }
                            }
                        },
                        error: function (data) {

                        },
                    });
                },
                error: function (data) {
                    alert(data);
                }
            });
        });
        var idFCLimpo;
        var idLCLimpo;
        var idFCLexpo;
        var idLCLexpo;
        var idAereoimpo;
        var idAereoexpo;
        $(document).on("click", ".deleteFCLimpo", function () {
            $("#modalDeleteTaxaFCLimpo").modal('show');
            idFCLimpo = $(this).attr('data-value');
            $("#modalDeleteTaxaFCLimpo").val(idFCLimpo);           
        });

        $(document).on("click", ".deleteLCLimpo", function () {
            $("#modalDeleteTaxaLCLimpo").modal('show');
            idLCLimpo = $(this).attr('data-value');
            $("#modalDeleteTaxaLCLimpo").val(idLCLimpo);
        });

        $(document).on("click", ".deleteFCLexpo", function () {
            $("#modalDeleteTaxaFCLexpo").modal('show');
            idFCLexpo = $(this).attr('data-value');
            $("#modalDeleteTaxaFCLexpo").val(idFCLexpo);
        });

        $(document).on("click", ".deleteLCLexpo", function () {
            $("#modalDeleteTaxaLCLexpo").modal('show');
            idLCLexpo = $(this).attr('data-value');
            $("#modalDeleteTaxaLCLexpo").val(idLCLexpo);
        });

        $(document).on("click", ".deleteImpoAereo", function () {
            $("#modalDeleteTaxaImpoAereo").modal('show');
            idAereoimpo = $(this).attr('data-value');
            $("#modalDeleteTaxaImpoAereo").val(idAereoimpo);
        });

        $(document).on("click", ".deleteExpoAereo", function () {
            $("#modalDeleteTaxaExpoAereo").modal('show');
            idAereoexpo = $(this).attr('data-value');
            $("#modalDeleteTaxaExpoAereo").val(idAereoexpo);
        });

        $("#btnDeletarTaxaFCLimpo").click(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/DeletarTaxa",
                data: '{Id:"' + id + '",IdTaxa: "' + idFCLimpo+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdFCLimpo").empty();
                    $("#grdFCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    $("#msgSuccessfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CarregarTaxaClienteFCLimpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdFCLimpo").empty();
                            $("#grdFCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            $("#grdFCLimpo").empty();
                            if (dado != null) {
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdFCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLimpo' onclick='BuscarFCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                            }
                            else {
                                $("#grdFCLimpo").empty();
                                $("#grdFCLimpo").append("<tr id='msgEmptyfclimpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                            }
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarTaxaClienteFCLimpo",
                                data: '{Id:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var data = data.d;
                                    data = $.parseJSON(data);
                                    $("#ddlTaxaClienteFCLimpo .remove").empty();
                                    if (data != null) {
                                        for (let i = 0; i < data.length; i++) {
                                            $("#ddlTaxaClienteFCLimpo").append("<option class='remove' value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                        }
                                    }
                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            alert(data);
                        }
                    });
                },
                error: function () {
                    $("#msgErrfclimpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
            $("#modalDeleteTaxaFCLimpo").modal('hide');

        });

        $("#btnDeletarTaxaLCLimpo").click(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/DeletarTaxa",
                data: '{Id:"' + id + '",IdTaxa: "' + idLCLimpo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdLCLimpo").empty();
                    $("#grdLCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    $("#msgSuccesslclimpo").fadeIn(500).delay(3500).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CarregarTaxaClienteLCLimpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdLCLimpo").empty();
                            $("#grdLCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            $("#grdLCLimpo").empty();
                            if (dado != null) {
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdLCLimpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLimpo' onclick='BuscarLCLimpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLimpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                            }
                            else {
                                $("#grdLCLimpo").empty();
                                $("#grdLCLimpo").append("<tr id='msgEmptylclimpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                            }
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarTaxaClienteLCLimpo",
                                data: '{Id:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var data = data.d;
                                    data = $.parseJSON(data);
                                    $("#ddlTaxaClienteLCLimpo").empty();
                                    if (data != null) {
                                        for (let i = 0; i < data.length; i++) {
                                            $("#ddlTaxaClienteLCLimpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                        }
                                    }
                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            alert(data);
                        }
                    });
                },
                error: function () {
                    $("#msgErrlclimpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            })
            $("#modalDeleteTaxaLCLimpo").modal('hide');

        });

        $("#btnDeletarTaxaFCLexpo").click(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/DeletarTaxa",
                data: '{Id:"' + id + '",IdTaxa: "' + idFCLexpo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdFCLexpo").empty();
                    $("#grdFCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    $("#msgSuccessfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CarregarTaxaClienteFCLexpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdFCLexpo").empty();
                            $("#grdFCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            $("#grdFCLexpo").empty();
                            if (dado != null) {
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdFCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalFCLexpo' onclick='BuscarFCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteFCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                            }
                            else {
                                $("#grdFCLexpo").empty();
                                $("#grdFCLexpo").append("<tr id='msgEmptyfclexpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                            }
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarTaxaClienteFCLexpo",
                                data: '{Id:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var data = data.d;
                                    data = $.parseJSON(data);
                                    $("#ddlTaxaClienteFCLexpo").empty();
                                    if (data != null) {
                                        for (let i = 0; i < data.length; i++) {
                                            $("#ddlTaxaClienteFCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                        }
                                    }
                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            alert(data);
                        }
                    });
                },
                error: function () {
                    $("#msgErrfclexpo").fadeIn(500).delay(3500).fadeOut(500);
                }
            })
            $("#modalDeleteTaxaFCLexpo").modal('hide');

        });

        $("#btnDeletarTaxaLCLexpo").click(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/DeletarTaxa",
                data: '{Id:"' + id + '",IdTaxa: "' + idLCLexpo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdLCLexpo").empty();
                    $("#grdLCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    $("#msgSuccesslclexpo").fadeIn(500).delay(3500).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CarregarTaxaClienteLCLexpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdLCLexpo").empty();
                            $("#grdLCLexpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            $("#grdLCLexpo").empty();
                            if (dado != null) {
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdLCLexpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalLCLexpo' onclick='BuscarLCLexpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteLCLexpo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                            }
                            else {
                                $("#grdLCLexpo").empty();
                                $("#grdLCLexpo").append("<tr id='msgEmptylclexpo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");

                            }
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarTaxaClienteLCLexpo",
                                data: '{Id:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var data = data.d;
                                    data = $.parseJSON(data);
                                    $("#ddlTaxaClienteLCLexpo").empty();
                                    if (data != null) {
                                        for (let i = 0; i < data.length; i++) {
                                            $("#ddlTaxaClienteLCLexpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                        }
                                    }

                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            alert(data);
                        }
                    })
                },
                error: function () {
                    $("#msgErrlclexpo").fadeIn(500).delay(3500).fadeOut(500);

                }
            })
            $("#modalDeleteTaxaLCLexpo").modal('hide');

        })

        $("#btnDeletarTaxaImpoAereo").click(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/DeletarTaxa",
                data: '{Id:"' + id + '",IdTaxa: "' + idAereoimpo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdAereoImpo").empty();
                    $("#grdAereoImpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    $("#msgSuccessimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CarregarTaxaClienteAereoImpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdAereoImpo").empty();
                            $("#grdAereoImpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            $("#grdAereoImpo").empty();
                            if (dado != null) {
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdAereoImpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoImpo' onclick='BuscarAereoImpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteImpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                            }
                            else {
                                $("#grdAereoImpo").empty();
                                $("#grdAereoImpo").append("<tr id='msgEmptyimpoaereo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");

                            }
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarTaxaClienteAereoImpo",
                                data: '{Id:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var data = data.d;
                                    data = $.parseJSON(data);
                                    $("#ddlTaxaClienteAereoImpo").empty();
                                    if (data != null) {
                                        for (let i = 0; i < data.length; i++) {
                                            $("#ddlTaxaClienteAereoImpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                        }
                                    }
                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            alert(data);
                        }
                    });
                },
                error: function () {
                    $("#msgErrimpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                }
            })
            $("#modalDeleteTaxaImpoAereo").modal('hide');

        })

        $("#btnDeletarTaxaExpoAereo").click(function () {
            var url = window.location.search.replace("?", "");
            var itens = url.split("&");
            var id_parceiro = itens.toString().replace("id=", "");
            var id = parseInt(id_parceiro);
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/DeletarTaxa",
                data: '{Id:"' + id + '",IdTaxa: "' + idAereoexpo + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdFCLimpo").empty();
                    $("#grdFCLimpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                },
                success: function () {
                    $("#msgSuccessexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                    $.ajax({
                        type: "POST",
                        url: "WebService1.asmx/CarregarTaxaClienteAereoExpo",
                        data: '{Id:"' + id + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdAereoExpo").empty();
                            $("#grdAereoExpo").append("<tr><td class='text-center' colspan='6'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (dado) {
                            var dado = dado.d;
                            dado = $.parseJSON(dado);
                            $("#grdAereoExpo").empty();
                            if (dado != null) {
                                for (let i = 0; i < dado.length; i++) {
                                    $("#grdAereoExpo").append("<tr><td><div class='btn btn-primary' data-toggle='modal' data-target='#modalAereoExpo' onclick='BuscarAereoExpo(" + dado[i]['ID_TAXA_CLIENTE'] + ")'><i class='fas fa-eye'></i></div><div class='deleteExpoAereo btn btn-primary' data-toggle='modal' data-value='" + dado[i]["ID_TAXA_CLIENTE"] + "'><i class='fas fa-trash'></i></div></td><td>" + dado[i]["ID_TAXA_CLIENTE"] + "</td><td>" + dado[i]["ITEM"] + "</td><td>" + dado[i]["NM_BASE_CALCULO_TAXA"] + "</td><td>" + dado[i]["NM_MOEDA"] + "</td><td>" + dado[i]["VL_TAXA_VENDA"] + "</td></tr>");
                                }
                            }
                            else {
                                $("#grdAereoExpo").empty();
                                $("#grdAereoExpo").append("<tr id='msgEmptyexpoaereo'><td colspan='6' class='alert alert-light text-center' >Esse parceiro não possui taxas.</td></tr>");
                            }
                            $.ajax({
                                type: "POST",
                                url: "WebService1.asmx/ListarTaxaClienteAereoExpo",
                                data: '{Id:"' + id + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var data = data.d;
                                    data = $.parseJSON(data);
                                    $("#ddlTaxaClienteAereoExpo").empty();
                                    if (data != null) {
                                        for (let i = 0; i < data.length; i++) {
                                            $("#ddlTaxaClienteAereoExpo").append("<option value='" + data[i]["ID_TAXA_CLIENTE"] + "'>" + data[i]["ID_TAXA_CLIENTE"] + " - " + data[i]["DataField"] + "</option>");
                                        }
                                    }
                                },
                                error: function (data) {

                                },
                            });
                        },
                        error: function (data) {
                            alert(data);
                        }
                    });
                },
                error: function () {
                    $("#msgErrexpoaereo").fadeIn(500).delay(3500).fadeOut(500);
                }
            });
            $("#modalDeleteTaxaExpoAereo").modal('hide');
         })

        
    </script>
</asp:Content>

