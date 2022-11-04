<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InativacaoTaxas.aspx.vb" Inherits="NVOCC.Web.InativacaoTaxas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .Historico {
            text-align: center;
        }
    </style>
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">INATIVAÇÃO/ATIVAÇÃO DE TAXAS
                        <asp:Label ID="lblteste" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                            <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                            <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                        </div>
                        <div class="row linhabotao text-center">
                            <asp:LinkButton ID="lkExportarCSV" runat="server" CssClass="btn  btnn btn-primary btn-sm" Style="font-size: 15px">Exportar CSV</asp:LinkButton>
                        </div>
                        <div class="row flexdiv topMarg" style="padding: 0 15px">
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label">Filtro</label>
                                    <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                        <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                        <asp:ListItem Value="1">Número processo</asp:ListItem>
                                        <asp:ListItem Value="11">Número BL</asp:ListItem>
                                        <asp:ListItem Value="2">Item Despesa</asp:ListItem>
                                        <asp:ListItem Value="3">Parceiro Vinculado</asp:ListItem>
                                        <asp:ListItem Value="4">Valor Taxa</asp:ListItem>
                                        <asp:ListItem Value="5">Valor Taxa Calculada</asp:ListItem>
                                        <asp:ListItem Value="6">Moeda</asp:ListItem>
                                        <asp:ListItem Value="7">Tipo Movimento</asp:ListItem>
                                        <asp:ListItem Value="8">Origem Pagamento</asp:ListItem>
                                        <asp:ListItem Value="9">Lançamento</asp:ListItem>
                                        <%-- <asp:ListItem Value="10">Histórico</asp:ListItem>--%>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label">Data Inicial(Embarque):</label>
                                    <asp:TextBox ID="txtDtInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label">Data Final(Embarque):</label>
                                    <asp:TextBox ID="txtDtFinal" runat="server" CssClass="form-control data"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-block btn-primary" Text="Consultar" />
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnFiltroAvancado" CssClass="btn btn-block btn-primary" Text="Filtro Avançado" />
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnLimparCampos" CssClass="btn btn-block btn-primary" Text="Limpar Filtros" />
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:CheckBox ID="ckAtivo" runat="server" AutoPostBack="true" Text="&nbsp;&nbsp;Ativo" Font-Size="Medium" Checked="true" />
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:CheckBox ID="ckInativo" runat="server" AutoPostBack="true" Text="&nbsp;&nbsp;Inativo" Font-Size="Medium" />
                                </div>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="txtCont" Text="0" CssClass="form-control" Style="display: none" />
                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-block btn-primary" Style="display: none" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeHistorico" runat="server" PopupControlID="pnHistorico" TargetControlID="Button1" CancelControlID="btnFecharHistorico"></ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnHistorico" runat="server" CssClass="modalPopup" Style="display: none;">
                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Historico de Status</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-12"> 
                                                                       <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">
                                                                            <asp:GridView ID="dgvHistorico" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="ID_INATIVACAO" DataSourceID="dsHistorico" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_INATIVACAO" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                 <asp:BoundField DataField="STATUS" HeaderText="Ativo?" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NOME" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="DT_INATIVACAO" HeaderText="Data" ItemStyle-HorizontalAlign="Center" />
                                                                                 <asp:BoundField DataField="NM_MOTIVO_INATIVACAO" HeaderText="Motivo" ItemStyle-HorizontalAlign="Center" />
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="Historico" />
                                                                        </asp:GridView>

                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharHistorico" text="Close" />                                                                
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                        </asp:Panel>



                        <ajaxToolkit:ModalPopupExtender ID="mpeConfirmacao" runat="server" PopupControlID="pnlConfirmar" TargetControlID="btnGravar" CancelControlID="btnFecharConfirmacao"></ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlConfirmar" runat="server" CssClass="modalPopup" Style="display: none;">
                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Confirmação de Inativação/Ativação</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-offset-2 col-sm-8">
                                                                       <label class="control-label">Tipo Motivo: </label>
                                                                   <asp:DropDownList ID="ddlMotivos" runat="server" CssClass="form-control" Font-Size="15px"  DataTextField="NM_MOTIVO_INATIVACAO" DataSourceID="dsMotivoInativacao" AutoPostBack="true" DataValueField="ID_MOTIVO_INATIVACAO"></asp:DropDownList>
                             </div>

                                                               </div>
                                                            <div class="row" id="divDescMotivo" runat="server" visible="false"> 
                                                                <div class="col-sm-offset-2 col-sm-8"> 
                                                                             <label class="control-label">Motivo:</label>

                                                <asp:TextBox runat="server" cssclass="form-control" ID="txtMotivo" TextMode="MultiLine" />                   
                                       </div>        
                                              </div>    
                                                  </div>          
                               <div class="modal-footer">
                  <asp:Button runat="server" CssClass="btn btn-success" ID="btnConfirmaGravacao" text="Gravar" />  
                                                                                               <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharConfirmacao" text="Close" />              
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                        </asp:Panel>


                        <ajaxToolkit:ModalPopupExtender ID="mpeFiltro" runat="server" PopupControlID="pnlFiltro" TargetControlID="btnFiltroAvancado" CancelControlID="btnFecharFiltro"></ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlFiltro" runat="server" CssClass="modalPopup" Style="display: none;">
                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Filtro Avançado</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Nº Processo:</label>
                                                                    <asp:TextBox ID="txtFiltroProcesso" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Item Despesa:</label>
                                                                   <asp:TextBox ID="txtFiltroDespesa" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Parceiro Vinculado:</label>
                                                                   <asp:TextBox ID="txtFiltroParceiro" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div>

                                                               </div>
                                                            <div class="row"> 
                                                                   <div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Valor Taxa:</label>
                                                                   <asp:TextBox ID="txtFiltroValor" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Valor Taxa Calculada:</label>
                                                                   <asp:TextBox ID="txtFiltroValorCalculada" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Moeda:</label>
                                                                   <asp:TextBox ID="txtFiltroMoeda" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div>

                                                               </div>
<div class="row"> 
                                                                   <div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Tipo Movimento:</label>
                                                                   <asp:TextBox ID="txtFiltroMovimento" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Origem Pagamento:</label>
                                                                   <asp:TextBox ID="txtFiltroOrigemPagamento" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Lançamento:</label>
                                                                   <asp:TextBox ID="txtFiltroLancamento" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div>

                                                               </div>
<div class="row"> 
                                                                   <div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Data Inicial(Processo):</label>
                                                                   <asp:TextBox ID="txtFiltroDataInicial" runat="server" CssClass="form-control data"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Data Final(Processo):</label>
                                                                   <asp:TextBox ID="txtFiltroDataFinal" runat="server" CssClass="form-control data"></asp:TextBox></div>
                             </div><div class="col-sm-4"> <div class="form-group">
                                                                       <label class="control-label">Nº BL:</label>
                                                                   <asp:TextBox ID="txtBLFiltro" runat="server" CssClass="form-control"></asp:TextBox></div>
                             </div>

                                                               </div>
                                                  </div>          
                               <div class="modal-footer">
                  <asp:Button runat="server" CssClass="btn btn-primary" ID="btnConsultaAvancada" text="Consultar" />  
                                                                                               <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharFiltroAvancado" text="Sair" />              
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                        </asp:Panel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div class="row table-responsive tableFixHead" style="max-height: 600px;">
                                    <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL_TAXA" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsTaxas" AutoGenerateColumns="False" Style="max-height: 300px; overflow: auto;" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="100">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Button ID="btnMarcarTudo" runat="server" Font-Size="Small" CssClass="btn btn-warning" Text="Marcar/Desmarcar todos" OnClick="btnMarcarTudo_Click" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckbSelecionar" runat="server" CssClass="ChkBoxClass" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Historico" />

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_EMBARQUE" HeaderText="EMBARQUE" SortExpression="DT_EMBARQUE" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº PROCESSO" SortExpression="NR_PROCESSO" />
                                            <asp:BoundField DataField="NR_BL" HeaderText="Nº BL" SortExpression="NR_BL" />
                                            <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="ITEM DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                            <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="PARCEIRO VINCULADO" SortExpression="NM_PARCEIRO_EMPRESA" />
                                            <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR TAXA" SortExpression="VL_TAXA" />
                                            <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR TAXA CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                            <asp:BoundField DataField="VL_TAXA_BR" HeaderText="VALOR TAXA BR" SortExpression="VL_TAXA_BR" />
                                            <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />
                                            <asp:BoundField DataField="TIPO_MOVIMENTO" HeaderText="TIPO MOVIMENTO" SortExpression="TIPO_MOVIMENTO" />
                                            <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM PAGAMENTO" SortExpression="NM_ORIGEM_PAGAMENTO" />
                                            <asp:BoundField DataField="LANCAMENTO" HeaderText="LANÇAMENTO" SortExpression="LANCAMENTO" />
                                            <asp:TemplateField HeaderText="HISTÓRICO" SortExpression="HISTORICO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrau" runat="server" Text='<%# Eval("GRAU") %>' Visible="false"></asp:Label>
													<asp:Label ID="lblTemHistorico" runat="server" Text='<%# Eval("HISTORICO") %>' Visible="false"></asp:Label>
                                                    <asp:ImageButton ID="ImageButton1" src="Content/imagens/hist.png" runat="server" CommandArgument='<%# Eval("ID_BL_TAXA") %>' ToolTip="Histórico" CommandName="Historico" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:Label ID="lblTaxa" Visible="False" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Historico" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-sm-1">
                                <div class="form-group">
                                    <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-block btn-primary" Text="Gravar Ação" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlMotivos" />
                        <asp:PostBackTrigger ControlID="lkExportarCSV" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,NR_PROCESSO,NR_BL,NM_PARCEIRO_EMPRESA,NM_ITEM_DESPESA,SIGLA_MOEDA,NM_ORIGEM_PAGAMENTO,VL_TAXA,VL_TAXA_CALCULADO, VL_TAXA_BR,LANCAMENTO,TIPO_MOVIMENTO,HISTORICO,DT_EMBARQUE,GRAU FROM [dbo].[View_Inativacao_Taxas] WHERE ISNULL(ID_BL_TAXA,0) <> 0 ORDER BY ID_BL_TAXA DESC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMotivoInativacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOTIVO_INATIVACAO,NM_MOTIVO_INATIVACAO FROM TB_MOTIVO_INATIVACAO
        union SELECT  0, '      Selecione' ORDER BY ID_MOTIVO_INATIVACAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsHistorico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INATIVACAO,CASE WHEN ISNULL(FL_TAXA_INATIVA,0) = 0 THEN 'ATIVO' ELSE 'INATIVO' END STATUS,NOME,DT_INATIVACAO,CASE WHEN ISNULL(C.FL_PRECISA_DESCR,0) = 1 THEN
C.NM_MOTIVO_INATIVACAO + ': ' +A.DS_MOTIVO_INATIVACAO ELSE C.NM_MOTIVO_INATIVACAO END NM_MOTIVO_INATIVACAO,A.DS_MOTIVO_INATIVACAO FROM TB_INATIVACAO_TAXAS A INNER JOIN TB_USUARIO B ON A.ID_USUARIO_INATIVACAO = B.ID_USUARIO INNER JOIN TB_MOTIVO_INATIVACAO C ON C.ID_MOTIVO_INATIVACAO = A.ID_MOTIVO_INATIVACAO WHERE A.ID_BL_TAXA = @ID_BL_TAXA ORDER BY DT_INATIVACAO DESC">
        <SelectParameters>
            <asp:Parameter Name="ID_BL_TAXA" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
