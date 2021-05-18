<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Faturamento.aspx.vb" Inherits="NVOCC.Web.Faturamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FATURAMENTO
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
                                    <asp:GridView ID="dgvFaturamento" DataKeyNames="ID_FATURAMENTO" DataSourceID="dsFaturamento" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                         <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_FATURAMENTO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                    <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />
                                    <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="Cliente" SortExpression="PARCEIRO_EMPRESA" />
                                    <asp:BoundField DataField="REFERENCIA_CLIENTE" HeaderText="Ref. Cliente" SortExpression="REFERENCIA_CLIENTE" />
                                    <asp:BoundField DataField="VL_NOTA_DEBITO" HeaderText="Valor Nota de Deb." SortExpression="VL_NOTA_DEBITO" />
                                    <asp:BoundField DataField="NR_NOTA_DEBITO" HeaderText="Nota de Debito." SortExpression="NR_NOTA_DEBITO" />
                                    <asp:BoundField DataField="DT_NOTA_DEBITO" HeaderText="Data de Nota de Deb." SortExpression="DT_NOTA_DEBITO" />
                                    <asp:BoundField DataField="NR_RPS" HeaderText="Nota RPS" SortExpression="NR_RPS" />
                                    <asp:BoundField DataField="DT_RPS" HeaderText="Data Nota RPS" SortExpression="DT_RPS" />
                                    <asp:BoundField DataField="NR_RECIBO" HeaderText="Nota de Recibo" SortExpression="NR_RECIBO" />
                                    <asp:BoundField DataField="DT_RECIBO" HeaderText="Data Nota de Deb." SortExpression="DT_RECIBO" />
                                    <asp:BoundField DataField="NR_NOTA_FISCAL" HeaderText="Nota Fiscal" SortExpression="NR_NOTA_FISCAL" />
                                    <asp:BoundField DataField="DT_NOTA_FISCAL" HeaderText="Data Nota Fiscal" SortExpression="DT_NOTA_FISCAL" />
                                    <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="Data de Liquidação" SortExpression="DT_LIQUIDACAO" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                CommandArgument='<%# Eval("ID_FATURAMENTO") %>' CommandName="Selecionar" Text="Selecionar"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="headerStyle" />
                            </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFaturamento" />
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

</div>

   <asp:SqlDataSource ID="dsFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Faturamento] ORDER BY DT_VENCIMENTO,NR_PROCESSO"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
