<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SolicitacaoPagamento.aspx.vb" Inherits="NVOCC.Web.SolicitacaoPagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">SOLICITAÇÃO DE PAGAMENTO

                </h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
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
                                <br />

                                <div class="row linhabotao text-center" style="margin-left: 20px">
                                    <div class="col-sm-2" style="border: ridge 1px;">
                                        <div class="form-group" style="margin-bottom: 18px">
                                            <label class="control-label">NÚMERO MASTER:</label><br />
                                            <asp:Label runat="server" ID="lblMBL" CssClass="control-label" />
                                        </div>
                                    </div>
                                    <div>
                                        <div class="col-sm-6" style="border: ridge 1px;">
                                            <div class="form-group">
                                                <label class="control-label">FORNECEDOR:</label>
                                                <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="true" DataTextField="NM_RAZAO" DataSourceID="dsFornecedor" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        
                                    </div>
                                </div>



                                <br />
                                <br />
                                <div class="row" runat="server" id="divgrids" visible="false">
                                    <div class="col-sm-8 table-responsive tableFixHead">
                                        <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" Checked="True" AutoPostBack="true" ToolTip="Só é possivel selecionar taxas calculadas"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Fornecedor" SortExpression="NM_PARCEIRO_EMPRESA" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" />
                                                <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="Valor da compra" SortExpression="VL_TAXA_CALCULADO" />
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA_BR") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoeda" runat="server" Text='<%# Eval("ID_MOEDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Calculado" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCalculado" runat="server" Text='<%# Eval("FL_CALCULADO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-4 table-responsive tableFixHead">
                                        <asp:GridView ID="dgvMoedas" DataKeyNames="ID_MOEDA_FRETE_ARMADOR" DataSourceID="dsMoeda" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com data de câmbio atual.">
                                            <Columns>
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
<%--                                                <asp:BoundField DataField="VL_TXOFICIAL" HeaderText="Valor" SortExpression="VL_TXOFICIAL" />--%>
                                                <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="VL_TXOFICIAL" runat="server" Text='<%# Eval("VL_TXOFICIAL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DT_CAMBIO" HeaderText="Data Câmbio" SortExpression="DT_CAMBIO" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoeda" runat="server" Text='<%# Eval("ID_MOEDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                
                                <div class="row">
                                    <div class="col-sm-offset-10 col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Atualizar valor de compra R$" ID="btnAtualizaValor" CssClass="btn btn-warning btn-block" />
                                        </div>
                                        </div>
                                </div>

                                </div>
                                <div class="row" style="border: ridge 1px;">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">VALOR DE PAGAMENTO:</label><br />
                                            <asp:Label runat="server" ID="lblTotal" Text="0" CssClass="control-label" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">VENCIMENTO:</label>
                                                <asp:TextBox ID="txtVencimento" runat="server" placeholder="__/__/____" CssClass="form-control data" Width="210px"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Solicitar Pagamento" ID="btnSolicitar" CssClass="btn btn-success" />
                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />                                                             <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFornecedor" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL = @ID_BL) AND CD_PR = 'P' AND ID_PARCEIRO_EMPRESA = @ID_EMPRESA ORDER BY TIPO,NR_PROCESSO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
                        <asp:ControlParameter Name="ID_EMPRESA" Type="Int32" ControlID="ddlFornecedor" />

        </SelectParameters>
    </asp:SqlDataSource>
   
    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE DT_CAMBIO = CONVERT(DATE,GETDATE(),103) AND ID_MOEDA <> 124"
        UpdateCommand="UPDATE [TB_MOEDA_FRETE_ARMADOR] SET [DT_CAMBIO] = @DT_CAMBIO , VL_TXOFICIAL = @VL_TXOFICIAL WHERE ID_MOEDA_FRETE_ARMADOR = @ID_MOEDA_FRETE_ARMADOR;">
           <UpdateParameters> 
            <asp:Parameter Name="DT_CAMBIO" Type="DateTime" />
            <asp:Parameter Name="VL_TXOFICIAL" Type="Decimal" />
            <asp:Parameter Name="ID_MOEDA_FRETE_ARMADOR" Type="Int32" />
            </UpdateParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND ID_BL = @ID_BL  AND VL_TAXA_CALCULADO>0)
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
         <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
        </SelectParameters>
     </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
