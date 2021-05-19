﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Faturamento.aspx.vb" Inherits="NVOCC.Web.Faturamento" %>

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
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Abertos</asp:ListItem>
                                   <asp:ListItem Value="2">&nbsp;Fechados</asp:ListItem>
                                   <asp:ListItem Value="3">&nbsp;Cancelados</asp:ListItem>
                               </asp:CheckBoxList>
                           </div>
                       </div>
                       <div class="col-sm-1" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                           </div>
                       </div>

                       <div class="col-sm-offset-2 col-sm-4">
                           <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />

                           <asp:LinkButton ID="lkBaixarFatura" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Baixar Fatura</asp:LinkButton>
                           <asp:LinkButton ID="lkCancelamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Cancelar Fatura</asp:LinkButton>
                           <asp:LinkButton ID="lkDesmosntrativos" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Demonstrativos</asp:LinkButton>
                           <asp:LinkButton ID="lkNotasFiscais" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Notas Ficais</asp:LinkButton>

                       </div>
                   </div>
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead">
                                    <asp:GridView ID="dgvFaturamento" DataKeyNames="ID_FATURAMENTO" DataSourceID="dsFaturamento" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_FATURAMENTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                            <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="Cliente" SortExpression="PARCEIRO_EMPRESA" />
                                            <asp:BoundField DataField="REFERENCIA_CLIENTE" HeaderText="Ref. Cliente" SortExpression="REFERENCIA_CLIENTE" />
                                            <asp:BoundField DataField="VL_NOTA_DEBITO" HeaderText="Valor Nota de Deb." SortExpression="VL_NOTA_DEBITO" />
                                            <asp:BoundField DataField="NR_NOTA_DEBITO" HeaderText="Nota de Debito." SortExpression="NR_NOTA_DEBITO" />
                                            <asp:BoundField DataField="DT_NOTA_DEBITO" HeaderText="Data de Nota de Deb." SortExpression="DT_NOTA_DEBITO" />
                                            <asp:BoundField DataField="NR_RPS" HeaderText="Nota RPS" SortExpression="NR_RPS" />
                                            <asp:BoundField DataField="DT_RPS" HeaderText="Data Nota RPS" SortExpression="DT_RPS" />
                                            <asp:BoundField DataField="NR_RECIBO" HeaderText="Nota de Recibo" SortExpression="NR_RECIBO" />
                                            <asp:BoundField DataField="DT_RECIBO" HeaderText="Data Nota de Deb." SortExpression="DT_RECIBO" />
                                            <asp:BoundField DataField="NR_NOTA_FISCAL" HeaderText="Nota Fiscal" SortExpression="NR_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="Data Nota Fiscal" SortExpression="DT_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="Data de Liquidação" SortExpression="DT_LIQUIDACAO" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_FATURAMENTO") & "|" & Container.DataItemIndex %>'  CommandName="Selecionar" Text="Selecionar"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>



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
                                                                        <h5>  <asp:label runat="server" ID="lblFaturaBaixa" text="xxx" />
                                           <asp:label runat="server" ID="lblProcessoBaixa" text="xxx" />                                          
                                           <asp:label runat="server" ID="lblClienteBaixa" text="xxx" /></h5>
                                         </div>                          
                                                                                        <div class="row">                        
                                           <h3>CONFIRMAÇÃO DA LIQUIDAÇÃO</h3>                           
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
                                                            <h5 class="modal-title">OBSERVAÇÃO DE CANCELAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                   
                                            <h5><asp:label runat="server" ID="lblFaturaCancelamento" />                       
                                            <asp:label runat="server" ID="lblProcessoCancelamento" />
                                            <asp:label runat="server" ID="lblClienteCancelamento" /></h5>
                        
                                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                        
                                         </div>
                                  
                           
                      
                                                       
                                                                        
                               <div class="modal-footer">
                                                            
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarCancelamento" text="Salvar" OnClientClick="javascript:return confirm('Deseja realmente cancelar esta fatura?');"/>
                                   <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCancelamento" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlDesmosntrativos" TargetControlID="lkDesmosntrativos" CancelControlID="btnFecharDesmosntrativos"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlDesmosntrativos" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">DEMONSTRATIVOS</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                           
<asp:LinkButton ID="lkSolicitacaoPagamento" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Nota de Débito</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                             
                                                                                <asp:LinkButton ID="lkMontagemPagamento" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Recibo Provisório de Serviço</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                                                                     <asp:LinkButton ID="lkBaixaCancel_Pagar" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Recibo de Pagamento</asp:LinkButton>
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

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFaturamento" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFiltro" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>

    <asp:SqlDataSource ID="dsFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Faturamento] WHERE DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL ORDER BY DT_VENCIMENTO,NR_PROCESSO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
