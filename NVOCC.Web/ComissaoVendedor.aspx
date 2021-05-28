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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>


                   <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                       <div class="col-sm-2" >
                           <div class="form-group">                           <asp:Label ID="Label2" runat="server">Filtro</asp:Label><br />

                               <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                   <asp:ListItem Value="1">Vendedor(a)</asp:ListItem>
                                   <asp:ListItem Value="2">Número do processo</asp:ListItem>
                               </asp:DropDownList>
                           </div>
                       </div>
                       <div class="col-sm-2" >
                           <div class="form-group"><asp:Label ID="Label4" Style="padding-left: 35px" runat="server"></asp:Label>
                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>
                      <div class="col-sm-1" >
                           <div class="form-group">                           <asp:Label ID="Label1" runat="server">Competencia</asp:Label><br />

                               <asp:TextBox ID="txtCompetencia" placeholder="__/__/____" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                       </div>
                       <div class="col-sm-1" >
                           <div class="form-group"><asp:Label ID="Label3" runat="server"></asp:Label><br />
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                           </div>
                       </div>

                       <div class="col-sm-offset-1 col-sm-5">
                           <asp:Label ID="Label6" Style="padding-left: 35px" runat="server">Ações</asp:Label><br />

                           <asp:LinkButton ID="lkComissoes" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Comissões</asp:LinkButton>
                           <asp:LinkButton ID="lkRelatorios" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Relatorios</asp:LinkButton>
                           <asp:LinkButton ID="lkCSV" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Exportar CSV</asp:LinkButton>
                           <asp:LinkButton ID="lkGravarCCProcesso" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Gravar no CC do Processo</asp:LinkButton>


                       </div>
                   </div>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblContador" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                    <asp:GridView ID="dgvComissoes" DataKeyNames="ID_CABECALHO_COMISSAO_VENDEDOR" DataSourceID="dsComissao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CABECALHO_COMISSAO_VENDEDOR") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                            <asp:BoundField DataField="ID_PARCEIRO_CLIENTE" HeaderText="Ref. Cliente" SortExpression="ID_PARCEIRO_CLIENTE" />
                                            <asp:BoundField DataField="VL_COMISSAO_TOTAL" HeaderText="VL_COMISSAO_TOTAL" SortExpression="VL_COMISSAO_TOTAL" />
                                            <asp:BoundField DataField="VL_COMISSAO_BASE" HeaderText="VL_COMISSAO_BASE" SortExpression="VL_COMISSAO_BASE" />
                                            <asp:BoundField DataField="DT_COMPETENCIA" HeaderText="DT_COMPETENCIA" SortExpression="DT_COMPETENCIA" />
                                            <asp:BoundField DataField="NR_NOTA_FISCAL" HeaderText="Nota Fiscal" SortExpression="NR_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="Data Nota Fiscal" SortExpression="DT_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="Data de Liquidação" SortExpression="DT_LIQUIDACAO" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_CABECALHO_COMISSAO_VENDEDOR") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>

                               

                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlComissoes" TargetControlID="lkComissoes" CancelControlID="btnFecharComissoes"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlComissoes" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
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
                                                                                <asp:LinkButton ID="lkAjustarComissao" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Ajustar Comissões</asp:LinkButton>
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
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">RELATORIOS</h5>
                                                        </div>
                                                        <div class="modal-body" style="padding-left: 50px;">                                       
                            <div class="row">
                                   <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">
                                           
<asp:LinkButton ID="lkRelPorVendedor" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px" >Por Vendedor</asp:LinkButton>
                                    </div>
                                        </div>
                                         </div>
                                    <div class="row">
                                     <div class="col-sm-10">
                                    <div class="form-group">                                             
                                        <asp:LinkButton ID="lkComDisparoEmail" runat="server" CssClass="btn btnn btn-default btn-sm btn-block" Style="font-size: 15px">Com Disparo de Email</asp:LinkButton>
                                        </div>
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

                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvComissoes" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFiltro" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
    <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>
    <asp:SqlDataSource ID="dsComissao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_CABECALHO_COMISSAO_VENDEDOR,DT_COMPETENCIA,NR_PROCESSO,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,DS_OBSERVACAO FROM TB_CABECALHO_COMISSAO_VENDEDOR A
LEFT JOIN TB_DETALHE_COMISSAO_VENDEDOR B ON B.ID_CABECALHO_COMISSAO_VENDEDOR = A.ID_CABECALHO_COMISSAO_VENDEDOR
"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
