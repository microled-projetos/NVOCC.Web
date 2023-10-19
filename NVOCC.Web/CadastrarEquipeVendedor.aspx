﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarEquipeVendedor.aspx.vb" Inherits="NVOCC.Web.CadastrarEquipeVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }

        .btnn {
            background-color: #d5d8db;
            margin: 5px;
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
        <div class="col-lg-8 col-lg-offset-2 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">EQUIPE 
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>

                                <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server">Filtro</asp:Label><br />

                                            <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                <asp:ListItem Value="0" Selected="True">Todos os registros</asp:ListItem>
                                                <asp:ListItem Value="1">Nome Equipe</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" Style="padding-left: 35px" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server"></asp:Label><br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                                        </div>
                                    </div>

                                    <div class="col-sm-offset-1 col-sm-3">
                                        <asp:Label ID="Label1" runat="server"></asp:Label><br />
                                        <asp:LinkButton ID="lkCadastrarEquipe" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Cadastrar Equipe</asp:LinkButton>
                                    </div>
                                </div>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                    <asp:GridView ID="gdvEquipesCadastradas" DataKeyNames="ID_EQUIPE" DataSourceID="dsEquipesCadastradas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_EQUIPE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NM_EQUIPE" HeaderText="NOME EQUIPE" SortExpression="NM_EQUIPE" />

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" title="Editar" runat="server" CssClass="btn btn-info btn-sm" CommandName="Editar"
                                                        CommandArgument='<%# Eval("ID_EQUIPE") %>' Autopostback="true"><span class="glyphicon glyphicon-edit" style="font-size: large"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                        OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_EQUIPE") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:large"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>


                                <%-- Início Modal para montar equipe--%>

                                <ajaxToolkit:ModalPopupExtender ID="mpeMontarEquipe" runat="server" PopupControlID="pnlMontarEquipe" TargetControlID="lkCadastrarEquipe" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMontarEquipe" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">MONTAR EQUIPE</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccessMontarEquipe" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccessMontarEquipe" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErroMontarEquipe" runat="server" visible="false">
                                                                <asp:Label ID="lblErroMontarEquipe" runat="server"></asp:Label>
                                                            </div>
                                                            <asp:TextBox ID="txtIDEquipe" runat="server" CssClass="form-control" Style="display: none"></asp:TextBox>
                                                            
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Nome Equipe:</label>
                                                                        <asp:TextBox ID="txtNomeEquipe" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>                                                               
                                                                                                                               
                                                                <div class="col-sm-2">
                                                                    <div class="form-group"><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" ID="btnSalvarEquipe" Text="Salvar" Visible="true" />

                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group"><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-primary btn-block" ID="btnAddTime" Text="Adicionar Time" Visible="false" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row" id="divMembrosEquipe" runat="server" Visible="false" >
                                                                <div class="col-sm-1" style="display: none">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cód Membro:</label>
                                                                        <asp:TextBox ID="txtCodMembroEquipe" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Busca Membro:</label>
                                                                        <asp:TextBox ID="txtBuscaMembrosEquipe" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Membro:</label>
                                                                        <asp:DropDownList ID="ddlBuscaMembrosEquipe" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsBuscaMembrosEquipe" >
                                                                        </asp:DropDownList>                                                                        
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group"><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAddMembroEquipe" Text="Adicionar Membro" Visible="true" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <%--Grid para preencher os dados de montar equipe--%>

                                                            <div class="row">
                                                                <asp:GridView ID="gdvMembrosEquipesCadastradas" DataKeyNames="ID_EQUIPE_MEMBROS" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 300px; overflow: auto;" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." DataSourceID="dsMembrosEquipesCadastradas">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="ID_USUARIO" Visible="False" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" />
                                                                                        <asp:BoundField DataField="NOME" HeaderText="NOME" SortExpression="NOME " />
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton CommandName="Editar" Visible='<%#Eval("ID_TIME") <> 0 %>' CommandArgument='<%# Eval("ID_TIME") %>' runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este usuario da equipe?');" CommandArgument='<%# Eval("ID_EQUIPE_MEMBROS") %>' Autopostback="true"><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                                </asp:GridView>
                                                                   
                                                                </div>

                                                            <%--Grid para preencher os dados de montar equipe--%>

                                                           <%-- <div class="row" id="divEquipe" runat="server" visible="false">
                                                                <div class="col-sm-1" style="display: none">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cód Membro:</label>
                                                                        <asp:TextBox ID="txtCodMembro" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Busca Membro:</label>
                                                                        <asp:TextBox ID="txtBuscaMembros" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Membro:</label></label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlMembro" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsBuscaMembrosEquipes">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <label class="control-label" style="color: white">Membro:</label><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarMembro" Text="Adicionar Membro" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <label class="control-label" style="color: white">Membro:</label><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarTime" Text="Adicionar Time" />
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div class="row">

                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <div class="table-responsive tableFixHead" style="margin: 10px; max-height: 300px;">
                                                                                <asp:GridView ID="gdvMembrosEquipesCadastradas" DataKeyNames="ID_EQUIPE_MEMBROS" DataSourceID="dsMembrosEquipesCadastradas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 300px; overflow: auto;"
                                                                                    AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="ID_USUARIO" Visible="False" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" />
                                                                                        <asp:BoundField DataField="NOME" HeaderText="NOME" SortExpression="NOME " />
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton CommandName="Editar" Visible='<%#Eval("ID_TIME") <> 0 %>' CommandArgument='<%# Eval("ID_TIME") %>' runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este usuario da equipe?');" CommandArgument='<%# Eval("ID_EQUIPE_MEMBROS") %>' Autopostback="true"><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                                </asp:GridView>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>--%>
                                                        </div>
                                                        <div class="modal-footer">

                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharMontarEquipe" Text="Close" />


                                                        </div>

                                                    </div>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvMembrosEquipesCadastradas" />
                                        <asp:AsyncPostBackTrigger ControlID="txtBuscaMembrosEquipe" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <%-- Fim Modal para montar equipe--%>

                                <%-- Início Modal para montar times--%>

                                <ajaxToolkit:ModalPopupExtender ID="mpeMontarTime" runat="server" PopupControlID="pnlMontarTime" TargetControlID="TextBox2" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMontarTime" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">MONTAR TIME</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divTimeSuccess" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccessTime" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divTimeErro" runat="server" visible="false">
                                                                <asp:Label ID="lblErroTime" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row" runat="server">
                                                                <div class="col-sm-8">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Nome Time:</label>
                                                                        <asp:TextBox ID="txtIDTime" runat="server" CssClass="form-control" Style="display: none"></asp:TextBox>
                                                                        <asp:TextBox ID="txtNomeTime" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Qtd. Membros:</label>
                                                                        <asp:TextBox ID="txtQtdMembrosTime" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <label class="control-label" style="color: white">:</label><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarTime" Text="Salvar" />
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row" id="divMembroTime" runat="server" visible="false">
                                                                <div class="col-sm-1" runat="server" visible="false"> 
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cód Membro:</label>
                                                                        <asp:TextBox ID="txtCodMembrosTime" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Busca Membro:</label>
                                                                        <asp:TextBox ID="txtBuscaMembrosTime" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label">Membro:</label></label><label runat="server" style="color: red">*</label>
                                                                        <asp:DropDownList ID="ddlBuscaMembrosTime" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsBuscaMembrosTime">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3" runat="server">
                                                                    <div class="form-group">
                                                                        <label class="control-label" style="color: white">Membro:</label><br />
                                                                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarMembroTime" Text="Adicionar Membro" />
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <br />


                                                                <div class="row">

                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <div class="table-responsive tableFixHead" style="margin: 10px; max-height: 300px;">
                                                                                <asp:GridView ID="dgvMembrosTime" DataKeyNames="ID_TIME" DataSourceID="dsMembrosTime" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 300px; overflow: auto;"
                                                                                    AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="ID_USUARIO" Visible="False" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" />
                                                                                        <asp:BoundField DataField="NOME" HeaderText="NOME" SortExpression="NOME" />
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir este usuario do Time?');" CommandArgument='<%# Eval("ID_TIME_MEMBROS") %>' Autopostback="true"><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                                </asp:GridView>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharMontaTime" Text="Close" />
                                                        </div>
                                                    </div>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTime" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvMembrosTime" />
                                        <asp:AsyncPostBackTrigger ControlID="txtBuscaMembrosTime" />
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharMontaTime" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <%-- Fim Modal para montar times--%>


                                <asp:TextBox ID="TextBox2" Style="display: none" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>

                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvMembrosEquipesCadastradas" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>


    </div> 
     
     <asp:SqlDataSource ID="dsEquipesCadastradas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_EQUIPE, NM_EQUIPE FROM TB_VENDEDOR_EQUIPE ORDER BY NM_EQUIPE"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsMembrosTime" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT T.ID_TIME,U.ID_USUARIO, U.NOME,M.ID_TIME_MEMBROS 
FROM TB_VENDEDOR_TIME T
INNER JOIN TB_VENDEDOR_TIME_MEMBROS M ON T.ID_TIME = M.ID_TIME 
LEFT JOIN TB_USUARIO U ON U.ID_USUARIO = M.ID_USUARIO_MEMBRO_TIME 
WHERE T.ID_TIME = @ID_TIME ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="ID_TIME" Type="Int32" ControlID="txtIDTime" />
        </SelectParameters>
    </asp:SqlDataSource>


      <asp:SqlDataSource ID="dsBuscaMembrosEquipe" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="NOME" Type="String" ControlID="txtBuscaMembrosEquipe" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtCodMembroEquipe" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsBuscaMembrosTime" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="NOME" Type="String" ControlID="txtBuscaMembrosTime" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtCodMembrosTime" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsMembrosEquipesCadastradas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand=" SELECT M.ID_EQUIPE_MEMBROS,M.ID_EQUIPE,U.ID_USUARIO,ISNULL(M.ID_TIME,0)ID_TIME, CASE WHEN M.ID_TIME IS NULL THEN U.NOME ELSE T.NM_TIME END NOME
FROM TB_VENDEDOR_EQUIPE E
INNER JOIN TB_VENDEDOR_EQUIPE_MEMBROS M ON E.ID_EQUIPE = M.ID_EQUIPE  
LEFT JOIN TB_USUARIO U ON U.ID_USUARIO = M.ID_USUARIO_MEMBRO_EQUIPE 
LEFT JOIN TB_VENDEDOR_TIME T ON T.ID_TIME = M.ID_TIME
WHERE E.ID_EQUIPE =  @ID_EQUIPE ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="ID_EQUIPE" Type="Int32" ControlID="txtIDEquipe" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(document).ready(function () {
            Mascara();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            Mascara();
        };

        function Mascara() {
            $(".valores").on("keypress keyup blur", function (e) {
                console.log("entrou")
                var chr = String.fromCharCode(e.which);
                if ("1234567890,".indexOf(chr) < 0)
                    return false;

            });
        };

    </script>
</asp:Content>
