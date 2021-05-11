<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BaixasCancelamentos.aspx.vb" Inherits="NVOCC.Web.BaixasCancelamentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">BAIXAS E CANCELAMENTOS - <asp:Label runat="server" ID="lblTipo" /></h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>

                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <br />

                             
                                    <div class="row linhabotao text-center" style="margin-left: 20px;border: ridge 1px;">
                                             <div class="col-sm-1">
                                            <div class="form-group">
                                                <label class="control-label" style="text-align: left"></label>
                                                <asp:TextBox ID="txtData" runat="server" placeholder="__/__/____" CssClass="form-control data"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-sm-2" runat="server" id="botoesPagamento">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixarPagamento" CssClass="btn btn-success" />
                                            <asp:Button runat="server" Text="Cancelar Fatura" ID="btnCancelarPagamento" CssClass="btn btn-danger" />
                                        </div>
                                    </div>
                                         <div class="col-sm-2" runat="server" id="botoesRecebimento" >
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixarRecebimento" CssClass="btn btn-success" />
                                            <asp:Button runat="server" Text="Cancelar Fatura" ID="btnCancelarRecebimento" CssClass="btn btn-danger" />
                                        </div>
                                    </div>
                                    </div>


                                <br />
                                <br />
                                <div class="row">
                                    <div class="table-responsive tableFixHead" runat="server"  id="gridPagar">
                                        <asp:GridView ID="dgvTaxasPagar" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsPagar" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_FATURA_FORNECEDOR" HeaderText="Nº Fatura" SortExpression="NR_FATURA_FORNECEDOR" />
                                                <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
<%--                                                <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA" />--%>
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                <asp:BoundField DataField="VL_DESCONTO" HeaderText="Desconto" SortExpression="VL_DESCONTO" />
                                                <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Acréscimo" SortExpression="VL_ACRESCIMO" />
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário laçamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div> 
                                    <div class="table-responsive tableFixHead" runat="server"  id="gridReceber">
                                        <asp:GridView ID="dgvTaxasReceber" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsReceber" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="NR_FATURA_FORNECEDOR" HeaderText="Nº Fatura" SortExpression="NR_FATURA_FORNECEDOR" />
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
<%--                                                <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA" />--%>
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                <asp:BoundField DataField="VL_DESCONTO" HeaderText="Desconto" SortExpression="VL_DESCONTO" />
                                                <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Acréscimo" SortExpression="VL_ACRESCIMO" />
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário laçamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>                                   

                                </div>
                                


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasPagar" />                                    <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasReceber" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsPagar" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] ORDER BY DT_VENCIMENTO DESC, NR_FATURA_FORNECEDOR">       
    </asp:SqlDataSource>
   
      <asp:SqlDataSource ID="dsReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] ORDER BY DT_VENCIMENTO DESC, NR_PROCESSO">       
    </asp:SqlDataSource>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
