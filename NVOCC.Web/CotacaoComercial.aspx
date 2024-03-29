﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CotacaoComercial.aspx.vb" Inherits="NVOCC.Web.CotacaoComercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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

        .none {
            display: none
        }

        th {
            position: sticky !important;
            top: 0;
            background-color: #e6eefa;
            text-align: center;
        }
    </style>
    <div class="row principal">

        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">COTAÇÃO COMERCIAL
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

                                <div class="row" id="divCotacoesRepetidas">
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Data:</label><br />
                                              <asp:TextBox ID="txtData" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Serviço:</label></label><label runat="server" style="color: red">*</label>
                                            <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServico" DataValueField="ID_SERVICO">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Estufagem:</label><br />
                                            <asp:DropDownList ID="ddlEstufagem" runat="server" CssClass="form-control" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Incoterm:</label><br />
                                            <asp:DropDownList ID="ddlIncoterm" runat="server" CssClass="form-control" DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Origem:</label><br />
                                            <asp:DropDownList ID="ddlOrigem" runat="server" CssClass="form-control" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Destino:</label><br />
                                            <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Valor M3:</label>
                                            <asp:TextBox ID="txtCBM" runat="server" CssClass="form-control valores"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Peso:</label>
                                            <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control valores"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label"></label>
                                            <br />
                                            <asp:Button ID="btnPesquisaRepetidas" runat="server" CssClass="btn btn-block btn-primary" Text="Verificar"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <br />


                                <div class="row linhabotao text-center">
                                    <asp:LinkButton ID="lkInserir" runat="server" CssClass="btn  btnn btn-default btn-sm" Style="font-size: 15px"><i  class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
                                    <asp:LinkButton ID="lkAlterar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i  class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                    <asp:LinkButton ID="lkDuplicar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i  class="glyphicon glyphicon-duplicate"  ></i>&nbsp;Duplicar</asp:LinkButton>

                                    <asp:LinkButton ID="lkImprimir" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="fa fa-file-alt"></i>&nbsp;Imprimir/Enviar</asp:LinkButton>
                                    <asp:LinkButton ID="lkFiltrar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i  class="glyphicon glyphicon-search"></i>&nbsp;Filtrar</asp:LinkButton>
                                    <asp:LinkButton ID="lkCalcular" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i  class="fa fa-calculator"></i>&nbsp;Calcular</asp:LinkButton>
                                    <asp:LinkButton ID="lkAprovar" runat="server" UseSubmitBehavior="false" OnClientClick="desabilitaButtonOnClick()" CssClass="lkAprovar btn btnn btn-default btn-sm" Style="font-size: 15px"><i class="fa fa-check-circle"></i>&nbsp;Aprovar</asp:LinkButton>
                                    <asp:LinkButton ID="lkCancelar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente cancelar este registro?');"><i class="glyphicon glyphicon-ban-circle"></i>&nbsp;Cancelar</asp:LinkButton>
                                    <asp:LinkButton ID="lkRejeitar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" OnClientClick="javascript:return confirm('Deseja realmente rejeitar este registro?');"><i class="glyphicon glyphicon-remove-sign"></i>&nbsp;Rejeitar</asp:LinkButton>
                                    <asp:LinkButton ID="lkRemover" runat="server" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px"><i  class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>
                                    <asp:LinkButton ID="lkUpdate" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" Visible="false"><i class="glyphicon glyphicon-refresh"></i>&nbsp;Em Update</asp:LinkButton>
                                    <asp:LinkButton ID="lkFollowUp" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px" Visible="false" OnClientClick="javascript:return confirm('Deseja realmente realizar o Follow Up deste registro?');"><i class="glyphicon glyphicon-envelope"></i>&nbsp;Follow Up</asp:LinkButton>
                                </div>
                                <br />
                                <div class="row linhabotao" runat="server" id="divPesquisa" visible="false">
                                    <div class="col-sm-2" style="border: ridge 1px;margin-left: 10px">
                                                <div class="form-group">
                                                    <center><label class="control-label">Via Transporte:</label></center>
                                                    <asp:RadioButtonList ID="rdTRansporte" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Font-Size="12px">
                                                        <asp:ListItem Value="1">&nbsp;Marítimo</asp:ListItem>
                                                        <asp:ListItem Value="4">&nbsp;Aéreo</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">&nbsp;Ambos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>                                       
                                        <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <center><label class="control-label">Estufagem:</label></center>
                                                    <asp:RadioButtonList ID="rdEstufagem" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Font-Size="12px">
                                                        <asp:ListItem Value="1">&nbsp;FCL</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;LCL</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">&nbsp;Ambos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                     <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <center><label class="control-label">Serviço:</label></center>
                                                    <asp:RadioButtonList ID="rdServico" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Font-Size="12px">
                                                        <asp:ListItem Value="1">&nbsp;Importação</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Exportação</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">&nbsp;Ambos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        <div class="col-sm-2">                                    

                                            <div class="form-group"><asp:Label ID="Label1" Style="padding-left: 18px" runat="server">Filtro:</asp:Label>
                                                <asp:DropDownList ID="ddlConsultas" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                    <asp:ListItem Value="1">Número da cotação</asp:ListItem>
                                                    <asp:ListItem Value="2">Status da cotação</asp:ListItem>
                                                    <asp:ListItem Value="3">Cliente</asp:ListItem>
                                                    <asp:ListItem Value="4">Origem</asp:ListItem>
                                                    <asp:ListItem Value="5">Destino</asp:ListItem>
                                                    <asp:ListItem Value="6">Nome do Agente</asp:ListItem>
                                                    <asp:ListItem Value="18">Nome do Exportador</asp:ListItem>
                                                    <asp:ListItem Value="7">Nome do Vendedor</asp:ListItem>
                                                    <asp:ListItem Value="8">Número Processo</asp:ListItem>
                                                    <asp:ListItem Value="10">Cliente Final</asp:ListItem>
                                                    <asp:ListItem Value="11">Via Transporte</asp:ListItem>
                                                    <asp:ListItem Value="12">Estufagem</asp:ListItem>
                                                    <asp:ListItem Value="13">Incoterm</asp:ListItem>
                                                    <asp:ListItem Value="14">Armador</asp:ListItem>
                                                    <asp:ListItem Value="15">Analista Inside</asp:ListItem>
                                                    <asp:ListItem Value="16">Analista Pricing</asp:ListItem>
                                                    <asp:ListItem Value="17">Serviço</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group"><br />
                                                <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <br />
                                        <asp:Button runat="server" Text="Pesquisar" ID="bntPesquisar" CssClass="btn btn-success" OnClientClick="MouseWait(); return true;"/>
                                </div>



                                <ajaxToolkit:ModalPopupExtender ID="mpeImprimir" runat="server" PopupControlID="PanelImprimir" TargetControlID="lkImprimir" CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PanelImprimir" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalMercaoriaNova">Imprimir</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  
                            <div class="row"  style="padding-left:18px">
                               <asp:label ID="Label2" runat="server">Selecione o idioma:</asp:label> 
                                <div>
                                   <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                             <asp:DropDownList ID="ddlLinguagem" Width="230px" runat="server"  CssClass="form-control" Font-Size="15px" >
                                             <asp:ListItem Value="p" Selected="True">PORTUGUÊS</asp:ListItem>
                                            <asp:ListItem Value="i">INGLÊS</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                         </div>
                                   </div>      
                                </div>  
                             </div>
                                                                                  </div>                     
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Close" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnImprimir" text="Imprimir" />
                                                            <asp:Button runat="server" CssClass="btn btn-info" ID="btnEnviar" text="Enviar" />
                                                                                               
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <asp:Button runat="server" Style="display: none" ID="btnAuxiliarStatus" />
                                <ajaxToolkit:ModalPopupExtender ID="mpeStatus" runat="server" PopupControlID="PanelStatus" TargetControlID="btnAuxiliarStatus" CancelControlID="btnFecharStatus"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PanelStatus" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Historico de Status</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                                               <div class="row"> <div class="col-sm-12"> <div class="table-responsive tableFixHead" style="max-height: 200px; font-size:12px!important">

                             <asp:GridView ID="dgvHistoricoStatus" CssClass="table table-hover table-sm grdViewTable" DataKeyNames="ID_COTACAO_HIST_STATUS" DataSourceID="dsHistoricoStatus" runat="server" Style="max-height: 200px !important; overflow: scroll;" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado." >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_COTACAO_HIST_STATUS" HeaderText="#" SortExpression="Id" Visible="false" />
                                                                                 <asp:BoundField DataField="NR_COTACAO" HeaderText="Nº Cotação" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NM_STATUS_COTACAO" HeaderText="Status" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="DT_STATUS_COTACAO" HeaderText="Data" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="NOME" HeaderText="Alterado por" ItemStyle-HorizontalAlign="Center" />
                                                                            </Columns>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:GridView>
                             </div> </div>         </div>     </div>          
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharStatus" text="Close" />                                                                
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                                <asp:Button runat="server" Style="display: none" ID="btnAuxiliarRepetida" />
                                <ajaxToolkit:ModalPopupExtender ID="mpeRepetida" runat="server" PopupControlID="PanelRepetida" TargetControlID="btnAuxiliarRepetida" CancelControlID="btnFecharRepetida"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PanelRepetida" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" >COTAÇÃO REPETIDA</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  
                             <div class="row"><div class="col-sm-12">
                               <h5>Nenhuma cotação encontrada! </h5> 
                               <h5>Deseja inserir uma cotação com os parametros pesquisados?</h5> 
                             </div></div>
                                                                                  </div>                     
                               <div class="modal-footer">
                                                             <asp:Button runat="server" CssClass="btn btn-success" ID="btnInserirRepetida" text="Sim" />
                                                            <asp:Button runat="server" CssClass="btn btn-danger" ID="btnFecharRepetida" text="Não" />
                                                                                               
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>



                                <asp:Button runat="server" Style="display: none" ID="btnAuxiliarEnvioSI" />
                                <ajaxToolkit:ModalPopupExtender ID="mpeEnvioSI" runat="server" PopupControlID="pnlEnvioSI" TargetControlID="btnAuxiliarEnvioSI" CancelControlID="btnAuxiliarEnvioSI"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlEnvioSI" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" >ENVIAR S.I</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  <div class="row"><div class="col-sm-12">
                                                                                              <asp:Label ID="lblAgenteSI" runat="server" Text=""></asp:Label><br /><br />
                                                                <asp:Label ID="lblPortoOrigemSI" runat="server" Text=""></asp:Label>

                               
                             </div></div>
                             <div class="row"><div class="col-sm-12">
                              
                               <h5>Deseja enviar S.I?</h5> 
                             </div></div>
                                                                                  </div>                     
                               <div class="modal-footer">
                                                             <asp:Button runat="server" CssClass="btn btn-success" ID="btnConfirmaEnviarSI" text="Sim" OnClientClick="MouseWaitSI(); return true;"/>
                                                            <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCancelaEnvioSI" text="Não" OnClientClick="MouseWaitSI(); return true;"/>
                                                                                               
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkFiltrar" />
                                <asp:PostBackTrigger ControlID="lkImprimir" />
                                <asp:AsyncPostBackTrigger ControlID="btnImprimir" />
                                <asp:AsyncPostBackTrigger ControlID="btnEnviar" />
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
                                    <asp:GridView ID="dgvCotacao" DataKeyNames="ID_COTACAO" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsCotacao" AutoGenerateColumns="False" Style="max-height: 600px; overflow: auto;" AllowSorting="True" OnSorting="dgvCotacao_Sorting" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="100">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_COTACAO") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar" OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID_COTACAO" HeaderText="#" Visible="false" />
                                            <asp:BoundField DataField="DT_ABERTURA" HeaderText="Abertura" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_ABERTURA" />
                                            <asp:BoundField DataField="NR_COTACAO" HeaderText="Nº Cotação" SortExpression="NR_COTACAO" />
                                            <asp:BoundField DataField="REF_REPETIDAS" HeaderText="REF. REPETIDAS" SortExpression="REF_REPETIDAS" />
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
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_COTACAO") %>'
                                                        Text="Duplicar" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="lblAprovadas" runat="server"></asp:Label><br />
                                <asp:Label ID="lblRejeitadas" runat="server"></asp:Label>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bntPesquisar" />
                                <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCotacao" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCotacao" />
                                <asp:AsyncPostBackTrigger ControlID="lkCalcular" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>

    </div>


    <asp:SqlDataSource ID="dsCotacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT top 500 * FROM View_Cotacao WHERE ID_SERVICO IN(1,2) AND ID_TIPO_ESTUFAGEM IN(1,2) AND ID_VIATRANSPORTE IN(1,4) ORDER BY DT_ABERTURA DESC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
    <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>

    <asp:SqlDataSource ID="dsHistoricoStatus" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_COTACAO_HIST_STATUS,D.NR_COTACAO,B.NM_STATUS_COTACAO,A.DT_STATUS_COTACAO,C.NOME from TB_COTACAO_HIST_STATUS A
INNER JOIN TB_STATUS_COTACAO B ON A.ID_STATUS_COTACAO = B.ID_STATUS_COTACAO
INNER JOIN TB_USUARIO C ON A.ID_USUARIO_STATUS = C.ID_USUARIO
INNER JOIN TB_COTACAO D ON A.ID_COTACAO = D.ID_COTACAO
WHERE A.ID_COTACAO = @ID_COTACAO
ORDER BY D.DT_STATUS_COTACAO DESC">
        <SelectParameters>
            <asp:Parameter Name="ID_COTACAO" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INCOTERM, cast((CD_INCOTERM)as varchar)+ ' - '+ NM_INCOTERM as NM_INCOTERM FROM TB_INCOTERM 
union SELECT  0, 'Selecione' ORDER BY ID_INCOTERM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM] 
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_ESTUFAGEM"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO,  NM_PORTO + ' - ' +  CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ISNULL(ID_VIATRANSPORTE,0) <> 4 union SELECT  0, '      Selecione' ORDER BY NM_PORTO "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsServico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO WHERE ISNULL(TP_VIA,'') <> '' AND ID_SERVICO in (1,4) 
union SELECT  0, 'Selecione' ORDER BY ID_SERVICO"></asp:SqlDataSource>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>


    <script type="text/javascript">
        function SalvaPosicao() {
            var posicao = document.getElementById('DivGrid').scrollTop;
            if (posicao) {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
        };


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= TextBox1.ClientID %>').value;
            document.getElementById('DivGrid').scrollTop = valor;
            InIEvent();
        };


        function desabilitaButtonOnClick() {
            document.getElementById('<%= lkAprovar.ClientID %>').style.display = 'none';
        }

        function ImprimirCotacao() {

            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            var Linguagem = document.getElementById('<%= ddlLinguagem.ClientID %>').value;

            console.log(Linguagem);
            console.log(ID);
            window.open('GeraPDF.aspx?c=' + ID + '&l=' + Linguagem + '&f=i', '_blank');

        }

        function EnviarCotacao() {

            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            var Linguagem = document.getElementById('<%= ddlLinguagem.ClientID %>').value;

            console.log(Linguagem);
            console.log(ID);
            window.open('GeraPDF.aspx?c=' + ID + '&l=' + Linguagem + '&f=e', '_blank');

        }



        function InIEvent() {
            $(".valores").on("keypress keyup blur", function (e) {
                console.log("entrou")
                var chr = String.fromCharCode(e.which);
                if ("1234567890,".indexOf(chr) < 0)
                    return false;

            });
        }

        $(document).ready(InIEvent);

        function MouseWait() {
            console.log("wait");
            document.body.style.cursor = "wait";
        };
        function MouseDefault() {
            console.log("default");
            document.body.style.cursor = "default";
        };


        function MouseWaitSI() {
            document.getElementById('<%= btnConfirmaEnviarSI.ClientID %>').style.display = 'none';
            document.getElementById('<%= btnCancelaEnvioSI.ClientID %>').style.display = 'none';
            console.log("wait");
            document.body.style.cursor = "wait";
        };
        function MouseDefaultSI() {
            document.getElementById('<%= btnConfirmaEnviarSI.ClientID %>').style.display = 'block';
            document.getElementById('<%= btnCancelaEnvioSI.ClientID %>').style.display = 'block';
            console.log("default");
            document.body.style.cursor = "default";
        };
    </script>
</asp:Content>
