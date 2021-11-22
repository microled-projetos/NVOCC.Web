<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CalcularRecebimento.aspx.vb" Inherits="NVOCC.Web.CalcularRecebimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        #imgFundo {
            display: none;
        }
         th {
    color: #337ab7;
}
          .ImageButton{
              padding-left:25px;
              padding-right:25px;
          }
         </STYLE>
    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">CALCULAR RECEBIMENTO
                    <asp:Label runat="server" ID="lblMBL" CssClass="control-label" /></h3>
            </div>
            <div class="panel-body">                    <asp:Label runat="server" ID="lblID_CONTA_PAGAR_RECEBER" CssClass="control-label" style="display:none" />

                
                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-warning" id="divInfo" runat="server" visible="false">
                                    <asp:Label ID="lblInfo" Text="ATENÇÃO: BL sem tipo de estufagem cadastrada!" runat="server"></asp:Label>
                                </div>
                                <div class="row linhabotao text-center" style="margin-left: 20px; border: ridge 1px;">

                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">PARCEIRO CLIENTE:</label>
                                            <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="true" DataTextField="NM_RAZAO" DataSourceID="dsFornecedor" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">CIDADE DO PARCEIRO:</label><br />
                                            <asp:Label runat="server" ID="lblCidade" CssClass="control-label" />
                                            <asp:Label runat="server" ID="lbl_ISS" CssClass="control-label" Style="display: none" />
                                            <asp:Label runat="server" ID="lbl_PIS" CssClass="control-label" Style="display: none" />
                                            <asp:Label runat="server" ID="lbl_COFINS" CssClass="control-label" Style="display: none" />
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
                                <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Marcar Todos" ID="btnMarcar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Desmarcar Todos" ID="btnDesmarcar" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div> <br />
                                    <div class="row">
                                        <div class="col-sm-9">
                                            <div class="table-responsive tableFixHead">
                                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                        <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Cliente" SortExpression="NM_PARCEIRO_EMPRESA" />
                                                        <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                        <asp:TemplateField HeaderText="ItemDespesa" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemDespesa" runat="server" Text='<%# Eval("ID_ITEM_DESPESA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" />
                                                        <asp:TemplateField HeaderText="Valor" SortExpression="VL_TAXA_CALCULADO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA_CALCULADO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Valor R$" SortExpression="VL_TAXA_BR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValorBR" runat="server" Text='<%# Eval("VL_TAXA_BR") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Calculado" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCalculado" runat="server" Text='<%# Eval("FL_CALCULADO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoeda" runat="server" Text='<%# Eval("ID_MOEDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                        <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_PARCEIRO_ARMAZEM_DESCARGA" runat="server" Text='<%# Eval("ID_PARCEIRO_ARMAZEM_DESCARGA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                      <asp:TemplateField HeaderText="Bloqueio FCA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFL_BLOQUEIO_FCA" runat="server" Text='<%# Eval("FL_BLOQUEIO_FCA") %>'  />
                                                        <asp:ImageButton ID="btnBloquearFCA" runat="server" CssClass="ImageButton" ToolTip="bloquear" Autopostback="true" src="Content/imagens/bloquear.png" CommandName="BloquearFCA" CommandArgument='<%# Eval("ID_BL") %>'  />
                                                        <asp:ImageButton ID="btnDesbloquearFCA" runat="server" CssClass="ImageButton" ToolTip="desbloquear" Autopostback="true" src="Content/imagens/desbloquear.png" CommandName="DesbloquearFCA" CommandArgument='<%# Eval("ID_BL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                                

                                                <asp:TemplateField HeaderText="Bloqueio Financeiro">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblFL_BLOQUEIO_FINANCEIRO" runat="server" Text='<%# Eval("FL_BLOQUEIO_FINANCEIRO") %>' />
                                                 <asp:ImageButton ID="btnBloquearFinanceiro" runat="server" ToolTip="bloquear" CssClass="ImageButton" src="Content/imagens/bloquear.png" CommandName="BloquearFinanceiro" CommandArgument='<%# Eval("ID_BL") %>' />
                                                 <asp:ImageButton ID="btnDesbloquearFinanceiro" runat="server" ToolTip="desbloquear" CssClass="ImageButton" src="Content/imagens/desbloquear.png" CommandName="DesbloquearFinanceiro" CommandArgument='<%# Eval("ID_BL") %>' />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>






                                                <asp:TemplateField HeaderText="Bloqueio Documental">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFL_BLOQUEIO_DOCUMENTAL" runat="server" Text='<%# Eval("FL_BLOQUEIO_DOCUMENTAL") %>'  />
                                                 <asp:ImageButton ID="btnBloquearDocumental" ToolTip="bloquear" runat="server" CssClass="ImageButton" src="Content/imagens/bloquear.png" CommandName="BloquearDocumental" CommandArgument='<%# Eval("ID_BL") %>' />
                                                 <asp:ImageButton ID="btnDesbloquearDocumental" runat="server" ToolTip="desbloquear" CssClass="ImageButton" src="Content/imagens/desbloquear.png" CommandName="DesbloquearDocumental" CommandArgument='<%# Eval("ID_BL") %>' />                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">

                                            <div class="table-responsive tableFixHead">
                                                <asp:GridView ID="dgvMoedaFreteArmador" DataKeyNames="ID_MOEDA" DataSourceID="dsMoedaFreteArmador" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com a data de câmbio atual." Visible="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                          <asp:TemplateField HeaderText="Valor" SortExpression="VL_TXOFICIAL">
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

                                                <asp:GridView ID="dgvMoedaFrete" DataKeyNames="ID_MOEDA" DataSourceID="dsMoedaFrete" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com a data de câmbio atual." Visible="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />

                                                    <asp:TemplateField HeaderText="Valor" SortExpression="VL_TXOFICIAL">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorCambio" runat="server" Text='<%# Eval("VL_TXOFICIAL") %>'  />
                                                    </ItemTemplate>
                                                   </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Valor Abertura" SortExpression="VL_TXABERTURA">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorAbertuda" runat="server" Text='<%# Eval("VL_TXABERTURA") %>'  />
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
                                            </div>
                                             <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Atualizar valor de compra R$" ID="btnAtualizaValor" CssClass="btn btn-warning btn-block" />
                                        </div>
                                        </div>
                                </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                           <div class="row" style="border: ridge 1px; display:block">
                                        <div class="col-sm-offset-5 col-sm-2 col-sm-offset-5">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">VALOR:</label>
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="form-control moeda"></asp:TextBox>
                                            </div>
                                            </div>
                                    </div>
                                    <div class="row" style="border: ridge 1px;">
                                        <div class="col-sm-offset-5 col-sm-2 col-sm-offset-5">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button runat="server" Text="Ok" ID="btnCalcularRecebimento" CssClass="btn btn-success btn-block" />
                                                <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" CssClass="btn btn-danger btn-block" />
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                
                                <ajaxToolkit:ModalPopupExtender id="mpeND" runat="server" PopupControlID="Panel1" TargetControlID="txtID_BL"  CancelControlID="btnNao"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
                                           <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalMercaoriaNova">NOTA DE DÉBITO</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  
                            <div class="row">
                               <h5>DESEJA IMPRIMIR NOTA DE DÉBITO?</h5>
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
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxas" />
                                <asp:PostBackTrigger ControlID="ddlFornecedor" />
                                                                <asp:PostBackTrigger ControlID="btnCalcularRecebimento" />

                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL = @ID_BL OR ID_BL_MASTER = @ID_BL) AND CD_PR = 'R' AND ID_PARCEIRO_EMPRESA = @ID_PARCEIRO_EMPRESA AND ID_DESTINATARIO_COBRANCA <> 3">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
            <asp:ControlParameter Name="ID_PARCEIRO_EMPRESA" Type="Int32" ControlID="ddlFornecedor" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoedaFreteArmador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_MOEDA, A.NM_MOEDA ,CASE WHEN(SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE_ARMADOR B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) IS NULL THEN 0
ELSE (SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE_ARMADOR B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) END VL_TXOFICIAL
FROM TB_MOEDA A
WHERE A.ID_MOEDA <> 124 "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoedaFrete" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT 
A.ID_MOEDA, A.NM_MOEDA ,CASE WHEN(SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) IS NULL THEN 0
ELSE (SELECT B.VL_TXOFICIAL
FROM TB_MOEDA_FRETE B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) END VL_TXOFICIAL,

CASE WHEN(SELECT B.VL_TXABERTURA
FROM TB_MOEDA_FRETE B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) IS NULL THEN 0
ELSE (SELECT B.VL_TXABERTURA
FROM TB_MOEDA_FRETE B WHERE A.ID_MOEDA = B.ID_MOEDA AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103) ) END VL_TXABERTURA
FROM TB_MOEDA A
WHERE 
A.ID_MOEDA <> 124 "></asp:SqlDataSource>

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
