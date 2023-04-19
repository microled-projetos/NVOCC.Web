<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ComissaoInsideSales.aspx.vb" Inherits="NVOCC.Web.ComissaoInsideSales" %>

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
    </style>

    
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">COMISSÃO DE INSIDE SALES
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

                                <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server">Filtro</asp:Label><br />

                                            <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                <asp:ListItem Value="0" Selected="True">Todos os registros</asp:ListItem>
                                                <asp:ListItem Value="1">Equipe</asp:ListItem>
                                                <asp:ListItem Value="2">Nome</asp:ListItem>
                                                <asp:ListItem Value="3">Número do processo</asp:ListItem>
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

                                            <asp:TextBox ID="txtCompetencia" placeholder="MM/AAAA"  runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
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
                                                <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="DATA NOTA" SortExpression="DT_NOTA_FISCAL" DataFormatString="{0:dd/MM/yyyy}"/>
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
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">COMISSÕES</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                 <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
<asp:LinkButton ID="lkTabelaComissoes" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" >Gestão</asp:LinkButton>
                                    </div>
                                        </div>
                                         </div>
                                <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
<asp:LinkButton ID="lkEquipe" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" href="CadastrarEquipe.aspx" target="_blank" >Equipes</asp:LinkButton>
                                    </div>
                                        </div>
                                         </div>
                                <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          
<asp:LinkButton ID="lkGerarComissao" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" >Gerar Comissões</asp:LinkButton>
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
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharComissoes" text="Close" />
                                                        </div>                                                    
                                                </div>
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlRelatorios" TargetControlID="lkRelatorios" CancelControlID="btnFecharRelatorios"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlRelatorios" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">RELATORIOS</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                          
                                    
                                    
                                 <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                        <asp:LinkButton ID="lkRelEquipe" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" >Equipes por Competência</asp:LinkButton>
                                        </div>
                                         </div>
                                   </div>    
                                 
                             </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharRelatorios" text="Close" />
                                                        </div>                                                   
                                                </div>      
                                       </div>     </center>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="mpeGestao" runat="server" PopupControlID="pnlTabela" TargetControlID="lkTabelaComissoes" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlTabela" runat="server" CssClass="modalPopup" Style="display: none">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-xl" role="document" id="myModal">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">TABELA DE COMISSÕES - GESTÃO</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                    
                                 <div class="row">
                                  <div class="col-sm-3">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label27" runat="server">ID</asp:Label><br />

                                <asp:TextBox ID="txtIDTabelaTaxa" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-3">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label5" runat="server">Validade Inicial</asp:Label><br />

                               <asp:TextBox ID="txtValidade" placeholder="___/___/____" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-3">
                                    <div class="form-group">                                          
                               
                                               <asp:Label ID="Label7" runat="server">Taxa LCL</asp:Label><br />

                               <asp:TextBox ID="txtLCL"  runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         </div>

                                     <div class="col-sm-3">
                                    <div class="form-group">                                          
 <asp:Label ID="Label8" runat="server">Taxa FCL</asp:Label><br />

                               <asp:TextBox ID="txtFCL"  runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                        </div>
                                          </div>  </div>
                                     <div class="row">
                                       <div class="col-sm-4">
                                    <div class="form-group">                                          
 <asp:Label ID="Label9" runat="server">Gestor</asp:Label><br />

 <asp:DropDownList ID="ddlGestor" runat="server" CssClass="combos form-control" DataTextField="NOME" DataSourceID="dsGestor" DataValueField="ID_USUARIO" style="font-size:15px" AutoPostBack="true" ></asp:DropDownList>                                    
                                        </div>
                                         </div>
                                       <div class="col-sm-4">
                                    <div class="form-group">                                          
 <asp:Label ID="Label20" runat="server">Equipes</asp:Label><br />

 <asp:DropDownList ID="ddlEquipes" runat="server" CssClass="combosMultiplos form-control" DataTextField="NM_EQUIPE" DataSourceID="dsEquipes" DataValueField="ID_EQUIPE" style="font-size:15px" ></asp:DropDownList>                                    
                                        </div>
                                         </div>

                                  
                                                                          <div class="col-sm-2">
                                    <div class="form-group">                                             
                                <div class="form-group"><asp:Label ID="Label10" runat="server"></asp:Label><br />
                               <asp:Button runat="server" Text="Gravar" ID="btnGravaTaxaTabela" CssClass="btn btn-success" /> 
                                                                  <asp:Button runat="server" Text="Limpar" ID="btnLimpaTaxaTabela" CssClass="btn btn-warning" /> 


                                    
                                   </div>          
                                </div>  

                             </div>    </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                    <div class="form-group">                                          
                                             <div id="DivExcluir" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblErroExcluir" Text="" runat="server" />
                                   </div>
                                   <div id="divInfo" runat="server" visible="false" class="alert alert-success">
                                        <asp:label ID="lblInfo" Text="" runat="server" />
                                   </div>  
                                                                <asp:GridView ID="dgvTabelaComissao" DataKeyNames="ID_TAXA_COMISSAO_VENDEDORES" DataSourceID="dsTabelaComissao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_TAXA_COMISSAO_VENDEDORES") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="VALIDADE INICIAL" SortExpression="DT_VALIDADE_INICIAL" />
                                            <asp:BoundField DataField="VL_TAXA_LCL" HeaderText="TAXA LCL" SortExpression="VL_TAXA_LCL" />
                                            <asp:BoundField DataField="VL_TAXA_FCL" HeaderText="TAXA FCL" SortExpression="VL_TAXA_FCL" />      
                                             <asp:BoundField DataField="NOME_GESTOR" HeaderText="GESTOR" SortExpression="NOME_GESTOR" />   
                                             <asp:BoundField DataField="NM_EQUIPE" HeaderText="EQUIPE" SortExpression="NM_EQUIPE" />   
                                     <asp:TemplateField HeaderText="">
                                            <ItemTemplate> 
                                                <asp:linkButton CommandName="Editar"  CommandArgument='<%# Eval("ID_TAXA_COMISSAO_VENDEDORES") %>' runat="server"  CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText=""  >
                                            <ItemTemplate>
                                                <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');"  CommandArgument='<%# Eval("ID_TAXA_COMISSAO_VENDEDORES") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                        </div>
                                                                    </div>
                                                                </div>
                               <div class="modal-footer">
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharTabela" text="Close" />
                                                        </div>                                                    
                                            
                                       </div>     </center>
                                        </asp:Panel>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlGestor" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGravaTaxaTabela" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimpaTaxaTabela" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTabelaComissao" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlGerarComissao" TargetControlID="lkGerarComissao" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlGerarComissao" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
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

                               <asp:TextBox ID="txtNovaCompetencia" AUTOPOSTBACK="true" placeholder="MM/AAAA" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
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

<asp:Label ID="lblLCL" runat="server"/>                                         
                                        </div>
                                         </div>
                                     <div class="col-sm-6" style="border: ridge 1px;">
                                    <div class="form-group">                                          
 <asp:Label ID="Label19" runat="server"><strong>Taxa FCL</strong></asp:Label><br />

<asp:Label ID="lblFCL" runat="server"/>                                         
                                    
                                        </div>
                                         </div>
                              
                                </div>
                                <br />
                                 <div class="row">
                                                                <div class="col-sm-12">
                                    <div class="form-group">VENDEDORES DIRETOS:                                          
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
                                      <div class="col-sm-6" Style="display:none">
                                    <div class="form-group">EQUIPE INSIDE SALES                                         
                                          
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
                                   <asp:Button runat="server" Text="Gerar" ID="btnGerarComissao" CssClass="btn btn-success" OnClientClick="MouseWait(); return true;"/> 
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharGerarComissao" text="Close" />
                                                                 

                                                        </div>                                                    
                                            
                                       </div>     </center>
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
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
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
                               <asp:DropDownList ID="ddlAjusteServico" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_SERVICO" DataSourceID="dsServico" DataValueField="ID_SERVICO"/>
                                   </div>          
                                </div>  
                                                                </div><div class="row">
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
                                </div>  <div class="col-sm-3">
                                    <div class="form-group">                                             
                                <asp:Label ID="Label22" runat="server">Estufagem:</asp:Label><label runat="server" style="color: red">*</label><br />
                               <asp:DropDownList ID="ddlAjusteEstufagem" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem" DataValueField="ID_TIPO_ESTUFAGEM"/>                              
                                   </div>          
                                </div>  
                                                                       
      </div>
                                                            <div class="row">
                                     <div class="col-sm-6">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label17" runat="server">Vendedor:</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:DropDownList ID="ddlAjusteVendedor" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_RAZAO" DataSourceID="dsVendedores" DataValueField="ID_PARCEIRO"/>
                                        </div>
                                         </div>
                                     <div class="col-sm-6">
                                    <div class="form-group">                                          
                                             <asp:Label ID="Label21" runat="server">Cliente:</asp:Label><label runat="server" style="color: red">*</label><br />
                               <asp:DropDownList ID="ddlAjusteCliente" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_RAZAO" DataSourceID="dsCliente" DataValueField="ID_PARCEIRO"/>
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
                               <asp:TextBox ID="txtAjusteObs" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"  MaxLength="100"></asp:TextBox>
                                        </div>
                                         </div>
      </div>

                                <br />
                               <div class="modal-footer"> 
                                                                      <asp:Button runat="server" Text="Gravar" ID="btnAlteraComisaao" CssClass="btn btn-success" /> 

                                   <asp:Button runat="server" Text="Excluir" ID="btnExcluirAlteraComisaao" CssClass="btn btn-danger" OnClientClick="javascript:return confirm('Deseja realmente excluir esta comissão?');"/> 
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharAlteraComisaao" text="Close" />
                                                                 

                                                        </div>                                                    
                                            
                                       </div>     </center>
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
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CONTA CORRENTE DO PROCESSO</h5>
                                                        </div>
                                                        <div class="modal-body" > 
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

 <asp:Label ID="lblCompetenciaCCProcesso" runat="server"/>                                        </div>
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
                                         <asp:LinkButton runat="server" CssClass="btn btn-success" ID="lkGravarCCProcesso" text="Gravar" />
                                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCCProcesso" text="Close" />

                                                        </div>                                                    
                                                </div>
                                       </div>     </center>
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
    <asp:SqlDataSource ID="dsTabelaComissao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INSIDE_TAXA,CONVERT(VARCHAR,DT_VALIDADE_INICIAL,103)DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL, B.NOME AS NOME_GESTOR, C.NM_EQUIPE FROM TB_INSIDE_TAXA_COMISSAO A INNER JOIN TB_USUARIO B ON A.ID_USUARIO_GESTOR = B.ID_USUARIO INNER JOIN TB_INSIDE_EQUIPE C ON A.ID_EQUIPE = C.ID_EQUIPE ORDER BY DT_VALIDADE_INICIAL"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsVendedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO = 1 AND FL_ATIVO = 1 ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEquipe" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE FL_EQUIPE_INSIDE_SALES = 1 AND FL_ATIVO = 1 ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Comissao_Inside] WHERE COMPETENCIA = '@COMPETENCIA' ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO">
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

     <asp:SqlDataSource ID="dsEquipes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_EQUIPE, NM_EQUIPE FROM TB_INSIDE_EQUIPE WHERE ID_USUARIO_LIDER = @ID_USUARIO_LIDER ORDER BY NM_EQUIPE">
                <SelectParameters>
            <asp:ControlParameter Name="ID_USUARIO_LIDER" Type="Int32" ControlID="ddlGestor" DefaultValue="0" />
        </SelectParameters>
            </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsGestor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_USUARIO, NOME FROM TB_USUARIO WHERE FL_ATIVO = 1 
union SELECT 0, '   Selecione' ORDER BY NOME"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <script>

        $(document).ready(function () {
            Combos();
        });


        function MouseWait() {
            console.log("wait");
            document.body.style.cursor = "wait";
        };
        function MouseDefault() {
            console.log("default");
            document.body.style.cursor = "default";
        };

        function Combos() {
            $(".combos").select2({
                 dropdownParent: $('#myModal .modal-content')
            });

            $(".combosMultiplos").select2({
                multiple: true,
                dropdownParent: $('#myModal .modal-content')
            });

        };

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            Combos();   
        };

    </script>
</asp:Content>
