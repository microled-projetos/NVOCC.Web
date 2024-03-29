﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Financeiro.aspx.vb" Inherits="NVOCC.Web.Financeiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnn {
            background-color: #d5d8db;
            margin: 3px;
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>
<div class="row linhabotao" >           
                       <div class="col-sm-4">
                           <asp:Label ID="Label6" runat="server">CONTAS A PAGAR:</asp:Label><br />
                           <div style="border: ridge 1px;">
                          <asp:LinkButton ID="lkSolicitacaoPagamento" runat="server" CssClass="btn btnn btn-default btn-sm">Solicitar Pagamento</asp:LinkButton>
                            <asp:LinkButton ID="lkMontagemPagamento" runat="server" CssClass="btn btnn btn-default btn-sm">Montar Pagamento</asp:LinkButton>
                           <asp:LinkButton ID="lkBaixaCancel_Pagar" runat="server" CssClass="btn btnn btn-default btn-sm">Baixar/Cancelar</asp:LinkButton></div>

                       </div> 
                                
                      
                       <div class="col-sm-4">
                                      <asp:Label ID="Label1" runat="server">CONTAS A RECEBER:</asp:Label><br />
                                        <div style="border: ridge 1px;">

                          <asp:LinkButton ID="lkCalcularRecebimento" runat="server" CssClass="btn btnn btn-default btn-sm">Calcular Recebimento</asp:LinkButton>
                           <asp:LinkButton ID="lkEmissaoND" runat="server" CssClass="btn btnn btn-default btn-sm">Emitir ND</asp:LinkButton>
                            <asp:LinkButton ID="lkBaixaCancel_Receber" runat="server" CssClass="btn btnn btn-default btn-sm">Baixar/Cancelar</asp:LinkButton>
                           <asp:LinkButton ID="lkFaturar" runat="server" CssClass="btn btnn btn-default btn-sm">Enviar para faturamento</asp:LinkButton></div>
                                        </div>
                       
                 

    </div>
    
    <br /><br /><br />
                   <div class="row linhabotao" >
   
                       <div class="col-sm-2">
                            <asp:Label ID="Label2" runat="server">FILTRO:</asp:Label>
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" Font-Size="15px" AutoPostBack="TRUE">
                                   <asp:ListItem Value="1">Número do Master</asp:ListItem>
                                   <asp:ListItem Value="3">Número do House</asp:ListItem>
                                   <asp:ListItem Value="2">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="5">Periodo de chegada</asp:ListItem>
                                   <asp:ListItem Value="6">Tipo Faturamento</asp:ListItem>
                                   <asp:ListItem Value="4" Selected="True">Todos em aberto</asp:ListItem>

                               </asp:DropDownList>
                           </div>

                       </div>
                     
                            <div runat="server" ID="divBusca">
                                 <div class="col-sm-2" >
                           <div class="form-group">
                                                           <asp:Label ID="Label3" Style="color:white" runat="server">x</asp:Label>

                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                                </div>
                           </div>
                           <div runat="server" ID="divDatasBusca"  style="display: none">
                        <div class="col-sm-1">                        
                               <div class="form-group">De:
                               <asp:TextBox ID="txtDataInicioBusca" runat="server"  CssClass="form-control data"></asp:TextBox>
                           </div>
                               </div>
                           <div class="col-sm-1" >
                               <div class="form-group">Até:                       
                               <asp:TextBox ID="txtDataFimBusca" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                               </div>
                       </div>
                            <div runat="server" ID="divddlBusca"  style="display: none">
                                    <div class="col-sm-2" >
                           <div class="form-group">
                                                           <asp:Label ID="Label5" Style="color:white" runat="server">x</asp:Label>

                               <asp:DropDownList ID="ddlTipoFaturamento" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_FATURAMENTO" DataSourceID="dsTipoFaturamento" DataValueField="ID_TIPO_FATURAMENTO">                                             
                                                </asp:DropDownList>
                           </div>
                                </div>
                       </div> 
                       <div class="col-sm-1" >
                           <div class="form-group">
                                                           <asp:Label ID="Label4" Style="color:white" runat="server">X</asp:Label><br />

                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>


                       <div class="col-sm-2" style="border: ridge 1px;">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label>
                                                    <asp:RadioButtonList ID="rdTransporte" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Marítimo</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Aéreo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                       <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <label class="control-label">Serviço:</label>
                                                    <asp:RadioButtonList ID="rdServico" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Importação</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Exportação</asp:ListItem>
                                                        <asp:ListItem Value="0">&nbsp;Outros</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                          
                       
                        
                   </div> 
                    
          

                               

                               

                                
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
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
                                    <asp:GridView ID="dgvFinanceiro" DataKeyNames="ID_BL" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFinanceiro" AutoGenerateColumns="False" Style="max-height: 600px; overflow: auto;" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <asp:BoundField DataField="ID_BL" HeaderText="#" Visible="false" />
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                            <asp:BoundField DataField="NR_BL_MASTER" HeaderText="MBL" SortExpression="NR_BL_MASTER" />
                                            <asp:BoundField DataField="DT_EMBARQUE_MASTER" HeaderText="Embarque" SortExpression="DT_EMBARQUE_MASTER" />
                                            <asp:BoundField DataField="DT_CHEGADA_MASTER" HeaderText="Chegada" SortExpression="DT_CHEGADA_MASTER" />
                                            <asp:BoundField DataField="NM_PARCEIRO_CLIENTE" HeaderText="Cliente" SortExpression="NM_PARCEIRO_CLIENTE" />
                                            <asp:BoundField DataField="NM_CLIENTE_FINAL" HeaderText="Cliente Final" SortExpression="NM_CLIENTE_FINAL" />
                                            <asp:BoundField DataField="NM_PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="NM_PARCEIRO_TRANSPORTADOR" />
                                            <asp:BoundField DataField="QT_TAXAS_PAGAR" HeaderText="Qtd. taxas a Pagar" SortExpression="QT_TAXAS_PAGAR" />
                                            <asp:BoundField DataField="QT_TAXAS_PAGAS" HeaderText="Qtd. taxas Pagas" SortExpression="QT_TAXAS_PAGAS" />
                                            <asp:BoundField DataField="QT_TAXAS_PAGAR_ABERTA" HeaderText="Qtd. taxas (a Pagar) em aberto" SortExpression="QT_TAXAS_PAGAR_ABERTA" />
                                            <asp:BoundField DataField="QT_TAXAS_RECEBER" HeaderText="Qtd. taxas a Receber" SortExpression="QT_TAXAS_RECEBER" />
                                            <asp:BoundField DataField="QT_TAXAS_RECEBIDAS" HeaderText="Qtd. Quantidade taxas Recebidas" SortExpression="QT_TAXAS_RECEBIDAS" />
                                            <asp:BoundField DataField="QT_TAXAS_RECEBER_ABERTA" HeaderText="Quantidade taxas (a Receber) em aberto" SortExpression="QT_TAXAS_RECEBER_ABERTA" />
                                            <asp:BoundField DataField="TIPO_FATURAMENTO" HeaderText="Tipo Faturamento" SortExpression="TIPO_FATURAMENTO" />
                                            <asp:BoundField DataField="QT_DIAS_FATURAMENTO" HeaderText="Qtd. Dias Faturamento" SortExpression="QT_DIAS_FATURAMENTO" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"  OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                            
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

    <asp:SqlDataSource ID="dsTipoFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_FATURAMENTO, substring(NM_TIPO_FATURAMENTO,0,11)NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsFinanceiro" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [View_Financeiro] WHERE QT_TAXAS_PAGAR_ABERTA > 0 OR QT_TAXAS_RECEBER_ABERTA > 0 ORDER BY NR_PROCESSO"></asp:SqlDataSource>
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
