<%@ Page Title="" Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TaxasLocaisArmador.aspx.vb" Inherits="NVOCC.Web.TaxasLocaisArmador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th {
            color: #337ab7;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="row principal">




                <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="Panel1" TargetControlID="Button1" CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>


                <asp:Button runat="server" Text="teste" ID="Button1" Style="display: none" CssClass="btn btn-success" />
                <asp:Button runat="server" Text="teste" ID="Button2" Style="display: none" CssClass="btn btn-success" />

                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                        <ContentTemplate>
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalFCLimpoTitle">TAXAS LOCAIS ARMADOR</h5>
                                        <asp:LinkButton ID="lkAnterior" runat="server" BackColor="White" ForeColor="Black" Style="float: right;" CssClass="btn btn-default"><i class="glyphicon glyphicon-step-backward"></i></asp:LinkButton><asp:LinkButton ID="lkProximo" runat="server" BackColor="White" ForeColor="Black" Style="float: right;" CssClass="btn btn-default"><i class="glyphicon glyphicon-step-forward"></i></asp:LinkButton>
                                    </div>
                                    <div class="modal-body">
                                        <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                            <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                        </div>

                                        <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                            <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Código:</label>
                                                    <asp:TextBox ID="txtIDTaxa" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Transportador:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlTransportadorTaxa" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">


                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Porto:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlPortoTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Origem Serviço:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlOrigemPagamento" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Tipo Comex:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlComexTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_COMEX" DataSourceID="dsComex" DataValueField="ID_TIPO_COMEX">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Item de Despesa:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlDespesaTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Data de Validade (Inicial):</label><label runat="server" style="color: red">*</label>
                                                    <asp:TextBox ID="txtValidadeInicialTaxa" runat="server" CssClass="form-control data"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Valor Taxa Local:</label><label runat="server" style="color: red">*</label>
                                                    <asp:TextBox ID="txtValorTaxaLocal" runat="server" CssClass="form-control moeda"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Base de calculo:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlBaseCalculo" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Qtd. Base de cálculo:</label>
                                                    <asp:TextBox ID="txtQtdBaseCalculo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" Text="Salvar Taxa" ID="btnSalvar" CssClass="btn btn-success" />
                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" Text="Close" />
                                    </div>

                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lkProximo" />
                            <asp:AsyncPostBackTrigger ControlID="ddlBaseCalculo" />
                            <asp:AsyncPostBackTrigger ControlID="lkAnterior" />
                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                            <asp:AsyncPostBackTrigger ControlID="btnSalvar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>


                <ajaxToolkit:ModalPopupExtender ID="mpeNovo" runat="server" PopupControlID="Panel2" TargetControlID="btnNovo" CancelControlID="Button1"></ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                        <ContentTemplate>
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">TAXAS LOCAIS ARMADOR</h5>
                                    </div>
                                    <div class="modal-body">
                                        <div class="alert alert-success" id="divSuccessNovo" runat="server" visible="false">
                                            <asp:Label ID="lblmsgSuccessNovo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                        </div>
                                        <div class="alert alert-danger" id="divErroNovo" runat="server" visible="false">
                                            <asp:Label ID="lblmsgErroNovo" runat="server"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Código:</label>
                                                    <asp:TextBox ID="txtIDTaxaNovo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Transportador:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlTransportadorTaxaNovo" Enabled="false" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">


                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Porto:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlPortoTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Origem Serviço:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlOrigemPagamentoNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlViaTransporteNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Tipo Comex:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlComexTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_COMEX" DataSourceID="dsComex" DataValueField="ID_TIPO_COMEX">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Item de Despesa:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlDespesaTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label class="control-label">Data de Validade (Inicial):</label><label runat="server" style="color: red">*</label>
                                                    <asp:TextBox ID="txtValidadeInicialTaxaNovo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Valor Taxa Local:</label><label runat="server" style="color: red">*</label>
                                                    <asp:TextBox ID="txtValorTaxaLocalNovo" runat="server" CssClass="form-control moeda"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlMoedaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Base de calculo:</label><label runat="server" style="color: red">*</label>
                                                    <asp:DropDownList ID="ddlBaseCalculoNovo" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Qtd. Base de Calculo:</label>
                                                    <asp:TextBox ID="txtQtdBaseCalculoNovo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" Text="Salvar Taxa" ID="btnSalvarNovo" CssClass="btn btn-success" />
                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNovo" Text="Close" />
                                    </div>

                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSalvarNovo" />
                            <asp:AsyncPostBackTrigger ControlID="btnFecharNovo" />
                            <asp:AsyncPostBackTrigger ControlID="ddlBaseCalculoNovo" />

                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>


                <asp:Button runat="server" Text="teste" ID="Button4" Style="display: none" CssClass="btn btn-success" />
                <asp:Button runat="server" Text="teste" ID="Button3" Style="display: none" CssClass="btn btn-success" />
                <ajaxToolkit:ModalPopupExtender ID="mpeAjusta" runat="server" PopupControlID="pnlAjusta" TargetControlID="Button4"></ajaxToolkit:ModalPopupExtender>


                <asp:Panel ID="pnlAjusta" runat="server" CssClass="modalPopup" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">ATUALIZA TAXAS</h5>
                                    </div>
                                    <div class="modal-body">
                                        <div class="alert alert-success" id="divSuccessAjusta" runat="server" visible="false">
                                            <asp:Label ID="lblSuccessAjusta" runat="server"></asp:Label>
                                        </div>

                                        <div class="alert alert-danger" id="divErroAjusta" runat="server" visible="false">
                                            <asp:Label ID="lblErroAjusta" runat="server"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtMsg" runat="server" Style="display: none" CssClass="form-control"></asp:TextBox>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblItemDespesa" CssClass="control-label" Style="display: none" />
                                                    <asp:Label runat="server" ID="lblPorto" CssClass="control-label" Style="display: none" />
                                                    <asp:Label runat="server" ID="lblComex" CssClass="control-label" Style="display: none" />
                                                    <asp:Label runat="server" ID="lblVia" CssClass="control-label" Style="display: none" />
                                                    <asp:Button runat="server" Text="Marcar Todos" ID="btnMarcar" CssClass="btn btn-primary" />
                                                    <asp:Button runat="server" Text="Desmarcar Todos" ID="btnDesmarcar" CssClass="btn btn-warning" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group" style="margin-bottom: 18px; display: none" id="divTaxaAntiga" runat="server">
                                                    <label class="control-label">Taxa Local Antiga:</label><br />
                                                    <asp:Label runat="server" ID="lblValorAntigo" CssClass="control-label" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group" style="margin-bottom: 18px">
                                                    <label class="control-label">Taxa Local Nova:</label><br />
                                                    <asp:Label runat="server" ID="lblValorNovo" CssClass="control-label" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="table-responsive tableFixHead">
                                            <asp:GridView ID="dgvAjustaTaxa" DataKeyNames="id_cotacao_taxa" DataSourceID="dsAjustaTaxa" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid_cotacao_taxa" runat="server" Text='<%# Eval("id_cotacao_taxa") %>' />
                                                            <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_BL") %>' Style="display: none" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbSelecionar" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NR_COTACAO" HeaderText="COTAÇÃO" SortExpression="NR_COTACAO" />
                                                    <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                    <asp:TemplateField HeaderText="ARMADOR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARMADOR" runat="server" Text='<%# Eval("ARMADOR") %>' />
                                                            <asp:Label ID="lblID_TRANSPORTADOR" runat="server" Text='<%# Eval("ID_TRANSPORTADOR") %>' Style="display: none" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DESPESA">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEM_DESPESA" runat="server" Text='<%# Eval("NM_ITEM_DESPESA") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PORTO" HeaderText="PORTO" SortExpression="PORTO" />
                                                    <asp:BoundField DataField="DT_EMBARQUE" HeaderText="EMBARQUE" SortExpression="DT_EMBARQUE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="DT_CHEGADA" HeaderText="CHEGADA" SortExpression="DT_CHEGADA" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="REGRA" HeaderText="REGRA" SortExpression="REGRA" />

                                                    <asp:TemplateField HeaderText="COMPRA(COTAÇÃO)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVL_TAXA_COMPRA" runat="server" Text='<%# Eval("VL_TAXA_COMPRA") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VENDA(COTAÇÃO)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVL_TAXA_VENDA" runat="server" Text='<%# Eval("VL_TAXA_VENDA") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="VL_TAXA_COMPRA" HeaderText="COMPRA(COTAÇÃO)" SortExpression="VL_TAXA_COMPRA" />
                                                   <asp:BoundField DataField="VL_TAXA_VENDA"  HeaderText="VENDA(COTAÇÃO)" SortExpression="VL_TAXA_VENDA" /> --%>
                                                    <asp:BoundField DataField="vl_taxa_local_compra" HeaderText="TAXA LOCAL" SortExpression="vl_taxa_local_compra" />
                                                    <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="VALIDADE INICIAL" SortExpression="DT_VALIDADE_INICIAL" DataFormatString="{0:dd/MM/yyyy}" />
                                                </Columns>
                                                <HeaderStyle CssClass="headerStyle" />
                                            </asp:GridView>

                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" CssClass="btn btn-success btnn" ID="btnAjustar" Text="Ajustar" />
                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharAjustaTaxa" Text="Close" />
                                    </div>

                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAjustar" />
                            <asp:AsyncPostBackTrigger ControlID="btnSalvarNovo" />
                            <asp:AsyncPostBackTrigger ControlID="btnSalvar" />
                            <asp:AsyncPostBackTrigger ControlID="btnMarcar" />
                            <asp:AsyncPostBackTrigger ControlID="btnDesmarcar" />
                            <asp:AsyncPostBackTrigger ControlID="btnFecharAjustaTaxa" />
                        </Triggers>
                    </asp:UpdatePanel>

                </asp:Panel>






                <div class="col-lg-12 ">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">TAXAS LOCAIS ARMADOR
                            </h3>
                        </div>

                        <div class="row" style="padding: 10px">
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label">Comex:</label>
                                    <asp:DropDownList ID="ddlComexConsulta" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_COMEX" DataSourceID="dsComexConsulta" DataValueField="ID_TIPO_COMEX" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label">Consultar por:</label>
                                    <asp:DropDownList ID="ddlConsulta" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True">
                                        <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                        <asp:ListItem Value="1">Porto</asp:ListItem>
                                        <asp:ListItem Value="2">Via Transporte</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-2" id="divPesquisa" runat="server">
                                <div class="form-group">
                                    <label class="control-label">Pesquisar</label>
                                    <asp:TextBox ID="txtConsulta" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="msgerro" runat="server" Style="color: red" />
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">
                            <br />
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Button runat="server" Text="Nova Taxa" ID="btnNovo" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>

                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <div class="alert alert-success" id="divExcluir_Success" runat="server" visible="false">
                                        <asp:Label ID="lblExcluir_Success" runat="server" Text="Registro deletado com sucesso!"></asp:Label>
                                    </div>
                                    <div class="alert alert-info" id="divinfo" runat="server" visible="false">
                                        <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-danger" id="divExcluir_Erro" runat="server" visible="false">
                                        <asp:Label ID="lblExcluir_Erro" runat="server"></asp:Label>
                                    </div>
                                    <div class="table-responsive tableFixHead" id="divGrid" runat="server">

                                        <asp:GridView ID="dgvTaxas" DataKeyNames="ID_TAXA_LOCAL_TRANSPORTADOR" DataSourceID="dstaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" OnSorting="dgvTaxas_Sorting">
                                            <Columns>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>' Text="Visualizar" CssClass="btn btn-primary btn-sm"><i class="fas fa-eye"></i></div></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>'
                                                            Text="Duplicar" CssClass="btn btn-warning btn-sm" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate"></i></div></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAtualiza" title="Atualização de Taxas" runat="server" CssClass="btn btn-success btn-sm" CommandName="Atualiza" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>' Autopostback="true"><i class="glyphicon glyphicon-upload"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="ID_TAXA_LOCAL_TRANSPORTADOR" HeaderText="#" SortExpression="ID_TAXA_LOCAL_TRANSPORTADOR" />
                                                <asp:BoundField DataField="NM_PORTO" HeaderText="Porto" SortExpression="NM_PORTO" />
                                                <asp:BoundField DataField="NM_TIPO_COMEX" HeaderText="Tipo Comex" SortExpression="NM_TIPO_COMEX" />
                                                <asp:BoundField DataField="NM_VIATRANSPORTE" HeaderText="Transporte" SortExpression="NM_VIATRANSPORTE" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Item Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="NM_BASE_CALCULO_TAXA" HeaderText="Base de Cálculo" SortExpression="NM_BASE_CALCULO_TAXA" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="Moeda" SortExpression="SIGLA_MOEDA" />
                                                <asp:BoundField DataField="VL_TAXA_LOCAL_COMPRA" HeaderText="Valor Taxa Local(Compra)" SortExpression="VL_TAXA_LOCAL_COMPRA" />
                                                <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Data de Validade (Inicial)" SortExpression="DT_VALIDADE_INICIAL" DataFormatString="{0:dd/MM/yyyy}" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxas" />
                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvarNovo" />
                                    <asp:AsyncPostBackTrigger ControlID="txtConsulta" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlComexConsulta" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlConsulta" />
                                </Triggers>
                            </asp:UpdatePanel>



                        </div>

                    </div>
                </div>


                <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT A.ID_TAXA_LOCAL_TRANSPORTADOR,
A.ID_TRANSPORTADOR,
A.ID_PORTO,B.NM_PORTO,
A.ID_TIPO_COMEX,D.NM_TIPO_COMEX,
A.ID_VIATRANSPORTE,C.NM_VIATRANSPORTE,
A.ID_ITEM_DESPESA,F.NM_ITEM_DESPESA,
A.VL_TAXA_LOCAL_COMPRA,
A.DT_VALIDADE_INICIAL,E.NM_BASE_CALCULO_TAXA,G.SIGLA_MOEDA
FROM 
TB_TAXA_LOCAL_TRANSPORTADOR A 
LEFT JOIN TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
LEFT JOIN TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
LEFT JOIN TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX
LEFT JOIN TB_ITEM_DESPESA F ON F.ID_ITEM_DESPESA = A.ID_ITEM_DESPESA
LEFT JOIN TB_BASE_CALCULO_TAXA E ON E.ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO
LEFT JOIN TB_MOEDA G ON G.ID_MOEDA = A.ID_MOEDA         
        WHERE a.ID_TIPO_COMEX = @ComexConsulta  AND  ID_TRANSPORTADOR = @ID  ORDER BY B.NM_PORTO,A.ID_TAXA_LOCAL_TRANSPORTADOR">
                    <SelectParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                        <asp:ControlParameter Name="ComexConsulta" Type="Int32" ControlID="ddlComexConsulta" DefaultValue="1" />

                    </SelectParameters>

                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,ID_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL 
          union SELECT  0, ' Selecione' ORDER BY NM_PORTO "></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsComex" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM [dbo].[TB_TIPO_COMEX]
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_COMEX"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM  [dbo].[TB_ITEM_DESPESA]
union SELECT  0, ' Selecione' FROM [dbo].[TB_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsContinente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_CONTINENTE,NM_CONTINENTE FROM [dbo].[TB_CONTINENTE]
union SELECT  0, 'Selecione' ORDER BY ID_CONTINENTE"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1 
union SELECT  0, 'Selecione'  ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione'  ORDER BY ID_TIPO_CONTAINER"></asp:SqlDataSource>

                <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
                    SelectCommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM  [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT  0, 'Selecione' ORDER BY ID_ORIGEM_PAGAMENTO"></asp:SqlDataSource>

            </div>
        </ContentTemplate>


        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNovo" />
            <asp:AsyncPostBackTrigger ControlID="btnFechar" />
            <asp:AsyncPostBackTrigger ControlID="btnFecharNovo" />

            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />

        </Triggers>
    </asp:UpdatePanel>


    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT  0, 'Selecione'  ORDER BY ID_MOEDA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComexConsulta" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM TB_TIPO_COMEX ORDER BY ID_TIPO_COMEX"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT  0,  '    Selecione' ORDER BY NM_BASE_CALCULO_TAXA"></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsAjustaTaxa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select id_cotacao_taxa,ID_BL,A.ID_TRANSPORTADOR, (select nr_cotacao from tb_cotacao where nr_processo_gerado = NR_PROCESSO)NR_COTACAO,ARMADOR,NR_PROCESSO,NM_ITEM_DESPESA,PORTO,DT_EMBARQUE,DT_CHEGADA,REGRA,VL_TAXA_COMPRA,VL_TAXA_VENDA,vl_taxa_local_compra,DT_VALIDADE_INICIAL from VW_AJUSTA_TAXA A INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO WHERE B.ID_STATUS_COTACAO <> 12 AND A.ID_TRANSPORTADOR = @ID ">
        <SelectParameters>
            <asp:QueryStringParameter Name="ID" QueryStringField="id" DefaultValue="0" />
        </SelectParameters>

    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
