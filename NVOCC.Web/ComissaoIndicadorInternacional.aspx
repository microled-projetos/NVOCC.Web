<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ComissaoIndicadorInternacional.aspx.vb" Inherits="NVOCC.Web.ComissaoIndicadorInternacional" %>

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
                    <h3 class="panel-title">COMISSÃO DE INDICADOR INTERNACIONAL
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
                                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                <asp:ListItem Value="1">Indicador</asp:ListItem>
                                                <asp:ListItem Value="2">Número do processo</asp:ListItem>
                                                <asp:ListItem Value="3">MBL</asp:ListItem>
                                                <asp:ListItem Value="4">HBL</asp:ListItem>
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
                                            <asp:Label ID="Label1" runat="server">Competencia</asp:Label><br />

                                            <asp:TextBox ID="txtCompetencia" placeholder="MM/AAAA" AutoPostBack="true" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label35" runat="server">Quinzena</asp:Label><br />
                                            <asp:TextBox ID="txtQuinzena" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server"></asp:Label><br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                                        </div>
                                    </div>
                                    <div class="col-sm-offset-1 col-sm-4">
                                        <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />
                                        <asp:LinkButton ID="lkComissoes" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Comissões</asp:LinkButton>
                                        <asp:LinkButton ID="lkCSV" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Exportar CSV</asp:LinkButton>
                                        <asp:LinkButton ID="lkBaixarPagamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Gravar no CC do Processo</asp:LinkButton>

                                    </div>
                                </div>


                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblCompetenciaSobrepor" runat="server"></asp:Label>
                                    <asp:Label ID="lblContasReceber" runat="server"></asp:Label>
                                    <asp:Label ID="lblContador" Style="display:none" runat="server"></asp:Label>

                                </div>
                               <div runat="server" visible="false" id="DivGrid2">

                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                    <asp:GridView ID="dgvComissoes" DataKeyNames="ID_CABECALHO_COMISSAO_INTERNACIONAL" DataSourceID="dsComissao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_DETALHE_COMISSAO_INTERNACIONAL") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="COMPETENCIA_QUINZENA" HeaderText="COMPETENCIA" SortExpression="COMPETENCIA_QUINZENA" />
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="PARCEIRO_VENDEDOR" HeaderText="INDICADOR" SortExpression="PARCEIRO_VENDEDOR" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="CNEE" SortExpression="PARCEIRO_CLIENTE" />
                                                <asp:BoundField DataField="MBL" HeaderText="MBL" SortExpression="MBL" />
                                                <asp:BoundField DataField="HBL" HeaderText="HBL" SortExpression="HBL" />                                                <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="ESTUFAGEM" SortExpression="TIPO_ESTUFAGEM" />
                                                <asp:BoundField DataField="QT_CNTR" HeaderText="QTD. CNTR" SortExpression="QT_CNTR" />
                                                <asp:BoundField DataField="VL_COMISSAO" HeaderText="PREMIAÇÃO" SortExpression="VL_COMISSAO" />
                                                <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                                <asp:BoundField DataField="PARCEIRO_AGENTE_INTERNACIONAL" HeaderText="AGENTE INTERNACIONAL" SortExpression="PARCEIRO_AGENTE_INTERNACIONAL" />
                                                <asp:BoundField DataField="DT_EMBARQUE(ETD)" HeaderText="ETD" SortExpression="DT_EMBARQUE(ETD)" />
                                                <asp:BoundField DataField="DT_CHEGADA(ETA)" HeaderText="ETA" SortExpression="DT_CHEGADA(ETA)" />
                                                <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="DATA LIQUIDAÇÃO" SortExpression="DT_LIQUIDACAO" />
                                                <asp:BoundField DataField="DT_EXPORTACAO" HeaderText="DATA EXPORTAÇÃO" SortExpression="DT_EXPORTACAO" />

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_DETALHE_COMISSAO_INTERNACIONAL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicao()"></asp:LinkButton>
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
<asp:LinkButton ID="lkTabelaComissoes" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" >Tabela de Comissões</asp:LinkButton>
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


                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlTabela" TargetControlID="lkTabelaComissoes" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlTabela" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">TABELA DE COMISSÕES</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                    
                                 <div class="row">
                                   <div class="col-sm-2">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label27" runat="server">ID</asp:Label><br />

                               <asp:TextBox ID="txtIDTabelaTaxa" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-10">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label9" runat="server">INDICADOR</asp:Label><br />

         <asp:DropDownList ID="ddlVendedorTabela" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_RAZAO" DataSourceID="dsVendedores" DataValueField="ID_PARCEIRO"/>                                        </div>
                                         </div>
                                     </div>
                                       <div class="row">
                                     <div class="col-sm-2">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label5" runat="server">Validade Inicial</asp:Label><br />

                               <asp:TextBox ID="txtValidadeTabela" placeholder="___/___/____" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-4">
                                    <div class="form-group">                                          
                               
                                               <asp:Label ID="Label7" runat="server">Moeda</asp:Label><br />
      <asp:DropDownList ID="ddlMoedaTabela" runat="server"  CssClass="form-control"  DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>                                        </div>
                                         </div>

                                     <div class="col-sm-4">
                                    <div class="form-group">                                          
 <asp:Label ID="Label8" runat="server">Taxa</asp:Label><br />

                               <asp:TextBox ID="txtTaxaTabela"  runat="server" CssClass="form-control"></asp:TextBox>
                                    
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
                                                                <asp:GridView ID="dgvTabelaComissao" DataKeyNames="ID_TAXA_COMISSAO_INDICADOR" DataSourceID="dsTabelaComissao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_TAXA_COMISSAO_INDICADOR") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="PARCEIRO_VENDEDOR" HeaderText="INDICADOR" SortExpression="PARCEIRO_VENDEDOR" />
                                            <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="VALIDADE INICIAL" SortExpression="DT_VALIDADE_INICIAL" />
                                            <asp:BoundField DataField="VL_TAXA" HeaderText="TAXA" SortExpression="VL_TAXA" />
                                            <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" SortExpression="MOEDA" />
                                     <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:linkButton CommandName="Editar"  CommandArgument='<%# Eval("ID_TAXA_COMISSAO_INDICADOR") %>' runat="server"  CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText=""  >
                                            <ItemTemplate>
                                                <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');"  CommandArgument='<%# Eval("ID_TAXA_COMISSAO_INDICADOR") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:linkButton>
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
                                     <div class="col-sm-2">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label11" runat="server">Competência</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:TextBox ID="txtNovaCompetencia" AUTOPOSTBACK="true" placeholder="MM/AAAA" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-2">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label36" runat="server">Quinzena</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:TextBox ID="txtNovaQuinzena" runat="server" AutoPostBack="true" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-4">
                                    <div class="form-group">                                          
                               
                                               <asp:Label ID="Label12" runat="server">Data Liquidação(Inicial)</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:TextBox ID="txtLiquidacaoInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                         </div>

                                     <div class="col-sm-4">
                                    <div class="form-group">                                             
                                <asp:Label ID="Label14" runat="server">Data Liquidação(Final)</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:TextBox ID="txtLiquidacaoFinal" runat="server" AutoPostBack="true" CssClass="form-control data"></asp:TextBox>
                                         
                                   </div>          
                                </div>  
      </div>
                                                            <div class="row">
                                     <div class="col-sm-12">
                                    <div class="form-group">                                          
                                               <asp:Label ID="Label18" runat="server">Observação</asp:Label><br />
                               <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"  MaxLength="100"></asp:TextBox>
                                        </div>
                                         </div>
      </div>
                                <br />
                               <div class="modal-footer"> 
                                   <asp:Button runat="server" Text="Gerar" ID="btnGerarComissao" CssClass="btn btn-success" /> 
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharGerarComissao" text="Close" />
                                                                 

                                                        </div>                                                    
                                            
                                       </div>     </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtLiquidacaoFinal" />
                                        <asp:AsyncPostBackTrigger ControlID="txtLiquidacaoInicial" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGerarComissao" />
                                        <asp:AsyncPostBackTrigger ControlID="txtNovaQuinzena" />
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
                                                               
                                     <div class="col-sm-3">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label13" runat="server">Processo</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:TextBox ID="txtAjusteProcesso" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         </div>
                                     
 
                                                                       
     
                                     <div class="col-sm-7">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label17" runat="server">Indicador:</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:DropDownList ID="ddlAjusteVendedor" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="NM_RAZAO" DataSourceID="dsVendedores" DataValueField="ID_PARCEIRO"/>
                                        </div>
                                         </div>
                                                                 </div>
                                                            

                              <div class="row">
                                  <div class="col-sm-3">
                                    <div class="form-group">                                             
                                <asp:Label ID="Label32" runat="server">Data Liquidação</asp:Label><label runat="server" style="color: red">*</label><br />
                               <asp:TextBox ID="txtAjusteLiquidacao" placeholder="___/___/____" runat="server" CssClass="form-control data"></asp:TextBox>
                                   </div>          
                                </div>  
                                     <div class="col-sm-3">
                                    <div class="form-group">                                          
                                               <asp:Label ID="Label23" runat="server">Taxa Base</asp:Label><label runat="server" style="color: red">*</label><br />
                               <asp:TextBox ID="txtAjusteBase" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         </div>

                                     <div class="col-sm-3">
                                    <div class="form-group">                                             
                                <asp:Label ID="Label25" runat="server">Qtd. CNTR</asp:Label><label runat="server" style="color: red">*</label><br />
                               <asp:TextBox ID="txtAjusteQtdCNTR" runat="server" CssClass="form-control inteiro"></asp:TextBox>                                      
                                   </div>          
                                </div>  
                                                                        
                                     <div class="col-sm-3">
                                    <div class="form-group">                                          
                               
                                               <asp:Label ID="Label31" runat="server">Moeda</asp:Label><label runat="server" style="color: red">*</label><br />

                               <asp:DropDownList ID="ddlMoeda" runat="server"  CssClass="form-control"  DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
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

                          <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"  style="display: none;"></asp:TextBox>
                  
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlBaixa" TargetControlID="lkBaixarPagamento" CancelControlID="TextBox2"></ajaxToolkit:ModalPopupExtender>
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                <asp:Panel ID="pnlBaixa" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CONTA CORRENTE DO PROCESSO</h5>
                                                        </div>
                                                        <div class="modal-body">  
                                                            
                                                            <div class="alert alert-success" id="divSuccessBaixa" runat="server" visible="false">
                                    <asp:Label ID="lblSuccessBaixa" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErroBaixa" runat="server" visible="false">
                                    <asp:Label ID="lblErroBaixa" runat="server"></asp:Label>
                                </div>
                                 <div class="alert alert-warning" id="divInfoBaixa" runat="server" visible="false">
                                    <asp:Label ID="lblInfoBaixa" runat="server"></asp:Label>
                                </div>
                                            <h5>
                                                Competência: <asp:label runat="server" ID="lblCompetencia"  /><br />
                                                Quinzena: <asp:label runat="server" ID="lblQuinzena"  /></h5>                                        
                                      
                                                        <div class="row">
                                                            <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left">ID</label>
                                            <asp:TextBox ID="txtIDBaixa" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                             <asp:TextBox ID="txtIDCC"  style="display:none" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
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
                               <asp:DropDownList ID="ddlContaBancaria" runat="server" CssClass="form-control" DataTextField="NM_CONTA_BANCARIA" AutoPostBack="true" DataSourceID="dsContaBancaria" DataValueField="ID_CONTA_BANCARIA"/>
                                        </div>
                                    </div>
                                             </div>
                                                        <div class="row">    
                                                            <div class="col-sm-12">
                                                                <asp:GridView ID="dgvMoedas" DataKeyNames="ID_MOEDA" DataSourceID="dsMoedaGrid" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado com data de câmbio atual.">
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
                                
                                <asp:AsyncPostBackTrigger ControlID="ddlContaBancaria" />
                                
                            </Triggers>
                        </asp:UpdatePanel>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFiltro" />
                                                                <asp:PostBackTrigger ControlID="lkCSV" />
                               
                                
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
    <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
     <asp:SqlDataSource ID="dsComissao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Comissao_Internacional] ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTabelaComissao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="
SELECT ID_TAXA_COMISSAO_INDICADOR,CONVERT(VARCHAR,DT_VALIDADE_INICIAL,103)DT_VALIDADE_INICIAL,ID_PARCEIRO_VENDEDOR,(SELECT  NM_RAZAO
                               FROM            dbo.TB_PARCEIRO
                               WHERE        (ID_PARCEIRO = B.ID_PARCEIRO_VENDEDOR)) AS PARCEIRO_VENDEDOR,VL_TAXA,ID_MOEDA,  (SELECT  NM_MOEDA
                               FROM            dbo.TB_MOEDA
                               WHERE        ID_MOEDA =B.ID_MOEDA)MOEDA FROM TB_TAXA_COMISSAO_INDICADOR B"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT  0, 'Selecione'  ORDER BY ID_MOEDA">
</asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoedaGrid" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA,NM_MOEDA FROM TB_MOEDA ">
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsContaBancaria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_CONTA_BANCARIA,NM_CONTA_BANCARIA FROM TB_CONTA_BANCARIA WHERE FL_ATIVO = 1
union SELECT 0, 'Selecione'  ORDER BY ID_CONTA_BANCARIA"></asp:SqlDataSource>

            <asp:SqlDataSource ID="dsVendedores" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1 AND FL_ATIVO = 1
union SELECT 0, 'Selecione' ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
     
    </script>
</asp:Content>
