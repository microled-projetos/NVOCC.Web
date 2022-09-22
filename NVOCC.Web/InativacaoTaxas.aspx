<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InativacaoTaxas.aspx.vb" Inherits="NVOCC.Web.InativacaoTaxas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">INATIVAÇÃO/ATIVAÇÃO DE TAXAS
                        <asp:Label ID="lblteste" runat="server"></asp:Label>
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
                            <br />
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
                                            <asp:ListItem Value="2">Item Despesa</asp:ListItem>
                                            <asp:ListItem Value="3">Parceiro Vinculado</asp:ListItem>
                                            <asp:ListItem Value="4">Valor Taxa</asp:ListItem>
                                            <asp:ListItem Value="5">Valor Taxa Calculada</asp:ListItem>
                                            <asp:ListItem Value="6">Moeda</asp:ListItem>
                                            <asp:ListItem Value="7">Tipo Movimento</asp:ListItem>
                                            <asp:ListItem Value="8">Origem Pagamento</asp:ListItem>
                                            <asp:ListItem Value="9">Lançamento</asp:ListItem>
                                            <asp:ListItem Value="10">Histórico</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Data Inicial(Processo):</label>
                                        <asp:TextBox ID="txtDtInicial" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Data Final(Processo):</label>
                                        <asp:TextBox ID="txtDtFinal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
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
                                        <asp:Button runat="server" ID="btnLimparCampos" CssClass="btn btn-block btn-primary" Text="Limpar Campos" />
                                    </div>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="txtCont" Text="0" CssClass="form-control" Style="display: none" />

                            <asp:Button runat="server" ID="Button1" CssClass="btn btn-block btn-primary" Style="display: none" />
                            <ajaxToolkit:ModalPopupExtender ID="mpeStatus" runat="server" PopupControlID="Panel2" TargetControlID="Button1" CancelControlID="btnFecharStatus"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
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

                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharStatus" text="Close" />                                                                
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                            </asp:Panel>



                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlConfirmar" TargetControlID="btnGravar" CancelControlID="btnFecharConfirmacao"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlConfirmar" runat="server" CssClass="modalPopup" Style="display: none;">
                                <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Confirmar</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> 
                                                                   <div class="col-sm-offset-2 col-sm-8">
                                                                       <label class="control-label">Tipo Motivo: </label>
                                                                   <asp:DropDownList ID="ddlMotivos" runat="server" CssClass="form-control" Font-Size="15px"></asp:DropDownList>
                             </div>

                                                               </div>
                                                            <div class="row"> 
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

                            <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary" Text="Gravar Ação" />

                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                        <ContentTemplate>
                            <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL_TAXA" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsTaxas" AutoGenerateColumns="False" Style="max-height: 500px; overflow: auto;" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="100">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Button ID="btnMarcarTudo" runat="server" Font-Size="Small" CssClass="btn btn-warning" Text="Marcar/Desmarcar todos" OnClick="btnMarcarTudo_Click" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckbSelecionar" runat="server" CssClass="ChkBoxClass" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº PROCESSO" SortExpression="NR_PROCESSO" />
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
                                                <asp:Label ID="lblTemHistorico" runat="server" Text='<%# Eval("HISTORICO") %>' Visible="false"></asp:Label>
                                                <asp:ImageButton ID="ImageButton1" src="Content/imagens/hist.png" runat="server" CommandArgument='<%# Eval("ID_BL_TAXA") %>' ToolTip="Histórico" CommandName="Historico" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:Label ID="lblID" Visible="False" runat="server" Text='<%# Eval("ID_BL_TAXA") %>' />
                                            </ItemTemplate>
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
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,NR_PROCESSO,NM_PARCEIRO_EMPRESA,NM_ITEM_DESPESA,SIGLA_MOEDA,NM_ORIGEM_PAGAMENTO,VL_TAXA,VL_TAXA_CALCULADO, VL_TAXA_BR,LANCAMENTO,TIPO_MOVIMENTO,HISTORICO FROM [dbo].[View_Inativacao_Taxas] WHERE ISNULL(ID_BL_TAXA,0) <> 0 ORDER BY ID_BL_TAXA DESC"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
