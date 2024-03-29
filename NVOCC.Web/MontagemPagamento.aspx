﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MontagemPagamento.aspx.vb" Inherits="NVOCC.Web.MontagemPagamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }
    </style>
     <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">MONTAGEM DE PAGAMENTO

                </h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <asp:Label runat="server" ID="lbl_ISS" CssClass="control-label" Style="display: none" />
                                <asp:TextBox ID="txtCancelaOperacao" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <br />

                                <div class="row linhabotao text-center" style="margin-left: 20px;border: ridge 1px;">
                                        <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">VENCIMENTO:</label>
                                                <asp:TextBox ID="txtVencimentoBusca" placeholder="__/__/____" runat="server" autopostback="true" CssClass="form-control data" ></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="col-sm-4">
                                            <div class="form-group">
                                                <label class="control-label">FORNECEDOR:</label>
                                                <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsFornecedor" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">Nº MASTER:</label>
                                                <asp:TextBox ID="txtMaster" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">Nº HOUSE:</label>
                                                <asp:TextBox ID="txtHouse" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">Nº PROCESSO:</label>
                                                <asp:TextBox ID="txtProcesso" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                        
                                    </div>
                                </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnMontar" /> 
                                <asp:PostBackTrigger ControlID="txtVencimentoBusca" /> 
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>

                                <br />
                                <br />
                                <div id="DivGrid1"  runat="server" class="DivGrid">
                                <div class="row" >
                                    <div id="DivGrid" class="DivGrid table-responsive tableFixHead">
                                        <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL,ID_BL_TAXA" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true" OnClick="SalvaPosicao()"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao"  />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                                <asp:BoundField DataField="NR_BL_MASTER" HeaderText="MBL" SortExpression="NR_BL_MASTER" />

                                                <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Fornecedor/Cliente" SortExpression="NM_PARCEIRO_EMPRESA" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" />
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                
                                                <asp:TemplateField HeaderText="Abater ISS" HeaderStyle-ForeColor="#4e81ad">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbISS" runat="server" autoPostBack="true" OnClick="SalvaPosicao()" />
                                                    </ItemTemplate>                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA_BR") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                </div>
                                <div class="row">
                                <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Marcar Todos" ID="btnMarcar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Desmarcar Todos" ID="btnDesmarcar" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="border: ridge 1px;">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">NÚMERO DA FATURA:</label><br />
                                           <asp:TextBox ID="txtNumeroFatura" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">DATA DA FATURA:</label>
                                                <asp:TextBox ID="txtDataFatura" runat="server" placeholder="__/__/____" CssClass="form-control data" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">DATA DE VENCIMENTO:</label>
                                                <asp:TextBox ID="txtVencimento" runat="server" placeholder="__/__/____" CssClass="form-control data" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">CONTA BANCARIA:</label>
                                                 <asp:DropDownList ID="ddlContaBancaria" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTA_BANCARIA" DataSourceID="dsContaBancaria" DataValueField="ID_CONTA_BANCARIA"></asp:DropDownList>
                                            </div>
                                     </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left">VALOR:</label>
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                    </div>
                                    <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left"></label>
                                                <asp:Checkbox ID="ckbBaixaAutomatica" runat="server" CssClass="form-control" Checked="true" text="&nbsp;&nbsp;Baixar Automaticamente"></asp:Checkbox>
                                            </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Montar Pagamento" ID="btnMontar" CssClass="btn btn-success" onClientclick="ConfirmaValorZerado();" />
                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" CssClass="btn btn-danger" />
                                        </div>
                                    </div>
                                </div>
                                </div>
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" /> 
                                <asp:AsyncPostBackTrigger ControlID="btnMontar" />     

                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
                           <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>

    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE CD_PR= 'P' AND ID_PARCEIRO_EMPRESA = @ID_PARCEIRO_EMPRESA AND DT_SOLICITACAO_PAGAMENTO = CONVERT(DATE, @DATA, 103) ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO_EMPRESA" Type="Int32" ControlID="ddlFornecedor" />
            <asp:ControlParameter Name="DATA" Type="string" ControlID="txtVencimentoBusca" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND DT_SOLICITACAO_PAGAMENTO IS NOT NULL) )
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">        
         <SelectParameters>
            <asp:ControlParameter Name="DATA" Type="string" ControlID="txtVencimentoBusca" />
                     </SelectParameters>

     </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsContaBancaria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CONTA_BANCARIA, NM_CONTA_BANCARIA FROM [dbo].[TB_CONTA_BANCARIA] WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_CONTA_BANCARIA] ORDER BY ID_CONTA_BANCARIA">        
     </asp:SqlDataSource>
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


         function ConfirmaValorZerado() {

             var txtvalor = document.getElementById('<%= txtValor.ClientID %>').value;
             console.log("valorzerado:" + txtvalor);
             if (txtvalor == "" || txtvalor == "0" || txtvalor == "R$ 0,00" || txtvalor == "R$ 0" || txtvalor == "0,00") {
                 var retorno = confirm("Valor zerado. Deseja continuar assim mesmo?");
                 if (retorno == true) {
                     console.log("Operação confirmada");
                     document.getElementById('<%= txtCancelaOperacao.ClientID %>').value = 0;
                 }
                 else {
                     console.log("Você cancelou a operação");
                     document.getElementById('<%= txtCancelaOperacao.ClientID %>').value = 1;
                 }
             }
             else {
                     console.log("Nem entrou no if");
                     document.getElementById('<%= txtCancelaOperacao.ClientID %>').value = 0;
             }     
            
         };
     </script> 
</asp:Content>
