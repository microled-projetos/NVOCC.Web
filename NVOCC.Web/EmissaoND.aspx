<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmissaoND.aspx.vb" Inherits="NVOCC.Web.EmissaoND" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #DivImpressao, #imgFundo {
            display: none;
        }
        @media print {

            @page {
            }

            #divGrid, #imgFundo {
                display: none;
            }
            #DivImpressao{
                display: block;
            }
            td{
                padding-left:10px;
                padding-right:10px;

            }
        }
    </style> <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
    <div id="divGrid" class="divGrid">
        
        <asp:TextBox ID="txtID" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
       
        <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">NOTA DE DÉBITO 
                        <asp:Label runat="server" ID="lblMBL" CssClass="control-label" />

                    </h3>
                </div>
                <div class="panel-body">
                    <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                        <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                    </div>
                    <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                        <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                    </div>
                    <div class="tab-content">
                        <div class="table-responsive tableFixHead">
                            <asp:GridView ID="dgvNotas" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsNotas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                  <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VL_LANCAMENTO" HeaderText="Valor de Lançamento" SortExpression="VL_LANCAMENTO" />
                                    <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Valor Liquido" SortExpression="VL_LIQUIDO" />
                                    <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuario de Lançamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                CommandArgument='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' CommandName="Selecionar" Text="Imprimir Nota"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="headerStyle" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
 
                                        
    </div>


                                        </ContentTemplate>
<Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvNotas" />

                                        </Triggers>
                                    </asp:UpdatePanel>
    <asp:SqlDataSource ID="dsNotas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Contas_Receber] WHERE DT_CANCELAMENTO IS NULL AND (CD_PR = 'R') AND ID_BL = @ID_BL" >
         <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>      
        function ND() {
            var ID = document.getElementById('<%= txtID.ClientID %>').value;

            window.open('ImprimirND.aspx?id=' + ID, '_blank');

        }
    </script>
</asp:Content>
