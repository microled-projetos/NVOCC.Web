﻿<%@ Page Title="" Language="vb" EnableEventValidation="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BaixasCancelamentosRecebimento.aspx.vb" Inherits="NVOCC.Web.BaixasCancelamentosRecebimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }

        th {
            color: #337ab7;
        }

        .ImageButton {
            padding-left: 25px;
            padding-right: 25px;
        }
    </style>
    <div style="float: right; display: none">
        <a id="ajuda" href="#" title="Ajuda">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
                <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z" />
            </svg></a>
    </div>

    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">BAIXAS E CANCELAMENTOS - CONTAS A RECEBER</h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <asp:TextBox ID="txtID" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtMsgLimite" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtItemDespesa" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLimiteBaixa" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <br />

                                <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px; border: ridge 1px;">

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
                                            <asp:RadioButtonList ID="rdStatus" runat="server" Style="margin-top: 5%; margin-left: 30%; font-size: 12px; text-align: justify">
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

                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Gerar CSV" ID="btnCSV" CssClass="btn btn-info" />
                                            <asp:Button runat="server" Text="Atualizar Cambio" ID="btnCambio" CssClass="btn btn-success" />
                                            <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Cancelar Baixa" ID="btnCancelarBaixa" CssClass="btn btn-warning" OnClientClick="javascript:return confirm('Deseja realmente cancelar a baixa deste registro?');" />
                                            <asp:Button runat="server" Text="Cancelar Conta Corrente" ID="btnCancelar" CssClass="btn btn-danger" />
                                        </div>
                                    </div>

                                </div>


                                <br />
                                <br />
                                <div class="row">
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
                                                            <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true" OnClick="SalvaPosicaoRecebimento()" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nº Processo" SortExpression="NM_PARCEIRO_EMPRESA">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcesso" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                            <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_BL") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NM_TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="NM_TIPO_ESTUFAGEM" />

                                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                                    <asp:TemplateField HeaderText="Liquidação" SortExpression="DT_LIQUIDACAO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLiquidacao" runat="server" Text='<%# Eval("DT_LIQUIDACAO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DT_ENVIO_FATURAMENTO" HeaderText="Envio ao Faturamento" SortExpression="DT_ENVIO_FATURAMENTO" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="NM_TIPO_FATURAMENTO" HeaderText="Tipo de Faturamento" SortExpression="NM_TIPO_FATURAMENTO" />
                                                    <asp:BoundField DataField="QT_DIAS_FATURAMENTO" HeaderText="Qtd. Dias" SortExpression="QT_DIAS_FATURAMENTO" />
                                                    <asp:TemplateField HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFornecedor" runat="server" Text='<%# Eval("NM_PARCEIRO_EMPRESA") %>' />
                                                            <asp:Label ID="lblID_PARCEIRO_EMPRESA" runat="server" Text='<%# Eval("ID_PARCEIRO_EMPRESA") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor lançamento(R$)" SortExpression="VL_LANCAMENTO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValorLancamento" runat="server" Text='<%# Eval("VL_LANCAMENTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                    <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Acréscimo" SortExpression="VL_ACRESCIMO" />
                                                    <asp:BoundField DataField="VL_DECRESCIMO" HeaderText="Decréscimo" SortExpression="VL_DECRESCIMO" />
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
                                                            <asp:Label ID="lblFL_BLOQUEIO_DOCUMENTAL" runat="server" Text='<%# Eval("FL_BLOQUEIO_DOCUMENTAL") %>' />
                                                            <asp:ImageButton ID="btnBloquearDocumental" ToolTip="bloquear" runat="server" CssClass="ImageButton" src="Content/imagens/bloquear.png" CommandName="BloquearDocumental" CommandArgument='<%# Eval("ID_BL") %>' />
                                                            <asp:ImageButton ID="btnDesbloquearDocumental" runat="server" ToolTip="desbloquear" CssClass="ImageButton" src="Content/imagens/desbloquear.png" CommandName="DesbloquearDocumental" CommandArgument='<%# Eval("ID_BL") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>





                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID_PARCEIRO_ARMAZEM_DESCARGA" runat="server" Text='<%# Eval("ID_PARCEIRO_ARMAZEM_DESCARGA") %>' />
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
                                   
                                            <h5>              
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
                                <asp:Button runat="server" CssClass="btn btn-success" ID="Button3" text="" Style="display:none" />
                                 <asp:Button runat="server" CssClass="btn btn-success" ID="Button2" text="" Style="display:none" />
                                <ajaxToolkit:ModalPopupExtender ID="mpeBaixa" runat="server" PopupControlID="Panel2" TargetControlID="Button3" CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BAIXA</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                            <h5>
                                             <asp:label runat="server" ID="lblClienteBaixa" /></h5>
                        
                                        <div class="row">
                                         <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Nº Processo:</label>
                                            <asp:TextBox ID="txtProcessoBaixa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Valor:</label>
                                            <asp:TextBox ID="txtValorBaixa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Valor Liquidado:</label>
                                            <asp:TextBox ID="txtValorLiquidadoBaixa" runat="server" CssClass="form-control teste" ></asp:TextBox>
                                        </div>
                                    </div>
                                            <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Acréscimo/Decréscimo:</label>
                                            <asp:TextBox ID="txtDiferencaBaixa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                            <div class="col-sm-3">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Data Liquidação:</label>
                                            <asp:TextBox ID="txtDataBaixa" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                            </div>               
                                                             <div class="row">
                                         <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Motivo:</label>
                                            <asp:TextBox ID="txtMotivoBaixa" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>
                                                </div>         </div>
                                       
                                                                                       
                                                                        
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBaixa" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarBaixa" text="Baixar Fatura"  OnClientClick="MouseWait();"/>
                                                        </div>
                                                  </div>    
                                                </div>
      
                                         </center>
                                </asp:Panel>

                                <asp:Button runat="server" CssClass="btn btn-success" ID="Button1" Style="display: none;" />
                                <ajaxToolkit:ModalPopupExtender ID="mpeCambio" runat="server" PopupControlID="PanelCambio" TargetControlID="Button1" CancelControlID="btnFecharCambio"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PanelCambio" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">ATUALIZAÇÃO DE CAMBIO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                            <h5>
                                                <asp:label runat="server" ID="lblProcessoCambio"  />
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
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasReceber" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxasReceber" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                <asp:AsyncPostBackTrigger ControlID="btnFecharBaixa" />
                                <asp:PostBackTrigger ControlID="btnCSV" />
                                <asp:PostBackTrigger ControlID="btnpesquisar" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="TextBox1" Style="display: none" Text="0" runat="server"></asp:TextBox>
   
    <asp:SqlDataSource ID="dsReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos_R] WHERE CD_PR =  'R' AND DT_LIQUIDACAO IS NULL ORDER BY DT_VENCIMENTO DESC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">

        function MouseWait() {
            document.body.style.cursor = "wait";
        };

        function MouseDefault() {
            console.log("default");
            document.body.style.cursor = "default";
        };

        function VariosProcessosSelecionados() {
            console.log("default");
            document.body.style.cursor = "default";
            alert('Não é permitida a seleção de mais de um processo!');

        };

        function SalvaPosicaoRecebimento() {
            MouseWait();
            <%--var posicao = document.getElementById('DivGridReceber').scrollTop;

            if (posicao) {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                 console.log('if:' + posicao);

             }
             else {
                 document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }--%>
        };

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= TextBox1.ClientID %>').value;
            if (valor != null) {
                console.log('entrou:' + valor);
                if (document.getElementById('DivGridReceber')) {
                    document.getElementById('DivGridReceber').scrollTop = valor;
                }
            }

            CalculaDiferenca();
            TextChange();
        };

        function CalculaDiferenca() {
            var btn = document.getElementById('<%= btnSalvarBaixa.ClientID %>');
            var LimiteBaixa = document.getElementById('<%= txtLimiteBaixa.ClientID %>').value;
            var MsgLimite = document.getElementById('<%= txtMsgLimite.ClientID %>').value;
            var Valor = document.getElementById('<%= txtValorLiquidadoBaixa.ClientID %>').value.replace('R$', "");
            if (Valor != null) {
 
                var ValorFormatado = Valor;
                ValorFormatado = ValorFormatado.replace('.', "").replace(',', ".");
                ValorFormatado = Number(ValorFormatado).toFixed(2);
                ValorFormatado = parseFloat(ValorFormatado);
                ValorFormatado = ValorFormatado.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });

                var Liquidado = document.getElementById('<%= txtValorBaixa.ClientID %>').value.replace('R$', "");

                var Diferenca = parseFloat(Valor.replace('.', "").replace(',', ".")) - parseFloat(Liquidado.replace('.', "").replace(',', "."))

                if (Math.abs(parseFloat(Diferenca)) > Math.abs(parseFloat(LimiteBaixa))) {
                     if (MsgLimite == 1) {
                     alert('Você ultrapassou os limites de valores para esse campo. Favor inserir valores em até R$ ' + LimiteBaixa + ' do valor original!');
                        btn.disabled = true;
                     }
                }
                else {
                    btn.disabled = false;
                }

<%--                var DiferencaAbs = Diferenca;
                 DiferencaAbs = Number(parseFloat(DiferencaAbs)).toFixed(2);
                console.log("DiferencaAbs: " + DiferencaAbs );

                document.getElementById('<%= lblDescontoAcrescimoBaixa.ClientID %>').innerHTML = DiferencaAbs;
                console.log("lblDescontoAcrescimoBaixa: " + document.getElementById('<%= lblDescontoAcrescimoBaixa.ClientID %>').innerHTML);--%>
                document.getElementById('<%= txtDiferencaBaixa.ClientID %>').value = Diferenca.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
                document.getElementById('<%= txtValorLiquidadoBaixa.ClientID %>').value = ValorFormatado.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
            }
        };

        
        function TextChange() {
            var Valor = document.getElementById('<%= txtValorLiquidadoBaixa.ClientID %>').value;
            if (Valor != null) {
                 $(".teste").blur(function () {
                    console.log("Change detected!");
                     var id = document.getElementById('<%= txtID.ClientID %>').value;
                     if (id != "") {
                         CalculaDiferenca();
                     }
                });
                
            }
        }


    </script>
</asp:Content>
