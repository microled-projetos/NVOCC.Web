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

        .tableFixHead {
            overflow-y: auto;
            max-height: 400px;
            overflow-x: auto;
            max-width: 100%;
        }


        th {
            position: sticky !important;
            top: 0;
            background-color: #e6eefa;
            text-align: left;
            padding-left: 18px;
        }
    </style>
    <div style="float: right; display: none">
        <a id="ajuda" href="#" title="Ajuda">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
                <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z" />
            </svg></a>
    </div>
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


                                <%--FILTROS--%>

                                <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">

                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server">Filtro:</asp:Label>

                                            <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                <asp:ListItem Value="1">Número do Invoice</asp:ListItem>
                                                <asp:ListItem Value="2">Número do Processo</asp:ListItem>
                                                <asp:ListItem Value="3">Número do BL</asp:ListItem>
                                                <asp:ListItem Value="4">Agente</asp:ListItem>
                                                <asp:ListItem Value="5">Emissor</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" Style="color: white" runat="server">x</asp:Label>

                                            <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server">Venc. Inicial</asp:Label>
                                            <asp:TextBox ID="txtVencimentoInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server">Venc. Final</asp:Label>
                                            <asp:TextBox ID="txtVencimentoFinal" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">

                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rdStatus" runat="server" Style="padding: 0px; font-size: 12px; text-align: justify">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;Conferidos</asp:ListItem>
                                                <asp:ListItem Value="0">&nbsp;Não Conferidos</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" Style="color: white" runat="server">x</asp:Label><br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="border: ridge 1px;">
                                        <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />

                                        <asp:LinkButton ID="lkInvoices" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Invoices</asp:LinkButton>
                                        <asp:LinkButton ID="lkRelatorios" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Relatórios</asp:LinkButton>
                                        <asp:LinkButton ID="lkProcessoPeriodo" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Processo no Periodo</asp:LinkButton>
                                    </div>
                                </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="Button1" CssClass="btn btn-success" />


                                <%--MENU DE INVOICES--%>


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
                                <asp:TextBox ID="TextBox3" Style="display: none" runat="server"></asp:TextBox>


                                <%--MENU DE RELATORIOS--%>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlRelatorios" TargetControlID="lkRelatorios" CancelControlID="btnFecharRelatorios" OkControlID="lkSOA"></ajaxToolkit:ModalPopupExtender>
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
                                           
                                        <asp:LinkButton ID="lkAvisoEmbarque" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px" Visible="true">Aviso de Embarque</asp:LinkButton>


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


                                <%--FILTROS RELATORIOS SOA--%>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender9" runat="server" PopupControlID="pnlSOA" TargetControlID="lkSOA" CancelControlID="btnFecharSOA"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlSOA" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">RELATÓRIO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                         <div class="row">
                              <div class="col-sm-offset-4 col-sm-2"">
                                    <div class="form-group">
                                          <label class="control-label">Venc. Inicial:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtVencimentoInicialSOA" runat="server"  CssClass="form-control data"></asp:TextBox>


                                    </div>
                                        </div>
                              <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Venc. Final:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtVencimentoFinalSOA" runat="server"  CssClass="form-control data"></asp:TextBox>


                                    </div>
                                        </div>
                             </div>
                                  
                                                        <div class="row">
                                     <div class="col-sm-offset-2 col-sm-8">
                                    <div class="form-group">
                                           
                                        <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlAgenteSoa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgenteSOA" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                    </div>
                                   
                             </div>   

                                                        </div>                
              
                                 <div class="row">
                                     <div class="col-sm-offset-4 col-sm-2">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkImprimirSOA1" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Imprimir SOA I</asp:LinkButton>
                                        </div>
                                         </div>
                                         <div class="col-sm-2">
                                              <div class="form-group">
                                        <asp:LinkButton ID="lkImprimirSOA2" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Imprimir SOA II</asp:LinkButton>

                                    </div>
                                   
                             </div>                   
                   </div>
                                                            </div>
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharSOA" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>
                                <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>

                                <%--ALTERAR/CADASTRAR INVOICE--%>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlNovaInvoice" TargetControlID="lkNovaInvoice" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                                    <ContentTemplate>
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
                                        <asp:TextBox ID="txtID_BL" style="display:none" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:TextBox ID="txtGrau" style="display:none" runat="server" CssClass="form-control"></asp:TextBox>

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
                                                <asp:TextBox ID="txtVencimento" runat="server"  CssClass="form-control data"></asp:TextBox>


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
                                                <asp:TextBox ID="txtNumeroInvoice" runat="server"  CssClass="form-control" MaxLength="40"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="txtDataInvoice" runat="server"  CssClass="form-control data"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                            <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbConferido" runat="server" Checked="true" CssClass="form-control" Text="&nbsp;&nbsp;Conferido"></asp:CheckBox>

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
                               <asp:Button runat="server" Text="Gravar" ID="btnGravarCabecalho" CssClass="btn btn-success btn-block" />


                                    </div>
                                        </div>
                                         </div>   
<div class="row">
                                     
                                      
                                       <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="DEVOLUÇÃO DE FRETE" ID="btnDevolucaoFrete" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-4">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS EXTERIOR/DECLARADAS" ID="btnTaxasExteriorDeclaradas" CssClass="btn btn-block btnn" />


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
                                            <div class="table-responsive tableFixHead">
                                     <asp:GridView ID="dgvItensInvoice" DataKeyNames="ID_ACCOUNT_INVOICE_ITENS" DataSourceID="dsItensInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro vinculado à invoice.">
                                            <Columns>
                                                <asp:BoundField DataField="ID_ACCOUNT_INVOICE_ITENS"  HeaderText="ID" Visible="False" SortExpression="ID_ACCOUNT_INVOICE_ITENS" />
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="NM_ACCOUNT_TIPO_DEVOLUCAO" HeaderText="TIPO DEVOLUÇÃO" SortExpression="NM_ACCOUNT_TIPO_DEVOLUCAO" />
                                                               <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_ACCOUNT_INVOICE_ITENS") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:small"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                                </div>
                                           </div>
                                                                </div>
                                                            <div class="row">
                                     
                                      
                                       <div class="col-sm-4">
                                                                          <asp:linkButton runat="server" Text="ABRIR CONFERENCIA" ID="Button8" CssClass="btn btn-primary" OnClientClick="Conferencia()" />

                                           </div>
                                                                <div class="col-sm-offset-4 col-sm-4">
                                                                    TOTAL DA INVOICE: <asp:label ID="lblTotalInvoice" runat="server"/>
                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNovaInvoice" text="Close" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>

                                        </asp:Panel>

                                        <%--GRID DEVOLUCAO--%>


                                        <asp:TextBox ID="AuxDevolucao" Style="display: none" runat="server"></asp:TextBox>

                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlDevolucaoFrete" TargetControlID="AuxDevolucao" CancelControlID="AuxDevolucao"></ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlDevolucaoFrete" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Devolução Frete</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                            <div class="alert alert-info" id="divinfo" runat="server" visible="false">
                                    <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                </div>
                                                            <br />
                                   <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           <label class="control-label">TIPO DE DEVOLUÇÃO:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlTipoDevolucao" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_FRETE_AGENTE" DataSourceID="dsDevolucaoFrete" DataValueField="ID_STATUS_FRETE_AGENTE" AutoPostBack="True" ></asp:DropDownList>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group"><div class="table-responsive tableFixHead">
                                                                                  <asp:GridView ID="dgvDevolucao" DataKeyNames="ID_BL" DataSourceID="dsDevolucao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID_BL_TAXA" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTAXA" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                 <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="TAXA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                        
                                                <asp:TemplateField HeaderText="VALOR COMPRA" SortExpression="VL_COMPRA">
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
                                        </div>

                                                            <div class="row">
                                     
                                      
                                       <div class="col-sm-4">
                                                             Compra: <asp:label ID="lblValorFreteCompra" runat="server"/>

                                           </div>
                                                                <div class="col-sm-4">
                                                             Venda: <asp:label ID="lblValorFreteVenda" runat="server"/>


                                           </div>
                                                                <div class="col-sm-4">
                                                            Total: <asp:label ID="lblValorFreteDevolucao" runat="server"/>


                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharDevolucaoFrete" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirDevolucaoFrete" text="Incluir" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnFecharDevolucaoFrete" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnIncluirDevolucaoFrete" />
                                                    <asp:AsyncPostBackTrigger EventName="load" ControlID="dgvDevolucao" />

                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoDevolucao" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>


                                        <%--GRID TAXAS DECLARADAS/EXTERIOR--%>

                                        <asp:TextBox ID="AuxExteriorDeclaradas" Style="display: none" runat="server"></asp:TextBox>

                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender10" runat="server" PopupControlID="pnlExteriorDeclaradas" TargetControlID="AuxExteriorDeclaradas" CancelControlID="AuxExteriorDeclaradas"></ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlExteriorDeclaradas" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Taxas Exterior/Declaradas</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                    <div class="row">
                                     
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                                                    <div class="table-responsive tableFixHead">                                <asp:GridView ID="dgvTaxasExteriorDeclaradas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasExteriorDeclaradas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado."  Visible="false">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
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
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                                                          TOTAL:    <asp:label ID="lblTotalExteriorDeclaradas" runat="server"/>

                                           </div>
                                                               
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharTaxasExteriorDeclaradas" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnIncluirTaxasExteriorDeclaradas" text="Incluir" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                        </asp:Panel>


                                        <%--GRID COMISSOES--%>

                                        <asp:TextBox ID="AuxComissoes" Style="display: none" runat="server"></asp:TextBox>

                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="pnlComissoes" TargetControlID="AuxComissoes" CancelControlID="AuxComissoes"></ajaxToolkit:ModalPopupExtender>
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
                                           <label class="control-label">TIPO DIVISÃO PROFIT:</label>
                                                 <asp:DropDownList ID="ddlDivisaoProfit" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_DIVISAO_PROFIT" DataTextField="NM_TIPO_DIVISAO_PROFIT" DataSourceID="dsDivisaoProfit" Enabled="false">
                                        </asp:DropDownList>

                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                        <div class="table-responsive tableFixHead">
<asp:GridView ID="dgvComissoes" DataKeyNames="ID_BL_TAXA" DataSourceID="dsComissoes" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
             
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                          <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorTaxa" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>         
                                              <asp:TemplateField HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA_CALCULADO") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                            </div>
                                        </div>
                                         </div> 
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                               TOTAL: <asp:label ID="lblTotalComissoes" runat="server"/>


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


                                        <%--GRID OUTRAS TAXAS--%>

                                        <asp:TextBox ID="AuxOutrasTaxas" Style="display: none" runat="server"></asp:TextBox>

                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" PopupControlID="pnlOutrasTaxas" TargetControlID="AuxOutrasTaxas" CancelControlID="AuxOutrasTaxas"></ajaxToolkit:ModalPopupExtender>
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

                                        <div class="table-responsive tableFixHead">
                                          <asp:GridView ID="dgvOutrasTaxas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsOutrasTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado."  Visible="false">
                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" cheched="True" runat="server" AutoPostBack="true"/>
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
                                        </div>

                                                            <div class="row">
                                     
                                      
                                                                <div class="col-sm-offset-6 col-sm-4">
                                                                   Total: <asp:label ID="lblTotalOutrasTaxas" runat="server"/>


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



                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharNovaInvoice" />
                                        <asp:AsyncPostBackTrigger ControlID="btnIncluirOutrasTaxas" />

                                        <asp:AsyncPostBackTrigger ControlID="ddlEmissor" />
                                        <asp:AsyncPostBackTrigger ControlID="txtProc_ou_BL" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGravarCabecalho" />
                                        <asp:AsyncPostBackTrigger ControlID="dgvItensInvoice" EventName="load" />

                                    </Triggers>
                                </asp:UpdatePanel>
                                <%--RELATORIO PROCESSOS PERIODO--%>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender8" runat="server" PopupControlID="pnlProcessoPeriodo" TargetControlID="lkProcessoPeriodo" CancelControlID="btnFecharProcessoPeriodo"></ajaxToolkit:ModalPopupExtender>

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProcessoPeriodo" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-xl" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Processos do Periodo</h5>
                                                        </div>
                                                        <div class="modal-body">   
                                                            <div class="alert alert-danger" id="divErroRelatorio" runat="server" visible="false">
                                    <asp:Label ID="lblErroRelatorio" runat="server"></asp:Label>
                                </div>
                                                            
 <div class="row">
                                     
                                      
                                                                <div class="col-sm-2">
<div class="form-group">
     <label class="control-label">Embarque Inicial:</label>
                                            <asp:TextBox ID="txtEmbarqueInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                           </div><div class="col-sm-2">
                                        <div class="form-group">
                                             <label class="control-label">Embarque Final:</label>
                                            <asp:TextBox ID="txtEmbarqueFinal" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
      <div class="col-sm-4">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label>
                                                  <asp:DropDownList ID="ddlAgenteRelatorio" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO">
                                        </asp:DropDownList>

                                    </div>
                                        </div>
      <div class=" col-sm-3">
<div class="form-group">
     <label class="control-label">Nº Processo:</label>
                                         <asp:TextBox ID="txtProcessoRelatorio" runat="server" CssClass="form-control"></asp:TextBox>                        

                                        </div>
                                           </div>
     <div class=" col-sm-1">
<div class="form-group">
     <label class="control-label"></label>
                                        <asp:Button runat="server" CssClass="btn btn-success" ID="btnBuscarRelatorio" text="Buscar" />                 

                                        </div>
                                           </div>
                                                               
                                                                </div>
                                   
 <div class="table-responsive tableFixHead">
<asp:GridView ID="dgvProcessoPeriodo" DataKeyNames="ID_BL" DataSourceID="dsProcessoPeriodo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." >
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="BL_MASTER" HeaderText="MBL" SortExpression="BL_MASTER" />
                                                <asp:BoundField DataField="TIPO FRETE MASTER" HeaderText="FRETE MASTER" SortExpression="TIPO FRETE MASTER" />
                                                <asp:BoundField DataField="BL_HOUSE" HeaderText="HBL" SortExpression="BL_HOUSE" />
                                                <asp:BoundField DataField="TIPO FRETE HOUSE" HeaderText="FRETE HOUSE" SortExpression="TIPO FRETE HOUSE" />
                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="ESTUFAGEM" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="ORIGEM" HeaderText="ORIGEM" SortExpression="ORIGEM" />
                                                <asp:BoundField DataField="DESTINO" HeaderText="DESTINO" SortExpression="DESTINO" />
                                                <asp:BoundField DataField="CLIENTE"  HeaderText="CLIENTE" SortExpression="CLIENTE" /> 
                                                <asp:BoundField DataField="AGENTE_INTERNACIONAL" HeaderText="AGENTE INTER." SortExpression="AGENTE_INTERNACIONAL" />
                                                <asp:BoundField DataField="TRANSPORTADOR" HeaderText="TRANSPORTADOR" SortExpression="TRANSPORTADOR" />
                                                <asp:BoundField DataField="DT_PREVISAO_EMBARQUE_MASTER" HeaderText="PREV. EMBARQUE" SortExpression="DT_PREVISAO_EMBARQUE_MASTER" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_EMBARQUE_MASTER" HeaderText="EMBARQUE" SortExpression="DT_EMBARQUE_MASTER" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="DT_PREVISAO_CHEGADA_MASTER" HeaderText="PREV. CHEGADA" SortExpression="DT_PREVISAO_CHEGADA_MASTER" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="DT_CHEGADA_MASTER" HeaderText="CHEGADA" SortExpression="DT_CHEGADA_MASTER" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="VL_CAMBIO"  HeaderText="CAMBIO" SortExpression="VL_CAMBIO" /> 
                                                <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="DATA LIQUIDAÇÃO" SortExpression="DT_LIQUIDACAO" DataFormatString="{0:dd/MM/yyyy}"/>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                       </div>
                                        

                                                           
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">                   <div style="float:left"> 
                                   <asp:Button runat="server" CssClass="btn btn-default btnn" ID="btnRelacaoAgentes" text="Relação de Agentes" />
                                   <asp:Button runat="server" CssClass="btn btn-default btnn" ID="btnCSVProcessoPeriodo" text="Exportar para CSV" /></div>
                                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharProcessoPeriodo" text="Close" />
                  
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscarRelatorio" />
                                        <asp:PostBackTrigger ControlID="btnCSVProcessoPeriodo" />
                                        <asp:PostBackTrigger ControlID="btnRelacaoAgentes" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <%--GRID PRINCIPAL--%>

                                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <div runat="server" id="divAuxiliar" style="display: none">
                                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                            <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                        </div>
                                        <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="max-height: 600px !important;">
                                            <asp:GridView ID="dgvInvoice" DataKeyNames="ID_ACCOUNT_INVOICE" DataSourceID="dsInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 600px !important; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false" OnSorting="dgvInvoice_Sorting">
                                                <Columns>
                                                    <asp:BoundField DataField="NR_INVOICE" HeaderText="Nº INVOICE" SortExpression="NR_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_INVOICE" HeaderText="TIPO" SortExpression="NM_ACCOUNT_TIPO_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                    <asp:BoundField DataField="DT_INVOICE" HeaderText="DATA INVOICE" SortExpression="DT_INVOICE" />
                                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="DATA VENCIMENTO" SortExpression="DT_VENCIMENTO" />
                                                    <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                    <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                                   <%-- <asp:BoundField DataField="NM_AGENTE" HeaderText="AGENTE" SortExpression="NM_AGENTE" />--%>
                                                         <asp:TemplateField HeaderText="AGENTE" HeaderStyle-ForeColor="#337ab7" HeaderStyle-HorizontalAlign="Center" SortExpression="NM_AGENTE">
                    <ItemTemplate>
                            <a href="DadosBancariosAgente.aspx?tipo=a&id=<%# Eval("ID_PARCEIRO_AGENTE") %>" title="AGENTE" target="_blank" > <asp:Label ID="lblAgente" runat="server" Text='<%# Eval("NM_AGENTE") %>' /></a>
                    </ItemTemplate>
                     <ItemStyle VerticalAlign="Middle" CssClass="campo-acao" />
                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CONFERIDO" SortExpression="FL_CONFERIDO">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbConferido" Checked='<%# Eval("FL_CONFERIDO") %>' Enabled="false" runat="server" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                    <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />
                                                    <asp:TemplateField HeaderText="VALOR" SortExpression="VALOR_TOTAL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VALOR_TOTAL") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DT_FECHAMENTO" HeaderText="DATA FECHAMENTO" SortExpression="DT_FECHAMENTO" />
                                                    <asp:BoundField DataField="DS_OBSERVACAO" HeaderText="OBSERVAÇÕES" SortExpression="DS_OBSERVACAO" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                                CommandArgument='<%# Eval("ID_ACCOUNT_INVOICE") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicao()"></asp:LinkButton>
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
                                <asp:AsyncPostBackTrigger ControlID="lkAvisoEmbarque" />
                                <asp:AsyncPostBackTrigger ControlID="lkAlterarInvoice" />
                                <asp:PostBackTrigger ControlID="lkGeraCSV" />
                                <asp:AsyncPostBackTrigger ControlID="lkExcluirInvoice" />
                                <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvInvoice" />

                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </div>

                </div>


            </div>
        </div>
        <asp:TextBox ID="TextBox2" Style="display: none" runat="server"></asp:TextBox>

    </div>
    <div class="modal fade" id="modal-ajuda">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Sobre NVOCC: Account Invoice</h4>
                </div>
                <div class="modal-body">
                    <strong>Objetivo:</strong>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="dsInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,CONVERT(VARCHAR,A.DT_VENCIMENTO,103)DT_VENCIMENTO,CONVERT(VARCHAR,A.DT_INVOICE,103)DT_INVOICE,
case when A.ID_ACCOUNT_TIPO_INVOICE = 2 then
B.NR_PROCESSO else '' end NR_PROCESSO,B.NR_BL,SUBSTRING(A.NM_AGENTE,0,15)NM_AGENTE,A.ID_PARCEIRO_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,
        CONVERT(VARCHAR,A.DT_FECHAMENTO,103)DT_FECHAMENTO,SUBSTRING(A.DS_OBSERVACAO,0,50)DS_OBSERVACAO,(SELECT SUM(ISNULL(VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE(CONVERT(varchar,@DATAINICIAL,103),CONVERT(varchar,@DATAFINAL,103))) AS A 
INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE 
        group by A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,A.NM_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,A.DT_FECHAMENTO,A.DS_OBSERVACAO,A.DT_VENCIMENTO,A.ID_ACCOUNT_TIPO_INVOICE,A.ID_PARCEIRO_AGENTE">
        <SelectParameters>
            <asp:ControlParameter Name="DATAINICIAL" Type="datetime" ControlID="txtVencimentoInicial" />
            <asp:ControlParameter Name="DATAFINAL" Type="datetime" ControlID="txtVencimentoFinal" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDevolucao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_COMPRA,VL_VENDA,DT_RECEBIMENTO FROM FN_ACCOUNT_DEVOLUCAO_FRETE (@ID_BL , '@GRAU') A WHERE ID_MOEDA = @MOEDA AND A.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissoes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA FROM  FN_ACCOUNT_DEVOLUCAO_COMISSAO (@ID_BL , '@GRAU') WHERE ID_MOEDA = @MOEDA AND A.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasExteriorDeclaradas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO FROM FN_ACCOUNT_TAXAS_DECLARADAS (@ID_BL , '@GRAU')  WHERE  ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)
        union SELECT  ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO  FROM FN_ACCOUNT_TAXAS_EXTERIOR (@ID_BL , '@GRAU')  WHERE  ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:SqlDataSource ID="dsOutrasTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,NM_ITEM_DESPESA,SIGLA_MOEDA,VL_TAXA,CD_DECLARADO,DT_RECEBIMENTO FROM FN_ACCOUNT_OUTRAS_TAXAS (@ID_BL , '@GRAU')  WHERE  ID_MOEDA = @MOEDA AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS)">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="txtID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="txtGrau" />
            <asp:ControlParameter Name="MOEDA" Type="Int32" ControlID="ddlMoeda" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDivisaoProfit" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_DIVISAO_PROFIT,NM_TIPO_DIVISAO_PROFIT FROM [dbo].TB_TIPO_DIVISAO_PROFIT
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_DIVISAO_PROFIT"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDevolucaoFrete" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_STATUS_FRETE_AGENTE, NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE 
union SELECT 0, 'Selecione' FROM [dbo].TB_STATUS_FRETE_AGENTE ORDER BY ID_STATUS_FRETE_AGENTE"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1 and FL_ATIVO = 1
union SELECT 0, '  Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgenteSOA" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1 AND ID_PARCEIRO IN (SELECT DISTINCT ID_PARCEIRO_AGENTE FROM TB_ACCOUNT_INVOICE)
union SELECT 0, '  Todos os Agentes  ' FROM [dbo].[TB_PARCEIRO] ORDER BY NM_RAZAO"></asp:SqlDataSource>

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
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] WHERE ID_MOEDA <> 124 union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsProcessoPeriodo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT NR_PROCESSO,BL_MASTER,PAGAMENTO_BL_MASTER AS 'TIPO FRETE MASTER'
,NR_BL AS 'BL_HOUSE',TIPO_PAGAMENTO AS 'TIPO DO FRETE HOUSE',TIPO_ESTUFAGEM,
CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM) = '' THEN ORIGEM ELSE

(SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM)
END ORIGEM,CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO) = '' THEN DESTINO ELSE

(SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO)
END DESTINO,(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_CLIENTE)CLIENTE,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_AGENTE_INTERNACIONAL)AGENTE_INTERNACIONAL,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_TRANSPORTADOR)TRANSPORTADOR,convert(varchar,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,convert(varchar,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,convert(varchar,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,convert(varchar,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER , B.VL_CAMBIO,B.DT_LIQUIDACAO
FROM [dbo].[View_House] A
LEFT JOIN [VW_PROCESSO_RECEBIDO] B ON A.ID_BL = B.ID_BL
  WHERE CONVERT(VARCHAR,DT_EMBARQUE_MASTER,103) BETWEEN CONVERT(VARCHAR,'@EmbarqueInicial',103) AND CONVERT(VARCHAR,'@EmbarqueFinal',103)">
        <SelectParameters>
            <asp:ControlParameter Name="EmbarqueInicial" Type="string" ControlID="txtEmbarqueInicial" />
            <asp:ControlParameter Name="EmbarqueFinal" Type="string" ControlID="txtEmbarqueFinal" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsItensInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_ACCOUNT_INVOICE, 
(SELECT NR_PROCESSO FROM TB_BL WHERE B.ID_BL = ID_BL )NR_PROCESSO,
B.ID_ACCOUNT_INVOICE_ITENS, B.ID_BL AS ID_BL_ITENS, B.ID_BL_TAXA,  K.NM_ITEM_DESPESA, B.VL_TAXA, VL_TAXA_BR, I.NM_ACCOUNT_TIPO_DEVOLUCAO
FROM TB_ACCOUNT_INVOICE A
LEFT JOIN TB_ACCOUNT_INVOICE_ITENS B ON A.ID_ACCOUNT_INVOICE=B.ID_ACCOUNT_INVOICE
LEFT JOIN TB_ACCOUNT_TIPO_DEVOLUCAO I ON B.CD_TIPO_DEVOLUCAO=I.CD_ACCOUNT_TIPO_DEVOLUCAO
LEFT JOIN TB_ITEM_DESPESA K ON B.ID_ITEM_DESPESA=K.ID_ITEM_DESPESA
WHERE A.ID_ACCOUNT_INVOICE = @ID_ACCOUNT_INVOICE ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_ACCOUNT_INVOICE" Type="string" ControlID="txtIDInvoice" />
        </SelectParameters>

    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>

        $('#ajuda').on("click", function () {
            $('#modal-ajuda').modal('show');
        });

        function AvisoEmbarque() {


            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            console.log(ID);

            window.open('AvisoEmbarque.aspx?id=' + ID, '_blank');
        }

        function Conferencia() {


            var ID = document.getElementById('<%= txtIDInvoice.ClientID %>').value;
            var GRAU = document.getElementById('<%= txtGRAU.ClientID %>').value;


            window.open('Conferencia.aspx?bl=&id=' + ID + '&T=' + GRAU, '_blank');
        }

        function InvoiceFCA() {


            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            console.log(ID);

            window.open('DebitNote.aspx?id=' + ID, '_blank');
        }



        function InvoiceCredit() {


            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            console.log(ID);

            window.open('CreditNote.aspx?id=' + ID, '_blank');
        }


        function SOA1() {
            var AGENTE = document.getElementById('<%= ddlAgenteSoa.ClientID %>').value;
            window.open('SOA_I.aspx?ag=' + AGENTE, '_blank');
        }

        function SOA2() {
            var AGENTE = document.getElementById('<%= ddlAgenteSoa.ClientID %>').value;

            window.open('SOA_II.aspx?ag=' + AGENTE, '_blank');

        }


        function SalvaPosicao() {
            var posicao = document.getElementById('DivGrid').scrollTop;
            if (posicao) {
                document.getElementById('<%= TextBox2.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= TextBox2.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
        };


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= TextBox2.ClientID %>').value;
            document.getElementById('DivGrid').scrollTop = valor;
        };
    </script>
</asp:Content>
