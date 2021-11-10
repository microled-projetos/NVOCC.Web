<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BaixasCancelamentos.aspx.vb" Inherits="NVOCC.Web.BaixasCancelamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
         #imgFundo {
            display: none;
        }
         </style>
    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">BAIXAS E CANCELAMENTOS -
                    <asp:Label runat="server" ID="lblTipo" /></h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>

                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <br />


                                <div class="row linhabotao text-center" style="margin-left: 20px;margin-right: 20px;border: ridge 1px;">
                                    
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Vencimento Inicial:</label>
                                            <asp:TextBox ID="txtVencimentoInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Vencimento Final:</label>
                                            <asp:TextBox ID="txtVencimentoFinal" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">

                           <div class="form-group">
                                   <asp:RadioButtonList ID="rdStatus" runat="server" Style="margin-top:5%;margin-left: 30%; font-size: 12px; text-align: justify">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Abertos</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Fechados</asp:ListItem>
                                                    </asp:RadioButtonList>      
                           </div>
                       </div>
                                    <div class="col-sm-2" runat="server">
                                        <div class="form-group">
                                            <br />
                                                                                        <asp:Button runat="server" Text="Pesquisar Fatura" ID="btnpesquisar" CssClass="btn btn-success" />

                                        </div>
                                    </div>
                                    
                                                                            <div class="col-sm-offset-4 col-sm-2">

                                        <div class="form-group">
                                            <br />
                                                                                        <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixar" CssClass="btn btn-primary" />

                                            <asp:Button runat="server" Text="Cancelar Fatura" ID="btnCancelar" CssClass="btn btn-danger" />

                                        </div>
                                    </div>

                                </div>


                                <br />
                                <br />
                                <div class="row">
                                    <div class="table-responsive tableFixHead" runat="server" id="gridPagar">
                                        <asp:GridView ID="dgvTaxasPagar" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsPagar" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº Fatura" SortExpression="NR_FATURA_FORNECEDOR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatura" runat="server" Text='<%# Eval("NR_FATURA_FORNECEDOR") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Liquidação" SortExpression="Liquidação">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLiquidacao" runat="server" Text='<%# Eval("DT_LIQUIDACAO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFornecedor" runat="server" Text='<%# Eval("NM_PARCEIRO_EMPRESA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor lançamento(R$)" SortExpression="VL_TAXA_BR" />                                    
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário laçamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                    <div class="table-responsive tableFixHead" runat="server" id="gridReceber">
                                        <asp:GridView ID="dgvTaxasReceber" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsReceber" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server"  AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº Processo" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProcesso" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:BoundField DataField="NM_TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="NM_TIPO_ESTUFAGEM" />

                                                <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                                <asp:TemplateField HeaderText="Liquidação" SortExpression="Liquidação">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLiquidacao" runat="server" Text='<%# Eval("DT_LIQUIDACAO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:BoundField DataField="DT_ENVIO_FATURAMENTO" HeaderText="Envio ao Faturamento" SortExpression="DT_ENVIO_FATURAMENTO" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFornecedor" runat="server" Text='<%# Eval("NM_PARCEIRO_EMPRESA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor lançamento(R$)" SortExpression="VL_TAXA_BR" />
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário laçamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                                  <asp:BoundField DataField="BLOQUEADO" HeaderText="Bloqueado" SortExpression="BLOQUEADO" />
                                                        <asp:TemplateField HeaderText=""  >
                                            <ItemTemplate>
                                                <asp:linkButton ID="btnBloquear" title="bloquear" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="bloquear"
                                OnClientClick="javascript:return confirm('Deseja realmente bloquear este parceiro?');"  CommandArgument='<%# Eval("ID_BL") %>' Autopostback="true" ><i class="glyphicon glyphicon-ban-circle"></i></span></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=""  >
                                            <ItemTemplate>
                                                <asp:linkButton ID="btnDesbloquear" title="desbloquear" runat="server"  CssClass="btn btn-success btn-sm" CommandName="desbloquear"
                                OnClientClick="javascript:return confirm('Deseja realmente desbloquear este parceiro?');"  CommandArgument='<%# Eval("ID_BL") %>' Autopostback="true" ><i class="glyphicon glyphicon-ok"></i></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_PARCEIRO_ARMAZEM_DESCARGA" runat="server" Text='<%# Eval("ID_PARCEIRO_ARMAZEM_DESCARGA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                </div>

                                <ajaxToolkit:ModalPopupExtender ID="mpeObs" runat="server" PopupControlID="Panel1" TargetControlID="btnCancelar" CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none;">
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
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarCancelamento" text="Salvar" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>





                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="btnBaixar" CancelControlID="btnFecharBaixa"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BAIXA</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                            <h5>
                                                <asp:label runat="server" ID="lblFaturaBaixa"  />
                                                <asp:label runat="server" ID="lblProcessoBaixa"  />                                          
                                            <asp:label runat="server" ID="lblClienteBaixa" /></h5>
                        
                                           <h5>CONFIRMAÇÃO DA LIQUIDAÇÃO</h5>
                                                            <div class="row">
                                        <div class="col-sm-offset-5 col-sm-2" runat="server">

                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Data Liquidação:</label>
                                            <asp:TextBox ID="txtData" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                            </div>                </div>
                                       
                                                                                       
                                                                        
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBaixa" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarBaixa" text="Baixar Fatura" />
                                                        </div>
                                                  </div>    
                                                </div>
      
                                         </center>
                                </asp:Panel>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasPagar" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasReceber" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxasPagar" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxasReceber" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsPagar" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'P' AND DT_LIQUIDACAO IS NULL ORDER BY DT_VENCIMENTO DESC, NR_FATURA_FORNECEDOR"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'R' AND DT_LIQUIDACAO IS NULL ORDER BY DT_VENCIMENTO DESC"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>