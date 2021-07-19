<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SolicitacaoPagamento.aspx.vb" Inherits="NVOCC.Web.SolicitacaoPagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }
    </style>
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
                                              <asp:Label runat="server" ID="lblID_MBL" CssClass="control-label" Style="display:none" />
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
                                    <div class="row">
                                <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Marcar Todos" ID="btnMarcar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Desmarcar Todos" ID="btnDesmarcar" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-sm-9 table-responsive tableFixHead">
                                        <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server"  AutoPostBack="true" ToolTip="Só é possivel selecionar taxas calculadas"/>
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

                                    <div class="col-sm-3 table-responsive tableFixHead">
                                        
                                        <asp:GridView ID="dgvMoedas" DataKeyNames="ID_MOEDA" DataSourceID="dsMoeda" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com data de câmbio atual." Visible="false">
                                            <Columns>
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorCambio" runat="server" Text='<%# Eval("VL_TXOFICIAL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoedaFrete" runat="server" Text='<%# Eval("ID_MOEDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>

                                        <asp:GridView ID="dgvMoedasArmador" DataKeyNames="ID_MOEDA" DataSourceID="dsMoedaArmador" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com data de câmbio atual." Visible="false">
                                            <Columns>
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorCambio" runat="server" Text='<%# Eval("VL_TXOFICIAL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:BoundField DataField="DT_CAMBIO" HeaderText="Data Câmbio" SortExpression="DT_CAMBIO" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoedaFrete" runat="server" Text='<%# Eval("ID_MOEDA") %>'  />
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

                                <ajaxToolkit:ModalPopupExtender id="mpeMontagem" runat="server" PopupControlID="Panel1" TargetControlID="txtID_BL"  CancelControlID="btnNao"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
                                           <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalMercaoriaNova">MONTAGEM DE PAGAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  
                            <div class="row">
                               <h5>DESEJA PROSSEGUIR PARA MONTAGEM DE PAGAMENTO?</h5>
                             </div>
                           
                      
                                                       
                                                        </div>                     
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-danger" ID="btnNao" text="Não" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSim" text="Sim" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>       
     </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />                                                             <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFornecedor" />
                                <asp:PostBackTrigger ControlID="btnAtualizaValor" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL_MASTER = @ID_BL) AND CD_PR = 'P' AND ID_PARCEIRO_EMPRESA = @ID_EMPRESA ORDER BY NR_PROCESSO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="lblID_MBL" />
                        <asp:ControlParameter Name="ID_EMPRESA" Type="Int32" ControlID="ddlFornecedor" />

        </SelectParameters>
    </asp:SqlDataSource>
   
    <asp:SqlDataSource ID="dsMoedaArmador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT 
A.ID_MOEDA, A.NM_MOEDA ,CASE WHEN(SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE_ARMADOR B WHERE  ID_ARMADOR = @ARMADOR AND A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) IS NULL THEN 0
ELSE (SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE_ARMADOR B WHERE ID_ARMADOR = @ARMADOR AND A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) END VL_TXOFICIAL
FROM TB_MOEDA A
WHERE 
A.ID_MOEDA <> 124  ">   
         <SelectParameters>
                        <asp:ControlParameter Name="ARMADOR" Type="Int32" ControlID="ddlFornecedor" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT 
A.ID_MOEDA, A.NM_MOEDA ,CASE WHEN(SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE B WHERE  A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) IS NULL THEN 0
ELSE (SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) END VL_TXOFICIAL
FROM TB_MOEDA A
WHERE 
A.ID_MOEDA <> 124  ">   
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND VL_TAXA_CALCULADO > 0 AND ID_BL IN ( SELECT ID_BL FROM TB_BL WHERE ID_BL_MASTER = @ID_BL))
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
         <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="lblID_MBL" />
        </SelectParameters>
     </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
