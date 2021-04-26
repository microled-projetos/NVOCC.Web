<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SolicitacaoPagamento.aspx.vb" Inherits="NVOCC.Web.SolicitacaoPagamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">SOLICITAÇÃO DE PAGAMENTO
                    </h3>
                </div>
                <div class="panel-body">
                   
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="Embarque">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>

                                    <asp:TextBox ID="txtID_Embarque" Style="display: none" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                                    <asp:TextBox ID="txtLinhaEmbarque" Style="display: none" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>

                                    <div class="alert alert-success" id="divSuccessEmbarque" runat="server" visible="false">
                                        <asp:Label ID="lblSuccessEmbarque" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-danger" id="divErroEmbarque" runat="server" visible="false">
                                        <asp:Label ID="lblErroEmbarque" runat="server"></asp:Label>
                                    </div>
                                    <br />
                                    
                                    <div class="row linhabotao text-center" style="margin-left: 20px">
                                        <div class="col-sm-2" style="border: ridge 1px;">
                                                <div class="form-group" Style="margin-bottom:18px">
                                                    <label class="control-label">NÚMERO MASTER:</label><br />
                                                     <asp:label  runat="server" CssClass="control-label">NÚMERO MASTER</asp:label>
                                                </div>
                                            </div>
                                        <div>
                                            <div class="col-sm-2" style="border: ridge 1px;">
                                                <div class="form-group">
                                                    <label class="control-label">FORNECEDOR:</label>
                                                    <asp:DropDownList ID="ddlFiltroEmbarque" AutoPostBack="true" Width="230px" runat="server" CssClass="form-control" Font-Size="15px">
                                                        <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                        <asp:ListItem Value="1">Número do processo</asp:ListItem>
                                                        <asp:ListItem Value="2">Tipo de Estufagem</asp:ListItem>
                                                        <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                                        <%--       <asp:ListItem Value="4">Nº BL Master</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2" style="border: ridge 1px;">
                                                <div class="form-group">
                                                    <label class="control-label" style="text-align:left">VENCIMENTO:</label>
                                                    <asp:TextBox ID="txtPesquisaEmbarque" runat="server" placeholder="__/__/____" CssClass="form-control data" Width="210px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-1" style="border: ridge 1px;">

                                                <div class="form-group"><br />
                                                    <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisaEmbarque" CssClass="btn btn-success" />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <br />
                                    <br />

                                    <div class="table-responsive tableFixHead">
                                        <asp:GridView ID="dgvEmbarque" DataKeyNames="ID_BL" DataSourceID="dsEmbarque" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="ID_BL" HeaderText="#" SortExpression="ID_BL" visible="false"/>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="Cliente" SortExpression="PARCEIRO_CLIENTE" />
                                                <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                                <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="PARCEIRO_AGENTE_INTERNACIONAL" HeaderText="Agente Internacional" SortExpression="PARCEIRO_AGENTE_INTERNACIONAL" />                                              
                                                <asp:BoundField DataField="PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="PARCEIRO_TRANSPORTADOR" />                                              
                                                <asp:BoundField DataField="NR_CE" HeaderText="CE" SortExpression="NR_CE" />                                                
                                                <asp:BoundField DataField="WEEK" HeaderText="Week" SortExpression="WEEK" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                   <%-- <ajaxToolkit:ModalPopupExtender id="mpe_Embarque" runat="server" PopupControlID="Panel1" TargetControlID="lkFiltrarEmbarque" ></ajaxToolkit:ModalPopupExtender>--%>

                                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Filtro Avançado - Embarque</h5>
                                                        </div>
                                                <div class="modal-body">
                                                     <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                                                <asp:Label ID="lblmsgSuccess" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                                                <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Agente:</label>
                                                                        <asp:TextBox ID="txtAgente_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Exportador:</label>
                                                                        <asp:TextBox ID="txtExportador_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Transportador:</label>
                                                                        <asp:TextBox ID="txtTransportador_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cliente:</label>
                                                                        <asp:TextBox ID="txtCliente_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Ref Cliente:</label>
                                                                        <asp:TextBox ID="txtRefCliente_Embarque" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo Estufagem:</label>
                                                                        <asp:TextBox ID="txtEstufagem_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Origem:</label>
                                                                        <asp:TextBox ID="txtOrigem_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Destino:</label>
                                                                        <asp:TextBox ID="txtDestino_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo Frete:</label>
                                                                        <asp:TextBox ID="txtTipoFrete_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>  
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Navio:</label>
                                                                        <asp:TextBox ID="txtNavio_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Navio Transbordo:</label>
                                                                        <asp:TextBox ID="txtNavioTransb_Embarque" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Previsão de Embarque</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtPrevInicialEmbarque_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>
                                                                        <asp:TextBox ID="txtPrevFimEmbarque_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Data de Embarque</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtInicialEmbarque_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>
                                                                        <asp:TextBox ID="txtFimEmbarque_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Previsão de Chegada</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtPrevInicialChegada_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>
                                                                        <asp:TextBox ID="txtPrevFimChegada_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Data de Chegada</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtInicialChegada_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>
                                                                        <asp:TextBox ID="txtFimChegada_Embarque" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Filtrar" ID="btnFiltrar_Embarque" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_Embarque" Text="Close" />

                                                    </div>

                                                </div>
                                                    </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvEmbarque" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFiltrar_Embarque" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFechar_Embarque" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvEmbarque" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPesquisaEmbarque" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        </div>
                    </div>
                </div>
                </div>
     <asp:SqlDataSource ID="dsEmbarque" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM View_Embarque
WHERE GRAU = 'C' and ID_BL_MASTER is null and ID_SERVICO = 1 "></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
