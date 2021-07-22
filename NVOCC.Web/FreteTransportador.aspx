<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FreteTransportador.aspx.vb" Inherits="NVOCC.Web.FreteTransportador" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnn {
            background-color:#d5d8db;
            margin:5px;
            font-size:13px
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
                                 <div class="row linhabotao text-center" >                                                        
                                      <asp:LinkButton ID="lkInserir" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
                                       <asp:LinkButton ID="lkAlterar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                       <asp:LinkButton ID="lkDuplicar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-duplicate" ></i>&nbsp;Duplicar</asp:LinkButton>
                                      <asp:LinkButton ID="lkFiltrar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i  class="glyphicon glyphicon-search" ></i>&nbsp;Filtrar</asp:LinkButton>
                                         <asp:LinkButton ID="lkInativar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i class="fa fa-toggle-on" ></i>&nbsp;Ativar/Inativar</asp:LinkButton>
                                                <asp:LinkButton ID="lkRemover" runat="server" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>
                                     <asp:LinkButton ID="lkExportar"  runat="server" CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i class="fa fa-file-excel"></i>&nbsp;Exportar</asp:LinkButton>
                                         <asp:LinkButton ID="lkSair" runat="server" OnClientClick="return confirm('Antes de sair verifique se há algum registro a ser salvo. Deseja mesmo sair?')" CssClass="btn btnn btn-default btn-sm" style="font-size:15px"><i class="glyphicon glyphicon-log-out" ></i>&nbsp;Sair</asp:LinkButton>
                       
                            </div>
               <br />
                            <div class="row" style="padding-left:20px" runat="server" id="divPesquisa" Visible="True" >                        
                               
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Validade Final:</label>
                                        <asp:TextBox ID="txtValidadeFinal" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>
                                            <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Porto Origem:</label>
                                        <asp:DropDownList ID="ddlOrigem" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                            <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Porto Destino:</label>
                                        <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                             <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label>
                                        <asp:DropDownList ID="ddlTransportador" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                                <div class="col-sm-3">
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


                                    </div>
        

        <ajaxToolkit:ModalPopupExtender id="mpeImprimir" runat="server" PopupControlID="Panel1" TargetControlID="lkExportar"  CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
                                           <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalMercaoriaNova">Exportar</h5>
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
       <asp:AsyncPostBackTrigger ControlID="lkFiltrar" />
            <asp:PostBackTrigger ControlID="lkExportar" />
                  <asp:PostBackTrigger ControlID="lkExportaTarifario" />
    </Triggers>
   </asp:UpdatePanel>
                            <br />        

                             <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>

                            <div id="DivGrid" class="table-responsive tableFixHead DivGrid" >
                                <asp:GridView ID="dgvFreteTranportador" DataKeyNames="Id,ID_TARIFARIO_FRETE_TRANSPORTADOR" CssClass="table table-hover table-sm grdViewTable dgvFreteTranportador" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFreteTranportador"  AutoGenerateColumns="false" style="max-height:600px; overflow:auto;" AllowSorting="true" OnSorting="dgvFreteTranportador_Sorting"  EmptyDataText="Nenhum registro encontrado." allowpaging="true" PageSize="100">
                                    <Columns>
                                        <asp:TemplateField>
	                                        <ItemTemplate>                                                                
		                                        <asp:LinkButton ID="lbSelecionar" runat="server" CausesValidation="False" CommandName="Select"
                                Style="display: none;"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" ReadOnly="true" />
                                        <asp:BoundField DataField="ID_TARIFARIO_FRETE_TRANSPORTADOR" HeaderText="#" SortExpression="ID_TARIFARIO_FRETE_TRANSPORTADOR" ReadOnly="true" Visible="false" />
                                        <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Validade Inicial" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_VALIDADE_INICIAL"/>
                                        <asp:BoundField DataField="DT_VALIDADE_FINAL" HeaderText="Validade Final" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_VALIDADE_FINAL" />
                                        <asp:BoundField DataField="PORTO_ORIGEM" HeaderText="Origem" ReadOnly="true" SortExpression="PORTO_ORIGEM" />
                                        <asp:BoundField DataField="PORTO_DESTINO" HeaderText="Destino" ReadOnly="true" SortExpression="PORTO_DESTINO" />
                                         <asp:BoundField DataField="Transportador" HeaderText="Transportador" ReadOnly="true" SortExpression="Transportador" />
                                        <asp:BoundField DataField="AGENTE" HeaderText="Agente" ReadOnly="true" SortExpression="AGENTE" />
                                        <asp:BoundField DataField="Tarifario" HeaderText="Tarifário" SortExpression="Tarifario" ReadOnly="true" />
                                        <asp:BoundField DataField="QT_DIAS_TRANSITTIME_MEDIA" HeaderText="TTime(Média)" SortExpression="QT_DIAS_TRANSITTIME_MEDIA" />
                                        <asp:BoundField DataField="QT_DIAS_FREETIME" HeaderText="FreeTime" SortExpression="QT_DIAS_FREETIME" />
                                        <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo" ReadOnly="true"/>
                                        <asp:TemplateField ShowHeader="False" >
                                    <EditItemTemplate>
                                        <asp:Button ID="btnEditarParcela" runat="server" CausesValidation="True" CommandName="Update" Text="Atualizar" CssClass="btn btn-success" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');"/>
                                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" CssClass="btn btn-danger" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarParcela" runat="server" CausesValidation="False" CommandName="Edit" Text="Selecionar"  CssClass="btn btn-primary" OnClientClick="SalvaPosicao()" CommandArgument='<%# Eval("id") %>'/>
                                    </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />

                                    <ControlStyle  />
                                </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
             
         </ContentTemplate>
  <Triggers>
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
        selectcommand="SELECT * FROM [View_FreteTransportador] order by ID DESC "
        	updatecommand="UPDATE [dbo].[TB_FRETE_TRANSPORTADOR] SET QT_DIAS_TRANSITTIME_INICIAL = @QT_DIAS_TRANSITTIME_INICIAL , QT_DIAS_TRANSITTIME_FINAL = @QT_DIAS_TRANSITTIME_FINAL , QT_DIAS_TRANSITTIME_MEDIA = @QT_DIAS_TRANSITTIME_MEDIA WHERE ID_FRETE_TRANSPORTADOR =  @Id ; 
            
            UPDATE [dbo].[TB_TARIFARIO_FRETE_TRANSPORTADOR] SET QT_DIAS_FREETIME = @QT_DIAS_FREETIME WHERE ID_FRETE_TRANSPORTADOR =  @Id AND ID_TARIFARIO_FRETE_TRANSPORTADOR = @ID_TARIFARIO_FRETE_TRANSPORTADOR "  >
            <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="ID_TARIFARIO_FRETE_TRANSPORTADOR" Type="Int32" />
            <asp:Parameter Name="QT_DIAS_FREETIME" Type="Int32" />

            <asp:Parameter Name="QT_DIAS_TRANSITTIME_MEDIA" Type="Int32" />
            <asp:Parameter Name="QT_DIAS_TRANSITTIME_INICIAL" Type="Int32" />
            <asp:Parameter Name="QT_DIAS_TRANSITTIME_FINAL" Type="Int32" />
        </UpdateParameters>
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PORTO, NM_PORTO FROM [dbo].[TB_PORTO] union SELECT  0, 'Selecione' ORDER BY ID_PORTO ">
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

    </script> 
</asp:Content>
