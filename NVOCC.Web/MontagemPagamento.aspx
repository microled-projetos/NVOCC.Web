<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MontagemPagamento.aspx.vb" Inherits="NVOCC.Web.MontagemPagamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }
    </style>
     <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">MONTAGEM DE PAGAMENTO

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

                                <div class="row linhabotao text-center" style="margin-left: 20px;border: ridge 1px;">
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">VENCIMENTO:</label>
                                                <asp:TextBox ID="txtVencimentoBusca" placeholder="__/__/____" runat="server" autopostback="true" CssClass="form-control data" ></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">FORNECEDOR:</label>
                                                <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsFornecedor" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">Nº MASTER:</label>
                                                <asp:TextBox ID="txtMaster" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">Nº HOUSE:</label>
                                                <asp:TextBox ID="txtHouse" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">Nº PROCESSO:</label>
                                                <asp:TextBox ID="txtProcesso" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                        
                                    </div>
                                </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="txtVencimentoBusca" /> 
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>

                                <br />
                                <br />
                                <div id="divgrid" visible="false" runat="server">
                                <div class="row" >
                                    <div class="table-responsive tableFixHead">
                                        <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL,ID_BL_TAXA" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                                <asp:BoundField DataField="NR_BL_MASTER" HeaderText="MBL" SortExpression="NR_BL_MASTER" />

                                                <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Fornecedor/Cliente" SortExpression="NM_PARCEIRO_EMPRESA" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" />
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA_BR") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                </div>
                                <div class="row">
                                <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Marcar Todos" ID="btnMarcar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Desmarcar Todos" ID="btnDesmarcar" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="border: ridge 1px;">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">NÚMERO DA FATURA:</label><br />
                                           <asp:TextBox ID="txtNumeroFatura" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">DATA DA FATURA:</label>
                                                <asp:TextBox ID="txtDataFatura" runat="server" placeholder="__/__/____" CssClass="form-control data" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">DATA DE VENCIMENTO:</label>
                                                <asp:TextBox ID="txtVencimento" runat="server" placeholder="__/__/____" CssClass="form-control data" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">CONTA BANCARIA:</label>
                                                 <asp:DropDownList ID="ddlContaBancaria" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTA_BANCARIA" DataSourceID="dsContaBancaria" DataValueField="ID_CONTA_BANCARIA"></asp:DropDownList>
                                            </div>
                                     </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">VALOR:</label>
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                    </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left"></label>
                                                <asp:Checkbox ID="ckbBaixaAutomatica" runat="server" CssClass="form-control" Checked="true" text="&nbsp;&nbsp;Baixar Automaticamente"></asp:Checkbox>
                                            </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Montar Pagamento" ID="btnMontar" CssClass="btn btn-success" />
                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" CssClass="btn btn-danger" />
                                        </div>
                                    </div>
                                </div>
                                </div>
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" /> 
                                <asp:AsyncPostBackTrigger ControlID="btnMontar" />     

                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE CD_PR= 'P' AND ID_PARCEIRO_EMPRESA = @ID_PARCEIRO_EMPRESA AND DT_SOLICITACAO_PAGAMENTO = CONVERT(DATE, @DATA, 103) ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_EMPRESA" Type="Int32" ControlID="ddlFornecedor" />
            <asp:ControlParameter Name="DATA" Type="string" ControlID="txtVencimentoBusca" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND DT_SOLICITACAO_PAGAMENTO IS NOT NULL) )
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">        
         <SelectParameters>
            <asp:ControlParameter Name="DATA" Type="string" ControlID="txtVencimentoBusca" />
                     </SelectParameters>

     </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsContaBancaria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CONTA_BANCARIA, NM_CONTA_BANCARIA FROM [dbo].[TB_CONTA_BANCARIA] WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_CONTA_BANCARIA] ORDER BY ID_CONTA_BANCARIA">        
     </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
