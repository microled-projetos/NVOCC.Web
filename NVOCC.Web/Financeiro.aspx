<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Financeiro.aspx.vb" Inherits="NVOCC.Web.Financeiro" %>

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

        .teste {
            text-align: left
        }

        th {
            position: sticky !important;
            top: 0;
            background-color: #e6eefa;
            text-align: center;
        }

        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }
    </style>
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FINANCEIRO
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
                                <div class="row linhabotao">
                                    <div class="col-sm-4" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                        <asp:Label ID="Label4" Style="padding-left: 35px;" runat="server">Contas a Pagar</asp:Label><br />
                                        <asp:LinkButton ID="lkSolicitacaoPagamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Solicitação Pagamento</asp:LinkButton>
                                        <asp:LinkButton ID="lkMontagemPagamento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Montagem Pagamento</asp:LinkButton>
                                        <asp:LinkButton ID="lkBaixaCancel_Pagar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Baixas e Cancel</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                        <asp:Label ID="Label1" Style="padding-left: 35px" runat="server">Contas a Receber</asp:Label><br />
                                        <asp:LinkButton ID="lkCalcularRecebimento" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Calcular Recebimento</asp:LinkButton>
                                        <asp:LinkButton ID="lkEmissaoND" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Emissão ND</asp:LinkButton>
                                        <asp:LinkButton ID="lkBaixaCancel_Receber" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Baixas e Cancel</asp:LinkButton>
                                        <asp:LinkButton ID="lkFaturar" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Faturar</asp:LinkButton>

                                    </div>
                                    <div class="col-sm-2" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                        <asp:Label ID="Label5" Style="padding-left: 35px" runat="server">Integração TOTVS</asp:Label><br />
                                        <asp:LinkButton ID="lkNotaDespesa" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Nota Despesa</asp:LinkButton>
                                        <asp:LinkButton ID="lkPA" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">PA</asp:LinkButton>

                                    </div>
                                    <div class="col-sm-2" style="border: ridge 1px; padding-top: 20px; padding-bottom: 10px">

                                        <asp:Label ID="Label3" Style="padding-left: 35px" runat="server">Indicadores</asp:Label><br />

                                        <asp:LinkButton ID="lkInternacional" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Internacional</asp:LinkButton>
                                        <asp:LinkButton ID="lkNacional" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Nacional</asp:LinkButton>

                                    </div>
                                </div>

                                <br />
                                Filtro:
                   <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">

                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                   <asp:ListItem Value="1">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="2">Número do Master</asp:ListItem>
                                   <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                   <asp:ListItem Value="3">Referência  do Cliente</asp:ListItem>
                               </asp:DropDownList>
                           </div>

                       </div>
                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>
                       <div class="col-sm-1">

                           <div class="form-group">

                               <asp:CheckBoxList ID="ckStatus" Style="padding: 0px; font-size: 12px; text-align: justify" runat="server" RepeatDirection="vertical">
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Abertos</asp:ListItem>
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Fechados</asp:ListItem>
                                   <asp:ListItem Value="1" Selected="True">&nbsp;Cancelados</asp:ListItem>
                               </asp:CheckBoxList>
                           </div>
                       </div>
                       <div class="col-sm-1" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>

                   </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="Button1" CssClass="btn btn-success" />


                                <%--<ajaxToolkit:ModalPopupExtender id="mpeImprimir" runat="server" PopupControlID="Panel1" TargetControlID="lkImprimir"  CancelControlID="Close"></ajaxToolkit:ModalPopupExtender>--%>
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" style="width:300px">
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
                                <%--<asp:AsyncPostBackTrigger ControlID="lkFiltrar" />
             <asp:PostBackTrigger ControlID="lkImprimir" />
                   <asp:PostBackTrigger ControlID="btnImprimir" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead" visible="false" style="text-align: center">
                                    <asp:GridView ID="dgvFinanceiro" DataKeyNames="ID_COTACAO" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsCotacao" AutoGenerateColumns="False" Style="max-height: 600px; overflow: auto;" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="teste">
                                        <Columns>
                                            <asp:BoundField DataField="ID_COTACAO" HeaderText="#" Visible="false" />
                                            <asp:BoundField DataField="NR_COTACAO" HeaderText="Nº Cotação" SortExpression="NR_COTACAO" />
                                            <asp:BoundField DataField="DT_ABERTURA" HeaderText="Abertura" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DT_ABERTURA" />
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-CssClass="teste">
                                                <ItemTemplate>

                                                    <asp:Image ID="Image1" runat="server" />
                                                    -
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
                                            <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                            <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                            <asp:BoundField DataField="TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="TIPO_ESTUFAGEM" />
                                            <asp:BoundField DataField="NR_PROCESSO_GERADO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO_GERADO" />
                                            <asp:BoundField DataField="Servico" HeaderText="Serviço" SortExpression="Servico" />
                                            <asp:BoundField DataField="Agente" HeaderText="Agente" SortExpression="Agente" />
                                            <asp:BoundField DataField="COR" SortExpression="COR" ItemStyle-CssClass="none" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_COTACAO") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="lblAprovadas" runat="server"></asp:Label><br />
                                <asp:Label ID="lblRejeitadas" runat="server"></asp:Label>

                            </ContentTemplate>
                            <Triggers>
                                <%--       <asp:AsyncPostBackTrigger ControlID="bntPesquisar" />
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCotacao" />
       <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCotacao" />
       <asp:AsyncPostBackTrigger ControlID="lkCalcular" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>

    </div>


    <asp:SqlDataSource ID="dsCotacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_COTACAO,NR_COTACAO,
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

FROM TB_COTACAO A ORDER BY ID_COTACAO DESC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
