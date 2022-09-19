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
                        <br />
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
                                    <asp:LinkButton ID="lkInserir" runat="server" CssClass="btn  btnn btn-primary btn-sm" Style="font-size: 15px" >Exportar CSV</asp:LinkButton>
                                </div>
                                <br />
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
                                                    <asp:ListItem Value="9">LAnçamento</asp:ListItem>
                                                    <asp:ListItem Value="10">Histórico</asp:ListItem>
                                                </asp:DropDownList>


                                        </div>
                                    </div>
                                     <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label"></label>
                                            <asp:TextBox  ID="txtFiltro" runat="server" cssclass="form-control" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Inicial(Processo):</label>
                                            <asp:TextBox  ID="txtDtInicial" runat="server" cssclass="form-control" TextMode="Date"  ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Data Final(Processo):</label>
                                            <asp:TextBox  ID="txtDtFinal" runat="server" cssclass="form-control" TextMode="Date"  ></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                   <div class="col-sm-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="Button2" CssClass="btn btn-block btn-primary" Text="Consultar"/>
                                           </div>
                                </div><div class="col-sm-1">
                                    <div class="form-group">
                                <asp:Button runat="server" ID="Button1" CssClass="btn btn-block btn-primary" Text="Filtro Avançado" />   </div>
                                </div><div class="col-sm-1">
                                    <div class="form-group">
                                <asp:Button runat="server" ID="Button3"  CssClass="btn btn-block btn-primary" Text="Limpar Campos" />
                                    </div>
                                </div> 
                                  </div>
                                

                                <ajaxToolkit:ModalPopupExtender ID="mpeStatus" runat="server" PopupControlID="Panel2" TargetControlID="Button1" CancelControlID="btnFecharStatus"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Historico de Status</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> <div class="col-sm-12"> <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">

                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharStatus" text="Close" />                                                                
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtServico" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtEstufagem" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtNumeroCotacao" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
                                    <asp:GridView ID="dgvCotacao" DataKeyNames="ID_COTACAO" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsCotacao" AutoGenerateColumns="False" Style="max-height: 600px; overflow: auto;" AllowSorting="True"  EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="100">
                                        <Columns>
                                             <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Button ID="btnMarcarTudo" runat="server" CssClass="btn btn-warning" Text="Marcar/Desmarcar todos" />                                      
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" Visible="False" runat="server" Text='<%# Eval("ID_COTACAO") %>' />                                                         
                                        <asp:CheckBox ID="ckbSelecionar" runat="server" CssClass="ChkBoxClass" />
                                    </ItemTemplate>
                                </asp:TemplateField>                                            
                                            <asp:BoundField DataField="ID_COTACAO" HeaderText="#" Visible="false" />
											<asp:BoundField DataField="DT_ABERTURA" HeaderText="Abertura" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_ABERTURA" />
                                            <asp:BoundField DataField="NR_COTACAO" HeaderText="Nº Cotação" SortExpression="NR_COTACAO" />
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnStatus" runat="server" CssClass="btn-default"
                                                        CommandArgument='<%# Eval("ID_COTACAO") & "|" & Container.DataItemIndex %>' CommandName="Status" OnClientClick="SalvaPosicao()">
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' Style="width: 100px; padding: 8px; text-align: center" />
                                                        <asp:Label ID="lblCor" runat="server" Text='<%# Eval("COR") %>' Visible="false" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
										    <asp:BoundField DataField="NR_PROCESSO_GERADO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO_GERADO" />
                                            <asp:BoundField DataField="CLIENTE" HeaderText="Cliente" SortExpression="CLIENTE" /> 
                                            <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />                              
                                            <asp:BoundField DataField="INCOTERM" HeaderText="Incoterm" SortExpression="INCOTERM" />
                                            <asp:BoundField DataField="ORIGEM" HeaderText="Origem" SortExpression="ORIGEM" />
                                            <asp:BoundField DataField="DESTINO" HeaderText="Destino" SortExpression="DESTINO" />  
                                            <asp:BoundField DataField="SERVICO" HeaderText="Serviço" SortExpression="SERVICO" />
											<asp:BoundField DataField="CLIENTE_FINAL" HeaderText="Cliente Final" SortExpression="CLIENTE_FINAL" />
                                            <asp:BoundField DataField="AGENTE" HeaderText="Agente" SortExpression="AGENTE" />
                                            <asp:BoundField DataField="ARMADOR" HeaderText="Armador" SortExpression="ARMADOR" />
                                            <asp:BoundField DataField="ANALISTA_COTACAO_INSIDE" HeaderText="Analista Inside" SortExpression="ANALISTA_COTACAO_INSIDE" />
                                            <asp:BoundField DataField="ANALISTA_COTACAO_PRICING" HeaderText="Analista Pricing" SortExpression="ANALISTA_COTACAO_PRICING" />
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="lblAprovadas" runat="server"></asp:Label><br />
                                <asp:Label ID="lblRejeitadas" runat="server"></asp:Label>

                            </ContentTemplate>
                            <Triggers>
                                 <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCotacao" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCotacao" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
      <asp:SqlDataSource ID="dsCotacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT top 500 * FROM View_Filtro_Cotacao ORDER BY ID_COTACAO DESC"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
