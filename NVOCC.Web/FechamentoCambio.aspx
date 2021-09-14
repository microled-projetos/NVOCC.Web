<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FechamentoCambio.aspx.vb" Inherits="NVOCC.Web.FechamentoCambio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnn {
            background-color: #d5d8db;
            margin: 5px;
            font-size: 13px
        }

        .selected1 {
            color: black;
            font-family: verdana;
            font-size: 8pt;
            background-color: #e6c3a5;
        }

       /* .none {
            display: none
        }

        th {
            position: sticky !important;
            top: 0;
            background-color: #e6eefa;
        }

        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }*/
    </style>
     <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FECHAMENTO DE CÂMBIO
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>

                                Filtro:
                   <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">

                       
                      
                       
                       <div class="col-sm-2">
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                   <asp:ListItem Value="1">Número do Contrato</asp:ListItem>
                                   <asp:ListItem Value="2">Data de Fechamento</asp:ListItem>
                                   <asp:ListItem Value="3">Agente</asp:ListItem>
                               </asp:DropDownList>
                           </div>

                       </div>
                       <div class="col-sm-2">
                           <div class="form-group">
                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>
                        <div class="col-sm-1">

                           <div class="form-group">

                                  <asp:RadioButtonList ID="rdStatus" runat="server" Style="padding: 0px; font-size: 12px; text-align: justify">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;Em aberto</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;Liquidados</asp:ListItem>
                                            </asp:RadioButtonList>
                           </div>
                       </div>
                      
                       <div class="col-sm-1">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>
                       <div class="col-sm-offset-2 col-sm-4">
                           <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Fechamento:</asp:Label><br />
                            <div style="border: ridge 1px;">
                            <asp:LinkButton ID="lkNovoFechamento" runat="server" CssClass="btn btnn btn-default" Style="font-size: 15px">Novo</asp:LinkButton>
                                 <asp:LinkButton ID="lkBaixarFechamento" runat="server" CssClass="btn btnn btn-default" Style="font-size: 15px">Baixar</asp:LinkButton>
                                 <asp:LinkButton ID="lkExcluirFechamento" runat="server" CssClass="btn btnn btn-default" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');">Excluir</asp:LinkButton>
                                 <asp:LinkButton ID="lkCancelarFechamento" runat="server" CssClass="btn btnn btn-default" Style="font-size: 15px">Cancelar</asp:LinkButton>

                               <%-- </div>
                           <asp:Label ID="Label1" Style="padding-left: 35px" runat="server">Relatório:</asp:Label><br />
                                <div style="border: ridge 1px;">--%>
                           <asp:LinkButton ID="lkContratosFirmados" runat="server" CssClass="btn btnn btn-default" Style="font-size: 15px">Contratos Firmados</asp:LinkButton> </div>
                       </div>
                   </div>
                                <br />                                                                                           <asp:TextBox ID="TextBox4" Style="display:none" runat="server"></asp:TextBox>

                                                                                           <asp:TextBox ID="TextBox3" Style="display:none" runat="server"></asp:TextBox>
                                                  
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlContratosFirmados" TargetControlID="lkContratosFirmados" CancelControlID="btnFecharContratos"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlContratosFirmados" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Relação dos Contratos Firmados</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                       <div class="col-sm-6">
                                    <div class="form-group">
                                           <label class="control-label">Fechamento Inicial:</label>
                                                                       <asp:TextBox ID="txtFechamentoInicial" runat="server" CssClass="form-control data"></asp:TextBox>

                                    </div>
                                           </div>
                                       <div class="col-sm-6">
                                    <div class="form-group">
                                           <label class="control-label">Fechamento Final:</label>
                                                 <asp:TextBox ID="txtFechamentoFinal" runat="server" CssClass="form-control data"></asp:TextBox>

                                    </div>
                                        </div>
                                         </div>

                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharContratos" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnPesquisarContratos" text="Imprimir" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                 <asp:Button runat="server" CssClass="btn btn-success" ID="Button1" Style="display: none;" />

                              <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlNovoFechamento" TargetControlID="lkNovoFechamento" CancelControlID="Button1"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlNovoFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">NOVO FECHAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body"> 
                                                             <div class="alert alert-danger" id="divErroNovoFechamento" runat="server" visible="false">
                                    <asp:Label ID="lblErroNovoFechamento" runat="server"></asp:Label>
                                </div>
                                                            <div class="row">
                                                                
                       <div class="col-sm-2">
                           <div class="form-group">
                               <label class="control-label">Venc. Inicial:</label><label runat="server" style="color: red">*</label>
                               <asp:TextBox ID="txtVencimentoInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                       </div>
                       <div class="col-sm-2">
                           <div class="form-group">
                               <label class="control-label">Venc. Final:</label><label runat="server" style="color: red">*</label>
                               <asp:TextBox ID="txtVencimentoFinal"  runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                       </div>

                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlAgenteNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO"></asp:DropDownList>

                                    </div>
                                        </div>
                                      
                                         </div>
                                    <div class="row">
                                      <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlMoedaNovo" runat="server" AutoPostBack="True" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList> 


                                    </div>
                                        </div>
                                       <div class="col-sm-10">
                                    <div class="form-group">
                                          <label class="control-label">Instituição Financeira:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlCorretorNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsCorretor" DataValueField="ID_PARCEIRO"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        </div>   
                                                                        <div class="row">
                                        <div class="col-sm-6">
                                    <div class="form-group">
                                           <label class="control-label">Número do Contrato:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtContratoNovo" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Fechamento:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtDataFechamentoNovo" runat="server"  CssClass="form-control data"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Tarifa Cobrada:</label><label runat="server" style="color: red">*</label>
                                          <asp:TextBox ID="txtTarifaNovo" runat="server"  CssClass="form-control moeda"></asp:TextBox>



                                    </div>
                                        </div>
                                         
                                     
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">IOF:</label><label runat="server" style="color: red">*</label>
                                          <asp:TextBox ID="txtIOFNovo" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-3">
                                    <div class="form-group">
                                           <label class="control-label">Valor:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtValorNovo" runat="server"  CssClass="form-control dinheiro"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Data Câmbio:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtDataCambioNovo" runat="server"  CssClass="form-control data"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-3">
                                    <div class="form-group">
                                            <label class="control-label">Taxa Câmbio</label><label runat="server" style="color: red">*</label>

                                              <asp:TextBox ID="txtCambioNovo" runat="server"  CssClass="form-control txs"></asp:TextBox>

                                    </div>
                                        </div>
                                    
                                     
                                      
                                        <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Valor BR:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtValorBRNovo" runat="server"  CssClass="form-control dinheiro"></asp:TextBox>


                                    </div>
                                        </div>
                               
                                         </div>   
                                                            
                                     
                                      
                                       <div class="table-responsive tableFixHead">
                                            <asp:GridView ID="dgvInvoice" DataKeyNames="ID_ACCOUNT_INVOICE" DataSourceID="dsInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 100px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false" >
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbSelecionar" Checked="True" runat="server" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_ACCOUNT_INVOICE") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:BoundField DataField="NR_INVOICE" HeaderText="Nº INVOICE" SortExpression="NR_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_INVOICE" HeaderText="TIPO" SortExpression="NM_ACCOUNT_TIPO_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                    <asp:BoundField DataField="DT_INVOICE" HeaderText="DATA INVOICE" SortExpression="DT_INVOICE" />
                                                    <asp:TemplateField HeaderText="VALOR" SortExpression="VALOR_TOTAL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VALOR_TOTAL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="headerStyle" />
                                            </asp:GridView>
                                           </div>
                                                               
                                                            <div class="row" runat="server" Visible="false"  id="divTotalInvoices"> 
                                                                <div  style="float: right;margin-right:10px">
                                                                    TOTAL DAS INVOICES SELECIONADAS:<asp:label ID="lblValorTotalInvoices" runat="server"/>
                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNovoFechamento" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarFechamento" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlBaixaFechamento" TargetControlID="TextBox4" CancelControlID="btnFecharBaixa"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBaixaFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">LIQUIDAÇÃO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row" style="border: ridge 1px;">
                                      
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label>  <br/>
                                                                                    <asp:Label ID="lblAgenteBaixa" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>  <label class="control-label">Valor<asp:Label ID="lblMoedaEstrangeiroBaixa" runat="server"/></strong>
                                              <br/>
                                                                                   <asp:Label ID="lblValorEstrangeiroBaixa" runat="server"></asp:Label>



                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Valor BR:</label>                       <br/>               <asp:Label ID="lblValorRealBaixa" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row" style="border: ridge 1px;">
                                     
                                       
                                        <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Instituição Financeira:</label>  <br/>
                                                                            <asp:Label ID="lblInstituicaoBaixa" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                        <div class="col-sm-4">
                                    <div class="form-group">
                                          <label class="control-label">Contrato:</label>  <br/>
                                    <asp:Label ID="lblContratoBaixa" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                      
                                         </div>   
                                    <div class="row">
                                     
                                       <div class="col-sm-offset-5 col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Data Liquidação:</label><label runat="server" style="color: red">*</label><asp:TextBox ID="txtDataLiquidacaoBaixa" runat="server"  CssClass="form-control data"></asp:TextBox>
                                                                                                
                                </div>  
                           </div>
                      </div>          <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                        <small style="color:gray">*Data da Liquidação refere-se à data do débito da Instituição Financeira</small>     
                                </div>  
                           </div>
                      </div>
                                                            </div>
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBaixa" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarBaixa" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>
                                                                 <asp:Button runat="server" CssClass="btn btn-success" ID="Button2" Style="display: none;" />

                               <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="pnlCancelFechamento" TargetControlID="TextBox3" CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlCancelFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CANCELAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row" style="border: ridge 1px;">
                                      
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><br />
                                                                                    <asp:Label ID="lblAgenteCancel" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                         <strong> <label class="control-label">Valor <asp:Label ID="lblMoedaEstrangeiroCancel" runat="server"></asp:Label>  </strong>

                                                                                   <br /> <asp:Label ID="lblValorEstrangeiroCancel" runat="server"></asp:Label>



                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Valor BR:</label>                     <br />              <asp:Label ID="lblValorRealCancel" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row" style="border: ridge 1px;">
                                     
                                       
                                        <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Instituição Financeira:</label><br />
                                                                            <asp:Label ID="lblInstituicaoCancel" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                        <div class="col-sm-4">
                                    <div class="form-group">
                                          <label class="control-label">Contrato:</label><br />
                                    <asp:Label ID="lblContratoCancel" runat="server"></asp:Label>


                                    </div>
                                        </div>
                                      
                                         </div>   
                         <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">Motivo Cancelamento:</label>
                                        <asp:TextBox ID="txtMotivoCancel" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                             
                                </div>  
                           </div>
                      </div>
                                                            </div>
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechaCancel" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvaCancel" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                            </ContentTemplate>       
                            <Triggers> 
                                                            <asp:PostBackTrigger ControlID="btnSalvaCancel" />
                                </Triggers> 
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                        <asp:GridView ID="dgvFechamento" DataKeyNames="ID_ACCOUNT_FECHAMENTO" DataSourceID="dsFechamento" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                            <Columns>
                                                                                                <asp:BoundField DataField="ID_ACCOUNT_FECHAMENTO" HeaderText="ID" SortExpression="ID_ACCOUNT_FECHAMENTO" />

                                                <asp:BoundField DataField="NR_CONTRATO" HeaderText="Nº CONTRATO" SortExpression="NR_CONTRATO" />
                                                <asp:BoundField DataField="NM_CORRETOR" HeaderText="INST. FINANCEIRA" SortExpression="NM_CORRETOR" />
                                                <asp:BoundField DataField="NM_AGENTE" HeaderText="AGENTE" SortExpression="NM_AGENTE" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                             <asp:BoundField DataField="VL_CONTRATO" HeaderText="VALOR" SortExpression="VL_CONTRATO" />             
                                                <asp:BoundField DataField="DT_FECHAMENTO" HeaderText="DATA FECHAMENTO" SortExpression="DT_FECHAMENTO" />
                                                <asp:BoundField DataField="DT_TAXA_CAMBIO" HeaderText="DATA CAMBIO" SortExpression="DT_TAXA_CAMBIO" />
                                                <asp:BoundField DataField="VL_TAXA_CAMBIO" HeaderText="VALOR CAMBIO" SortExpression="VL_TAXA_CAMBIO" />
                                                <asp:BoundField DataField="VL_CONTRATO_BR" HeaderText="VALOR R$" SortExpression="VL_CONTRATO_BR" />
                                                                                                <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="DATA LIQUIDACAO" SortExpression="DT_LIQUIDACAO" />
                                                                                                <asp:BoundField DataField="DT_CANCELAMENTO" HeaderText="DATA CANCELAMENTO" SortExpression="DT_CANCELAMENTO" />

                                                <asp:BoundField DataField="DS_MOTIVO_CANCELAMENTO" HeaderText="MOTIVO CANCELAMENTO" SortExpression="DS_MOTIVO_CANCELAMENTO" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_ACCOUNT_FECHAMENTO") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>

                                </div>
                                                          <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>

                            </ContentTemplate>       <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnFecharNovoFechamento" />

                                <asp:AsyncPostBackTrigger ControlID="btnPesquisarContratos" />
                                
                             <asp:AsyncPostBackTrigger ControlID="btnSalvarFechamento" />
                                                            <asp:PostBackTrigger ControlID="btnPesquisa" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMoedaNovo" />
                                                            <asp:AsyncPostBackTrigger ControlID="lkNovoFechamento" />
                                   </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
         <asp:SqlDataSource ID="dsFechamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT distinct ID_ACCOUNT_FECHAMENTO,NR_CONTRATO,NM_CORRETOR,NM_AGENTE,SIGLA_MOEDA,VL_CONTRATO,VL_CONTRATO_BR,CONVERT(VARCHAR,DT_FECHAMENTO,103)DT_FECHAMENTO,CONVERT(VARCHAR,DT_TAXA_CAMBIO,103)DT_TAXA_CAMBIO,VL_TAXA_CAMBIO,CONVERT(VARCHAR,DT_LIQUIDACAO,103)DT_LIQUIDACAO,CONVERT(VARCHAR,DT_CANCELAMENTO,103)DT_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO FROM [dbo].[View_Fechamento]">
</asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT 0, ' Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO"></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] WHERE ID_MOEDA <> 124 union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsCorretor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_CORRETORA = 1
union SELECT ID_CONTA_BANCARIA,NM_CONTA_BANCARIA FROM TB_CONTA_BANCARIA 
union SELECT 0, ' Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_ACCOUNT_INVOICE, F.NM_ACCOUNT_TIPO_INVOICE, G.NM_ACCOUNT_TIPO_EMISSOR, A.NR_INVOICE, A.DT_INVOICE, (SELECT SUM(ISNULL(VL_TAXA,0))FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE=B.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM FN_ACCOUNT_INVOICE('@DATAINICIAL','@DATAFINAL') A LEFT JOIN TB_ACCOUNT_TIPO_INVOICE F ON A.ID_ACCOUNT_TIPO_INVOICE=F.ID_ACCOUNT_TIPO_INVOICE LEFT JOIN TB_ACCOUNT_TIPO_EMISSOR G ON A.ID_ACCOUNT_TIPO_EMISSOR=G.ID_ACCOUNT_TIPO_EMISSOR WHERE (A.DT_FECHAMENTO IS NULL OR A.DT_FECHAMENTO IS NOT NULL AND DT_CANCELAMENTO_FECHAMENTO IS NOT NULL) AND ID_MOEDA = @ID_MOEDA AND ID_PARCEIRO_AGENTE = @ID_AGENTE AND VL_TAXA <> 0 group by A.ID_ACCOUNT_INVOICE, F.NM_ACCOUNT_TIPO_INVOICE, 
 G.NM_ACCOUNT_TIPO_EMISSOR, A.NR_INVOICE, A.DT_INVOICE,DT_CANCELAMENTO_FECHAMENTO,DT_FECHAMENTO">
        <SelectParameters>
            <asp:ControlParameter Name="DATAINICIAL" Type="string" ControlID="txtVencimentoInicial"  DefaultValue=""/>
            <asp:ControlParameter Name="DATAFINAL" Type="string" ControlID="txtVencimentoFinal" DefaultValue=""/>
            <asp:ControlParameter Name="ID_MOEDA" Type="Int32" ControlID="ddlAgenteNovo" />
            <asp:ControlParameter Name="ID_AGENTE" Type="Int32" ControlID="ddlMoedaNovo" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
  <script type="text/javascript" src="Content/js/jquery.maskMoney.js" ></script>
   <script>
        $(document).ready(function(){
            $(".dinheiro").maskMoney({ showSymbol: false, decimal: ",", thousands: ".", allowNegative: true});
        });

        jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();


       function ContratosFirmados() {

           window.open('ContratosFirmados.aspx', '_blank');
       };


   </script>


</asp:Content>
