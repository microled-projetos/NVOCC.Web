<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FreteTransportador_2.aspx.vb" Inherits="NVOCC.Web.FreteTransportador_2" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnGrid {
            color: black;
            margin: 5px;
        }

        th {
            z-index: 90;
        }

        .portos {
            z-index: 100;
        }
    </style>
    <div class="row principal">

        <div class="col-lg-12 table-responsive">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FRETE TRANSPORTADOR
                    </h3>
                </div>

                <div class="panel-body">
                    <br />

                    <div class="tab-pane fade active in" id="consulta">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtIDTafifario" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtOrigem" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>

                                <br />
                                <div class="row" runat="server">
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">ID:</label>
                                            <asp:TextBox ID="txtFiltroID" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Via Transporte:</label>
                                            <asp:TextBox ID="txtViaTransporte" runat="server" Style="display: none" CssClass="form-control" />
                                            <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" AutoPostBack="TRUE">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 portos" id="divOrigem" runat="server">
                                    </div>
                                    <div class="col-sm-1 portos" id="divDestino" runat="server">
                                    </div>


                                    <%-- <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Porto Origem:</label>
                                        <asp:DropDownList ID="ddlOrigem" runat="server" CssClass="combos2 form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                            <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Porto Destino:</label>
                                        <asp:DropDownList ID="ddlDestino" runat="server" CssClass="combos2 form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>--%>


                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Validade Inicial:</label>
                                            <asp:TextBox ID="txtValidadeInicial" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Validade Final:</label>
                                            <asp:TextBox ID="txtValidadeFinal" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Transportador:</label>
                                            <asp:DropDownList ID="ddlTransportador" runat="server" CssClass="combos form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Agente:</label>
                                            <asp:DropDownList ID="ddlAgente" runat="server" CssClass="combos form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                            <asp:CheckBox ID="ckInativo" runat="server" Text="&nbsp;&nbsp;Inativo" Font-Size="Medium" /><br />
                                            <asp:CheckBox ID="ckConsolidada" runat="server" Text="&nbsp;&nbsp;Consolidada" Font-Size="Medium" /><br />
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="color: white">x:</label><br />
                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnBusca" Text="Pesquisar" />
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="color: white">x:</label><br />
                                            <asp:LinkButton ID="lkExportar" runat="server" CssClass="btn btn-default" Style="font-size: 15px; background-color: lightgray"><i class="fa fa-file-excel"></i>&nbsp;Exportar</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>


                                <ajaxToolkit:ModalPopupExtender ID="mpeImprimir" runat="server" PopupControlID="Panel1" TargetControlID="lkExportar" CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Exportar</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  
                            <div class="row"  style="padding-left:18px">
                                <div>
                                   <div class="row">
                             
                                        <asp:LinkButton ID="lkExportaTarifario" runat="server" style="font-size:15px">Tarifario Importação</asp:LinkButton><br />
                                            <a href="TaxasLocaisImpo_PDF.aspx" target="_blank" >Taxas Locais FCL - Impo</a>  <br />                    
                                       <a href="TaxasLocaisExpo_PDF.aspx" target="_blank"  >Taxas Locais FCL - Expo</a> 
                                        <br />
                                       
                                   </div>      
                                </div>  
                             </div>
                           
                      
                                                       
                                                        </div>                     
                               <div class="modal-footer">
                                                            
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lkExportar" />
                                <asp:PostBackTrigger ControlID="ddlViaTransporte" />

                                <asp:PostBackTrigger ControlID="lkExportaTarifario" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />

                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBox4" Style="display: none" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox3" Style="display: none" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" Style="display: none" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>

                                <div id="DivGrid" class="table-responsive tableFixHead DivGrid">
                                    <asp:GridView ID="dgvFreteTranportador" DataKeyNames="ID_FRETE_TRANSPORTADOR" CssClass="table table-hover table-sm grdViewTable dgvFreteTranportador" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFreteTranportador" AutoGenerateColumns="false" Style="max-height: 600px; overflow: auto;" AllowSorting="true" OnSorting="dgvFreteTranportador_Sorting" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="100" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-ForeColor="#337ab7">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ckbSelecionarTodos" runat="server" Font-Size="Small" Text="Selecionar Todos" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
                                                    <asp:Button ID="btnExpandirRecolher" runat="server" CssClass="btn-default" CommandName="ExpandirRecolher" Text="Expandir/Recolher"></asp:Button>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckbSelecionar" runat="server" />
                                                    <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" CssClass="btnGrid" CommandName="Edit" CommandArgument='<%# Eval("ID_FRETE_TRANSPORTADOR") %>' OnClientClick="SalvaPosicao()" Text="Editar"><i class="glyphicon glyphicon-pencil" style="font-size:small"></i></div></asp:LinkButton>

                                                    <asp:LinkButton ID="btnDuplicar" runat="server" CssClass="btnGrid" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_FRETE_TRANSPORTADOR") %>'
                                                        Text="Visualizar" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></div></asp:LinkButton>

                                                    <asp:LinkButton ID="btnExcluir" title="Excluir" CssClass="btnGrid" runat="server" CommandName="Excluir"
                                                        OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_FRETE_TRANSPORTADOR") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:small"></span></asp:LinkButton>
                                                </ItemTemplate>


                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnSalvar" runat="server" CausesValidation="True" CommandName="Atualizar" CssClass="btnGrid" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');" CommandArgument='<%# Eval("ID_FRETE_TRANSPORTADOR") %>'><i class="glyphicon glyphicon-floppy-disk"  style="font-size:small"></i></asp:LinkButton>
                                                    &nbsp;<asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" CssClass="btnGrid"><i class="glyphicon glyphicon-remove"  style="font-size:small"></i></asp:LinkButton>
                                                </EditItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnNovo" runat="server" CausesValidation="False" CommandName="Incluir" Text="Incluir" CssClass="btnGrid"><i class="glyphicon glyphicon-plus"  style="font-size:small"></i></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="ID" HeaderStyle-ForeColor="#337ab7" SortExpression="ID_FRETE_TRANSPORTADOR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_FRETE_TRANSPORTADOR") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="POL" HeaderStyle-ForeColor="#337ab7" SortExpression="PORTO_ORIGEM">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("PORTO_ORIGEM") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlOrigem" runat="server" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlOrigem" runat="server" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="POD" HeaderStyle-ForeColor="#337ab7" SortExpression="PORTO_DESTINO">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("PORTO_DESTINO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDestino" runat="server" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlDestino" runat="server" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Carga" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_TIPO_CARGA">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("NM_TIPO_CARGA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoCarga" runat="server" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCarga" DataValueField="ID_TIPO_CARGA">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlTipoCarga" runat="server" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCarga" DataValueField="ID_TIPO_CARGA">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rota" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_VIA_ROTA">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("NM_VIA_ROTA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlRota" runat="server" DataTextField="NM_VIA_ROTA" DataSourceID="dsRota" DataValueField="ID_VIA_ROTA"></asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlRota" runat="server" DataTextField="NM_VIA_ROTA" DataSourceID="dsRota" DataValueField="ID_VIA_ROTA"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TTime Inicial" HeaderStyle-ForeColor="#337ab7" SortExpression="QT_DIAS_TRANSITTIME_INICIAL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTTInicial" runat="server" Text='<%# Eval("QT_DIAS_TRANSITTIME_INICIAL") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTTInicial" runat="server" Text='<%# Eval("QT_DIAS_TRANSITTIME_INICIAL") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTTInicial" runat="server" Text='<%# Eval("QT_DIAS_TRANSITTIME_INICIAL") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TTime Final" HeaderStyle-ForeColor="#337ab7" SortExpression="QT_DIAS_TRANSITTIME_FINAL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTTFinal" runat="server" Text='<%# Eval("QT_DIAS_TRANSITTIME_FINAL") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTTFinal" runat="server" Text='<%# Eval("QT_DIAS_TRANSITTIME_FINAL") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTTFinal" runat="server" Text='<%# Eval("QT_DIAS_TRANSITTIME_FINAL") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Transportador" HeaderStyle-ForeColor="#337ab7" SortExpression="Transportador">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("TRANSPORTADOR") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTransportador" runat="server" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportadorGrid" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlTransportador" runat="server" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportadorGrid" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Agente" HeaderStyle-ForeColor="#337ab7" SortExpression="Agente">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("AGENTE") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlAgente" runat="server" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgenteGrid" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlAgente" runat="server" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsAgenteGrid" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Obs. Cliente" HeaderStyle-ForeColor="#337ab7" SortExpression="OBS_CLIENTE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("OBS_CLIENTE_REDUZIDO") %>' ToolTip='<%# Eval("OBS_CLIENTE") %>' />
                                                    <asp:LinkButton ID="btnCopiarCliente" runat="server" CssClass="btnGrid" CausesValidation="False" CommandName="Cliente" CommandArgument='<%# Eval("OBS_CLIENTE") %>' Text="Copiar" OnClientClick="SalvaPosicao()"><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></i></div></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCliente" runat="server" Text='<%# Eval("OBS_CLIENTE") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCliente" runat="server" Text='<%# Eval("OBS_CLIENTE") %>'></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Obs. Interna" HeaderStyle-ForeColor="#337ab7" SortExpression="OBS_INTERNA">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterna" runat="server" Text='<%# Eval("OBS_INTERNA_REDUZIDO") %>' ToolTip='<%# Eval("OBS_INTERNA") %>' />
                                                    <asp:LinkButton ID="btnCopiarInterna" runat="server" CssClass="btnGrid" CausesValidation="False" CommandName="Interna" CommandArgument='<%# Eval("OBS_INTERNA") %>' Text="Copiar" OnClientClick="SalvaPosicao()"><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></i></div></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtInterna" runat="server" Text='<%# Eval("OBS_INTERNA") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtInterna" runat="server" Text='<%# Eval("OBS_INTERNA") %>'></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Container" SortExpression="QTD_CNTR" HeaderStyle-ForeColor="#337ab7">
                                                <ItemTemplate>
                                                    <a href="JavaScript:divexpandcollapse('div<%# Eval("ID_FRETE_TRANSPORTADOR") %>');">
                                                        <img id="imgdiv<%# Eval("ID_FRETE_TRANSPORTADOR") %>" border="0" src="Content/imagens/plus.png" alt="" /></a>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                                <EditItemTemplate>
                                                    <a href="JavaScript:divexpandcollapse('div<%# Eval("ID_FRETE_TRANSPORTADOR") %>');">
                                                        <img id="imgdiv<%# Eval("ID_FRETE_TRANSPORTADOR") %>" border="0" src="Content/imagens/plus.png" alt="" /></a>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Validade Final" HeaderStyle-ForeColor="#337ab7" SortExpression="DT_VALIDADE_FINAL">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("DT_VALIDADE_FINAL") %>' CssClass="data" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtValidadeFinal" runat="server" CssClass="data" Text='<%# Eval("DT_VALIDADE_FINAL") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValidadeFinal" runat="server" CssClass="data" Text='<%# Eval("DT_VALIDADE_FINAL") %>'></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Frequencia" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_TIPO_FREQUENCIA">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("NM_TIPO_FREQUENCIA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlFrequencia" runat="server" DataTextField="NM_TIPO_FREQUENCIA" DataSourceID="dsFrequencia" DataValueField="ID_TIPO_FREQUENCIA">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlFrequencia" runat="server" DataTextField="NM_TIPO_FREQUENCIA" DataSourceID="dsFrequencia" DataValueField="ID_TIPO_FREQUENCIA"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ativo?" HeaderStyle-ForeColor="#337ab7" SortExpression="Ativo">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("ATIVO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="ckAtivo" runat="server" Checked='<%# Eval("FL_ATIVO") %>'></asp:CheckBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="ckAtivo" runat="server" Checked='<%# Eval("FL_ATIVO") %>'></asp:CheckBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Historico" HeaderStyle-ForeColor="#337ab7" SortExpression="QTD_HISTORICO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQTD_HISTORICO" Visible="false" runat="server" Text='<%# Eval("QTD_HISTORICO") %>' />
                                                    <asp:LinkButton ID="btnHistorico" runat="server" CssClass="btnGrid" CausesValidation="False" CommandName="Historico" CommandArgument='<%# Eval("ID_FRETE_TRANSPORTADOR") %>' Text="Historico" OnClientClick="SalvaPosicao()"><i class="glyphicon glyphicon-list-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Consolidada?" HeaderStyle-ForeColor="#337ab7" SortExpression="FL_CONSOLIDADA">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("CONSOLIDADA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="ckConsolidada" runat="server" Checked='<%# Eval("FL_CONSOLIDADA") %>'></asp:CheckBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="ckConsolidada" runat="server" Checked='<%# Eval("FL_CONSOLIDADA") %>'></asp:CheckBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="8">

                                                            <div class="col-md-12">
                                                                <div id="div<%# Eval("ID_FRETE_TRANSPORTADOR") %>" style="display: none; position: relative; left: 15px; top: 10px; white-space: nowrap;">

                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="dgvCntr" DataKeyNames="ID_TARIFARIO_FRETE_TRANSPORTADOR" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." ShowHeaderWhenEmpty="true" ShowFooterWhenEmpty="true" ShowFooter="true" OnRowCommand="dgvCntr_RowCommand" OnRowEditing="dgvCntr_OnRowEditing" >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-ForeColor="#337ab7">

                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" CssClass="btnGrid" CommandName="EditarCntr" CommandArgument='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") %>' Text="Editar"><i class="glyphicon glyphicon-pencil" style="font-size:small"></i></div></asp:LinkButton>

                                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CssClass="btnGrid" CausesValidation="False" CommandName="DuplicarCntr" CommandArgument='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") %>'
                                                                                            Text="Duplicar" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></div></asp:LinkButton>

                                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" CssClass="btnGrid" runat="server" CommandName="ExcluirCntr"
                                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:small"></span></asp:LinkButton>
                                                                                    </ItemTemplate>


                                                                                    <EditItemTemplate>
                                                                                        <asp:LinkButton ID="btnSalvar" runat="server" CausesValidation="True" CommandName="AtualizarCntr" CssClass="btnGrid" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');" CommandArgument='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") & "|" & Eval("ID_FRETE_TRANSPORTADOR") %>'><i class="glyphicon glyphicon-floppy-disk"  style="font-size:small"></i></asp:LinkButton>
                                                                                        &nbsp;<asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" CssClass="btnGrid"><i class="glyphicon glyphicon-remove"  style="font-size:small"></i></asp:LinkButton>
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <asp:LinkButton ID="btnNovoCntr" runat="server" CausesValidation="False" CommandName="IncluirCntr" Text="Incluir" CssClass="btnGrid" CommandArgument='<%# Eval("ID_FRETE_TRANSPORTADOR") %>'><i class="glyphicon glyphicon-plus"  style="font-size:small"></i></asp:LinkButton>
                                                                                    </FooterTemplate>

                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="ID" SortExpression="ID_TARIFARIO_FRETE_TRANSPORTADOR">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID_TARIFARIO_FRETE_TRANSPORTADOR" runat="server" Text='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") %>' />
                                                                                        <asp:Label ID="lblID_FRETE_TRANSPORTADOR" Visible="False" runat="server" Text='<%# Eval("ID_FRETE_TRANSPORTADOR") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="CONTAINER" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_TIPO_CONTAINER">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Text='<%# Eval("NM_TIPO_CONTAINER") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="ddlCntr" runat="server" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER">
                                                                                        </asp:DropDownList>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:DropDownList ID="ddlCntr" runat="server" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER">
                                                                                        </asp:DropDownList>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="MOEDA" HeaderStyle-ForeColor="#337ab7" SortExpression="SIGLA_MOEDA">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Text='<%# Eval("SIGLA_MOEDA") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="ddlMoeda" runat="server" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                                                        </asp:DropDownList>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:DropDownList ID="ddlMoeda" runat="server" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                                                                        </asp:DropDownList>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="COMPRA" HeaderStyle-ForeColor="#337ab7" SortExpression="VL_COMPRA">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Text='<%# Eval("VL_COMPRA") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="txtCompra" runat="server" Text='<%# Eval("VL_COMPRA") %>' CssClass="valores"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox ID="txtCompra" runat="server" Text='<%# Eval("QT_DIAS_FREETIME") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="FREETIME" HeaderStyle-ForeColor="#337ab7" SortExpression="QT_DIAS_FREETIME">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Text='<%# Eval("QT_DIAS_FREETIME") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="txtFreeTime" runat="server" Text='<%# Eval("QT_DIAS_FREETIME") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox ID="txtFreeTime" runat="server" Text='<%# Eval("QT_DIAS_FREETIME") %>' CssClass="ApenasNumeros"></asp:TextBox>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="ORIGIN CHARGES" HeaderStyle-ForeColor="#337ab7" SortExpression="ORIGIN_CHARGES">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblORIGIN_CHARGES" runat="server" Text='<%# Eval("ORIGIN_CHARGES") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="MOEDA ORIGIN CHARGES" HeaderStyle-ForeColor="#337ab7" SortExpression="ORIGIN_CHARGES">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMOEDAORIGIN_CHARGES" runat="server" Text='<%# Eval("SIGLA_MOEDA") %>' />
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>

                                                                            <%--    <EmptyDataTemplate>

        <tr>
             
            <td>
                <asp:LinkButton ID="btnNovo" runat="server" CausesValidation="False" CommandName="IncluirCntr" Text="Incluir" CssClass="btnGrid" CommandArgument="0" ><i class="glyphicon glyphicon-plus"  style="font-size:small"></i></asp:LinkButton>
             </td>
            <td> 
               <asp:Label ID="lblID_FRETE_TRANSPORTADOR_INCLUSAO" Visible="true" runat="server" Text='<%# Eval("ID_FRETE_TRANSPORTADOR") %>'  />
              </td>
            <td>
                 <asp:DropDownList ID="ddlCntr" runat="server" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER"></asp:DropDownList>
            </td>
            <td>
                 <asp:DropDownList ID="ddlMoeda" runat="server" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"></asp:DropDownList>
            </td>
             <td>
                <asp:TextBox ID="txtCompra" runat="server" CssClass="valores" />
            </td>
             <td>
                <asp:TextBox ID="txtFreeTime" runat="server"  CssClass="ApenasNumeros"/>
            </td>
             <td>
            </td>
            <td>
            </td>


        </tr>
    </EmptyDataTemplate>--%>

                                                                            <HeaderStyle CssClass="headerStyle" />
                                                                        </asp:GridView>

                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </td>
                                                        <td colspan="10"></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                    </asp:GridView>















                                </div>


                                
                                <ajaxToolkit:ModalPopupExtender ID="mpeCntr" runat="server" PopupControlID="pnlCntr" TargetControlID="TextBox3" CancelControlID="TextBox4"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlCntr" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     
                                                <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">CONTAINER</h5>
                                                        </div>
                                                        <div class="modal-body">   
                                                            <div class="alert alert-danger" id="divErroCntr" runat="server" visible="false">
                                    <asp:Label ID="lblErroCntr" runat="server"></asp:Label>
                                </div>                        
                                    <div class="row"> 
                                         <div class="alert alert-success" id="divSuccessCntr" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccessCntr" runat="server"></asp:Label>
                                </div>                                       
                                  <div class="col-sm-3">
                                        <label class="control-label">Tipo de Container:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlContainer" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER" ></asp:DropDownList>                      
                                  </div>
                                   <div class="col-sm-3">
                                         <label class="control-label">Tipo moeda:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA" >
                                        </asp:DropDownList>                      
                                  </div>  
                                  <div class="col-sm-3">
                                         <label class="control-label">Valor da Compra:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorCompra" runat="server" CssClass="form-control moeda" maxlength="15" ></asp:TextBox>                     
                                  </div>  
                                  <div class="col-sm-3">
                                        <label class="control-label">Dias Freetime:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtFreetime" runat="server" CssClass="form-control inteiro" ></asp:TextBox>                    
                                  </div>    
                               </div>
                               <div class="modal-footer">                   
                                   <asp:Button runat="server" CssClass="btn btn-success" ID="btnGravarCntr" text="Gravar" /> 
                                   <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharCntr" text="Close" />
                              </div>
                                                    </div>    
                                                </div>  
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGravarCntr" />
                                    </Triggers>
                                </asp:UpdatePanel>



                                <ajaxToolkit:ModalPopupExtender ID="mpeHistorico" runat="server" PopupControlID="pnlHistorico" TargetControlID="Textbox1" CancelControlID="btnFecharHistorico"></ajaxToolkit:ModalPopupExtender>

                                <asp:Panel ID="pnlHistorico" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     
                                                <div class=" modal-dialog modal-dialog-centered" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">HISTORICO</h5>
                                                        </div>
                                                        <div class="modal-body">   
                                                            <div class="alert alert-danger" id="div1" runat="server" visible="false">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>                        
                                    <div class="row">
                                        <div class="col-sm-12">
                                        <div class="table-responsive tableFixHead">
<asp:GridView ID="dgvHistorico" DataKeyNames="ID_HISTORICO" DataSourceID="dsHistorico" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." >
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_HISTORICO") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ACAO" HeaderText="ACAO" SortExpression="ACAO" />
                                                <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" SortExpression="USUARIO" />
                                                <asp:BoundField DataField="DATA" HeaderText="DATA" SortExpression="DATA" />
                                             </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                       </div>  
                                     </div>                      
                                  </div>  
                               <div class="modal-footer">                   
                                   <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharHistorico" text="Close" />
                              </div>
                                                    </div>    
                                                </div>  
                                            </center>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                             <%--   <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFreteTranportador" />
                                <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvFreteTranportador" />--%>
                                <asp:PostBackTrigger ControlID="dgvFreteTranportador" />
                                <asp:PostBackTrigger ControlID="dgvFreteTranportador" />
                                <asp:AsyncPostBackTrigger ControlID="btnBusca" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsFreteTranportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT TOP 50 * FROM [View_FreteTransportador_new] WHERE DT_VALIDADE_FINAL >= GETDATE() order by ID_FRETE_TRANSPORTADOR DESC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCntr" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_FRETE_TRANSPORTADOR, 
B.ID_TARIFARIO_FRETE_TRANSPORTADOR,
C.NM_TIPO_CONTAINER,
B.QT_DIAS_FREETIME,
B.VL_COMPRA,
B.ID_MOEDA,
M.SIGLA_MOEDA,
A.ID_AGENTE,
CASE WHEN ID_VIATRANSPORTE = 1 AND ID_TIPO_COMEX = 1
THEN (SELECT SUM(VL_TAXA_COMPRA) FROM TB_TAXA_CLIENTE D WHERE A.ID_AGENTE = D.ID_PARCEIRO AND ID_TIPO_ESTUFAGEM = 1 AND D.ID_MOEDA_COMPRA = M.CD_MOEDA AND D.ID_BASE_CALCULO_TAXA IN (SELECT  I.ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA I WHERE ISNULL(I.ID_TIPO_CONTAINER,0) = B.ID_TIPO_CONTAINER) AND D.FL_DECLARADO = 1 AND ID_ORIGEM_PAGAMENTO = 2) ELSE 0 END ORIGIN_CHARGES
FROM TB_FRETE_TRANSPORTADOR A
INNER JOIN TB_TARIFARIO_FRETE_TRANSPORTADOR B ON A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR
INNER JOIN TB_TIPO_CONTAINER C ON B.ID_TIPO_CONTAINER= C.ID_TIPO_CONTAINER
INNER JOIN TB_MOEDA M ON M.ID_MOEDA= B.ID_MOEDA
WHERE A.ID_FRETE_TRANSPORTADOR =@ID_FRETE_TRANSPORTADOR ">
        <SelectParameters>
            <asp:Parameter Name="ID_FRETE_TRANSPORTADOR" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsHistorico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_HISTORICO, A.ID_FRETE_TRANSPORTADOR, A.ACAO, A.ID_USUARIO, B.NOME AS USUARIO, A.DATA FROM TB_FRETE_TRANSPORTADOR_HIST A INNER JOIN TB_USUARIO B ON A.ID_USUARIO = B.ID_USUARIO WHERE A.ID_FRETE_TRANSPORTADOR = @ID_FRETE_TRANSPORTADOR ORDER BY A.DATA DESC">
        <SelectParameters>
            <asp:Parameter Name="ID_FRETE_TRANSPORTADOR" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO]  WHERE NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = @ID_VIATRANSPORTE union SELECT  0, '    Selecione' ORDER BY NM_PORTO ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_VIATRANSPORTE" Type="Int32" ControlID="txtViaTransporte" DefaultValue="1" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTransportadorGrid" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1 union SELECT  0, '    Selecione'  ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CONTAINER"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgenteGrid" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1 union SELECT  0, '    Selecione' ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFrequencia" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_FREQUENCIA, NM_TIPO_FREQUENCIA FROM [dbo].[TB_TIPO_FREQUENCIA] 
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_FREQUENCIA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsCarga" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CARGA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsRota" runat="server" DataSourceMode="DataSet" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_VIA_ROTA,NM_VIA_ROTA FROM [dbo].[TB_VIA_ROTA]
union SELECT  0, 'Selecione' ORDER BY ID_VIA_ROTA"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_TRANSPORTADOR = 1 AND FL_ATIVO = 1 union SELECT  0,'', '  Selecione' ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL= 1 and FL_ATIVO = 1 union SELECT  0,'', '   Selecione' ORDER BY NM_RAZAO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] WHERE ID_MOEDA <> 124 union SELECT 0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA"></asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/css/bootstrap-select.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            Combos();
            InIEvent();
        });


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);


        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= TextBox1.ClientID %>').value;
            document.getElementById('DivGrid').scrollTop = valor;
            IDDestino();
            IDOrigem();
            copiarTexto();
            Combos();
            InIEvent();

        };

        function Combos() {
            $(".combos").select2({ placeholder: "  Selecione", allowClear: true });
            $(".combos").val(null).trigger('change');

            $(".combos2").select2({ multiple: true, placeholder: "  Selecione" });
            $(".combos2").val(null).trigger('change');

            $('.selectpicker').selectpicker();

        };

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


        function copiarTexto() {

            document.getElementById('<%= TextBox2.ClientID %>').style.display = 'block';
            let textoCopiado = document.getElementById('<%= TextBox2.ClientID %>');
            textoCopiado.select();
            textoCopiado.setSelectionRange(0, 99999)
            document.execCommand("copy");
            console.log("O texto é: " + textoCopiado.value);
            document.getElementById('<%= TextBox2.ClientID %>').style.display = 'none';

        }


        function IDDestino() {

            var selected = [];
            for (var option of document.getElementById('comboDestino').options) {
                if (option.selected) {
                    selected.push(option.value);
                }
            }
            console.log("IDDestino selected : " + selected);
            document.getElementById('<%= txtDestino.ClientID %>').value = selected;
        }

        function IDOrigem() {

            var selected = [];
            for (var option of document.getElementById('comboOrigem').options) {
                if (option.selected) {
                    selected.push(option.value);
                }
            }
            console.log("IDOrigem selected : " + selected);
            document.getElementById('<%= txtOrigem.ClientID %>').value = selected;
        }

        function InIEvent() {
            $(".valores").on("keypress keyup blur", function (e) {
                console.log("entrou")
                var chr = String.fromCharCode(e.which);
                if ("1234567890,".indexOf(chr) < 0)
                    return false;

            });
        }

        function divexpandcollapse(divname) {

            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display === "none") {
                div.style.display = "inline";
                img.src = "Content/imagens/minus.png";
            } else {
                div.style.display = "none";
                img.src = "Content/imagens/plus.png";
            }

        }

        function SomaComissoes() {
            var total = 0;
            var gridView = document.getElementById('<%= dgvFreteTranportador.ClientID %>');
            console.log('Inicio:');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');

                if (inputs[0].type == "checkbox") {
                    if (inputs[0].checked) {
                        console.log("sim");
                        var auxiliar = i - 1
                        console.log('auxiliar:' + auxiliar);
                        var label = 'MainContent_dgvComissoes_lblID_' + auxiliar


 
                        console.log('label:' + document.getElementById(label).innerHTML);
                     }

                }
            }
         };

    </script>
</asp:Content>
