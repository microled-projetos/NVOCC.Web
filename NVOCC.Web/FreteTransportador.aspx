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
            <asp:Button runat="server" Text="TESTE" Visible="false" id="btnExport" CssClass="btn btn-success" />
                      <asp:Button runat="server" Visible="false"  Text="TESTE1" id="Button1" CssClass="btn btn-success" />


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
                            <div class="row" style="padding-left:20px" runat="server" id="divPesquisa" Visible="false" >                        
                               <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Consulta:</label>
                                        <asp:DropDownList ID="ddlConsultas" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="11px" >
                                             <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                            <asp:ListItem Value="1">OCEAN FREIGHT</asp:ListItem>
                                            <asp:ListItem Value="2">TAXAS LOCAIS</asp:ListItem>
                                        </asp:DropDownList>                                    </div>
                                </div></div>
                                                            

                               
                                    <div id="ocean" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Porto Origem:</label>
                                        <asp:DropDownList ID="ddlOrigemOcean" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                            <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Porto Destino:</label>
                                        <asp:DropDownList ID="ddlDestinoOcena" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                

                                             <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label>
                                        <asp:DropDownList ID="ddlTransportadorOcean" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                                             <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Container:</label>
                                        <asp:DropDownList ID="ddlContainer" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER" ></asp:DropDownList>            </div>
                                    </div>

                                </div>
                                        <div class="row">

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Data Inicial:</label>
                                        <asp:TextBox ID="txtDataInicial" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Data Final:</label>
                                        <asp:TextBox ID="txtDataFinal" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>

                            </div>
   <asp:Button runat="server" Text="Pesquisar" id="bntPesquisarOcean" CssClass="btn btn-success" />
                                    </div>
                                    <div id="locais" runat="server" visible="false">
                                         <div class="row">
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto Origem:</label>
                                        <asp:DropDownList ID="ddlOrigemLocais" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto Destino:</label>
                                        <asp:DropDownList ID="ddlDestinoLocais" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                             <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label>
                                        <asp:DropDownList ID="ddlTransportadorLocais" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                                </div>
                                <asp:Button runat="server" Text="Pesquisar" id="bntPesquisarLocais" CssClass="btn btn-success" />
                                    </div>
                       </div>



        <ajaxToolkit:ModalPopupExtender id="mpeImprimir" runat="server" PopupControlID="Panel1" TargetControlID="lkExportar"  CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
                                           <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
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
                                        <br />   <%--<a visible="false" href="GeraPDF.aspx?d=1" target="_blank"  >teste</a>--%> 
                                         <asp:LinkButton ID="lkExportaImpo" runat="server" Visible="false"  style="font-size:15px">Taxas Locais FCL - Impo</asp:LinkButton><br />

                                         <asp:LinkButton ID="lkExportaExpo" runat="server" Visible="false"  style="font-size:15px">Taxas Locais FCL - Expo</asp:LinkButton>

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
             <asp:AsyncPostBackTrigger ControlID="ddlConsultas" />
       <asp:AsyncPostBackTrigger ControlID="bntPesquisarOcean" />
             <asp:AsyncPostBackTrigger ControlID="bntPesquisarLocais" />
            <asp:PostBackTrigger ControlID="lkExportar" />
                  <asp:PostBackTrigger ControlID="lkExportaTarifario" />
                  <asp:PostBackTrigger ControlID="lkExportaImpo" />
                  <asp:PostBackTrigger ControlID="lkExportaExpo" />




    </Triggers>
   </asp:UpdatePanel>
                            <br />        

                             <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>

                            <div id="DivGrid" runat="server" class="table-responsive tableFixHead DivGrid" >
                                <asp:GridView ID="dgvFreteTranportador" DataKeyNames="Id,ID_TARIFARIO_FRETE_TRANSPORTADOR" CssClass="table table-hover table-sm grdViewTable dgvFreteTranportador" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFreteTranportador"  AutoGenerateColumns="false" onscroll="javascript:document.getElementById('scroll').value = this.scrollTop" style="max-height:600px; overflow:auto;" AllowSorting="true" OnSorting="dgvFreteTranportador_Sorting"  EmptyDataText="Nenhum registro encontrado." >
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
                                         <asp:BoundField DataField="Transportador" HeaderText="Transportador" ReadOnly="true" SortExpression="Transportador" />
                                        <asp:BoundField DataField="NM_FANTASIA" HeaderText="Agente" ReadOnly="true" SortExpression="NM_FANTASIA" />
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
                                        <asp:Button ID="btnEditarParcela" runat="server" CausesValidation="False" CommandName="Edit" Text="Selecionar"  CssClass="btn btn-primary" OnClientClick="teste()" CommandArgument='<%# Eval("id") %>'/>
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
       <asp:AsyncPostBackTrigger ControlID="bntPesquisarOcean" />
             <asp:AsyncPostBackTrigger ControlID="bntPesquisarLocais" />
             <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvFreteTranportador" />
    </Triggers>
   </asp:UpdatePanel>  
                        </div>
            
                     </div>
            </div>
        </div>
        </div> 
        <asp:SqlDataSource ID="dsFreteTranportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT A.ID_FRETE_TRANSPORTADOR as Id ,A.ID_TRANSPORTADOR,G.NM_FANTASIA Transportador, A.ID_AGENTE, F.NM_FANTASIA,A.ID_PORTO_ORIGEM,D.NM_PORTO as PORTO_ORIGEM,A.ID_PORTO_DESTINO,E.NM_PORTO as PORTO_DESTINO,A.QT_DIAS_TRANSITTIME_INICIAL,A.QT_DIAS_TRANSITTIME_FINAL,A.QT_DIAS_TRANSITTIME_MEDIA,A.FL_ATIVO,A.DT_VALIDADE_FINAL, 
case when A.FL_Ativo = 1 then 'Sim' else 'Não' end as Ativo,

(select MAX( C.NM_TIPO_CONTAINER + ':' + CAST(B.VL_COMPRA AS VARCHAR))AS Tarifario from TB_TARIFARIO_FRETE_TRANSPORTADOR B
LEFT JOIN TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) AS Tarifario,

(select MAX( DT_VALIDADE_INICIAL)AS DT_VALIDADE_INICIAL from TB_TARIFARIO_FRETE_TRANSPORTADOR B
WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) AS DT_VALIDADE_INICIAL,

(select MAX( ID_TARIFARIO_FRETE_TRANSPORTADOR)AS ID_TARIFARIO_FRETE_TRANSPORTADOR from TB_TARIFARIO_FRETE_TRANSPORTADOR B
WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) AS ID_TARIFARIO_FRETE_TRANSPORTADOR,

(select MAX( QT_DIAS_FREETIME)AS Tarifario from TB_TARIFARIO_FRETE_TRANSPORTADOR B
WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) AS QT_DIAS_FREETIME,

(select MAX( VL_COMPRA)AS Tarifario from TB_TARIFARIO_FRETE_TRANSPORTADOR B
WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) AS VL_COMPRA

FROM TB_FRETE_TRANSPORTADOR A
              
            left Join TB_PORTO D ON D.ID_PORTO = A.ID_PORTO_ORIGEM
            left Join TB_PORTO E ON E.ID_PORTO = A.ID_PORTO_DESTINO
            left join TB_PARCEIRO F ON F.ID_PARCEIRO = A.ID_AGENTE
            left join TB_PARCEIRO G ON G.ID_PARCEIRO = A.ID_TRANSPORTADOR

           order by ID_FRETE_TRANSPORTADOR DESC "
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
        selectcommand="SELECT ID_PORTO, NM_PORTO FROM [dbo].[TB_PORTO] union SELECT  0, 'Selecione' FROM [dbo].[TB_PORTO] ORDER BY ID_PORTO ">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_TIPO_CONTAINER] ORDER BY ID_TIPO_CONTAINER">
</asp:SqlDataSource>

<input id="div_position" name="div_position" hidden="hidden" class="div_position"/>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
 <%--       window.onload = function () {
            var div = document.getElementById("DivGrid");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form("DivGrid") %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };--%>

        ////$(window).load(function () {
        ////    $("html, body").animate({ scrollTop: $(document).height() }, 1000);
        ////});

        ////$('html, body').animate({
        ////    scrollTop: $('.dgvFreteTranportador').offset().top
        ////}, 500);

        //var xPos, yPos;
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_beginRequest(BeginRequestHandler);
        //prm.add_endRequest(EndRequestHandler);
        //function BeginRequestHandler(sender, args) {
        //    xPos = $get('DivGrid').scrollLeft;
        //    yPos = $get('DivGrid').scrollTop;
        //}
        //function EndRequestHandler(sender, args) {
        //    $get('DivGrid').scrollLeft = xPos;
        //    $get('DivGrid').scrollTop = yPos;
        //    console.log(xPos)
        //    console.log(yPos)

        //}

        function teste() {
            var posicao = document.getElementById('<%= DivGrid.ClientID %>').scrollTop;

            if (posicao) {
                document.getElementById('<%= DivGrid.ClientID %>').scrollTo = posicao;
                div_position.value = posicao;
                document.getElementById('<%= DivGrid.ClientID %>').scrollTop = posicao;
                console.log('if:' + posicao);
                document.getElementById('<%= DivGrid.ClientID %>').scrollTop = div_position;

            }
            else {
                posicao = document.getElementById('<%= DivGrid.ClientID %>').scrollTop;
                div_position.value = posicao;
                document.getElementById('<%= DivGrid.ClientID %>').scrollTop = posicao;
                console.log('else:' + posicao);
                document.getElementById('<%= DivGrid.ClientID %>').scrollTop = div_position;

            }
        };

        window.onload = function () {
            var div_position = document.getElementById("div_position").value;
            document.getElementById('<%= DivGrid.ClientID %>').scrollTop = div_position;
            console.log('load:' + div_position);

        };

        function x() {
            var div_position = document.getElementById("div_position").value;
            document.getElementById('<%= DivGrid.ClientID %>').scrollTop = div_position;
            console.log('load3:' + div_position);
        };

        function pageLoad(sender, args) {
            var div_position = document.getElementById("div_position").value;
            document.getElementById('<%= DivGrid.ClientID %>').scrollTop = div_position;
            console.log('load2:' + div_position);

        };

        var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
        pageRequestManager.add_endRequest(window.onload);

       
        
            //< body onload = "javascript:document.getElementById('div1').scrollTop = document.getElementById('scroll').value;" >

            //    <input type="hidden" id="scroll" runat="server" />

            //    <div id="div1" onscroll="javascript:document.getElementById('scroll').value = this.scrollTop">    
    </script> 
</asp:Content>
