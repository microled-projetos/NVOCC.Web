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
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Em Aberto</asp:ListItem>
                                   <asp:ListItem Value="3" Selected="True">&nbsp;Liquidados</asp:ListItem>
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

                           <asp:LinkButton ID="lkFechamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Opções de Fechamento</asp:LinkButton>
                           <asp:LinkButton ID="lkRelatorios" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Relação dos Contratos</asp:LinkButton>
                       </div>
                   </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="Button1" CssClass="btn btn-success" />

                               

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlFechamento" TargetControlID="lkFechamento" CancelControlID="btnFecharFechamento"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Opções de Fechamento</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                            <div class="row" style="margin-left:5px;margin-right:5px">
                                   <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkNovoFechamento" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Novo Fechamento</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                 <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkBaixarFechamento" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Baixar Fechamento</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">
                                             
                                                   <asp:LinkButton ID="lkCancelarFechamento" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Cancelar Fechamento</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>      
                                      <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">                                             
                                                   <asp:LinkButton ID="lkExclurFechamento" runat="server" CssClass="btn btnn btn-default btn-block" Style="font-size: 15px">Excluir Fechamento</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>     
                                </div>  
                             </div>                   
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharFechamento" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlRelatorio" TargetControlID="lkRelatorios" CancelControlID="btnFecharNovaInvoice"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlRelatorio" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Relação dos Contratos Firmados</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                       <div class="col-sm-offset-3 col-sm-3">
                                    <div class="form-group">
                                           <label class="control-label">Fechamento Inicial:</label>
                                                                       <asp:TextBox ID="txtIDInvoice" runat="server" CssClass="form-control data"></asp:TextBox>

                                    </div>
                                           </div>
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                           <label class="control-label">Fechamento Final:</label>
                                                 <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control data"></asp:TextBox>

                                    </div>
                                        </div>
                                         </div>

                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNovaInvoice" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNovaInvoice" text="Imprimir" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                              <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlNovoFechamento" TargetControlID="lkNovoFechamento" CancelControlID="btnFecharNovaInvoice"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlNovoFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">NOVO FECHAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                           <label class="control-label">ID:</label>
                                                                       <asp:TextBox ID="TextBox5" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                           </div>
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>

                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Emissor:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Processo ou BL:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox7" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Vencimento:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox8" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>   
                                                                        <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo Fatura:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Número Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox9" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox10" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                            <label class="control-label"></label>

                                                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Conferido"></asp:CheckBox>

                                    </div>
                                        </div>
                                         </div>   
                                                                                                                                    
                                                            <div class="row">
                                     
                                      
                                        <div class="col-sm-10">
                                    <div class="form-group">
                                          <label class="control-label">Observações:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox11" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                               <asp:Button runat="server" Text="Gravar" ID="Button3" CssClass="btn btn-success btn-block" />


                                    </div>
                                        </div>
                                         </div>   
<div class="row">
                                     
                                      
                                       <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="DEVOLUÇÃO DE FRETE" ID="Button4" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS EXTERIOR" ID="Button5" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS DECLARADAS" ID="Button6" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="COMISSOES" ID="Button7" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="OUTRAS TAXAS" ID="Button9" CssClass="btn btn-block btnn" />


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
                                                                          <asp:linkButton runat="server" Text="ABRIR CONFERENCIA" ID="LinkButton1" CssClass="btn btn-primary" href="Conferencia.aspx" target="_blank"/>

                                           </div>
                                                                <div class="col-sm-offset-4 col-sm-4">
                                                                    TOTAL DA INVOICE:0,00
                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="Button10" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button11" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlBaixaFechamento" TargetControlID="lkNovoFechamento" CancelControlID="btnFecharNovaInvoice"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBaixaFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BAIXAR FECHAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                           <label class="control-label">ID:</label>
                                                                       <asp:TextBox ID="TextBox3" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                           </div>
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>

                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Emissor:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


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
                                                <asp:TextBox ID="TextBox4" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Vencimento:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox6" runat="server"  CssClass="form-control"></asp:TextBox>


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
                                                <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Número Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox12" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox13" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                            <label class="control-label"></label>

                                                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Conferido"></asp:CheckBox>

                                    </div>
                                        </div>
                                         </div>   
                                                                                                                                    
                                                            <div class="row">
                                     
                                      
                                        <div class="col-sm-10">
                                    <div class="form-group">
                                          <label class="control-label">Observações:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox14" runat="server"  CssClass="form-control"></asp:TextBox>


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
                               <asp:Button runat="server" Text="DEVOLUÇÃO DE FRETE" ID="Button8" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS EXTERIOR" ID="Button12" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS DECLARADAS" ID="Button13" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="COMISSOES" ID="Button14" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="OUTRAS TAXAS" ID="Button15" CssClass="btn btn-block btnn" />


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
                                                                          <asp:linkButton runat="server" Text="ABRIR CONFERENCIA" ID="LinkButton2" CssClass="btn btn-primary" href="Conferencia.aspx" target="_blank"/>

                                           </div>
                                                                <div class="col-sm-offset-4 col-sm-4">
                                                                    TOTAL DA INVOICE:0,00
                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="Button16" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button17" text="Salvar" />

                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="pnlCancelarFechamento" TargetControlID="lkNovoFechamento" CancelControlID="btnFecharNovaInvoice"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlCancelarFechamento" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">NOVO FECHAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                   <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                           <label class="control-label">ID:</label>
                                                                       <asp:TextBox ID="TextBox15" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                           </div>
                                     <div class="col-sm-8">
                                    <div class="form-group">
                                           <label class="control-label">Agente:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>

                                    </div>
                                        </div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Emissor:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList13" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Processo ou BL:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox16" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Vencimento:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox17" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Moeda:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList14" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                         </div>   
                                                                        <div class="row">
                                     
                                       <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label">Tipo Fatura:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="DropDownList15" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList> 


                                    </div>
                                        </div>
                                        <div class="col-sm-5">
                                    <div class="form-group">
                                           <label class="control-label">Número Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox18" runat="server"  CssClass="form-control"></asp:TextBox>

                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label">Data Invoice:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox19" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">
                                            <label class="control-label"></label>

                                                                        <asp:CheckBox ID="CheckBox3" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Conferido"></asp:CheckBox>

                                    </div>
                                        </div>
                                         </div>   
                                                                                                                                    
                                                            <div class="row">
                                     
                                      
                                        <div class="col-sm-10">
                                    <div class="form-group">
                                          <label class="control-label">Observações:</label><label runat="server" style="color: red">*</label>
                                                <asp:TextBox ID="TextBox20" runat="server"  CssClass="form-control"></asp:TextBox>


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                               <asp:Button runat="server" Text="Gravar" ID="Button18" CssClass="btn btn-success btn-block" />


                                    </div>
                                        </div>
                                         </div>   
<div class="row">
                                     
                                      
                                       <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="DEVOLUÇÃO DE FRETE" ID="Button19" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS EXTERIOR" ID="Button20" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-3">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="TAXAS DECLARADAS" ID="Button21" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="COMISSOES" ID="Button22" CssClass="btn btn-block btnn" />


                                    </div>
                                        </div>
    <div class="col-sm-2">
                                    <div class="form-group">
                               <asp:Button runat="server" Text="OUTRAS TAXAS" ID="Button23" CssClass="btn btn-block btnn" />


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
                                                                          <asp:linkButton runat="server" Text="ABRIR CONFERENCIA" ID="LinkButton3" CssClass="btn btn-primary" href="Conferencia.aspx" target="_blank"/>

                                           </div>
                                                                <div class="col-sm-offset-4 col-sm-4">
                                                                    TOTAL DA INVOICE:0,00
                                           </div>
                                                                </div>
                                </div>  
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="Button24" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="Button25" text="Salvar" />

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
