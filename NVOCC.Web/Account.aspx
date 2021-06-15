<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Account.aspx.vb" Inherits="NVOCC.Web.Account" %>
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

        .none {
            display: none
        }

        th {
            position: sticky !important;
            top: 0;
            background-color: #e6eefa;
            text-align: center;
        }

        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }
    </style>
     <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">INVOICES
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
                                   <asp:ListItem Value="1">Número do Invoice</asp:ListItem>
                                   <asp:ListItem Value="2">Número do Processo</asp:ListItem>
                                   <asp:ListItem Value="3">Nome do BL</asp:ListItem>
                                   <asp:ListItem Value="4">Agente</asp:ListItem>
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
                               <asp:TextBox ID="txtVencimentoInicial" placeholder="Venc. Inicial" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                       </div>
                       <div class="col-sm-1">
                           <div class="form-group">
                               <asp:TextBox ID="txtVencimentoFinal" placeholder="Venc. Final" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                       </div>
                       <div class="col-sm-1">

                           <div class="form-group">

                               <asp:CheckBoxList ID="ckStatus" Style="padding: 0px; font-size: 12px; text-align: justify" runat="server" RepeatDirection="vertical">
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Conferidos</asp:ListItem>
                                   <asp:ListItem Value="3" Selected="True">&nbsp;Não Conferidos</asp:ListItem>
                               </asp:CheckBoxList>
                           </div>
                       </div>
                       <div class="col-sm-1">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>
                       <div class="col-sm-4">
                           <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />

                           <asp:LinkButton ID="lkInvoices" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Invoices</asp:LinkButton>
                           <asp:LinkButton ID="lkRelatorios" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Relatórios</asp:LinkButton>
                           <asp:LinkButton ID="lkFechamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" href="FechamentoCambio.aspx">Fechamento</asp:LinkButton>
                           <asp:LinkButton ID="lkProcessoPeriodo" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Processo no Periodo</asp:LinkButton>
                       </div>
                   </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="Button1" CssClass="btn btn-success" />

                                <ajaxToolkit:ModalPopupExtender ID="mpeInvoice" runat="server" PopupControlID="pnlInvoice" TargetControlID="lkInvoices" CancelControlID="btnFecharInvoice"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlInvoice" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">INVOICES</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                            <div class="row" style="margin-left:5px;margin-right:5px">
                                   <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           
<asp:LinkButton ID="lkNovaInvoice" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Nova Invoice</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                             
                                                                                <asp:LinkButton ID="lkAlterarInvoice" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Alterar Invoice</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                                                                     <asp:LinkButton ID="lkExcluirInvoice" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Excluir Invoice</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharInvoice" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlRelatorios" TargetControlID="lkRelatorios" CancelControlID="btnFecharRelatorios"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlRelatorios" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">RELATÓRIO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                            <div class="row" style="margin-left:5px;margin-right:5px">
                                   <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkSOA" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Statement of Account</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                 <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkAvisoEmbarque" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Aviso de Embarque</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                             
                                                   <asp:LinkButton ID="lkInvoiceFCA" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Invoice FCA</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>      
                                      <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">                                             
                                                   <asp:LinkButton ID="lkGeraCSV" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Gerar CSV</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>     
                                </div>  
                             </div>                   
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRelatorios" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlNovaInvoice" TargetControlID="lkNovaInvoice" CancelControlID="btnFecharNovaInvoice"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlNovaInvoice" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">NOVA INVOICE</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                           <label class="control-label">ID:</label>
                                                                       <asp:TextBox ID="txtIDInvoice" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                           </div>
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlServico_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>

                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Emissor:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Processo ou BL:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox2" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Vencimento:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtProcesso_BasicoMaritimo" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>   
                                                                        <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo Fatura:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Número Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox3" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox4" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                            <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbPremiacao_TaxasMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Conferido"></asp:CheckBox>

                                    </div>
                                        </div>
                                         </div>   
                                                                                                                                    
                                                            <div class="row">
                                     
                                      
                                        <div class="col-sm-10">
                                    <div class="form-group">
                                          <label class="control-label">Observações:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox6" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                               <asp:Button runat="server" Text="Gravar" ID="Button2" CssClass="btn btn-success btn-block" />


                                    </div>
                                        </div>
                                         </div>   
<div class="row">
                                     
                                      
                                       <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="DEVOLUÇÃO DE FRETE" ID="btnDevolucaoFrete" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS EXTERIOR" ID="btnTaxasExterior" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS DECLARADAS" ID="btnTaxasDeclaradas" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="COMISSOES" ID="btnComissoes" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="OUTRAS TAXAS" ID="btnOutrasTaxas" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
                                         </div>   
                                                            <div class="row">
                                     
                                      
                                       <div class="col-sm-12">
                                           gridview de itens
                                           </div>
                                                                </div>
                                                            <div class="row">
                                     
                                      
                                       <div class="col-sm-4">
                                                                          <asp:linkButton runat="server" Text="ABRIR CONFERENCIA" ID="Button8" CssClass="btn btn-primary" href="Conferencia.aspx" target="_blank"/>

                                           </div>
                                                                <div class="col-sm-offset-4 col-sm-4">
                                                                    TOTAL DA INVOICE:0,00
                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNovaInvoice" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNovaInvoice" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlDevolucaoFrete" TargetControlID="btnDevolucaoFrete" CancelControlID="btnFecharDevolucaoFrete"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlDevolucaoFrete" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Devolução Frete</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           <label class="control-label">TIPO DE DEVOLUÇÃO:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                          gridview
                                        </div>
                                         </div> 
                                        </div>

                                                            <div class="row">
                                     
                                      
                                       <div class="col-sm-4">
                                                              COMPRA

                                           </div>
                                                                <div class="col-sm-4">
                                                              VENDA

                                           </div>
                                                                <div class="col-sm-4">
                                                              DEVOLUCAO

                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharDevolucaoFrete" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarDevolucaoFrete" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlTaxasExterior" TargetControlID="btnTaxasExterior" CancelControlID="btnFecharTaxasExterior"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlTaxasExterior" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Taxas Exterior</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                          gridview
                                        </div>
                                         </div> 
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                                                              TOTAL

                                           </div>
                                                               
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharTaxasExterior" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button4" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="pnlTaxasDeclaradas" TargetControlID="btnTaxasDeclaradas" CancelControlID="btnFecharTaxasDeclaradas"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlTaxasDeclaradas" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Taxas Declaradas</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                          gridview
                                        </div>
                                         </div> 
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                                                              TOTAL

                                           </div>
                                                               
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharTaxasDeclaradas" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button5" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="pnlComissoes" TargetControlID="btnComissoes" CancelControlID="btnFecharComissoes"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlComissoes" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Comissões</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                          gridview
                                        </div>
                                         </div> 
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                                                              TOTAL

                                           </div>
                                                               
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharComissoes" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button6" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" PopupControlID="pnlOutrasTaxas" TargetControlID="btnOutrasTaxas" CancelControlID="btnFecharOutrasTaxas"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlOutrasTaxas" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Outras Taxas</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                          gridview
                                        </div>
                                         </div> 
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                                                              TOTAL

                                           </div>
                                                               
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharOutrasTaxas" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button7" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                            </ContentTemplate>                            
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
<%--                                        <asp:GridView ID="dgvInvoice" DataKeyNames="ID_ACCOUNT_INVOICE" DataSourceID="dsInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="NR_INVOICE" HeaderText="Nº INVOICE" SortExpression="NR_INVOICE" />
                                                <asp:BoundField DataField="NM_ACCOUNT_TIPO_INVOICE" HeaderText="TIPO" SortExpression="NM_ACCOUNT_TIPO_INVOICE" />
                                                <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                <asp:BoundField DataField="DT_INVOICE" HeaderText="DATA INVOICE" SortExpression="DT_INVOICE" />                             <asp:BoundField DataField="PROCESSO" HeaderText="PROCESSO" SortExpression="PROCESSO" />             
                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                                <asp:BoundField DataField="NM_AGENTE" HeaderText="AGENTE" SortExpression="NM_AGENTE" />
                                                <asp:BoundField DataField="FL_CONFERIDO" HeaderText="CONFERIDO" SortExpression="FL_CONFERIDO" /> 
                                                <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                                  <asp:BoundField DataField="VL_TOTAL" HeaderText="VALOR" SortExpression="VL_TOTAL" />             
                                                <asp:BoundField DataField="DT_FECHAMENTO" HeaderText="DATA FECHAMENTO" SortExpression="DT_FECHAMENTO" />
                                                <asp:BoundField DataField="DS_OBSERVACAO" HeaderText="OBSERVAÇÕES" SortExpression="DS_OBSERVACAO" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_ACCOUNT_INVOICE") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>--%>

                                </div>

                            </ContentTemplate>                           
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
         <asp:SqlDataSource ID="dsInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_BL, NM_TIPO_BL FROM TB_TIPO_BL 
union SELECT  0, 'Selecione' FROM TB_TIPO_BL ORDER BY ID_TIPO_BL">
</asp:SqlDataSource>
                          <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
