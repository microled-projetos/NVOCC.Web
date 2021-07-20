<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FaturarRecebimento.aspx.vb" Inherits="NVOCC.Web.FaturarRecebimento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FATURAR RECEBIMENTO
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

                                Filtro:
                   <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">

                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                   <asp:ListItem Value="1">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="2">Número do Master</asp:ListItem>
                                   <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                   <asp:ListItem Value="4">Referência  do Cliente</asp:ListItem>
                               </asp:DropDownList>
                           </div>

                       </div>
                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>                       
                       <div class="col-sm-1" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>
                       
                   </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="btnPesquisar" CssClass="btn btn-success" />
                                


                        <br />
                  
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead" >
                                    <asp:GridView ID="dgvContasReceber" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsContasReceber" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                         <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                    <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="Cliente" SortExpression="PARCEIRO_EMPRESA" />
                                    <asp:BoundField DataField="QT_DIAS_FATURAMENTO" HeaderText="Qtd. Dias Faturamento" SortExpression="QT_DIAS_FATURAMENTO" />
                                    <asp:BoundField DataField="REFERENCIA_CLIENTE" HeaderText="Ref. Cliente" SortExpression="REFERENCIA_CLIENTE" />
                                    <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Valor" SortExpression="VL_LIQUIDO" />
                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Data de Vencimento" SortExpression="DT_VENCIMENTO" />
                                    <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="Data de Liquidação" SortExpression="DT_LIQUIDACAO" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                CommandArgument='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' CommandName="Selecionar" Text="Faturar"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="headerStyle" />
                            </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvContasReceber" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

</div>

   <asp:SqlDataSource ID="dsContasReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Contas_Receber] WHERE  DT_CANCELAMENTO IS NULL AND (CD_PR = 'R') AND ID_CONTA_PAGAR_RECEBER NOT IN (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE DT_CANCELAMENTO IS NULL)"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
