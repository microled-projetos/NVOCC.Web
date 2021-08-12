<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BuscaFreteTransportador.aspx.vb" Inherits="NVOCC.Web.BuscaFreteTransportador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>
          <div class="col-lg-12 table-responsive">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FRETE TRANSPORTADOR
                    </h3>
                </div>                      
                <div class="panel-body">
                    <br />
    <div class="row" style="padding-left:20px" runat="server" id="divPesquisa" >                        
                               <div class="row">
                                <div class="col-sm-2">
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
                                            <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte:</label>
                                           <asp:TextBox ID="txtViaTransporteOcean" runat="server" style="display:none" CssClass="form-control" />
                                        <asp:DropDownList ID="ddlViaTransporteOcean" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" AutoPostBack="TRUE" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                             <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Origem:</label>
                                        <asp:DropDownList ID="ddlOrigemOcean" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoOcean" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                            <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Destino:</label>
                                        <asp:DropDownList ID="ddlDestinoOcean" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoOcean" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                

                                             <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label>
                                        <asp:DropDownList ID="ddlTransportadorOcean" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                                             <%--<div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Container:</label>
                                        <asp:DropDownList ID="ddlContainer" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER" ></asp:DropDownList>            </div>
                                    </div>

                                </div>
                                        <div class="row">--%>

                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Validade inicial de:</label>
                                        <asp:TextBox ID="txtDataInicial" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Validade inicial até:</label>
                                        <asp:TextBox ID="txtDataFinal" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>

                            </div>
   <asp:Button runat="server" Text="Pesquisar" id="bntPesquisarOcean" CssClass="btn btn-success" />
                                    </div>
                                    <div id="locais" runat="server" visible="false">
                                         <div class="row"> 
                                             <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte:</label>
                                           <asp:TextBox ID="txtViaTransporteLocais" runat="server" style="display:none" CssClass="form-control" />
                                        <asp:DropDownList ID="ddlViaTransporteLocais" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" AutoPostBack="TRUE" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto:</label>
                                        <asp:DropDownList ID="ddlDestinoLocais" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPortoLocais" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
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
    <br/>
                                    <div class="alert alert-success" ID="divsuccess" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccess" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="diverro" runat="server" visible="false">
                                        <asp:label ID="lblmsgErro" runat="server"></asp:label>
                                    </div>
        <br/>
     
                            <div id="DivGridLocais" runat="server" class="table-responsive tableFixHead" visible="false">
                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_TAXA_LOCAL_TRANSPORTADOR" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsTaxas"  AutoGenerateColumns="false"  style="max-height:600px; overflow:auto;" AllowSorting="true" OnSorting="dgvTaxas_Sorting"  EmptyDataText="Nenhum registro encontrado." >
                                    <Columns>
                                        <asp:TemplateField>
	                                        <ItemTemplate>                                                                
		                                        <asp:LinkButton ID="lbSelecionar" runat="server" CausesValidation="False" CommandName="Select"
                                Style="display: none;"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID_TAXA_LOCAL_TRANSPORTADOR" HeaderText="#" SortExpression="ID_TAXA_LOCAL_TRANSPORTADOR" ReadOnly="true" />
                                        <asp:BoundField DataField="NM_TRANSPORTADOR" HeaderText="Transportador" ReadOnly="true" SortExpression="NM_TRANSPORTADOR" />
                                        <asp:BoundField DataField="Porto" HeaderText="Porto de Destino" ReadOnly="true" SortExpression="Porto" />
                                           <asp:BoundField DataField="Comex" HeaderText="Comex" ReadOnly="true" SortExpression="Comex" />
                                                 <asp:BoundField DataField="ViaTransporte" HeaderText="Via de Transporte" ReadOnly="true" SortExpression="ViaTransporte" />
                                        <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Validade Inicial" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_VALIDADE_INICIAL"/>                                                                       
                                        <asp:BoundField DataField="ItemDespesa" HeaderText="Item de Despesa" ReadOnly="true" SortExpression="ItemDespesa" />
                                        <asp:BoundField DataField="Moeda" HeaderText="Moeda" SortExpression="Moeda" />
                                        <asp:BoundField DataField="BaseCalculo" HeaderText="Base de Cálculo" SortExpression="BaseCalculo" />
                                        <asp:BoundField DataField="VL_TAXA_LOCAL_COMPRA" HeaderText="Valor de Compra" SortExpression="VL_TAXA_LOCAL_COMPRA" />
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
                    <div id="DivGridTarifario" runat="server" class="table-responsive tableFixHead" visible="false">
                                <asp:GridView ID="dgvTarifario" DataKeyNames="ID_TARIFARIO_FRETE_TRANSPORTADOR" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsTarifario"  AutoGenerateColumns="false"  style="max-height:600px; overflow:auto;" AllowSorting="true" OnSorting="dgvTaxas_Sorting"  EmptyDataText="Nenhum registro encontrado." >
                                    <Columns>
                                        <asp:TemplateField>
	                                        <ItemTemplate>                                                                
		                                        <asp:LinkButton ID="lbSelecionar" runat="server" CausesValidation="False" CommandName="Select"
                                Style="display: none;"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID_TARIFARIO_FRETE_TRANSPORTADOR" HeaderText="#" SortExpression="ID_TARIFARIO_FRETE_TRANSPORTADOR" ReadOnly="true" />
                                        <asp:BoundField DataField="ID_FRETE_TRANSPORTADOR" HeaderText="Tabela de Frete" ReadOnly="true" SortExpression="ID_FRETE_TRANSPORTADOR" />
                                        <asp:BoundField DataField="TRANSPORTADOR" HeaderText="Transportador" ReadOnly="true" SortExpression="TRANSPORTADOR" />
                                                                                <asp:BoundField DataField="ORIGEM" HeaderText="Porto de Origem" ReadOnly="true" SortExpression="ORIGEM" />

                                        <asp:BoundField DataField="DESTINO" HeaderText="Porto de Destino" ReadOnly="true" SortExpression="DESTINO" />
                                           <asp:BoundField DataField="ESTUFAGEM" HeaderText="Estufagem" ReadOnly="true" SortExpression="ESTUFAGEM" />
                                               
                                        <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Validade Inicial" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_VALIDADE_INICIAL"/>                                                      <asp:BoundField DataField="DT_VALIDADE_FINAL" HeaderText="Validade Final" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_VALIDADE_FINAL"/>                             
                                        <asp:BoundField DataField="MERCADORIA" HeaderText="Mercadoria" ReadOnly="true" SortExpression="MERCADORIA" />
                                        <asp:BoundField DataField="VL_MINIMO" HeaderText="Valor Minimo" SortExpression="VL_MINIMO" />
                                     <asp:BoundField DataField="VL_COMPRA" HeaderText="Valor de Compra" SortExpression="VL_COMPRA" />
                                        <asp:BoundField DataField="VL_M3_INICIAL" HeaderText="M3 Inicial" SortExpression="VL_M3_INICIAL" />               <asp:BoundField DataField="VL_M3_FINAL" HeaderText="M3 Final" SortExpression="VL_M3_FINAL" />
                                           <asp:BoundField DataField="QT_DIAS_FREETIME" HeaderText="FreeTime(dias)" SortExpression="QT_DIAS_FREETIME" />   <asp:BoundField DataField="TIPO_CONTAINER" HeaderText="Container" SortExpression="TIPO_CONTAINER" />


                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
                    </div>
                </div>
              </div>
    </ContentTemplate>
  <Triggers>
       <asp:AsyncPostBackTrigger ControlID="bntPesquisarOcean" />
             <asp:AsyncPostBackTrigger ControlID="bntPesquisarLocais" />
             <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxas" />
      <asp:AsyncPostBackTrigger ControlID="ddlViaTransporteLocais" />
      <asp:AsyncPostBackTrigger ControlID="ddlViaTransporteOcean" />
      
    </Triggers>
   </asp:UpdatePanel>

      <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
  <asp:SqlDataSource ID="dsPortoOcean" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO]  WHERE NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = @ID_VIATRANSPORTE union SELECT  0, ' Selecione' ORDER BY NM_PORTO ">
              <SelectParameters>
                <asp:ControlParameter Name="ID_VIATRANSPORTE" Type="Int32" ControlID="txtViaTransporteOcean" DefaultValue="1" />
            </SelectParameters>
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsPortoLocais" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO]  WHERE NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = @ID_VIATRANSPORTE union SELECT  0, ' Selecione' ORDER BY NM_PORTO ">
              <SelectParameters>
                <asp:ControlParameter Name="ID_VIATRANSPORTE" Type="Int32" ControlID="txtViaTransporteLocais" DefaultValue="1" />
            </SelectParameters>
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1
union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CONTAINER">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TAXA_LOCAL_TRANSPORTADOR, (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO)PORTO, (SELECT NM_TIPO_COMEX FROM TB_TIPO_COMEX WHERE ID_TIPO_COMEX = A.ID_TIPO_COMEX)COMEX, (SELECT NM_VIATRANSPORTE FROM TB_VIATRANSPORTE WHERE ID_VIATRANSPORTE = A.ID_VIATRANSPORTE)VIATRANSPORTE, (SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)ITEMDESPESA,(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO)BASECALCULO,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)MOEDA,
CONVERT(varchar,DT_VALIDADE_INICIAL,103)DT_VALIDADE_INICIAL,VL_TAXA_LOCAL_COMPRA FROM TB_TAXA_LOCAL_TRANSPORTADOR A">
         </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTarifario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT 
ID_TARIFARIO_FRETE_TRANSPORTADOR,
ID_FRETE_TRANSPORTADOR,
DT_VALIDADE_INICIAL,
DT_VALIDADE_FINAL,
VL_COMPRA,
VL_MINIMO,
VL_M3_INICIAL,
VL_M3_FINAL,
QT_DIAS_FREETIME,
(SELECT ID_PORTO_ORIGEM FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR)ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = (SELECT ID_PORTO_ORIGEM FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR))ORIGEM,
(SELECT ID_PORTO_DESTINO FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR)ID_PORTO_DESTINO,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = (SELECT ID_PORTO_DESTINO FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR))DESTINO,
(SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR)ID_TRANSPORTADOR,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = (SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR))TRANSPORTADOR,
(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM)ESTUFAGEM,
(select NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER)TIPO_CONTAINER,
(select NM_MERCADORIA FROM TB_MERCADORIA WHERE ID_MERCADORIA = A.ID_MERCADORIA)MERCADORIA,
SERVICO
FROM
TB_TARIFARIO_FRETE_TRANSPORTADOR A">
         </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>