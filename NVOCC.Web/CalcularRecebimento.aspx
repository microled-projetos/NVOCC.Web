<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CalcularRecebimento.aspx.vb" Inherits="NVOCC.Web.CalcularRecebimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">CALCULAR RECEBIMENTO <asp:Label runat="server" ID="lblMBL" CssClass="control-label" /></h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in" id="Embarque">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <div class="row linhabotao text-center" style="margin-left: 20px; border: ridge 1px;">

                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">FORNECEDOR:</label>
                                            <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="true" DataTextField="NM_RAZAO" DataSourceID="dsFornecedor" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">CIDADE DO PARCEIRO:</label><br />
                                            <asp:Label runat="server" ID="lblCidade" CssClass="control-label" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row linhabotao text-center" style="margin-left: 20px; border: ridge 1px;">

                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">DATA CAMBIO:</label>
                                            <asp:TextBox ID="txtCambio" runat="server" placeholder="__/__/____" CssClass="form-control data"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">DATA DE VENCIMENTO:</label><br />
                                            <asp:TextBox ID="txtVencimento" runat="server" placeholder="__/__/____" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label class="control-label">TIPO FATURAMENTO:</label><br />
                                            <asp:Label ID="lblTipoFaturamento" runat="server" CssClass="control-label" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">DIAS FATURAMENTO:</label><br />
                                            <asp:Label ID="lblDiasFaturamento" runat="server" CssClass="control-label" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label class="control-label">ACORDO:</label><br />
                                            <asp:Label ID="lblAcordo" runat="server" CssClass="control-label" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">

                                            <label class="control-label">SPREAD:</label><br />
                                            <asp:Label runat="server" ID="lblSpread" CssClass="control-label" />
                                        </div>
                                    </div>
                                </div>




                                <br />
                                <br />
                                <div id="divConteudo" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-sm-9">
                                            <div class="table-responsive tableFixHead">
                                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                        <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Fornecedor" SortExpression="NM_PARCEIRO_EMPRESA" />
                                                        <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" />
                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="Valor da compra" SortExpression="VL_TAXA_CALCULADO" />
                                                        <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />

                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">

                                            <div class="table-responsive tableFixHead">
                                                <asp:GridView ID="dgvMoedaFreteArmador" DataKeyNames="ID_MOEDA_FRETE_ARMADOR" DataSourceID="dsMoedaFreteArmador" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com a data de câmbio atual." Visible="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                        <asp:BoundField DataField="VL_TXOFICIAL" HeaderText="Valor" SortExpression="VL_TXOFICIAL" />
                                                        <asp:BoundField DataField="DT_CAMBIO" HeaderText="Data Câmbio" SortExpression="DT_CAMBIO" DataFormatString="{0:dd/MM/yyyy}" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                                <asp:GridView ID="dgvMoedaFrete" DataKeyNames="ID_MOEDA_FRETE" DataSourceID="dsMoedaFrete" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com a data de câmbio atual." Visible="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                        <asp:BoundField DataField="VL_TXOFICIAL" HeaderText="Valor" SortExpression="VL_TXOFICIAL" />
                                                        <asp:BoundField DataField="DT_CAMBIO" HeaderText="Data Câmbio" SortExpression="DT_CAMBIO" DataFormatString="{0:dd/MM/yyyy}" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row" style="border: ridge 1px;">
                                        <div class="col-sm-offset-5 col-sm-2 col-sm-offset-5">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button runat="server" Text="Ok" ID="btnCalcularRecebimento" Enabled="false" CssClass="btn btn-success btn-block" />
                                                <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" CssClass="btn btn-danger btn-block" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxas" />
                                <asp:PostBackTrigger ControlID="ddlFornecedor" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL = @ID_BL OR ID_BL_MASTER = @ID_BL) AND CD_PR = 'R' AND ID_PARCEIRO_EMPRESA = @ID_PARCEIRO_EMPRESA">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
            <asp:ControlParameter Name="ID_PARCEIRO_EMPRESA" Type="Int32" ControlID="ddlFornecedor" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoedaFreteArmador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoedaFrete" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'R' AND ID_BL = @ID_BL or ID_BL IN (SELECT ID_BL FROM TB_BL WHERE ID_BL_MASTER = @ID_BL))
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
