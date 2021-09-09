<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Faturamento.aspx.vb" Inherits="NVOCC.Web.Faturamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }

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
    </style>
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FATURAMENTO
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>


                                Filtro:
                   <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">

                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                   <asp:ListItem Value="1">Data Vencimento</asp:ListItem>
                                   <asp:ListItem Value="2">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                   <asp:ListItem Value="4">Referência  do Cliente</asp:ListItem>
                                   <asp:ListItem Value="5">Nº Nota Débito</asp:ListItem>
                                   <asp:ListItem Value="6">Nº RPS</asp:ListItem>
                                   <asp:ListItem Value="7">Nº Nota Fiscal</asp:ListItem>
                                   <asp:ListItem Value="8">Nº Recibo</asp:ListItem>
                                   <asp:ListItem Value="9">Data Liquidação</asp:ListItem>
                               </asp:DropDownList>
                           </div>

                       </div>
                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>
                       <div class="col-sm-1">

                           <div class="form-group">

                               <asp:CheckBoxList ID="ckStatus" Style="padding: 0px; font-size: 12px; text-align: justify" runat="server" RepeatDirection="vertical">
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Não liquidados</asp:ListItem>
                                   <asp:ListItem Value="2" Selected="True">&nbsp;Liquidados desde:</asp:ListItem>
                                   <asp:ListItem Value="3">&nbsp;Cancelados</asp:ListItem>
                               </asp:CheckBoxList>
                           </div>
                       </div>
                        <div class="col-sm-1" style="padding-top: 27px;">
                           <div class="form-group">
                               <asp:TextBox ID="txtDataCheckLiquidados" Style="font-size: 12px;" runat="server" CssClass="data"></asp:TextBox>

                           </div>
                       </div>
                       <div class="col-sm-1" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                           </div>
                       </div>

                       <div class="col-sm-5">
                           <asp:LinkButton ID="lkFatura" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Fatura</asp:LinkButton>
                           <asp:LinkButton ID="lkDesmosntrativos" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Demonstrativos</asp:LinkButton>
                           <asp:LinkButton ID="lkRPS" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">RPS</asp:LinkButton>              
                           <asp:LinkButton ID="lkNotasFiscais" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Notas Ficais</asp:LinkButton>
                           <asp:LinkButton ID="lkOpcoesBoletos" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Boletos</asp:LinkButton>

                       </div>
                   </div>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtCNPJFCA" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtCOD_VER_NFSE" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtNR_NOTA" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblContador" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                    <asp:GridView ID="dgvFaturamento" DataKeyNames="ID_FATURAMENTO" DataSourceID="dsFaturamento" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_FATURAMENTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckSelecionar" runat="server" AutoPostBack="true"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                            <asp:BoundField DataField="NM_CLIENTE" HeaderText="Cliente" SortExpression="NM_CLIENTE" />
                                            <asp:BoundField DataField="REFERENCIA_CLIENTE" HeaderText="Ref. Cliente" SortExpression="REFERENCIA_CLIENTE" />
                                            <asp:BoundField DataField="VL_NOTA_DEBITO" HeaderText="Valor Nota de Deb." SortExpression="VL_NOTA_DEBITO" />
                                            <asp:BoundField DataField="NR_NOTA_DEBITO" HeaderText="Nota de Deb." SortExpression="NR_NOTA_DEBITO" />
                                            <asp:BoundField DataField="DT_NOTA_DEBITO" HeaderText="Data de Nota de Deb." SortExpression="DT_NOTA_DEBITO" />
                                            <asp:BoundField DataField="NR_RPS" HeaderText="RPS" SortExpression="NR_RPS" />
                                            <asp:BoundField DataField="DT_RPS" HeaderText="Data RPS" SortExpression="DT_RPS" />
                                            <asp:BoundField DataField="NR_RECIBO" HeaderText="Recibo" SortExpression="NR_RECIBO" />
                                            <asp:BoundField DataField="DT_RECIBO" HeaderText="Data Recibo" SortExpression="DT_RECIBO" />
                                            <asp:BoundField DataField="NR_NOTA_FISCAL" HeaderText="Nota Fiscal" SortExpression="NR_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="Data Nota Fiscal" SortExpression="DT_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="Data de Liquidação" SortExpression="DT_LIQUIDACAO" />
                                            <asp:BoundField DataField="DT_CANCELAMENTO" HeaderText="Data de Cancelamento" SortExpression="DT_CANCELAMENTO" />

                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_FATURAMENTO") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender11" runat="server" PopupControlID="pnlOpcoesBoletos" TargetControlID="lkOpcoesBoletos" CancelControlID="btnFecharOpcoesBoletos" OkControlID="lkBoleto"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlOpcoesBoletos" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">OPÇÕES DE BOLETO</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
                           <asp:LinkButton ID="lkBoleto" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Imprimir Boleto</asp:LinkButton>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                                                                    <asp:LinkButton ID="lkBoletoRemessa" href="RemessaBoletos.aspx" runat="server" CssClass="btn btnn btn-default btn-sm  btn-block" Style="font-size: 15px">Enviar Remessa</asp:LinkButton>
                           
                                        </div>
                                         </div>
                                   </div>                                         
                                </div>  
                             </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharOpcoesBoletos" text="Close" />
                                                        </div>                                                    
                                                </div>
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" PopupControlID="pnlFatura" TargetControlID="lkFatura" CancelControlID="btnFecharFatura" OkControlID="lkBaixarFatura"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlFatura" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">OPÇÕES DE FATURA</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
                           <asp:LinkButton ID="lkBaixarFatura" runat="server" Visible="false" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Baixar Fatura</asp:LinkButton>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                                                                    <asp:LinkButton ID="lkCancelamento" runat="server" CssClass="btn btnn btn-default btn-sm  btn-block" Style="font-size: 15px">Cancelar Fatura</asp:LinkButton>
                           
                                        </div>
                                         </div>
                                   </div>                                         
                                </div>  
                             </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharFatura" text="Close" />
                                                        </div>                                                    
                                                </div>
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender8" runat="server" PopupControlID="pnlRPS" TargetControlID="lkRPS" CancelControlID="btnFecharRPS"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlRPS" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">OPÇÕES DE RPS</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
                            <asp:LinkButton ID="lkGerarRPS" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Gerar RPS</asp:LinkButton>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                                                                      <asp:LinkButton ID="lkReenviarRPS" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Reenviar RPS</asp:LinkButton>                           
                                        </div>
                                         </div>
                                   </div>                                         
                                </div>  
                             </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRPS" text="Close" />
                                                        </div>                                                    
                                                </div>
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlBaixarFatura" TargetControlID="lkBaixarFatura" CancelControlID="btnFecharCancelamento"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBaixarFatura" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BAIXAR FATURA</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       

                                                            <div class="row">
                                <div class="col-sm-offset-5 col-sm-2 col-sm-offset-5">
                                     <div class="form-group">
                                        <label class="control-label">Data:</label></label><label runat="server" style="color:red" >*</label>
                               <asp:TextBox ID="txtData" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                       </div> 
                                  <div class="form-group">
                                                                        <h5>  
                                           <asp:label runat="server" ID="lblProcessoBaixa"/><br/>                                          
                                           <asp:label runat="server" ID="lblClienteBaixa"/></h5>
                                         </div>                          
                                                                                        <div class="row">                        
                                           <h5>CONFIRMAÇÃO DA LIQUIDAÇÃO</h5>                           
                                </div>
                             </div>                         
                               <div class="modal-footer">
                                                                  <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixarFatura" CssClass="btn btn-success" OnClientClick="javascript:return confirm('Deseja realmente baixar esta fatura?');" />

                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBaixa" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlCancelamento" TargetControlID="lkCancelamento" CancelControlID="btnFecharCancelamento"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlCancelamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">OBSERVAÇÃO DE CANCELAMENTO <label runat="server" style="color:red" >*</label></h5>
                                                        </div>
                                                        <div class="modal-body">    
                                    <div class="alert alert-warning" id="divInfo" runat="server" visible="false">
                                    <asp:Label ID="lblmsgInfo" runat="server"></asp:Label>
                                </div>
                                            <h5>                       
                                            <asp:label runat="server" ID="lblProcessoCancelamento" /><br/>      
                                            <asp:label runat="server" ID="lblClienteCancelamento" /></h5>
                    
                                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                        
                                         </div>
                                  
                           
                      
                                                       
                                                                        
                               <div class="modal-footer">
<%--                                                   OnClientClick="javascript:return confirm('Deseja realmente cancelar esta fatura?');"         --%>
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarCancelamento" text="Salvar"/>
                                   <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCancelamento" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlDesmosntrativos" TargetControlID="lkDesmosntrativos" CancelControlID="btnFecharDesmosntrativos"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlDesmosntrativos" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">DEMONSTRATIVOS</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
<asp:LinkButton ID="lkNotaDebito" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px"  href="#" OnClientClick="ImprimirND()" >Nota de Débito</asp:LinkButton>
                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                                                                <asp:LinkButton ID="lkReciboServico" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Recibo Provisório de Serviço</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                                                                     <asp:LinkButton ID="lkReciboPagamento" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Recibo de Pagamento</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharDesmosntrativos" text="Close" />
                                                        </div>                                                    
                                                </div>
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlNotasFiscais" TargetControlID="lkNotasFiscais" CancelControlID="btnFecharNotas"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlNotasFiscais" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">NOTAS FISCAIS</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                           
<asp:LinkButton ID="lkCancelarNota" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" OnClientClick="return confirm( getProcesso() + '\n'+ getCliente() + '\n\n\n\CONFIRMAR CANCELAMENTO DA NOTA FISCAL');" >Cancelar Nota</asp:LinkButton>
                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                                                                <asp:LinkButton ID="lkSubstituirNota" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Substituir Nota</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                                                                     <asp:LinkButton ID="lkVisualizarNota" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Visualizar Nota</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                                                                     <asp:LinkButton ID="lkConsultaNotas" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Consulta de Notas</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNotas" text="Close" />
                                                        </div>                                                   
                                                </div>      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="pnlSubstituirNota" TargetControlID="TextBox2" CancelControlID="btnFecharSubstituicao"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlSubstituirNota" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">SUBSTITUIR NOTA</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
<div class="alert alert-danger" id="divErroSubstituir" runat="server" visible="false">
                                    <asp:Label ID="lblErroSubstituir" runat="server"></asp:Label>
                                </div>
                                                           
                                  <div class="form-group">
                                                                        <h5>  
                                           <asp:label runat="server" ID="lblProcessoSubs"/><br/>                                          
                                           <asp:label runat="server" ID="lblClienteSubs"/></h5>
                                         </div>      
                                                            <div class="row">
                                <div class="col-sm-offset-4 col-sm-2">
                                     <div class="form-group">
                                        <label class="control-label">Número da Nota:</label><br />
                                   <asp:label runat="server" ID="lblNumeroNota"/>
                           </div>
                                     </div>
                                                                <div class="col-sm-2">
                                     <div class="form-group">
                                        <label class="control-label">Data de Emissão:</label><br />
                                   <asp:label runat="server" ID="lblDataEmissao"/>
                           </div>
                                     </div>
                       </div>                      
                                                            <div class="row">
                                <div class="col-sm-offset-4 col-sm-2">
                                     <div class="form-group">
                                        <label class="control-label">Novo Número:</label></label><label runat="server" style="color:red" >*</label>
                               <asp:TextBox ID="txtNovoNumeroNota" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                                     </div>
                                                                <div class="col-sm-2">
                                     <div class="form-group">
                                        <label class="control-label">Nova Data:</label></label><label runat="server" style="color:red" >*</label>
                               <asp:TextBox ID="txtNovaEmissaoNota" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                       </div>                      
                                                                                        
                             </div>                         
                               <div class="modal-footer">
                                                                  <asp:Button runat="server" Text="Substituir" ID="btnSubstituir" CssClass="btn btn-success" OnClientClick="javascript:return confirm('Deseja realmente substituir esta nota?');" />

                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharSubstituicao" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="pnlBanco" TargetControlID="lkBoleto" CancelControlID="btnFecharBoleto"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBanco" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BOLETO</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                   
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="control-label">BANCO:</label><label runat="server" style="color:red" >*</label>
                                                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTA_BANCARIA" DataSourceID="dsBanco" DataValueField="ID_CONTA_BANCARIA">                                             
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                         </div></div>
                                  
                           
                      
                                                       
                                                                        
                               <div class="modal-footer">
<%--                                                   OnClientClick="javascript:return confirm('Deseja realmente cancelar esta fatura?');"         --%>
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnImprimirBoleto" text="Imprimir"/>
                                   <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBoleto" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>



                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender9" runat="server" PopupControlID="pnlConsultaNota" TargetControlID="lkConsultaNotas" CancelControlID="btnFecharConsulta"></ajaxToolkit:ModalPopupExtender>
                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:Panel ID="pnlConsultaNota" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CONSULTA DE NOTAS FISCAIS</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
       <div class="alert alert-danger" id="divErroConsultasNotas" runat="server" visible="false">
                                    <asp:Label ID="lblErroConsultasNotas" runat="server"></asp:Label>
                                </div>
                  <div class="row">
                                        <div class="col-sm-6">
                                    <div class="linha-colorida">Número Nota Fiscal</div>
                                     <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaNotaInicio" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
   </div>
                                    <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaNotaFim" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                                    </div>
                                    </div>
                      <div class="col-sm-6">
                                    <div class="linha-colorida">Número RPS</div>
                                     <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaRPSInicio" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                                         </div>
                                     <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaRPSFim" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                                     </div>
                                                                

                                </div>
                       </div>  
                                                            <div class="row">
                                                             <div class="col-sm-6">      <div class="linha-colorida">Vencimento</div>
                                <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaVencimentoInicio" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                                                                <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaVencimentoFim" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                                                                 </div>
                                                                 <div class="col-sm-6">  
                      <div class="linha-colorida">Liquidação</div>
                                <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaPagamentoInicio" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                              <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaPagamentoFim" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                       </div>

                                                          </div>
                                                          <div class="row">                                                                                                                           
                                <div class="col-sm-6">
                                    <div class="linha-colorida">Status</div><br />
                                     <div class="form-group">
                               <asp:DropDownList ID="ddlStatusConsultaNotas" runat="server" CssClass="form-control" Font-Size="11px">
                                                    <asp:ListItem Value="0" Text="TODAS"></asp:ListItem>
                                                    <asp:ListItem Value="1">ATIVAS</asp:ListItem>
                                                    <asp:ListItem Value="2">CANCELADAS</asp:ListItem>
                                           
                                                </asp:DropDownList>
                           </div>
                                     </div> 
                                                             
                                                                <div class="col-sm-6"><div class="linha-colorida">Cliente</div><br />
                                     <div class="form-group"> 
                              <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="COD" DataTextField="NM_CLIENTE" DataSourceID="dsClientes">
                                                </asp:DropDownList>
                           </div>
                                     </div></div>
                                                            
                             </div>
                               <div class="modal-footer">
                                                                  <asp:Button runat="server" Text="Pesquisar" ID="btnConsultaNotas" CssClass="btn btn-success"  />

                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharConsulta" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>
                                </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultaNotas" />
                            </Triggers>
                        </asp:UpdatePanel>

                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Style="display: none;"></asp:TextBox>

                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender10" runat="server" PopupControlID="pnlOBSRPS" TargetControlID="TextBox2" CancelControlID="TextBox2"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlOBSRPS" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">OBSERVAÇÃO DE RPS</h5>
                                                        </div>
                                                        <div class="modal-body">    
                    
                                            <asp:TextBox ID="txtOBSRPS" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                        
                                         </div>
                                  
                           
                      
                                                       
                                                                        
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnProsseguir" text="Prosseguir"/>
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFaturamento" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFiltro" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvFaturamento" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
    <asp:TextBox ID="txtResultado" runat="server" Style="display: none" CssClass="form-control"></asp:TextBox>
    <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
    <asp:SqlDataSource ID="dsFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Faturamento] WHERE DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL ORDER BY DT_VENCIMENTO,NR_PROCESSO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

<asp:SqlDataSource ID="dsBanco" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CONTA_BANCARIA, NM_CONTA_BANCARIA FROM TB_CONTA_BANCARIA
union SELECT  0, ' Selecione' ORDER BY ID_CONTA_BANCARIA"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT DISTINCT 1 COD, NM_CLIENTE FROM TB_FATURAMENTO WHERE NM_CLIENTE IS NOT NULL union SELECT  0, 'Selecione' ORDER BY COD"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>

        function ImprimirND() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            if (ID == "") {
                alert("Selecione um registro!");
                console.log(0);
            }
            else {

                var resultado = confirm("Deseja emitir nota de débito da fatura selecionada?");
                if (resultado == true) {
                    console.log(ID);

                    window.open('EmissaoNDFaturamento.aspx?id=' + ID, '_blank');

                }
            }
        }


        function getCliente() {
            var Cliente = document.getElementById('<%= lblClienteBaixa.ClientID %>').innerHTML;
            return Cliente;

        }
        function getProcesso() {
            var Processo = document.getElementById('<%= lblProcessoBaixa.ClientID %>').innerHTML;
            return Processo;
        }

        function FuncImprimirRPS() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            console.log(ID);

            window.open('ReciboProvisorioServico.aspx?id=' + ID, '_blank');
        }


        function ImprimirNota() {
            var COD_VER_NFSE = document.getElementById('<%= txtCOD_VER_NFSE.ClientID %>').value
            var NR_NOTA = document.getElementById('<%= txtNR_NOTA.ClientID %>').value;
            var CNPJFCA = document.getElementById('<%= txtCNPJFCA.ClientID %>').value;

            var LINK = 'http://visualizar.ginfes.com.br/report/consultarNota?__report=nfs_ver4&cdVerificacao=' + COD_VER_NFSE + '&numNota=' + NR_NOTA + '&cnpjPrestador=' + CNPJFCA
            console.log(LINK);
            document.getElementById('<%= txtCOD_VER_NFSE.ClientID %>').value = ''
            document.getElementById('<%= txtNR_NOTA.ClientID %>').value = ''
            window.open(LINK, '_blank');

        }



        function FuncRecibo() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            console.log(ID);

            window.open('ReciboPagamento.aspx?id=' + ID, '_blank');
        }


        function SalvaPosicao() {
            var posicao = document.getElementById('DivGrid').scrollTop;
            if (posicao) {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
        };


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= TextBox1.ClientID %>').value;
            document.getElementById('DivGrid').scrollTop = valor;
        };













        function gerarRPS() {

            //$.ajax({
            //    type: "POST",
            //    url: "wsNvocc.asmx/IntegraNFePrefeitura",
            //    data: '{idCont:"' + id + '" }',
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (dado) {
            //        alert("DEU CERTO!");

            //    }
            //})

        }

    </script>
</asp:Content>
