<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Financeiro.aspx.vb" Inherits="NVOCC.Web.Financeiro" %>

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
                    <h3 class="panel-title">FINANCEIRO
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
                                   <asp:ListItem Value="1">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="2">Número do Master</asp:ListItem>
                                   <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                   <asp:ListItem Value="4">Referência  do Cliente</asp:ListItem>
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
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>
                       <div class="col-sm-offset-2 col-sm-4">
                           <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />

                           <asp:LinkButton ID="lkContasPagar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Contas a Pagar</asp:LinkButton>
                           <asp:LinkButton ID="lkContasReceber" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Contas a Receber</asp:LinkButton>
                           <asp:LinkButton ID="lkIntegracao" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Integração TOTVS</asp:LinkButton>
                           <asp:LinkButton ID="lkIndicadores" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Indicadores</asp:LinkButton>

                       </div>
                   </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="Button1" CssClass="btn btn-success" />

                                <ajaxToolkit:ModalPopupExtender ID="mpeImprimir" runat="server" PopupControlID="pnlContasPagar" TargetControlID="lkContasPagar" CancelControlID="btnFecharContasPagar"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlContasPagar" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CONTAS A PAGAR</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                           
<asp:LinkButton ID="lkSolicitacaoPagamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Solicitação Pagamento</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                             
                                                                                <asp:LinkButton ID="lkMontagemPagamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Montagem Pagamento</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                                                                     <asp:LinkButton ID="lkBaixaCancel_Pagar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Baixas e Cancel</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharContasPagar" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlContasReceber" TargetControlID="lkContasReceber" CancelControlID="btnFecharContasReceber"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlContasReceber" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CONTAS A RECEBER</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkCalcularRecebimento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Calcular Recebimento</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                 <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkEmissaoND" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Emissão ND</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                             
                                                   <asp:LinkButton ID="lkBaixaCancel_Receber" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Baixas e Cancel</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                                                                                                             <asp:LinkButton ID="lkFaturar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Faturar Recebimento</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharContasReceber" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlIntegracao" TargetControlID="lkIntegracao" CancelControlID="btnFecharIntegracao"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlIntegracao" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">INTEGRAÇÃO TOTVS</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                           
                                        <asp:LinkButton ID="lkNotaDespesa" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Nota Despesa</asp:LinkButton>


                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                             
                                                                                  <asp:LinkButton ID="lkPA" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">PA</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>      
                                    
                                </div>  
                             </div>
                           
                      
                                                       
                   
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharIntegracao" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlIndicadores" TargetControlID="lkIndicadores" CancelControlID="btnFecharIndicadores"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlIndicadores" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">INDICADORES</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                            <div class="row">                                  
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                                                                  <asp:LinkButton ID="lkInternacional" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Internacional</asp:LinkButton>

                                        </div>
                                         </div>
                                   </div>      
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                         <asp:LinkButton ID="lkNacional" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Nacional</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                           
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharIndicadores" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="lkFiltrar" />
             <asp:PostBackTrigger ControlID="lkImprimir" />
                   <asp:PostBackTrigger ControlID="btnImprimir" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
                                    <asp:GridView ID="dgvFinanceiro" DataKeyNames="ID_BL" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFinanceiro" AutoGenerateColumns="False" Style="max-height: 600px; overflow: auto;" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="ID_BL" HeaderText="#" Visible="false" />
                                            <asp:BoundField DataField="NR_BL" HeaderText="MBL" SortExpression="NR_BL" />
                                            <asp:BoundField DataField="NM_PARCEIRO_CLIENTE" HeaderText="Cliente" SortExpression="NM_PARCEIRO_CLIENTE" />
                                            <asp:BoundField DataField="NM_PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="NM_PARCEIRO_TRANSPORTADOR" />
                                            <asp:BoundField DataField="TOTAL_A_PAGAR" HeaderText="Qtd. taxas a Pagar" SortExpression="TOTAL_A_PAGAR" />
                                            <asp:BoundField DataField="TOTAL_A_PAGAR_QUITADAS" HeaderText="Qtd. taxas Pagas" SortExpression="TOTAL_A_PAGAR_QUITADAS" />
                                            <asp:BoundField DataField="TOTAL_A_PAGAR_ABERTAS" HeaderText="Qtd. taxas (a Pagar) em aberto" SortExpression="TOTAL_A_PAGAR_ABERTAS" />
                                            <asp:BoundField DataField="TOTAL_A_PAGAR_CANCELADAS" HeaderText="Qtd. taxas (a Pagar) Canceladas" SortExpression="TOTAL_A_PAGAR_CANCELADAS" />
                                            <asp:BoundField DataField="TOTAL_A_RECEBER" HeaderText="Qtd. taxas a Receber" SortExpression="TOTAL_A_RECEBER" />
                                            <asp:BoundField DataField="TOTAL_A_RECEBER_QUITADAS" HeaderText="Qtd. Quantidade taxas Recebidas" SortExpression="TOTAL_A_RECEBER_QUITADAS" />
                                            <asp:BoundField DataField="TOTAL_A_RECEBER_ABERTAS" HeaderText="Quantidade taxas (a Receber) em aberto" SortExpression="TOTAL_A_RECEBER_ABERTAS" />
                                            <asp:BoundField DataField="TOTAL_A_RECEBER_CANCELADAS" HeaderText="Qtd. taxas (a Receber) Canceladas" SortExpression="TOTAL_A_RECEBER_CANCELADAS" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"  OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="lblAprovadas" runat="server"></asp:Label><br />
                                <asp:Label ID="lblRejeitadas" runat="server"></asp:Label>

                            </ContentTemplate>
                            <Triggers>
                                <%--       <asp:AsyncPostBackTrigger ControlID="bntPesquisar" />
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCotacao" />
       <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCotacao" />
       <asp:AsyncPostBackTrigger ControlID="lkCalcular" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>

    <asp:SqlDataSource ID="dsFinanceiro" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Financeiro] WHERE TOTAL_A_PAGAR_ABERTAS > 0 OR TOTAL_A_RECEBER_ABERTAS > 0 ORDER BY NR_PROCESSO"></asp:SqlDataSource>
                          <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
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
    </script>
</asp:Content>
