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
        }
    </style> <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
    <div id="divGrid" class="divGrid">

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
                                    <asp:BoundField DataField="VL_DESCONTO" HeaderText="Valor de Desconto" SortExpression="VL_DESCONTO" />
                                    <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Valor de Acréscimo" SortExpression="VL_ACRESCIMO" />
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

    <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">
                <table border="1">
                    <tr>
                        <td>
        <table border="1">
            <tr>
                <td>
                    <center>
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br /></center>
                    <div style="text-align: center;">
                        R QUINZE DE NOVEMBRO, 46/48 - CENTRO<br />
                        SANTOS - SÃO PAULO - BRASIL - CEP:11010150<br />
                        FONE:+55 13 3797-7850 - FAX:<br />
                        CNPJ:00.693.367/0003-11  &nbsp;&nbsp;&nbsp;I.E: 633.672.235.110
                    </div>
                </td>
                <td>
                    <center>
    <img src="Content/imagens/FCA-Log - Copia.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                </td>
            </tr>
        </table>
        <table border="1">

            <tr>
                <td>
                    <div style="text-align: center;">NOTA DE DÉBITO: <asp:Label ID="lblProcesso" runat="server" /></div>
                </td>
            </tr>
        </table>
        <table  style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>CLIENTE</strong>&nbsp;<asp:Label ID="lblEmpresa" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CNPJ:</strong>&nbsp;<asp:Label ID="lblCNPJ" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>ENDEREÇO:</strong>&nbsp;<asp:Label ID="lblEndereco" runat="server" /><asp:Label ID="lblNumero" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CEP:</strong>&nbsp;<asp:Label ID="lblCEP" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>BAIRRO:</strong>&nbsp;<asp:Label ID="lblBairro" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CIDADE:</strong>&nbsp;<asp:Label ID="lblCidade" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>TELEFONE:</strong>&nbsp;<asp:Label ID="lblTelefone" runat="server" />
                    <br />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table border="1">

            <tr>
                <td>
                    <strong>DATA DE EMISSAO:</strong>&nbsp;<asp:Label ID="lblDataEmissao" runat="server" />

                </td>
                <td><strong>DATA DE VENCIMENTO:</strong>&nbsp;<asp:Label ID="lblVencimento" runat="server" />
                </td>
                <td><strong>Nº FATURA:</strong>&nbsp;<asp:Label ID="lblFatura" runat="server" />
                </td>
            </tr>
        </table>
        <table style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>EXPORTADOR</strong>&nbsp;<asp:Label ID="lblExportador" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>REF. CLIENTE:</strong>&nbsp;<asp:Label ID="lblReferencias" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>FRETE:</strong>&nbsp;<asp:Label ID="lblFrete" runat="server" /><asp:Label ID="Label7" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>NAVIO:</strong>&nbsp;<asp:Label ID="lblNavio" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>CONHECIMENTO:</strong>&nbsp;<asp:Label ID="lblConhecimento" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>HOUSE:</strong>&nbsp;<asp:Label ID="lblHouse" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>ORIGEM:</strong>&nbsp;<asp:Label ID="lblOrigem" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>DESTINO:</strong>&nbsp;<asp:Label ID="lblDestino" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>EMBARQUE:</strong>&nbsp;<asp:Label ID="lblDataEmbarque" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="lblDataChegada" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>QTD. VOLUMES:</strong>&nbsp;<asp:Label ID="lblQtdVolumes" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>PESO BRUTO:</strong>&nbsp;<asp:Label ID="lblPesoBruto" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>CONTAINER</strong>&nbsp;<asp:Label ID="lblContainer" runat="server" />
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
           <br />
            <br />
        <div id="divConteudoDinamico" runat="server" style="border-style:solid;border-width: thin;" >
        </div>
<%--        <div style="float: right;"><asp:Label ID="lbltotal" runat="server" /></div>--%>
           <br />
            <br />
        <div>
            Horário de Pagamento: Envio de comprovante até as 13h. Pagamentos efetuados após o horário podem incidir em difereça cambial
            <br />
            <br />
            Prazo para Desbloqueio:<br />
            FCL: Em até 24 horas<br />
            LCL: Até o final do dia<br />
            (Após o pagamento e apresentação dos documentos)<br />
            <br />
            <br />

            O comprovante de pagamento deverá ser enviado para o e-mail financeiro@fcalog.com com a identificação do processo ou número do BL, para que possamos confirmá-lo e liberar a carga.
            <br />
            <br />
            <br />
            <br />
            <br />
            <strong>No aguardo do seu contato,&nbsp;<asp:Label ID="lblUsuario" runat="server" /></strong>
            <div style="float: right;">Impresso &nbsp;<asp:Label ID="lblDataImpressao" runat="server" /></div>
        </div>
</td>
                    </tr>
</table>

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
        function ImprimirND() {
            window.print();
        }
    </script>
</asp:Content>
