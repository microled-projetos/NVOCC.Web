<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FreteTransportador_New.aspx.vb" Inherits="NVOCC.Web.FreteTransportador_New" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnn {
            background-color:#d5d8db;
            margin:5px;
            font-size:13px
        }
        .btnGrid{
            color:black;
            margin:5px;
        }
       
    </style>
      <div class="row principal">

          <div runat="server" id="divAuxiliar" visible="false" >
              <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX" Enabled="false"></asp:TextBox>
              <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX" Enabled="false"></asp:TextBox>
          </div>
                                
                 
        <div class="col-lg-12 table-responsive">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FRETE TRANSPORTADOR
                    </h3>
                </div>
                       
                <div class="panel-body">
                    <br />
                                                                <asp:LinkButton ID="lkInserir" runat="server"  CssClass="btn btn-primary btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
          
                        <div class="tab-pane fade active in" id="consulta">
                            <br />
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div class="alert alert-success" ID="divSuccess" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccess" runat="server"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErro" runat="server" visible="false">
                                        <asp:label ID="lblmsgErro" runat="server"></asp:label>
                                    </div>
                                                                                     
                                      <%-- <asp:LinkButton ID="lkAlterar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                       <asp:LinkButton ID="lkDuplicar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-duplicate" ></i>&nbsp;Duplicar</asp:LinkButton>
                                      <asp:LinkButton ID="lkFiltrar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-search" ></i>&nbsp;Filtrar</asp:LinkButton>
                                         <asp:LinkButton ID="lkInativar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i class="fa fa-toggle-on" ></i>&nbsp;Ativar/Inativar</asp:LinkButton>
                                                <asp:LinkButton ID="lkRemover" runat="server" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>
                                 
                                         <asp:LinkButton ID="lkSair" runat="server" OnClientClick="return confirm('Antes de sair verifique se há algum registro a ser salvo. Deseja mesmo sair?')" CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i class="glyphicon glyphicon-log-out" ></i>&nbsp;Sair</asp:LinkButton>--%>
                       
               <br />
                            <div class="row" runat="server" >                        
                               <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">ID:</label>
                                        <asp:TextBox ID="txtFiltroID" runat="server" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                                
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte:</label>
                                           <asp:TextBox ID="txtViaTransporte" runat="server" style="display:none" CssClass="form-control" />
                                        <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" AutoPostBack="TRUE" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                            <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Porto Origem:</label>
                                        <asp:DropDownList ID="ddlOrigem" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" SelectionMode="Multiple" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>   
                                        <%--<asp:ListBox ID="ListBox1" runat="server" CssClass="form-control" Rows="2"  DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO" SelectionMode="Multiple" ></asp:ListBox>
                                        <asp:CheckBoxList ID="CheckBoxList1" ItemType="CheckBox" runat="server"  DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO" SelectionMode="Multiple" AppendDataBoundItems="false"></asp:CheckBoxList>--%>
                                    </div>
                                </div>
                                            <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Porto Destino:</label>
                                        <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Validade Inicial:</label>
                                        <asp:TextBox ID="txtValidadeInicial" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>   
                                     <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Validade Final:</label>
                                        <asp:TextBox ID="txtValidadeFinal" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>
                                             <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label>
                                        <asp:DropDownList ID="ddlTransportador" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Agente:</label>
                                         <asp:DropDownList ID="ddlAgente" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO"></asp:DropDownList>         </div>
                                    </div>
                                  <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label" style="color:white">x:</label><br />
                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnBusca" text="Pesquisar" />
                                        </div>
                                    </div>

                                <div class="col-sm-1">
                                <div class="form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                    <asp:CheckBox ID="ckInativo" runat="server" Text="&nbsp;&nbsp;Inativo" Font-Size="Medium" />
                                </div>
                            </div>

                                  <div class="col-sm-1">
                                <div class="form-group">  
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                    <asp:LinkButton ID="lkExportar"  runat="server" CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i class="fa fa-file-excel"></i>&nbsp;Exportar</asp:LinkButton>
                                      </div>  </div>
                                    </div>
        

        <ajaxToolkit:ModalPopupExtender id="mpeImprimir" runat="server" PopupControlID="Panel1" TargetControlID="lkExportar"  CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
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
                          <asp:TextBox ID="TextBox2" Style="display:none" runat="server"></asp:TextBox> 

                             <div id="DivGrid" class="table-responsive tableFixHead DivGrid" >
                                <asp:GridView ID="dgvFreteTranportador" DataKeyNames="ID" CssClass="table table-hover table-sm grdViewTable dgvFreteTranportador" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFreteTranportador"  AutoGenerateColumns="false" style="max-height:600px; overflow:auto;" AllowSorting="true" OnSorting="dgvFreteTranportador_Sorting"  EmptyDataText="Nenhum registro encontrado." allowpaging="true" PageSize="100">
                                    <Columns>
                                       <asp:TemplateField  HeaderStyle-ForeColor="#337ab7">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ckbSelecionarTodos" runat="server" text="Selecionar Todos" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckbSelecionar" runat="server"/>

                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                                        
                                        <asp:TemplateField ShowHeader="False" >
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="True" CommandName="Update" CssClass="btnGrid" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');"><i class="glyphicon glyphicon-floppy-disk"  style="font-size:small"></i></asp:LinkButton> 
                                        &nbsp;<asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" CssClass="btnGrid" ><i class="glyphicon glyphicon-remove"  style="font-size:small"></i></asp:LinkButton> 
                                    </EditItemTemplate>
                                         <ItemTemplate>    
                                             <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" CssClass="btnGrid" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'  OnClientClick="SalvaPosicao()"
                                                                            Text="Editar" ><i class="glyphicon glyphicon-pencil" style="font-size:small"></i></div></asp:LinkButton>

                                             
                                             <asp:LinkButton ID="btnDuplicar" runat="server"  CssClass="btnGrid" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID") %>'
                                                                            Text="Visualizar" OnClientClick="javascript:return confirm('Deseja realmente duplicar este registro?');"><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></div></asp:LinkButton>
                                                                    
                                             <asp:LinkButton ID="btnExcluir" title="Excluir"  CssClass="btnGrid" runat="server"  CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:small"></span></asp:LinkButton>
                                                                    </ItemTemplate>

                                </asp:TemplateField>




                                          <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" />

                                         <asp:TemplateField HeaderText="POL" HeaderStyle-ForeColor="#337ab7" SortExpression="PORTO_ORIGEM">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("PORTO_ORIGEM") %>' />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:DropDownList ID="ddlOrigem" runat="server" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                                 </asp:DropDownList>
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="POD" HeaderStyle-ForeColor="#337ab7" SortExpression="PORTO_DESTINO">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("PORTO_DESTINO") %>' />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:DropDownList ID="ddlDestino" runat="server" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                                 </asp:DropDownList>
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Carga" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_TIPO_CARGA">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("NM_TIPO_CARGA") %>' />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:DropDownList ID="ddlTipoCarga" runat="server" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCarga" DataValueField="ID_TIPO_CARGA" >
                                        </asp:DropDownList>   
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rota" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_TIPO_CARGA">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("NM_VIA_ROTA") %>' />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:DropDownList ID="ddlRota" runat="server" DataTextField="NM_VIA_ROTA" DataSourceID="dsRota" DataValueField="ID_VIA_ROTA" >
                                        </asp:DropDownList>  
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                         <asp:BoundField DataField="QT_DIAS_TRANSITTIME_INICIAL" HeaderText="TTime Inicial" SortExpression="QT_DIAS_TRANSITTIME_INICIAL" />
                                         <asp:BoundField DataField="QT_DIAS_TRANSITTIME_FINAL" HeaderText="TTime Final" SortExpression="QT_DIAS_TRANSITTIME_FINAL" />

                                        <asp:TemplateField HeaderText="Transportador" HeaderStyle-ForeColor="#337ab7" SortExpression="Transportador">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("Transportador") %>'  />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:Textbox ID="txtTransportador" runat="server" Text='<%# Eval("Transportador") %>'>
                                        </asp:Textbox> 
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                         <asp:BoundField DataField="AGENTE" HeaderText="Agente" SortExpression="AGENTE" />
                                      
                                        <asp:TemplateField HeaderText="Obs. Cliente" HeaderStyle-ForeColor="#337ab7" SortExpression="OBS_CLIENTE">
                                                <ItemTemplate>
                                                    <asp:label ID="lblCliente" runat="server" Text='<%# Eval("OBS_CLIENTE_REDUZIDO") %>' ToolTip='<%# Eval("OBS_CLIENTE") %>' />
                                            <asp:LinkButton ID="btnCopiarCliente" runat="server"  CssClass="btnGrid" CausesValidation="False" CommandName="Cliente" CommandArgument='<%# Eval("OBS_CLIENTE") %>' Text="Copiar"  OnClientClick="SalvaPosicao()" ><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></i></div></asp:LinkButton>
                                                </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:Textbox ID="txtCliente" runat="server" Text='<%# Eval("OBS_CLIENTE") %>'></asp:Textbox> 
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Obs. Interna" HeaderStyle-ForeColor="#337ab7" SortExpression="OBS_INTERNA">
                                                <ItemTemplate>
                                                   <asp:label ID="lblInterna" runat="server" Text='<%# Eval("OBS_INTERNA_REDUZIDO") %>' ToolTip='<%# Eval("OBS_INTERNA") %>' />
                                            <asp:LinkButton ID="btnCopiarInterna" runat="server"  CssClass="btnGrid" CausesValidation="False" CommandName="Interna" CommandArgument='<%# Eval("OBS_INTERNA") %>' Text="Copiar"  OnClientClick="SalvaPosicao()" ><i class="glyphicon glyphicon-duplicate" style="font-size:small"></i></i></div></asp:LinkButton>
                                                </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:Textbox ID="txtInterna" runat="server" Text='<%# Eval("OBS_INTERNA") %>'></asp:Textbox> 
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo Container" SortExpression="QTD_CNTR" HeaderStyle-ForeColor="#337ab7" >
                                                <ItemTemplate>
                                                     <asp:label ID="lblQTDCNTR" Visible="false" runat="server" Text='<%# Eval("QTD_CNTR") %>' />                                            
                                                  <asp:LinkButton ID="btnCntr" runat="server"  CssClass="btnGrid" CausesValidation="False" CommandName="Cntr" CommandArgument='<%# Eval("ID") %>' Text="Detalhes Container"  OnClientClick="SalvaPosicao()"><i class="glyphicon glyphicon-plus-sign"  style="font-size:small"></i></div></asp:LinkButton>
                                             </ItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Validade Final" HeaderStyle-ForeColor="#337ab7" SortExpression="DT_VALIDADE_FINAL">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("DT_VALIDADE_FINAL") %>' CssClass="data" />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:Textbox ID="txtValidadeFinal" runat="server" CssClass="data" Text='<%# Eval("DT_VALIDADE_FINAL") %>'></asp:Textbox> 
                                             </EditItemTemplate>
                                         </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Frequencia" HeaderStyle-ForeColor="#337ab7" SortExpression="NM_TIPO_FREQUENCIA">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("NM_TIPO_FREQUENCIA") %>' />
                                                </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFrequencia" runat="server" DataTextField="NM_TIPO_FREQUENCIA" DataSourceID="dsFrequencia" DataValueField="ID_TIPO_FREQUENCIA">
                                        </asp:DropDownList> 
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ativo?" HeaderStyle-ForeColor="#337ab7" SortExpression="Ativo">
                                                <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("Ativo") %>' />
                                                </ItemTemplate>
                                             <EditItemTemplate>
                                                     <asp:CheckBox ID="ckAtivo" runat="server" checked='<%# Eval("FL_ATIVO") %>'></asp:CheckBox>
                                             </EditItemTemplate>
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Historico" HeaderStyle-ForeColor="#337ab7" SortExpression="QTD_HISTORICO">
                                                <ItemTemplate>
                                                     <asp:label ID="lblQTD_HISTORICO" Visible="false" runat="server" Text='<%# Eval("QTD_HISTORICO") %>' />
                                                     <asp:LinkButton ID="btnHistorico" runat="server"  CssClass="btnGrid" CausesValidation="False" CommandName="Historico" CommandArgument='<%# Eval("ID") %>' Text="Historico"  OnClientClick="SalvaPosicao()"></div></asp:LinkButton>
                                                     </ItemTemplate>
                                         </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Consolidada?" HeaderStyle-ForeColor="#337ab7" SortExpression="QTD_CONSOLIDADA">
                                             <ItemTemplate>
                                                     <asp:label runat="server" Text='<%# Eval("QTD_CONSOLIDADA") %>' />
                                                     </ItemTemplate>
                                             <EditItemTemplate>
                                                     <asp:CheckBox ID="ckConsolidada" runat="server" checked='<%# Eval("QTD_CONSOLIDADA") %>'></asp:CheckBox>
                                             </EditItemTemplate>

                                         </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
             
         </ContentTemplate>
  <Triggers>
        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFreteTranportador" />
        <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvFreteTranportador" />
        <asp:AsyncPostBackTrigger ControlID="btnBusca" />
    </Triggers>
   </asp:UpdatePanel>  
                        </div>
            
                     </div>
            </div>
        </div>
        </div> 
        <asp:SqlDataSource ID="dsFreteTranportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT * FROM [View_FreteTransportador_new] order by ID DESC"
        	updatecommand="UPDATE [dbo].[TB_FRETE_TRANSPORTADOR] SET QT_DIAS_TRANSITTIME_MEDIA = @QT_DIAS_TRANSITTIME_MEDIA WHERE ID_FRETE_TRANSPORTADOR =  @Id ; "  >
            <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="QT_DIAS_TRANSITTIME_MEDIA" Type="Int32" />           
        </UpdateParameters>
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>

    <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT top 10 ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO]  WHERE NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = @ID_VIATRANSPORTE union SELECT  0, ' Selecione' ORDER BY NM_PORTO ">
              <SelectParameters>
                <asp:ControlParameter Name="ID_VIATRANSPORTE" Type="Int32" ControlID="txtViaTransporte" DefaultValue="1" />
            </SelectParameters>
</asp:SqlDataSource>

        <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1
union SELECT  0, ' Selecione' ORDER BY NM_RAZAO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CONTAINER">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT  0, ' Selecione' ORDER BY NM_RAZAO">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsFrequencia" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_FREQUENCIA, NM_TIPO_FREQUENCIA FROM [dbo].[TB_TIPO_FREQUENCIA] 
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_FREQUENCIA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCarga" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CARGA">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsRota" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIA_ROTA,NM_VIA_ROTA FROM [dbo].[TB_VIA_ROTA]
union SELECT  0, 'Selecione' ORDER BY ID_VIA_ROTA">
</asp:SqlDataSource>
                  <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
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

    </script> 
</asp:Content>
