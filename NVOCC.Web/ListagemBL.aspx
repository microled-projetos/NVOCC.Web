<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListagemBL.aspx.vb" Inherits="NVOCC.Web.ListagemBL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12">
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
        </style>
        <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">MÓDULO OPERACIONAL
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#Embarque" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Instrução de Embarque
                            </a>
                        </li>
                        <li>
                            <a href="#Master" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Master
                            </a>
                        </li>
                        <li>
                            <a href="#House" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>House
                            </a>
                        </li>
                    </ul>

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
                                    <div class="row linhabotao text-center">
                                        <asp:LinkButton ID="lkInserirEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
                                        <asp:LinkButton ID="lkAlterarEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                        <asp:LinkButton ID="lkDuplicarEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate"></i>&nbsp;Duplicar</asp:LinkButton>
                                        <asp:LinkButton ID="lkFollowUpEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-list"></i>&nbsp;FollowUp</asp:LinkButton>
                                        <asp:LinkButton Visible="false" ID="lkRemoverEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>
                                        <asp:LinkButton ID="lkFiltrarEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-search"></i>&nbsp;Filtrar</asp:LinkButton>
                                         <asp:LinkButton ID="lkCancelarEmbarque" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-ban-circle"></i>&nbsp;Cancelar</asp:LinkButton>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Label ID="Label4" Style="padding-left: 35px" runat="server">Filtro:</asp:Label>
                                    <div class="row linhabotao text-center" style="margin-left: 20px">

                                        <div>
                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px; padding-top: 20px; padding-bottom: 10px">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlFiltroEmbarque" AutoPostBack="true" Width="230px" runat="server" CssClass="form-control" Font-Size="15px">
                                                        <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Número do processo</asp:ListItem>
                                                        <asp:ListItem Value="2">Tipo de Estufagem</asp:ListItem>
                                                        <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                                        <%--       <asp:ListItem Value="4">Nº BL Master</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-sm-2" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPesquisaEmbarque" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                <div class="form-group">
                                                    <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisaEmbarque" CssClass="btn btn-success" />

                                                </div>
                                            </div>


                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 150px">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label>
                                                    <asp:RadioButtonList ID="rdTRansporteEmbarque" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Marítimo</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Aéreo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <label class="control-label">Serviço:</label>
                                                    <asp:RadioButtonList ID="rdServicoEmbarque" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Importação</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Exportação</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>



                                    <br />
                                    <br />

                                    <div class="table-responsive tableFixHead DivGridEmbarque" id="DivGridEmbarque">
                                                              <asp:TextBox ID="txtPosicaoEmbarque" Style="display:none" runat="server"></asp:TextBox>

                                        <asp:GridView ID="dgvEmbarque" DataKeyNames="ID_BL" DataSourceID="dsEmbarque" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." OnSorting="dgvEmbarque_Sorting">
                                            <Columns>      
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"  OnClientClick="SalvaPosicaoEmbarque()"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID_BL" HeaderText="#" SortExpression="ID_BL" Visible="false" />
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="Cliente" SortExpression="PARCEIRO_CLIENTE" />
                                                <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                                <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="PARCEIRO_AGENTE_INTERNACIONAL" HeaderText="Agente Internacional" SortExpression="PARCEIRO_AGENTE_INTERNACIONAL" />
                                                <asp:BoundField DataField="PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="PARCEIRO_TRANSPORTADOR" />
                                                <asp:BoundField DataField="NR_CE" HeaderText="CE" SortExpression="NR_CE" />
                                                <asp:BoundField DataField="WEEK" HeaderText="Week" SortExpression="WEEK" />
                                        
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                    <ajaxToolkit:ModalPopupExtender ID="mpe_Embarque" runat="server" PopupControlID="Panel1" TargetControlID="lkFiltrarEmbarque"></ajaxToolkit:ModalPopupExtender>

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
                                    <asp:AsyncPostBackTrigger ControlID="rdTransporteEmbarque" />
                                    <asp:AsyncPostBackTrigger ControlID="rdServicoEmbarque" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar_Embarque" />
                                    <asp:AsyncPostBackTrigger ControlID="lkDuplicarEmbarque" />
                                    <asp:AsyncPostBackTrigger ControlID="lkAlterarEmbarque" />
                                    <asp:AsyncPostBackTrigger ControlID="lkFollowUpEmbarque" />
                                    <asp:AsyncPostBackTrigger ControlID="lkCancelarEmbarque" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                        <div class="tab-pane fade" id="Master">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>

                                    <asp:TextBox ID="txtID_Master" Style="display: none" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                                    <asp:TextBox ID="txtLinhaMaster" Style="display: none" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>


                                    <div class="alert alert-success" id="divSuccessMaster" runat="server" visible="false">
                                        <asp:Label ID="lblSuccessMaster" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-danger" id="divErroMaster" runat="server" visible="false">
                                        <asp:Label ID="lblErroMaster" runat="server"></asp:Label>
                                    </div>
                                    <br />
                                    <div class="row linhabotao text-center">
                                        <asp:LinkButton ID="lkInserirMaster" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
                                        <asp:LinkButton ID="lkAlterarMaster" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                        <asp:LinkButton ID="lkDuplicarMaster" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate"></i>&nbsp;Duplicar</asp:LinkButton>
                                        <asp:LinkButton ID="lkTracking" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-map-marker"></i>&nbsp;Tracking</asp:LinkButton>
                                        <asp:LinkButton ID="lkFollowUpMaster" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-list"></i>&nbsp;FollowUp</asp:LinkButton>
                                        <asp:LinkButton ID="lkRemoverMaster" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>

                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Filtro:</asp:Label>
                                    <div class="row linhabotao text-center" style="margin-left: 20px">

                                        <div>
                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px; padding-top: 20px; padding-bottom: 10px">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddFiltroMaster" AutoPostBack="true" Width="230px" runat="server" CssClass="form-control" Font-Size="15px">
                                                        <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                        <asp:ListItem Value="1">Número do Master</asp:ListItem>
                                                        <asp:ListItem Value="2">Tipo de Estufagem</asp:ListItem>
                                                        <asp:ListItem Value="3">Origem</asp:ListItem>
                                                        <asp:ListItem Value="4">Destino</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-sm-2" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPesquisaMaster" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                <div class="form-group">
                                                    <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisaMaster" CssClass="btn btn-success" />

                                                </div>
                                            </div>


                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 150px">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label>
                                                    <asp:RadioButtonList ID="rdTransporteMaster" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Marítimo</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Aéreo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <label class="control-label">Serviço:</label>
                                                    <asp:RadioButtonList ID="rdServicoMaster" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Importação</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Exportação</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>



                                    <br />
                                    <br />


                                    <div runat="server" class="table-responsive tableFixHead DivGridMaster" id="DivGridMaster">
                                        <asp:TextBox ID="txtPosicaoMaster" Style="display:none" runat="server"></asp:TextBox>
                                        <asp:GridView ID="dgvMaster" DataKeyNames="ID_BL" DataSourceID="dsMaster" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." OnSorting="dgvMaster_Sorting">
                                            <Columns>   
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"  OnClientClick="SalvaPosicaoMaster()"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID_BL" HeaderText="#" SortExpression="ID_BL" Visible="false" />
                                                <asp:BoundField DataField="NR_BL" HeaderText="MBL" SortExpression="NR_BL" />
                                                <asp:BoundField DataField="PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="PARCEIRO_TRANSPORTADOR" />
                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="Tipo Frete" SortExpression="TIPO_PAGAMENTO" />
                                                <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                                <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                                <asp:BoundField DataField="DT_PREVISAO_EMBARQUE" HeaderText="Previsão(Embarque)" SortExpression="DT_PREVISAO_EMBARQUE" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_EMBARQUE" HeaderText="Embarque" SortExpression="DT_EMBARQUE" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_PREVISAO_CHEGADA" HeaderText="Previsão(Chegada)" SortExpression="DT_PREVISAO_CHEGADA" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_CHEGADA" HeaderText="Chegada" SortExpression="DT_CHEGADA" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="NAVIO" HeaderText="Navio" SortExpression="NAVIO" />
                                                <asp:BoundField DataField="PARCEIRO_AGENTE" HeaderText="Agente" SortExpression="PARCEIRO_AGENTE" />
                                             
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPesquisaMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="rdTransporteMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="rdServicoMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="lkDuplicarMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="lkAlterarMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="lkFollowUpMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="lkRemoverMaster" />
                                    <asp:AsyncPostBackTrigger ControlID="lkTracking" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                        <div class="tab-pane fade" id="House">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtlinhaHouse" Style="display: none" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                                    <asp:TextBox ID="txtIDHouse" Style="display: none" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>

                                    <div class="alert alert-success" id="divSuccessHouse" runat="server" visible="false">
                                        <asp:Label ID="lblSuccessHouse" runat="server"></asp:Label>
                                    </div>
                                    <div class="alert alert-danger" id="divErroHouse" runat="server" visible="false">
                                        <asp:Label ID="lblErroHouse" runat="server"></asp:Label>
                                    </div>
                                    <br />
                                    <div class="row linhabotao text-center">
                                        <asp:LinkButton ID="lkInserirHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
                                        <asp:LinkButton ID="lkAlterarHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                        <asp:LinkButton ID="lkDuplicarHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate"></i>&nbsp;Duplicar</asp:LinkButton>
                                        <asp:LinkButton Visible="false" ID="lkCancelaHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="icomoon icon-cancel-circle"></i>&nbsp;Cancelar</asp:LinkButton>
                                        <asp:LinkButton Visible="false" ID="lkRemoverHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>
                                        <asp:LinkButton ID="lkFiltrarHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-search"></i>&nbsp;Filtrar</asp:LinkButton>
                                        <asp:LinkButton ID="lkCalcularHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="fa fa-calculator"></i>&nbsp;Calcular</asp:LinkButton>
                                        <asp:LinkButton ID="lkCourrierHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-transfer"></i>&nbsp;Courrier</asp:LinkButton>
                                        <asp:LinkButton ID="lkBLHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="fa fa-file"></i>&nbsp;Emissão BL</asp:LinkButton>
                                        <asp:LinkButton ID="lkFollowUpHouse" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-list"></i>&nbsp;FollowUp</asp:LinkButton>
                                         <asp:LinkButton ID="lkConferir" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-check"></i>&nbsp;Conferir</asp:LinkButton>    
                                        <asp:LinkButton ID="lkTrackingHBL" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="glyphicon glyphicon-map-marker"></i>&nbsp;Tracking HBL</asp:LinkButton>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Label ID="Label9" Style="padding-left: 35px" runat="server">Filtro:</asp:Label>
                                    <div class="row linhabotao text-center" style="margin-left: 20px">

                                        <div>
                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px; padding-top: 20px; padding-bottom: 10px">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlFiltroHouse" AutoPostBack="true" Width="230px" runat="server" CssClass="form-control" Font-Size="15px">
                                                        <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Número do processo</asp:ListItem>
                                                        <asp:ListItem Value="2">Tipo de Estufagem</asp:ListItem>
                                                        <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                                        <asp:ListItem Value="4">Nº BL Master</asp:ListItem>
                                                        <asp:ListItem Value="5">Nº HBL</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-sm-2" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPesquisaHouse" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                <div class="form-group">
                                                    <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisaHouse" CssClass="btn btn-success" />

                                                </div>
                                            </div>


                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 150px">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label>
                                                    <asp:RadioButtonList AutoPostBack="True" ID="rdTransporteHouse" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Marítimo</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Aéreo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <label class="control-label">Serviço:</label>
                                                    <asp:RadioButtonList AutoPostBack="True" ID="rdServicoHouse" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Importação</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Exportação</asp:ListItem>
                                                        <asp:ListItem Value="0">&nbsp;Outros</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>



                                    <br />
                                    <br />

                                    <div class="table-responsive tableFixHead DivGridHouse" id="DivGridHouse">
                                        <asp:TextBox ID="txtPosicaoHouse" Style="display:none" runat="server"></asp:TextBox>
                                        <asp:GridView ID="dgvHouse" DataKeyNames="ID_BL" DataSourceID="dsHouse" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." OnSorting="dgvHouse_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicaoHouse()"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID_BL" HeaderText="#" SortExpression="ID_BL" Visible="false" />
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NR_BL" HeaderText="HBL" SortExpression="NR_BL" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="Cliente" SortExpression="PARCEIRO_CLIENTE" />
                                                <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                                <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                                <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="Pagamento" SortExpression="TIPO_PAGAMENTO" />
                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="PARCEIRO_AGENTE_INTERNACIONAL" HeaderText="Agente" SortExpression="PARCEIRO_AGENTE_INTERNACIONAL" />
                                                <asp:BoundField DataField="DT_PREVISAO_EMBARQUE_MASTER" HeaderText="Prev.(Embarque)" SortExpression="DT_PREVISAO_EMBARQUE_MASTER" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_EMBARQUE_MASTER" HeaderText="Embarque" SortExpression="DT_EMBARQUE_MASTER" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_PREVISAO_CHEGADA_MASTER" HeaderText="Prev.(Chegada)" SortExpression="DT_PREVISAO_CHEGADA_MASTER" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_CHEGADA_MASTER" HeaderText="Chegada" SortExpression="DT_CHEGADA_MASTER" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="PARCEIRO_TRANSPORTADOR" />
                                                <asp:BoundField DataField="BL_MASTER" HeaderText="MBL" SortExpression="BL_MASTER" />
                                                <asp:BoundField DataField="NR_CE" HeaderText="CE" SortExpression="NR_CE" />
                                                <asp:BoundField DataField="DT_REDESTINACAO" HeaderText="Redestinação" SortExpression="DT_REDESTINACAO" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DT_DESCONSOLIDACAO" HeaderText="Desconsolidação" SortExpression="DT_DESCONSOLIDACAO" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="NAVIO" HeaderText="Navio" SortExpression="NAVIO" />
                                                <asp:BoundField DataField="WEEK" HeaderText="Week" SortExpression="WEEK" />
                                               
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                    <ajaxToolkit:ModalPopupExtender ID="mpe_House" runat="server" PopupControlID="Panel2" TargetControlID="lkFiltrarHouse"></ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Filtro Avançado - House</h5>
                                                        </div>
                                                        <div class="modal-body">

                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Agente:</label>
                                                                        <asp:TextBox ID="txtAgente_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Exportador:</label>
                                                                        <asp:TextBox ID="txtExportador_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Transportador:</label>
                                                                        <asp:TextBox ID="txtTransportador_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cliente:</label>
                                                                        <asp:TextBox ID="txtCliente_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Ref Cliente:</label>
                                                                        <asp:TextBox ID="txtRefCliente_House" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo Estufagem:</label>
                                                                        <asp:TextBox ID="txtEstufagem_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Origem:</label>
                                                                        <asp:TextBox ID="txtOrigem_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Destino:</label>
                                                                        <asp:TextBox ID="txtDestino_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo Frete:</label>
                                                                        <asp:TextBox ID="txtTipoFrete_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Navio:</label>
                                                                        <asp:TextBox ID="txtNavio_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Navio Transbordo:</label>
                                                                        <asp:TextBox ID="txtNavioTransb_House" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Previsão de Embarque</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtInicioPrevEmbarque_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>

                                                                        <asp:TextBox ID="txtFimPrevEmbarque_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Data de Embarque</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtInicioEmbarque_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>
                                                                        <asp:TextBox ID="txtFimEmbarque_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Previsão de Chegada</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>
                                                                        <asp:TextBox ID="txtInicioPrevChegada_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>
                                                                        <asp:TextBox ID="txtFimPrevChegada_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="linha-colorida">Data de Chegada</div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Inicial:</label>

                                                                        <asp:TextBox ID="txtInicioChegada_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Data Final:</label>

                                                                        <asp:TextBox ID="txtFimChegada_House" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Filtrar" ID="btnFiltrar_House" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_House" Text="Close" />
                                                        </div>

                                                    </div>

                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvEmbarque" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFiltrar_House" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFechar_House" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPesquisaHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="rdTransporteHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="rdServicoHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar_House" />
                                    <asp:AsyncPostBackTrigger ControlID="lkCalcularHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="lkAlterarHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="lkDuplicarHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="lkBLHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="lkFollowUpHouse" />
                                    <asp:AsyncPostBackTrigger ControlID="lkTrackingHBL" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>



                </div>
            </div>
        </div>



        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" Style="display: none" />

    </div>


    <asp:SqlDataSource ID="dsHouse" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM View_House
WHERE  ID_BL_MASTER IS NOT NULL AND ID_SERVICO = 1 AND GRAU = 'C' ORDER BY ID_BL DESC "></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsMaster" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM View_Master
WHERE GRAU = 'M' and ID_SERVICO = 1 ORDER BY ID_BL DESC "></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsEmbarque" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM View_Embarque
WHERE GRAU = 'C' and ID_BL_MASTER is null and ID_SERVICO = 1 ORDER BY ID_BL DESC "></asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
        <script type="text/javascript">
            //EMBARQUE
            function SalvaPosicaoEmbarque() {
                var posicao = document.getElementById('DivGridEmbarque').scrollTop;
            if (posicao) {
                document.getElementById('<%= txtPosicaoEmbarque.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= txtPosicaoEmbarque.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
      };
     
    
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= txtPosicaoEmbarque.ClientID %>').value;
            document.getElementById('DivGridEmbarque').scrollTop = valor;
            };

            //MASTER
            function SalvaPosicaoMaster() {
                var posicao = document.getElementById('DivGridMaster').scrollTop;
                if (posicao) {
                    document.getElementById('<%= txtPosicaoMaster.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= txtPosicaoMaster.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
      };
     
    
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
                var valor = document.getElementById('<%= txtPosicaoMaster.ClientID %>').value;
            document.getElementById('DivGridMaster').scrollTop = valor;
            };


            //HOUSE
            function SalvaPosicaoHouse() {
                var posicao = document.getElementById('DivGridHouse').scrollTop;
                if (posicao) {
                    document.getElementById('<%= txtPosicaoHouse.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= txtPosicaoHouse.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
      };
     
    
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
                var valor = document.getElementById('<%= txtPosicaoHouse.ClientID %>').value;
            document.getElementById('DivGridHouse').scrollTop = valor;
            };
        </script>

</asp:Content>
