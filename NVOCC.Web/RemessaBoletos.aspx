<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RemessaBoletos.aspx.vb" Inherits="NVOCC.Web.RemessaBoletos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> <style>
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
        .linha-colorida{
            text-align:center
        }
    </style>
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">BOLETOS PARA REMESSA
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>--%>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>
                                <asp:Label ID="Label1" runat="server"></asp:Label>

                                         <div class="row">
                                        <div class="col-sm-2">
                                    <div class="linha-colorida">Número Nota Fiscal</div>
                                     <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaNotaInicio" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
   </div>
                                    <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaNotaFim" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                                    </div>
                                    </div>                      
                                                             <div class="col-sm-2">      <div class="linha-colorida">Vencimento</div>
                                <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaVencimentoInicio" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                                                                <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaVencimentoFim" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                                                                 </div>
                                                                 <div class="col-sm-2">  
                      <div class="linha-colorida">Data de Emissao NF</div>
                                <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">De:</label>
                               <asp:TextBox ID="txtConsultaEmissaoInicio" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                              <div class="col-sm-6">
                                     <div class="form-group">
                                        <label class="control-label">Até:</label>
                               <asp:TextBox ID="txtConsultaEmissaoFim" runat="server" CssClass="form-control data"></asp:TextBox>
                           </div>
                                     </div>
                       </div>

                                                                                                                                                                                 
                               
                                                             
                                                                <div class="col-sm-3"><div class="linha-colorida">Cliente</div><br />
                                     <div class="form-group"> 
                              <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_PARCEIRO_CLIENTE" DataTextField="NM_CLIENTE" DataSourceID="dsClientes">
                                                </asp:DropDownList>
                           </div>
                                     </div>
                                              <div class="col-sm-2"><div class="linha-colorida">Banco</div><br />
                                     <div class="form-group"> 
                              <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTA_BANCARIA" DataSourceID="dsBanco" DataValueField="NR_BANCO">                                 
                                                </asp:DropDownList>
                           </div>
                                     </div>
                                         
                       <div class="col-sm-1">
                           <div style="color:white">x</div><br />
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                           </div>
                       </div>

                      
                   </div>

                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblContador" runat="server"></asp:Label>
                                </div>
                                <br />
                                <br />
                                  <div id="divBotoes" runat="server" visible="false">
                                      <div class="row">
                                <div class="col-sm-6">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Marcar Todos" ID="btnMarcar" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" Text="Desmarcar Todos" ID="btnDesmarcar" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>
                                                                      <br />

                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                    <asp:GridView ID="dgvFaturamento" DataKeyNames="ID_FATURAMENTO" DataSourceID="dsFaturamento" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." allowpaging="true" PageSize="100">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_FATURAMENTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckSelecionar" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                            <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                            <asp:BoundField DataField="NM_CLIENTE" HeaderText="Cliente" SortExpression="NM_CLIENTE" />
                                            <asp:BoundField DataField="NOSSONUMERO" HeaderText="Nosso Número" SortExpression="NOSSONUMERO" />
                                            <asp:BoundField DataField="NR_NOTA_FISCAL" HeaderText="Nota Fiscal" SortExpression="NR_NOTA_FISCAL" />
                                            <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="Data Nota Fiscal" SortExpression="DT_NOTA_FISCAL" />
                                            <asp:BoundField DataField="VL_BOLETO" HeaderText="Valor do Boleto" SortExpression="VL_BOLETO" />
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>

                            
                                   
                                    <div class="row">
                                <div class="col-sm-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Gerar Remessa" ID="btnEnviarRemessa" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                                </div>

                          <%--  </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnPesquisar" />
                                  <asp:PostBackTrigger ControlID="btnEnviarRemessa" />                                         

                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>

                </div>


            </div>
        </div>

    </div>
    <asp:SqlDataSource ID="dsFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Faturamento] WHERE NOSSONUMERO IS NOT NULL AND FL_ENVIADO_REM = 0 AND COD_BANCO = 0 ORDER BY DT_VENCIMENTO,NR_PROCESSO"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsBanco" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT NR_BANCO,NM_CONTA_BANCARIA FROM TB_CONTA_BANCARIA
union SELECT  '0', ' Selecione' ORDER BY NR_BANCO"></asp:SqlDataSource>
     <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT DISTINCT ID_PARCEIRO_CLIENTE, NM_CLIENTE FROM TB_FATURAMENTO WHERE NM_CLIENTE IS NOT NULL union SELECT  0, ' Selecione' ORDER BY NM_CLIENTE"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">   
</asp:Content>
