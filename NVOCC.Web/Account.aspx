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
                                                <asp:ListItem Value="3">Número do BL</asp:ListItem>
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
                                            <asp:RadioButtonList ID="rdStatus" runat="server" Style="padding: 0px; font-size: 12px; text-align: justify">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;Conferidos</asp:ListItem>
                                                <asp:ListItem Value="0">&nbsp;Não Conferidos</asp:ListItem>
                                            </asp:RadioButtonList>
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
                                                                                     <asp:LinkButton ID="lkExcluirInvoice" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');">Excluir Invoice</asp:LinkButton>
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

                                <asp:Button runat="server" Visible="false" ID="Button2" Text="Close" />
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

                                <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlNovaInvoice" TargetControlID="TextBox1" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlNovaInvoice" runat="server" CssClass="modalPopup" Style="display: none;">

                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">INVOICE</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                    <div class="alert alert-success" id="divSuccessInvoice" runat="server" visible="false">
                                    <asp:Label ID="lblSuccessInvoice" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErroInvoice" runat="server" visible="false">
                                    <asp:Label ID="lblErroInvoice" runat="server"></asp:Label>
                                </div>        
                                   <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                           <label class="control-label">ID:</label>
                                        <asp:TextBox ID="txtIDInvoice" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txtID_BL" visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:TextBox ID="txtGrau" visible="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                           </div>
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlAgente" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO"></asp:DropDownList>

                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Emissor:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlEmissor" runat="server" CssClass="form-control" AutoPostBack="true" Font-Size="11px" DataTextField="NM_ACCOUNT_TIPO_EMISSOR" DataSourceID="dsTipoEmissor" DataValueField="ID_ACCOUNT_TIPO_EMISSOR"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Tipo:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlTipoInvoice" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ACCOUNT_TIPO_INVOICE" DataSourceID="dsTipoInvoice" DataValueField="ID_ACCOUNT_TIPO_INVOICE"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Processo ou BL:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtProc_ou_BL" Autopostback="true" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Vencimento:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtVencimento" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>   
                                                                        <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo Fatura:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlTipoFatura" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ACCOUNT_TIPO_FATURA" DataSourceID="dsTipoFatura" DataValueField="ID_ACCOUNT_TIPO_FATURA"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Número Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtNumeroInvoice" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtDataInvoice" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                            <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbConferido" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Conferido"></asp:CheckBox>

                                    </div>
                                        </div>
                                         </div>   
                                                                                                                                    
                                                            <div class="row">
                                     
                                      
                                        <div class="col-sm-10">
                                    <div class="form-group">
                                          <label class="control-label">Observações:</label>
                                                <asp:TextBox ID="txtObsInvoice" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                               <asp:Button runat="server" Text="Gravar" ID="btnGravarCabeçalho" CssClass="btn btn-success btn-block" />


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
                                                            <asp:Button runat="server" CssClass="btn btn-success" Visible="false" ID="btnSalvarNovaInvoice" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>

                                </asp:Panel>



                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlDevolucaoFrete" TargetControlID="Button2" CancelControlID="btnFecharDevolucaoFrete"></ajaxToolkit:ModalPopupExtender>
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
                                                <asp:DropDownList ID="ddlTipoDevolucao" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_FRETE_AGENTE" DataSourceID="dsDevolucaoFrete" DataValueField="CD_STATUS_FRETE_AGENTE"></asp:DropDownList>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                                                                  <asp:GridView ID="dgvDevolucao" DataKeyNames="ID_BL_TAXA" DataSourceID="dsDevolucao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                <asp:BoundField DataField="DT_INVOICE" HeaderText="DATA INVOICE" SortExpression="DT_INVOICE" />                             <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />             
                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                        <asp:TemplateField HeaderText="VALOR COMPRA" SortExpression="VL_COMPRA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorCompra" runat="server" Text='<%# Eval("VL_COMPRA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                 <asp:TemplateField HeaderText="VALOR VENDA" SortExpression="VL_VENDA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorVenda" runat="server" Text='<%# Eval("VL_VENDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                                                               <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />
                                       
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>

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
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirDevolucaoFrete" text="Incluir" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlTaxasExterior" TargetControlID="Button2" CancelControlID="btnFecharTaxasExterior"></ajaxToolkit:ModalPopupExtender>
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
                                                          <asp:GridView ID="dgvTaxasExterior" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasExterior" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="CD_ORIGEM" HeaderText="ORIGEM" SortExpression="CD_ORIGEM" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />          
                                                                       <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                    <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
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
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirTaxasExterior" text="Incluir" />

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
                                                                                                    <asp:GridView ID="dgvTaxasDeclaradas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasDeclaradas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                                                              <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="CD_DECLARADO" HeaderText="DECLARADO" SortExpression="CD_DECLARADO" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                 <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>              
                                                                                               <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>

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
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirTaxasDeclaradas" text="Incluir" />

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
                                     dgvComissoes
                                       <div class="col-sm-12">
                                    <div class="form-group">
<asp:GridView ID="dgvComissoes" DataKeyNames="ID_BL_TAXA" DataSourceID="dsComissoes" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
             
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                          <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>         
                                                                                           
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
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
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirComissoes" text="Incluir" />

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
                                          <asp:GridView ID="dgvOutrasTaxas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsOutrasTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="CD_DECLARADO" HeaderText="DECLARADO" SortExpression="CD_DECLARADO" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />       
                                                               <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>

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
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirOutrasTaxas" text="Incluir" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <div runat="server" id="divAuxiliar" visible="false">
                                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                            <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                        </div>
                                        <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
                                            <asp:GridView ID="dgvInvoice" DataKeyNames="ID_ACCOUNT_INVOICE" DataSourceID="dsInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                                <Columns>
                                                    <asp:BoundField DataField="NR_INVOICE" HeaderText="Nº INVOICE" SortExpression="NR_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_INVOICE" HeaderText="TIPO" SortExpression="NM_ACCOUNT_TIPO_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                    <asp:BoundField DataField="DT_INVOICE" HeaderText="DATA INVOICE" SortExpression="DT_INVOICE" />
                                                    <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                    <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                                    <asp:BoundField DataField="NM_AGENTE" HeaderText="AGENTE" SortExpression="NM_AGENTE" />
                                                    <asp:TemplateField HeaderText="CONFERIDO" SortExpression="FL_CONFERIDO">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbConferido" Checked='<%# Eval("FL_CONFERIDO") %>' Enabled="false" runat="server" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                    <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />
                                                    <asp:BoundField DataField="VALOR_TOTAL" HeaderText="VALOR" SortExpression="VALOR_TOTAL" />
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
                                            </asp:GridView>

                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnPesquisa" />
                                <asp:AsyncPostBackTrigger ControlID="ddlEmissor" />
                                <asp:AsyncPostBackTrigger ControlID="txtProc_ou_BL" />
                                <asp:AsyncPostBackTrigger ControlID="btnFecharNovaInvoice" />
                                <asp:AsyncPostBackTrigger ControlID="btnTaxasExterior" />
                                <asp:AsyncPostBackTrigger ControlID="btnDevolucaoFrete" />


                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </div>

                </div>


            </div>
        </div>

    </div>
    <asp:SqlDataSource ID="dsInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,A.NM_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,A.DT_FECHAMENTO,A.DS_OBSERVACAO,(SELECT SUM(ISNULL(VL_TAXA_BR,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE('@DATAINICIAL','@DATAFINAL')) AS A 
INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE">
        <SelectParameters>
            <asp:ControlParameter Name="DATAINICIAL" Type="string" ControlID="txtVencimentoInicial" />
            <asp:ControlParameter Name="DATAFINAL" Type="string" ControlID="txtVencimentoFinal" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDevolucao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_COMPRA,VL_VENDA,DT_RECEBIMENTO FROM FN_ACCOUNT_DEVOLUCAO_FRETE (@ID_BL , '@GRAU')  WHERE ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasExterior" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT  ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO,CD_ORIGEM  FROM FN_ACCOUNT_TAXAS_EXTERIOR (@ID_BL , '@GRAU')  WHERE  ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissoes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA FROM  FN_ACCOUNT_DEVOLUCAO_COMISSAO (@ID_BL , '@GRAU') WHERE ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasDeclaradas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO,CD_DECLARADO FROM FN_ACCOUNT_TAXAS_DECLARADAS (@ID_BL , '@GRAU')  WHERE  ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsOutrasTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,NM_ITEM_DESPESA,SIGLA_MOEDA,VL_TAXA,CD_DECLARADO,DT_RECEBIMENTO  FROM FN_ACCOUNT_OUTRAS_TAXAS (@ID_BL , '@GRAU')  WHERE  ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDevolucaoFrete" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT '0'CD_STATUS_FRETE_AGENTE, 'Selecione'NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE 
UNION
SELECT CD_STATUS_FRETE_AGENTE, NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE 
"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoEmissor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ACCOUNT_TIPO_EMISSOR, NM_ACCOUNT_TIPO_EMISSOR FROM TB_ACCOUNT_TIPO_EMISSOR
union SELECT 0, 'Selecione' FROM TB_ACCOUNT_TIPO_EMISSOR ORDER BY ID_ACCOUNT_TIPO_EMISSOR"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ACCOUNT_TIPO_INVOICE, NM_ACCOUNT_TIPO_INVOICE FROM TB_ACCOUNT_TIPO_INVOICE
union SELECT 0, 'Selecione' FROM TB_ACCOUNT_TIPO_INVOICE ORDER BY ID_ACCOUNT_TIPO_INVOICE"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoFatura" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ACCOUNT_TIPO_FATURA, NM_ACCOUNT_TIPO_FATURA FROM TB_ACCOUNT_TIPO_FATURA
union SELECT 0, 'Selecione' FROM TB_ACCOUNT_TIPO_FATURA ORDER BY ID_ACCOUNT_TIPO_FATURA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
