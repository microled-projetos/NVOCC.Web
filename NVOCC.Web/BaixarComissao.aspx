<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BaixarComissao.aspx.vb" Inherits="NVOCC.Web.BaixarComissao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }
        #imgFundo { 
display:none; 

}
    </style>
       <div style="float:right; display:none" > <a id="ajuda" href="#" title="Ajuda" ><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
  <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z"/>
</svg></a></div>
        <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">BAIXA PAGAMENTO DE INDICADOR
                    <asp:Label runat="server" ID="lblTipo" /></h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>

                                <asp:TextBox ID="txtID" Style="display:none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinha" Style="display:none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblContador" Style="display:none" runat="server"></asp:Label>


                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <br />


                                <div class="row linhabotao text-center" style="border: ridge 1px;">
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Competencia:</label>
                                            <asp:TextBox ID="txtCompetencia" runat="server" placeholder="___/______" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Quinzena:</label>
                                            <asp:TextBox ID="txtQuinzena" runat="server"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-1" style="padding-top: 10px;">

                           <div class="form-group">

                               <asp:CheckBoxList ID="ckStatus" Style="padding: 0px; font-size: 12px; text-align: justify" runat="server" RepeatDirection="vertical">
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Abertos</asp:ListItem>
                                   <asp:ListItem Value="2">&nbsp;Fechados</asp:ListItem>
                               </asp:CheckBoxList>
                           </div>
                       </div>--%>
                                                                   <div class="col-sm-1" runat="server">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success btn-block" />
                                        </div>
                                    </div>     
                                    <%--<div class="col-sm-offset-6 col-sm-2" runat="server">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixar" CssClass="btn btn-success" />

                                        </div>
                                    </div>--%>

                                </div>


                                <br />
                                <br />
                                    <div class="table-responsive tableFixHead" runat="server" id="gridPagar" visible="false">
                                        <asp:GridView ID="dgvTaxasPagar" DataKeyNames="ID_CABECALHO_COMISSAO_INTERNACIONAL" DataSourceID="dsPagar" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="COMPETENCIA" HeaderText="COMPETÊNCIA" SortExpression="COMPETENCIA" />
                                                <asp:BoundField DataField="NR_QUINZENA" HeaderText="QUINZENA" SortExpression="NR_QUINZENA" />
                                                <asp:BoundField DataField="DT_GERACAO" HeaderText="GERACAO" SortExpression="DT_GERACAO" />
                                                <asp:BoundField DataField="NOME_USUARIO_GERACAO" HeaderText="USUARIO GERACAO" SortExpression="NOME_USUARIO_GERACAO" />                                                
                                               <asp:BoundField DataField="DT_EXPORTACAO" HeaderText="EXPORTACAO" SortExpression="DT_EXPORTACAO" />                                                         
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_CABECALHO_COMISSAO_INTERNACIONAL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Baixar"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                    

                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  style="display: none;"></asp:TextBox>
                  
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="TextBox1" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BAIXA</h5>
                                                        </div>
                                                        <div class="modal-body">  
                                                            
                                                            <div class="alert alert-success" id="divSuccessBaixa" runat="server" visible="false">
                                    <asp:Label ID="lblSuccessBaixa" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErroBaixa" runat="server" visible="false">
                                    <asp:Label ID="lblErroBaixa" runat="server"></asp:Label>
                                </div>
                                 <div class="alert alert-warning" id="divInfo" runat="server" visible="false">
                                    <asp:Label ID="lblmsgInfo" runat="server"></asp:Label>
                                </div>
                                            <h5>
                                                <asp:label runat="server" ID="lblCompetencia"  />
                                                <asp:label runat="server" ID="lblQuinzena"  /></h5>                                        
                                      
                                                        <div class="row">
                                                            <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">ID</label>
                                            <asp:TextBox ID="txtIDBaixa" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                            <div class="col-sm-3">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Liquidação:</label>
                                            <asp:TextBox ID="txtLiquidacao" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                                            <div class="col-sm-3">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Contrato:</label>
                                            <asp:TextBox ID="txtContrato" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                            <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">Conta Bancária:</label> 
                               <asp:DropDownList ID="ddlContaBancaria" runat="server" CssClass="form-control" DataTextField="NM_CONTA_BANCARIA" DataSourceID="dsContaBancaria" DataValueField="ID_CONTA_BANCARIA"/>
                                        </div>
                                    </div>
                                             </div>
                                                        <div class="row">    
                                                            <div class="col-sm-12">
                                                                <asp:GridView ID="dgvMoedas" DataKeyNames="ID_MOEDA" DataSourceID="dsMoeda" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com data de câmbio atual.">
                                            <Columns>                                               
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoeda" runat="server" Text='<%# Eval("ID_MOEDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                 <asp:TemplateField HeaderText="Valor Câmbio" SortExpression="" >
                                                    <ItemTemplate>
                                                        <asp:Textbox ID="txtValorCambio" runat="server"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
</div>
                                                                        </div>               
                                                          </div>                
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBaixa" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarBaixa" text="Baixar" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasPagar" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxasPagar" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-ajuda">
   <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Sobre NVOCC:</h4>
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

    <asp:SqlDataSource ID="dsPagar" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Comissao_Internacional] WHERE DT_EXPORTACAO IS NULL"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA,NM_MOEDA FROM TB_MOEDA ">
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsContaBancaria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CONTA_BANCARIA,NM_CONTA_BANCARIA FROM TB_CONTA_BANCARIA WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione'  ORDER BY ID_CONTA_BANCARIA"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script>

         $('#ajuda').on("click", function () {
             $('#modal-ajuda').modal('show');
         });
    </script>
</asp:Content>