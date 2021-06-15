﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarEmbarqueHouse.aspx.vb" Inherits="NVOCC.Web.CadastrarEmbarqueHouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">MÓDULO OPERACIONAL -
                    <asp:Label ID="lblTipoModulo" runat="server" />
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-offset-9 col-sm-3">
                            <asp:LinkButton ID="btnCapaMaritimo" runat="server" Visible="False" CssClass="btn btn-success btn-block" Text="Imprimir Capa do Processo" href="#" OnClientClick="CapaMaritimo()" />
                            <asp:LinkButton ID="btnCapaAereo" runat="server" Visible="False" CssClass="btn btn-success btn-block" Text="Imprimir Capa do Processo" href="#" OnClientClick="CapaAereo()" />
                        </div>
                    </div>
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#Maritimo" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Maritimo
                            </a>
                        </li>
                        <li>
                            <a href="#Aereo" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Aéreo
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">
                        <br />

                        <div class="tab-pane fade active in" id="Maritimo">


                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active">
                                    <a href="#BasicoMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Informações Básicas
                                    </a>
                                </li>
                                <li>
                                    <a href="#CargaMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Carga
                                    </a>
                                </li>
                                <li>
                                    <a href="#TaxasMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas
                                    </a>
                                </li>
                                <li>
                                    <a href="#ObsMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Observações
                                    </a>
                                </li>
                                <li>
                                    <a href="#RefMaritimo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Referência do Cliente
                                    </a>
                                </li>
                            </ul>

                            <div class="tab-content">

                                <div class="tab-pane fade active in" id="BasicoMaritimo">
                                    <div class="alert alert-success" id="divSuccess_BasicoMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblSuccess_BasicoMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                    </div>
                                    <div class="alert alert-danger" id="divErro_BasicoMaritimo" runat="server" visible="false">
                                        <asp:Label ID="lblErro_BasicoMaritimo" runat="server"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="display: none">
                                            <div class="form-group">
                                                <label class="control-label">Código:</label>
                                                <asp:TextBox ID="txtID_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número do Processo:</label>
                                                <asp:TextBox ID="txtProcesso_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número do MBL:</label>
                                                <asp:TextBox ID="txtMBL_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número do HBL:</label>
                                                <asp:TextBox ID="txtHBL_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Transportador:</label>
                                                <asp:DropDownList ID="ddlTransportador_BasicoMaritimo" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Serviço:</label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlServico_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServicoMaritimo" DataValueField="ID_SERVICO"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto de Origem:</label>
                                                <asp:DropDownList ID="ddlOrigem_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Porto de Destino:</label>
                                                <asp:DropDownList ID="ddlDestino_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Cliente:</label>
                                                <asp:DropDownList ID="ddlCliente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" DataSourceID="dsCliente">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Importador:</label>
                                                <asp:DropDownList ID="ddlImportador_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" DataSourceID="dsImportador">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Exportador:</label>
                                                <asp:DropDownList ID="ddlExportador_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsExportador" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Comissária:</label>
                                                <asp:DropDownList ID="ddlComissaria_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsComissaria" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Agente Internacional:</label>
                                                <asp:DropDownList ID="ddlAgente_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Incoterm:</label>
                                                <asp:DropDownList ID="ddlIncoterm_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label"></label>
                                                <asp:CheckBox ID="ckbFreeHand" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Free Hand"></asp:CheckBox>

                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Tipo de Pagamento:</label>
                                                <asp:DropDownList ID="ddlTipoPagamento_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Tipo de Carga:</label>
                                                <asp:DropDownList ID="ddlTipoCarga_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCargas" DataValueField="ID_TIPO_CARGA">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Tipo de Estufagem:</label>
                                                <asp:DropDownList ID="ddlEstufagem_BasicoMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Número CE:</label>
                                                <asp:TextBox ID="txtCE_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="col-sm-3" style="display:none">
                                            <div class="form-group">
                                                <label class="control-label">Data CE:</label>
                                                <asp:TextBox ID="txtDataCE_BasicoMaritimo" runat="server" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Referência Comercial:</label>
                                                <asp:TextBox ID="txtRefComercial_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Referência Auxiliar:</label>
                                                <asp:TextBox ID="txtRefAuxiliar_BasicoMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="control-label">Resumo Mercadoria:</label>
                                                <asp:TextBox ID="txtResumoMercadoria_BasicoMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-sm-3 col-sm-offset-6">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:Button ID="btnLimpar_BasicoMaritimo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:Button ID="btnGravar_BasicoMaritimo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane fade" id="CargaMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Nova Carga" ID="btnNovaCargaMaritimo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_CargaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_CargaMaritimo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_CargaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_CargaMaritimo1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive tableFixHead" id="divGrid" runat="server">
                                                        <asp:GridView ID="dgvCargaMaritimo" DataKeyNames="ID_CARGA_BL" DataSourceID="dsCargaMaritimo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>

                                                                <asp:BoundField DataField="ID_CARGA_BL" HeaderText="#" SortExpression="ID_CARGA_BL" />
                                                                <asp:BoundField DataField="CONTAINER" HeaderText="Container" SortExpression="CONTAINER" />
                                                                <asp:BoundField DataField="MERCADORIA" HeaderText="Mercadoria" SortExpression="MERCADORIA" />
                                                                <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso Bruto" SortExpression="VL_PESO_BRUTO" />
                                                                <asp:BoundField DataField="VL_M3" HeaderText="M3" SortExpression="VL_M3" />
                                                                <asp:BoundField DataField="NCM" HeaderText="NCM" SortExpression="NCM" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                            Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                            Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_CARGA_BL") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCargaMaritimo" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaMaritimo" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaMaritimo" />
                                                </Triggers>
                                            </asp:UpdatePanel>




                                            <ajaxToolkit:ModalPopupExtender ID="mpeCargaMaritimo" runat="server" PopupControlID="Panel2" TargetControlID="btnNovaCargaMaritimo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                    <ContentTemplate>
                                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title">Carga - Maritimo</h5>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="alert alert-success" id="divSuccess_CargaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblSuccess_CargaMaritimo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger" id="divErro_CargaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblErro_CargaMaritimo2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-3" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_CargaMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Mercadoria:</label>
                                                                                <asp:DropDownList ID="ddlMercadoria_CargaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <%--<div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">NCM:</label>
                                                                                <asp:DropDownList ID="ddlNCM_CargaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NCM" DataSourceID="dsNCM" DataValueField="ID_NCM"></asp:DropDownList>
                                                                            </div>
                                                                        </div>--%>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">NCM:</label>
                                                                                <asp:DropDownList ID="ddlNCM_CargaMaritimo" AutoPostBack="true"  runat="server" CssClass="form-control" Font-Size="11px">                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
</asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Grupo NCM:</label>
                                                                                <asp:TextBox ID="txtGrupoNCM_CargaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Quantidade de Volumes:</label>
                                                                                <asp:TextBox ID="txtQtdVolumes_CargaMaritimo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Peso Bruto:</label>
                                                                                <asp:TextBox ID="txtPesoBruto_CargaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Peso Volumétrico:</label>
                                                                                <asp:TextBox ID="txtPesoVolumetrico_CargaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Embalagem:</label>
                                                                                <asp:DropDownList ID="ddlEmbalagem_CargaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo Container:</label>
                                                                                <asp:DropDownList ID="ddlTipoContainer_CargaMaritimo" AutoPostBack="True" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Número do Container:</label>
                                                                                <asp:TextBox ID="txtNumeroContainer_CargaMaritimo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlNumeroCNTR_CargaMaritimo" AutoPostBack="True" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NR_CNTR" DataSourceID="dsCNTR" DataValueField="ID_CNTR_BL"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Número do Lacre:</label>
                                                                                <asp:TextBox ID="txtNumeroLacre_CargaMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Tara:</label>
                                                                                <asp:TextBox ID="txtValorTara_CargaMaritimo" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar" ID="btnSalvar_CargaMaritimo" CssClass="btn btn-success" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_CargaMaritimo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <ajaxToolkit:ModalPopupExtender ID="mpeNCM_CargaMaritimo" runat="server" PopupControlID="PanelNCM_CargaMaritimo" TargetControlID="ddlNCM_CargaMaritimo" CancelControlID="btnFecharNCM_CargaMaritimo"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNCM_CargaMaritimo" runat="server" CssClass="modalPopup" Style="display: none;">
                                       
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" >Selecione um NCM</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />

                                                            <asp:Label ID="Label1" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNCMFiltro_CargaMaritimo" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNCM_CargaMaritimo" runat="server" DataTextField="NCM" DataSourceID="dsNCM_CargaMaritimo" DataValueField="ID_NCM" Style="text-align:justify;font-size:12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNCM_CargaMaritimo" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNCM_CargaMaritimo" Text="Salvar NCM" />
                                                        </div>


                                                    </div>
                                                </div>
                                           
                                    </asp:Panel>

                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtNCMFiltro_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarNCM_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoContainer_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNCM_CargaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNumeroCNTR_CargaMaritimo" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaMaritimo" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="TaxasMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" AllowCustomErrorsRedirect="True" >
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Text="Nova Taxa" ID="btnNovaTaxaMaritimo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="alert alert-success" id="divSuccess_TaxaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblSuccess_TaxaMaritimo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErro_TaxaMaritimo1" runat="server" visible="false">
                                                        <asp:Label ID="lblErro_TaxaMaritimo1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive tableFixHead" id="div7" runat="server">
                                                        <br />
                                                    COMPRAS:                                           <asp:GridView ID="dgvTaxaMaritimoCompras" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasMaritimoCompras" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                        <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                        <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                        <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                        <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                        <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>

VENDAS:
                                                        <asp:GridView ID="dgvTaxaMaritimoVendas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasMaritimoVendas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                            <Columns>
                                                        <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                        <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                        <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                        <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                        <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                            Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="headerStyle" />
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaMaritimoVendas" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoVendas" />
                                                    <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaMaritimoCompras" />
                                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoCompras" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaMaritimo" />
                                                </Triggers>
                                            </asp:UpdatePanel>




                                            <ajaxToolkit:ModalPopupExtender ID="mpeTaxaMaritimo" runat="server" PopupControlID="Panel3" TargetControlID="btnNovaTaxaMaritimo"></ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Style="display: none">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                    <ContentTemplate>
                                                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title">Taxa - Maritimo</h5>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="alert alert-success" id="divSuccess_TaxaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblSuccess_TaxaMaritimo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger" id="divErro_TaxaMaritimo2" runat="server" visible="false">
                                                                        <asp:Label ID="lblErro_TaxaMaritimo2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="row" style="display: none">
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Código:</label>
                                                                                <asp:TextBox ID="txtID_TaxaMaritimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo Despesa:</label>
                                                                                <asp:DropDownList ID="ddlDespesa_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" AutoPostBack="true" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4" style="display: none">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>

                                                                                <asp:CheckBox ID="ckbPremiacao_TaxaMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Premiação"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>

                                                                                <asp:CheckBox ID="ckbDeclarado_TaxaMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Declarado"></asp:CheckBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label"></label>
                                                                                <asp:CheckBox ID="ckbProfit_TaxaMaritimo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;PROFIT"></asp:CheckBox>
                                                                            </div>
                                                                        </div>

                                                                    </div>


                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tipo de Pagamento:</label>
                                                                                <asp:DropDownList ID="ddlTipoPagamento_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Origem do Pagamento:</label>
                                                                                <asp:DropDownList ID="ddlOrigemPagamento_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO"></asp:DropDownList>

                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Status do pagamento:</label>
                                                                                <asp:DropDownList ID="ddlStatusPagamento_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_PAGAMENTO" DataSourceID="dsStatusPagamento" Enabled="false" DataValueField="ID_STATUS_PAGAMENTO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">

                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Destinatario de Cobrança:</label>
                                                                                <asp:DropDownList ID="ddlDestinatarioCob_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_DESTINATARIO_COBRANCA" DataSourceID="dsDestinatarioCobranca" DataValueField="ID_DESTINATARIO_COBRANCA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Fornecedor:</label>
                                                                                <asp:DropDownList ID="ddlEmpresa_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsFornecedorMaritimo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Base de cálculo:</label>
                                                                                <asp:DropDownList ID="ddlBaseCalculo_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" runat="server" id="divCompraMaritimo">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Moeda de Compra:</label>
                                                                                <asp:DropDownList ID="ddlMoedaCompra_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <%--                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Base da Compra:</label>
                                                                        <asp:TextBox ID="txtBaseCompra_TaxaMaritimo" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Mínimo de Compra:</label>
                                                                                <asp:TextBox ID="txtMinCompra_TaxaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Compra:</label>
                                                                                <asp:TextBox ID="txtValorCompra_TaxaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" runat="server" id="divVendaMaritimo">
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Moeda de Venda:</label>
                                                                                <asp:DropDownList ID="ddlMoedaVenda_TaxaMaritimo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Base da Venda:</label>
                                                                        <asp:TextBox ID="txtBaseVenda_TaxaMaritimo" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor Mínimo de Venda:</label>
                                                                                <asp:TextBox ID="txtMinVenda_TaxaMaritimo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Valor da Venda:</label>
                                                                                <asp:TextBox ID="txtValorVenda_TaxaMaritimo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Observações:</label>
                                                                                <asp:TextBox ID="txtObs_TaxaMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button runat="server" Text="Salvar Taxa" ID="btnSalvar_TaxaMaritimo" CssClass="btn btn-success" />
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_TaxaMaritimo" Text="Close" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlDespesa_TaxaMaritimo" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtValorVenda_TaxaMaritimo" />                                                       
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoVendas" />
                                                                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaMaritimoCompras" />

                                            <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="ObsMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_ObsMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_ObsMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_ObsMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_ObsMaritimo" runat="server"></asp:Label>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Operacional Interna:</label>
                                                        <asp:TextBox ID="txtObsoperacional_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Comercial:</label>
                                                        <asp:TextBox ID="txtObsComercial_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Agente Internacional:</label>
                                                        <asp:TextBox ID="txtObsAgente_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação Cliente:</label>
                                                        <asp:TextBox ID="txtObsCliente_ObsMaritimo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_ObsMaritimo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_ObsMaritimo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_ObsMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_ObsMaritimo" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="RefMaritimo">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <br />

                                            <asp:Label ID="Label11" Style="padding-left: 35px" runat="server">Referência do Cliente:</asp:Label>
                                            <div class="row linhabotao text-center" style="margin-left: 20px">

                                                <div>
                                                    <div class="col-sm-4" style="border: ridge 1px; margin-left: 10px; padding-top: 20px; padding-bottom: 10px">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtRefMaritimo" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>

                                                        </div>

                                                    </div>
                                                    <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                        <div class="form-group">
                                                            <asp:Button runat="server" Text="Gravar" ID="btnGravar_RefMaritimo" CssClass="btn btn-success" />


                                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar_RefMaritimo" CssClass="btn btn-danger" />

                                                        </div>
                                                    </div>





                                                </div>
                                            </div>
                                            <br />
                                            <asp:TextBox ID="txtID_RefMaritimo" Style="display: none" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>


                                            <div class="alert alert-success" id="divSuccess_RefMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_RefMaritimo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_RefMaritimo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_RefMaritimo" runat="server"></asp:Label>
                                            </div>

                                            <div class="table-responsive tableFixHead" id="div10" runat="server">

                                                <asp:GridView ID="dgvRefMaritimo" DataKeyNames="ID_REFERENCIA_CLIENTE" DataSourceID="dsRefMaritimo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>

                                                        <asp:BoundField DataField="ID_REFERENCIA_CLIENTE" ReadOnly="true" HeaderText="#" SortExpression="ID_REFERENCIA_CLIENTE" />
                                                        <asp:BoundField DataField="ID_BL" ReadOnly="true" HeaderText="ID_BL" SortExpression="ID_BL" />
                                                        <asp:BoundField DataField="NR_REFERENCIA_CLIENTE" HeaderText="NR_REFERENCIA_CLIENTE" SortExpression="NR_REFERENCIA_CLIENTE" />
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditarParcela" runat="server" CausesValidation="False" CommandName="visualizar" CssClass="btn btn-info" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>'><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            <ControlStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <br />

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvRefMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar_RefMaritimo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_RefMaritimo" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                </div>
                            </div>
                        </div>

                        <div class="tab-pane fade" id="Aereo">

                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active">
                                    <a href="#BasicoAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Informações Básicas
                                    </a>
                                </li>
                                <li>
                                    <a href="#CargaAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Carga
                                    </a>
                                </li>
                                <li>
                                    <a href="#TaxasAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Taxas
                                    </a>
                                </li>
                                <li>
                                    <a href="#ObsAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Observações
                                    </a>
                                </li>
                                <li>
                                    <a href="#RefAereo" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Referência do Cliente
                                    </a>
                                </li>
                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="BasicoAereo">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_BasicoAereo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_BasicoAereo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_BasicoAereo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_BasicoAereo" runat="server"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3" style="display: none">
                                                    <div class="form-group">
                                                        <label class="control-label">Código:</label>
                                                        <asp:TextBox ID="txtID_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do Processo:</label>
                                                        <asp:TextBox ID="txtProcesso_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do MBL:</label>
                                                        <asp:TextBox ID="txtMBL_BasicoAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número do HBL:</label>
                                                        <asp:TextBox ID="txtHBL_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Serviço:</label>
                                                        <asp:DropDownList ID="ddlServico_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServicoAereo" DataValueField="ID_SERVICO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Incoterm:</label>
                                                        <asp:DropDownList ID="ddlIncoterm_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Transportador:</label>
                                                        <asp:DropDownList ID="ddlTransportador_BasicoAereo" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Transporte Rodoviário:</label>
                                                        <asp:DropDownList ID="ddlTranspRod_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsRodoviario" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Armazém de Desembaraço:</label>
                                                        <asp:DropDownList ID="ddlArmazem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsArmazemDesembaraco" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Pagamento:</label>
                                                        <asp:DropDownList ID="ddlTipoPagamento_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Porto de Origem:</label>
                                                        <asp:DropDownList ID="ddlOrigem_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Porto de Destino:</label>
                                                        <asp:DropDownList ID="ddlDestino_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Cliente:</label>
                                                        <asp:DropDownList ID="ddlCliente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" DataSourceID="dsCliente">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Tipo de Carga:</label>
                                                        <asp:DropDownList ID="ddlTipoCarga_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCargas" DataValueField="ID_TIPO_CARGA">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Exportador:</label>
                                                        <asp:DropDownList ID="ddlExportador_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsExportador" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Comissária:</label>
                                                        <asp:DropDownList ID="ddlComissaria_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsComissaria" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Agente:</label>
                                                        <asp:DropDownList ID="ddlAgente_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Importador:</label>
                                                        <asp:DropDownList ID="ddlImportador_BasicoAereo" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" DataSourceID="dsImportador">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Número CE:</label>
                                                        <asp:TextBox ID="txtNumeroCE_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-sm-3" style="display:none">
                                                    <div class="form-group">
                                                        <label class="control-label">Data CE:</label>
                                                        <asp:TextBox ID="txtDataCE_BasicoAereo" runat="server" CssClass="form-control data"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Comercial:</label>
                                                        <asp:TextBox ID="txtRefComercial_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label class="control-label">Referência Auxiliar:</label>
                                                        <asp:TextBox ID="txtRefAuxiliar_BasicoAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Resumo Mercadoria:</label>
                                                        <asp:TextBox ID="txtResumoMercadoria_BasicoAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_BasicoAereo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_BasicoAereo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_BasicoAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_BasicoAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="CargaAereo">
                                    <br />
                                    <div class="row">

                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Button runat="server" Text="Nova Carga" ID="btnNovaCargaAereo" CssClass="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_CargaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_CargaAereo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_CargaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblErro_CargaAereo1" runat="server"></asp:Label>
                                            </div>
                                            <div class="table-responsive tableFixHead" id="div3" runat="server">

                                                <asp:GridView ID="dgvCargaAereo" DataKeyNames="ID_CARGA_BL" DataSourceID="dsCargaAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_CARGA_BL" HeaderText="#" SortExpression="ID_CARGA_BL" />
                                                        <asp:BoundField DataField="CONTAINER" HeaderText="Container" SortExpression="CONTAINER" />
                                                        <asp:BoundField DataField="MERCADORIA" HeaderText="Mercadoria" SortExpression="MERCADORIA" />
                                                        <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso Bruto" SortExpression="VL_PESO_BRUTO" />
                                                        <asp:BoundField DataField="VL_M3" HeaderText="M3" SortExpression="VL_M3" />
                                                        <asp:BoundField DataField="NCM" HeaderText="NCM" SortExpression="NCM" />
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                    Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_CARGA_BL") %>'
                                                                    Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_CARGA_BL") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCargaAereo" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaAereo" />

                                        </Triggers>
                                    </asp:UpdatePanel>




                                    <ajaxToolkit:ModalPopupExtender ID="mpeCargaAereo" runat="server" PopupControlID="Panel1" TargetControlID="btnNovaCargaAereo"></ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Carga - Aereo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccess_CargaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccess_CargaAereo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErro_CargaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblErro_CargaAereo2" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3" style="display: none">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código:</label>
                                                                        <asp:TextBox ID="txtID_CargaAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Mercadoria:</label>
                                                                        <asp:DropDownList ID="ddlMercadoria_CargaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA"></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                               <%-- <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">NCM:</label>
                                                                        <asp:DropDownList ID="ddlNCM_CargaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NCM" DataSourceID="dsNCM" DataValueField="ID_NCM"></asp:DropDownList>
                                                                    </div>
                                                                </div>--%>
                                                                 <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">NCM:</label>
                                                                        <asp:DropDownList ID="ddlNCM_CargaAereo" runat="server" CssClass="form-control" Font-Size="11px">
<asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Quantidade de Volumes:</label>
                                                                        <asp:TextBox ID="txtQtdVolume_CargaAereo" runat="server" CssClass="form-control ApenasNumeros"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Peso Bruto:</label>
                                                                        <asp:TextBox ID="txtPesoBruto_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Peso Volumétrico:</label>
                                                                        <asp:TextBox ID="txtPesoVolumetrico_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Descrição da Mercadoria:</label>
                                                                        <asp:TextBox ID="txtDescMercadoria_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Comprimento:</label>
                                                                        <asp:TextBox ID="txtComprimento_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Largura:</label>
                                                                        <asp:TextBox ID="txtLargura_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Altura:</label>
                                                                        <asp:TextBox ID="txtAltura_CargaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar" ID="btnSalvar_CargaAereo" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_CargaAereo" Text="Close" />
                                                        </div>

                                                    </div>

                                                </div>

                                                 <ajaxToolkit:ModalPopupExtender ID="mpeNCM_CargaAereo" runat="server" PopupControlID="PanelNCM_CargaAereo" TargetControlID="ddlNCM_CargaAereo" CancelControlID="btnFecharNCM_CargaAereo"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PanelNCM_CargaAereo" runat="server" CssClass="modalPopup" Style="display: none;">
                                      
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" >Selecione um NCM</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />

                                                            <asp:Label ID="Label2" Style="padding-left: 35px" runat="server">Pesquisa:</asp:Label>
                                                            <div class="row linhabotao text-center" style="margin-left: 20px; margin-right: 20px">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtNCMFiltro_CargaAereo" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row" style="max-height: 300px; overflow: auto;">

                                                                    <div class="col-sm-12">

                                                                        <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdNCM_CargaAereo" runat="server" AutoPostBack="true" DataTextField="NCM" DataSourceID="dsNCM_CargaAereo" DataValueField="ID_NCM" Style="text-align:justify;font-size:12px;">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNCM_CargaAereo" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarNCM_CargaAereo" Text="Salvar NCM" />
                                                        </div>


                                                    </div>
                                                </div>
                               
                                    </asp:Panel>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtNCMFiltro_CargaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnSalvarNCM_CargaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnSalvar_CargaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFechar_CargaAereo" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCargaAereo" />

                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>


                                </div>
                                <div class="tab-pane fade" id="TaxasAereo">
                                    <br />
                                    <div class="row">

                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Button runat="server" Text="Nova Taxa" ID="btnNovaTaxaAereo" CssClass="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>

                                            <div class="alert alert-success" id="divSuccess_TaxaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_TaxaAereo1" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_TaxaAereo1" runat="server" visible="false">
                                                <asp:Label ID="lblErro_TaxaAereo1" runat="server"></asp:Label>
                                            </div>

                                            <div class="table-responsive tableFixHead" id="div12" runat="server">
                                                <br/>
                                                COMPRAS:
                                                                                                <asp:GridView ID="dgvTaxaAereoCompras" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasAereoCompras" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                        <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                        <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                        <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                        <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                    Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                    Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>

                                                VENDAS:
                                                <asp:GridView ID="dgvTaxaAereoVendas" DataKeyNames="ID_BL_TAXA" DataSourceID="dsTaxasAereoVendas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>
                                                        <asp:BoundField DataField="ITEM_DESPESA" HeaderText="DESPESA" SortExpression="ITEM_DESPESA" />
                                                        <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                        <asp:BoundField DataField="VL_TAXA" HeaderText="VALOR" SortExpression="VL_TAXA" />
                                                        <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="VALOR CALCULADO" SortExpression="VL_TAXA_CALCULADO" />
                                                        <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                        <asp:BoundField DataField="TIPO_PAGAMENTO" HeaderText="TIPO DE PAGAMENTO" SortExpression="TIPO_PAGAMENTO" />
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                    Text="Visualizar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_BL_TAXA") %>'
                                                                    Text="Visualizar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></div></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_BL_TAXA") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaAereoVendas" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoVendas" />
                                            <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxaAereoCompras" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoCompras" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>




                                    <ajaxToolkit:ModalPopupExtender ID="mpeTaxaAereo" runat="server" PopupControlID="Panel4" TargetControlID="btnNovaTaxaAereo"></ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Taxa - Aereo</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccess_TaxaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccess_TaxaAereo2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErro_TaxaAereo2" runat="server" visible="false">
                                                                <asp:Label ID="lblErro_TaxaAereo2" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row" style="display: none">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Código:</label>
                                                                        <asp:TextBox ID="txtID_TaxaAereo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo de despesa:</label>
                                                                        <asp:DropDownList ID="ddlDespesa_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" AutoPostBack="true" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4" style="display: none">
                                                                    <div class="form-group">
                                                                        <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbPremiacao_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Premiação"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbProfit_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;PROFIT"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label"></label>

                                                                        <asp:CheckBox ID="ckbDeclarado_TaxaAereo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Declarado"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Tipo Pagamento:</label>
                                                                        <asp:DropDownList ID="ddlTipoPagamento_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Origem do Pagamento:</label>
                                                                        <asp:DropDownList ID="ddlOrigemPagamento_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Destinatário de cobrança:</label>
                                                                        <asp:DropDownList ID="ddlDestinatarioCob_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_DESTINATARIO_COBRANCA" DataSourceID="dsDestinatarioCobranca" DataValueField="ID_DESTINATARIO_COBRANCA"></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Fornecedor:</label>
                                                                        <asp:DropDownList ID="ddlEmpresa_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsFornecedorAereo" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Base de cálculo:</label>
                                                                        <asp:DropDownList ID="ddlBaseCalculo_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" runat="server" id="divCompraAereo">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda da compra:</label>
                                                                        <asp:DropDownList ID="ddlMoedaCompra_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <%-- <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Base da Compra:</label>
                                                                        <asp:TextBox ID="txtBaseCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Mínimo de Compra:</label>
                                                                        <asp:TextBox ID="txtMinCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor da Compra:</label>
                                                                        <asp:TextBox ID="txtValorCompra_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" runat="server" id="divVendaAereo">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Moeda de Venda:</label>
                                                                        <asp:DropDownList ID="ddlMoedaVenda_TaxaAereo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <%-- <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Base da Venda:</label>
                                                                        <asp:TextBox ID="txtBaseVenda_TaxaAereo" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor Mínimo de Venda:</label>
                                                                        <asp:TextBox ID="txtMinVenda_TaxaAereo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Valor da Venda:</label>
                                                                        <asp:TextBox ID="txtValorVenda_TaxaAereo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Observações:</label>
                                                                        <asp:TextBox ID="txtObs_TaxaAereo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="1000"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar Taxa" ID="btnSalvar_TaxaAereo" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar_TaxaAereo" Text="Close" />
                                                        </div>



                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSalvar_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="btnFechar_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoCompras" />
                                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxaAereoVendas" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlDespesa_TaxaAereo" />
                                                <asp:AsyncPostBackTrigger ControlID="txtValorVenda_TaxaAereo" />                                   
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </div>
                                <div class="tab-pane fade" id="ObsAereo">
                                    <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="alert alert-success" id="divSuccess_ObsAereo" runat="server" visible="false">
                                                <asp:Label ID="lblSuccess_ObsAereo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" id="divErro_ObsAereo" runat="server" visible="false">
                                                <asp:Label ID="lblErro_ObsAereo" runat="server"></asp:Label>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação operacional interna:</label>
                                                        <asp:TextBox ID="txtObsOperacional_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação comercial:</label>
                                                        <asp:TextBox ID="txtObsComercial_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação agente internacional:</label>
                                                        <asp:TextBox ID="txtObsAgente_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group">
                                                        <label class="control-label">Observação cliente:</label>
                                                        <asp:TextBox ID="txtObsCliente_ObsAereo" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3 col-sm-offset-6">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnLimpar_ObsAereo" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnGravar_ObsAereo" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_ObsAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpar_ObsAereo" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane fade" id="RefAereo">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>

                                            <br />
                                            <asp:Label ID="Label12" Style="padding-left: 35px" runat="server">Referência do Cliente:</asp:Label>
                                            <div class="row linhabotao text-center" style="margin-left: 20px">

                                                <div>
                                                    <div class="col-sm-4" style="border: ridge 1px; margin-left: 10px; padding-top: 20px; padding-bottom: 10px">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtRefAereo" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>

                                                        </div>

                                                    </div>
                                                    <div class="col-sm-1" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                                        <div class="form-group">
                                                            <asp:Button runat="server" Text="Gravar" ID="btnGravar_RefAereo" CssClass="btn btn-success" />

                                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar_RefAereo" CssClass="btn btn-danger" />

                                                        </div>
                                                    </div>


                                                    <asp:TextBox ID="txtID_RefAereo" Style="display: none" runat="server" CssClass="form-control" Width="550px"></asp:TextBox>


                                                </div>
                                            </div>
                                            <br />

                                            <div class="table-responsive tableFixHead" id="div15" runat="server">

                                                <div class="alert alert-success" id="divSuccess_RefAereo" runat="server" visible="false">
                                                    <asp:Label ID="lblSuccess_RefAereo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:Label>
                                                </div>
                                                <div class="alert alert-danger" id="divErro_RefAereo" runat="server" visible="false">
                                                    <asp:Label ID="lblErro_RefAereo" runat="server"></asp:Label>
                                                </div>
                                                <asp:GridView ID="dgvRefAereo" DataKeyNames="ID_REFERENCIA_CLIENTE" DataSourceID="dsRefAereo" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                    <Columns>

                                                        <asp:BoundField DataField="ID_REFERENCIA_CLIENTE" HeaderText="#" SortExpression="ID_REFERENCIA_CLIENTE" />
                                                        <asp:BoundField DataField="ID_BL" HeaderText="ID_BL" SortExpression="ID_BL" />
                                                        <asp:BoundField DataField="NR_REFERENCIA_CLIENTE" HeaderText="NR_REFERENCIA_CLIENTE" SortExpression="NR_REFERENCIA_CLIENTE" />
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditarParcela" runat="server" CausesValidation="False" CommandName="visualizar" CssClass="btn btn-info" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>'><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                            <ControlStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_REFERENCIA_CLIENTE") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerStyle" />
                                                </asp:GridView>
                                            </div>
                                            <br />


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvRefAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar_RefAereo" />
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar_RefAereo" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                </div>

                            </div>
                        </div>

                    </div>





                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT 0, 'Selecione' FROM [dbo].[TB_ORIGEM_PAGAMENTO] ORDER BY ID_ORIGEM_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsNCM_CargaMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NCM,CD_NCM +' - '+ NM_NCM AS NCM FROM [dbo].[TB_NCM] WHERE (NM_NCM like '%' + @Nome + '%' Or @Nome = '0') or (CD_NCM like '%' + @Nome + '%' Or @Nome = '0')">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNCMFiltro_CargaMaritimo" />
        </SelectParameters>
        </asp:SqlDataSource> 
     <asp:SqlDataSource ID="dsNCM_CargaAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NCM,CD_NCM +' - '+ NM_NCM AS NCM FROM [dbo].[TB_NCM] WHERE (NM_NCM like '%' + @Nome + '%' Or @Nome = '0') or (CD_NCM like '%' + @Nome + '%' Or @Nome = '0')">
        <SelectParameters>
            <asp:ControlParameter Name="Nome" Type="String" ControlID="txtNCMFiltro_CargaAereo" />
        </SelectParameters>
        </asp:SqlDataSource> 
    <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM [dbo].[TB_ITEM_DESPESA]
union SELECT 0, ' Selecione' FROM [dbo].[TB_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT 0, 'Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY ID_BASE_CALCULO_TAXA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO, NM_PORTO FROM [dbo].[TB_PORTO] union SELECT 0, 'Selecione' FROM [dbo].[TB_PORTO] ORDER BY ID_PORTO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComex" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM [dbo].[TB_TIPO_COMEX]
union SELECT 0, 'Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY ID_TIPO_COMEX"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR = 1
        union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsRodoviario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_RODOVIARIO = 1
        union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsArmazemDesembaraco" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_ARMAZEM_DESEMBARACO  = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFornecedorMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN((SELECT ID_PARCEIRO_EXPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_CLIENTE FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_IMPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_TRANSPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_COMISSARIA FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_AGENTE_INTERNACIONAL FROM TB_BL WHERE ID_BL = @ID_BL  ))
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO]
ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFornecedorAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN((SELECT ID_PARCEIRO_EXPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_CLIENTE FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_IMPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_TRANSPORTADOR FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_COMISSARIA FROM TB_BL WHERE ID_BL = @ID_BL 
UNION 
SELECT ID_PARCEIRO_AGENTE_INTERNACIONAL FROM TB_BL WHERE ID_BL = @ID_BL  ))
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO]
ORDER BY ID_PARCEIRO">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INCOTERM, cast((CD_INCOTERM)as varchar)+ ' - '+ NM_INCOTERM as NM_INCOTERM FROM TB_INCOTERM 
union SELECT  0, 'Selecione' FROM TB_INCOTERM ORDER BY ID_INCOTERM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissaria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_COMISSARIA = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsExportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_EXPORTADOR = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFrequencia" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_FREQUENCIA, NM_TIPO_FREQUENCIA FROM [dbo].[TB_TIPO_FREQUENCIA]
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_FREQUENCIA] ORDER BY ID_TIPO_FREQUENCIA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCargas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_CARGA] ORDER BY ID_TIPO_CARGA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDestinatarioCobranca" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_DESTINATARIO_COBRANCA,NM_DESTINATARIO_COBRANCA from TB_DESTINATARIO_COBRANCA
union SELECT  0, 'Selecione' FROM TB_DESTINATARIO_COBRANCA ORDER BY ID_DESTINATARIO_COBRANCA
"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsServicoMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO IN (1,4)
union SELECT  0, 'Selecione' FROM TB_SERVICO ORDER BY ID_SERVICO"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsServicoAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO IN (2,5)
union SELECT  0, 'Selecione' FROM TB_SERVICO ORDER BY ID_SERVICO"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsStatusPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_STATUS_PAGAMENTO, NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO
union SELECT  0, 'Selecione' FROM TB_STATUS_PAGAMENTO ORDER BY ID_STATUS_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_CONTAINER] ORDER BY ID_TIPO_CONTAINER"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCNTR" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CNTR_BL, NR_CNTR FROM TB_CNTR_BL WHERE ID_TIPO_CNTR = 0
union SELECT 0, 'Selecione' FROM [dbo].[TB_CNTR_BL] ORDER BY ID_CNTR_BL">
        <%--<SelectParameters>
            <asp:ControlParameter Name="TIPO_CNTR" Type="Int32" ControlID="ddlNumeroCNTR_CargaMaritimo" />
        </SelectParameters>--%>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMercadoria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MERCADORIA, NM_MERCADORIA FROM [dbo].[TB_MERCADORIA]
union SELECT 0, 'Selecione' FROM [dbo].[TB_MERCADORIA] ORDER BY ID_MERCADORIA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM]
union SELECT 0, 'Selecione' FROM [dbo].[TB_MERCADORIA] ORDER BY ID_TIPO_ESTUFAGEM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_PAGAMENTO, NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO
union SELECT  0, 'Selecione' FROM TB_TIPO_PAGAMENTO ORDER BY ID_TIPO_PAGAMENTO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCliente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1
union SELECT  0, 'Selecione' FROM TB_PARCEIRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsImportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_IMPORTADOR =1 
union SELECT  0, 'Selecione' FROM TB_PARCEIRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsRefMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_REFERENCIA_CLIENTE,ID_BL,NR_REFERENCIA_CLIENTE FROM TB_REFERENCIA_CLIENTE WHERE ID_BL = @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsRefAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_REFERENCIA_CLIENTE,ID_BL,NR_REFERENCIA_CLIENTE FROM TB_REFERENCIA_CLIENTE WHERE ID_BL = @ID_BL
">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:SqlDataSource ID="dsCargaMaritimo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CARGA_BL,
ID_CNTR_BL,
(SELECT NR_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = A.ID_CNTR_BL)CONTAINER,
ID_MERCADORIA,
(SELECT NM_MERCADORIA FROM TB_MERCADORIA WHERE ID_MERCADORIA = A.ID_MERCADORIA)MERCADORIA,
VL_PESO_BRUTO,
VL_M3,
ID_NCM,
(SELECT NM_NCM FROM TB_NCM WHERE ID_NCM = A.ID_NCM)NCM
FROM TB_CARGA_BL A WHERE ID_BL = @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCargaAereo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CARGA_BL,
ID_CNTR_BL,
(SELECT NR_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = A.ID_CNTR_BL)CONTAINER,ID_MERCADORIA,
(SELECT NM_MERCADORIA FROM TB_MERCADORIA WHERE ID_MERCADORIA = A.ID_MERCADORIA)MERCADORIA,
VL_PESO_BRUTO,
VL_M3,
ID_NCM,
(SELECT NM_NCM FROM TB_NCM WHERE ID_NCM = A.ID_NCM)NCM
FROM TB_CARGA_BL A WHERE ID_BL = @ID_BL

">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasAereoCompras" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)ITEM_DESPESA,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA)BASE_CALCULO,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)MOEDA,
(SELECT NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO WHERE ID_STATUS_PAGAMENTO = A.ID_STATUS_PAGAMENTO)STATUS_PAGAMENTO,
(SELECT NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA WHERE ID_DESTINATARIO_COBRANCA = A.ID_DESTINATARIO_COBRANCA)DESTINATARIO_COBRANCA,
VL_TAXA,
VL_TAXA_CALCULADO
FROM TB_BL_TAXA A WHERE ID_BL =  @ID_BL AND CD_PR = 'P'

">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsTaxasAereoVendas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)ITEM_DESPESA,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA)BASE_CALCULO,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)MOEDA,
(SELECT NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO WHERE ID_STATUS_PAGAMENTO = A.ID_STATUS_PAGAMENTO)STATUS_PAGAMENTO,
(SELECT NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA WHERE ID_DESTINATARIO_COBRANCA = A.ID_DESTINATARIO_COBRANCA)DESTINATARIO_COBRANCA,
VL_TAXA,
VL_TAXA_CALCULADO
FROM TB_BL_TAXA A WHERE ID_BL =  @ID_BL AND CD_PR = 'R'

">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoAereo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTaxasMaritimoVendas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)ITEM_DESPESA,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA)BASE_CALCULO,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)MOEDA,
(SELECT NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO WHERE ID_STATUS_PAGAMENTO = A.ID_STATUS_PAGAMENTO)STATUS_PAGAMENTO,
(SELECT NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA WHERE ID_DESTINATARIO_COBRANCA = A.ID_DESTINATARIO_COBRANCA)DESTINATARIO_COBRANCA,
VL_TAXA,
VL_TAXA_CALCULADO
FROM TB_BL_TAXA A WHERE ID_BL = @ID_BL AND CD_PR ='R'

">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="dsTaxasMaritimoCompras" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BL_TAXA,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)ITEM_DESPESA,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA)BASE_CALCULO,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)MOEDA,
(SELECT NM_STATUS_PAGAMENTO FROM TB_STATUS_PAGAMENTO WHERE ID_STATUS_PAGAMENTO = A.ID_STATUS_PAGAMENTO)STATUS_PAGAMENTO,
(SELECT NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA WHERE ID_DESTINATARIO_COBRANCA = A.ID_DESTINATARIO_COBRANCA)DESTINATARIO_COBRANCA,
VL_TAXA,
VL_TAXA_CALCULADO
FROM TB_BL_TAXA A WHERE ID_BL = @ID_BL AND CD_PR ='P'

">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BasicoMaritimo" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>


        function CapaMaritimo() {

            var ID_BasicoMaritimo = document.getElementById('<%= txtID_BasicoMaritimo.ClientID %>').value;
            console.log(ID_BasicoMaritimo);

            window.open('CapaProcesso.aspx?id=' + ID_BasicoMaritimo, '_blank');
        }


        function CapaAereo() {


            var ID_BasicoAereo = document.getElementById('<%= txtID_BasicoAereo.ClientID %>').value;
            console.log(ID_BasicoAereo);

            window.open('CapaProcesso.aspx?id=' + ID_BasicoAereo, '_blank');

        }


        function Func() {
            alert("O valor de venda é menor que o valor de compra!");
        }
    </script>
</asp:Content>
