<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CotacaoComercial.aspx.vb" Inherits="NVOCC.Web.CotacaoComercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnn {
            background-color:#d5d8db;
            margin:5px;
            font-size:13px
        }
        .selected1 {
            color:black; font-family:verdana; font-size:8pt; background-color:#e6c3a5;

        }
        .none{
            display:none
        }
       .teste{
           text-align:left;
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
                    <h3 class="panel-title">COTAÇÃO COMERCIAL <asp:label ID="lblteste" runat="server"></asp:label>
                    </h3>
                </div>
                       
                <div class="panel-body">
                        <div class="tab-pane fade active in" id="consulta">
                            <br />                           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>            

                              <div class="alert alert-success" ID="divSuccess" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccess" runat="server"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErro" runat="server" visible="false">
                                        <asp:label ID="lblmsgErro" runat="server"></asp:label>
                                    </div>
                            <br/>
                                 <div class="row linhabotao text-center" >                                                        
                                      <asp:LinkButton ID="lkInserir" runat="server"  CssClass="btn  btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-plus"></i>&nbsp;Inserir</asp:LinkButton>
                                       <asp:LinkButton ID="lkAlterar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-pencil"></i>&nbsp;Alterar</asp:LinkButton>
                                       <asp:LinkButton ID="lkDuplicar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-duplicate"></i>&nbsp;Duplicar</asp:LinkButton>
                                       <asp:LinkButton ID="lkRemover" runat="server" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-trash"></i>&nbsp;Remover</asp:LinkButton>
                                       <asp:LinkButton ID="lkImprimir"  runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-print"></i>&nbsp;Imprimir</asp:LinkButton>  
                                     <asp:LinkButton ID="lkFiltrar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="glyphicon glyphicon-search"></i>&nbsp;Filtrar</asp:LinkButton>
                                       <asp:LinkButton ID="lkCalcular" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i  class="fa fa-calculator"></i>&nbsp;Calcular</asp:LinkButton>
                                      <asp:LinkButton ID="lkAprovar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i class="fa fa-check-circle"></i>&nbsp;Aprovar</asp:LinkButton>
                                      <asp:LinkButton ID="lkCancelar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i class="glyphicon glyphicon-ban-circle"></i>&nbsp;Cancelar</asp:LinkButton>
                                      <asp:LinkButton ID="lkRejeitar" runat="server"  CssClass="btn btnn btn-default btn-sm" style="font-size:15px" ><i class="glyphicon glyphicon-remove-sign"></i>&nbsp;Rejeitar</asp:LinkButton>
                            </div>
               <br />
                            <div class="row linhabotao" runat="server" id="divPesquisa" Visible="false" > 
                                 <asp:label ID="Label1" style="padding-left:18px" runat="server">Filtro:</asp:label> 
                                <div>
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlConsultas" AutoPostBack="true" Width="230px" runat="server" CssClass="form-control" Font-Size="15px" >
                                             <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                             <asp:ListItem Value="1">Número da cotação</asp:ListItem>
                                             <asp:ListItem Value="2">Status da cotação</asp:ListItem>
                                             <asp:ListItem Value="3">Cliente</asp:ListItem>
                                             <asp:ListItem Value="4">Origem</asp:ListItem>
                                             <asp:ListItem Value="5">Destino</asp:ListItem>
                                             <asp:ListItem Value="6">Nome do agente</asp:ListItem>
                                        </asp:DropDownList>      </div> </div>
                                      <div class="col-sm-3">
                                    <div class="form-group">
                                          <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                       </div></div>
                                
                                                                <asp:Button runat="server" Text="Pesquisar" id="bntPesquisar" CssClass="btn btn-success" />
</div>
                                </div>
                           

             <asp:Button runat="server" Text="Pesquisar" Style="display:none" id="Button1" CssClass="btn btn-success" />

                            
           <ajaxToolkit:ModalPopupExtender id="mpeImprimir" runat="server" PopupControlID="Panel1" TargetControlID="lkImprimir"  CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
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
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>       
     </asp:Panel>
         </ContentTemplate>
  <Triggers>
       <asp:AsyncPostBackTrigger ControlID="lkFiltrar" />
             <asp:PostBackTrigger ControlID="lkImprimir" />
                   <asp:PostBackTrigger ControlID="btnImprimir" />
    </Triggers>
   </asp:UpdatePanel> 
                            <br />
                             <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div runat="server" id="divAuxiliar" visible="false" >
              <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
              <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
              <asp:TextBox ID="txtServico" runat="server" CssClass="form-control"></asp:TextBox>
              <asp:TextBox ID="txtEstufagem" runat="server" CssClass="form-control"></asp:TextBox>
              <asp:TextBox ID="txtNumeroCotacao" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
                            <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align:center" >
                                <asp:GridView ID="dgvCotacao" DataKeyNames="ID_COTACAO" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsCotacao"  AutoGenerateColumns="False" style="max-height:600px; overflow:auto;" AllowSorting="True" OnSorting="dgvCotacao_Sorting"  EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="teste" allowpaging="true" PageSize="100">
                                    <Columns >
                                       <asp:BoundField DataField="ID_COTACAO" HeaderText="#" visible="false" />
                                        <asp:BoundField DataField="NR_COTACAO" HeaderText="Nº Cotação" SortExpression="NR_COTACAO"/>
                                        <asp:BoundField DataField="DT_ABERTURA" HeaderText="Abertura" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_ABERTURA"/>                            
                                       <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-CssClass="teste">
                    <ItemTemplate>
                                            
                        <asp:Image ID="Image1" runat="server" /> - <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
                                        <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                        <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                        <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />
                                        <asp:BoundField DataField="NR_PROCESSO_GERADO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO_GERADO"/>
                                        <asp:BoundField DataField="Servico" HeaderText="Serviço" SortExpression="Servico"/>
                                        <asp:BoundField DataField="Agente" HeaderText="Agente" SortExpression="Agente"/>
                                        <asp:BoundField DataField="COR"  SortExpression="COR" ItemStyle-CssClass="none" />
                                          <asp:TemplateField HeaderText="">
                                              <ItemTemplate>
                                                 <asp:linkButton ID="btnSelecionar" runat="server"  CssClass="btn btn-primary btn-sm" 
                                CommandArgument='<%# Eval("ID_COTACAO") & "|" & Container.DataItemIndex %>'   CommandName="Selecionar" Text="Selecionar"  OnClientClick="SalvaPosicao()"></asp:linkButton>                     
                                              </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>               
                                    </Columns>
                                    <HeaderStyle  CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
                                                <asp:label ID="lblAprovadas" runat="server"></asp:label><br />
                                        <asp:label ID="lblRejeitadas" runat="server"></asp:label>

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
        selectcommand="SELECT ID_COTACAO,NR_COTACAO,
A.ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM) Origem,

A.ID_PORTO_DESTINO, 
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) Destino,

A.ID_PORTO_ESCALA1,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA1) Escala,

A.ID_CLIENTE_FINAL,
(SELECT NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL WHERE ID_CLIENTE_FINAL = A.ID_CLIENTE_FINAL) CLIENTE_FINAL,
ID_CLIENTE ,
            (SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE )AS CLIENTE,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_AGENTE_INTERNACIONAL and FL_AGENTE_INTERNACIONAL = 1) AGENTE,

A.ID_TIPO_ESTUFAGEM,
(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM) TIPO_ESTUFAGEM,

A.NR_PROCESSO_GERADO,
(SELECT NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SERVICO,
NR_COTACAO, 
DT_ABERTURA,
ID_STATUS_COTACAO,
(SELECT NM_STATUS_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO)STATUS,
(SELECT CD_COR FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO)COR

FROM TB_COTACAO A ORDER BY ID_COTACAO DESC">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO">
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
