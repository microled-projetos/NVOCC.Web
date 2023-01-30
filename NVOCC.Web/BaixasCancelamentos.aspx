<%@ Page Title="" Language="vb" enableEventValidation="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BaixasCancelamentos.aspx.vb" Inherits="NVOCC.Web.BaixasCancelamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
         #imgFundo {
            display: none;
        }
          th {
    color: #337ab7;
}
          .ImageButton{
              padding-left:25px;
              padding-right:25px;
          }
          
         </style>
       <div style="float:right; display:none" > <a id="ajuda" href="#" title="Ajuda" ><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
  <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z"/>
</svg></a></div>

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
                                <asp:TextBox ID="txtID" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    
                                                                            <div class="col-sm-4">

                                        <div class="form-group"><br />
                                            <asp:Button runat="server" Text="Gerar CSV" ID="btnCSV" CssClass="btn btn-info" />
                                            <asp:Button runat="server" Text="Atualizar Cambio" ID="btnCambio" CssClass="btn btn-success" Visible="true" />
                                            <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Cancelar Baixa" ID="btnCancelarBaixa" CssClass="btn btn-warning" OnClientClick="javascript:return confirm('Deseja realmente cancelar a baixa deste registro?');"/>
                                            <asp:Button runat="server" Text="Cancelar Conta Corrente" ID="btnCancelar" CssClass="btn btn-danger" />
                                        </div>
                                    </div>

                                </div>


                                <br />
                                <br />
                                <div class="row">
                                    <div runat="server" id="gridPagar">
                                        <div class="DivGridPagar table-responsive tableFixHead" id="DivGridPagar">
                                        <asp:GridView ID="dgvTaxasPagar" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsPagar" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true" OnClick="SalvaPosicaoPagamento()" />
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
                                                <asp:BoundField DataField="VL_LANCAMENTO" HeaderText="Valor lançamento(R$)" SortExpression="VL_LANCAMENTO" />                                    
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário lançamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                                 
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                            </div>
                                    </div>
                                    <div runat="server" id="gridReceber">
                                        <div class="DivGridReceber table-responsive tableFixHead" id="DivGridReceber">
                                        <asp:GridView ID="dgvTaxasReceber" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsReceber" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true" OnClick="SalvaPosicaoRecebimento()"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº Processo" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProcesso" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                        <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_BL") %>' VISIBLE="false" />
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
                                                <asp:BoundField DataField="NM_TIPO_FATURAMENTO" HeaderText="Tipo de Faturamento" SortExpression="NM_TIPO_FATURAMENTO" />
                                            <asp:BoundField DataField="QT_DIAS_FATURAMENTO" HeaderText="Qtd. Dias" SortExpression="QT_DIAS_FATURAMENTO" />
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFornecedor" runat="server" Text='<%# Eval("NM_PARCEIRO_EMPRESA") %>' />
                                                        <asp:Label ID="lblID_PARCEIRO_EMPRESA" runat="server" Text='<%# Eval("ID_PARCEIRO_EMPRESA") %>' VISIBLE="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VL_LANCAMENTO" HeaderText="Valor lançamento(R$)" SortExpression="VL_LANCAMENTO" />
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário lançamento" SortExpression="NOME_USUARIO_LANCAMENTO" />                                               

                                                <asp:TemplateField HeaderText="Bloqueio Financeiro">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblFL_BLOQUEIO_FINANCEIRO" runat="server" Text='<%# Eval("FL_BLOQUEIO_FINANCEIRO") %>' />
                                                 <asp:ImageButton ID="btnBloquearFinanceiro" runat="server" ToolTip="bloquear" CssClass="ImageButton" src="Content/imagens/bloquear.png" CommandName="BloquearFinanceiro" CommandArgument='<%# Eval("ID_BL") %>' />
                                                 <asp:ImageButton ID="btnDesbloquearFinanceiro" runat="server" ToolTip="desbloquear" CssClass="ImageButton" src="Content/imagens/desbloquear.png" CommandName="DesbloquearFinanceiro" CommandArgument='<%# Eval("ID_BL") %>' />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>






                                                <asp:TemplateField HeaderText="Bloqueio Documental">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFL_BLOQUEIO_DOCUMENTAL" runat="server" Text='<%# Eval("FL_BLOQUEIO_DOCUMENTAL") %>'  />
                                                 <asp:ImageButton ID="btnBloquearDocumental" ToolTip="bloquear" runat="server" CssClass="ImageButton" src="Content/imagens/bloquear.png" CommandName="BloquearDocumental" CommandArgument='<%# Eval("ID_BL") %>' />
                                                 <asp:ImageButton ID="btnDesbloquearDocumental" runat="server" ToolTip="desbloquear" CssClass="ImageButton" src="Content/imagens/desbloquear.png" CommandName="DesbloquearDocumental" CommandArgument='<%# Eval("ID_BL") %>' />                                              
                                            </ItemTemplate>
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

                                <asp:Button runat="server" CssClass="btn btn-success" ID="Button1" Style="display: none;" />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="PanelCambio" TargetControlID="Button1" CancelControlID="btnFecharCambio"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PanelCambio" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">ATUALIZAÇÃO DE CAMBIO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                            <h5>
                                                <asp:label runat="server" ID="lblProcessoCambio"  />
                                                <asp:label runat="server" ID="lblFaturaCambio"  />                                          
                                            <asp:label runat="server" ID="lblClienteCambio" /></h5>
                        
                                          
                                                            <div class="row">
                                        <div class="col-sm-offset-2 col-sm-2" runat="server">

                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Data Cambio:</label>
                                            <asp:TextBox ID="txtDataCambio" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                            </div>
                                         <div class="col-sm-3" runat="server">
                                            <div class="form-group">
                                            <label class="control-label" style="text-align: left">Valor Cambio:</label>
                                            <asp:TextBox ID="txtValorCambio" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                       <div class="col-sm-3" runat="server">

                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Moeda:</label>
                                            <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                        </div>
                                            </div>
                                            </div>                </div>
                                       
                                                                                       
                                                                        
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCambio" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnAtualizaCambio" text="Atualizar Fatura" />
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
                                <asp:PostBackTrigger ControlID="btnCSV" /> 
                                <asp:PostBackTrigger ControlID="btnpesquisar" /> 
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="TextBox1" Style="display:none" Text="0" runat="server"></asp:TextBox>
    <asp:SqlDataSource ID="dsPagar" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'P' AND DT_LIQUIDACAO IS NULL ORDER BY DT_VENCIMENTO DESC, NR_FATURA_FORNECEDOR"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'R' AND DT_LIQUIDACAO IS NULL ORDER BY DT_VENCIMENTO DESC"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script type="text/javascript">
         function SalvaPosicaoPagamento() {
             var posicao = document.getElementById('DivGridPagar').scrollTop;
             if (posicao) {
                 document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('if:' + posicao);

             }
            else {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('else:' + posicao);

             }
         };

         function SalvaPosicaoRecebimento() {
             var posicao = document.getElementById('DivGridReceber').scrollTop;
            
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
            if (valor != null) {
                console.log('entrou:' + valor);
                if (document.getElementById('DivGridReceber')) {
                    document.getElementById('DivGridReceber').scrollTop = valor;
                }
                if (document.getElementById('DivGridPagar')) {
                    document.getElementById('DivGridPagar').scrollTop = valor;
                }                
            }
           
        };
     </script>
</asp:Content>