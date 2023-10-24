<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ComissaoVendedor.aspx.vb" Inherits="NVOCC.Web.ComissaoVendedor" %>

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
            font-size: 13px
        }

        .selected1 {
            color: black;
            font-family: verdana;
            font-size: 8pt;
            background-color: #e6c3a5;
        }

        .modal-xxl {
            width: 100%;
            max-width: 2000px;
        }
    </style>


    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">COMISSÃO DE VENDEDOR
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>

                                <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server">Filtro</asp:Label><br />

                                            <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                <asp:ListItem Value="0" Selected="True">Todos os registros</asp:ListItem>
                                                <asp:ListItem Value="3">Vendedores</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Vendedores</asp:ListItem>
                                                <asp:ListItem Value="5">Equipe</asp:ListItem>
                                                <asp:ListItem Value="1">Nome</asp:ListItem>
                                                <asp:ListItem Value="2">Número do processo</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" Style="padding-left: 35px" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server">Competência</asp:Label><br />

                                            <asp:TextBox ID="txtCompetencia" placeholder="MM/AAAA" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server"></asp:Label><br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                                        </div>
                                    </div>

                                    <div class="col-sm-offset-1 col-sm-5">
                                        <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />

                                        <asp:LinkButton ID="lkComissoes" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Comissões</asp:LinkButton>
                                        <asp:LinkButton ID="lkRelatorios" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Relatorios</asp:LinkButton>
                                        <asp:LinkButton ID="lkCSV" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Exportar CSV</asp:LinkButton>
                                        <asp:LinkButton ID="lkCCProcesso" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Gravar no CC do Processo</asp:LinkButton>


                                    </div>
                                </div>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblCompetenciaSobrepor" runat="server"></asp:Label>
                                    <asp:Label ID="lblContasReceber" runat="server"></asp:Label>
                                </div>
                                <div runat="server" visible="false" id="DivGrid2">
                                    <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                        <asp:GridView ID="dgvComissoes" DataKeyNames="ID_DETALHE_COMISSAO_VENDEDOR,ID_CABECALHO_COMISSAO_VENDEDOR" DataSourceID="dsComissao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_DETALHE_COMISSAO_VENDEDOR") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="COMPETENCIA" HeaderText="COMPETENCIA" SortExpression="COMPETENCIA" />
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NR_NOTAS_FISCAL" HeaderText="NOTA FISCAL" SortExpression="NR_NOTAS_FISCAL" />
                                                <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="DATA NOTA" SortExpression="DT_NOTA_FISCAL" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PARCEIRO_VENDEDOR" HeaderText="VENDEDOR" SortExpression="PARCEIRO_VENDEDOR" />
                                                <asp:BoundField DataField="ANALISTA_COTACAO" HeaderText="ANALISTA COTAÇÃO" SortExpression="ANALISTA_COTACAO" />
                                                <asp:BoundField DataField="USUARIO_LIDER" HeaderText="USUARIO LÍDER" SortExpression="USUARIO_LIDER" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="CLIENTE" SortExpression="PARCEIRO_CLIENTE" />
                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="ESTUFAGEM" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="QTD. BL/CNTR" HeaderText="QTD. BL/CNTR" SortExpression="QTD. BL/CNTR" />
                                                <asp:BoundField DataField="VL_COMISSAO_BASE" HeaderText="BASE" SortExpression="VL_COMISSAO_BASE" />
                                                <asp:BoundField DataField="VL_PERCENTUAL" HeaderText="PERCENTUAL" SortExpression="VL_PERCENTUAL" />
                                                <asp:BoundField DataField="VL_COMISSAO_TOTAL" HeaderText="COMISSAO" SortExpression="VL_COMISSAO_TOTAL" />
                                                <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="LIQUIDAÇÃO" SortExpression="DT_LIQUIDACAO" />
                                                <asp:BoundField DataField="DT_EXPORTACAO" HeaderText="EXPORTAÇÃO" SortExpression="DT_EXPORTACAO" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                            CommandArgument='<%# Eval("ID_DETALHE_COMISSAO_VENDEDOR") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlComissoes" TargetControlID="lkComissoes" CancelControlID="btnFecharComissoes" OkControlID="lkTabelaComissoes"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlComissoes" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>
                                        <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">COMISSÕES</h5>
                                                </div>
                                                <div class="modal-body" style="padding-left: 50px;">
                                                    <div class="row">
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkTabelaComissoes" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Tabela de Comissões</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkCadastrarSubVendedor" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" href="CadastrarSubVendedor.aspx" target="_blank">Sub-Vendedor</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkGerarComissao" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Gerar Comissões</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <%-- Comentado o item antigo de equipes 
                                    
                                                                <div class="row">
                                                                 <div class="col-sm-10">
                                                                <div class="form-group">                                          
                            <asp:LinkButton ID="lkEquipe" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" href="CadastrarEquipe.aspx" target="_blank" Visible="false">Equipes</asp:LinkButton>
                                                                </div>
                                                                    </div>
                                                                     </div>
                                                                Comentado o item antigo de equipes  --%>


                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkEquipeVendedor" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" href="CadastrarEquipeVendedor.aspx" target="_blank">Equipes</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkCadastroMeta" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Cadastro de Meta</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkMeta" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Meta</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>





                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkAjustarComissao" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" Visible="false">Ajustar Comissões</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharComissoes" Text="Close" />
                                                </div>
                                            </div>
                                        </div>
                                    </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlRelatorios" TargetControlID="lkRelatorios" CancelControlID="btnFecharRelatorios"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlRelatorios" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>
                                        <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">RELATORIOS</h5>
                                                </div>
                                                <div class="modal-body" style="padding-left: 50px;">
                                                    <div class="row">
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">

                                                                    <asp:LinkButton ID="lkRelPorVendedor" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Vendedor Selecionado</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">

                                                                    <asp:LinkButton ID="lkRelTodosVendedores" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" OnClientClick="RelTodosVendedores()">Todos os Vendedores</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkComDisparoEmail" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" OnClientClick="RelPorEmail()">Com Disparo de Email</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkRelEquipe" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" Visible="false">Equipes por Competência</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkRelComissaoVendas" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Comissão de Vendas</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkRelComissaoProspecao" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Comissão Prospeção</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="lkRelComissaoIndicacaoInterna" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Comissão Ind. Interna</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRelatorios" Text="Close" />
                                                </div>
                                            </div>
                                        </div>
                                    </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="mpeTabelas" runat="server" PopupControlID="pnlTabelas" TargetControlID="lkTabelaComissoes" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlTabelas" runat="server" CssClass="modalPopup">
                                            <center>
                                                    <div class=" modal-dialog modal-dialog-centered modal-xxl" role="document">
                                                    <div class="modal-content">
                                                        <div id="tabs">
                                                            <ul class="nav nav-tabs" role="tablist">
                                                                <li class="active">
                                                                    <a href="#Comissao" id="hdComissao" role="tab" data-toggle="tab">
                                                                        <i class="fa fa-money-bill-alt" style="padding-right: 8px;"></i>Tabela Comissão de Vendas 
                                                                    </a>
                                                                </li>
                                                                <li>
                                                                    <a href="#Prospeccao" id="hdProspeccao" role="tab" data-toggle="tab">
                                                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Tabela de Prospecção
                                                                    </a>
                                                                </li>
                                                                <li>
                                                                    <a href="#IndicacaoInt" id="hdIndicacaoInt" role="tab" data-toggle="tab">
                                                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Tabela de Indicação Interna
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                            
                                                        <%--Tabela comissoes de vendas --%>
                                                        <div class="tab-pane fade in" id="Comissao">
                                                            <div class="modal-header">
                                                                <h5 class="modal-title">TABELA DE COMISSÕES DE VENDAS</h5>
                                                            </div>
                                                            <div class="modal-body">

                                                                <div class="alert alert-success" ID="divComissoesVendaSucesso" runat="server" visible="false">
                                                                    <asp:label ID="lblComissoesVendaSucesso" runat="server" Text="Registro deletado com sucesso!"></asp:label>
                                                                </div>
 
                                                                <div class="alert alert-danger" ID="divComissoesVendaErro" runat="server" visible="false">
                                                                    <asp:label ID="lblComissoesVendaErro" runat="server" ></asp:label>
                                                                </div>


                                                                <div class="row">
                                                                    <div class="col-sm-1">
                                                                        <span class="font-bold text-align-left">ID:</span> <br />
                                                                        <asp:TextBox ID="txtIDComissoesVenda" runat="server" CssClass="form-control" Enabled="false"/>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <span class="font-bold text-align-left">Validade Inicial:</span> <br />
                                                                        <asp:TextBox ID="txtValidadaInicialComissoesVenda" runat="server" CssClass="form-control" />                                                                
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <span class="font-bold text-align-left">Estufagem: </span><br />
                                                                        <asp:DropDownList ID="ddlEstufagemComissoesVenda" runat="server" CssClass="form-control" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                                                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <span class="font-bold text-align-left">Via:</span> <br />
                                                                        <asp:DropDownList ID="ddlViaComissoesVenda" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" >
                                                                        </asp:DropDownList>                                                                        
                                                                    </div>                                                                                                                          
                                                                    <div class="col-sm-2">
                                                                        <span class="font-bold text-align-left">Tipo de Cálculo: </span> <br />
                                                                        <asp:DropDownList ID="ddlTipoCalculoComissoesVenda" runat="server" CssClass="form-control"  DataTextField="NM_TIPO_CALCULO" DataSourceID="dsTipoCalculo" DataValueField="ID_TIPO_CALCULO">
                                                                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <span class="font-bold text-align-left">Base de Cálculo: </span><br />
                                                                        <asp:DropDownList ID="ddlBaseCalculoComissoesVenda" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                                                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    
                                                                </div>
                                                                <hr />
                                                                <div class="row">  
                                                                    <div class="col-sm-2">
                                                                        <span class="font-bold text-align-left">Valor: </span> <br />
                                                                        <asp:TextBox ID="txtValorComissoesVenda" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                   <br />      Escalonado por Profit Brasil
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        De<br />
                                                                        <asp:TextBox ID="txtProfitInicialComissoesVenda" CssClass="form-control" runat="server" placeholder="USD 0.00"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        Até: <br />
                                                                        <asp:TextBox ID="txtProfitFinalComissoesVenda" CssClass="form-control" runat="server"  placeholder="USD 0.00"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        Valor de Comissão: 
                                                                        <asp:TextBox ID="txtCalculadoComissoesVenda" CssClass="form-control" runat="server"  placeholder="R$ 0.00"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                         <br />
                                                                        <asp:Button ID="btnSalvarComissaoVendedor" CssClass="btn btn-block btn-success" Text="Salvar Tabela" runat="server" />
                                                                    </div>
                                                                                                                                            

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <div id="Div3" runat="server" visible="false" class="alert alert-danger">
                                                                                <asp:Label ID="Label35" Text="" runat="server" />
                                                                            </div>
                                                                            <div id="div4" runat="server" visible="false" class="alert alert-success">
                                                                                <asp:Label ID="Label36" Text="" runat="server" />
                                                                            </div>
                                                                            <asp:GridView ID="dgvTabelaComissaoVendedor" CssClass="table table-hover table-sm grdViewTable" GridLines="None" DataSourceID="dsTabelaComissaoVendedor" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="ID_VENDEDOR_TAXA_COMISSAO" HeaderText="ID" SortExpression="ID_VENDEDOR_TAXA_COMISSAO" />
                                                                                    <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Validade Inicial" SortExpression="DT_VALIDADE_INICIAL" />
                                                                                    <asp:BoundField DataField="NM_TIPO_ESTUFAGEM" HeaderText="Tipo de Estufagem" SortExpression="NM_TIPO_ESTUFAGEM" />                                                                                                                                                                        
                                                                                    <asp:BoundField DataField="NM_VIATRANSPORTE" HeaderText="VIA" SortExpression="NM_VIATRANSPORTE" />
                                                                                    <asp:BoundField DataField="NM_BASE_CALCULO_TAXA" HeaderText="Base de Calculo" SortExpression="NM_BASE_CALCULO_TAXA" />
                                                                                    <asp:BoundField DataField="VL_TAXA" HeaderText="Valor" SortExpression="VALOR" />                                                                         <asp:BoundField DataField="VL_PROFIT_INICIO" HeaderText="USD - DE" SortExpression="VL_PROFIT_INICIO" />
                                                                                    <asp:BoundField DataField="VL_PROFIT_FIM" HeaderText="USD - ATÉ" SortExpression="VL_PROFIT_FIM" />
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton CommandName="Editar" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"  CommandArgument='<%# Eval("ID_VENDEDOR_TAXA_COMISSAO") %>'><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir" CommandArgument='<%# Eval("ID_VENDEDOR_TAXA_COMISSAO") %>'  OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="headerStyle" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-11"></div>
                                                                    <div class="col-md-1">
                                                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharComissoesVenda" Text="Close"  />

                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                             
                                                        

                                                        <%--Tabela Prospeccao --%>
                                                        <div class="tab-pane fade" id="Prospeccao" style="display:none" >
                                                            <div class="modal-header">
                                                                <h5 class="modal-title">TABELA DE COMISSÕES PROSPECÇÃO</h5>
                                                            </div>

                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                      <ContentTemplate>
                                                            <div class="modal-body">
                                                                  <div class="alert alert-success" ID="divComissoesProspeccaoSucesso" runat="server" visible="false">
                                                                    <asp:label ID="lblComissoesProspeccaoSucesso" runat="server" Text="Registro deletado com sucesso!"></asp:label>
                                                                </div>
 
                                                                <div class="alert alert-danger" ID="divComissoesProspeccaoErro" runat="server" visible="false">
                                                                    <asp:label ID="lblComissoesProspeccaoErro" runat="server" ></asp:label>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-1">
                                                                        <strong>ID:</strong> <br />
                                                                        <asp:TextBox ID="txtIDComissoesProspeccao" runat="server" CssClass="form-control" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <strong>Validade:</strong> <br />
                                                                       <asp:TextBox ID="txtValidadeInicialComissoesProspeccao" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                       <strong>Tipo Estufagem:</strong> <br />
                                                                        <asp:DropDownList ID="ddlEstufagemComissoesProspeccao" runat="server" CssClass="form-control" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                      <strong> Via:</strong> <br />
                                                                        <asp:DropDownList ID="ddlViaComissoesProspeccao" runat="server" CssClass="form-control" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE">
                                                                        </asp:DropDownList>                                                                        
                                                                    </div>
                                                                    <div class="col-sm-2" style="border:1px ridge; text-align:left">                                                                        
                                                                     <%--<asp:RadioButtonList ID="rdPrimeiroProcessoComissaoProspeccao" runat="server" Text =" &nbsp; 1º Processo" /><br/>
                                                                        <asp:RadioButton ID="rdDemaisProcessosComissaoProspeccao" runat="server" Text =" &nbsp; Demais Processos" />--%>      
                                                                        <asp:RadioButtonList ID="rdPagamentoComissoesProspeccao" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">&nbsp; 1º Processo</asp:ListItem>
                                                        <asp:ListItem Value="1">&nbsp; Demais Processos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                                    </div>                                                                    
                                                                    <div class="col-sm-2">
                                                                        <strong>Tipo de Cálculo:</strong> <br />
                                                                        <asp:DropDownList ID="ddlTipoCalculoComissoesProspeccao" runat="server" CssClass="form-control" DataTextField="NM_TIPO_CALCULO" DataSourceID="dsTipoCalculo" DataValueField="ID_TIPO_CALCULO">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    
                                                                    
                                                                </div>
                                                          
                                                                <div class="row">
                                                                    <div class="col-sm-2">
                                                                        <strong>Base de Cálculo:</strong> <br />
                                                                        <asp:DropDownList ID="ddlBaseCalculoComissoesProspeccao" runat="server" CssClass="form-control" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <strong>Valor:</strong> <br />
                                                                        <asp:TextBox ID="txtValorComissoesProspecccao" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-4"> 
                                                                        <strong>Equipe:</strong> <br />
                                                                        <asp:DropDownList ID="ddlEquipeComissoesProspeccao" runat="server" CssClass="form-control" DataTextField="NM_EQUIPE" DataSourceID="dsEquipes" DataValueField="ID_EQUIPE">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                     <div class="col-sm-2"><br/>
                                                                        <asp:Button ID="btnSalvarComissoesProspeccao" CssClass="btn btn-block btn-success" Text="Gravar" runat="server"/>
                                                                    </div>
                                                                    <div class="col-sm-2"><br/>
                                                                        <asp:Button ID="btnLimparComissoesProspeccao" CssClass="btn btn-block btn-warning" Text="Limpar" runat="server"/>
                                                                    </div>
                                                                </div>
                                                              <br />
                                                              <br />
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                             
                                                                            <asp:GridView ID="dgvTabelaComissaoProspeccao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" DataSourceID="dsTabelaComissaoProspeccao" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="ID" DataField="ID_VENDEDOR_PROSPECCAO" SortExpression="ID_VENDEDOR_PROSPECCAO" />
                                                                                    <asp:BoundField HeaderText="Validade Inicial" DataField="DT_VALIDADE_INICIAL" SortExpression="DT_VALIDADE_INICIAL" />
                                                                                    <asp:BoundField HeaderText="Equipe" DataField="NM_EQUIPE" SortExpression="NM_EQUIPE" />
                                                                                    <asp:BoundField HeaderText="Estufagem" DataField="NM_TIPO_ESTUFAGEM" SortExpression="NM_TIPO_ESTUFAGEM" />
                                                                                    <asp:BoundField HeaderText="Via" DataField="NM_VIATRANSPORTE" SortExpression="NM_VIATRANSPORTE" />
                                                                                    <asp:BoundField HeaderText="Processo" DataField="FL_PAGAMENTO_RECORRENTE" SortExpression="FL_PAGAMENTO_RECORRENTE" />
                                                                                    <asp:BoundField HeaderText="Tipo de Calculo" DataField="NM_TIPO_CALCULO" SortExpression="NM_TIPO_CALCULO" />
                                                                                    <asp:BoundField HeaderText="Base de Calculo" DataField="NM_BASE_CALCULO_TAXA" SortExpression="NM_BASE_CALCULO_TAXA" />
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton CommandName="Editar" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"  CommandArgument='<%# Eval("ID_VENDEDOR_PROSPECCAO") %>'><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"  CommandArgument='<%# Eval("ID_VENDEDOR_PROSPECCAO") %>'
                                                                                                OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');" Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
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
                                                            <div class="modal-footer">
                                                                <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharComissoesProspecao" Text="Close" />
                                                            </div>
                                                  </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarComissoesProspeccao" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimparComissoesProspeccao" />
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharComissoesProspecao" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTabelaComissaoProspeccao" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                                          
                                                          </div>
                                                            
                                                            
                                                             

                                                        <%--Tabela Indicacao Interno --%>
                                                        <div class="tab-pane fade" id="IndicacaoInt" style="display:none;">
                                                            <div class="modal-header">
                                                                <h5 class="modal-title">TABELA DE INDICADOR INTERNO</h5>
                                                            </div>
                                                             <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                      <ContentTemplate>
                                                            <div class="modal-body">
                                                                <div class="alert alert-success" id="divIndicacaoInternoSucesso" runat="server" visible="false">
                                                                    <asp:Label ID="lblIndicacaoInternoSucesso" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="alert alert-danger" id="divIndicacaoInternoErro" runat="server" visible="false">
                                                                    <asp:Label ID="lblIndicacaoInternoErro" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="row">
                                                                <div class="col-md-2">
                                                                        <strong>ID:</strong> <br />
                                                                        <asp:TextBox ID="txtIDIndicadorInterno" runat="server" CssClass="form-control" Enabled="false" />
                                                                    </div>
                                                                    
                                                                        <div class="col-md-3">
                                                                        Validade<br />
                                                                        <asp:TextBox ID="txtValidadeIndicadorInterno" CssClass="form-control data" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        Valor <br />
                                                                        <asp:TextBox ID="txtValorIndicadorInterno" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-3"><br />
                                                                        <asp:Button ID="btnSalvarIndicadorInterno" CssClass="btn btn-success" Text="Gravar" runat="server" />
                                                                        <asp:Button ID="btnLimparIndicadorInterno" CssClass="btn btn-warning" Text="Limpar" runat="server" />
                                                                     </div>
                                                                 </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="dgvIndicadorInterno" CssClass="table table-hover table-sm grdViewTable" GridLines="None" DataSourceID="dsIndicadorInterno" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="ID_VENDEDOR_INDICADOR_INTERNO" HeaderText="ID" SortExpression="ID_VENDEDOR_INDICADOR_INTERNO" />
                                                                                    <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Validade" SortExpression="DT_VALIDADE_INICIAL" />
                                                                                    <asp:BoundField DataField="VL_TAXA"  HeaderText="Valor Indicação" SortExpression="VL_TAXA" />
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton CommandName="Editar" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar" CommandArgument='<%# Eval("ID_VENDEDOR_INDICADOR_INTERNO") %>'><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("ID_VENDEDOR_INDICADOR_INTERNO") %>' CommandName="Excluir"
                                                                                                OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');" Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="headerStyle" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-10"></div>
                                                                    <div class="col-md-2">                                                                        
                                                                        <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharIndicadorInterno" Text="Close" />
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                       
                                                          </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarIndicadorInterno" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimparIndicadorInterno" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvIndicadorInterno" />
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharIndicadorInterno" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                                          </div>
                                                 
                                                        </div>

                                                </div>                                               
                                                </div>    
                                            </center>
                                        </asp:Panel>


                                <ajaxToolkit:ModalPopupExtender ID="mpeCadastroMetas" runat="server" PopupControlID="pnlCadastroMeta" TargetControlID="lkCadastroMeta" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePnlCadastroMeta" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlCadastroMeta" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>
                                                    <div class="modal-dialog modal-dialog-centered modal-xxl" role="document">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <h5 class="modal-title font-bold">TABELA CADASTRO DE META</h5>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div id="divCadastroMetaErro" runat="server" visible="false" class="alert alert-danger">
                                                                    <asp:Label ID="lblCadastroMetaErro" Text="" runat="server" />
                                                                </div>
                                                                <div id="divCadastroMetaSucesso" runat="server" visible="false" class="alert alert-success">
                                                                    <asp:Label ID="lblCadastroMetaSucesso" Text="" runat="server" />
                                                               </div>
                                                                <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                                                                    <div class="col-md-1">
                                                                        <strong>ID:</strong> <br />
                                                                        <asp:TextBox ID="txtIDCadastroMeta" runat="server" CssClass="form-control" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <span class="font-bold text-align-left">VALIDADE</span> <br />
                                                                        <asp:TextBox ID="txtValidadeCadastroMeta" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <span class="font-bold text-align-left">TIPO DE PRODUTO </span><br />
                                                                        <asp:DropDownList ID="ddlEstufagemCadastroMeta" runat="server" CssClass="form-control font-size-11" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem" DataValueField="ID_TIPO_ESTUFAGEM"></asp:DropDownList>                                        
                                                                   </div>
                                                                  <div class="col-md-2">
                                                                      <span class="font-bold text-align-left">VIA </span><br />
                                                                        <asp:DropDownList ID="ddlViaCadastroMeta" runat="server" CssClass="form-control font-size-11"  DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" ></asp:DropDownList>
                                                                  </div>                                      
                                                                   <div class="col-md-1">
                                                                       <span class="font-bold text-align-left">META MÍN.</span> <br />
                                                                       <asp:TextBox ID="txtMetaMinimaCadastroMeta" runat="server" CssClass="form-control"></asp:TextBox>
                                                                   </div>
                                                                    <div class="col-md-1">
                                                                        <span class="font-bold text-align-left">META MÁX. </span><br />
                                                                       <asp:TextBox ID="txtMetaMaximaCadastroMeta" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <span class="font-bold text-align-left">VALOR META</span> <br />
                                                                        <asp:TextBox ID="txtValorCadastroMeta" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2"><br />
                                                                        <asp:Button ID="btnSalvarCadastroMeta" runat="server" CssClass="btn btn-success" Text="Gravar"  />
                                                                        <asp:Button ID="btnLimparCadastroMeta" runat="server" CssClass="btn btn-warning" Text="Limpar" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                        <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                        <div class="col-sm-12">
                                                                            <div class="form-group">
                                                                                
                                                                                <asp:GridView ID="dgvCadastroMeta" CssClass="table table-hover table-sm grdViewTable" GridLines="None" DataSourceID="dsCadastroMetas" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="ID_VENDEDOR_METAS" HeaderText="ID" SortExpression="ID_VENDEDOR_METAS" />
                                                                                        <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="VALIDADE" SortExpression="DT_VALIDADE_INICIAL" />
                                                                                        <asp:BoundField DataField="NM_TIPO_ESTUFAGEM" HeaderText="TIPO DE PRODUTO" SortExpression="NM_TIPO_ESTUFAGEM" />                                              <asp:BoundField DataField="NM_VIATRANSPORTE" HeaderText="VIA" SortExpression="NM_VIATRANSPORTE" />
                                                                                        <asp:BoundField DataField="VL_META_MIN" HeaderText="META MÍNIMA" SortExpression="VL_META_MIN" />
                                                                                        <asp:BoundField DataField="VL_META_MAX" HeaderText="META MÁXIMA" SortExpression="VL_META_MAX" />
                                                                                        <asp:BoundField DataField="VL_META" HeaderText="VALOR DA META " SortExpression="VALOR_META" />
                                                                                          <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton CommandName="Editar" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"  CommandArgument='<%# Eval("ID_VENDEDOR_METAS") %>'><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                                    </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"  CommandArgument='<%# Eval("ID_VENDEDOR_METAS") %>'
                                                                                                    OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');" Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
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
                                                           <div class="modal-footer">
                                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCadastrarMetas" Text="Close" />
                                                                </div>
                                                           </div>
                                                        </div>
                                                    </div>
                                                </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                     <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarCadastroMeta" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimparCadastroMeta" />
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharCadastrarMetas" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCadastroMeta" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <ajaxToolkit:ModalPopupExtender ID="mpeGerarMetasAlcancadas" runat="server" PopupControlID="pnlMetasAlcancadas" TargetControlID="lkMeta" CancelControlID="btnFecharMetasAlcancadas"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanelMetasAlcancadas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMetasAlcancadas" CssClass="modalPopUp" runat="server" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-xxl" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">METAS</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                             <div id="divMetasAlcancadasErro" runat="server" visible="false" class="alert alert-danger">
                                                                <asp:Label ID="lblMetasAlcancadasErro" Text="" runat="server" />
                                                             </div>
                                                              <div id="divMetasAlcancadasSucesso" runat="server" visible="false" class="alert alert-success">
                                                                <asp:Label ID="lblMetasAlcancadasSucesso" Text="" runat="server" />
                                                              </div>
                                                            <div class="row">
                                                                <div class="col-md-2">
                                                                    Data Início: 
                                                                    <asp:TextBox ID="txtDataInicioMetasAlcancadas" runat="server" CssClass="form-control" placeholder="__/__/____"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    Data Término: 
                                                                    <asp:TextBox ID="txtDataTerminoMetasAlcancadas" runat="server" CssClass="form-control" placeholder="__/__/____">
                                                                    </asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4"><br />
                                                                    <asp:Button ID="btnGerarMetasAlcancadas" CssClass="btn btn-success" Text="GERAR COMPETÊNCIA DE METAS ALCAÇANDAS" runat="server" />
                                                                </div>                                                                
                                                                <div class="col-md-4"><br />
                                                                    <asp:Button ID="btnLimparGeradorMetasAlcancadas" CssClass="btn btn-secondary" Text="LIMPAR" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                       
                                                                        <asp:GridView ID="dgvMetasAlcancadas" CssClass="table table-sm grdViewTable" GridLines="None" DataSourceID="dsTabelaComissaoVendedor" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="Vendedor" SortExpression="VENDEDOR" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Processo" SortExpression="PROCESSO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="Cliente" SortExpression="CLIENTE" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Serviço" SortExpression="SERVICO" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Via" SortExpression="VIA" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Tipo de Estufagem" SortExpression="TIPO_ESTUFAGEM" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Qtde BL" SortExpression="QTDE_BL"  HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Qtde CNTR" SortExpression="QTDE_CNTR"  HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="Valor adicional" SortExpression="VALOR_ADICIONAL"  HeaderStyle-CssClass="header-blue" />                                                                                
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="headerStyle" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharMetasAlcancadas" Text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnValidarMetasAlcancadas" Text="Close" />
                                                        </div>
                                                   </div>
                                                </div>
                                        </center>
                                        </asp:Panel>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvMetasAlcancadas" /> 
                                        <asp:AsyncPostBackTrigger ControlID="btnGerarMetasAlcancadas" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimparGeradorMetasAlcancadas" />
                                        <asp:AsyncPostBackTrigger ControlID="btnValidarMetasAlcancadas" />
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharMetasAlcancadas" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender9" runat="server" PopupControlID="pnlRelComissaoVendas" TargetControlID="lkRelComissaoVendas" CancelControlID="btnFecharRelComissaoVendas"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdateRelComissaoVendas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlRelComissaoVendas" CssClass="modalPopUp" runat="server" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-xxl" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title font-bold">Relatório - Comissão de Vendas</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row">
                                                                <div class="col-md-2">
                                                                    Data Início: 
                                                                    <asp:TextBox ID="txtDtInicioRelComissaoVendas" runat="server" CssClass="form-control" placeholder="__/__/____"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    Data Término: 
                                                                    <asp:TextBox ID="txtDtTerminoRelComissaoVendas" runat="server" CssClass="form-control" placeholder="__/__/____"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2"><br />
                                                                    <asp:Button ID="btnFiltrarRelComissaoVendas" CssClass="btn btn-success" Text="FILTRAR" runat="server" />
                                                                </div>
                                                                
                                                                <div class="col-md-2"><br />
                                                                    <asp:Button ID="btnLimparRelComissaoVendas" CssClass="btn btn-warning" Text="LIMPAR" runat="server" />
                                                                </div>

                                                                <div class="col-md-4">Ações<br />
                                                                    <asp:Button ID="btnRelGerarCompetenciaComissaoVendas" CssClass="btn btnn btn-default" Text="Gerar a competência" runat="server" />
                                                                    <asp:Button ID="btnRelGravarCCProcessoComissaoVendas" CssClass="btn btnn btn-default" Text="Gravar no CC do Processo" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <div id="Div11" runat="server" visible="false" class="alert alert-danger">
                                                                            <asp:Label ID="Label43" Text="" runat="server" />
                                                                        </div>
                                                                        <div id="div12" runat="server" visible="false" class="alert alert-success">
                                                                            <asp:Label ID="Label44" Text="" runat="server" />
                                                                        </div>
                                                                        <asp:GridView ID="GridView6" CssClass="table table-sm grdViewTable" GridLines="None" DataSourceID="dsTabelaComissaoVendedor" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="COMP." SortExpression="COMP" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="PROCESSO" SortExpression="PROCESSO"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="NF" SortExpression="NF"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VENDEDOR" SortExpression="VENDEDOR"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="CLIENTE" SortExpression="CLIENTE"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="SERVIÇO" SortExpression="SERVICO"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VIA" SortExpression="VIA"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="ESTUFAGEM" SortExpression="ESTUFAGEM"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="BL" SortExpression="BL"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="CNTR" SortExpression="CNTR"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VL.COMISSÃO" SortExpression="VL_COMISSAO"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VL.COMISSÃO TOTAL" SortExpression="VL_COMISSAO_TOTAL"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="ADD META" SortExpression="ADD_META"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="TOTAL COMISSÃO" SortExpression="TOTAL_COMISSAO"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="LIQUIDAÇÃO" SortExpression="LIQUIDACAO"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="OBSERVAÇÂO" SortExpression="OBSERVACAO"  HeaderStyle-CssClass="header-blue"/>                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="Profit negativo" SortExpression="PROFIT_NEGATIVO"  HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="headerStyle" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRelComissaoVendas" Text="Close" />
                                                        </div>
                                                   </div>
                                                </div>
                                        </center>
                                        </asp:Panel>

                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="btnRelGerarCompetenciaComissaoVendas" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRelGravarCCProcessoComissaoVendas" />

                                    </Triggers>
                                </asp:UpdatePanel>

                                <ajaxToolkit:ModalPopupExtender ID="mpeTabelas0" runat="server" PopupControlID="pnlRelComissaoProspecao" TargetControlID="lkRelComissaoProspecao" CancelControlID="btnFecharRelComissaoProspecao"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdateRelComissaoProspecao" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlRelComissaoProspecao" CssClass="modalPopUp" runat="server" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-xxl" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title font-bold" style="text-transform:uppercase;">Relatório Comissão de Prospeção</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row">
                                                                <div class="col-md-2">
                                                                    Data Início: 
                                                                    <asp:TextBox ID="txtDtInicioComissaoProspecao" runat="server" CssClass="form-control" placeholder="__/__/____"></asp:TextBox>
                                                                    <asp:Label ID="lblErroDataInicio" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    Data Término: 
                                                                    <asp:TextBox ID="txtDtTerminoComissaoProspecao" runat="server" CssClass="form-control" placeholder="__/__/____">  </asp:TextBox>
                                                                    <asp:Label ID="lblErroDataTermino" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-md-2"><br />
                                                                    <asp:Button ID="btnFiltrarComissaoProspecao" CssClass="btn btn-success" Text="FILTRAR" runat="server"  />
                                                                </div>
                                                                
                                                                <div class="col-md-1"><br />
                                                                    <asp:Button ID="btnLimparComissaoProspecao" CssClass="btn btn-warning" Text="LIMPAR" runat="server" />
                                                                </div>

                                                                <div class="col-md-5">Ações<br />
                                                                    <asp:Button ID="btnGerarCompetenciaComissaoProspecao" CssClass="btn btnn btn-default btn-sm" Text="Gerar a competência" runat="server" />
                                                                    <asp:Button ID="btnGravarCCProcessoComissaoProspecao" CssClass="btn btnn btn-default btn-sm" Text="Gravar no CC do Processo" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <div id="Div13" runat="server" visible="false" class="alert alert-danger">
                                                                            <asp:Label ID="Label45" Text="" runat="server" />
                                                                        </div>
                                                                        <div id="div14" runat="server" visible="false" class="alert alert-success">
                                                                            <asp:Label ID="Label46" Text="" runat="server" />
                                                                        </div>
                                                                        <asp:GridView ID="GridView7" CssClass="table table-sm grdViewTable" GridLines="None" DataSourceID="dsTabelaComissaoVendedor" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="COMP." SortExpression="COMP" HeaderStyle-CssClass="header-blue" HeaderStyle-Height="40"  />
                                                                                <asp:BoundField HeaderText="PROCESSO" SortExpression="PROCESSO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="NF" SortExpression="NF" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="PROSPECÇÃO" SortExpression="PROSPECCAO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="CLIENTE" SortExpression="CLIENTE" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="SERVIÇO" SortExpression="SERVICO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VIA" SortExpression="VIA" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="ESTUFAGEM" SortExpression="ESTUFAGEM" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="BL" SortExpression="BL" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="CNTR" SortExpression="CNTR" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VL.COMISSÃO" SortExpression="VL_COMISSAO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VL.COMISSÃO TOTAL" SortExpression="VL_COMISSAO_TOTAL" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="LIQUIDAÇÃO" SortExpression="LIQUIDACAO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                                                                                                        
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="headerStyle" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRelComissaoProspecao" Text="Close" />
                                                        </div>
                                                   </div>
                                                </div>
                                        </center>
                                        </asp:Panel>

                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="btnGerarCompetenciaComissaoProspecao" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGravarCCProcessoComissaoProspecao" />

                                    </Triggers>
                                </asp:UpdatePanel>

                                <ajaxToolkit:ModalPopupExtender ID="mpeTabelas1" runat="server" PopupControlID="pnlRelComissaoIndicacaoInterna" TargetControlID="lkRelComissaoIndicacaoInterna" CancelControlID="btnFecharRelComissaoIndicacaoInterna"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdateRelComissaoIndicacaoInterna" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlRelComissaoIndicacaoInterna" CssClass="modalPopUp" runat="server" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-xxl" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title font-bold" style="text-transform:uppercase">Relatório Comissão de Indicação Interna</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row">
                                                                <div class="col-md-2">
                                                                    Data Início: 
                                                                    <asp:TextBox ID="txtDtInicioRelIndicacaoInterna" runat="server" CssClass="form-control" placeholder="__/__/____"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    Data Término: 
                                                                    <asp:TextBox ID="txtDtTerminoRelIndicacaoInterna" runat="server" CssClass="form-control" placeholder="__/__/____"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2"><br />
                                                                    <asp:Button ID="btnFiltrarRelIndicacaoInterna" CssClass="btn btn-success btn-sm" Text="FILTRAR" runat="server" />
                                                                </div>
                                                                
                                                                <div class="col-md-1"><br />
                                                                    <asp:Button ID="btnLimparRelIndicacaoInterna" CssClass="btn btnn btn-warning btn-sm" Text="LIMPAR" runat="server" />
                                                                </div>

                                                                <div class="col-md-5">Ações<br />
                                                                    <asp:Button ID="btnRelGerarCompetenciaComissaoIndicacaoInterna" CssClass="btn btnn btn-default btn-sm" Text="Gerar a competência" runat="server" />
                                                                    <asp:Button ID="btnRelGravarCCProcessoComissaoIndicacaoInterna" CssClass="btn btnn btn-default btn-sm" Text="Gravar no CC do Processo" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12" style="padding-bottom:20px;"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <div id="Div15" runat="server" visible="false" class="alert alert-danger">
                                                                            <asp:Label ID="Label47" Text="" runat="server" />
                                                                        </div>
                                                                        <div id="div16" runat="server" visible="false" class="alert alert-success">
                                                                            <asp:Label ID="Label48" Text="" runat="server" />
                                                                        </div>
                                                                        <asp:GridView ID="GridView8" CssClass="grdViewTable" GridLines="None" DataSourceID="dsTabelaComissaoVendedor" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" ShowHeader="true" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="COMP." SortExpression="COMP" HeaderStyle-CssClass="header-blue" />
                                                                                <asp:BoundField HeaderText="PROCESSO" SortExpression="PROCESSO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="NF" SortExpression="NF" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="COLABORADOR" SortExpression="COLABORADOR" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="VALOR" SortExpression="VALOR" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                                                                                                        
                                                                                <asp:BoundField HeaderText="LIQUIDAÇÃO" SortExpression="LIQUIDACAO" HeaderStyle-CssClass="header-blue" />                                                                                                                                                                                                                                                        
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="headerStyle" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRelComissaoIndicacaoInterna" Text="Close" />
                                                        </div>
                                                   </div>
                                                </div>
                                        </center>
                                        </asp:Panel>

                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="btnRelGerarCompetenciaComissaoIndicacaoInterna" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRelGravarCCProcessoComissaoIndicacaoInterna" />

                                    </Triggers>
                                </asp:UpdatePanel>


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlGerarComissao" TargetControlID="lkGerarComissao" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlGerarComissao" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">GERAR COMISSÕES</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccessGerarComissao" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccessGerarComissao" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErroGerarComissao" runat="server" visible="false">
                                                                <asp:Label ID="lblErroGerarComissao" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-info" id="divInfoGerarComissao" runat="server" visible="false">
                                                                <asp:Label ID="lblInfoGerarComissao" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-warning" id="divAtencaoGerarComissao" runat="server" visible="false">
                                                                <asp:Label ID="lblAtencaoGerarComissao" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label11" runat="server">Competência</asp:Label><br />

                                                                        <asp:TextBox ID="txtNovaCompetencia" AutoPostBack="true" placeholder="MM/AAAA" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label12" runat="server">Data Liquidação(Inicial)</asp:Label><br />

                                                                        <asp:TextBox ID="txtLiquidacaoInicial" runat="server" AutoPostBack="true" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label14" runat="server">Data Liquidação(Final)</asp:Label><br />

                                                                        <asp:TextBox ID="txtLiquidacaoFinal" runat="server" CssClass="form-control data"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6" style="border: ridge 1px;">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label18" runat="server"><strong>Taxa LCL</strong></asp:Label><br />

                                                                        <asp:Label ID="lblLCL" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6" style="border: ridge 1px;">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label19" runat="server"><strong>Taxa FCL</strong></asp:Label><br />

                                                                        <asp:Label ID="lblFCL" runat="server" />

                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        VENDEDORES DIRETOS:                                          
                                                                <asp:GridView ID="dgvVendedor" DataKeyNames="ID_PARCEIRO" DataSourceID="dsVendedor" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_PARCEIRO") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="NM_RAZAO" HeaderText="NOME" SortExpression="NM_RAZAO" />
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="headerStyle" />
                                                                </asp:GridView>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6" style="display: none">
                                                                    <div class="form-group">
                                                                        EQUIPE INSIDE SALES                                        
                                          
                                                                          <asp:GridView ID="dgvEquipe" DataKeyNames="ID_PARCEIRO" DataSourceID="dsEquipe" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_PARCEIRO") %>' />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="NM_RAZAO" HeaderText="NOME" SortExpression="NM_RAZAO" />
                                                                              </Columns>
                                                                              <HeaderStyle CssClass="headerStyle" />
                                                                          </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:Button runat="server" Text="Gerar" ID="btnGerarComissao" CssClass="btn btn-success" OnClientClick="MouseWait(); return true;" />
                                                                <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharGerarComissao" Text="Close" />


                                                            </div>

                                                        </div>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtLiquidacaoInicial" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGerarComissao" />
                                        <asp:AsyncPostBackTrigger ControlID="txtNovaCompetencia" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="pnlAjustarComissao" TargetControlID="lkAjustarComissao" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlAjustarComissao" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>
                                                <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">AJUSTAR COMISSÕES</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="alert alert-success" id="divSuccesAjuste" runat="server" visible="false">
                                                                <asp:Label ID="lblSuccesAjuste" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-danger" id="divErroAjuste" runat="server" visible="false">
                                                                <asp:Label ID="lblErroAjuste" runat="server"></asp:Label>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label33" runat="server">ID</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtIDAjuste" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label29" runat="server">Serviço</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:DropDownList ID="ddlAjusteServico" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_SERVICO" DataSourceID="dsServico" DataValueField="ID_SERVICO" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label13" runat="server">Processo</asp:Label><label runat="server" style="color: red">*</label><br />

                                                                        <asp:TextBox ID="txtAjusteProcesso" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label15" runat="server">Nota Fiscal</asp:Label><label runat="server" style="color: red">*</label><br />

                                                                        <asp:TextBox ID="txtAjusteNotaFiscal" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label16" runat="server">Data NF</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtAjusteDataNota" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label22" runat="server">Estufagem:</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:DropDownList ID="ddlAjusteEstufagem" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem" DataValueField="ID_TIPO_ESTUFAGEM" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label17" runat="server">Vendedor:</asp:Label><label runat="server" style="color: red">*</label><br />

                                                                        <asp:DropDownList ID="ddlAjusteVendedor" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_RAZAO" DataSourceID="dsVendedores" DataValueField="ID_PARCEIRO" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label21" runat="server">Cliente:</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:DropDownList ID="ddlAjusteCliente" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_RAZAO" DataSourceID="dsCliente" DataValueField="ID_PARCEIRO" />
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label23" runat="server">Comissão Base</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtAjusteBase" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label24" runat="server">Qtd. BL</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtAjusteQtdBl" runat="server" AutoPostBack="true" CssClass="form-control inteiro"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label25" runat="server">Qtd. CNTR</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtAjusteQtdCNTR" runat="server" CssClass="form-control inteiro"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label30" runat="server">%</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtAjustePorcentagem" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label31" runat="server">Comissão Total</asp:Label><label runat="server" style="color: red">*</label><br />

                                                                        <asp:TextBox ID="txtAjusteTotal" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-2">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label32" runat="server">Data Liquidação</asp:Label><label runat="server" style="color: red">*</label><br />
                                                                        <asp:TextBox ID="txtAjusteLiquidacao" runat="server" CssClass="form-control data"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label26" runat="server">Observação</asp:Label><br />
                                                                        <asp:TextBox ID="txtAjusteObs" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <br />
                                                            <div class="modal-footer">
                                                                <asp:Button runat="server" Text="Gravar" ID="btnAlteraComisaao" CssClass="btn btn-success" />

                                                                <asp:Button runat="server" Text="Excluir" ID="btnExcluirAlteraComisaao" CssClass="btn btn-danger" OnClientClick="javascript:return confirm('Deseja realmente excluir esta comissão?');" />
                                                                <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharAlteraComisaao" Text="Close" />


                                                            </div>

                                                        </div>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtLiquidacaoInicial" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <asp:TextBox ID="TextBox2" runat="server" Style="display: none;"></asp:TextBox>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="pnlCCProcesso" TargetControlID="lkCCProcesso" CancelControlID="TextBox2"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlCCProcesso" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>
                                        <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">CONTA CORRENTE DO PROCESSO</h5>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="alert alert-warning" id="divInfoCCProcesso" runat="server" visible="false">
                                                        <asp:Label ID="lblInfoCCProcesso" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="alert alert-danger" id="divErroCCProcesso" runat="server" visible="false">
                                                        <asp:Label ID="lblErroCCProcesso" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-6">
                                                            <div class="form-group">

                                                                <asp:Label ID="Label28" runat="server">Competência</asp:Label><br />

                                                                <asp:Label ID="lblCompetenciaCCProcesso" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label34" runat="server">Data Liquidação</asp:Label><label runat="server" style="color: red">*</label><br />

                                                                <asp:TextBox ID="txtLiquidacaoCCProcesso" runat="server" CssClass="form-control data" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="modal-footer">
                                                    <asp:LinkButton runat="server" CssClass="btn btn-success" ID="lkGravarCCProcesso" Text="Gravar" />
                                                    <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCCProcesso" Text="Close" />

                                                </div>
                                            </div>
                                        </div>
                                    </center>
                                </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger ControlID="btnFecharAlteraComisaao" />
                                <asp:AsyncPostBackTrigger ControlID="txtLiquidacaoCCProcesso" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFiltro" />
                                <asp:PostBackTrigger ControlID="lkCSV" />
                                <asp:PostBackTrigger ControlID="lkRelEquipe" />
                            </Triggers>
                        </asp:UpdatePanel>


                    </div>

                </div>


            </div>
        </div>
    </div>

    <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
    <asp:SqlDataSource ID="dsTabelaComissaoVendedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_VENDEDOR_TAXA_COMISSAO, DT_VALIDADE_INICIAL DT_VALIDADE_INICIAL,VL_TAXA,VL_PROFIT_INICIO,VL_PROFIT_FIM,VL_COMISSAO ,B.NM_BASE_CALCULO_TAXA,C.NM_TIPO_ESTUFAGEM,D.NM_VIATRANSPORTE, F.NM_TIPO_CALCULO FROM 
TB_VENDEDOR_TAXA_COMISSAO  A INNER JOIN TB_BASE_CALCULO_TAXA B ON B.ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA INNER JOIN TB_TIPO_ESTUFAGEM C ON C.ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM INNER JOIN TB_VIATRANSPORTE D ON D.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE INNER JOIN [DBO].[TB_TIPO_CALCULO] F ON F.ID_TIPO_CALCULO = A.ID_TIPO_CALCULO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTabelaComissaoProspeccao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT  A.ID_VENDEDOR_PROSPECCAO,  A.DT_VALIDADE_INICIAL, A.VL_TAXA,E.NM_EQUIPE, A.FL_PAGAMENTO_RECORRENTE ,B.NM_BASE_CALCULO_TAXA,C.NM_TIPO_ESTUFAGEM,D.NM_VIATRANSPORTE, F.NM_TIPO_CALCULO FROM 
TB_VENDEDOR_PROSPECCAO  A INNER JOIN TB_BASE_CALCULO_TAXA B ON B.ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA INNER JOIN TB_TIPO_ESTUFAGEM C ON C.ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM INNER JOIN TB_VIATRANSPORTE D ON D.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE INNER JOIN TB_VENDEDOR_EQUIPE E ON E.ID_EQUIPE = A.ID_EQUIPE  INNER JOIN [DBO].[TB_TIPO_CALCULO] F ON F.ID_TIPO_CALCULO = A.ID_TIPO_CALCULO"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsCadastroMetas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_VENDEDOR_METAS,A.DT_VALIDADE_INICIAL,C.NM_TIPO_ESTUFAGEM,D.NM_VIATRANSPORTE,A.VL_META,A.VL_META_MIN,A.VL_META_MAX
  FROM  TB_VENDEDOR_METAS A
 INNER JOIN TB_TIPO_ESTUFAGEM C ON C.ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM 
 INNER JOIN TB_VIATRANSPORTE D ON D.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE"></asp:SqlDataSource>
    

    <asp:SqlDataSource ID="dsVendedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO = 1 AND FL_ATIVO = 1 ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEquipe" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE FL_EQUIPE_INSIDE_SALES = 1 AND FL_ATIVO = 1 ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA = '@COMPETENCIA' ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO">
        <SelectParameters>
            <asp:ControlParameter Name="COMPETENCIA" Type="string" ControlID="txtCompetencia" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM]
union SELECT 0, 'Selecione' FROM [dbo].[TB_TIPO_ESTUFAGEM] ORDER BY ID_TIPO_ESTUFAGEM"></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsServico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO 
union SELECT  0, 'Selecione' ORDER BY ID_SERVICO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCliente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO IN (SELECT DISTINCT ID_PARCEIRO_CLIENTE FROM TB_BL)
union SELECT 0, 'Selecione' ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsVendedores" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE (FL_VENDEDOR_DIRETO = 1 OR FL_VENDEDOR = 1 OR ID_PARCEIRO = (SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)) AND FL_ATIVO = 1 
union SELECT 0, ' Selecione' ORDER BY NM_RAZAO"></asp:SqlDataSource>


    <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA] WHERE ID_BASE_CALCULO_TAXA IN (31,32,34)
union SELECT 0, '      Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY NM_BASE_CALCULO_TAXA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CALCULO ,NM_TIPO_CALCULO FROM [dbo].[TB_TIPO_CALCULO]
union SELECT 0, '      Selecione' FROM [dbo].[TB_TIPO_CALCULO] ORDER BY NM_TIPO_CALCULO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE] WHERE ID_VIATRANSPORTE IN (1,4)
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEquipes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_EQUIPE, NM_EQUIPE FROM [dbo].[TB_VENDEDOR_EQUIPE]
UNION SELECT  0, '    Selecione' ORDER BY NM_EQUIPE"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsIndicadorInterno" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_VENDEDOR_INDICADOR_INTERNO,DT_VALIDADE_INICIAL,VL_TAXA FROM TB_VENDEDOR_INDICADOR_INTERNO  ORDER BY ID_VENDEDOR_INDICADOR_INTERNO"></asp:SqlDataSource>


    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function RelPorVendedor() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            var COMPETENCIA = document.getElementById('<%= txtCompetencia.ClientID %>').value;


            window.open('RelatorioVendedor.aspx?tipo=1&id=' + ID + '&c=' + COMPETENCIA, '_blank');
        }

        function RelTodosVendedores() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            var COMPETENCIA = document.getElementById('<%= txtCompetencia.ClientID %>').value;

            window.open('RelatorioVendedor.aspx?tipo=2&c=' + COMPETENCIA, '_blank');
        }

        function RelPorEmail() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            var COMPETENCIA = document.getElementById('<%= txtCompetencia.ClientID %>').value;

            window.open('RelatorioVendedor.aspx?tipo=3&c=' + COMPETENCIA, '_blank');
        }

        function MouseWait() {
            console.log("wait");
            document.body.style.cursor = "wait";
        };
        function MouseDefault() {
            console.log("default");
            document.body.style.cursor = "default";
        };
        $(document).ready(function () {

            $('#hdComissao').click(function () {
                $('#Comissao').show();
                $('#IndicacaoInt').hide();
                $('#Prospeccao').hide();
            });

            $('#hdIndicacaoInt').click(function () {
                $('#Comissao').hide();
                $('#IndicacaoInt').show();
                $('#Prospeccao').hide();
            });

            $('#hdProspeccao').click(function () {
                $('#Comissao').hide();
                $('#IndicacaoInt').hide();
                $('#Prospeccao').show();
            });

            $('#MainContent_txtDataInicioMetas').mask('99/99/9999');
            $('#MainContent_txtDataFimMetas').mask('99/99/9999');
            $('#MainContent_txtDtInicioComissaoProspecao').mask('99/99/9999');
            $('#MainContent_txtDtTerminoComissaoProspecao').mask('99/99/9999');
            $('#MainContent_txtDtInicioComissaoProspecao').attr('placeholder', '__/__/____');
            $('#MainContent_txtDtTerminoComissaoProspecao').attr('placeholder', '__/__/____');

        });



        </script>
</asp:Content>
