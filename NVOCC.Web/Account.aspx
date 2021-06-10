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
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
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
                                    <%--GRIDVIEW--%>
                                </div>

                            </ContentTemplate>                           
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>

                          <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
